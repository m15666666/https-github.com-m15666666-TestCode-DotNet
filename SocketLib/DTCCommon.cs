//All rights reserved.
// 
//
//<summary>
// 设备状态监测管理软件

//</summary>
// 
//<module>
// 数据传输通道
//</module>
//
//<modify history> 
//2009/4/14 樊仪雪 创建

namespace SocketLib
{
    /// <summary>
    /// 数据传输通道协议类型
    /// </summary>
    public enum DTCProtocolType
    {
        ///<summary>
        /// 无效数据类型
        /// </summary>
        None = 0,

        /// <summary>
        /// 登陆
        /// </summary>
        Login = 11,

        /// <summary>
        /// 退出
        /// </summary>
        Logout = 21,

        /// <summary>
        /// 数据
        /// </summary>
        Data = 31,

        /// <summary>
        /// 命令
        /// </summary>
        Command = 41,

        /// <summary>
        /// 断点续传
        /// </summary>
        Recontinue = 51,

        /// <summary>
        /// 文件
        /// </summary>
        File = 61,

        /// <summary>
        /// 心跳
        /// </summary>
        Heart = 71,

        /// <summary>
        /// 扩展类型
        /// 以后备用
        /// </summary>
        Extend = 81
    }
}