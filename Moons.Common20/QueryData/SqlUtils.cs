namespace Moons.Common20.QueryData
{
    /// <summary>
    /// Sql语句的实用工具类
    /// </summary>
    public static class SqlUtils
    {
        #region Sql 常量

        public const string Sql_Select = "select";

        public const string Sql_CountStar = "count( * )";

        public const string Sql_From = "from";

        public const string Sql_Where = "where";

        public const string Sql_OrderBy = "order by";

        public const string Sql_Asc = "asc";

        public const string Sql_Desc = "desc";

        public const string Sql_Join = "join";

        public const string Sql_LeftJoin = "left join";

        public const string Sql_RightJoin = "right join";

        public const string Sql_InnerJoin = "inner join";

        public const string Sql_Like = "like";

        public const string Sql_Not = "not";

        //public const string Sql_In = "in";

        public const string Sql_Equal = "=";

        public const string Sql_GreatEqual = ">=";

        public const string Sql_Great = ">";

        public const string Sql_LessEqual = "<=";

        public const string Sql_Less = "<";

        public const string Sql_And = "and";

        public const string Sql_Or = "or";

        public const string Sql_In = "in";

        public const string Sql_NotIn = "not in";

        #endregion

        #region 关于QueryConditionBase

        /// <summary>
        /// 是否为查询条件组
        /// </summary>
        /// <param name="condition">QueryConditionBase</param>
        /// <returns>true：是查询条件组，false：不是</returns>
        public static bool IsConditionGroup( QueryConditionBase condition )
        {
            return condition is ConditionGroupBase;
        }

        /// <summary>
        /// 是否为基本查询条件
        /// </summary>
        /// <param name="condition">QueryConditionBase</param>
        /// <returns>true：是基本查询条件，false：不是</returns>
        public static bool IsBaseCondition( QueryConditionBase condition )
        {
            return condition != null && !IsConditionGroup( condition );
        }

        #endregion
    }
}