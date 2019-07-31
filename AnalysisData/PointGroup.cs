using System;

namespace AnalysisData
{
    /// <summary>
    /// 测点分组数据
    /// </summary>
    [Serializable]
    public class PointGroup
    {
        /// <summary>
        /// 组编号
        /// </summary>
        public int PointGroup_ID { get; set; }

        /// <summary>
        /// 测点编号
        /// </summary>
        public int Point_ID { get; set; }

        /// <summary>
        /// 设备编号
        /// </summary>
        public int Mobject_ID { get; set; }

        /// <summary>
        /// 组号
        /// </summary>
        public int GroupNo_NR { get; set; }

        /// <summary>
        /// 等级号
        /// </summary>
        public int Level_NR { get; set; }

        public double? TopClearance_NR { get; set; }
    }
}