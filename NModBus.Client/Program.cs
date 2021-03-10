using System;
using NModbus;
using System;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;

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
            int port = 1286;
            IPAddress address = new IPAddress(new byte[] { 127, 0, 0, 1 });

            var factory = new ModbusFactory();

            // create the master
            TcpClient masterTcpClient = new TcpClient(address.ToString(), port);
            //masterTcpClient.Connect();
            IModbusMaster master = factory.CreateMaster(masterTcpClient);

            Stopwatch sw = new Stopwatch();
            ushort numInputs = 5;
            ushort startAddress = 0;// 100;

            #region 测试写入速度
            //{
            //    sw.Restart();
            //    ushort addr1 = 0;
            //    for (ushort i = 1; i < ushort.MaxValue; i++)
            //    {
            //        ushort v = i;
            //        master.WriteSingleRegister(slaveId, addr1, v);
            //        addr1++;
            //    }
            //    sw.Stop();
            //    Console.WriteLine($"write speed: {sw.ElapsedMilliseconds} ms, {sw.ElapsedMilliseconds / (double)ushort.MaxValue} ms");
            //}
            #endregion
            #region 测试读取速度
            //{
            //    sw.Restart();
            //    for (ushort addr1 = 0; addr1 < ushort.MaxValue; addr1++)
            //    {
            //        master.ReadHoldingRegisters(slaveId, addr1, 1);
            //    }
            //    sw.Stop();
            //    Console.WriteLine($"write speed: {sw.ElapsedMilliseconds} ms, {sw.ElapsedMilliseconds / (double)ushort.MaxValue} ms");
            //}
            #endregion

            ushort slaveCount = 1;// 2;
            for (byte id = 1; id <= slaveCount; id++)
            {
                ushort addr = startAddress;
                for (ushort i = 1; i <= numInputs; i++)
                {
                    ushort v = i;
                    master.WriteSingleRegister(id, addr, v);
                    var r1 = master.ReadHoldingRegisters(id, addr, 1);
                    var r2 = master.ReadInputRegisters(id, addr, 1);
                    addr++;
                }
            }

            // read five register values
            for (byte id = 1; id <= slaveCount; id++)
            {
                ushort[] inputs = master.ReadInputRegisters(id, startAddress, numInputs);
                for (int i = 0; i < numInputs; i++)
                    Console.WriteLine($"Register {(startAddress + i)}={(inputs[i])}");

                inputs = master.ReadHoldingRegisters(id, startAddress, numInputs);
                for (int i = 0; i < numInputs; i++)
                    Console.WriteLine($"Register {(startAddress + i)}={(inputs[i])}");
            }

            //sw.Stop();
            //Console.WriteLine($"{sw.ElapsedMilliseconds} ms");
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
