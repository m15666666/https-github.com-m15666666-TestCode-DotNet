using System.Data;

namespace Moons.Common20.QueryData
{
    /// <summary>
    /// 查询参数的基类
    /// </summary>
    public class QueryParameterBase
    {
        /// <summary>
        /// ctor
        /// </summary>
        public QueryParameterBase()
        {
            DbType = DbType.Object;

            ParamDirection = ParameterDirection.Input;
        }

        /// <summary>
        /// 参数名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 参数值
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// 参数类型，默认值为：DbType.Object，表示自动从Value的类型判断。用于Value为空时，设置数据库参数类型。
        /// </summary>
        public DbType DbType { get; set; }

        /// <summary>
        /// 参数传递方向，默认为：输入。
        /// </summary>
        public ParameterDirection ParamDirection { get; set; }
    }
}