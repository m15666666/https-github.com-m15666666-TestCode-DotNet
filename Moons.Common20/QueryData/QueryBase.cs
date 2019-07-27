using System;
using System.Collections.Generic;
using System.Text;

namespace Moons.Common20.QueryData
{
    /// <summary>
    /// 查询数据的抽象基类
    /// </summary>
    public abstract class QueryBase
    {
        #region 查询

        /// <summary>
        /// 查询结果
        /// </summary>
        public IList<NameValueRow> Query( QueryInfoBag queryInfoBag )
        {
            CheckQueryInfoBag( queryInfoBag );

            return DoQuery( queryInfoBag );
        }

        /// <summary>
        /// 查询记录数
        /// </summary>
        public int QueryCount( QueryInfoBag queryInfoBag )
        {
            CheckQueryInfoBag( queryInfoBag );

            return DoQueryCount( queryInfoBag );
        }


        /// <summary>
        /// 检查QueryInfoBag对象是否合法
        /// </summary>
        private void CheckQueryInfoBag( QueryInfoBag queryInfoBag )
        {
            IList<Table> tables = queryInfoBag.Tables;
            if( tables.Count == 0 && string.IsNullOrEmpty( queryInfoBag.FromString ) )
            {
                throw new ArgumentException( "未设置表集合！" );
            }

            int tableIndex = 0;
            foreach( Table table in tables )
            {
                if( table.Alias == null )
                {
                    table.Alias = "t" + ( tableIndex++ );
                }
            }

            bool oneTable = queryInfoBag.IsOneTable;
            Table firstTable = tables[0];
            int parameterIndex = 0;
            foreach( QueryConditionBase parameter in GetParameterList( queryInfoBag ) )
            {
                if( parameter.Table == null )
                {
                    if( oneTable )
                    {
                        parameter.Table = firstTable;
                    }
                    else
                    {
                        throw new ArgumentException( string.Format( "查询条件（{0}）未设置表的引用！", parameter.FieldName ) );
                    }
                }

                if( string.IsNullOrEmpty( parameter.ParameterName ) )
                {
                    parameter.ParameterName = "p" + ( parameterIndex++ );
                }
            }

            foreach( ReturnField returnField in queryInfoBag.ReturnFields )
            {
                if( returnField.Table == null )
                {
                    if( oneTable )
                    {
                        returnField.Table = firstTable;
                    }
                    else
                    {
                        throw new ArgumentException( string.Format( "返回字段（{0}）未设置表的引用！", returnField.FieldName ) );
                    }
                }
            }

            foreach( OrderBase order in queryInfoBag.Orders )
            {
                if( order.Table == null )
                {
                    if( oneTable )
                    {
                        order.Table = firstTable;
                    }
                    else
                    {
                        throw new ArgumentException( string.Format( "排序字段（{0}）未设置表的引用！", order.FieldName ) );
                    }
                }
            }
        }

        /// <summary>
        /// 执行查询
        /// </summary>
        protected abstract IList<NameValueRow> DoQuery( QueryInfoBag queryInfoBag );

        /// <summary>
        /// 执行查询记录数
        /// </summary>
        protected abstract int DoQueryCount( QueryInfoBag queryInfoBag );

        /// <summary>
        /// 获得查询数据的Sql语句
        /// </summary>
        /// <param name="queryInfoBag">QueryInfoBag</param>
        /// <returns>Sql语句</returns>
        protected virtual string GetQuerySql( QueryInfoBag queryInfoBag )
        {
            string fromString = GetFromString( queryInfoBag );
            string returnFieldString = GetReturnFieldString( queryInfoBag );
            string orderString = GetOrderString( queryInfoBag );
            string whereString = GetWhereString( queryInfoBag );

            bool orderStringValid = !string.IsNullOrEmpty( orderString );
            bool whereStringValid = !string.IsNullOrEmpty( whereString );

            var buffer = new StringBuilder();
            buffer.AppendFormat( "{0} {1} {2} {3}",
                                 SqlUtils.Sql_Select, returnFieldString, SqlUtils.Sql_From, fromString );

            if( whereStringValid )
            {
                buffer.AppendFormat( " {0} {1}", SqlUtils.Sql_Where, whereString );
            }

            if( orderStringValid )
            {
                buffer.AppendFormat( " {0} {1}", SqlUtils.Sql_OrderBy, orderString );
            }

            return buffer.ToString();
        }

