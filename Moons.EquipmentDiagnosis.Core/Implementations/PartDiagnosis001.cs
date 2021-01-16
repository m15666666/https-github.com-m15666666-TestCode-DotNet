using AnalysisAlgorithm;
using Moons.Common20;
using Moons.EquipmentDiagnosis.Core.Abstractions;
using Moons.EquipmentDiagnosis.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using AnalysisData.FeatureFreq;

namespace Moons.EquipmentDiagnosis.Core.Implementations
{
    /// <summary>
    /// 部件诊断模型基类001
    /// </summary>
    public abstract class PartDiagnosisBase001
    {
        /// <summary>
        /// 要诊断的部件
        /// </summary>
        public PartData Part { get; set; }

        protected PartParameter PartParameter => Part.PartParameter;

        /// <summary>
        /// 测点
        /// </summary>
        protected PointDataCollection Points => Part.Points;

        /// <summary>
        /// 连轴端测点
        /// </summary>
        protected PointDataCollection DEPoints => Part.DEPoints;

        /// <summary>
        /// 非连轴端测点
        /// </summary>
        protected PointDataCollection NDEPoints => Part.NDEPoints;

        public abstract void DoDiagnosis();

        protected IEquipmentDiagnosisContext Context => Config.Context;

        //protected string EquipmentType => _equipmentData.EquipmentType;

        private List<PossibleFault> _possibleFaults = new List<PossibleFault>();
        protected bool HasPossibleFaults => 0 < _possibleFaults.Count;

        protected void AddPossibleFault(PointData p, string code)
        {
            PossibleFault possibleFault = new PossibleFault { FaultCode = code, Point_ID = p.Point_ID };
            _possibleFaults.Add(possibleFault);
            Config.Logger.Info($"Add possible fault: {p.Point_ID},{p.PointPath},{code}");
        }

        #region 具体故障

