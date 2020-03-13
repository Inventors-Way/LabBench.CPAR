using System;
using System.Collections.Generic;
using System.Text;
using Inventors.CPAR.Functions;
using Inventors.CPAR.Messages;
using Inventors.ECP;
using Inventors.ECP.Communication;
using Inventors.ECP.Functions;
using Inventors.Logging;
using LabBench.Interface;
using System.Threading;
using System.Linq;

namespace LabBench.CPAR
{
    public class CPARDevice :
        Device,
        IPressureAlgometer
    {
        #region Device implementation
        private bool _ping = false;

        private bool Ping
        {
            get { lock (this) { return _ping; } }
            set { lock (this) { _ping = value; } }
        }


        public CPARDevice() :
            base(new SerialPortLayer())
        {
            BaudRate = 38400;

            FunctionList.Add(new DeviceIdentification());
            FunctionList.Add(new Ping());
            FunctionList.Add(new GetEndianness());

            FunctionList.Add(new SetWaveformProgram());
            FunctionList.Add(new StartStimulation());
            FunctionList.Add(new StopStimulation());
            FunctionList.Add(new WriteSerialNumber());
            FunctionList.Add(new WriteCalibration());
            FunctionList.Add(new ReadCalibration());
            FunctionList.Add(new ForceStartStimulation());


            Master.Add(new StatusMessage());
            Master.Add(new EventMessage());

            timer = new Timer(OnTimer, this, 500, 500);
            _channels.Add(new PressureChannel(1, this));
            _channels.Add(new PressureChannel(2, this));
        }

        public override bool IsCompatible(DeviceIdentification identification)
        {
            if (identification == null)
            {
                throw new ArgumentNullException(nameof(identification));
            }

            return (identification.ManufactureID == Manufacturer.Nocitech) && 
                   (identification.DeviceID == 1) &&
                   (identification.MajorVersion >= 7);
        }

        public void Accept(EventMessage msg)
        {
            Log.Debug(msg.Code.ToString());
        }

        public void Accept(StatusMessage msg)
        {
            
        }

        #endregion
        #region Device Definitions
        public enum PressureType
        {
            SUPPLY_PRESSURE = 0,
            STIMULATING_PRESSURE
        }

        public static readonly int UPDATE_RATE = 20;
        public static readonly double MAX_PRESSURE = 100;
        public static readonly double MAX_SUPPLY_PRESSURE = 1000;
        public static readonly double MAX_SCORE = 10;

        public static int TimeToRate(double time) => (int)Math.Round(time * UPDATE_RATE);

        public static double BinaryToPressure(byte x, PressureType type = PressureType.STIMULATING_PRESSURE) =>
            type == PressureType.SUPPLY_PRESSURE ? 
            MAX_SUPPLY_PRESSURE * ((double)x) / byte.MaxValue : 
            MAX_PRESSURE * ((double)x) / byte.MaxValue;

        public static double BinaryToScore(byte x) => MAX_SCORE * ((double)x) / byte.MaxValue;
        public static double PressureToBinary(double pressure) => (255.0 / 100.0) * pressure;
        public static double DeltaPressureToBinary(double delta) => (255.0 / 100.0) * (delta / UPDATE_RATE);
        public static double CountToTime(int count) => ((double)count) / UPDATE_RATE;
        public static ushort TimeToCount(double time) => (ushort)Math.Ceiling(time * UPDATE_RATE);
        #endregion
        #region IPressureAlgometer
        private Timer timer;

        private static void OnTimer(object state)
        {
            if (state is CPARDevice device)
            {
                if (device.Ping)
                {
                    device.Execute(new Ping());
                }
            }
        }

        double IPressureAlgometer.MaximalPressure => MAX_PRESSURE;

        double IPressureAlgometer.SamplePeriod => 1 / ((double)UPDATE_RATE);

        private AlgometerState _state = AlgometerState.STATE_IDLE;

        public AlgometerState State
        {
            private set => SetProperty(ref _state, value);
            get => _state;
        }

        private string _error;

        public string Error
        {
            private set => SetProperty(ref _error, value);
            get => _error;
        }

        private double _supplyPressure;

        public double SupplyPressure
        {
            private set => SetProperty(ref _supplyPressure, value);
            get => _supplyPressure;
        }

        public IList<double> VasScore => vasScore.AsReadOnly();

        private StopCondition _stopCondition;

        public StopCondition StopCondition
        {
            private set => SetProperty(ref _stopCondition, value);
            get => _stopCondition;
        }

        public IList<IPressureChannel> Channels => (from c in _channels select c as IPressureChannel).ToList();

        public void Start(StopCriterion criterion, bool forcedStart)
        {
            if (forcedStart)
            {
                var function = new ForceStartStimulation()
                {
                    Criterion = criterion == StopCriterion.STOP_CRITERION_ON_BUTTON ?
                                ForceStartStimulation.StopCriterion.STOP_CRITERION_ON_BUTTON :
                                ForceStartStimulation.StopCriterion.STOP_CRITERION_ON_BUTTON_VAS
                };
                Execute(function);
                Ping = true;
            }
            else
            {
                var function = new StartStimulation()
                {
                    Criterion = criterion == StopCriterion.STOP_CRITERION_ON_BUTTON ?
                                             StartStimulation.StopCriterion.STOP_CRITERION_ON_BUTTON :
                                             StartStimulation.StopCriterion.STOP_CRITERION_ON_BUTTON_VAS

                };
                Execute(function);
                Ping = true;
            }
            
        }

        public void Stop()
        {
            Execute(new StopStimulation());
            Ping = false;
        }

        public void Reset()
        {
            vasScore.Clear();
            Notify(nameof(VasScore));
            _channels.ForEach((c) => c.Reset());
        }

        private readonly List<double> vasScore = new List<double>();
        private List<PressureChannel> _channels = new List<PressureChannel>();
        #endregion
    }
}
