using System;

namespace AnalysisData.SampleData
{
    /// <summary>
    /// 被缓存的监测数据
    /// </summary>
    [Serializable]
    public class CachedMornitorData
    {
        /// <summary>
        /// 数据编号
        /// </summary>
        public long DataID { get; set; }

        /// <summary>
        /// 分析的数据
        /// </summary>
        public TrendData Data { get; set; }
    }
}