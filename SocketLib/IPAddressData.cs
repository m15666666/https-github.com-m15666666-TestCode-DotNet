using System;

namespace SocketLib
{
    /// <summary>
    /// IP地址数据类
    /// </summary>
    [Serializable]
    public class IPAddressData
    {
        /// <summary>
        /// IP
        /// </summary>
        public string IP { get; set; }

        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// 登陆密码
        /// </summary>
        public string Password { get; set; }

        public override string ToString()
        {
            return string.Format( "IPAddress({0}:{1})", IP, Port );
        }
    }
}