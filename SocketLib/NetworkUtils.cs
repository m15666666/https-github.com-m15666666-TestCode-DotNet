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
        public static bool NetworkConnected
        {
            get
            {
                int connectionDescription;
                return InternetGetConnectedState( out connectionDescription, 0 );
            }
        }

        /// <summary>
        /// 未连接上网络
        /// </summary>
        public static bool NetworkNotConnected
        {
            get { return !NetworkConnected; }
        }

        #endregion
    }
}
