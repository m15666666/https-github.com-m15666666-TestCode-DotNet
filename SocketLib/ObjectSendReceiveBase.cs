using System;
using System.Text;
using Moons.Common20;

namespace SocketLib
{
    /// <summary>
    /// 通过Socket发送/接收对象的类的基类
    /// </summary>
    public abstract class ObjectSendReceiveBase : SendReceiveBase
    {
        #region 变量和属性

        /// <summary>
        /// Socket的包头字节数
        /// </summary>
        private const int Socket_PackageHeadByteCount = 8;

        /// <summary>
        /// 包头，4位类型，4位包长  
        /// </summary>
        private readonly byte[] _byteHead = new byte[Socket_PackageHeadByteCount];

        #region 异步缓存和异常

        /// <summary>
        /// 异步读取的字节数组缓存
        /// </summary>
        public ByteBuffer ReadByteBuffer
        {
            get { return InnerSocketWrapper.ReadByteBuffer; }
        }

        /// <summary>
        /// 释放异步读取的字节数组缓存
        /// </summary>
        public void ReleaseReadByteBuffer()
        {
            ByteBuffer readByteBuffer = ReadByteBuffer;
            byte[] buffer = readByteBuffer.Buffer;
            if( buffer != null )
            {
                readByteBuffer.Clear();
                SocketLibConfig.ReturnByteArray( buffer );
            }
        }

        #endregion

        #endregion

        #region 异步事件的代理

        /// <summary>
        /// 异步读包数据成功时调用的函数
        /// </summary>
        public Action<ObjectSendReceiveBase> ReadPackageSuccess { private get; set; }

        /// <summary>
        /// 异步读包数据失败时调用的函数
        /// </summary>
        public Action<ObjectSendReceiveBase> ReadPackageFail { private get; set; }

        #endregion

        #region 数据接收

        #region 接收数据包

        /// <summary>
        /// 接收数据包
        /// </summary>
        /// <returns>数据包</returns>
        public byte[] ReceivePackage()
        {
            int type;
            return ReceivePackage( out type );
        }

        /// <summary>
        /// 接收数据包
        /// </summary>
        /// <param name="type">数据包类型</param>
        /// <returns>数据包</returns>
        private byte[] ReceivePackage( out int type )
        {
            int length;
            ReceiveHead( out type, out length );

            var ret = new byte[length];

            ReceiveBytes( ret );

            return ret;
        }

        /// <summary>
        /// 接收包头
        /// </summary>
        /// <param name="type">包类型</param>
        /// <param name="length">包长度</param>
        private void ReceiveHead( out int type, out int length )
        {
            ReceiveBytes( _byteHead );

            ParseHead( _byteHead, out type, out length );
        }

        /// <summary>
        /// 解析包头
        /// </summary>
        /// <param name="byteHead">包头字节数组</param>
        /// <param name="type">包类型</param>
        /// <param name="length">包长度</param>
        private static void ParseHead( byte[] byteHead, out int type, out int length )
        {
            type = BitConverter.ToInt32( byteHead, 0 );
            length = BitConverter.ToInt32( byteHead, 4 );
        }

        #endregion

        #region 异步接收数据包

        /// <summary>
        /// 异步接收数据包
        /// </summary>
        public void BeginReceivePackage()
        {
            BeginReceiveBytes( _byteHead, OnReceiveHeadSuccess, OnReceivePackageFail );
        }

        /// <summary>
        /// 响应异步接收数据包头的成功事件
        /// </summary>
        private void OnReceiveHeadSuccess( SocketWrapper socket )
        {
            int type;
            int length;
            ParseHead( socket.ReadBuffer, out type, out length );

            try
            {
                // 实际分配的数组长度可能大于length
                byte[] buffer = SocketLibConfig.GetMoreThanLengthByteArray( length );
                BeginReceiveBytes( buffer, 0, length, OnReceiveBodySuccess, OnReceivePackageFail );
            }
            catch( Exception ex )
            {
                OnReceivePackageFail( ex );
            }
        }

        /// <summary>
        /// 响应异步接收数据包体的成功事件
        /// </summary>
        private void OnReceiveBodySuccess( SocketWrapper socketWrapper )
        {
            FireEvent( ReadPackageSuccess );
        }

        /// <summary>
        /// 响应异步接收数据包的失败事件
        /// </summary>
        private void OnReceivePackageFail( SocketWrapper socketWrapper )
        {
            OnReceivePackageFail( socketWrapper.InnerException );
        }

        /// <summary>
        /// 响应异步接收数据包的失败事件
        /// </summary>
        private void OnReceivePackageFail( Exception ex )
        {
            InnerException = ex;
            FireEvent( ReadPackageFail );
        }

        #endregion

        #endregion

        #region 数据发送

        /// <summary>
        /// 发送数据包
        /// </summary>
        /// <param name="packageBody">包内容</param>
        public void SendPackage( byte[] packageBody )
        {
            SendPackage( packageBody, 1 );
        }

        /// <summary>
        /// 发送数据包
        /// </summary>
        /// <param name="packageBody">包内容</param>
        /// <param name="type">数据包类型</param>
        private void SendPackage( byte[] packageBody, int type )
        {
            SendHead( type, packageBody.Length );

            SendBytes( packageBody );
        }

        /// <summary>
        /// 发送包头
        /// </summary>
        /// <param name="type">数居包类型</param> 
        /// <param name="length">数据包长度</param>
        private void SendHead( int type, int length )
        {
            SendBytes( BitConverter.GetBytes( type ) );
            SendBytes( BitConverter.GetBytes( length ) );
        }

        #endregion

        #region 服务器回应

        /// <summary>
        /// 服务器回应的字节数组
        /// </summary>
        private static readonly byte[] _serverReplay = Encoding.ASCII.GetBytes( "ServerReplay" );

        /// <summary>
        /// 发送服务器的回应
        /// </summary>
        public void SendServerReplay()
        {
            SendBytes( _serverReplay );
        }

        /// <summary>
        /// 接收服务器的回应
        /// </summary>
        public void ReceiveServerReplay()
        {
            BeginReceiveBytes( new byte[_serverReplay.Length], null, null );

            //Receive( _serverReplay.Length );
        }

        #endregion
    }
}