using System.Collections.Generic;
using System.Data;
namespace Moons.Common20.QueryData
{
    /// <summary>
    /// 包含所有查询前提条件的类，用于存储过程查询
    /// </summary>
    public class QueryInfoBagSp : QueryInfoBagBase
    {
        /// <summary>
        /// ctor
        /// </summary>
        public QueryInfoBagSp()
        {
            QueryParameters = new List<QueryParameterBase>();
        }

        /// <summary>
        /// 存储过程名
        /// </summary>
        public string SpName { get; set; }

        /// <summary>
        /// 表集合
        /// </summary>
        public IList<QueryParameterBase> QueryParameters { get; set; }

        /// <summary>
        /// 输出参数值
        /// </summary>
        public NameValueRow OutputParameterValues { get; set; }

        #region 增加信息的函数

        /// <summary>
        /// 添加查询参数
        /// </summary>
        /// <param name="parameter">QueryParameterBase</param>
        /// <returns>QueryParameterBase</returns>
        public QueryParameterBase AddQueryParameter( QueryParameterBase parameter )
        {
            QueryParameters.Add( parameter );
            return parameter;
        }

        /// <summary>
        /// 添加查询参数
        /// </summary>
        /// <param name="name">参数名</param>
        /// <param name="value">参数值</param>
        /// <returns>QueryParameterBase</returns>
        public QueryParameterBase AddQueryParameter( string name, object value   )
        {
            return AddQueryParameter( new QueryParameterBase { Name = name, Value = value } );
        }

        /// <summary>
        /// 添加查询参数
        /// </summary>
        /// <param name="name">参数名</param>
        /// <param name="value">参数值</param>
        /// <param name="paramDir">ParameterDirection</param>
        /// <returns>QueryParameterBase</returns>
        public QueryParameterBase AddQueryParameter( string name, object value,ParameterDirection paramDir )
        {
            return AddQueryParameter( new QueryParameterBase { Name = name, Value = value, ParamDirection =paramDir } );
        }
        #endregion
    }
}