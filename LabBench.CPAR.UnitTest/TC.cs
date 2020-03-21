using Inventors.ECP;
using System;
using System.Collections.Generic;
using System.Text;

namespace LabBench.CPAR.UnitTest
{
    public class TC : IDisposable
    {
        private static TC instance;
        private CPARDevice device;

        private TC(string port)
        {
            device = new CPARDevice()
            {
                Location = Location.Parse(port)
            };
            device.Open();
        }

        private static TC Instance
        {
            get
            {
                if (instance is null)
                {
                    instance = new TC("COM18");
                }

                return instance;
            }
        }

        public static CPARDevice Device => Instance.device;

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (device is object)
                    {
                        device.Close();
                    }
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
