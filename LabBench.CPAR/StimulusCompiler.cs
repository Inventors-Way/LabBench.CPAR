using Inventors.CPAR.Functions;
using LabBench.Interface.Stimuli;
using LabBench.Interface.Stimuli.Analysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LabBench.CPAR
{
    public class StimulusCompiler
    {
        private class Line
        {
            public double Time { get; set; }
            public double Value { get; set; }
            public double Slope { get; set; }
            public double Length { get; set; }
            public int Count { get; set; }
        }

        private List<Line> CompileLines(IStimulus stimulus)
        {
            List<Line> retValue = new List<Line>();
            var analyser = new TimeAnalyser();
            stimulus.Visit(analyser);
            analyser.TimePoints.Sort();           
            time = analyser.TimePoints.Distinct().ToArray();

            for (int n = 0; n < time.Length - 1; ++n)
            {
                retValue.Add(new Line()
                {
                    Time = time[n],
                    Value = stimulus.GetValue(time[n]),
                    Slope = stimulus.GetSlope(time[n]),
                    Length = time[n + 1] - time[n],
                    Count = CPARDevice.TimeToCount(time[n + 1] - time[n])
                });
            }


            return retValue;
        }

        private List<Line> CleanLines(List<Line> input)
        {
            List<Line> output = new List<Line>();

            foreach (var line in input)
            {
                if (line.Count > 0)
                {
                    output.Add(line);
                }
            }

            return output;
        }

        private void CompileInstructions(List<Line> lines)
        {
            foreach (var line in lines)
            {
                if (line.Slope == 0)
                {
                    Program.Instructions.Add(SetWaveformProgram.CreateStepInstr(line.Value, line.Time));
                }
                else
                {
                    if (line.Value == 0)
                    {
                        if (line.Slope > 0)
                        {
                            Program.Instructions.Add(SetWaveformProgram.CreateIncrementInstr(line.Slope, line.Time));
                        }
                        else
                        {
                            Program.Instructions.Add(SetWaveformProgram.CreateDecrementInstr(-line.Slope, line.Time));
                        }
                    }
                    else
                    {
                        throw new ArgumentException("CPAR Does not support ramps that does not start from zero pressure");
                    }
                }
            }
        }

        public SetWaveformProgram Compile(IStimulus stimulus, double period)
        {
            Program = new SetWaveformProgram();

            if (stimulus != null)
            {
                List<Line> lines = CompileLines(stimulus);
                lines = CleanLines(lines);
                CompileInstructions(lines);
            }

            return Program;
        }

        public SetWaveformProgram Program { get; private set; }

        private double[] time;
    }
}