        /// <summary>
        /// 获得查询记录数的Sql语句
        /// </summary>
        /// <param name="queryInfoBag">QueryInfoBag</param>
        /// <returns>Sql语句</returns>
        protected virtual string GetQueryCountSql( QueryInfoBag queryInfoBag )
        {
            string fromString = GetFromString( queryInfoBag );
            string whereString = GetWhereString( queryInfoBag );

            bool whereStringValid = !string.IsNullOrEmpty( whereString );

            var buffer = new StringBuilder();
            buffer.AppendFormat( "{0} {1} {2} {3}",
                                 SqlUtils.Sql_Select, SqlUtils.Sql_CountStar, SqlUtils.Sql_From, fromString );

            if( whereStringValid )
            {
                buffer.AppendFormat( " {0} {1}", SqlUtils.Sql_Where, whereString );
            }

            return buffer.ToString();
        }

        /// <summary>
        /// 获得From之后的Sql字符串
        /// </summary>
        protected virtual string GetFromString( QueryInfoBag queryInfoBag )
        {
            if( !string.IsNullOrEmpty( queryInfoBag.FromString ) )
            {
                return queryInfoBag.FromString;
            }

            var buffer = new StringBuilder();
            foreach( Table table in queryInfoBag.Tables )
            {
                if( 0 < buffer.Length )
                {
                    buffer.Append( ", " );
                }
                string tableName = table.Name;

                // 是否需要在表名外面加小括号，需要加的情况：包含空格且最外面没加小括号。
                bool addParentheses = tableName.Contains( StringUtils.WhiteSpace ) &&
                                      !tableName.StartsWith( StringUtils.LeftParenthese );

                string format = addParentheses ? "( {0} ) {1}" : "{0} {1}";
                buffer.AppendFormat( format, tableName, table.Alias );
            }

            return buffer.ToString();
        }

        /// <summary>
        /// 获得Select之后的Sql字符串
        /// </summary>
        protected virtual string GetReturnFieldString( QueryInfoBag queryInfoBag )
        {
            if( !string.IsNullOrEmpty( queryInfoBag.ReturnFieldString ) )
            {
                return queryInfoBag.ReturnFieldString;
            }

            var buffer = new StringBuilder();
            IList<ReturnField> returnFields = queryInfoBag.ReturnFields;
            if( returnFields.Count == 0 )
            {
                foreach( Table table in queryInfoBag.Tables )
                {
                    if( 0 < buffer.Length )
                    {
                        buffer.Append( ", " );
                    }
                    buffer.AppendFormat( "{0}.*", table.Alias );
                }

                return buffer.ToString();
            }

            foreach( ReturnField returnField in returnFields )
            {
                if( 0 < buffer.Length )
                {
                    buffer.Append( ", " );
                }

                buffer.AppendFormat( "{0}.{1}", returnField.Table.Alias, returnField.FieldName );
                if( !string.IsNullOrEmpty( returnField.Alias ) )
                {
                    buffer.AppendFormat( " {0}", returnField.Alias );
                }
            }

            return buffer.ToString();
        }

        /// <summary>
        /// 获得where后面的Sql字符串
        /// </summary>
        /// <param name="queryInfoBag">QueryInfoBag</param>
        /// <returns>查询条件字符串</returns>
        protected virtual string GetWhereString( QueryInfoBag queryInfoBag )
        {
            return GetConditionStringRecursive( queryInfoBag.QueryCondition );
        }

        /// <summary>
        /// 获得order by后面的Sql字符串
        /// </summary>
        protected virtual string GetOrderString( QueryInfoBag queryInfoBag )
        {
            IList<OrderBase> orders = queryInfoBag.Orders;
            if( orders.Count == 0 )
            {
                return null;
            }

            var buffer = new StringBuilder();
            foreach( OrderBase order in orders )
            {
                if( 0 < buffer.Length )
                {
                    buffer.Append( ", " );
                }

                buffer.AppendFormat( "{0}.{1}", order.Table.Alias, order.FieldName );

                if( order is DescOrder )
                {
                    buffer.Append( " " ).Append( SqlUtils.Sql_Desc );
                }
            }

            return buffer.ToString();
        }

        #endregion

        #region 数据库相关的基础信息

        /// <summary>
        /// 是否支持命名参数
        /// </summary>
        protected virtual bool SupportNamedParameter
        {
            get { return true; }
        }

