using System;

namespace AnalysisData
{
    /// <summary>
    /// 趋势数据查询参数
    /// </summary>
    [Serializable]
    public class TrendQueryParam
    {
        /// <summary>
        /// 时间段的开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 时间段的结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 测点编号
        /// </summary>
        public int PointID { get; set; }

        /// <summary>
        /// 通道编号
        /// </summary>
        public byte ChannelNumber { get; set; }

        /// <summary>
        /// 指标编号
        /// </summary>
        public int FeatureValueID { get; set; }

        /// <summary>
        /// 趋势类型编号类
        /// </summary>
        public int TrendTypeID { get; set; }

        /// <summary>
        /// 是否根据速过滤趋势数据
        /// </summary>
        public bool isFiltTrendDataBySpeed
        {
            get; set;
        }

        /// <summary>
        /// 转速上限
        /// </summary>
        public int RotSpeedUpper
        {
            get; set;
        }

        /// <summary>
        /// 转速下限
        /// </summary>
        public int RotSpeedLower
        {
            get; set;
        }
    }
}
