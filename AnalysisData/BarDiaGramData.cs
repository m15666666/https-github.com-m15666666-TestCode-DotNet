using System;
using System.Collections.Generic;

namespace AnalysisData
{
    /// <summary>
    /// 棒图组设置数据
    /// </summary>
    [Serializable]
    public class BarDiaGramData
    {
        /// <summary>
        /// 组名
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// 组编号
        /// </summary>
        public int GroupID { get; set; }

        /// <summary>
        /// 组对应设备ID
        /// </summary>
        public int MObjectID { get; set; }

        /// <summary>
        /// 棒图测点设置数据
        /// </summary>
        public List<BarPointData> BarPoints { get; set; }
    }

    /// <summary>
    /// 棒图测点设置数据
    /// </summary>
    [Serializable]
    public class BarPointData
    {
        /// <summary>
        /// 测点名称
        /// </summary>
        public string PointName { get; set; }

        /// <summary>
        /// 测点编号
        /// </summary>
        public int PointID { get; set; }

        /// <summary>
        /// 特征值编号
        /// </summary>
        public int FeatureValueID { get; set; }

        /// <summary>
        /// 特征值名称
        /// </summary>
        public string FeatureValueName { get; set; }

        /// <summary>
        /// 上限值
        /// </summary>
        public double HighValue { get; set; }

        /// <summary>
        /// 下限值
        /// </summary>
        public double LowValue { get; set; }
    }
}
