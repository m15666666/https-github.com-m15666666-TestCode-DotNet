using System;
using System.Collections.Generic;
using System.Text;

namespace AnalysisData.FeatureFreq
{
    /// <summary>
    /// 叶片通过特征频率
    /// </summary>
    public class FanFeatureFreq
    {
        /// <summary>
        /// 轴乘数（本轴的转速与采集的记录RPM转速的比值）
        /// </summary>
        public double ShaftMultiplier { get; set; } = 1;

        /// <summary>
        /// 叶片数
        /// </summary>
        public int BladeCount { get; set; }
    }
}
