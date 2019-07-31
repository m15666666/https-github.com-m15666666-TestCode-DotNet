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

using System;

namespace SocketLib
{
    /// <summary>
    /// 共通数据包
    /// </summary>
    [Serializable]
    public class CommonDataPackage
    {
        /// <summary>
        /// 发送协议类型        /// </summary>
        private DTCProtocolType dtcProtocolType = DTCProtocolType.None;

        /// <summary>
        /// 发送的字符串数据
        /// 内部使用信息
        /// </summary>
        private string objString = string.Empty;

        #region 创建CommonDataPackage对象

        /// <summary>
        /// 创建登陆CommonDataPackage对象
        /// </summary>
        /// <param name="password">密码</param>
        /// <param name="databaseID">数据库标示</param>
        /// <returns>登陆CommonDataPackage对象</returns>
        public static CommonDataPackage CreateLogin( string password, string databaseID )
        {
            var login = new Login { LoginPwd = password, DatabaseID = databaseID, CheckDatabaseID = true };
            return CreateLogin( login );
        }

        /// <summary>
        /// 创建登陆CommonDataPackage对象
        /// </summary>
        /// <param name="password">密码</param>
        /// <returns>登陆CommonDataPackage对象</returns>
        public static CommonDataPackage CreateLogin( string password )
        {
            return CreateLogin( new Login { LoginPwd = password } );
        }

        /// <summary>
        /// 创建登陆CommonDataPackage对象
        /// </summary>
        /// <param name="login">Login</param>
        /// <returns>登陆CommonDataPackage对象</returns>
        public static CommonDataPackage CreateLogin( Login login )
        {
            return new CommonDataPackage
                       {
                           Protocol = DTCProtocolType.Login,
                           InnerData = login
                       };
        }

        #endregion

        #region 属性
        public DTCProtocolType Protocol
        {
            get { return dtcProtocolType; }
            set { dtcProtocolType = value; }
        }

        public string ObjectString
        {
            get { return objString; }
            set { objString = value; }
        }

        /// <summary>
        /// 内部包装的数据
        /// </summary>
        public Object InnerData { get; set; }

        #endregion

        /// <summary>
        /// 获得内部数据对象
        /// </summary>
        /// <typeparam name="T">内部数据对象类型</typeparam>
        /// <returns>内部数据对象</returns>
        public T GetData<T>() where T : class
        {
            return InnerData as T;
        }
    }
}