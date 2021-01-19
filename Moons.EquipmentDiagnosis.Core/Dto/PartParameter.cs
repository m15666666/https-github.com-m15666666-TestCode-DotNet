using System;
using System.Collections.Generic;
using System.Text;
using AnalysisData.Constants;
using AnalysisData.FeatureFreq;

namespace Moons.EquipmentDiagnosis.Core.Dto
{
    /// <summary>
    /// 设备(部件)参数类
    /// </summary>
    public class PartParameter
    {
        #region 特征频率相关
        /// <summary>
        /// 叶片通过特征频率集合
        /// </summary>
        public List<FanFeatureFreq> FanFeatureFreqs { get; set; }

        /// <summary>
        /// 滚动轴承特征频率集合
        /// </summary>
        public List<BearingFeatureFreq> BearingFeatureFreqs { get; set; }
        #endregion

        #region 支撑相关
        /// <summary>
        /// 支撑类型
        /// </summary>
        public int SupportTypeID { get; set; } = EquipSupportTypeID.TwoEnd;
        #endregion

        #region 转速相关
        /// <summary>
        /// 转速类型
        /// </summary>
        public int SpeedTypeID { get; set; } = EquipSpeedTypeID.Stable;
        #endregion

        #region 基础相关

        /// <summary>
        /// 基础类型
        /// </summary>
        public int BaseTypeID { get; set; } = EquipBaseTypeID.Stiffness;

        /// <summary>
        /// 是否刚性基础
        /// </summary>
        public bool IsStiffBase => BaseTypeID == EquipBaseTypeID.Stiffness;

        /// <summary>
        /// 是否柔性基础
        /// </summary>
        public bool IsFlexibilityBase => BaseTypeID == EquipBaseTypeID.Flexibility;

        #endregion

        #region 负载相关

        /// <summary>
        /// 电流测点
        /// </summary>
        public PointData Point_I { get; set; }
        /// <summary>
        /// 入口流量测点
        /// </summary>
        public PointData Point_QI { get; set; }
        /// <summary>
        /// 出口流量测点
        /// </summary>
        public PointData Point_QO { get; set; }

        /// <summary>
        /// 按照优先顺序返回第一个负载测点
        /// </summary>
        public PointData FirstLoadPoint => Point_I ?? (Point_QI ?? Point_QO);

        #endregion
    }
}
