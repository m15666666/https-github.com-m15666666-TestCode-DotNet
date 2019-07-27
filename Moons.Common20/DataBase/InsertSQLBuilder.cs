using System.Collections.Generic;
using Moons.Common20.QueryData;

namespace Moons.Common20.DataBase
{
    /// <summary>
    /// 插入语句生成器
    /// </summary>
    public class InsertSQLBuilder
    {
        public InsertSQLBuilder()
        {
            InsertFormat = "Insert Into {0}({1}) Values({2});";
        }

        #region 变量和属性

        /// <summary>
        /// 数据库对象
        /// </summary>
        public DBBase DataBase { get; set; }

        /// <summary>
        /// Insert语句的格式
        /// </summary>
        public string InsertFormat { get; set; }

        #endregion

        /// <summary>
        /// 获得Insert Sql语句集合
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="rows">数据集合</param>
        /// <returns>Insert Sql语句集合</returns>
        public List<string> GetInsertSQLs( string tableName, NameValueRowCollection rows )
        {
            if( rows.Count == 0 )
            {
                return new List<string>();
            }

            const string ValuesPlaceHolder = "__Values__";
            string format = null;
            var ret = new List<string>();
            foreach( NameValueRow row in rows )
            {
                if( format == null )
                {
                    format = string.Format( InsertFormat, tableName,
                                            StringUtils.Join( row.OriginColumnNames ), ValuesPlaceHolder );

                    // 将参数位置提前
                    format = format.Replace( ValuesPlaceHolder, "{0}" );
                }

                ret.Add( string.Format( format, GetValuesPartSQL( row.Values ) ) );
            }

            return ret;
        }

        /// <summary>
        /// 获得值的部分sql语句
        /// </summary>
        /// <param name="values">值集合</param>
        /// <returns>值的部分sql语句</returns>
        private string GetValuesPartSQL( object[] values )
        {
            var list = new List<string>();
            foreach( object value in values )
            {
                list.Add( DataBase.GetValueString( value ) );
            }
            return StringUtils.Join( list.ToArray() );
        }
    }
}