using LabBench.Interface.Stimuli;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabBench.CPAR.UnitTest.Scaffolding
{
    public class Ramp :
        IRamp
    {
        public double Is { get; set; }

        public double Ts { get; set; }

        public double Tdelay { get; set; }

        public double Tmax => Tdelay + Ts;

        public double Tmin => Tdelay;

        private bool IsActive(double time) => (time >= Tmin) && (time < Tmax);

        public double GetSlope(double time) => IsActive(time) ? Is / (Tmax - Tmin) : 0;

        public double GetValue(double time) => IsActive(time) ? Is * (time - Tmin) / Ts : 0;

        public void Visit(IStimulusVisitor visitor) => visitor.Accept(this);
    }
}
