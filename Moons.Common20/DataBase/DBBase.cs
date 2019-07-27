using System;
using System.Data.Common;
using Moons.Common20.QueryData;

namespace Moons.Common20.DataBase
{
    /// <summary>
    /// 表示数据库的基类
    /// </summary>
    public abstract class DBBase
    {
        #region 变量和属性

        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ConnectionString { get; set; }

        #endregion

        #region 可以覆盖的成员

        /// <summary>
        /// 数据库类型
        /// </summary>
        public abstract DataBaseType DataBaseType { get; }

        /// <summary>
        /// 获得数据库连接
        /// </summary>
        /// <returns>数据库连接</returns>
        public abstract DbConnection GetConnection();

        /// <summary>
        /// 获得用于Sql语句中参数的参数名，例如：@P_Size
        /// </summary>
        /// <param name="originName">初始参数名</param>
        /// <returns>用于Sql语句中参数的参数名</returns>
        public virtual string GetSQLParameterName( string originName )
        {
            return originName;
        }

        /// <summary>
        /// 获得Command对象
        /// </summary>
        /// <param name="connection">数据库连接</param>
        /// <returns>Command对象</returns>
        public virtual DbCommand GetCommand( DbConnection connection )
        {
            return connection.CreateCommand();
        }

        #region get sql value string

        /// <summary>
        /// 获得Sql语句中表示值的字符串
        /// </summary>
        /// <param name="value">值</param>
        /// <returns>表示值的字符串</returns>
        public virtual string GetValueString( object value )
        {
            if( DBConvertUtils.IsNull( value ) )
            {
                return GetNullString();
            }

            Type type = value.GetType();
            if( type == typeof(string) )
            {
                return GetValueString_String( (string)value );
            }

            if( type == typeof(DateTime) )
            {
                return GetValueString_DateTime( (DateTime)value );
            }

            if( type == typeof(DateTime?) )
            {
                return GetValueString_DateTime( ( (DateTime?)value ).Value );
            }

            return value.ToString();
        }

        /// <summary>
        /// 获得Sql语句中表示字符串值的字符串
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected virtual string GetValueString_String( string value )
        {
            return string.Format( "'{0}'", value.Replace( "'", "''" ) );
        }

        /// <summary>
        /// 获得Sql语句中表示DateTime值的字符串
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected virtual string GetValueString_DateTime( DateTime value )
        {
            return string.Format( "'{0}'", TimeUtils.DateTimeToString( value ) );
        }

        /// <summary>
        /// 获得Sql语句中表示空（null）的字符串
        /// </summary>
        /// <returns>表示空（null）的字符串</returns>
        protected virtual string GetNullString()
        {
            return "null";
        }

        #endregion

        #endregion

        /// <summary>
        /// 生成DbDataReader
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="handler">在执行ExecuteReader时调用的函数</param>
        public void ExecuteReader( string sql, Action<DbDataReader> handler )
        {
            using( DbConnection connection = CreateConnection() )
            {
                using( DbCommand cmd = AdoNetUtils.CreateCommand_Text( connection, sql ) )
                {
                    using( DbDataReader reader = cmd.ExecuteReader() )
                    {
                        EventUtils.FireEvent( handler, reader );
                    }
                }
            }
        }

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns>NameValueRowCollection</returns>
        public NameValueRowCollection Query( string sql )
        {
            NameValueRowCollection ret = null;
            Action<DbDataReader> handler = reader => ret = NameValueRowCollection.FromDbDataReader( reader );
            ExecuteReader( sql, handler );
            return ret;
        }

        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="sql">sql语句</param>
        public void ExecuteNonQuery( string sql )
        {
            using( DbConnection connection = CreateConnection() )
            {
                using( DbCommand cmd = AdoNetUtils.CreateCommand_Text( connection, sql ) )
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// 创建连接并打开
        /// </summary>
        /// <returns>DbConnection</returns>
        private DbConnection CreateConnection()
        {
            DbConnection connection = GetConnection();
            connection.Open();
            return connection;
        }
    }
}