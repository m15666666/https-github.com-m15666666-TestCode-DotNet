using System;
using System.Collections.Generic;

namespace Moons.Common20.QueryData
{
    /// <summary>
    /// 查询前提条件的类的基类
    /// </summary>
    public abstract class QueryInfoBagBase
    {
        #region 事件相关

        /// <summary>
        /// 查询数据之后调用的函数
        /// </summary>
        public Action<NameValueRowCollection> AfterQueryData { get; set; }

        /// <summary>
        /// 激发AfterQueryData事件
        /// </summary>
        /// <param name="collection">NameValueRowCollection</param>
        public void FireAfterQueryData( NameValueRowCollection collection )
        {
            Action<NameValueRowCollection> handler = AfterQueryData;
            if( handler != null )
            {
                handler( collection );
            }
        }

        #endregion
    }

    /// <summary>
    /// 包含所有查询前提条件的类
    /// </summary>
    public class QueryInfoBag : QueryInfoBagBase
    {
        public QueryInfoBag()
        {
            Tables = new List<Table>();
            ReturnFields = new List<ReturnField>();
            Orders = new List<OrderBase>();
        }

        /// <summary>
        /// 表集合
        /// </summary>
        public IList<Table> Tables { get; set; }

        /// <summary>
        /// 返回的字段集合，如果设置了ReturnFieldString则忽略该值。
        /// 如果该值也为空，则返回所有表的所有字段，如：t.*。
        /// </summary>
        public IList<ReturnField> ReturnFields { get; set; }

        /// <summary>
        /// 查询条件
        /// </summary>
        public QueryConditionBase QueryCondition { get; set; }

        /// <summary>
        /// 排序条件集合
        /// </summary>
        public IList<OrderBase> Orders { get; set; }

        /// <summary>
        /// 返回字符串，如果为空则自动生成。
        /// </summary>
        public string ReturnFieldString { get; set; }

        /// <summary>
        /// 数据源字符串，如果为空则自动生成。
        /// </summary>
        public string FromString { get; set; }

        /// <summary>
        /// 分页的下标，从0开始，null表示不分页
        /// </summary>
        public int? PageIndex { get; set; }

        /// <summary>
        /// 每页的大小，null表示不分页
        /// </summary>
        public int? PageSize { get; set; }

        /// <summary>
        /// 是否分页，也就是PageIndex和PageSize是否有效。
        /// </summary>
        public bool IsPaging
        {
            get { return PageIndex.HasValue && 0 <= PageIndex.Value && PageSize.HasValue && 0 < PageSize.Value; }
        }

        /// <summary>
        /// 有效的分页的下标，从0开始
        /// </summary>
        public int ValidPageIndex
        {
            get { return PageIndex.Value; }
        }

        /// <summary>
        /// 有效的每页的大小
        /// </summary>
        public int ValidPageSize
        {
            get { return PageSize.Value; }
        }

        /// <summary>
        /// 是否只查询一个表
        /// </summary>
        public bool IsOneTable
        {
            get { return Tables.Count == 1; }
        }

        #region 增加信息的函数

        public Table AddTable( Table table )
        {
            Tables.Add( table );
            return table;
        }

        public Table AddTable( string name )
        {
            return AddTable( new Table { Name = name } );
        }

        public Table AddTable( string name, string alias )
        {
            return AddTable( new Table { Name = name, Alias = alias } );
        }

        public ReturnField AddReturnField( ReturnField returnField )
        {
            ReturnFields.Add( returnField );
            return returnField;
        }

        public ReturnField AddReturnField( string fieldName )
        {
            return AddReturnField( new ReturnField { FieldName = fieldName } );
        }

        public void AddReturnFields( params string[] fieldNames )
        {
            if( fieldNames != null )
            {
                Array.ForEach( fieldNames, fieldName => AddReturnField( fieldName ) );
            }
        }

        public ReturnField AddReturnField( string fieldName, string alias )
        {
            return AddReturnField( new ReturnField { FieldName = fieldName, Alias = alias } );
        }

        public ReturnField AddReturnField( string fieldName, string alias, Table table )
        {
            return AddReturnField( new ReturnField { FieldName = fieldName, Alias = alias, Table = table } );
        }

        public OrderBase AddOrder( OrderBase order )
        {
            Orders.Add( order );
            return order;
        }

        public OrderBase AddAscOrder( string fieldName )
        {
            return AddOrder( new AscOrder { FieldName = fieldName } );
        }

        public OrderBase AddAscOrder( string fieldName, Table table )
        {
            return AddOrder( new AscOrder { FieldName = fieldName, Table = table } );
        }

        public OrderBase AddDescOrder( string fieldName )
        {
            return AddOrder( new DescOrder { FieldName = fieldName } );
        }

        public OrderBase AddDescOrder( string fieldName, Table table )
        {
            return AddOrder( new DescOrder { FieldName = fieldName, Table = table } );
        }

        /// <summary>
        /// 添加一个条件，如果QueryCondition为空则设置QueryCondition为这个条件。
        /// 否则，与这个条件，即：QueryCondition and condition。
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns>当前的QueryCondition</returns>
        public QueryConditionBase AddCondition( QueryConditionBase condition )
        {
            return QueryCondition = QueryCondition == null
                                        ? condition
                                        : QueryCondition.And( condition );
        }

        #endregion
    }
}