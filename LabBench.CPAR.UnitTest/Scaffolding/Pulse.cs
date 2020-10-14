using LabBench.Interface.Stimuli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabBench.CPAR.UnitTest.Scaffolding
{
    public class Pulse :
        IPulse
    {
        public double Is { get; set; }

        public double Tmax => Tdelay + Ts;

        public double Tmin => Tdelay;

        public double Ts { get; set; }

        public double Tdelay { get; set; }

        public double GetSlope(double time) => 0;

        public double GetValue(double time) => (time >= Tmin) && (time < Tmax) ? Is : 0;

        public void Visit(IStimulusVisitor visitor) => visitor.Accept(this);
    }
}
