using Moons.EquipmentDiagnosis.Core.Dto;
using Moons.EquipmentDiagnosis.Core.Utils;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TrendAlarmSetting = Moons.EquipmentDiagnosis.Core.Dto.TrendAlarmSettingV2;

namespace Moons.EquipmentDiagnosis.Core.Implementations
{
    /// <summary>
    /// 趋势报警、诊断
    /// </summary>
    public class TrendDiagnosisV2
    {
        public TrendDiagnosisOutputDto MakeDiagnosis(TrendDiagnosisInputDto input)
        {
            //Config.Logger.Info("MakeDiagnosis");
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
            //Config.Logger.Info("MakeDiagnosis_Vel");
            TrendDiagnosisOutputDto output = new TrendDiagnosisOutputDto();
            CalcAlarm(SignalTypeIdEnum.Vel, AlarmTypeIdEnum.Alm_Trend_30Day, input.TrendData_30Day, input.TrendData_30Day_XAxis, TrendAlarmSetting.TrendAlarmSetting_Vel_30Day, output);
            CalcAlarm(SignalTypeIdEnum.Vel, AlarmTypeIdEnum.Alm_Trend_10Day, input.TrendData_10Day, input.TrendData_10Day_XAxis, TrendAlarmSetting.TrendAlarmSetting_Vel_10Day, output);
            CalcAlarm(SignalTypeIdEnum.Vel, AlarmTypeIdEnum.Alm_Trend_24Hour, input.TrendData_24Hour, input.TrendData_24Hour_XAxis, TrendAlarmSetting.TrendAlarmSetting_Vel_24Hour, output);
            CalcAlarm(SignalTypeIdEnum.Vel, AlarmTypeIdEnum.Alm_Trend_2Hour, input.TrendData_2Hour, input.TrendData_2Hour_XAxis, TrendAlarmSetting.TrendAlarmSetting_Vel_2Hour, output);
            CalcAlarmImpulse(SignalTypeIdEnum.Vel, input.TrendData_Impulse, TrendAlarmSetting.TrendAlarmSetting_Vel_Impulse, output);
            return output;
        }

        private TrendDiagnosisOutputDto MakeDiagnosis_Acc(TrendDiagnosisInputDto input)
        {
            TrendDiagnosisOutputDto output = new TrendDiagnosisOutputDto();
            CalcAlarm(SignalTypeIdEnum.Acc, AlarmTypeIdEnum.Alm_Trend_30Day, input.TrendData_30Day, input.TrendData_30Day_XAxis, TrendAlarmSetting.TrendAlarmSetting_Acc_30Day, output);
            CalcAlarm(SignalTypeIdEnum.Acc, AlarmTypeIdEnum.Alm_Trend_10Day, input.TrendData_10Day, input.TrendData_10Day_XAxis, TrendAlarmSetting.TrendAlarmSetting_Acc_10Day, output);
            CalcAlarm(SignalTypeIdEnum.Acc, AlarmTypeIdEnum.Alm_Trend_24Hour, input.TrendData_24Hour, input.TrendData_24Hour_XAxis, TrendAlarmSetting.TrendAlarmSetting_Acc_24Hour, output);
            CalcAlarm(SignalTypeIdEnum.Acc, AlarmTypeIdEnum.Alm_Trend_2Hour, input.TrendData_2Hour, input.TrendData_2Hour_XAxis, TrendAlarmSetting.TrendAlarmSetting_Acc_2Hour, output);
            CalcAlarmImpulse(SignalTypeIdEnum.Acc, input.TrendData_Impulse, TrendAlarmSetting.TrendAlarmSetting_Acc_Impulse, output);
            return output;
        }

        private TrendDiagnosisOutputDto MakeDiagnosis_Temperature(TrendDiagnosisInputDto input)
        {
            TrendDiagnosisOutputDto output = new TrendDiagnosisOutputDto();
            CalcAlarmImpulse(SignalTypeIdEnum.Temperature, input.TrendData_Impulse, TrendAlarmSetting.TrendAlarmSetting_Temperature_Impulse, output);
            return output;
        }

