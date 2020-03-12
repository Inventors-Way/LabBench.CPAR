using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Xml.Serialization;
using Inventors.ECP;
using Inventors.ECP.Communication;
using Inventors.ECP.Utility;

namespace Inventors.CPAR.Functions
{
    public class SetWaveformProgram :
        DeviceFunction
    {
        public const int MAX_NO_OF_INSTRUCTIONS = 20;
        public const int INSTRUCTIONS_LENGTH = 5;
        public const byte FUNCTION_CODE = 0x12;
        public const double MAX_PRESSURE = 100;
        public const double UPDATE_RATE = 20;

        public enum InstructionType
        {
            NOP = 0x00,
            INC = 0x01,
            DEC = 0x02,
            STEP = 0x03
        }

        public class Instruction
        {
            private const double UPDATE_RATE = 20;
            private const double MAX_PRESSURE = 100;

            public Instruction()
            {
                encoding = new byte[INSTRUCTIONS_LENGTH];
                InstructionType = InstructionType.NOP;
                Argument = 0;
                Steps = 1;
            }

            public Instruction(InstructionType type)
            {
                encoding = new byte[INSTRUCTIONS_LENGTH];
                InstructionType = type;
                Argument = 0;
                Steps = 1;
            }

            [Category("Instruction")]
            [XmlAttribute("instruction-type")]
            public InstructionType InstructionType
            {
                get
                {
                    return (InstructionType)encoding[0];
                }
                set
                {
                    encoding[0] = (byte)value;
                }
            }

            [Category("Instruction")]
            [XmlAttribute("argument")]
            public double Argument
            {
                get
                {
                    return (((double)encoding[1]) / 256) + encoding[2];
                }
                set
                {
                    double truncated;
                    truncated = value > 255 ? 255 : value;
                    truncated = value < 0 ? 0 : value;
                    encoding[2] = (byte) Math.Truncate(truncated);
                    encoding[1] = (byte) Math.Truncate(256*truncated);
                }
            }

            [Category("Instruction")]
            [XmlAttribute("steps")]
            public ushort Steps
            {
                get
                {
                    return (ushort)(encoding[3] + encoding[4] * 256);
                }
                set
                {
                    ushort steps = (ushort) (value == 0 ? 1 : value);
                    encoding[4] = (byte)(steps >> 8);
                    encoding[3] = (byte)(steps - (ushort)(encoding[4] << 8));
                }
            }

            [Category("Encoding")]
            public byte[] Encoding
            {
                get
                {
                    return encoding;
                }
            }

            public override string ToString()
            {
                string retValue = "Instruction";

                switch (InstructionType)
                {
                    case InstructionType.INC:
                        retValue = String.Format("{0} ({1:0.000}kPa/s, {2:0.00}s)",
                                                  InstructionType.ToString(),
                                                  MAX_PRESSURE*Argument/255,
                                                  ((double)Steps)/UPDATE_RATE);
                        break;
                    case InstructionType.DEC:
                        retValue = String.Format("{0} (-{1:0.000}kPa/s, {2:0.00}s)",
                                                  InstructionType.ToString(),
                                                  MAX_PRESSURE*Argument/255,
                                                  ((double)Steps)/UPDATE_RATE);
                        break;
                    case InstructionType.STEP:
                        retValue = String.Format("{0} ({1:0.000}kPa, {2:0.00}s)",
                                                  InstructionType.ToString(),
                                                  MAX_PRESSURE*Argument/255,
                                                  ((double)Steps)/UPDATE_RATE);
                        break;
                    case InstructionType.NOP:
                        retValue = String.Format("{0} ({1:0.00}s)",
                                                  InstructionType.ToString(),
                                                  ((double)Steps)/UPDATE_RATE); ;
                        break;

                }

                return retValue;
            }

            private byte[] encoding;
        }

        public static Instruction CreateIncrementInstr(double delta, double time)
        {
            return new Instruction()
            {
                InstructionType = InstructionType.INC,
                Argument = (255 * (delta / 100)) / UPDATE_RATE,
                Steps = (ushort)(time * UPDATE_RATE)
            };
        }

        public static Instruction CreateDecrementInstr(double delta, double time)
        {
            return new Instruction()
            {
                InstructionType = InstructionType.DEC,
                Argument = (255 * (delta / 100)) / UPDATE_RATE,
                Steps = (ushort)(time * UPDATE_RATE)
            };
        }

        public static Instruction CreateStepInstr(double pressure, double time)
        {
            return new Instruction()
            {
                InstructionType = InstructionType.STEP,
                Argument = (255 * (pressure / 100)),
                Steps = (ushort)(time * UPDATE_RATE)
            };
        }

        public SetWaveformProgram() : 
            base(FUNCTION_CODE, requestLength: 0, responseLength: 1)
        {
        }

        public override FunctionDispatcher CreateDispatcher() => new FunctionDispatcher(FUNCTION_CODE, () => new SetWaveformProgram());

        public override bool Dispatch(dynamic listener) => listener.Accept(this);

        [Category("Waveform Program")]
        [XmlElement("instruction")]
        public Instruction[] Instructions { get; set; } = new Instruction[0];

        [Category("Waveform Program")]
        [XmlAttribute("channel")]
        public byte Channel
        {
            get
            {
                return channel;
            }
            set
            {
                channel = (byte) (value > 1 ? 1 : value);
            }
        }

        [Category("Waveform Program")]
        [XmlAttribute("repeat")]
        public byte Repeat
        {
            get
            {
                return repeat;
            }
            set
            {
                repeat = (byte) (value > 0 ? value : 1);
            }
        }

        [Category("Waveform Program")]
        public byte ExpectedChecksum
        {
            get
            {
                return CRC8CCITT.Calculate(SerializeInstructions());
            }
        }

        [Category("Waveform Program")]
        public double ProgramLength
        {
            get
            {
                double retValue = 0;

                foreach (var instr in Instructions)
                {
                    retValue += instr.Steps;
                }

                return retValue = Repeat * retValue/UPDATE_RATE;
            }
        }

        [Category("Control")]
        public byte ActualChecksum
        {
            get
            {
                if (Response != null)
                {
                    return Response.GetByte(0);
                }
                else
                    return 0;
            }
        }

        public override void OnSend()
        {
            var encodedInstructions = SerializeInstructions();
            Request = new Packet(FUNCTION_CODE, encodedInstructions.Length + 2);
            Request.InsertByte(0, Channel);
            Request.InsertByte(1, Repeat);

            for (int n = 0; n < encodedInstructions.Length; ++n)
            {
                Request.InsertByte(n + 2, encodedInstructions[n]);
            }
        }

        private byte[] SerializeInstructions()
        {
            int noOfInstructions = NumberOfInstructions;
            int numBytes = noOfInstructions * INSTRUCTIONS_LENGTH;
            int counter = 0;
            byte[] retValue = new byte[numBytes];

            for (int n = 0; n < noOfInstructions; ++n)
            {
                foreach (var b in Instructions[n].Encoding)
                {
                    retValue[counter] = b;
                    ++counter;
                }
            }

            return retValue;
        }

        [Category("Waveform Program")]
        public int NumberOfInstructions
        {
            get
            {
                return Instructions.Length > MAX_NO_OF_INSTRUCTIONS ?
                       MAX_NO_OF_INSTRUCTIONS :
                       Instructions.Length;
            }
        }

        public override string ToString() => "[0x12] Set Waveform Program";

        private byte channel = 0;
        private byte repeat = 1;
    }
}
