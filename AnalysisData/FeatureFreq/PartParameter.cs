using System;
using System.Collections.Generic;
using System.Text;

namespace AnalysisData.FeatureFreq
{
    /// <summary>
    /// 设备(部件)参数类
    /// </summary>
    public class PartParameter
    {
        /// <summary>
        /// 叶片通过特征频率集合
        /// </summary>
        public List<FanFeatureFreq> FanFeatureFreqs { get; set; }

        /// <summary>
        /// 滚动轴承特征频率集合
        /// </summary>
        public List<BearingFeatureFreq> BearingFeatureFreqs { get; set; }
    }
}
