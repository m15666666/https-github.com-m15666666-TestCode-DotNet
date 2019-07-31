using System;
using Moons.Common20;

namespace AnalysisData.SampleData
{
    /// <summary>
    /// 活动采集工作站数据类
    /// </summary>
    [Serializable]
    public class ActiveSampleStationData : EntityBase
    {
        #region 变量和属性

        /// <summary>
        /// 采集工作站的IP（例如：“127.0.0.1”）
        /// </summary>
        public string SampleStationIP { get; set; }

        /// <summary>
        /// 采集工作站的端口号（例如：“1181”）
        /// </summary>
        public int SampleStationPort { get; set; }

        /// <summary>
        /// 波形测点编号数组
        /// </summary>
        public int[] TimeWavePointId { get; set; }

        #endregion
    }
}