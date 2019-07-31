using System;

namespace AnalysisData.SampleData
{
    /// <summary>
    ///     数据采集器配置数据类
    /// </summary>
    [Serializable]
    public class DataSamplerConfigData
    {
        /// <summary>
        ///     变量与测点映射数据集合
        /// </summary>
        public Variant2PointData[] Variant2PointDatas { get; set; }

        /// <summary>
        ///     采集工作站集合
        /// </summary>
        public SampleStationData[] SampleStationDatas { get; set; }
    }
}