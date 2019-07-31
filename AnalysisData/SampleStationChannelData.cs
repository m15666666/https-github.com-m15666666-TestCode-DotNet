using System;

namespace AnalysisData
{
    /// <summary>
    /// 采集工作站通道
    /// </summary>
    [Serializable]
    public class SampleStationChannelData
    {
        /// <summary>
        /// 采集工作站ID
        /// </summary>
        public int Station_ID { get; set; }
        /// <summary>
        /// 采集工作站名称
        /// </summary>
        public string Name_TX { get; set; }
        /// <summary>
        /// 采集工作站通道ID
        /// </summary>
        public int StationChannel_ID { get; set; }
        /// <summary>
        /// 通道类型编号
        /// </summary>
        public int ChannelType_NR { get; set; }
        /// <summary>
        /// 通道类型名称
        /// </summary>
        public string Type_TX { get; set; }
        /// <summary>
        /// 通道号
        /// </summary>
        public int ChannelNumber_NR { get; set; }
        /// <summary>
        /// 通道参数
        /// </summary>
        public string StationChannelParam_TX { get; set; }

    }
}
