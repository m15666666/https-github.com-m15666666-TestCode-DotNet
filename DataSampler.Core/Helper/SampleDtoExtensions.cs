using AnalysisData.SampleData;
using Moons.DataSample.Shared.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataSampler.Core.Helper
{
    /// <summary>
    /// 数据转换
    /// </summary>
    public static class SampleDtoExtensions
    {
        public static TrendDataDto ToTrendDataDto(this TrendData d)
        {
            return d.ToTrendDataDto<TrendDataDto>();
        }

        private static T ToTrendDataDto<T>(this TrendData d) where T : TrendDataDto, new()
        {
            return new T
            {
                SyncUniqueID = d.SyncUniqueID,
                SyncID = d.SyncID,
                PointID = d.PointID,
                DataUsageID = d.DataUsageID,
                AlmLevelID = d.AlmLevelID,
                SampleTime = d.SampleTime,
                DataLength = d.DataLength,
                Rev = d.Rev,
                SampleFreq = d.SampleFreq,
                MultiFreq = d.MultiFreq,
                AlmEventUniqueID = d.AlmEventUniqueID,
                AlmID = d.AlmID,
                MeasurementValue = d.MeasurementValue,
                MeasurementValue2 = d.MeasurementValue2,
                MeasurementValueString4Opc = d.MeasurementValueString4Opc,
                AxisOffsetValue = d.AxisOffsetValue,
                WirelessSignalIntensity = d.WirelessSignalIntensity,
                BatteryPercent = d.BatteryPercent,
                VariantName = d.VariantName,
            };
        }

        public static TimeWaveDataDto ToTimeWaveDataDto(this TimeWaveData_1D d)
        {
            var ret = d.ToTrendDataDto<TimeWaveDataDto>();
            ret.Wave = d.Wave;
            return ret;
        }

        public static AlmEventDataDto ToAlmEventDataDto(this AlmEventData d)
        {
            return new AlmEventDataDto
            {
                PointID = d.PointID,
                AlmTime = d.AlmTime,
                AlmLevel = d.AlmLevel,
                AlmSourceID = d.AlmSourceID,
                AlmEventUniqueID = d.AlmEventUniqueID,
                AlmID = d.AlmID,
                AlmCount = d.AlmCount,
                AlmValue = d.AlmValue,
            };
        }
    }
}
