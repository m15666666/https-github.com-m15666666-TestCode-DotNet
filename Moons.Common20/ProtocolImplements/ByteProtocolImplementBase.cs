using Moons.Common20.Communications;
using Moons.Common20.Serialization;

namespace Moons.Common20.ProtocolImplements
{
    /// <summary>
    /// 基于字节协议的实现类的基类
    /// </summary>
    public abstract class ByteProtocolImplementBase
    {
        #region 变量和属性

        /// <summary>
        /// ByteCommunicationWrapper
        /// </summary>
        public ByteCommunicationWrapper Communication { get; set; }

        /// <summary>
        /// true: connection is closed, else false
        /// </summary>
        public bool IsConnectionClosed
        {
            get { return Communication == null || Communication.IsConnectionClosed; }
        }

        /// <summary>
        /// has data can read
        /// </summary>
        public bool HasData
        {
            get { return Communication != null && Communication.HasData; }
        }

        /// <summary>
        /// 每个包的最大尺寸
        /// </summary>
        public int PackageSize { get; set; }

        #endregion

        #region 发送、接收基础函数

        /// <summary>
        /// 发送数据，指定每个包的最大尺寸
        /// </summary>
        /// <param name="buffer">待发送的缓冲区</param>
        public void SendBytes_PackageSize( byte[] buffer )
        {
            int packageSize = PackageSize;
            if( 0 < packageSize )
            {
                Communication.SendBytes_PackageSize( buffer, packageSize );
                return;
            }
            Communication.SendBytes( buffer );
        }

        /// <summary>
        /// 接收数据，指定每个包的最大尺寸
        /// </summary>
        /// <param name="buffer">缓冲区</param>
        public void ReceiveBytes_PackageSize( byte[] buffer )
        {
            int packageSize = PackageSize;
            if( 0 < packageSize )
            {
                Communication.ReceiveBytes_PackageSize( buffer, packageSize );
                return;
            }
            Communication.ReceiveBytes( buffer );
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
            Communication.SendBytes( buffer, offset, size );
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
            Communication.ReceiveBytes( buffer, offset, size );
        }

        #endregion

        #region ProtocolToHost 接收函数

        #region ProtocolToHostInt16

        /// <summary>
        /// 将协议的字节转化为本机的Int16类型
        /// </summary>
        /// <param name="buffer">字节数组</param>
        /// <param name="offset">偏移</param>
        /// <returns>转化后的数值</returns>
        protected virtual short ProtocolToHostInt16( byte[] buffer, int offset )
        {
            int index = offset;
            return ProtocolToHost( ByteUtils.ToInt16( buffer, ref index ) );
        }

        /// <summary>
        /// 将协议的字节转化为本机的Int16类型
        /// </summary>
        /// <param name="reader">IBinaryRead</param>
        /// <returns>转化后的数值</returns>
        protected virtual short ProtocolToHostInt16( IBinaryRead reader )
        {
            return ProtocolToHost( reader.ReadInt16() );
        }

        #endregion

        #region ProtocolToHostInt32

        /// <summary>
        /// 将协议的字节转化为本机的Int32类型
        /// </summary>
        /// <param name="buffer">字节数组</param>
        /// <param name="offset">偏移</param>
        /// <returns>转化后的数值</returns>
        protected virtual int ProtocolToHostInt32( byte[] buffer, int offset )
        {
            int index = offset;
            return ProtocolToHost( ByteUtils.ToInt32( buffer, ref index ) );
        }

        /// <summary>
        /// 将协议的字节转化为本机的Int32类型
        /// </summary>
        /// <param name="reader">IBinaryRead</param>
        /// <returns>转化后的数值</returns>
        protected virtual int ProtocolToHostInt32( IBinaryRead reader )
        {
            return ProtocolToHost( reader.ReadInt32() );
        }

        #endregion

        #region ProtocolToHostInt64

        /// <summary>
        /// 将协议的字节转化为本机的Int64类型
        /// </summary>
        /// <param name="buffer">字节数组</param>
        /// <param name="offset">偏移</param>
        /// <returns>转化后的数值</returns>
        protected virtual long ProtocolToHostInt64(byte[] buffer, int offset) {
            int index = offset;
            return ProtocolToHost(ByteUtils.ToInt64(buffer, ref index));
        }

        /// <summary>
        /// 将协议的字节转化为本机的Int64类型
        /// </summary>
        /// <param name="reader">IBinaryRead</param>
        /// <returns>转化后的数值</returns>
        protected virtual long ProtocolToHostInt64(IBinaryRead reader) {
            return ProtocolToHost(reader.ReadInt64());
        }

        #endregion

        #region ProtocolToHost

        /// <summary>
        /// 将协议的数值转化为本机的类型，协议默认是大端
        /// </summary>
        /// <param name="value">数值</param>
        /// <returns>转化后的数值</returns>
        protected virtual short ProtocolToHost( short value )
        {
            return ByteUtils.BigEndianToHost( value );
        }

        /// <summary>
        /// 将协议的数值转化为本机的类型，协议默认是大端
        /// </summary>
        /// <param name="value">数值</param>
        /// <returns>转化后的数值</returns>
        protected virtual int ProtocolToHost( int value )
        {
            return ByteUtils.BigEndianToHost( value );
        }

        /// <summary>
        /// 将协议的数值转化为本机的类型，协议默认是大端
        /// </summary>
        /// <param name="value">数值</param>
        /// <returns>转化后的数值</returns>
        protected virtual long ProtocolToHost(long value) {
            return ByteUtils.BigEndianToHost(value);
        }
        #endregion

        #endregion
    }
}