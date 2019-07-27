using System.Collections.Generic;

namespace Moons.Common20.QueryData
{
    /// <summary>
    /// 查询数据的接口
    /// </summary>
    public interface IQueryData
    {
        /// <summary>
        ///  查询数据
        /// </summary>
        /// <param name="queryInfoBag">QueryInfoBag</param>
        /// <returns>数据</returns>
        NameValueRowCollection Query( QueryInfoBag queryInfoBag );

        /// <summary>
        /// 查询记录数
        /// </summary>
        /// <param name="queryInfoBag">QueryInfoBag</param>
        /// <returns>记录数</returns>
        int QueryCount( QueryInfoBag queryInfoBag );

         /// <summary>
        /// 获得参数列表
        /// </summary>
        List<QueryConditionBase> GetParameterList(QueryInfoBag queryInfoBag);
    }
}