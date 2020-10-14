using LabBench.Interface;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabBench.CPAR
{
    public class CPARFactory :
        IConnectionFactory
    {
        public List<IConnection> GetPossibleConnections() =>
            (from port in SerialPort.GetPortNames()
             select new CPARConnection(port)).ToList<IConnection>();

        public IConnection Parse(string connection) => new CPARConnection(connection);
    }

    public static class DependencyInjector
    {
        public static string Name { get; } = "CPAR";

        public static void Run() => ConnectionManager.Register(Name, new CPARFactory());
    }
}
