using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.Common;

namespace Moons.Common20.QueryData
{
    /// <summary>
    /// NameValueRow对象的集合
    /// </summary>
    public class NameValueRowCollection : CollectionBase<NameValueRow, NameValueRowCollection>
    {
        #region 初始化

        public NameValueRowCollection()
        {
        }

        public NameValueRowCollection( IEnumerable<NameValueRow> rows )
            : base( rows )
        {
        }

        /// <summary>
        /// 从DbDataReader返回NameValueRowCollection对象
        /// </summary>
        /// <param name="reader">DbDataReader</param>
        /// <returns>NameValueRowCollection</returns>
        public static NameValueRowCollection FromDbDataReader( DbDataReader reader )
        {
            bool hasData = reader.Read();
            if( !hasData )
            {
                return new NameValueRowCollection();
            }

            string[] fieldNames = AdoNetUtils.GetFieldNames( reader );
            var ret = new NameValueRowCollection();
            do
            {
                var row = new NameValueRow();
                int index = 0;
                foreach( string fieldName in fieldNames )
                {
                    row[fieldName] = reader.GetValue( index++ );
                }
                ret.Add( row );

                hasData = reader.Read();
            } while( hasData );

            return ret;
        }

        #endregion

        #region ToID2Row、ToID2Rows

        /// <summary>
        /// 转化为ID到NameValueRowCollection的映射，即根据ID分组
        /// </summary>
        /// <param name="idFieldName">ID字段名</param>
        /// <returns>ID到NameValueRowCollection的映射</returns>
        public IDictionary<int, NameValueRowCollection> ToID2Rows( string idFieldName )
        {
            var ret = new Dictionary<int, NameValueRowCollection>();
            foreach( NameValueRow row in this )
            {
                int id = Convert.ToInt32( row[idFieldName] );
                NameValueRowCollection rows;
                if( ret.ContainsKey( id ) )
                {
                    rows = ret[id];
                }
                else
                {
                    ret[id] = rows = new NameValueRowCollection();
                }
                rows.Add( row );
            }
            return ret;
        }

        /// <summary>
        /// 转化为ID到NameValueRow的映射，即根据ID分组
        /// </summary>
        /// <param name="idFieldName">ID字段名</param>
        /// <returns>ID到NameValueRow的映射</returns>
        public IDictionary<int, NameValueRow> ToID2Row( string idFieldName )
        {
            var ret = new Dictionary<int, NameValueRow>();
            foreach( NameValueRow row in this )
            {
                ret[Convert.ToInt32( row[idFieldName] )] = row;
            }
            return ret;
        }

        #endregion

        #region 转化为DataView

        /// <summary>
        /// 转化为DataView
        /// </summary>
        /// <returns>DataView</returns>
        public DataView ToDataView()
        {
            return ToDataView( this );
        }

        /// <summary>
        /// 转化为DataView
        /// </summary>
        /// <param name="rows">NameValueRow集合</param>
        /// <returns>DataView</returns>
        private static DataView ToDataView( List<NameValueRow> rows )
        {
            var table = new DataTable();

            if( 0 < rows.Count )
            {
                NameObjectCollectionBase.KeysCollection keys = rows[0].Keys;
                foreach( string key in keys )
                {
                    table.Columns.Add( new DataColumn( key, typeof( object ) ) );
                }

                rows.ForEach(
                    row =>
                        {
                            DataRow newRow = table.NewRow();
                            foreach( string key in keys )
                            {
                                newRow[key] = row[key];
                            }

                            table.Rows.Add( newRow );
                        } );
            }

            return new DataView( table );
        }

        #endregion

        #region 通过反射转化为对象

        /// <summary>
        /// 通过反射转化为对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <returns>对象集合</returns>
        public IList<T> ToObjectByReflection<T>() where T : class, new()
        {
            return ConvertAll( row => row.ToObjectByReflection<T>() );
        }

        #endregion

        #region 通过反射将对象转化为NameValueRow

        /// <summary>
        /// 通过反射将对象转化为NameValueRow
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="objs">对象集合</param>
        /// <returns>NameValueRowCollection</returns>
        public static NameValueRowCollection FromObjectByReflection<T>( IEnumerable<T> objs ) where T : class
        {
            var ret = new NameValueRowCollection();
            foreach( T obj in objs )
            {
                ret.Add( NameValueRow.FromObjectByReflection( obj ) );
            }
            return ret;
        }

        #endregion
    }
}