        /// <summary>
        /// 计算突变报警
        /// </summary>
        /// <param name="signalTypeId"></param>
        /// <param name="alarmTypeId"></param>
        /// <param name="yData"></param>
        /// <param name="alarmSetting"></param>
        /// <param name="output"></param>
        private void CalcAlarmImpulse(SignalTypeIdEnum signalTypeId, double[] yData, TrendAlarmSetting alarmSetting, TrendDiagnosisOutputDto output)
        {
            //Config.Logger.Info($"signalTypeId:{signalTypeId}");
            if (yData == null || yData.Length < 2)
            {
                //Config.Logger.Info("yData == null || yData.Length < 2");
                return;
            }

            const AlarmTypeIdEnum alarmTypeId = AlarmTypeIdEnum.Alm_Trend_Impulse;
            double first = yData.First();
            if (first == 0) return;

            var last = yData.Last();
            var k = last / first;
            var list = output.AlarmEvents;
            if (signalTypeId == SignalTypeIdEnum.Vel)
            {
                if (first <= alarmSetting.Threshold1)
                {
                    if (alarmSetting.KThreshold1 < k)
                    {
                        Config.Logger.Info($"impulse alm: signalTypeId:{signalTypeId},{alarmTypeId},k1: {k},{first},{last},{alarmSetting.KThreshold1}");
                        list.Add(new AlarmEvent { AlarmTypeId = alarmTypeId, Description = TrendAlarmDescription.Alm_Vel_Impulse });
                    }
                }
                else if (first <= alarmSetting.Threshold2)
                {
                    if (alarmSetting.KThreshold2 < k)
                    {
                        Config.Logger.Info($"impulse alm: signalTypeId:{signalTypeId},{alarmTypeId},k2: {k},{first},{last},{alarmSetting.KThreshold2}");
                        list.Add(new AlarmEvent { AlarmTypeId = alarmTypeId, Description = TrendAlarmDescription.Alm_Vel_Impulse });
                    }
                }
                else if (first <= alarmSetting.Threshold3)
                {
                    if (alarmSetting.KThreshold3 < k)
                    {
                        Config.Logger.Info($"impulse alm: signalTypeId:{signalTypeId},{alarmTypeId},k3: {k},{first},{last},{alarmSetting.KThreshold3}");
                        list.Add(new AlarmEvent { AlarmTypeId = alarmTypeId, Description = TrendAlarmDescription.Alm_Vel_Impulse });
                    }
                }
                else if (alarmSetting.KThreshold4 < k)
                {
                    Config.Logger.Info($"impulse alm: signalTypeId:{signalTypeId},{alarmTypeId},k4: {k},{first},{last},{alarmSetting.KThreshold4}");
                    list.Add(new AlarmEvent { AlarmTypeId = alarmTypeId, Description = TrendAlarmDescription.Alm_Vel_Impulse });
                }
            }
            else if (signalTypeId == SignalTypeIdEnum.Acc)
            {
                if (first <= alarmSetting.Threshold1)
                {
                    if (alarmSetting.KThreshold1 < k)
                    {
                        Config.Logger.Info($"impulse alm: signalTypeId:{signalTypeId},{alarmTypeId},k1: {k},{first},{last},{alarmSetting.KThreshold1}");
                        list.Add(new AlarmEvent { AlarmTypeId = alarmTypeId, Description = TrendAlarmDescription.Alm_Acc_Impulse });
                    }
                }
                else if (first <= alarmSetting.Threshold2)
                {
                    if (alarmSetting.KThreshold2 < k)
                    {
                        Config.Logger.Info($"impulse alm: signalTypeId:{signalTypeId},{alarmTypeId},k2: {k},{first},{last},{alarmSetting.KThreshold2}");
                        list.Add(new AlarmEvent { AlarmTypeId = alarmTypeId, Description = TrendAlarmDescription.Alm_Acc_Impulse });
                    }
                }
                else if (alarmSetting.KThreshold3 < k)
                {
                    Config.Logger.Info($"impulse alm: signalTypeId:{signalTypeId},{alarmTypeId},k3: {k},{first},{last},{alarmSetting.KThreshold3}");
                    list.Add(new AlarmEvent { AlarmTypeId = alarmTypeId, Description = TrendAlarmDescription.Alm_Acc_Impulse });
                }
            }
            else if (signalTypeId == SignalTypeIdEnum.Temperature) // 处理温度
            {
                if (first <= alarmSetting.Threshold1)
                {
                    if (alarmSetting.KThreshold1 < k)
                    {
                        Config.Logger.Info($"impulse alm: signalTypeId:{signalTypeId},{alarmTypeId},k1: {k},{first},{last},{alarmSetting.KThreshold1}");
                        list.Add(new AlarmEvent { AlarmTypeId = alarmTypeId, Description = TrendAlarmDescription.Alm_Temperature_Impulse });
                    }
                }
                else if (alarmSetting.KThreshold2 < k)
                {
                    Config.Logger.Info($"impulse alm: signalTypeId:{signalTypeId},{alarmTypeId},k2: {k},{first},{last},{alarmSetting.KThreshold2}");
                    list.Add(new AlarmEvent { AlarmTypeId = alarmTypeId, Description = TrendAlarmDescription.Alm_Temperature_Impulse });
                }
            }
        }

