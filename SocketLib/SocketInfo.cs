using System.Net;
using System.Net.Sockets;

namespace SocketLib
{
    /// <summary>
    /// 记录Socket连接的信息
    /// </summary>
    public class SocketInfo
    {
        #region 变量和属性

        /// <summary>
        /// 输出IP和端口的格式
        /// </summary>
        private const string IPPortFormat = "{0}:{1}";

        /// <summary>
        /// 远端的IP
        /// </summary>
        public string RemoteIP { get; set; }

        /// <summary>
        /// 远端的端口
        /// </summary>
        public int RemotePort { get; set; }

        /// <summary>
        /// 远端的IP和端口
        /// </summary>
        public string RemoteIPPort
        {
            get { return string.Format( IPPortFormat, RemoteIP, RemotePort ); }
        }

        /// <summary>
        /// 本地的IP
        /// </summary>
        public string LocalIP { get; set; }

        /// <summary>
        /// 本地的端口
        /// </summary>
        public int LocalPort { get; set; }

        /// <summary>
        /// 本地的IP和端口
        /// </summary>
        public string LocalIPPort
        {
            get { return string.Format( IPPortFormat, LocalIP, LocalPort ); }
        }

        /// <summary>
        /// IP和端口的信息
        /// </summary>
        public string IPPortInfo
        {
            get { return string.Format( "(Remote = {0}, Local = {1})", RemoteIPPort, LocalIPPort ); }
        }

        #endregion

        /// <summary>
        /// 初始化信息对象
        /// </summary>
        /// <param name="socket">Socket</param>
        public void Init( Socket socket )
        {
            try
            {
                var endPoint = socket.RemoteEndPoint as IPEndPoint;
                if( endPoint != null )
                {
                    RemotePort = endPoint.Port;
                    RemoteIP = endPoint.Address.ToString();
                }
            }
            catch
            {
            }

            InitLocalEndPointInfo( socket );
        }

        /// <summary>
        /// 初始化端点信息
        /// </summary>
        /// <param name="socket">Socket</param>
        private void InitLocalEndPointInfo( Socket socket )
        {
            if( socket == null )
            {
                return;
            }

            try
            {
                var endPoint = socket.LocalEndPoint as IPEndPoint;
                if( endPoint != null )
                {
                    LocalPort = endPoint.Port;
                    LocalIP = endPoint.Address.ToString();
                }
            }
            catch
            {
            }
        }
    }
}