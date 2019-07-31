using System;

namespace AnalysisData
{
    /// <summary>
    /// 特征频率分组数据
    /// </summary>
    [Serializable]
    public class FeatureFreqGroupData
    {
        /// <summary>
        /// 特征频率分组ID
        /// </summary>
        public int Group_ID
        {
            get;
            set;
        }

        /// <summary>
        /// 特征频率分组名称
        /// </summary>
        public string GroupName_TX
        {
            get;
            set;
        }
    }
}
