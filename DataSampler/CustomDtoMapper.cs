using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnalysisData.Dto;
using AnalysisData.SampleData;
using AutoMapper;
using AutoMapper.Configuration;

namespace DataSampler
{
    /// <summary>
    /// automapper 映射初始化
    /// </summary>
    internal static class CustomDtoMapper
    {
        public static void CreateCustomMappings(this IMapperConfigurationExpression cfg)
        {
            //cfg.CreateMap<string, long>()
            //    .ConvertUsing(str => Convert.ToInt64(str));
            cfg.CreateMap<bool, int>().ConvertUsing( v => v ? 1 : 0);
            cfg.CreateMap<AlmCountData, AlmCountDataDto>();
            cfg.CreateMap<ChannelData, ChannelDataDto>();
            cfg.CreateMap<AlmStand_CommonSettingData, AlmStand_CommonSettingDataDto>();
            cfg.CreateMap<PointData, PointDataDto > ();
            cfg.CreateMap<SampleStationData, SampleStationDataDto > ();

            cfg.CreateMap<TrendData, TrendDataDto> ();
            cfg.CreateMap<TimeWaveData_1D, TimeWaveDataDto> ();
            cfg.CreateMap<AlmEventData, AlmEventDataDto> ();
        }
    }
}
