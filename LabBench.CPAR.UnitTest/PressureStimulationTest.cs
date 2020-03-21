﻿using Inventors.ECP;
using LabBench.Interface.Stimuli;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using LabBench.Interface;

namespace LabBench.CPAR.UnitTest
{
    [TestClass]
    public class PressureStimulationTest
    {
        [TestMethod]
        public void KickWatchdog()
        {
            var device = TC.Device;
            var kicks = device.Ping();
            Assert.IsTrue(kicks >= 0);
        }

        [TestMethod]
        public void SinglePulse()
        {
            var device = TC.Device;
            device.Channels[0].SetStimulus(1, new Pulse() { Is = 50, Ts = 1, Tdelay = 0 });
            device.Start(AlgometerStopCriterion.STOP_CRITERION_ON_BUTTON, true);
            Thread.Sleep(2000);
            Assert.AreEqual(expected: AlgometerState.STATE_IDLE, actual: device.State);
            Assert.AreEqual(expected: AlgometerStopCondition.STOPCOND_STIMULATION_COMPLETED, actual: device.StopCondition);
        }
    }
}
