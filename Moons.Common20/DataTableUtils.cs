using System;
using System.Collections.Generic;
using System.Data;

namespace Moons.Common20
{
    /// <summary>
    /// DataSet、DataTable、DataRow相关的实用工具类
    /// </summary>
    public static class DataTableUtils
    {
        /// <summary>
        /// 检查数据行是否全部为空
        /// </summary>
        /// <param name="row">DataRow</param>
        /// <returns>true：都为空，false：不都为空</returns>
        public static bool IsEmpty( DataRow row )
        {
            object[] values = row.ItemArray;
            if( values.Length == 0 )
            {
                return true;
            }

            foreach( object value in values )
            {
                if( !StringUtils.IsNullOrEmpty( value ) )
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 获得列名集合
        /// </summary>
        /// <param name="dataTable">DataTable</param>
        /// <returns>列名集合</returns>
        public static string[] GetColumnNames( DataTable dataTable )
        {
            var list = new List<string>();
            foreach( DataColumn column in dataTable.Columns )
            {
                list.Add( column.ColumnName );
            }
            return list.ToArray();
        }

        /// <summary>
        /// 增加多个列
        /// </summary>
        /// <param name="dataTable">DataTable</param>
        /// <param name="columnType">列类型</param>
        /// <param name="columnNames">列名集合</param>
        public static void AddColumns( DataTable dataTable, Type columnType, params string[] columnNames )
        {
            foreach( string columnName in columnNames )
            {
                dataTable.Columns.Add( new DataColumn( columnName, columnType ) );
            }
        }
    }
}