        /// <summary>
        /// 获得参数名，例如：?、:userName、@username等。
        /// </summary>
        protected virtual string GetParameterName( string name )
        {
            return ":" + name;
        }

        /// <summary>
        /// 获得Like字符，例如：%。
        /// </summary>
        protected virtual string GetLikeChar()
        {
            return "%";
        }

        #endregion

        #region 获得条件字符串

        /// <summary>
        /// 获得条件字段字符串
        /// </summary>
        protected virtual string GetConditionFieldString( QueryConditionBase condition )
        {
            return condition.Table.Alias + "." + condition.FieldName;
        }

        /// <summary>
        /// 组合条件字符串
        /// </summary>
        /// <param name="left">左条件字符串</param>
        /// <param name="right">右条件字符串</param>
        /// <param name="relationString">关系字符串</param>
        /// <returns>组合后的条件字符串</returns>
        protected string CombineConditionString( string left, string right, string relationString )
        {
            bool leftValid = !string.IsNullOrEmpty( left );
            bool rightValid = !string.IsNullOrEmpty( right );

            if( ( !leftValid && !rightValid ) || string.IsNullOrEmpty( relationString ) )
            {
                return null;
            }

            if( leftValid && rightValid )
            {
                return string.Format( "( {0} {1} {2} )", left, relationString, right );
            }

            if( leftValid )
            {
                return left;
            }

            return right;
        }

        /// <summary>
        /// 获得关系字符串
        /// </summary>
        /// <param name="binaryConditionGroup">BinaryConditionGroupBase</param>
        /// <returns>关系字符串</returns>
        protected virtual string GetRelationString( BinaryConditionGroupBase binaryConditionGroup )
        {
            if( binaryConditionGroup == null )
            {
                return null;
            }

            if( binaryConditionGroup is AndConditionGroup )
            {
                return SqlUtils.Sql_And;
            }

            if( binaryConditionGroup is OrConditionGroup )
            {
                return SqlUtils.Sql_Or;
            }

            return null;
        }

        /// <summary>
        /// 递归获得条件字符串
        /// </summary>
        /// <param name="condition">QueryConditionBase</param>
        /// <returns>条件字符串</returns>
        protected virtual string GetConditionStringRecursive( QueryConditionBase condition )
        {
            if( condition == null )
            {
                return null;
            }

            string conditionString;
            if( SqlUtils.IsConditionGroup( condition ) )
            {
                conditionString = GetConditionGroupString( condition as ConditionGroupBase );
            }
            else
            {
                conditionString = GetBaseConditionString( condition );
            }

            return condition.IsNot
                       ? string.Format( "( " + SqlUtils.Sql_Not + " {0} )", conditionString )
                       : conditionString;
        }

        /// <summary>
        /// 获得查询条件组的字符串
        /// </summary>
        protected virtual string GetConditionGroupString( ConditionGroupBase condition )
        {
            if( condition == null )
            {
                return null;
            }

            var binaryConditionGroup = condition as BinaryConditionGroupBase;
            if( binaryConditionGroup != null )
            {
                string left = GetConditionStringRecursive( binaryConditionGroup.Left );
                string right = GetConditionStringRecursive( binaryConditionGroup.Right );

                return CombineConditionString( left, right, GetRelationString( binaryConditionGroup ) );
            }

            return null;
        }

        /// <summary>
        /// 获得基本查询条件的字符串
        /// </summary>
        protected virtual string GetBaseConditionString( QueryConditionBase condition )
        {
            if( condition == null )
            {
                return null;
            }

            string conditionString = null;
            if( condition is LikeCondition )
            {
                conditionString = GetLikeConditionString( condition as LikeCondition );
            }
            else if( condition is GreatThenCondition )
            {
                conditionString = GetGreatThenConditionString( condition as GreatThenCondition );
            }
            else if( condition is LessThenCondition )
            {
                conditionString = GetLessThenConditionString( condition as LessThenCondition );
            }
            else if( condition is EqualCondition )
            {
                conditionString = GetEqualConditionString( condition as EqualCondition );
            }
            else if (condition is ConstantCondition)
            {
                conditionString = GetConstantConditionString(condition as ConstantCondition);
            }
                //in & not in
            else if (condition is InCondition)
            {
                conditionString = GetInConditionString(condition as InCondition);
            }
            else if (condition is NotInCondition)
            {
                conditionString = GetNotInConditionString(condition as NotInCondition);
            }

            return string.IsNullOrEmpty( conditionString ) ? null : string.Format( "( {0} )", conditionString );
        }

