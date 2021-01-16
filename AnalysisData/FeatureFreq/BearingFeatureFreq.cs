using System;
using System.Collections.Generic;
using System.Text;

namespace AnalysisData.FeatureFreq
{
    /// <summary>
    /// 滚动轴承特征频率
    /// </summary>
    public class BearingFeatureFreq
    {
        /// <summary>
        /// 滚动体自传频率比值
        /// </summary>
        public double BSF { get; set; } = 2.07;

        /// <summary>
        /// 保持架频率比值
        /// </summary>
        public double FTF { get; set; } = 0.39;

        /// <summary>
        /// 滚动体过外环频率比值
        /// </summary>
        public double BPFO { get; set; } = 3.09;

        /// <summary>
        /// 滚动体过内环频率比值
        /// </summary>
        public double BPFI { get; set; } = 4.91;

        /// <summary>
        /// 轴乘数（本轴承的转速与采集的记录RPM转速的比值）
        /// </summary>
        public double ShaftMultiplier { get; set; } = 1;

        /// <summary>
        /// 滚动体数
        /// </summary>
        public int RollerCount { get; set; }
    }
}
