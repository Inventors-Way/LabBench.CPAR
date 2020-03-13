using System;
using System.Collections.Generic;
using System.Text;
using LabBench.Interface;
using Inventors.ECP.Utility;
using LabBench.Interface.Stimuli;
using LabBench.Interface.Stimuli.Analysis;

namespace LabBench.CPAR
{
    public class PressureChannel :
        NotifyPropertyChanged,
        IPressureChannel
    {
        public PressureChannel(byte channel, CPARDevice device)
        {
            _channel = channel;
            _device = device;
        }

        internal void Add(double pressure)
        {

        }

        internal void Reset()
        {
            _pressures.Clear();
            Notify(nameof(Pressure));
        }

        public string Name => _channel.ToString();

        public IList<double> Pressure => throw new NotImplementedException();

        public void SetStimulus(int repeat, double period, IStimulus stimulus)
        {
            throw new NotImplementedException();
        }

        private List<double> _pressures = new List<double>();
        private byte _channel;
        private CPARDevice _device;
    }
}
