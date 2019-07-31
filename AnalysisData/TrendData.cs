using System;

namespace AnalysisData
{
    /// <summary>
    /// 趋势数据
    /// </summary>
    [Serializable]
    public class TrendData
    {
        /// <summary>
        /// 采样时间
        /// </summary>
        public DateTime SampleTime { get; set; }

        /// <summary>
        /// 测量值
        /// </summary>
        public double MeasurementValue { get; set; }

        /// <summary>
        /// 无线通讯信号强度百分比，范围：0 ~ 1。
        /// </summary>
        public float? WirelessSignalIntensity { get; set; }

        /// <summary>
        /// 剩余电池电量百分比，范围：0 ~ 1。
        /// </summary>
        public float? BatteryPercent { get; set; }

        /// <summary>
        /// 数据长度
        /// </summary>
        public int DataLength { get; set; }

        /// <summary>
        /// 数据编号
        /// </summary>
        public long DataId { get; set; }

        /// <summary>
        /// 测点编号
        /// </summary>
        public int Point_ID { get; set; }

        /// <summary>
        /// 通道号
        /// </summary>
        public int ChannelNumber { get; set; }

        /// <summary>
        /// 分钟转速
        /// </summary>
        public int Rev { get; set; }

        /// <summary>
        /// 压缩ID
        /// </summary>
        public int CompressID { get; set; }

        /// <summary>
        /// 分区ID
        /// </summary>
        public long PartitionID { get; set; }

        /// <summary>
        ///     报警级别ID
        /// </summary>
        public int AlmLevelID { get; set; }

        /// <summary>
        ///     信号类型
        /// </summary>
        public int SigType_NR { get; set; }

        /// <summary>
        ///     信号类型名称
        /// </summary>
        public string SigType_TX { get; set; }

        /// <summary>
        ///     单位
        /// </summary>
        public int EngUnit_ID { get; set; }

        /// <summary>
        ///     工程单位名称（英文）
        /// </summary>
        public string EngUnitEn_TX { get; set; }

        /// <summary>
        ///     采样方式
        /// </summary>
        public int SampMod { get; set; }

        /// <summary>
        ///     类型ID
        /// </summary>
        public int TypeID { get; set; }
    }
}
