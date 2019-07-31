using System;

namespace AnalysisData.SampleData
{
    /// <summary>
    /// 特征指标
    /// </summary>
    [Serializable]
    public class FeatureData
    {
        /// <summary>
        /// 测量值
        /// </summary>
        public double MeasurementValue { get; set; }

        /// <summary>
        /// 峰值
        /// </summary>
        public double P { get; set; }

        /// <summary>
        /// 峰峰值
        /// </summary>
        public double PP { get; set; }

        /// <summary>
        /// 有效值
        /// </summary>
        public double RMS { get; set; }

        /// <summary>
        /// 均值
        /// </summary>
        public double Mean { get; set; }

        /// <summary>
        /// 波形指标
        /// </summary>
        public double ShapeFactor { get; set; }

        /// <summary>
        /// 脉冲指标
        /// </summary>
        public double ImpulseFactor { get; set; }

        /// <summary>
        /// 峰值指标
        /// </summary>
        public double CrestFactor { get; set; }

        /// <summary>
        /// 裕度指标
        /// </summary>
        public double ClearanceFactor { get; set; }

        /// <summary>
        /// 歪度指标
        /// </summary>
        public double SkewFactor { get; set; }

        /// <summary>
        /// 峭度指标
        /// </summary>
        public double KurtoFactor { get; set; }

        /// <summary>
        /// 轴位移
        /// </summary>
        public double AxisOffset { get; set; }

        /// <summary>
        /// 复制
        /// </summary>
        /// <returns>复制的副本</returns>
        public FeatureData Clone()
        {
            return MemberwiseClone() as FeatureData;
        }
    }
}