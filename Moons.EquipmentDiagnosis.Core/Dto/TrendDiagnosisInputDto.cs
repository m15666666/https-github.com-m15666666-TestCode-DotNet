using AnalysisData.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moons.EquipmentDiagnosis.Core.Dto
{
    public class TrendDiagnosisInputDto
    {
        /// <summary>
        /// 信号类型，101：速度、102：加速度
        /// </summary>
        public SignalTypeIdEnum SignalTypeId { get; set; }

        /// <summary>
        /// 30天趋势数据
        /// </summary>
        public double[] TrendData_30Day { get; set; }

        /// <summary>
        /// 10天趋势数据
        /// </summary>
        public double[] TrendData_10Day { get; set; }

        /// <summary>
        /// 5天趋势数据
        /// </summary>
        public double[] TrendData_5Day { get; set; }

        /// <summary>
        /// 24小时趋势数据
        /// </summary>
        public double[] TrendData_24Hour { get; set; }

        /// <summary>
        /// 2小时趋势数据
        /// </summary>
        public double[] TrendData_2Hour { get; set; }

        /// <summary>
        /// 	单次突变趋势数据
        /// </summary>
        public double[] TrendData_Impulse { get; set; }
    }

    public enum SignalTypeIdEnum
    {
        Vel = SignalTypeID.Velocity,
        Acc = SignalTypeID.Acceleration,
        Temperature = SignalTypeID.Tempt,
    }

    public enum AlarmTypeIdEnum
    {
        Alm_Trend_30Day = 101,
        Alm_Trend_10Day = 102,
        Alm_Trend_5Day = 103,
        Alm_Trend_24Hour = 104,
        Alm_Trend_2Hour = 105,
        Alm_Trend_Impulse = 111,
    }
}
