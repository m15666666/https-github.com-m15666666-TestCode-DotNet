namespace Moons.Common20.QueryData
{
    /// <summary>
    /// 查询条件的基类
    /// </summary>
    public abstract class QueryConditionBase
    {
        /// <summary>
        /// 属于的表对象
        /// </summary>
        public Table Table { get; set; }

        /// <summary>
        /// 字段名
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        /// 参数名，为空则自动生成
        /// </summary>
        public string ParameterName { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// 是否对条件取反
        /// </summary>
        public bool IsNot { get; set; }

        protected QueryConditionBase()
        {
            IsNot = false;
        }

        /// <summary>
        /// 添加一个条件，即：this and condition
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns>新的组合条件</returns>
        public QueryConditionBase And( QueryConditionBase condition )
        {
            return new AndConditionGroup { Left = this, Right = condition };
        }

        /// <summary>
        /// 添加一个条件，即：this or condition
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns>新的组合条件</returns>
        public QueryConditionBase Or( QueryConditionBase condition )
        {
            return new OrConditionGroup { Left = this, Right = condition };
        }
    }
}