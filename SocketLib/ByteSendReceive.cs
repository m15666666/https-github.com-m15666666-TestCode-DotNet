namespace SocketLib
{
    /// <summary>
    /// 通过Socket发送/接收字节的类
    /// </summary>
    public class ByteSendReceive : SendReceiveBase
    {
        #region 创建对象

        protected ByteSendReceive()
        {
        }

        /// <summary>
        /// 创建ByteSendReceive对象
        /// </summary>
        /// <param name="socketWrapper">SocketWrapper，连接已经打开</param>
        /// <returns>ByteSendReceive对象</returns>
        public static ByteSendReceive CreateByteSendReceive( SocketWrapper socketWrapper )
        {
            return new ByteSendReceive { InnerSocketWrapper = socketWrapper };
        }

        #endregion
    }
}