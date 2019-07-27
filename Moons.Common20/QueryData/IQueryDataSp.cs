namespace Moons.Common20.QueryData
{
    /// <summary>
    /// 实用存储过程查询数据的接口
    /// </summary>
    public interface IQueryDataSp
    {
        /// <summary>
        ///  查询数据
        /// </summary>
        /// <param name="queryInfoBag">QueryInfoBagSp</param>
        /// <returns>数据</returns>
        NameValueRowCollection Query( QueryInfoBagSp queryInfoBag );

        /// <summary>
        ///  执行存储过程
        /// </summary>
        /// <param name="queryInfoBag">QueryInfoBagSp</param>
        void ExecuteNonQuery( QueryInfoBagSp queryInfoBag );

        /// <summary>
        /// 获取记录总数，只用于项目（如：2641、2642）特定格式的存储过程
        /// </summary>
        /// <param name="queryInfoBag">QueryInfoBagSp</param>
        /// <returns>记录总数</returns>
        int GetRecordCount( QueryInfoBagSp queryInfoBag );
    }
}