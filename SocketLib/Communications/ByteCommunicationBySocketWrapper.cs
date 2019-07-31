using Moons.Common20;
using Moons.Common20.Communications;

namespace SocketLib.Communications
{
    /// <summary>
    /// 通过SocketWrapper实现的字节通讯类
    /// </summary>
    public class ByteCommunicationBySocketWrapper : IByteCommunication
    {
        #region ctor

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="socket">SocketWrapper</param>
        /// <param name="isSocketOwner">true：是Socket拥有者，负责释放Socket，false：不负责释放</param>
        public ByteCommunicationBySocketWrapper( SocketWrapper socket, bool isSocketOwner )
        {
            _socket = socket;
            _isSocketOwner = isSocketOwner;
        }

        #endregion

        #region 变量和属性

        /// <summary>
        /// true：是Socket拥有者，负责释放Socket，false：不负责释放
        /// </summary>
        private readonly bool _isSocketOwner;

        /// <summary>
        /// 内部socket对象
        /// </summary>
        private readonly SocketWrapper _socket;

        #endregion

        #region Implementation of IDisposable

        public void Dispose()
        {
            if( _isSocketOwner )
            {
                DisposeUtils.Dispose( _socket );
            }
        }

        #endregion

        #region Implementation of IByteCommunication

        public bool HasData
        {
            get { return _socket.DataAvailable; }
        }

        public int Send( byte[] buffer, int offset, int size )
        {
            return _socket.Send( buffer, offset, size );
        }

        public int Receive( byte[] buffer, int offset, int size )
        {
            return _socket.Receive( buffer, offset, size );
        }

        #endregion
    }
}