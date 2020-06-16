using NModbus;
using System;
using System.Net;
using System.Net.Sockets;

namespace NModBus.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            ModbusTcpMasterReadInputsFromModbusSlave();
            Console.WriteLine("Input enter to exit.");
            Console.ReadLine();
        }


        /// <summary>
        ///     Modbus TCP master and slave example.
        /// </summary>
        private static void ModbusTcpMasterReadInputsFromModbusSlave()
        {
            byte slaveId = 1;
            int port = 502;
            IPAddress address = new IPAddress(new byte[] { 127, 0, 0, 1 });

            // create and start the TCP slave
            TcpListener slaveTcpListener = new TcpListener(address, port);
            slaveTcpListener.Start();

            var factory = new ModbusFactory();
            var network = factory.CreateSlaveNetwork(slaveTcpListener);

            IModbusSlave slave = factory.CreateSlave(slaveId);

            network.AddSlave(slave);

            var listenTask = network.ListenAsync();

            //slaveTcpListener.Stop();

            // output
            // Register 100=0
            // Register 101=0
            // Register 102=0
            // Register 103=0
            // Register 104=0
        }
    }
}
