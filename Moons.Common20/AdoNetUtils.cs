using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace Moons.Common20
{
    /// <summary>
    /// Ado.Net的实用工具类
    /// </summary>
    public static class AdoNetUtils
    {
        #region 创建参数

        /// <summary>
        /// 创建参数
        /// </summary>
        /// <param name="cmd">DbCommand</param>
        /// <param name="parameterName">参数名</param>
        /// <param name="dbType">DbType</param>
        /// <returns>参数</returns>
        public static DbParameter CreateParameter( DbCommand cmd, string parameterName, DbType dbType )
        {
            DbParameter ret = cmd.CreateParameter();

            ret.ParameterName = parameterName;
            ret.DbType = dbType;

            return ret;
        }

        /// <summary>
        /// 创建Int64参数
        /// </summary>
        /// <param name="cmd">DbCommand</param>
        /// <param name="parameterName">参数名</param>
        /// <returns>参数</returns>
        public static DbParameter CreateParameter_DateTime( DbCommand cmd, string parameterName )
        {
            return CreateParameter( cmd, parameterName, DbType.DateTime );
        }

        /// <summary>
        /// 创建Int64参数
        /// </summary>
        /// <param name="cmd">DbCommand</param>
        /// <param name="parameterName">参数名</param>
        /// <returns>参数</returns>
        public static DbParameter CreateParameter_Int64( DbCommand cmd, string parameterName )
        {
            return CreateParameter( cmd, parameterName, DbType.Int64 );
        }

        /// <summary>
        /// 创建Int32参数
        /// </summary>
        /// <param name="cmd">DbCommand</param>
        /// <param name="parameterName">参数名</param>
        /// <returns>参数</returns>
        public static DbParameter CreateParameter_Int32( DbCommand cmd, string parameterName )
        {
            return CreateParameter( cmd, parameterName, DbType.Int32 );
        }

        /// <summary>
        /// 创建Int16参数
        /// </summary>
        /// <param name="cmd">DbCommand</param>
        /// <param name="parameterName">参数名</param>
        /// <returns>参数</returns>
        public static DbParameter CreateParameter_Int16( DbCommand cmd, string parameterName )
        {
            return CreateParameter( cmd, parameterName, DbType.Int16 );
        }

        /// <summary>
        /// 创建Byte参数
        /// </summary>
        /// <param name="cmd">DbCommand</param>
        /// <param name="parameterName">参数名</param>
        /// <returns>参数</returns>
        public static DbParameter CreateParameter_Byte( DbCommand cmd, string parameterName )
        {
            return CreateParameter( cmd, parameterName, DbType.Byte );
        }

        /// <summary>
        /// 创建字符串参数
        /// </summary>
        /// <param name="cmd">DbCommand</param>
        /// <param name="parameterName">参数名</param>
        /// <returns>参数</returns>
        public static DbParameter CreateParameter_String( DbCommand cmd, string parameterName )
        {
            return CreateParameter( cmd, parameterName, DbType.String );
        }

        /// <summary>
        /// 创建字符串参数
        /// </summary>
        /// <param name="cmd">DbCommand</param>
        /// <param name="parameterName">参数名</param>
        /// <param name="size">最大的尺寸</param>
        /// <returns>参数</returns>
        public static DbParameter CreateParameter_String( DbCommand cmd, string parameterName, int size )
        {
            DbParameter ret = CreateParameter_String( cmd, parameterName );

            ret.Size = size;

            return ret;
        }

        #endregion

        #region DbDataReader相关

        /// <summary>
        /// 获得字段名数组
        /// </summary>
        /// <param name="reader">DbDataReader</param>
        /// <returns>字段名数组</returns>
        public static string[] GetFieldNames( DbDataReader reader )
        {
            var ret = new string[reader.FieldCount];
            for( int index = 0; index < ret.Length; index++ )
            {
                ret[index] = reader.GetName( index );
            }

            return ret;
        }

        #endregion

        #region 命令对象

        // <summary>
        /// 创建命令对象
        /// </summary>
        /// <param name="conn">DbConnection</param>
        /// <returns>命令对象</returns>
        public static DbCommand CreateDbCommand( DbConnection conn )
        {
            DbCommand cmd = conn.CreateCommand();
            if( 0 < EnvironmentUtils.DbCmdTimeoutInSecond )
            {
                cmd.CommandTimeout = EnvironmentUtils.DbCmdTimeoutInSecond;
            }
            return cmd;
        }

        /// <summary>
        /// 创建执行存储过程的命令对象
        /// </summary>
        /// <param name="conn">DbConnection</param>
        /// <param name="spName">存储过程名</param>
        /// <returns>命令对象</returns>
        public static DbCommand CreateCommand_Sp( DbConnection conn, string spName )
        {
            DbCommand cmd = CreateDbCommand( conn );

            cmd.CommandText = spName;
            cmd.CommandType = CommandType.StoredProcedure;

            return cmd;
        }

        /// <summary>
        /// 创建执行sql语句的命令对象
        /// </summary>
        /// <param name="conn">DbConnection</param>
        /// <param name="sql">sql语句</param>
        /// <returns>命令对象</returns>
        public static DbCommand CreateCommand_Text( DbConnection conn, string sql )
        {
            DbCommand cmd = CreateDbCommand( conn );

            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;

            return cmd;
        }

        /// <summary>
        /// 创建执行sql语句的命令对象
        /// </summary>
        /// <param name="conn">DbConnection</param>
        /// <returns>命令对象</returns>
        public static DbCommand CreateCommand_Text( DbConnection conn )
        {
            DbCommand cmd = CreateDbCommand( conn );

            cmd.CommandType = CommandType.Text;

            return cmd;
        }

        #region 执行sql语句集合

        /// <summary>
        /// 执行sql语句集合
        /// </summary>
        /// <param name="cmd">命令对象</param>
        /// <param name="sqls">sql语句集合</param>
        public static void ExecuteNonQueryTexts( DbCommand cmd, params string[] sqls )
        {
            ExecuteNonQueryTexts( cmd, (IEnumerable<string>)sqls );
        }

        /// <summary>
        /// 执行sql语句集合
        /// </summary>
        /// <param name="cmd">命令对象</param>
        /// <param name="sqls">sql语句集合</param>
        public static void ExecuteNonQueryTexts( DbCommand cmd, IEnumerable<string> sqls )
        {
            cmd.CommandType = CommandType.Text;
            foreach( string sql in sqls )
            {
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
            }
        }

        #endregion

        #endregion
    }
}