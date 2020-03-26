using AnalysisData.SampleData;
using AnalysisData.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using Moons.Common20.ObjectMapper;

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
            //return new T
            //{
            //    SyncUniqueID = d.SyncUniqueID,
            //    SyncID = d.SyncID,
            //    PointID = d.PointID,
            //    DataUsageID = d.DataUsageID,
            //    AlmLevelID = d.AlmLevelID,
            //    SampleTime = d.SampleTime,
            //    DataLength = d.DataLength,
            //    Rev = d.Rev,
            //    SampleFreq = d.SampleFreq,
            //    MultiFreq = d.MultiFreq,
            //    AlmEventUniqueID = d.AlmEventUniqueID,
            //    AlmID = d.AlmID,
            //    MeasurementValue = d.MeasurementValue,
            //    MeasurementValue2 = d.MeasurementValue2,
            //    MeasurementValueString4Opc = d.MeasurementValueString4Opc,
            //    AxisOffsetValue = d.AxisOffsetValue,
            //    WirelessSignalIntensity = d.WirelessSignalIntensity,
            //    BatteryPercent = d.BatteryPercent,
            //    VariantName = d.VariantName,
            //};
            var ret = new T();
            Config.ObjectMapper.MapTo(d, ret);
            return ret;
        }

        public static TimeWaveDataDto ToTimeWaveDataDto(this TimeWaveData_1D d)
        {
            //var ret = d.ToTrendDataDto<TimeWaveDataDto>();
            //ret.Wave = d.Wave;
            TimeWaveDataDto ret = new TimeWaveDataDto();
            Config.ObjectMapper.MapTo(d, ret);
            return ret;
        }

        public static AlmEventDataDto ToAlmEventDataDto(this AlmEventData d)
        {
            //return new AlmEventDataDto
            //{
            //    PointID = d.PointID,
            //    AlmTime = d.AlmTime,
            //    AlmLevel = d.AlmLevel,
            //    AlmSourceID = d.AlmSourceID,
            //    AlmEventUniqueID = d.AlmEventUniqueID,
            //    AlmID = d.AlmID,
            //    AlmCount = d.AlmCount,
            //    AlmValue = d.AlmValue,
            //};
            var ret = Config.ObjectMapper.MapTo<AlmEventDataDto>(d);
            return ret;
        }

        public static SampleStationDataDto ToSampleStationDataDto(this SampleStationData d)
        {
            //var ret = new SampleStationDataDto {
            //    DataSamplerIP = d.DataSamplerIP,
            //    DataSamplerPort = d.DataSamplerPort,
            //    SampleStationIP = d.SampleStationIP,
            //    SampleStationPort = d.SampleStationPort,
            //    QueryIntervalInSecond = d.QueryIntervalInSecond,
            //    UploadDBWaveIntervalInSecond = d.UploadDBWaveIntervalInSecond,
            //    UploadDBStaticIntervalInSecond = d.UploadDBStaticIntervalInSecond,
            //};
            //foreach (var data in d.PointDatas)
            //    ret.PointDatas.Add(data.ToPointDataDto());
            //foreach (var data in d.ChannelDatas)
            //    ret.ChannelDatas.Add(data.ToChannelDataDto());

            var ret = Config.ObjectMapper.MapTo<SampleStationDataDto>(d);
            return ret;
        }

        public static PointDataDto ToPointDataDto(this PointData d)
        {
            //var ret = new PointDataDto
            //{
            //    PointID = d.PointID,
            //    MeasValueTypeID = d.MeasValueTypeID,
            //    Dimension = d.Dimension,
            //    PointName = d.PointName,
            //    EngUnitID = d.EngUnitID,
            //    EngUnit = d.EngUnit,
            //    ChannelCDs = d.ChannelCDs,
            //};
            //foreach (var data in d.AlmStand_CommonSettingDatas)
            //    ret.AlmStand_CommonSettingDatas.Add(data.ToAlmStand_CommonSettingDataDto());

            var ret = Config.ObjectMapper.MapTo<PointDataDto>(d);
            return ret;
        }

        public static AlmStand_CommonSettingDataDto ToAlmStand_CommonSettingDataDto(this AlmStand_CommonSettingData d)
        {
            //var ret = new AlmStand_CommonSettingDataDto
            //{
            //    AlmSource_ID = d.AlmSource_ID,
            //    AlmType_ID = d.AlmType_ID,
            //    LowLimit1_NR = d.LowLimit1_NR,
            //    LowLimit2_NR = d.LowLimit2_NR,
            //    HighLimit1_NR = d.HighLimit1_NR,
            //    HighLimit2_NR = d.HighLimit2_NR,
            //};

            var ret = Config.ObjectMapper.MapTo<AlmStand_CommonSettingDataDto>(d);
            return ret;
        }

        public static ChannelDataDto ToChannelDataDto(this ChannelData d)
        {
            //var ret = new ChannelDataDto
            //{
            //    ChannelIdentifier = d.ChannelIdentifier,
            //    ChannelCD = d.ChannelCD,
            //    RevChannelCD = d.RevChannelCD,
            //    KeyPhaserRevChannelCD = d.KeyPhaserRevChannelCD,
            //    SwitchChannelCD = d.SwitchChannelCD,
            //    SwitchTriggerStatus = d.SwitchTriggerStatus,
            //    SwitchTriggerMethod = d.SwitchTriggerMethod,
            //    ChannelTypeID = d.ChannelTypeID,
            //    ChannelNumber = d.ChannelNumber,
            //    SignalTypeID = d.SignalTypeID,
            //    SampleFreq = d.SampleFreq,
            //    MultiFreq = d.MultiFreq,
            //    DataLength = d.DataLength,
            //    AverageCount = d.AverageCount,
            //    RevLowThreshold = d.RevLowThreshold,
            //    RevHighThreshold = d.RevHighThreshold,
            //    ReferenceRev = d.ReferenceRev,
            //    RevTypeID = d.RevTypeID,
            //    RevRatio = d.RevRatio,
            //    ScaleFactor = d.ScaleFactor,
            //    VoltageOffset = d.VoltageOffset,
            //    ScaleFactorEngUnitID = d.ScaleFactorEngUnitID,
            //    CenterPositionVoltage = d.CenterPositionVoltage,
            //    CenterPositionVoltageEngUnitID = d.CenterPositionVoltageEngUnitID,
            //    CenterPosition = d.CenterPosition,
            //    CenterPositionEngUnitID = d.CenterPositionEngUnitID,
            //    LinearRangeMin = d.LinearRangeMin,
            //    LinearRangeMax = d.LinearRangeMax,
            //    LinearRangeEngUnitID = d.LinearRangeEngUnitID,
            //};

            //foreach (var data in d.AlmCountDatas)
            //    ret.AlmCountDatas.Add(data.ToAlmCountDataDto());

            var ret = Config.ObjectMapper.MapTo<ChannelDataDto>(d);
            
            return ret;
        }

        public static AlmCountDataDto ToAlmCountDataDto(this AlmCountData d)
        {
            return Config.ObjectMapper.MapTo<AlmCountDataDto>(d);
            //var ret = new AlmCountDataDto
            //{
            //    AlmSource_ID = d.AlmSource_ID,
            //    AlmCount = d.AlmCount,
            //};
            //return ret;
        }
    }
}
