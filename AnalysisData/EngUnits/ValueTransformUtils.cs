using AnalysisData.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnalysisData.EngUnits
{
    /// <summary>
    /// 指标和波形根据单位转换的实用工具类
    /// </summary>
    public class ValueTransformUtils
    {
        private readonly Dictionary<long, EngUnitData> _engId2EngUnitData = new Dictionary<long, EngUnitData>();
        private readonly Dictionary<long, EngUnitData> _defaultEngUnitData = new Dictionary<long, EngUnitData>();

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="engUnitDatas"></param>
        public void Init( IEnumerable<EngUnitData> engUnitDatas)
        {
            foreach (var engUnit in engUnitDatas )
            {
                _engId2EngUnitData[engUnit.EngUnit_ID] = engUnit;
                if (engUnit.IsDefault_YN == "1") _defaultEngUnitData[engUnit.UnitType_ID] = engUnit;
            }
        }

        /// <summary>
        /// 根据单位ID获得单位和默认单位
        /// </summary>
        /// <param name="engUnitID">单位ID</param>
        /// <param name="engUnit">输出，单位</param>
        /// <param name="defaultEngUnit">输出，默认单位</param>
        public void GetEngUnitsByID(long engUnitID, out EngUnitData engUnit, out EngUnitData defaultEngUnit)
        {
            engUnit = _engId2EngUnitData[engUnitID];
            defaultEngUnit = _defaultEngUnitData[engUnit.UnitType_ID];
        }

        #region 将数据从默认单位转换为当前单位

        /// <summary>
        /// 将数据从默认单位转换为当前单位
        /// </summary>
        /// <param name="timeWaveData">源数据</param>
        /// <param name="engUnitID">单位ID</param>
        public void TransformFromDefaultEngUnit(TimeWaveData timeWaveData, int engUnitID)
        {
            EngUnitData engUnit, defaultEngUnit;
            GetEngUnitsByID(engUnitID, out engUnit, out defaultEngUnit);
            if (defaultEngUnit != engUnit)
            {
                timeWaveData.MeasurementValue = TransformValue(defaultEngUnit, engUnit, timeWaveData.MeasurementValue);
                TransformValues(defaultEngUnit, engUnit, timeWaveData.Wave);
            }
        }

        /// <summary>
        /// 将数据从默认单位转换为当前单位
        /// </summary>
        /// <param name="spectrumTimeWaveData">源数据</param>
        /// <param name="engUnitID">单位ID</param>
        public void TransformFromDefaultEngUnit(SpectrumTimeWaveData spectrumTimeWaveData, int engUnitID)
        {
            EngUnitData engUnit, defaultEngUnit;
            GetEngUnitsByID(engUnitID, out engUnit, out defaultEngUnit);
            if (defaultEngUnit != engUnit)
            {
                spectrumTimeWaveData.MeasurementValue = TransformValue(defaultEngUnit, engUnit,
                                                                        spectrumTimeWaveData.MeasurementValue);
                TransformValues(defaultEngUnit, engUnit, spectrumTimeWaveData.TimeWave);
            }
        }

        /// <summary>
        /// 将数据从默认单位转换为当前单位
        /// </summary>
        /// <param name="trendDataList">源数据</param>
        /// <param name="engUnitID">单位ID</param>
        /// <param name="featureValueID">特征参数类型编号</param>
        public void TransformFromDefaultEngUnit(List<TrendData> trendDataList, int engUnitID,
                                                        int featureValueID)
        {
            EngUnitData engUnit, defaultEngUnit;
            GetEngUnitsByID(engUnitID, out engUnit, out defaultEngUnit);
            if (defaultEngUnit == engUnit)
            {
                return;
            }

            foreach (TrendData trendData in trendDataList)
            {
                trendData.MeasurementValue = TransformFeatureValue(defaultEngUnit, engUnit, featureValueID,
                                                                    trendData.MeasurementValue);
            }
        }

        /// <summary>
        /// 将数据从默认单位转换为当前单位
        /// </summary>
        /// <param name="trendDataList">源数据</param>
        /// <param name="engUnitID">单位ID</param>
        /// <returns>转换后的数据</returns>
        public List<AllFeatureTrendData> TransformFromDefaultEngUnit(List<AllFeatureTrendData> trendDataList,
                                                                             int engUnitID)
        {
            EngUnitData engUnit, defaultEngUnit;
            GetEngUnitsByID(engUnitID, out engUnit, out defaultEngUnit);
            if (defaultEngUnit == engUnit)
            {
                return trendDataList;
            }

            foreach (AllFeatureTrendData trendData in trendDataList)
            {
                trendData.MeasurementValue = TransformValue(defaultEngUnit, engUnit, trendData.MeasurementValue);
                trendData.P = TransformValue(defaultEngUnit, engUnit, trendData.P);
                trendData.PP = TransformValue(defaultEngUnit, engUnit, trendData.PP);
                trendData.RMS = TransformValue(defaultEngUnit, engUnit, trendData.RMS);
                trendData.Mean = TransformValue(defaultEngUnit, engUnit, trendData.Mean);
            }

            return trendDataList;
        }

        /// <summary>
        /// 将数据从默认单位转换为当前单位
        /// </summary>
        /// <param name="value">源数据</param>
        /// <param name="engUnitID">单位ID</param>
        /// <param name="featureValueID">特征参数ID</param>
        /// <returns>转换后的数据</returns>
        public double TransformFromDefaultEngUnit(double value, int engUnitID, int featureValueID)
        {
            EngUnitData engUnit, defaultEngUnit;
            GetEngUnitsByID(engUnitID, out engUnit, out defaultEngUnit);
            return defaultEngUnit == engUnit
                       ? value
                       : TransformFeatureValue(defaultEngUnit, engUnit, featureValueID, value);
        }

        #endregion

        #region 基本函数

        /// <summary>
        /// 转换数据
        /// </summary>
        /// <param name="source">数据原始单位</param>
        /// <param name="target">目标单位</param>
        /// <param name="value">源数据</param>
        /// <returns>转换计算后的数据</returns>
        private double? TransformValue(EngUnitData source, EngUnitData target, double? value)
        {
            return value == null ? value : TransformValue(source, target, value.Value);
        }

        /// <summary>
        /// 转换特征参数数据
        /// </summary>
        /// <param name="source">数据原始单位</param>
        /// <param name="target">目标单位</param>
        /// <param name="featureValueID">特征参数类型编号</param>
        /// <param name="value">源数据</param>
        /// <returns>转换计算后的数据</returns>
        public double TransformFeatureValue(EngUnitData source, EngUnitData target, int featureValueID,
                                                    double value)
        {
            if (featureValueID == FeatureValueID.Measurement)
            {
                return TransformValue(source, target, value);
            }

            double scale = target.Scale_NR / source.Scale_NR;
            switch (featureValueID)
            {
                case FeatureValueID.P:
                case FeatureValueID.PP:
                case FeatureValueID.RMS:
                case FeatureValueID.Mean:
                case FeatureValueID.AxisOffset:
                    return value * scale;

                default:
                    return value;
            }
        }

        /// <summary>
        /// 转换数据
        /// </summary>
        /// <param name="source">数据原始单位</param>
        /// <param name="target">目标单位</param>
        /// <param name="value">源数据</param>
        /// <returns>转换计算后的数据</returns>
        private double TransformValue(EngUnitData source, EngUnitData target, double value)
        {
            return (value / (double)source.Scale_NR - (double)source.Offset_NR + (double)target.Offset_NR) * (double)target.Scale_NR;
        }

        /// <summary>
        /// 转换数据
        /// </summary>
        /// <param name="source">数据原始单位</param>
        /// <param name="target">目标单位</param>
        /// <param name="values">数据</param>
        public void TransformValues(EngUnitData source, EngUnitData target, double[] values)
        {
            if (source.EngUnit_ID == target.EngUnit_ID)
            {
                return;
            }

            double sourceScale = source.Scale_NR;
            double sourceOffset = source.Offset_NR;
            double targetOffset = target.Offset_NR;
            double targetScale = target.Scale_NR;
            for (int index = 0; index < values.Length; index++)
            {
                values[index] = (values[index] / sourceScale - sourceOffset + targetOffset) * targetScale;
            }
        }

        #endregion
    }
}
