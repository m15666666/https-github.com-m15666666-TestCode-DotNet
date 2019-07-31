using Moons.Common20.Serialization;

namespace AnalysisData.ToFromBytes
{
    /// <summary>
    /// 结构类型ID
    /// </summary>
    public static class StructTypeID
    {
        /// <summary>
        /// 结构体类型ID的起始
        /// </summary>
        private const int StructTypeIDBase = StructTypeIDs.StructTypeIDBase;

        /// <summary>
        /// 单点数据
        /// </summary>
        public const int TrendData = StructTypeIDBase + 1;

        /// <summary>
        /// 一维时间波形数据
        /// </summary>
        public const int TimeWaveData_1D = StructTypeIDBase + 2;

        /// <summary>
        /// 二维时间波形数据
        /// </summary>
        public const int TimeWaveData_2D = StructTypeIDBase + 3;

        /// <summary>
        /// 报警的一维时间波形数据
        /// </summary>
        public const int AlmTimeWaveData_1D = StructTypeIDBase + 5;

        /// <summary>
        /// 报警的趋势数据（用于数值量报警）
        /// </summary>
        public const int AlmTrendData = StructTypeIDBase + 6;

        /// <summary>
        /// 无线传感器趋势数据
        /// </summary>
        public const int WirelessTrendData = StructTypeIDBase + 11;

        /// <summary>
        /// 带特征值的趋势数据，目前无线传感器使用
        /// </summary>
        public const int FeatureValueTrendData = StructTypeIDBase + 12;

        /// <summary>
        ///     变量趋势数据Int16，目前用于PLC传输数据
        /// </summary>
        public const int VariantTrendDataInt16 = StructTypeIDBase + 16;

        /// <summary>
        /// 报警事件数据
        /// </summary>
        public const int AlmEventData = StructTypeIDBase + 71;

        /// <summary>
        /// 采集器的状态数据
        /// </summary>
        public const int SamplerStatusData = StructTypeIDBase + 101;

        /// <summary>
        /// 采集器的硬件信息数据
        /// </summary>
        public const int SamplerHardwareInfoData = StructTypeIDBase + 102;

        /// <summary>
        /// 采集器的校时数据
        /// </summary>
        public const int TimingData = StructTypeIDBase + 103;

        /// <summary>
        /// 测点配置数据
        /// </summary>
        public const int PointConfigData = StructTypeIDBase + 111;

        /// <summary>
        /// 通道配置数据
        /// </summary>
        public const int ChannelConfigData = StructTypeIDBase + 112;

        /// <summary>
        /// 普通报警设置数据
        /// </summary>
        public const int AlmStand_CommonSettingData = StructTypeIDBase + 113;

        /// <summary>
        /// 连续报警次数设置数据
        /// </summary>
        public const int AlmCountData = StructTypeIDBase + 114;

        /// <summary>
        /// 采集工作站配置数据
        /// </summary>
        public const int SampleStationConfigData = StructTypeIDBase + 115;

        /// <summary>
        /// 采集工作站配置数据，版本2
        /// </summary>
        public const int SampleStationConfigData2 = StructTypeIDBase + 116;

        /// <summary>
        /// 文件片段数据
        /// </summary>
        public const int FileSegmentData = StructTypeIDBase + 121;

        /// <summary>
        /// 测点ID集合数据
        /// </summary>
        public const int PointIDCollectionData = StructTypeIDBase + 131;

        /// <summary>
        /// 采集额外的测点数据
        /// </summary>
        public const int SampleExtraPointData = StructTypeIDBase + 141;

        /// <summary>
        /// 错误消息，用于传递错误信息
        /// </summary>
        public const int ErrorMessage = StructTypeIDBase + 201;

        /// <summary>
        /// 采集工作站参数错误数据集合，用于传递下位机参数错误信息
        /// </summary>
        public const int ParameterErrorDataCollection = StructTypeIDBase + 202;
    }
}