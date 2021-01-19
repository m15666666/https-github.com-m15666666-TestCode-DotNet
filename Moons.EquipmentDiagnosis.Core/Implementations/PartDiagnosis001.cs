using Moons.Common20;
using Moons.EquipmentDiagnosis.Core.Abstractions;
using Moons.EquipmentDiagnosis.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using AnalysisData.FeatureFreq;
using AnalysisData.Constants;
using AnalysisAlgorithm;

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

        #region 查询历史数据

        protected DateTime Now => DateTime.Now;

        /// <summary>
        /// 获得几天之内的summary数据
        /// </summary>
        /// <param name="point">PointData</param>
        /// <param name="day">天数</param>
        /// <returns></returns>
        protected Int32MinuteSummary[] GetInt32MinuteSummaryByDays(PointData point, int days )
        {
            var now = Now;
            HistoryQueryConditionData condition = new HistoryQueryConditionData
            {
                HistoryDataBegin = now.AddDays(-days),
                HistoryDataEnd = now,
            };
            return Context.GetInt32MinuteSummary(point, condition);
        }

        /// <summary>
        /// 计算线性相关系数
        /// </summary>
        /// <param name="x">按照采样时间升序排列的数据</param>
        /// <param name="y">按照采样时间升序排列的数据</param>
        /// <returns></returns>
        protected double CalcPearsonCorrelationCoefficient(Int32MinuteSummary[] x, Int32MinuteSummary[] y)
        {
            if (CollectionUtils.IsNullOrEmptyG(x) || CollectionUtils.IsNullOrEmptyG(y)) return 0;
            int indexX = 0, indexY = 0;
            List<double> vXs = new List<double>();
            List<double> vYs = new List<double>();
            int lenX = x.Length;
            int lenY = y.Length;
            while( indexX < lenX && indexY < lenY)
            {
                var vX = x[indexX];
                var vY = y[indexY];
                if(vX.SampleMinute == vY.SampleMinute)
                {
                    vXs.Add(vX.MeasureValue);
                    vYs.Add(vY.MeasureValue);
                    indexX++;
                    indexY++;
                    continue;
                }
                if (vX.SampleMinute < vY.SampleMinute) indexX++;
                else indexY++;
            }
            return vXs.Count == 0 ? 0 : StatisticsUtils.PearsonCorrelationCoefficient(vXs.ToArray(), vYs.ToArray());
        }


        #endregion

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
        /// 计算 动静摩擦故障 
        /// </summary>
        /// <returns>true:存在故障</returns>
        protected virtual bool CalcRub()
        {
            /*
            1）RUB1--泵、风机（以下按（1）、（2）顺序依次判断） 
（1）如果 FDE-H-VEL、FDE-V-VEL、FDE-A-VEL 的最大值的频谱中大于 6X 的所有整数倍
频分量中至少有 10 个频率的幅值大于频谱中 1X、2X、3X、4X、5X、6X 的最高峰值的 10%。
结论：联轴端轴承或轴上零部件存在动静摩擦故障，检查联轴端轴承等部位动静安装配合状
态。 
（2）如果 FNDE-H-VEL、FNDE-V-VEL、FNDE-A-VEL 的最大值的频谱中大于 6X 的所有整
数倍频分量中至少有 10 个频率的幅值大于频谱中 1X、2X、3X、4X、5X、6X 的最高峰值的
10%。结论：非联轴端轴承或轴上零部件存在松动动静摩擦，检查非联轴端轴承等部位动静
安装配合状态。 
      2）RUB2--电机（以下按（1）、（2）顺序依次判断） 
  （1）  如果 MDE-H-VEL、MDE-V-VEL、MDE-A-VEL 的最大值的频谱中大于 6X 的所有整
数倍频分量中至少有 10 个频率的幅值大于频谱中 1X、2X、3X、4X、5X、6X 的最高峰值的
10%。结论：联轴端轴承或轴上零部件存在动静摩擦故障，检查联轴端轴承等部位动静安装
配合状态。 
（2）如果 MNDE-H-VEL、MNDE-V-VEL、MNDE-A-VEL 的最大值的频谱中大于 6X 的所有
整数倍频分量中至少有 10 个频率的幅值大于频谱中 1X、2X、3X、4X、5X、6X 的最高峰值
的 10%。结论：非联轴端轴承或轴上零部件存在动静摩擦，检查非联轴端轴承等部位动静安
装配合状态。 
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

        /// <summary>
        /// 计算轴承配合间隙不良CLEARANCE
        /// </summary>
        /// <returns>true:存在故障</returns>
        protected virtual bool CalcCLEARANCE()
        {
            /*
            1）CLEARANCE1--泵、风机（测点 X 与（1）、（2）对号入座） 
（1）如果 FDE-H-VEL、FDE-V-VEL、FDE-A-VEL 的最大值的频谱中 1X、2X、3X、4X、5X 之和
大于 80%总值，且至少有 4 个分量幅值都大于 10%总值。结论：联轴端轴承配合间隙不良，
检查联轴端轴承等部位动静安装配合状态。 
（2）如果 FNDE-H-VEL、FNDE-V-VEL、FNDE-A-VEL 的最大值的频谱中 1X、2X、3X、4X、5X
之和大于 80%总值，且至少有 4 个分量幅值都大于 10%总值。结论：非联轴端轴承配合间隙
不良，检查非联轴端轴承等部位动静安装配合状态。 
              2）CLEARANCE2--电机（测点 X 与（1）、（2）对号入座） 
（1）如果 MDE-H-VEL、MDE-V-VEL、MDE-A-VEL 的最大值的频谱中 1X、2X、3X、4X、5X 之
和大于 80%总值，且至少有 4 个分量幅值都大于 10%总值。结论：联轴端轴承配合间隙不良，
检查联轴端轴承等部位动静安装配合状态。 
（2）如果 MNDE-H-VEL、MNDE-V-VEL、MNDE-A-VEL 的最大值的频谱中 1X、2X、3X、4X、5X 之和大于 80%总值，且至少有 4 个分量幅值都大于 10%总值。结论：非联轴端轴承配合
间隙不良，检查非联轴端轴承等部位动静安装配合状态。 
             */
            bool found = false;
            if (Calc(DEPoints.VelPoints,EquipmentFaultType.Generic.Loose004)) found = true;
            if (Calc(NDEPoints.VelPoints,EquipmentFaultType.Generic.Loose005)) found = true;
            return found;

            bool Calc(PointDataCollection velPoints, string code)
            {
                if (0 == velPoints.Count) return false;
                var point = velPoints.MaxItem(p => p.HistorySummaryData.MeasureValue);
                var summary = point.HistorySummaryData;
                var timewave = point.TimewaveData;

                double limit_0p8_Overall = 0.8 * timewave.Overall;
                double limit_0p1_Overall = 0.1 * timewave.Overall;
                int xFFTStartIndex = 0;
                int xFFTCount = 5;
                var xFFT = timewave.XFFT;
                var partialOverall = timewave.SpectrumUtils.GetOverallByXFFT(xFFTStartIndex, xFFTCount);
                var count = ArrayUtils.Count(xFFT, xFFTStartIndex, xFFTCount, v => limit_0p1_Overall < v);
                if (4 <= count && limit_0p8_Overall < partialOverall)
                {
                    Config.Logger.Info(code);
                    AddPossibleFault(point, code);
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// 计算 FDLOOSE 基础松动、软脚等故障 
        /// </summary>
        /// <returns>true:存在故障</returns>
        protected virtual bool CalcFDLOOSE()
        {
            /*
            1）FDLOOSE1--泵、风机--刚性支撑 
  如果 FDEV、FNDEV 至少一个有效，则这两个的最大值如果大于水平方向振动速度值（优
先同轴承）的 0.80 倍，且这个最大值的 1-6 倍频之和大于 80%总值。结论：基础松动、
软脚等基础垂直刚度不足故障。检查台板、水泥基础以及垫铁等紧固松动或台板不平。 
2）FDLOOSE2--电机--卧式、刚性支撑 
  如果 MDEV、MNDEV 至少一个有效，则这两个的最大值如果大于水平方向振动速度值
（优先同轴承）的 0.80 倍，且这个最大值的 1-6 倍频之和大于 80%总值。结论：基础
松动、软脚等基础垂直刚度不足故障。检查台板、水泥基础以及垫铁等紧固松动或台板
不平。 
             */
            if (!PartParameter.IsStiffBase) return false;
            bool found = false;
            if (Calc(DEPoints.VelPoints,EquipmentFaultType.Generic.Loose006)) found = true;
            else if (Calc(NDEPoints.VelPoints,EquipmentFaultType.Generic.Loose006)) found = true;
            else if (Calc(Points.VelPoints,EquipmentFaultType.Generic.Loose006)) found = true;
            return found;

            bool Calc(PointDataCollection velPoints, string code)
            {
                if (0 == velPoints.Count) return false;
                var pVertical = velPoints.GetByDirectionId(PntDirectionID.Vertical).MaxItem(p => p.HistorySummaryData.MeasureValue);
                var pHorizontal = velPoints.GetByDirectionId(PntDirectionID.Horizontal).MaxItem(p => p.HistorySummaryData.MeasureValue);
                if (pHorizontal == null || pVertical == null) return false;
                if (!(0.8 * pHorizontal.HistorySummaryData.MeasureValue < pVertical.HistorySummaryData.MeasureValue)) return false;

                var summary = pVertical.HistorySummaryData;
                var timewave = pVertical.TimewaveData;

                double limit_0p8_Overall = 0.8 * timewave.Overall;
                int xFFTStartIndex = 0;
                int xFFTCount = 6;
                var partialOverall = timewave.SpectrumUtils.GetOverallByXFFT(xFFTStartIndex, xFFTCount);
                if (limit_0p8_Overall < partialOverall)
                {
                    Config.Logger.Info(code);
                    AddPossibleFault(pVertical, code);
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// 计算 STRESS 台板不平、管线应力等引起的壳体变形 
        /// </summary>
        /// <returns>true:存在故障</returns>
        protected virtual bool CalcSTRESS()
        {
            /*
            1）STRESS1--泵、风机 
  如果 X 测点其 4 天趋势符合以下描述：大于 4 天拟合直线上对应点的所有振动速度值的
平均值 Vmax 减去小于 4 天拟合直线上对应点的所有振动速度值的平均值 Vmin 的差大
于 0.5Vmax。同时该测点 1-6 倍频之和大于总值的 80%。则设备存在壳体变形故障；检
查基础台板变形或出入口管线应力。 
2）STRESS1--电机 
  如果 X 测点其 4 天趋势符合以下描述：大于 4 天拟合直线上对应点的所有振动速度值的
平均值 Vmax 减去小于 4 天拟合直线上对应点的所有振动速度值的平均值 Vmin 的差大
于 0.5Vmax。同时该测点 1-6 倍频之和大于总值的 80%。则设备存在壳体变形故障；检
查基础台板变形。 
             */
            // todo
            if (!PartParameter.IsStiffBase) return false;
            bool found = false;
            if (Calc(DEPoints.VelPoints,EquipmentFaultType.Generic.Stress001)) found = true;
            return found;

            bool Calc(PointDataCollection velPoints, string code)
            {
                if (0 == velPoints.Count) return false;
                var pVertical = velPoints.GetByDirectionId(PntDirectionID.Vertical).MaxItem(p => p.HistorySummaryData.MeasureValue);
                var pHorizontal = velPoints.GetByDirectionId(PntDirectionID.Horizontal).MaxItem(p => p.HistorySummaryData.MeasureValue);
                if (pHorizontal == null || pVertical == null) return false;
                if (!(0.8 * pHorizontal.HistorySummaryData.MeasureValue < pVertical.HistorySummaryData.MeasureValue)) return false;

                var summary = pVertical.HistorySummaryData;
                var timewave = pVertical.TimewaveData;

                double limit_0p8_Overall = 0.8 * timewave.Overall;
                int xFFTStartIndex = 0;
                int xFFTCount = 6;
                var partialOverall = timewave.SpectrumUtils.GetOverallByXFFT(xFFTStartIndex, xFFTCount);
                if (limit_0p8_Overall < partialOverall)
                {
                    Config.Logger.Info(code);
                    AddPossibleFault(pVertical, code);
                    return true;
                }
                return false;
            }
        }
        #endregion

        #region 泵的流体相关故障

        /// <summary>
        /// 计算 流体激励故障  FL-EXCIT1 汽蚀-泵
        /// </summary>
        /// <returns>true:存在故障</returns>
        protected virtual bool CalcFL_EXCIT1()
        {
            /*
              泵入口压力小于 1.2 倍汽蚀余量（如果能计算的话），且加速度峰值最大测点的振动速
度谱图上 60+1*谱图分辨率+......+500*谱图分辨率的所有谱线中至少有 200 个频率幅值
大于谱图上最高峰值的 20%。则该泵存在汽蚀故障。 
  如果没有泵入口压力，则如果加速度峰值最大测点的振动速度谱图上 60+1*谱图分辨率
+......+500*谱图分辨率的所有谱线中至少有 200 个频率幅值大于谱图上最高峰值的
20%。则该泵存在汽蚀故障或流体激励故障。 
             */
            // todo
            if (!PartParameter.IsStiffBase) return false;
            bool found = false;
            if (Calc(DEPoints.VelPoints,EquipmentFaultType.Pump.Liquid001)) found = true;
            if (Calc(DEPoints.VelPoints,EquipmentFaultType.Pump.Liquid002)) found = true;
            return found;

            bool Calc(PointDataCollection velPoints, string code)
            {
                if (0 == velPoints.Count) return false;
                return false;
            }
        }

        /// <summary>
        /// 计算 流体激励故障 FL-EXCIT2 泵回流--泵 
        /// </summary>
        /// <returns>true:存在故障</returns>
        protected virtual bool CalcFL_EXCIT2()
        {
            /*
            如果泵背压大于泵出口压力（如果能计算的话），且加速度峰值最大测点的振动速度谱
图上 60+1*谱图分辨率+......+300*谱图分辨率的所有谱线中至少有 100 个频率幅值大于
谱图上最高峰值的 20%。则该泵存在回流故障。 
            如果不能计算泵背压与出口压力，则加速度峰值最大测点的振动速度谱图上 60+1*谱图
分辨率+......+300*谱图分辨率的所有谱线中至少有 100 个频率幅值大于谱图上最高峰值
的 20%。则该泵存在回流或流体激励故障。 
             */
            // todo
            if (!PartParameter.IsStiffBase) return false;
            bool found = false;
            if (Calc(DEPoints.VelPoints,EquipmentFaultType.Pump.Liquid003)) found = true;
            if (Calc(DEPoints.VelPoints,EquipmentFaultType.Pump.Liquid004)) found = true;
            return found;

            bool Calc(PointDataCollection velPoints, string code)
            {
                if (0 == velPoints.Count) return false;
                return false;
            }
        }

        /// <summary>
        /// 计算 泵、风机叶轮偏心或流体不均故障  
        /// </summary>
        /// <returns>true:存在故障</returns>
        protected virtual bool CalcRotor_ecc1(PointData pnt = null)
        {
            /*
            1）Rotor-ecc1--泵、风机 
  如果测点 X 的频谱上存在叶轮通过频率（主频*叶片数）的幅值大于总值 60%，则诊断
为叶轮偏心或流体不均故障。 
             */
            bool found = false;
            PointDataCollection points = pnt != null ? new PointDataCollection {pnt} : Points.VelPoints;
            if (Calc(points,PartParameter?.FanFeatureFreqs,EquipmentFaultType.Pump.Liquid005)) found = true;
            return found;

            bool Calc(PointDataCollection velPoints,ICollection<FanFeatureFreq> featureFreqs,string code)
            {
                if (CollectionUtils.IsNullOrEmptyG(velPoints) || CollectionUtils.IsNullOrEmptyG(featureFreqs)) return false;
                foreach( var point in velPoints )
                {
                    var summary = point.HistorySummaryData;
                    var f0 = summary.F0;
                    var timewave = point.TimewaveData;
                    var spectrumUtils = timewave.SpectrumUtils;
                    double limit = 0.6 * timewave.Overall;
                    foreach( var featureFreq in featureFreqs)
                    {
                        var freq = featureFreq.BladeCount * featureFreq.ShaftMultiplier * f0;
                        var featureValue = spectrumUtils.GetFFTAmp(freq);
                        if (limit <= featureValue)
                        {
                            Config.Logger.Info(code);
                            AddPossibleFault(point, code);
                            return true;
                        }
                    }
                }

                return false;
            }
        }

        /// <summary>
        /// 计算 不对中故障  
        /// </summary>
        /// <returns>true:存在故障</returns>
        protected virtual bool CalcMISAGN1(PointData pnt = null)
        {
            /*
            1）MISAGN1--通用风机、泵（离心风机、轴流风机、离心泵、轴流泵）、刚性基础 
（1）如果 4 天内 X 与负载（电流 I、入口流量 Qi、出口流量 Qo，按优先顺序只取一个指标）
线性相关系数 r 大于 0.3。（如果 I、Qi、Qo 其中一个有效，则（1）、（2）同时成立则为
不对中故障；如果 I、Qi、Qo 均无效，则（1）忽略，只看（2）知否成立） 
（2）（X 与下面对号入座） 
              如果 FDE-A-VEL 或者 FNDE-A-VEL（按优先顺序只取一个）大于 0.7 倍 FDE-H-VEL 或者 0.7
倍 FNDE-H-VEL 或者 1.5 倍 FDE-V-VEL 或者 1.5 倍 FNDE-V-VEL（按优先顺序只取一个）、
且 FDE-A-VEL 或者 FNDE-A-VEL 其主频与 2 倍频的和大于通频值的 60%同时成立时。 
              如果 FDE-H-VEL、FNDE-H-VEL、FDE-V-VEL、FNDE-V-VEL（取最大值）其主频与 2 倍频的
和大于通频值的 80%同时成立时。 
             */
            bool found = false;
            var code = EquipmentFaultType.Generic.Misalign001;
            var firstLoadPoint = PartParameter.FirstLoadPoint;
            if(firstLoadPoint != null)
            {
                int days = 4;
                var trend1 = GetInt32MinuteSummaryByDays(pnt, days);
                var trend2 = GetInt32MinuteSummaryByDays(firstLoadPoint, days);

                var pearson = CalcPearsonCorrelationCoefficient(trend1, trend2);
                const double limit_pearson = 0.3;
                if (pearson <= limit_pearson) return false;
            }

            IEnumerable<PointData>[] pointDatas = new IEnumerable<PointData>[] {DEPoints,NDEPoints};
            var pAxis = CollectionUtils.FirstOrDefault(p => p.IsAxial && p.IsVel,  pointDatas);
            // 不存在轴向测点，走的二条规则
            if (pAxis == null)
            {
                var pMaxVelHV = Points.Where(p => p.IsVel && (p.IsHorizontal || p.IsVertical)).MaxItem(p => p.MeasureValue);
                if(pMaxVelHV != null)
                {
                    var point = pMaxVelHV;
                    var summary = point.HistorySummaryData;
                    var timewave = point.TimewaveData;
                    var spectrumUtils = timewave.SpectrumUtils;
                    // 如果 FDE-H-VEL、FNDE-H-VEL、FDE-V-VEL、FNDE-V-VEL（取最大值）其主频与 2 倍频的和大于通频值的 80%同时成立时
                    if(0.8 * timewave.Overall < spectrumUtils.GetOverallByXFFT(0,2))
                    {
                        Config.Logger.Info(code);
                        AddPossibleFault(point, code);
                        found = true;
                    }
                }
                return found;
            }

            double limit;
            var pHorizontal = CollectionUtils.FirstOrDefault(p => p.IsHorizontal && p.IsVel,  pointDatas);
            if(pHorizontal != null) limit = 0.7 * pHorizontal.HistorySummaryData.MeasureValue;
            else
            {
                var pVertical = CollectionUtils.FirstOrDefault(p => p.IsVertical && p.IsVel,  pointDatas);
                if (pVertical == null) return false;
                limit = 1.5 * pVertical.HistorySummaryData.MeasureValue;
            }
            //  如果 FDE-A-VEL 或者 FNDE-A-VEL（按优先顺序只取一个）大于 0.7 倍 FDE-H-VEL 或者 0.7倍 FNDE-H-VEL 或者 1.5 倍 FDE-V-VEL 或者 1.5 倍 FNDE-V-VEL
            if(limit < pAxis.HistorySummaryData.MeasureValue)
            {
                var point = pAxis;
                var timewave = point.TimewaveData;
                var spectrumUtils = timewave.SpectrumUtils;
                // FDE-A-VEL 或者 FNDE-A-VEL 其主频与 2 倍频的和大于通频值的 60%同时成立时
                if(0.6 * timewave.Overall < spectrumUtils.GetOverallByXFFT(0,2))
                {
                    Config.Logger.Info(code);
                    AddPossibleFault(point, code);
                    found = true;
                }
            }
            return found;
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

        #region 计算是否趋势报警，并进一步计算故障

        /// <summary>
        /// 是否加速度测点趋势报警 
        /// </summary>
        /// <returns>报警测点</returns>
        protected virtual PointData CalcIsAccAlarm()
        {
            /*
            如果泵所有振动测点振动加速度值有一
个达到报警值，或者所有测点振动加速度有效值虽然都没有达到报警值，但其中任一个测点在 4 天内的振动加速度峰
值趋势直线拟合斜率大于 0.417。 
             */
            //todo
            PointData ret = null;
            return ret;

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
        /// 是否速度测点趋势报警 
        /// </summary>
        /// <returns>报警测点</returns>
        protected virtual PointData CalcIsVelAlarm()
        {
            /*
            如果电机所有振动测点振动速度值有一个达到报警值，或者
所有测点振动速度有效值虽然都没有达到报警值，但其中任
一个测点在 4 天内的振动速度有效值趋势直线拟合斜率大
于 0.375，则按以下进行配合间隙不良故障诊断分析。 
             */
            //todo
            PointData ret = null;
            return ret;
        }

        #endregion
    }

}