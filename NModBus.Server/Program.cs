using Microsoft.Extensions.Configuration;
using NModbus;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace NModBus.Server
{
    /// <summary>
    /// 基于NModBus建立一个独立的modbus服务器程序
    /// https://stackoverflow.com/questions/177856/how-do-i-trap-ctrl-c-sigint-in-a-c-sharp-console-app
    /// https://docs.microsoft.com/en-us/dotnet/api/system.console.cancelkeypress?redirectedfrom=MSDN&view=net-5.0
    /// https://www.jianshu.com/p/2c53c9d17c66
    ///
    /// </summary>
    internal class Program
    {
        private static ModbusServer _modbusServer;
        private static ManualResetEvent _exitEvent = new ManualResetEvent(false);
        private static IConfigurationRoot _configuration;

        private static void InitConfig()
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false);

            _configuration = builder.Build();
        }

        private static void Main(string[] args)
        {
            int port = 1286;
            try
            {
                InitConfig();

                int.TryParse(_configuration["Modbus:Port"], out port);
                int slaveCount = 1;
                int.TryParse(_configuration["Modbus:SlaveCount"], out slaveCount);

                var server = _modbusServer = new ModbusServer();
                server.Port = port;
                server.SlaveCount = 1;
                server.StartSever();

                //ModbusTcpMasterReadInputsFromModbusSlave();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Start Server error." + ex.ToString());
                _modbusServer.StopServer();
                Console.WriteLine("Press any key to exit.");
                Console.ReadKey();
                return;
            }

            Console.Clear();
            Console.CancelKeyPress += OnCtrlC;
            Console.WriteLine($"Application started, port: {port}. Press Ctrl+C to shutdown.");

            _exitEvent.WaitOne();
            Console.WriteLine("Application shutdown.");
            Console.WriteLine("Press any key to exit.");
        }

        private static void OnCtrlC(object sender, ConsoleCancelEventArgs e)
        {
            e.Cancel = true;
            _modbusServer.StopServer();
            _exitEvent.Set();
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