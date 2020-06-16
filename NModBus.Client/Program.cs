using System;
using NModbus;
using System;
using System.Net;
using System.Net.Sockets;

namespace NModBus.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("ModbusTcpMasterReadInputsFromModbusSlave");
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

            var factory = new ModbusFactory();

            // create the master
            TcpClient masterTcpClient = new TcpClient(address.ToString(), port);
            //masterTcpClient.Connect();
            IModbusMaster master = factory.CreateMaster(masterTcpClient);

            ushort numInputs = 5;
            ushort startAddress = 100;

            // read five register values
            ushort[] inputs = master.ReadInputRegisters(slaveId, startAddress, numInputs);

            for (int i = 0; i < numInputs; i++)
            {
                Console.WriteLine($"Register {(startAddress + i)}={(inputs[i])}");
            }

            // clean up
            masterTcpClient.Close();

            // output
            // Register 100=0
            // Register 101=0
            // Register 102=0
            // Register 103=0
            // Register 104=0
        }
    }
}
