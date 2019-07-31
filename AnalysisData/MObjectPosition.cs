using System;

namespace AnalysisData
{
    /// <summary>
    /// 设备位置
    /// </summary>
    [Serializable]
    public class MObjectPosition
    {
        /// <summary>
        /// 设备ID
        /// </summary>
        public int MObject_ID { get; set; }
        /// <summary>
        /// 设备名称
        /// </summary>
        public string NameTX { get; set; }

        /// </summary>
        public double? PosX_NR { get; set; }
        /// <summary>
        /// 测点纵坐标

        /// </summary>
        public double? PosY_NR { get; set; }
        /// <summary>
        /// 标签横坐标

        /// </summary>
        public double? TagX_NR { get; set; }
        /// <summary>
        /// 标签纵坐标

        /// </summary>
        public double? TagY_NR { get; set; }
    }
}
