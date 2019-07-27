namespace Moons.Common20.DataBase
{
    /// <summary>
    /// ADO.NET数据库连接字符串的实用工具类
    /// </summary>
    public static class ConnectionStringAdoNetUtils
    {
        #region 创建连接字符串

        /// <summary>
        /// 创建SqlServer的连接字符串
        /// </summary>
        /// <param name="serverName">服务器名/IP地址</param>
        /// <param name="dataBaseName">数据库名</param>
        /// <param name="userName">用户名/ID</param>
        /// <param name="password">密码</param>
        /// <returns>SqlServer的连接字符串</returns>
        public static string CreateConnectionString_SqlServer( string serverName, string dataBaseName, string userName,
                                                               string password )
        {
            const string Format = "Data Source={0};Initial Catalog={1};User Id={2};Password={3};";
            return string.Format( Format, serverName, dataBaseName, userName, password );
        }

        /// <summary>
        /// 创建受信的SqlServer的连接字符串
        /// </summary>
        /// <param name="serverName">服务器名/IP地址</param>
        /// <param name="dataBaseName">数据库名</param>
        /// <returns>SqlServer的连接字符串</returns>
        public static string CreateConnectionStringIntegratedSecurity_SqlServer( string serverName, string dataBaseName )
        {
            const string Format = "Data Source={0};Initial Catalog={1};Integrated Security=SSPI;";
            return string.Format( Format, serverName, dataBaseName );
        }

        /// <summary>
        /// 创建Oracle的连接字符串
        /// </summary>
        /// <param name="dataSource">数据源名</param>
        /// <param name="userName">用户名/ID</param>
        /// <param name="password">密码</param>
        /// <returns>Oracle的连接字符串</returns>
        public static string CreateConnectionString_Oracle( string dataSource, string userName, string password )
        {
            const string Format = "Data Source={0};User Id={1};Password={2};";
            return string.Format( Format, dataSource, userName, password );
        }

        /// <summary>
        /// 创建受信的Oracle的连接字符串
        /// </summary>
        /// <param name="dataSource">数据源名</param>
        /// <returns>Oracle的连接字符串</returns>
        public static string CreateConnectionStringIntegratedSecurity_Oracle( string dataSource )
        {
            const string Format = "Data Source={0};Integrated Security=SSPI;";
            return string.Format( Format, dataSource );
        }

        /// <summary>
        /// 创建Sqlite的连接字符串
        /// </summary>
        /// <param name="dbFilePath">数据库文件路径</param>
        /// <returns>Sqlite的连接字符串</returns>
        public static string CreateConnectionString_Sqlite( string dbFilePath )
        {
            const string Format = "data source={0};";
            return string.Format( Format, dbFilePath );
        }

        #endregion

        #region 创建AEF连接字符串

        /// <summary>
        /// AEF连接字符串嵌入资源的部分，字符串模板
        /// </summary>
        private const string AEFEmbedPartPattern = "metadata=res://{0}/{1}.csdl|res://{0}/{1}.ssdl|res://{0}/{1}.msl";

        /// <summary>
        /// 获得AEF连接字符串嵌入资源的部分
        /// </summary>
        /// <param name="assemblyInfo">程序集信息，例如：MEMSAnalyzer.Model, Version=1.0.0.0, Culture=neutral, PublicKeyTokken=null、或者”*“(搜索全部程序集)</param>
        /// <param name="modeNameSpace">模型命名空间，例如：MEMSAnalyzerModel，用于MEMSAnalyzerModel.ssdl、MEMSAnalyzerModel.csdl、MEMSAnalyzerModel.msl等</param>
        /// <returns>AEF连接字符串嵌入资源的部分</returns>
        private static string GetAEFEmbedPart( string assemblyInfo, string modeNameSpace )
        {
            return string.Format( AEFEmbedPartPattern, assemblyInfo, modeNameSpace );
        }

        /// <summary>
        /// 获得AEF的连接字符串
        /// </summary>
        /// <param name="modeNameSpace"></param>
        /// <param name="dbPath">数据库路径</param>
        /// <param name="assemblyInfo"></param>
        /// <returns>AEF的连接字符串</returns>
        public static string CreateAEFConnectionString_Sqlite( string assemblyInfo, string modeNameSpace, string dbPath )
        {
            return string.Format(
                "{0};provider=System.Data.SQLite;provider connection string=\"{1}\";",
                GetAEFEmbedPart( assemblyInfo, modeNameSpace ),
                CreateConnectionString_Sqlite( dbPath ) );
        }

        #endregion
    }
}