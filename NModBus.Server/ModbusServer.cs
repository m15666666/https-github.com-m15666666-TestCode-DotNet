using NModbus;
using System;
using System.Net;
using System.Net.Sockets;

namespace NModBus.Server
{
    /// <summary>
    /// modbus服务器类
    /// </summary>
    public class ModbusServer
    {
        /// <summary>
        /// 绑定的端口
        /// </summary>
        public int Port { get; set; } = 1286;

        /// <summary>
        /// 包含的slave数量，每个slave可以访问0～65535这些地址
        /// </summary>
        public int SlaveCount { get; set; } = 1;

        private TcpListener _slaveTcpListener;
        private IModbusSlaveNetwork _network;
        private readonly object _lock = new object();

        /// <summary>
        ///    开启服务器
        /// </summary>
        public void StartSever()
        {
            //StopServer();

            try
            {
                lock (_lock)
                {
                    Console.WriteLine("Starting Server ..." );
                    int port = Port;

                    _slaveTcpListener = new TcpListener(IPAddress.Any, port);
                    _slaveTcpListener.Start();

                    var factory = new ModbusFactory();
                    _network = factory.CreateSlaveNetwork(_slaveTcpListener);

                    for (byte slaveId = 1; slaveId <= SlaveCount; slaveId++)
                    {
                        IModbusSlave slave = factory.CreateSlave(slaveId);
                        _network.AddSlave(slave);
                    }

                    _network.ListenAsync();
                    Console.WriteLine("Server started." );
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("StartServer error." + ex.ToString());
                _slaveTcpListener?.Stop();
                _slaveTcpListener = null;
            }
        }

        /// <summary>
        /// 关闭服务器
        /// </summary>
        public void StopServer()
        {
            lock (_lock)
            {
                try
                {
                    var listener = _slaveTcpListener;
                    if (listener == null) return;
                    Console.WriteLine("Stoping Server ..." );

                    _slaveTcpListener = null;

                    _network?.Dispose();
                    _network = null;

                    listener.Stop();
                    Console.WriteLine("Server stoped." );
                }
                catch (Exception ex)
                {
                    Console.WriteLine("StopServer error." + ex.ToString());
                }
            }
        }
    }
}