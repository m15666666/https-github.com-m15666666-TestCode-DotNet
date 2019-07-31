using System;

namespace AnalysisData
{
    /// <summary>
    /// 设备数据
    /// </summary>
    [Serializable]
    public class MObjectData : NodeData
    {
        /// <summary>
        /// 设备对象ID
        /// </summary>
        public int MObject_ID { get; set; }
    }
}
