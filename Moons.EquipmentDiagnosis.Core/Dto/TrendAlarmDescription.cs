using System;
using System.Collections.Generic;
using System.Text;

namespace Moons.EquipmentDiagnosis.Core.Dto
{
    public class TrendAlarmDescription
    {
        public static string Alm_30Day { get; set; } = "30日趋势报警";
        public static string Alm_10Day { get; set; } = "10日趋势报警";
        public static string Alm_5Day { get; set; } = "5日趋势报警";
        public static string Alm_24Hour { get; set; } = "24小时报警";
        public static string Alm_2Hour { get; set; } = "2小时报警";
        public static string Alm_Vel_Impulse { get; set; } = "振动速度有效值突然增大";
        public static string Alm_Acc_Impulse { get; set; } = "振动加速度峰值突然增大";
        public static string Alm_Temperature_Impulse { get; set; } = "轴承温度突然增大";
    }
}
