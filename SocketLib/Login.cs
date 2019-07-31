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
//2009/4/30 樊仪雪 创建
using System;

namespace SocketLib
{
    /// <summary>
    /// 登陆信息
    /// </summary>
    [Serializable]
    public class Login
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public Login()
        {
            CheckDatabaseID = false;
        }

        #region 属性字段

        /// <summary>
        /// 用户名
        /// </summary>
        public string LoginName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string LoginPwd { get; set; }

        /// <summary>
        /// 数据库ID
        /// </summary>
        public string DatabaseID { get; set; }

        /// <summary>
        /// 是否验证数据库ID，默认为false：不验证数据库ID。
        /// </summary>
        public bool CheckDatabaseID { get; set; }

        #endregion

        /// <summary>
        /// 数据库ID是否相同，如果CheckDatabaseID == false，则始终返回true
        /// </summary>
        /// <param name="databaseID">数据库ID</param>
        /// <returns>true：相同，false：不同</returns>
        public bool IsSameDatabaseID( string databaseID )
        {
            return CheckDatabaseID ? DatabaseID == databaseID : true;
        }
    }

    /// <summary>
    /// 登陆对象校验实用工具
    /// </summary>
    public static class LoginCheckUtils
    {
        /// <summary>
        /// 校验登陆对象
        /// </summary>
        /// <param name="login">Login</param>
        /// <param name="password">密码</param>
        /// <returns>null--成功，否则返回出错原因</returns>
        public static string Check( Login login, string password )
        {
            return Check( login, password, null );
        }

        /// <summary>
        /// 校验登陆对象
        /// </summary>
        /// <param name="login">Login</param>
        /// <param name="password">密码</param>
        /// <param name="databaseID">数据库标示，为null或空则不校验</param>
        /// <returns>null--成功，否则返回出错原因</returns>
        public static string Check( Login login, string password, string databaseID )
        {
            if( login == null )
            {
                return "未收到Login对象";
            }

            if( login.LoginPwd != password )
            {
                return "密码不正确";
            }

            if( !string.IsNullOrEmpty( databaseID ) )
            {
                if( !login.IsSameDatabaseID( databaseID ) )
                {
                    return string.Format( "“{0}”数据库标示不正确", databaseID );
                }
            }
            return null;
        }
    }
}