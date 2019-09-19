using Moons.EquipmentDiagnosis.Core.Dto;
using Moons.EquipmentDiagnosis.Core.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moons.EquipmentDiagnosis.Core.Implementations
{
    /// <summary>
    /// 趋势报警、诊断
    /// </summary>
    public class TrendDiagnosis
    {
        public TrendDiagnosisOutputDto MakeDiagnosis(TrendDiagnosisInputDto input)
        {
            switch (input.SignalTypeId)
            {
                case SignalTypeIdEnum.Vel:
                    return MakeDiagnosis_Vel(input);
                case SignalTypeIdEnum.Acc:
                    return MakeDiagnosis_Acc(input);
                case SignalTypeIdEnum.Temperature:
                    return MakeDiagnosis_Temperature(input);
            }
            return null;
        }

        private TrendDiagnosisOutputDto MakeDiagnosis_Vel(TrendDiagnosisInputDto input)
        {
            TrendDiagnosisOutputDto output = new TrendDiagnosisOutputDto();
            CalcAlarm(SignalTypeIdEnum.Vel, AlarmTypeIdEnum.Alm_Trend_30Day, input.TrendData_30Day, TrendAlarmSetting.TrendAlarmSetting_Vel_30Day, output);
            CalcAlarm(SignalTypeIdEnum.Vel, AlarmTypeIdEnum.Alm_Trend_10Day, input.TrendData_10Day, TrendAlarmSetting.TrendAlarmSetting_Vel_10Day,output);
            CalcAlarm(SignalTypeIdEnum.Vel, AlarmTypeIdEnum.Alm_Trend_5Day, input.TrendData_5Day, TrendAlarmSetting.TrendAlarmSetting_Vel_5Day, output);
            CalcAlarm(SignalTypeIdEnum.Vel, AlarmTypeIdEnum.Alm_Trend_24Hour, input.TrendData_24Hour, TrendAlarmSetting.TrendAlarmSetting_Vel_24Hour, output);
            CalcAlarm(SignalTypeIdEnum.Vel, AlarmTypeIdEnum.Alm_Trend_2Hour, input.TrendData_2Hour, TrendAlarmSetting.TrendAlarmSetting_Vel_2Hour, output);
            CalcAlarm(SignalTypeIdEnum.Vel, AlarmTypeIdEnum.Alm_Trend_Impulse, input.TrendData_Impulse, TrendAlarmSetting.TrendAlarmSetting_Vel_Impulse, output);
            return output;
        }

        private TrendDiagnosisOutputDto MakeDiagnosis_Acc(TrendDiagnosisInputDto input)
        {
            TrendDiagnosisOutputDto output = new TrendDiagnosisOutputDto();
            CalcAlarm(SignalTypeIdEnum.Acc, AlarmTypeIdEnum.Alm_Trend_30Day, input.TrendData_30Day, TrendAlarmSetting.TrendAlarmSetting_Acc_30Day, output);
            CalcAlarm(SignalTypeIdEnum.Acc, AlarmTypeIdEnum.Alm_Trend_10Day, input.TrendData_10Day, TrendAlarmSetting.TrendAlarmSetting_Acc_10Day, output);
            CalcAlarm(SignalTypeIdEnum.Acc, AlarmTypeIdEnum.Alm_Trend_5Day, input.TrendData_5Day, TrendAlarmSetting.TrendAlarmSetting_Acc_5Day, output);
            CalcAlarm(SignalTypeIdEnum.Acc, AlarmTypeIdEnum.Alm_Trend_24Hour, input.TrendData_24Hour, TrendAlarmSetting.TrendAlarmSetting_Acc_24Hour, output);
            CalcAlarm(SignalTypeIdEnum.Acc, AlarmTypeIdEnum.Alm_Trend_2Hour, input.TrendData_2Hour, TrendAlarmSetting.TrendAlarmSetting_Acc_2Hour, output);
            CalcAlarm(SignalTypeIdEnum.Acc, AlarmTypeIdEnum.Alm_Trend_Impulse, input.TrendData_Impulse, TrendAlarmSetting.TrendAlarmSetting_Acc_Impulse, output);
            return output;
        }

        private TrendDiagnosisOutputDto MakeDiagnosis_Temperature(TrendDiagnosisInputDto input)
        {
            TrendDiagnosisOutputDto output = new TrendDiagnosisOutputDto();
            CalcAlarm(SignalTypeIdEnum.Temperature, AlarmTypeIdEnum.Alm_Trend_2Hour, input.TrendData_2Hour, TrendAlarmSetting.TrendAlarmSetting_Temperature_2Hour, output);
            CalcAlarm(SignalTypeIdEnum.Temperature, AlarmTypeIdEnum.Alm_Trend_Impulse, input.TrendData_Impulse, TrendAlarmSetting.TrendAlarmSetting_Temperature_Impulse, output);
            return output;
        }

        /// <summary>
        /// 计算报警
        /// </summary>
        /// <param name="signalTypeId"></param>
        /// <param name="alarmTypeId"></param>
        /// <param name="yData"></param>
        /// <param name="alarmSetting"></param>
        /// <param name="output"></param>
        private void CalcAlarm(SignalTypeIdEnum signalTypeId, AlarmTypeIdEnum alarmTypeId, double[] yData, TrendAlarmSetting alarmSetting,TrendDiagnosisOutputDto output )
        {
            if (yData == null || yData.Length == 0) return;

            var lineFit = LineFitUtils.CreateByYData(yData); // 生成直线拟合
            double first = lineFit.CalcY(lineFit.X[0]);
            if (first == 0) return;

            double last = lineFit.CalcY(lineFit.X[lineFit.X.Length-1]);
            double k = last / first;

            List<AlarmEvent> list = new List<AlarmEvent>();
            if(signalTypeId == SignalTypeIdEnum.Temperature) // 处理温度
            {
                if (first < alarmSetting.Threshold1)
                {
                    if (alarmSetting.KThreshold1 < k) list.Add(new AlarmEvent { AlarmTypeId = alarmTypeId });
                }
                else if (alarmSetting.KThreshold2 < k) list.Add(new AlarmEvent { AlarmTypeId = alarmTypeId });

                if (0 < list.Count) output.AlarmEvents.AddRange(list);
                return;
            }

            if(alarmTypeId == AlarmTypeIdEnum.Alm_Trend_Impulse) // 单次突变
            {
                if (signalTypeId == SignalTypeIdEnum.Vel)
                {
                    if (first <= alarmSetting.Threshold1) return;

                    if (first <= alarmSetting.Threshold2)
                    {
                        if (alarmSetting.KThreshold1 < k) list.Add(new AlarmEvent { AlarmTypeId = alarmTypeId });
                    }
                    else if (first <= alarmSetting.Threshold3)
                    {
                        if (alarmSetting.KThreshold2 < k) list.Add(new AlarmEvent { AlarmTypeId = alarmTypeId });
                    }
                    else if (alarmSetting.KThreshold3 < k) list.Add(new AlarmEvent { AlarmTypeId = alarmTypeId });
                }
                else if (signalTypeId == SignalTypeIdEnum.Acc)
                {
                    if (first <= alarmSetting.Threshold1) return;

                    if (first <= alarmSetting.Threshold2)
                    {
                        if (alarmSetting.KThreshold1 < k) list.Add(new AlarmEvent { AlarmTypeId = alarmTypeId });
                    }
                    else if (alarmSetting.KThreshold2 < k) list.Add(new AlarmEvent { AlarmTypeId = alarmTypeId });
                }
                if (0 < list.Count) output.AlarmEvents.AddRange( list );
                return;
            }

            if(first <= alarmSetting.Threshold1)
            {
                if (alarmSetting.KThreshold1 <= k) list.Add(new AlarmEvent { AlarmTypeId = alarmTypeId });
            }
            else if (first <= alarmSetting.Threshold2)
            {
                if (alarmSetting.KThreshold2 <= k) list.Add(new AlarmEvent { AlarmTypeId = alarmTypeId });
            }
            else if (alarmSetting.KThreshold3 <= k) list.Add(new AlarmEvent { AlarmTypeId = alarmTypeId });

            if (0 < list.Count) output.AlarmEvents.AddRange(list);
        }
    }
}
