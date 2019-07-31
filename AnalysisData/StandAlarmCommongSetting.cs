using System;

namespace AnalysisData
{
    /// <summary>
    /// 标准报警设置数据
    /// </summary>
    [Serializable]
    public class StandAlarmCommongSetting
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int SettingID { get; set; }

        /// <summary>
        /// 报警类型
        /// </summary>
        public byte? AlmType_ID { get; set; }

        /// <summary>
        /// 下限
        /// </summary>
        public double? LowLimit1_NR { get; set; }

        /// <summary>
        /// 下限
        /// </summary>
        public double? LowLimit2_NR { get; set; }

        /// <summary>
        /// 下限
        /// </summary>
        public double? LowLimit3_NR { get; set; }

        /// <summary>
        /// 下限
        /// </summary>
        public double? LowLimit4_NR { get; set; }

        /// <summary>
        /// 上限
        /// </summary>
        public double? HighLimit1_NR { get; set; }

        /// <summary>
        /// 上限
        /// </summary>
        public double? HighLimit2_NR { get; set; }

        /// <summary>
        /// 上限
        /// </summary>
        public double? HighLimit3_NR { get; set; }

        /// <summary>
        /// 上限
        /// </summary>
        public double? HighLimit4_NR { get; set; }
    }
}