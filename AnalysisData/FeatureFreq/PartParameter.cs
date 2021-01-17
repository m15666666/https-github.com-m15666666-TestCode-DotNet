using System;
using System.Collections.Generic;
using System.Text;
using AnalysisData.Constants;

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

        /// <summary>
        /// 基础类型
        /// </summary>
        public int BaseTypeID { get; set; } = EquipBaseTypeID.Stiffness;

        /// <summary>
        /// 支撑类型
        /// </summary>
        public int SupportTypeID { get; set; } = EquipSupportTypeID.TwoEnd;

        /// <summary>
        /// 转速类型
        /// </summary>
        public int SpeedTypeID { get; set; } = EquipSpeedTypeID.Stable;

        /// <summary>
        /// 是否刚性基础
        /// </summary>
        public bool IsStiffBase => BaseTypeID == EquipBaseTypeID.Stiffness;

        /// <summary>
        /// 是否柔性基础
        /// </summary>
        public bool IsFlexibilityBase => BaseTypeID == EquipBaseTypeID.Flexibility;
    }
}
