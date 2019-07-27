using System.Net;

namespace Moons.Common20.PC
{
    /// <summary>
    /// 与网络相关的实用工具类
    /// </summary>
    public static class NetworkUtils
    {
        #region IP地址相关

        /// <summary>
        /// 将IP地址转化为字节数组，例如：10.3.2.1对应的数组元素下标分别为：0,1,2,3。
        /// </summary>
        /// <param name="host">ip或主机名</param>
        /// <returns>字节数组</returns>
        public static byte[] IP2Bytes( string host )
        {
            IPAddress ipAddress = Host2IPAddress( host );
            if( ipAddress == null )
            {
                return null;
            }

            int[] ints = StringUtils.ToInts( ipAddress.ToString(), StringUtils.PointChar );

            var ret = new byte[ints.Length];

            for( int index = 0; index < ret.Length; index++ )
            {
                ret[index] = (byte)ints[index];
            }

            return ret;
        }

        /// <summary>
        /// 获得IPAddress对象
        /// </summary>
        /// <param name="host">ip或主机名</param>
        /// <param name="port">端口</param>
        /// <returns>IPAddress</returns>
        public static IPAddress Host2IPAddress( string host )
        {
            IPAddress address;
            if( !IPAddress.TryParse( host, out address ) )
            {
                // 尝试解析主机名
                try
                {
                    address = Dns.GetHostEntry( host ).AddressList[0];
                }
                catch
                {
                    return null;
                }
            }

            return address;
        }

        /// <summary>
        /// 获得IPEndPoint(地址)
        /// </summary>
        /// <param name="host">ip或主机名</param>
        /// <param name="port">端口</param>
        /// <returns>IPEndPoint(地址)</returns>
        public static IPEndPoint GetEndPoint( string host, int port )
        {
            IPAddress address = Host2IPAddress( host );
            if( address == null )
            {
                return null;
            }

            return new IPEndPoint( address, port );
        }

        #endregion
    }
}