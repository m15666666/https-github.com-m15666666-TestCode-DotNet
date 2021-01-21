using System;
using System.Collections.Generic;
using System.Text;

namespace AnalysisData.Constants
{
    /// <summary>
    /// 设备基础类型ID
    /// </summary>
    public static class EquipBaseTypeID
    {
        /// <summary>
        /// 刚性基础
        /// </summary>
        public const int Stiffness = 0;

        /// <summary>
        /// 弹性基础
        /// </summary>
        public const int Flexibility = 1;
    }

    /// <summary>
    /// 设备支撑类型ID
    /// </summary>
    public static class EquipSupportTypeID
    {
        /// <summary>
        /// 两端支撑
        /// </summary>
        public const int TwoEnd = 0;

        /// <summary>
        /// 悬臂支撑
        /// </summary>
        public const int OneEnd = 1;
    }

    /// <summary>
    /// 设备转速类型ID
    /// </summary>
    public static class EquipSpeedTypeID
    {
        /// <summary>
        /// 定转速
        /// </summary>
        public const int Stable = 0;

        /// <summary>
        /// 变转速
        /// </summary>
        public const int Untable = 1;
    }
}
