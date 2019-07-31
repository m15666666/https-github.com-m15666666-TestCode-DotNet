using System;
using System.Threading;

namespace SocketLib
{
    /// <summary>
    /// 心跳包发送者
    /// </summary>
    public class HeartbeatSender : IDisposable
    {
        #region 变量和属性

        /// <summary>
        /// 内部错误
        /// </summary>
        public Exception InnerException { get; set; }

        /// <summary>
        /// ObjectSendReceive
        /// </summary>
        public ObjectSendReceive SendReceive { get; set; }

        /// <summary>
        /// 心跳包间隔（毫秒），默认1秒钟
        /// </summary>
        public int HeartbeatIntervalMSec { get; set; }

        /// <summary>
        /// 发送心跳包的线程
        /// </summary>
        private Thread _threadSendHeartbeat;

        /// <summary>
        /// 是否继续发送心跳包
        /// </summary>
        public bool ContinueSend { get; set; }

        public HeartbeatSender()
        {
            HeartbeatIntervalMSec = 1000;
        }

        #endregion

        /// <summary>
        /// 开始发送心跳包
        /// </summary>
        public void StartSendHeartbeat()
        {
            ContinueSend = true;
            _threadSendHeartbeat = new Thread( SendHearbeat ) { Name = "HeartbeatSender_SendHearbeat" };
            _threadSendHeartbeat.Start();
        }

        /// <summary>
        /// 发送心跳包
        /// </summary>
        private void SendHearbeat()
        {
            try
            {
                while( ContinueSend )
                {
                    Thread.Sleep( HeartbeatIntervalMSec );

                    if( SendReceive != null )
                    {
                        SendReceive.SendHearbeat();
                    }
                }
            }
            catch( Exception ex )
            {
                InnerException = ex;
            }
        }

        /// <summary>
        /// 停止发送心跳包
        /// </summary>
        public void StopSendHeartbeat()
        {
            ContinueSend = false;
            _threadSendHeartbeat.Join();
        }

        #region Implementation of IDisposable

        public void Dispose()
        {
            StopSendHeartbeat();
        }

        #endregion
    }
}
