namespace AnalysisData.EngUnits
{
    /// <summary>
    /// 单位数据类
    /// </summary>
    public class EngUnitData
    {
        /// <summary>
        /// 单位ID
        /// </summary>
        public int EngUnit_ID { get; set; }

        /// <summary>
        /// 是否默认单位
        /// </summary>
        public string IsDefault_YN { get; set; }

        /// <summary>
        /// 是否内置单位
        /// </summary>
        public string IsInner_YN { get; set; }

        /// <summary>
        /// 中文名称
        /// </summary>
        public string NameC_TX { get; set; }

        /// <summary>
        /// 英文名称
        /// </summary>
        public string NameE_TX { get; set; }

        /// <summary>
        /// 偏移量
        /// </summary>
        public double Offset_NR { get; set; }

        /// <summary>
        /// 比例因子
        /// </summary>
        public double Scale_NR { get; set; }

        /// <summary>
        /// 单位类型ID
        /// </summary>
        public int UnitType_ID { get; set; }
    }
}
