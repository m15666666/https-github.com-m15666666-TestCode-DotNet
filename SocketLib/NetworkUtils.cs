using System.Net;
using System.Runtime.InteropServices;

namespace SocketLib
{
    /// <summary>
    /// 网络的实用工具
    /// </summary>
    public static class NetworkUtils
    {
        #region 是否连接上了网络

        /// <summary>
        /// Retrieves the connected state of the local system
        /// </summary>
        /// <param name="connectionDescription">Pointer to a variable that receives the connection description. This parameter can be one or more of the following values</param>
        /// <param name="reservedValue">Reserved. Must be zero</param>
        /// <returns>Returns TRUE if there is an Internet connection, or FALSE otherwise</returns>
        [DllImport( "wininet.dll" )]
        private static extern bool InternetGetConnectedState( out int connectionDescription, int reservedValue );

        /// <summary>
        /// 是否连接上了网络
        /// </summary>
        public static bool NetworkConnected => InternetGetConnectedState( out int connectionDescription, 0 );

        /// <summary>
        /// 未连接上网络
        /// </summary>
        public static bool NetworkNotConnected => !NetworkConnected;

        #endregion

        #region 从IP地址或者域名或者主机地址获得IP

        /// <summary> 
        /// 从IP地址或者域名或者主机地址获得IP
        /// var ips = Dns.GetHostAddresses("10.3.2.188"); //返回10.3.2.188
        /// ips = Dns.GetHostAddresses("127.0.0.1"); // 返回127.0.0.1
        /// ips = Dns.GetHostAddresses("www.baidu.com"); // 返回一个ip数组，包括百度实际使用的两个ip地址
        /// </summary>
        /// <param name="hostNameOrIpOrDnsname">主机名或者ip地址或者dns域名</param>
        /// <returns></returns>
        public static IPAddress[] GetIPs(string hostNameOrIpOrDnsname) => Dns.GetHostAddresses(hostNameOrIpOrDnsname);

        #endregion
    }
}
