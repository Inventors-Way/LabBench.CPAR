using LabBench.CPAR.Functions;
using LabBench.Interface;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabBench.CPAR
{
    public class CPARConnection :
        IConnection
    {
        public string Value { get; }

        internal CPARConnection(string value) => Value = value;

        public IInstrument Create()
        {
            return new CPARDevice() { Connection = this };
        }

        /// <summary>
        /// This function will verify if there is actually a LIO device connected to the serial port
        /// specified in the connection string. This will be verified with the following method:
        /// 
        /// 1. Checking that the serial port exists.
        /// 2. Creating a LIODevice.
        /// 3. Opening the LIODevice.
        /// 4. Sending a DeviceIdentification message.
        /// 5. Checking that the device is compatible.
        /// 
        /// If all of these steps complete successfully then the connection will be verified, and this
        /// function will return true.
        /// 
        /// </summary>
        /// <returns>true if a LIO device is found, otherwise false</returns>
        public bool Verify(out string information)
        {
            bool retValue = false;
            CPARDevice device = null;
            information = "";

            try
            {

                if (PortAvailable)
                {
                    device = new CPARDevice() { Connection = this };
                    if (device.CreateIdentificationFunction() is DeviceIdentification identification)
                    {
                        device.Open();
                        device.Execute(identification);
                        retValue = device.IsCompatible(identification);
                        information = $"SN: {identification.SerialNumber} Firmware: {identification.Version}";
                    }
                }
            }
            catch { }
            finally
            {
                if (device is object)
                {
                    if (device.IsOpen)
                    {
                        device.Close();
                    }
                }
            }

            return retValue;
        }

        public void Test(ITestReporter reporter)
        {
            reporter.Started();
            reporter.Completed();
        }

        /// <summary>
        /// Is the connection a valid serial port on the system
        /// </summary>
        internal bool PortAvailable => SerialPort.GetPortNames().Any((p) => p == Value);
    }
}
