using System;

namespace AnalysisData
{
    /// <summary>
    /// 时间波形数据查询参数
    /// </summary>
    [Serializable]
    public class TimeWaveQueryParam
    {
        /// <summary>
        /// 历史数据编号
        /// </summary>
        public long HistoryID { get; set; }

        /// <summary>
        /// 通道编号
        /// </summary>
        public byte ChannelNumber { get; set; }

        /// <summary>
        /// 趋势类型编号类
        /// </summary>
        public int TrendTypeID { get; set; }
    }
}
