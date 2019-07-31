using System;

namespace AnalysisData
{
    /// <summary>
    /// 测点位置
    /// </summary>
    [Serializable]
    public class PointPosition
    {
        /// <summary>
        /// 设备ID
        /// </summary>
        public int MObject_ID { get; set; }
        /// <summary>
        /// 测点ID
        /// </summary>
        public int Point_ID { get; set; }
        /// <summary>
        /// 测点名称
        /// </summary>
        public string PointNameTX { get; set; }
        /// <summary>
        /// 工程单位名称
        /// </summary>
        public string EngUnitName { get; set; }
        /// <summary>
        /// 测点横坐标
        /// </summary>
        public double? PosX_NR{get;set;}
        /// <summary>
        /// 测点纵坐标
        /// </summary>
        public double? PosY_NR{get;set;}
        /// <summary>
        /// 标签横坐标
        /// </summary>
        public double? TagX_NR{get;set;}
        /// <summary>
        /// 标签纵坐标
        /// </summary>
        public double? TagY_NR{get;set;}

        /// <summary>
        /// 自定义测量值类型
        /// </summary>
        public short? CustomFeatureValue_ID { get; set; }

        /// <summary>
        /// 信号类型编号
        /// </summary>
        public short? SigType_NR { get; set; }
    }
}
