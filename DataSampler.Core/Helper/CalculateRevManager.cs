using AnalysisAlgorithm;
using AnalysisData.Constants;
using AnalysisData.SampleData;
using Moons.Common20;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataSampler.Helper
{
    /// <summary>
    /// 软件计算转速
    /// </summary>
    internal static class CalculateRevManager
    {
        /// <summary>
        /// 内部锁
        /// </summary>
        private static object _lock = new object();

        private static Dictionary<int, PointSamplingParameter> PointId2SamplingParameter { get; set; } = new Dictionary<int, PointSamplingParameter>();

        /// <summary>
        /// 注册采样信息
        /// </summary>
        /// <param name="sampleStationData"></param>
        public static void RegisterSamplingParameters( SampleStationData sampleStationData )
        {
            lock (_lock) {
                foreach( var pointData in sampleStationData.PointDatas )
                {
                    var channelCd = pointData.ChannelCD_1;
                    var channelData = sampleStationData.ChannelDatas.FirstOrDefault( p => p.ChannelCD == channelCd );

                    var revLow = channelData.RevLowThreshold;
                    var revHigh = channelData.RevHighThreshold;
                    if ( channelData.SignalTypeID == SignalTypeID.Velocity && 0 < revLow && revLow <= revHigh )
                    {
                        PointId2SamplingParameter[pointData.PointID] = new PointSamplingParameter { PointData = pointData, ChannelData = channelData };
                    }
                }
            }
        }

        public static void AddRev( TimeWaveData_1D data )
        {
            var pointId = data.PointID;
            if ( 0 < data.Rev )
            {
                TraceUtils.Info( $"AddRev fail, pointId : {pointId}, 0 < data.Rev, return." );
                return;
            }

            
            var pointId2SamplingParameter = PointId2SamplingParameter;
            lock (_lock)
            {
                if( !pointId2SamplingParameter.ContainsKey( pointId ) )
                {
                    TraceUtils.Info( $"AddRev fail, pointId : {pointId}, !pointId2SamplingParameter.ContainsKey( pointId ), return." );
                    return;
                }

                var parameter = pointId2SamplingParameter[pointId];
                var channelData = parameter.ChannelData;
                var beginFreq = DSPBasic.RpmtoHz( channelData.RevLowThreshold );
                var endFreq = DSPBasic.RpmtoHz( channelData.RevHighThreshold );

                var fs = data.SampleFreq;

                if ( DSPBasic.GetFreqBandBySampleFreq( fs ) < endFreq )
                {
                    TraceUtils.Info( $"AddRev fail, pointId : {pointId}, endFreq0 is too large, return." );
                    return;
                }

                double amplitudeOfF0;
                double f0 = DSPBasic.HztoRpm( SpectrumBasic.GetBaseFreq( fs, beginFreq, endFreq, data.Wave, out amplitudeOfF0 ) );

                const double AmpThreshold_Velocity = 0.25;
                // 速度测点，单位是mm/s，如果幅值大于“AmpThreshold_Velocity”，则认为转速有效。
                if ( amplitudeOfF0 < AmpThreshold_Velocity )
                {
                    TraceUtils.Info( $"AddRev fail, pointId : {pointId}, amplitude of f0 is too small, return." );
                    return;
                }

                data.Rev = (int)f0;
            }
        }
    }

    /// <summary>
    /// 测点采样参数类
    /// </summary>
    internal class PointSamplingParameter
    {
        public PointData PointData { get; set; }

        public ChannelData ChannelData { get; set; }
    }

}
