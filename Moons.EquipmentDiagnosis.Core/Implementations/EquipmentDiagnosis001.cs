using AnalysisAlgorithm;
using AnalysisAlgorithm.FFT;
using AnalysisData.Constants;
using MathNet.Numerics;
using Moons.EquipmentDiagnosis.Core.Abstractions;
using Moons.EquipmentDiagnosis.Core.Dto;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Moons.EquipmentDiagnosis.Core.Implementations
{
    /// <summary>
    /// 设备诊断实现001
    /// </summary>
    public class EquipmentDiagnosis001 : IEquipmentDiagnosis
    {
        public EquipmentDiagnosisOutputDto DoDiagnosis(EquipmentDiagnosisInputDto equipmentDiagnosisInput)
        {
            return new ED001().DoDiagnosis(equipmentDiagnosisInput);
        }

        public class ED001 : EDBase
        {
            public EquipmentDiagnosisOutputDto DoDiagnosis(EquipmentDiagnosisInputDto equipmentDiagnosisInput)
            {
                Init(equipmentDiagnosisInput);
                return MotorDiagnosis(equipmentDiagnosisInput);
            }


            private EquipmentDiagnosisOutputDto MotorDiagnosis(EquipmentDiagnosisInputDto equipmentDiagnosisInput)
            {
                CalcAccPoints();

                var velPoints = _pointDatas.VelPoints_HasTimewave;
                if(Moons.EquipmentDiagnosis.Core.EquipmentType.Motor.Belt_2Motor_001 == EquipmentType)
                {
                    {
                        var h = velPoints.GetByPositionNoAndDirectionId(PositionNumber.No_1, PositionNumber.No_2, PntDirectionID.Horizontal);
                        var v = velPoints.GetByPositionNoAndDirectionId(PositionNumber.No_1, PositionNumber.No_2, PntDirectionID.Vertical);
                        Calc_MDEH_MNDEV(h, v);
                    }
                    {
                        int maxPositionNo = velPoints.GetMaxPositionNo();
                        var h = velPoints.GetByPositionNoAndDirectionId(maxPositionNo - 1, maxPositionNo, PntDirectionID.Horizontal);
                        var v = velPoints.GetByPositionNoAndDirectionId(maxPositionNo - 1, maxPositionNo, PntDirectionID.Vertical);
                        Calc_MDEH_MNDEV(h, v);
                    }
                }

                CalcVelPoints(velPoints);

                return GetEquipmentDiagnosisOutputDto();
            }

            /// <summary>
            /// 计算速度测点的诊断结果
            /// </summary>
            private void CalcVelPoints(PointDataCollection velPoints)
            {
                //var velPoints = _pointDatas.VelPoints;
                double maxVelAlm = double.MinValue;
                PointData velAlmPoint = null;
                foreach (var p in velPoints)
                {
                    if (p.IsAlm && maxVelAlm < p.HistorySummaryData.MeasureValue)
                    {
                        velAlmPoint = p;
                        maxVelAlm = p.HistorySummaryData.MeasureValue;
                    }
                }
                if (velAlmPoint == null) return;

                var summary = velAlmPoint.HistorySummaryData;
                var timewave = velAlmPoint.TimewaveData;

                double limit0_2 = 0.2 * timewave.HighestPeak;
                double[] values = new double[] { timewave.X0_5, timewave.X1_5, timewave.X2_5, timewave.X3_5, timewave.X4_5, timewave.X5_5 };

                var count1 = values.Count(v => limit0_2 < v);
                if( 3 <= count1)
                {
                    Config.Logger.Info($"如果0.5X、1.5X、2.5X、3.5X、4.5X、5.5X至少有3个大于频谱图中最高峰值20%。结论：轴承或轴上零部件存在松动摩擦，检查轴承等部位动静安装配合状态。（第一优先判断）");
                    AddPossibleFault(velAlmPoint, EquipmentFaultType.Generic.Loose001);
                    //return;
                }

                double limit0_1 = 0.1 * timewave.HighestPeak;
                var count2 = 0;
                var xfft = timewave.XFFT;
                // 从6x开始
                for (int i = 5; i < xfft.Length; i++ )
                    if (limit0_1 < xfft[i]) count2++;
                if (10 <= count2 )
                {
                    Config.Logger.Info($"如果大于6X的所有整数倍频分量中至少有10个频率的 幅值大于频谱中最高频率幅值的10%。结论：摩擦故障。（第二优先判断） ");
                    AddPossibleFault(velAlmPoint, EquipmentFaultType.Generic.Rub001);
                    //return;
                }

                double limit0_8overall = 0.8 * timewave.Overall;
                double limit0_1overall = 0.1 * timewave.Overall;
                double squareSum = 0;
                int gtOverall0_1Count = 0;
                for( int i = 0; i < xfft.Length && i < 6; i++)
                {
                    var v = xfft[i];
                    squareSum += v * v;
                    if (limit0_1overall < v) gtOverall0_1Count++;
                }
                if (0 < squareSum) squareSum = Math.Sqrt(squareSum);
                if (4 <= gtOverall0_1Count && limit0_8overall < squareSum )
                {
                    Config.Logger.Info($"如果1X、2X、3X、4X、5X之和大于80%总值，且至少有 4个分量幅值都大于10%总值。结论：轴承存在摩擦或轴瓦间隙不良。（第三优先判断） ");
                    AddPossibleFault(velAlmPoint, EquipmentFaultType.Generic.Rub002);
                    //return;
                }

                double limit0_5overall = 0.5 * timewave.Overall;
                if (limit0_5overall < timewave.X2)
                {
                    Config.Logger.Info($"如果最大报警值测点的2X分量大于50%总值。结论： 轴承座或联轴器不同心（第四优先判断） ");
                    AddPossibleFault(velAlmPoint, EquipmentFaultType.Generic.Misalign001);
                    //return;
                }

                if (limit0_8overall < timewave.Hz100)
                {
                    Config.Logger.Info($"如果100Hz分量大于总值80%。结论：电气故障。（第 五优先判断） ");
                    AddPossibleFault(velAlmPoint, EquipmentFaultType.Generic.Misalign001);
                    //return;
                }

                var histories = Context.GetHistorySummaryData(velAlmPoint, new HistoryQueryConditionData {
                    HistoryDataBegin = _equipmentDiagnosisInput.HistoryDataBegin,
                    HistoryDataEnd = _equipmentDiagnosisInput.HistoryDataEnd,
                    MeasurementValueLowLimit = velAlmPoint.MeasurementValueLowLimit ?? 0.3,
                    Count = 50,
                });
                if (histories.Count < 50) return;
                double[] fiftyMeasurements = histories.Select(h => h.MeasureValue).ToArray();
                {
                    Array.Sort(fiftyMeasurements);
                    var minMean = StatisticsUtils.Mean(fiftyMeasurements.AsSpan(0, 10));
                    var maxMean = StatisticsUtils.Mean(fiftyMeasurements.AsSpan(fiftyMeasurements.Length - 10, 10));
                    if( 1.5f * minMean < maxMean)
                    {
                        Config.Logger.Info($"如果此前趋势中50个振动速度值中，最大的10个值的 平均值大于1.5倍最小的10个值的平均值。结论：台板变形、不平等引起定子偏心。（第六优先判断）");
                        AddPossibleFault(velAlmPoint, EquipmentFaultType.Generic.Stator001);
                    }
                }

                if(!HasPossibleFaults)
                {
                    Config.Logger.Info($"其他未知故障，需便携式振动分析仪全面测量数据");
                    AddPossibleFault(velAlmPoint, EquipmentFaultType.Generic.Unknown001);
                }
            }

            private bool IsNoTimewaveData( PointData point)
            {
                return point == null || !point.HasTimewaveData();
            }

            /// <summary>
            /// 有可能驱动端(连轴端)和非驱动端(自由端)水平和垂直方向对换了，但第一个测点总是水平，第二个测点总是垂直。
            /// 如果MDEH大于2倍MNDEV，且MDEH主频大于总值的60%。结论：转子存在不平衡。须通过测量台板、水泥基础振动排除支撑水平刚度不足故障。（如果主频小于等于总值60% ，则结论轴承配合间隙不良） 
            /// B、如果MDEH小于2倍MNDEV且大于1.34倍MNDEV，且主频大于总值的60%。结论：转子存在不平衡且支撑刚度不足。（如果主频小于等于总值60% ，则轴承配合间隙不良）
            /// C、如果MNDEV大于0.75倍MDEH，且主频大于50%总值。结论：基础垂直刚度不足。检查台板、水泥基础以及垫铁等紧固松动或台板不平。（如果主频小于等于50% ，则轴承配合间隙不良）
            /// </summary>
            private void Calc_MDEH_MNDEV(  PointData mdeh, PointData mndev )
            {
                if ( IsNoTimewaveData(mdeh) ||  IsNoTimewaveData(mndev) ) return;

                var measureValue_h = mdeh.HistorySummaryData.MeasureValue;
                var measureValue_v = mndev.HistorySummaryData.MeasureValue;

                var timewave_h = mdeh.TimewaveData;
                double overall_h = timewave_h.Overall;
                double x1_h = timewave_h.X1;
                
                var timewave_v = mndev.TimewaveData;
                double overall_v = timewave_v.Overall;
                double x1_v = timewave_v.X1;
                if( 2f * measureValue_v < measureValue_h )
                {
                    if( 0.6 * overall_h < x1_h )
                    {
                        Config.Logger.Info($"如果MDEH大于2倍MNDEV，且MDEH主频大于总值的60%。结论：转子存在不平衡。须通过测量台板、水泥基础振动排除支撑水平刚度不足故障。");
                        AddPossibleFault(mdeh, EquipmentFaultType.Generic.Imbalance001);
                    }
                    else
                    {
                        Config.Logger.Info($"如果MDEH大于2倍MNDEV，且主频小于等于总值60% ，结论轴承配合间隙不良");
                        AddPossibleFault(mdeh, EquipmentFaultType.Generic.Bearing002);
                    }
                }
                else if( 1.34f * measureValue_v < measureValue_h )
                {
                    if( 0.6 * overall_h < x1_h )
                    {
                        Config.Logger.Info($"如果MDEH小于2倍MNDEV且大于1.34倍MNDEV，且主频大于总值的60%。结论：转子存在不平衡且支撑刚度不足。");
                        AddPossibleFault(mdeh, EquipmentFaultType.Generic.Imbalance002);
                    }
                    else
                    {
                        Config.Logger.Info($"如果MDEH小于2倍MNDEV且大于1.34倍MNDEV，且主频小于等于总值60% ，结论轴承配合间隙不良");
                        AddPossibleFault(mdeh, EquipmentFaultType.Generic.Bearing002);
                    }
                }
                else if( 0.75f * measureValue_h < measureValue_v )
                {
                    if( 0.5 * overall_v < x1_v )
                    {
                        Config.Logger.Info($"如果MNDEV大于0.75倍MDEH，且主频大于50%总值。结论：基础垂直刚度不足。检查台板、水泥基础以及垫铁等紧固松动或台板不平。");
                        AddPossibleFault(mndev, EquipmentFaultType.Generic.Base001);
                    }
                    else
                    {
                        Config.Logger.Info($"如果MNDEV大于0.75倍MDEH，且主频小于等于50% ，则轴承配合间隙不良");
                        AddPossibleFault(mndev, EquipmentFaultType.Generic.Bearing002);
                    }
                }
            }

            /// <summary>
            /// 计算加速度测点的诊断结果
            /// </summary>
            private void CalcAccPoints()
            {
                var accPoints = _pointDatas.AccPoints;
                PointData accAlmPoint = accPoints.FirstOrDefault(p => p.IsAlm);
                if (accAlmPoint == null) return;

                Config.Logger.Info($"当加速度值或SKenergy值大于等于报警值（如果加速度没有设置报警值，按40m/s2）时，结论为：轴承故障，建议加强监测或检查轴承。");
                AddPossibleFault(accAlmPoint, EquipmentFaultType.Generic.Bearing001);
            }
        }
    }

    /// <summary>
    /// 诊断基类
    /// </summary>
    public class EDBase
    {
        protected EquipmentDiagnosisInputDto _equipmentDiagnosisInput;
        protected EquipmentData _equipmentData;
        protected readonly PointDataCollection _pointDatas = new PointDataCollection();
        protected void Init(EquipmentDiagnosisInputDto equipmentDiagnosisInput)
        {
            object equipmentId = equipmentDiagnosisInput.EquipmentId;
            _equipmentDiagnosisInput = equipmentDiagnosisInput;
            _equipmentData = Context.GetEquipmentData(equipmentId);
            _pointDatas.AddRange( Context.GetPointDatas(equipmentDiagnosisInput) );
            foreach (var p in _pointDatas.AccPoints)
                InitPointData(p);
            foreach (var p in _pointDatas.VelPoints)
                InitPointData(p);
        }
        private void InitPointData(PointData p)
        {
            var summary = p.HistorySummaryData;
            if (summary == null)
            {
                var histories = Context.GetHistorySummaryData(p, new HistoryQueryConditionData
                {
                    HistoryDataBegin = _equipmentDiagnosisInput.HistoryDataBegin,
                    HistoryDataEnd = _equipmentDiagnosisInput.HistoryDataEnd,
                    //MeasurementValueLowLimit = p.GetMeasurementValueLowLimit(),
                    Count = 1,
                    MustHasTimewave = true,
                    MaxFirst = true,
                });

                if (0 == histories.Count) return;
                summary = p.HistorySummaryData = histories[0];
            }
            var timewave = p.TimewaveData;
            if (timewave == null)
            {
                timewave = p.TimewaveData = Context.GetTimewaveData(summary);
            }
            if (summary == null || timewave == null) return;
            if (summary.RotSpeed_NR <= 0) summary.RotSpeed_NR = p.DefaultRotSpeed;
            var rev = summary.RotSpeed_NR;
            if (rev <= 0) return;

            SpectrumUtils spectrumUtils = new SpectrumUtils();
            spectrumUtils.InitByTimewave(summary.SampleFreq_NR, summary.F0, timewave.Timewave, true);

            timewave.Init(spectrumUtils);
        }
        protected IEquipmentDiagnosisContext Context => Config.Context;

        protected string EquipmentType => _equipmentData.EquipmentType;

        private List<PossibleFault> _possibleFaults = new List<PossibleFault>();
        protected bool HasPossibleFaults => 0 < _possibleFaults.Count;
        protected void AddPossibleFault( PointData p, string code )
        {
            PossibleFault possibleFault = new PossibleFault { FaultCode = code, Point_ID = p.Point_ID };
            _possibleFaults.Add(possibleFault);
            Config.Logger.Info($"Add possible fault: {p.Point_ID},{p.PointPath},{code}");

        }
        protected EquipmentDiagnosisOutputDto GetEquipmentDiagnosisOutputDto()
        {
            return new EquipmentDiagnosisOutputDto 
            {
                EquipmentId = _equipmentDiagnosisInput.EquipmentId,
                PossibleFaults = _possibleFaults.ToArray(),
            };
        }
    }
}
