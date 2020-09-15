using System;
using System.Collections.Generic;
using System.Text;

namespace Moons.EquipmentDiagnosis.Core.Dto
{
    /// <summary>
    /// 用于趋势报警的门限值等属性的设置
    /// </summary>
    public class TrendAlarmSettingV2
    {
        public double Threshold1 { get; set; }
        public double Threshold2 { get; set; }
        public double Threshold3 { get; set; }

        public double KThreshold1 { get; set; }
        public double KThreshold2 { get; set; }
        public double KThreshold3 { get; set; }
        public double KThreshold4 { get; set; }

        public static TrendAlarmSettingV2 TrendAlarmSetting_Acc_30Day = new TrendAlarmSettingV2
        {
            //Threshold1 = 10,
            KThreshold1 = 1,//4.5,
            //Threshold2 = 20,
            //KThreshold2 = 2.5,
            //KThreshold3 = 2
        };
        public static TrendAlarmSettingV2 TrendAlarmSetting_Acc_10Day = new TrendAlarmSettingV2
        {
            //Threshold1 = 10,
            KThreshold1 = 3,//4,
            //Threshold2 = 20,
            //KThreshold2 = 2,
            //KThreshold3 = 1.8
        };
        public static TrendAlarmSettingV2 TrendAlarmSetting_Acc_5Day = new TrendAlarmSettingV2
        {
            //Threshold1 = 10,
            //KThreshold1 = 3.5,
            //Threshold2 = 20,
            //KThreshold2 = 1.8,
            //KThreshold3 = 1.4
        };
        public static TrendAlarmSettingV2 TrendAlarmSetting_Acc_24Hour = new TrendAlarmSettingV2
        {
            //Threshold1 = 10,
            KThreshold1 = 1.25,//3.2,
            //Threshold2 = 20,
            //KThreshold2 = 1.6,
            //KThreshold3 = 1.2
        };
        public static TrendAlarmSettingV2 TrendAlarmSetting_Acc_2Hour = new TrendAlarmSettingV2
        {
            //Threshold1 = 10,
            KThreshold1 = 15,//3.6,
            //Threshold2 = 20,
            //KThreshold2 = 1.6,
            //KThreshold3 = 1.2
        };
        public static TrendAlarmSettingV2 TrendAlarmSetting_Vel_30Day = new TrendAlarmSettingV2
        {
            //Threshold1=1,
            KThreshold1 = 0.1,//3.5,
            //Threshold2 =3,
            //KThreshold2 =2.5,
            //KThreshold3 =2
        };
        public static TrendAlarmSettingV2 TrendAlarmSetting_Vel_10Day = new TrendAlarmSettingV2
        {
            //Threshold1 = 1,
            KThreshold1 = 0.3,//3,
            //Threshold2 = 3,
            //KThreshold2 = 2.2,
            //KThreshold3 = 1.8
        };
        public static TrendAlarmSettingV2 TrendAlarmSetting_Vel_5Day = new TrendAlarmSettingV2
        {
            //Threshold1 = 1,
            //KThreshold1 = 2.6,
            //Threshold2 = 3,
            //KThreshold2 = 2,
            //KThreshold3 = 1.6
        };
        public static TrendAlarmSettingV2 TrendAlarmSetting_Vel_24Hour = new TrendAlarmSettingV2
        {
            //Threshold1 = 1,
            KThreshold1 = 0.125,//2.2,
            //Threshold2 = 3,
            //KThreshold2 = 1.8,
            //KThreshold3 = 1.4
        };
        public static TrendAlarmSettingV2 TrendAlarmSetting_Vel_2Hour = new TrendAlarmSettingV2
        {
            //Threshold1 = 1,
            KThreshold1 = 1.5,//2.2,
            //Threshold2 = 3,
            //KThreshold2 = 1.8,
            //KThreshold3 = 1.4
        };
        public static TrendAlarmSettingV2 TrendAlarmSetting_Temperature_2Hour = new TrendAlarmSettingV2
        {
            //Threshold1 = 35,
            //KThreshold1 = 1.6,
            //KThreshold2 = 1.4,
        };
        public static TrendAlarmSettingV2 TrendAlarmSetting_Vel_Impulse = new TrendAlarmSettingV2
        {
            Threshold1 = 1,
            KThreshold1 = 3,
            Threshold2 = 3,
            KThreshold2 = 2,
            Threshold3 = 5,
            KThreshold3 = 1.7,
            KThreshold4 = 1.5,
        };
        public static TrendAlarmSettingV2 TrendAlarmSetting_Acc_Impulse = new TrendAlarmSettingV2
        {
            Threshold1 = 10,
            KThreshold1 = 4,
            Threshold2 = 30,
            KThreshold2 = 3,
            KThreshold3 = 2,
        };
        public static TrendAlarmSettingV2 TrendAlarmSetting_Temperature_Impulse = new TrendAlarmSettingV2
        {
            Threshold1 = 35,
            KThreshold1 = 1.5,
            KThreshold2 = 1.3,
        };
    }
}
