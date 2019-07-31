using System;

namespace AnalysisData.SampleData
{
    /// <summary>
    ///     变量与测点映射数据类
    /// </summary>
    [Serializable]
    public class Variant2PointData
    {
        /// <summary>
        ///     变量名
        /// </summary>
        public string VariantName { get; set; }

        /// <summary>
        ///     测点ID
        /// </summary>
        public int PointID { get; set; }

        /// <summary>
        ///     变量变换的比例因子
        /// </summary>
        public float Scale { get; set; }

        /// <summary>
        ///     变量变换的偏移
        /// </summary>
        public float Offset { get; set; }
    }
}