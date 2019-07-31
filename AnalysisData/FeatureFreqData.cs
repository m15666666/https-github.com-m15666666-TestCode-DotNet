using System;

namespace AnalysisData
{
    /// <summary>
    /// 特征频率数据
    /// </summary>
    [Serializable]
    public class FeatureFreqData
    {
        /// <summary>
        /// 特征频率ID
        /// </summary>
        public int FeatureFreq_ID
        {
            get;
            set;
        }

        /// <summary>
        /// 特征频率名称
        /// </summary>
        public string Name_TX
        {
            get;
            set;
        }

        /// <summary>
        /// 特征频率值
        /// </summary>
        public double FeatureFreqValue_NR
        {
            get;
            set;
        }

        /// <summary>
        /// 单位编号
        /// </summary>
        public int? Unit_NR
        {
            get;
            set;
        }

        /// <summary>
        /// 单位名称
        /// </summary>
        public string Unit_TX
        {
            get;
            set;
        }
    }
}
