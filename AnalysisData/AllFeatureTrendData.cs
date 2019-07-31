using System;

namespace AnalysisData
{
    /// <summary>
    /// 包含各指标值的趋势数据
    /// </summary>
    [Serializable]
    public class AllFeatureTrendData
    {
        /// <summary>
        /// 采样时间
        /// </summary>
        public DateTime SampleTime { get; set; }

        /// <summary>
        /// 特征参数ID
        /// </summary>
        public int FeatureValueID { get; set; }

        /// <summary>
        /// 测量值
        /// </summary>
        public double MeasurementValue { get; set; }

        /// <summary>
        /// 峰值
        /// </summary>
        public double? P { get; set; }

        /// <summary>
        /// 峰峰值
        /// </summary>
        public double? PP { get; set; }

        /// <summary>
        /// 有效值
        /// </summary>
        public double? RMS { get; set; }

        /// <summary>
        /// 均值
        /// </summary>
        public double? Mean { get; set; }

        /// <summary>
        /// 波形指标
        /// </summary>
        public double? ShapeFactor { get; set; }

        /// <summary>
        /// 峭度
        /// </summary>
        public double? KurtoFactor { get; set; }

        /// <summary>
        /// 轴位移
        /// </summary>
        public double? AxisOffset { get; set; }
        
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
        /// 报警编号
        /// </summary>
        public long Alm_ID { get; set; }

        /// <summary>
        /// 报警等级颜色
        /// </summary>
        public int AlmLevel_ID { get; set; }

        /// <summary>
        /// 同步号
        /// </summary>
        public long SynchNR { get; set; }
    }
}
