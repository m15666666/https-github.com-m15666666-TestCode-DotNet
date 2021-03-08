using System;
using System.Collections.Generic;
using System.Text;

namespace Moons.EquipmentDiagnosis.Core
{
    /// <summary>
    /// 设备类型枚举类
    /// </summary>
    public static class EquipmentType
    {
        public static class Motor
        {
            /// <summary>
            /// 通用电机001
            /// </summary>
            public static readonly string Generic001 = "-Motor-Generic001";

            /// <summary>
            /// 皮带机带两个电机001
            /// </summary>
            public static readonly string Belt_2Motor_001 = "-Belt_2Motor_001";
        }

        /// <summary>
        /// 泵
        /// </summary>
        public static class Pump
        {
            /// <summary>
            /// 悬臂泵001
            /// </summary>
            public static readonly string Generic001 = "-Pump-Generic001";
            
        }

        /// <summary>
        /// 风机
        /// </summary>
        public static class Fan
        {
            /// <summary>
            /// 悬臂风机001
            /// </summary>
            public static readonly string Generic001 = "-Fan-Generic001";
            
        }
    }
}