        /// <summary>
        /// 获得条件字符串
        /// </summary>
        protected string GetConditionString( QueryConditionBase condition, string relationString )
        {
            return string.Format( "{0} {1} {2}", GetConditionFieldString( condition ), relationString,
                                  GetParameterName( condition.ParameterName ) );
        }

        /// <summary>
        /// 获得In & NotIn条件字符串
        /// </summary>
        protected string GetInAndNotConditionString(QueryConditionBase condition, string relationString)
        {
            return string.Format("{0} {1} ( {2} )", GetConditionFieldString(condition), relationString,
                                  GetParameterName(condition.ParameterName));
        }

        /// <summary>
        /// 获得In条件字符串
        /// </summary>
        protected virtual string GetInConditionString(InCondition condition)
        {
            return GetInAndNotConditionString(condition, SqlUtils.Sql_In);
        }

        /// <summary>
        /// 获得Not In条件字符串
        /// </summary>
        protected virtual string GetNotInConditionString(NotInCondition condition)
        {
            return GetInAndNotConditionString(condition, SqlUtils.Sql_NotIn);
        }

        /// <summary>
        /// 获得等于条件字符串
        /// </summary>
        protected virtual string GetEqualConditionString( EqualCondition condition )
        {
            return GetConditionString( condition, SqlUtils.Sql_Equal );
        }

        /// <summary>
        /// 获得等于条件字符串
        /// </summary>
        protected virtual string GetConstantConditionString( ConstantCondition condition )
        {
            return condition.Condition;
        }

        /// <summary>
        /// 获得大于条件字符串
        /// </summary>
        protected virtual string GetGreatThenConditionString( GreatThenCondition condition )
        {
            return GetConditionString( condition, condition.IncludeEqual ? SqlUtils.Sql_GreatEqual : SqlUtils.Sql_Great );
        }

        /// <summary>
        /// 获得小于条件字符串
        /// </summary>
        protected virtual string GetLessThenConditionString( LessThenCondition condition )
        {
            return GetConditionString( condition, condition.IncludeEqual ? SqlUtils.Sql_LessEqual : SqlUtils.Sql_Less );
        }

        /// <summary>
        /// 获得Like条件字符串
        /// </summary>
        protected virtual string GetLikeConditionString( LikeCondition condition )
        {
            return GetConditionString( condition, SqlUtils.Sql_Like );
        }

        #endregion

        #region 参数列表、获得参数值

        /// <summary>
        /// 获得参数列表
        /// </summary>
        public virtual List<QueryConditionBase> GetParameterList( QueryInfoBag queryInfoBag )
        {
            var ret = new List<QueryConditionBase>();
            GetParameterList( queryInfoBag.QueryCondition, ret );
            return ret;
        }

        /// <summary>
        /// 获得参数列表
        /// </summary>
        protected virtual void GetParameterList( QueryConditionBase queryCondition, List<QueryConditionBase> list )
        {
            if( queryCondition == null || queryCondition is ConstantCondition )
            {
                return;
            }


            if( SqlUtils.IsBaseCondition( queryCondition ) )
            {
                list.Add( queryCondition );
                return;
            }

            var binaryConditionGroup = queryCondition as BinaryConditionGroupBase;
            if( binaryConditionGroup != null )
            {
                GetParameterList( binaryConditionGroup.Left, list );
                GetParameterList( binaryConditionGroup.Right, list );
                return;
            }
        }

        /// <summary>
        /// 获得参数值
        /// </summary>
        protected virtual object GetParameterValue( QueryConditionBase queryCondition )
        {
            if( queryCondition is LikeCondition )
            {
                return GetLikeConditionValueString( queryCondition as LikeCondition );
            }

            return queryCondition.Value;
        }

        /// <summary>
        /// 获得LikeCondition的参数值
        /// </summary>
        protected virtual string GetLikeConditionValueString( LikeCondition condition )
        {
            string likeChar = GetLikeChar();
            string prefix = condition.AppendPrefixChar ? likeChar : null;
            string suffix = condition.AppendSuffixChar ? likeChar : null;
            return prefix + condition.Value + suffix;
        }

        #endregion
    }
}