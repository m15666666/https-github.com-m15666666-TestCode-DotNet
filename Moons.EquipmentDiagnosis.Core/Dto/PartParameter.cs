using AnalysisData.Constants;
using AnalysisData.FeatureFreq;
using System.Collections.Generic;

namespace Moons.EquipmentDiagnosis.Core.Dto
{
    /// <summary>
    /// 设备(部件)参数类
    /// </summary>
    public class PartParameter
    {
        #region 趋势相关

        /// <summary>
        /// 查询趋势数据的天数
        /// </summary>
        public int TrendDays { get; set; } = 4;

        #endregion 趋势相关

        #region 电机相关

        /// <summary>
        /// 电机极数
        /// </summary>
        public int MotorPoles { get; set; } = 0;

        /// <summary>
        /// 电机额定转速，每分钟多少转
        /// </summary>
        public int MotorRatedRev { get; set; } = 0;

        /// <summary>
        /// 电机滑差频率
        /// </summary>
        public double MotorSlipDiffFrequence
        {
            get
            {
                if (MotorPoles <= 0 || MotorRatedRev <= 0) return 0;

                double f0 = MotorRatedRev / 60f; // 实际转速
                double syncFreq; // 旋转磁场转速（同步转速）
                switch (MotorPoles)
                {
                    case 2: syncFreq = 50;/* 3000 转每分 */ break;
                    case 4: syncFreq = 25;/* 1500 转每分 */ break;
                    case 6: syncFreq = 16.67;/* 1000 转每分 */ break;
                    case 8: syncFreq = 12.5;/* 750 转每分 */ break;
                    default: return 0;
                }
                return syncFreq <= f0 ? 0 : (syncFreq - f0) * MotorPoles;
            }
        }

        #endregion 电机相关

        #region 特征频率相关

        /// <summary>
        /// 叶片通过特征频率集合
        /// </summary>
        public List<FanFeatureFreq> FanFeatureFreqs { get; set; }

        /// <summary>
        /// 滚动轴承特征频率集合
        /// </summary>
        public List<BearingFeatureFreq> BearingFeatureFreqs { get; set; }

        #endregion 特征频率相关

        #region 支撑相关

        /// <summary>
        /// 支撑类型
        /// </summary>
        public int SupportTypeID { get; set; } = EquipSupportTypeID.TwoEnd;

        /// <summary>
        /// 是否为两端支撑
        /// </summary>
        public bool IsTwoEnd => SupportTypeID == EquipSupportTypeID.TwoEnd;

        #endregion 支撑相关

        #region 转速相关

        /// <summary>
        /// 转速类型
        /// </summary>
        public int SpeedTypeID { get; set; } = EquipSpeedTypeID.Stable;

        /// <summary>
        /// 是否变转速
        /// </summary>
        public bool IsUnStableSpeed => SpeedTypeID == EquipSpeedTypeID.Untable;

        /// <summary>
        /// 是否定转速
        /// </summary>
        public bool IsStableSpeed => SpeedTypeID == EquipSpeedTypeID.Stable;

        #endregion 转速相关

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
        /// 是否弹性基础
        /// </summary>
        public bool IsFlexibilityBase => BaseTypeID == EquipBaseTypeID.Flexibility;

        #endregion 基础相关

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

        #endregion 负载相关
    }
}