        /// <summary>
        /// 计算松动
        /// </summary>
        /// <returns>true:存在松动故障</returns>
        protected virtual bool CalcLoose()
        {
            /*
             2）LOOSE2-电机（以下按（1）、（2）顺序依次判断）
（1）如果MDE-H-VEL、MDE-V-VEL、MDE-A-VEL的最大值的测点频谱中0.5X、1.5X、2.5X、3.5X、4.5X、5.5X至少有3个大于1X、2X、3X、4X、5X、6X的最高峰值的20%。结论：联轴端轴承或轴上其它零部件存在松动或间隙不良。
如果MNDE-H-VEL、MNDE-V-VEL、MNDE-A-VEL的最大值的测点频谱中0.5X、1.5X、2.5X、3.5X、4.5X、5.5X至少有3个大于1X、2X、3X、4X、5X、6X的最高峰值的20%。结论：非联轴端轴承或轴上其它零部件存在松动或间隙不良。
             */
            bool found = false;
            if (Calc(DEPoints.VelPoints,EquipmentFaultType.Generic.Loose002)) found = true;
            if (Calc(NDEPoints.VelPoints,EquipmentFaultType.Generic.Loose003)) found = true;
            return found;

            bool Calc(PointDataCollection velPoints, string code)
            {
                if (0 == velPoints.Count) return false;
                var point = velPoints.MaxItem(p => p.HistorySummaryData.MeasureValue);
                var summary = point.HistorySummaryData;
                var timewave = point.TimewaveData;

                double limit = 0.2 * ArrayUtils.Max(timewave.XFFT, 0, 6);
                var count = ArrayUtils.Count(timewave.XHalfFFT, 0, 6, v => limit < v);
                if (3 <= count)
                {
                    Config.Logger.Info(code);
                    AddPossibleFault(point, code);
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// 计算摩擦
        /// </summary>
        /// <returns>true:存在摩擦故障</returns>
        protected virtual bool CalcRub()
        {
            /*
              （1） 如果MDE-H-VEL、MDE-V-VEL、MDE-A-VEL的最大值的频谱中大于6X的所有整数倍频分量中至少有10个频率的幅值大于频谱中1X、2X、3X、4X、5X、6X的最高峰值的10%。结论：联轴端轴承或轴上零部件存在动静摩擦故障，检查联轴端轴承等部位动静安装配合状态。
（2）如果MNDE-H-VEL、MNDE-V-VEL、MNDE-A-VEL的最大值的频谱中大于6X的所有整数倍频分量中至少有10个频率的幅值大于频谱中1X、2X、3X、4X、5X、6X的最高峰值的10%。结论：非联轴端轴承或轴上零部件存在动静摩擦，检查非联轴端轴承等部位动静安装配合状态。
             */
            bool found = false;
            if (Calc(DEPoints.VelPoints,EquipmentFaultType.Generic.Rub003)) found = true;
            if (Calc(NDEPoints.VelPoints,EquipmentFaultType.Generic.Rub004)) found = true;
            return found;

            bool Calc(PointDataCollection velPoints, string code)
            {
                if (0 == velPoints.Count) return false;
                var point = velPoints.MaxItem(p => p.HistorySummaryData.MeasureValue);
                var summary = point.HistorySummaryData;
                var timewave = point.TimewaveData;

                double limit = 0.1 * ArrayUtils.Max(timewave.XFFT, 0, 6);
                var xFFT = timewave.XFFT;
                int startIndex = 6;
                var count = ArrayUtils.Count(xFFT, startIndex, xFFT.Length - startIndex, v => limit < v);
                if (10 <= count)
                {
                    Config.Logger.Info(code);
                    AddPossibleFault(point, code);
                    return true;
                }
                return false;
            }
        }

        #endregion

        #region 计算轴承

        /// <summary>
        /// 计算BEAR-Fc 保持架故障--滚动轴承
        /// </summary>
        /// <returns>true:存在故障</returns>
        protected virtual bool CalcBearingFTF()
        {
            /*
             1）BEAR-Fc 保持架故障--滚动轴承 
对每个测点振动速度谱图逐个进行诊断，当 1 倍保持架特征频率（即保持架特征频率系数*
主频）、2 倍保持架特征频率、3 倍保持架特征频率中至少有两个的幅值大于谱图中最高谱
线的 20%时，该点轴承保持架存在碰磨故障。 
             */
            return CalcBearingFeatureFreq(Points.VelPoints, PartParameter?.BearingFeatureFreqs,featureFreq => featureFreq.FTF,EquipmentFaultType.Generic.Bearing003);
        }
        /// <summary>
        /// 计算BEAR-Fb 滚珠故障-滚动轴承 
        /// </summary>
        /// <returns>true:存在故障</returns>
        protected virtual bool CalcBearingBSF()
        {
            /*
            对每个测点振动速度谱图逐个进行诊断，当 1 倍滚动体特征频率（即滚动体特征频率系数*
主频）、2 倍滚动体特征频率、3 倍滚动体特征频率中至少有两个的幅值大于谱图中最高谱
线的 20%时，该点轴承滚动体存在损伤故障。 
             */
            return CalcBearingFeatureFreq(Points.VelPoints, PartParameter?.BearingFeatureFreqs,featureFreq => featureFreq.BSF,EquipmentFaultType.Generic.Bearing004);
        }
        /// <summary>
        /// 计算BEAR-Fo 外圈故障-滚动轴承
        /// </summary>
        /// <returns>true:存在故障</returns>
        protected virtual bool CalcBearingBPFO()
        {
            /*
             对每个测点振动速度谱图逐个进行诊断，当 1 倍外圈特征频率（即外圈特征频率系数*主频）、
2 倍外圈特征频率、3 倍外圈特征频率中至少有两个的幅值大于谱图中最高谱线的 20%时，
该点轴承外圈存在损伤故障。 
             */
            return CalcBearingFeatureFreq(Points.VelPoints, PartParameter?.BearingFeatureFreqs,featureFreq => featureFreq.BPFO,EquipmentFaultType.Generic.Bearing005);
        }
        /// <summary>
        /// 计算BEAR-Fi 内圈故障-滚动轴承 
        /// </summary>
        /// <returns>true:存在故障</returns>
        protected virtual bool CalcBearingBPFI()
        {
            /*
            对每个测点振动速度谱图逐个进行诊断，当 1 倍内圈特征频率（即内圈特征频率系数*主频）、
2 倍外圈特征频率、3 倍内圈特征频率中至少有两个的幅值大于谱图中最高谱线的 20%时，
该点轴承内圈存在损伤故障。 
             */
            return CalcBearingFeatureFreq(Points.VelPoints, PartParameter?.BearingFeatureFreqs,featureFreq => featureFreq.BPFI,EquipmentFaultType.Generic.Bearing006);
        }

        /// <summary>
        /// 计算滚动轴承特征频率故障
        /// </summary>
        /// <param name="velPoints">速度测点集合</param>
        /// <param name="featureFreqs">轴承特征频率集合</param>
        /// <param name="func"></param>
        /// <param name="code">故障代码</param>
        /// <returns>true：有故障</returns>
        private bool CalcBearingFeatureFreq(PointDataCollection velPoints, 
            ICollection<BearingFeatureFreq> featureFreqs, Func<BearingFeatureFreq,double> func,string code)
        {
            if (CollectionUtils.IsNullOrEmptyG(velPoints) || CollectionUtils.IsNullOrEmptyG(featureFreqs)) return false;
            bool ret = false;
            double[] featureValues = new double[3];
            foreach( var point in velPoints )
            {
                var summary = point.HistorySummaryData;
                var f0 = summary.F0;
                var timewave = point.TimewaveData;
                var spectrumUtils = timewave.SpectrumUtils;
                double limit = 0.2 * timewave.HighestPeak;
                foreach( var featureFreq in featureFreqs)
                {
                    var freq = func(featureFreq) * featureFreq.ShaftMultiplier * f0;
                    for( int i = 1; i <= featureValues.Length; i++)
                        featureValues[i-1] = spectrumUtils.GetFFTAmp(i * freq);

                    var count = featureValues.Count(v => limit < v);
                    if (2 <= count)
                    {
                        Config.Logger.Info(code);
                        AddPossibleFault(point, code);
                        ret = true;
                    }
                }
            }
            return ret;
        }
        #endregion
    }

    /// <summary>
    /// 电机诊断模型
    /// </summary>
    public class MotorDiagnosis001 : PartDiagnosisBase001
    {
        public override void DoDiagnosis()
        {
            CalcLoose();
            CalcRub();
            CalcELECTRC1();//todo
            CalcELECTRC2();
            CalcELECTRC3();
            CalcBearingFTF();
            if (CalcIsAlarm()) // todo
            {
                CalcBearingBSF();
                CalcBearingBPFO();
                CalcBearingBPFI();
            }
        }

        #region 具体故障

        /// <summary>
        /// 计算转子断条 ELECTRC1 断条--电机 故障
        /// </summary>
        /// <returns></returns>
        private bool CalcELECTRC1()
        {
            //todo
            /*
             1.1）ELECTRC1 断条--电机 
  电机所有测点中振动速度值最大的频谱图上存在主频 1X 幅值大于 50%总值，且 1X+极
数*滑差或 1X-极数*滑差的幅值大于 1X 幅值的 20%。诊断为断条故障。 
             
             */
            return false;
        }

        /// <summary>
        /// ELECTRC2 转子偏心、气隙不均、定子松动故障--电机 
        /// </summary>
        /// <returns></returns>
        private bool CalcELECTRC2()
        {
            /*
             1.2）ELECTRC2 转子偏心、气隙不均、定子松动故障--电机 
  电机所有测点中振动速度值最大的频谱图上存在 100Hz，且其幅值大于总值的 50%。诊
断为电机转系偏心或气隙不均或定子松动。 
             */
            bool found = false;
            if (Calc(Points.VelPoints,EquipmentFaultType.Generic.Electric003)) found = true;
            return found;

            bool Calc(PointDataCollection velPoints, string code)
            {
                if (0 == velPoints.Count) return false;
                var point = velPoints.MaxItem(p => p.HistorySummaryData.MeasureValue);
                var summary = point.HistorySummaryData;
                var timewave = point.TimewaveData;

                double limit = 0.5 * timewave.Overall;
                if (limit < timewave.Hz100)
                {
                    Config.Logger.Info(code);
                    AddPossibleFault(point, code);
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// ELECTRC3 定子短路--电机 
        /// </summary>
        /// <returns></returns>
        private bool CalcELECTRC3()
        {
            /*
            1.3）ELECTRC3 定子短路--电机 
  电机所有测点中振动速度值最大的频谱图上 100Hz、200Hz、300Hz、400Hz、600Hz 频
率中至少有 3 个幅值大于频谱中最高幅值的 50%。诊断为定子短路故障。 
             */
            bool found = false;
            if (Calc(Points.VelPoints,EquipmentFaultType.Generic.Electric004)) found = true;
            return found;

            bool Calc(PointDataCollection velPoints, string code)
            {
                if (0 == velPoints.Count) return false;
                var point = velPoints.MaxItem(p => p.HistorySummaryData.MeasureValue);
                var summary = point.HistorySummaryData;
                var timewave = point.TimewaveData;

                double limit = 0.5 * timewave.HighestPeak;
                var xHz100 = timewave.XHz100;
                int startIndex = 0;
                var count = ArrayUtils.Count(xHz100, startIndex, 5, v => limit < v);
                if (3 <= count)
                {
                    Config.Logger.Info(code);
                    AddPossibleFault(point, code);
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// 是否报警 
        /// </summary>
        /// <returns></returns>
        private bool CalcIsAlarm()
        {
            /*
            如果泵所有振动测点振动加速度值有一
个达到报警值，或者所有测点振动加速度有效值虽然都没有达到报警值，但其中任一个测点在 4 天内的振动加速度峰
值趋势直线拟合斜率大于 0.417。 
             */
            //todo
            if (CollectionUtils.IsNotEmptyG(PartParameter?.BearingFeatureFreqs)) return false;
            bool found = false;
            if (Calc(Points.VelPoints,EquipmentFaultType.Generic.Electric004)) found = true;
            return found;

            bool Calc(PointDataCollection velPoints, string code)
            {
                if (0 == velPoints.Count) return false;
                var point = velPoints.MaxItem(p => p.HistorySummaryData.MeasureValue);
                var summary = point.HistorySummaryData;
                var timewave = point.TimewaveData;

                double limit = 0.5 * timewave.HighestPeak;
                var xHz100 = timewave.XHz100;
                int startIndex = 0;
                var count = ArrayUtils.Count(xHz100, startIndex, 5, v => limit < v);
                if (3 <= count)
                {
                    Config.Logger.Info(code);
                    AddPossibleFault(point, code);
                    return true;
                }
                return false;
            }
        }

        #endregion

    }
}