        /// <summary>
        /// 计算报警
        /// </summary>
        /// <param name="signalTypeId"></param>
        /// <param name="alarmTypeId"></param>
        /// <param name="yData"></param>
        /// <param name="alarmSetting"></param>
        /// <param name="output"></param>
        private void CalcAlarm(SignalTypeIdEnum signalTypeId, AlarmTypeIdEnum alarmTypeId, double[] yData, double[] xData, TrendAlarmSetting alarmSetting, TrendDiagnosisOutputDto output)
        {
            //Config.Logger.Info($"signalTypeId:{signalTypeId},{alarmTypeId}");

            // 两小时数据不够4次，不做计算，其他的趋势计算也不做
            if (yData == null || xData == null || xData.Length != yData.Length || yData.Length < 4)
            {
                //Config.Logger.Info("yData.Length < 4");
                return;
            }
            var deltaX = xData[xData.Length - 1] - xData[0];
            double lowThreshold = 0;
            switch(alarmTypeId)
            {
                case AlarmTypeIdEnum.Alm_Trend_30Day:
                    lowThreshold = 20;
                    break;

                case AlarmTypeIdEnum.Alm_Trend_10Day:
                    lowThreshold = 6;
                    break;

                case AlarmTypeIdEnum.Alm_Trend_24Hour:
                    lowThreshold = 16;
                    break;

                case AlarmTypeIdEnum.Alm_Trend_2Hour:
                    lowThreshold = 2 * 2f / 3;
                    break;
            }

            if(deltaX < lowThreshold)
            {
                //Config.Logger.Info($"deltaX < lowThreshold:{deltaX} < {lowThreshold}");
                return;
            }

            var lineFit = LineFitUtils.CreateByXYData(xData, yData); // 生成直线拟合
            //Config.Logger.Info($"lineFit:{lineFit.LineCoef.Item1},{lineFit.LineCoef.Item2}");
            var k = lineFit.K;
            //Config.Logger.Info($"k:{k}");

            var list = output.AlarmEvents;
            if (alarmSetting.KThreshold1 <= k)
            {
                Config.Logger.Info($"trend alm: signalTypeId:{signalTypeId},{alarmTypeId}, alarmSetting.KThreshold1 <= k: {alarmSetting.KThreshold1} <= {k}, deltaX:{deltaX}");
                //Config.Logger.Info($"length: {yData.Length}");
                //Config.Logger.Info("yDatas:" + string.Join( ",", yData));
                //Config.Logger.Info("xDatas:" + string.Join( ",", xData));
                list.Add(new AlarmEvent { AlarmTypeId = alarmTypeId });
            }
        }
    }
}