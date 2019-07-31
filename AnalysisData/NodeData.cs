using System;

namespace AnalysisData
{
    /// <summary>
    /// 节点类
    /// </summary>
    [Serializable]
    public class NodeData
    {
        /// <summary>
        /// 节点名
        /// </summary>
        public string Name_TX { get; set; }

        /// <summary>
        /// 组织机构ID
        /// </summary>
        public int Org_ID { get; set; }
    }
}
