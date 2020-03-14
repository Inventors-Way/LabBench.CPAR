﻿using System;
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
            _pressures.Add(pressure);
            Notify(nameof(Pressure));
        }

        internal void Reset()
        {
            _pressures.Clear();
            Notify(nameof(Pressure));
        }

        public string Name => _channel.ToString();

        public IList<double> Pressure => _pressures.AsReadOnly();

        public double FinalPressure
        {
            get => _finalPressure;
            internal set => SetProperty(ref _finalPressure, value);
        }

        public void SetStimulus(int repeat, IStimulus stimulus)
        {
            var program = StimulusCompiler.Compile(stimulus);
            program.Channel = (byte) (_channel - 1);
            program.Repeat = (byte) repeat;
            _device.Execute(program);
        }

        private List<double> _pressures = new List<double>();
        private double _finalPressure = 0;
        private byte _channel;
        private CPARDevice _device;
    }
}
