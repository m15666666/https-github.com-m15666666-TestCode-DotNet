using System;
using System.Collections.Generic;
using System.Text;
using AnalysisAlgorithm;
using Moons.Common20;
using Moons.EquipmentDiagnosis.Core.Abstractions;
using Moons.EquipmentDiagnosis.Core.Dto;
using System.Linq;
using AnalysisData.FeatureFreq;
using AnalysisData.Constants;


namespace Moons.EquipmentDiagnosis.Core.Implementations
{
    /// <summary>
    /// 电机诊断模型
    /// </summary>
    public class MotorDiagnosis001 : PartDiagnosisBase001
    {
        public override void DoDiagnosis()
        {
            Points.ClearNDaysCache();

            CalcLoose();
            CalcRub();
            CalcELECTRC1();
            CalcELECTRC2();
            CalcELECTRC3();
            CalcBearingFTF();
            PointData almPoint = CalcIsAccAlarm();
            if (almPoint != null)
            {
                CalcBearingBSF();
                CalcBearingBPFO();
                CalcBearingBPFI();
            }
            almPoint = CalcIsVelAlarm();
            if (almPoint != null)
            {
                CalcCLEARANCE();
                CalcFDLOOSE();
                CalcSTRESS(almPoint);
                if (PartParameter.IsStiffBase) // 刚性基础
                {
                    CalcMISAGN1(almPoint);
                    CalcUNBL10(almPoint);

                }
                else // 弹性基础
                {
                    CalcMISAGN2(almPoint);
                    CalcUNBL11(almPoint);
                }
            }

            Points.ClearNDaysCache();
        }

        #region 具体故障

        /// <summary>
        /// 计算转子断条 ELECTRC1 断条--电机 故障
        /// </summary>
        /// <returns></returns>
        private bool CalcELECTRC1()
        {
            /*
             1.1）ELECTRC1 断条--电机 
  电机所有测点中振动速度值最大的频谱图上存在主频 1X 幅值大于 50%总值，且 1X+极
数*滑差或 1X-极数*滑差的幅值大于 1X 幅值的 20%。诊断为断条故障。 
             
             */
            if (PartParameter.IsUnStableSpeed) return false;

            bool found = false;
            if (Calc(Points.VelPoints,EquipmentFaultType.Generic.Electric002)) found = true;
            return found;

            bool Calc(PointDataCollection velPoints, string code)
            {
                if (0 == velPoints.Count) return false;
                var point = velPoints.MaxMeasureValueP();
                var summary = point.HistorySummaryData;
                var timewave = point.TimewaveData;
                var spectrumUtils = timewave.SpectrumUtils;
                if (!timewave.IsFreqResolutionMatchELECTRCFault) return false;
                
                double offsetFreq = PartParameter.MotorSlipDiffFrequence;
                if (offsetFreq <= double.Epsilon) return false;

                var freqResolution = spectrumUtils.FrequencyResolution;
                if (offsetFreq < 2 * freqResolution) return false;

                int peakOffset = 3 * freqResolution <= offsetFreq ? 1 : 0;
                double leftFreq = spectrumUtils.F0 - offsetFreq;
                double rightFreq = spectrumUtils.F0 - offsetFreq;
                var leftAmp = spectrumUtils.GetFFTAmp(leftFreq, peakOffset);
                var rightAmp = spectrumUtils.GetFFTAmp(rightFreq, peakOffset);

                double limit = 0.2 * timewave.X1;
                if (limit < leftAmp || limit < rightAmp)
                {
                    Config.Logger.Info(code);
                    AddPossibleFault(point, code);
                    return true;
                }
                return false;
            }
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
                var point = velPoints.MaxMeasureValueP();
                var summary = point.HistorySummaryData;
                var timewave = point.TimewaveData;
                if (!timewave.IsFreqResolutionMatchELECTRCFault) return false;

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
  电机所有测点中振动速度值最大的频谱图上 100Hz、200Hz、300Hz、400Hz、500Hz 频
率中至少有 3 个幅值大于频谱中最高幅值的 50%。诊断为定子短路故障。 
             */
            bool found = false;
            if (Calc(Points.VelPoints,EquipmentFaultType.Generic.Electric004)) found = true;
            return found;

            bool Calc(PointDataCollection velPoints, string code)
            {
                if (0 == velPoints.Count) return false;
                var point = velPoints.MaxMeasureValueP();
                var summary = point.HistorySummaryData;
                var timewave = point.TimewaveData;
                if (!timewave.IsFreqResolutionMatchELECTRCFault) return false;

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
