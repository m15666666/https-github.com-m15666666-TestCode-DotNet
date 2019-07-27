using System;

namespace Moons.Common20.Communications
{
    /// <summary>
    /// IByteCommunication接口的包装器类
    /// </summary>
    public class ByteCommunicationWrapper : IDisposable
    {
        #region ctor

        public ByteCommunicationWrapper( IByteCommunication communication )
        {
            Communication = communication;
        }

        #endregion

        #region 变量和属性

        /// <summary>
        /// IByteCommunication
        /// </summary>
        public IByteCommunication Communication { get; private set; }

        /// <summary>
        /// true: connection is closed, else false
        /// </summary>
        public bool IsConnectionClosed { get; private set; }

        /// <summary>
        /// has data can read
        /// </summary>
        public bool HasData
        {
            get { return Communication != null && Communication.HasData; }
        }

        /// <summary>
        /// 标题，用于ToString()函数，输出日志时使用，不设置则返回Communication.ToString()或者空字符串。
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 发送字节数组时激发的事件
        /// </summary>
        public static event ByteBufferHandler<ByteCommunicationWrapper> SendBytesEvent;

        /// <summary>
        /// 接收字节数组时激发的事件
        /// </summary>
        public static event ByteBufferHandler<ByteCommunicationWrapper> ReceiveBytesEvent;

        #endregion

        #region 发送、接收函数

        /// <summary>
        /// 发送数据，指定每个包的最大尺寸
        /// </summary>
        /// <param name="buffer">待发送的缓冲区</param>
        /// <param name="packageSize">每个包的最大尺寸</param>
        public void SendBytes_PackageSize( byte[] buffer, int packageSize )
        {
            int length = buffer.Length;

            int offset = 0;
            while( 0 < length )
            {
                int size = Math.Min( packageSize, length );
                SendBytes( buffer, offset, size );

                length -= size;
                offset += size;
            }
        }

        /// <summary>
        /// 接收数据，指定每个包的最大尺寸
        /// </summary>
        /// <param name="buffer">缓冲区</param>
        /// <param name="packageSize">每个包的最大尺寸</param>
        public void ReceiveBytes_PackageSize( byte[] buffer, int packageSize )
        {
            int length = buffer.Length;

            int offset = 0;
            while( 0 < length )
            {
                int size = Math.Min( packageSize, length );
                ReceiveBytes( buffer, offset, size );

                length -= size;
                offset += size;
            }
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="buffer">待发送的缓冲区</param>
        public void SendBytes( byte[] buffer )
        {
            SendBytes( buffer, 0, buffer.Length );
        }

        /// <summary>
        /// 发送数据（从指定的偏移量开始）
        /// </summary>
        /// <param name="buffer">待发送的缓冲区</param>
        /// <param name="offset">偏移量</param>
        /// <param name="size">要发送的字节数</param>
        public void SendBytes( byte[] buffer, int offset, int size )
        {
            IByteCommunication communication = Communication;
            int currentOffset = offset;

            try
            {
                while( 0 < size )
                {
                    int sendCount = communication.Send( buffer, currentOffset, size );
                    if( sendCount == 0 )
                    {
                        throw new ObjectDisposedException( "ByteCommunication closed" );
                    }

                    EventUtils.FireEvent( SendBytesEvent, this, buffer, currentOffset, sendCount );

                    size -= sendCount;
                    currentOffset += sendCount;
                }
            }
            catch( ObjectDisposedException )
            {
                IsConnectionClosed = true;
                throw;
            }
        }

        /// <summary>
        /// 接收数据
        /// </summary>
        /// <param name="buffer">缓冲区</param>
        public void ReceiveBytes( byte[] buffer )
        {
            ReceiveBytes( buffer, 0, buffer.Length );
        }

        /// <summary>
        /// 接收数据，存入接收缓冲区的指定偏移量位置
        /// </summary>
        /// <param name="buffer">缓冲区</param>
        /// <param name="offset">偏移量</param>
        /// <param name="size">要接收的字节数</param>
        public void ReceiveBytes( byte[] buffer, int offset, int size )
        {
            IByteCommunication communication = Communication;
            int currentOffset = offset;

            try
            {
                while( 0 < size )
                {
                    int receiveCount = communication.Receive( buffer, currentOffset, size );
                    if( receiveCount == 0 )
                    {
                        throw new ObjectDisposedException( "ByteCommunication closed" );
                    }

                    EventUtils.FireEvent( ReceiveBytesEvent, this, buffer, currentOffset, receiveCount );

                    size -= receiveCount;
                    currentOffset += receiveCount;
                }
            }
            catch( ObjectDisposedException )
            {
                IsConnectionClosed = true;
                throw;
            }
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            DisposeUtils.Dispose( Communication );
        }

        #endregion

        public override string ToString()
        {
            string title = Title;
            if( !string.IsNullOrEmpty( title ) )
            {
                return title;
            }

            IByteCommunication communication = Communication;
            return communication != null ? communication.ToString() : string.Empty;
        }
    }
}