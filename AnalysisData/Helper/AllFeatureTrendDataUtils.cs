using System;
using System.Collections.Generic;
using AnalysisData.Constants;

namespace AnalysisData.Helper
{
    /// <summary>
    /// 包含各特征指标的趋势数据的实用工具类
    /// </summary>
    public static class AllFeatureTrendDataUtils
    {
        /// <summary>
        /// 根据设置
        /// </summary>
        /// <param name="featureTrendData">特征参数</param>
        /// <param name="featureItem">特征参数数据来源</param>
        public static void SetFeaturesByFeatureValueID(AllFeatureTrendData featureTrendData, AllFeatureTrendData featureItem)
        {
            switch (featureItem.FeatureValueID)
            {
                case FeatureValueID.Measurement:
                    featureTrendData.MeasurementValue = featureItem.MeasurementValue;
                    break;
                case FeatureValueID.P:
                    featureTrendData.P = featureItem.MeasurementValue;
                    break;

                case FeatureValueID.RMS:
                    featureTrendData.RMS = featureItem.MeasurementValue;
                    break;

                case FeatureValueID.PP:
                    featureTrendData.PP = featureItem.MeasurementValue;
                    break;

                case FeatureValueID.Mean:
                    featureTrendData.Mean = featureItem.MeasurementValue;
                    break;

                case FeatureValueID.ShapeFactor:
                    featureTrendData.ShapeFactor = featureItem.MeasurementValue;
                    break;

                case FeatureValueID.KurtoFactor:
                    featureTrendData.KurtoFactor = featureItem.MeasurementValue;
                    break;

                case FeatureValueID.AxisOffset:
                    featureTrendData.AxisOffset = featureItem.AxisOffset;
                    break;
                default:
                    //case FeatureValueID.Measurement:
                    //featureTrendData.MeasurementValue = featureItem.MeasurementValue;
                    break;
            }
        }

        /// <summary>
        /// 抽取数据
        /// </summary>
        /// <param name="trendDatas">趋势数据</param>
        /// <param name="begin">起始的时间点</param>
        /// <param name="interval">时间间隔</param>
        /// <param name="max">最新的采样时间</param>
        /// <returns>抽取的数据</returns>
        public static List<AllFeatureTrendData> FilterByInterval(List<AllFeatureTrendData> trendDatas, DateTime begin, TimeSpan interval, DateTime max)
        {
            if (interval <= TimeSpan.Zero)
            {
                return trendDatas;
            }

            AllFeatureTrendData[] datas = trendDatas.ToArray();
            var ret = new List<AllFeatureTrendData>();
            int index = 0;
            while ((begin <= max) && (index < datas.Length))
            {
                DateTime end = begin.Add(interval);

                while (index < datas.Length)
                {
                    var data = datas[index];
                    var sampleTime = data.SampleTime;

                    // 移动到下一个时间范围
                    if (end <= sampleTime)
                    {
                        break;
                    }

                    // 移动到下一个数据
                    if (sampleTime < end)
                    {
                        index++;

                        // 同时也移动到下一个时间范围
                        if (begin <= sampleTime)
                        {
                            ret.Add(data);
                            break;
                        }

                        continue;
                    }
                } // while( index < datas.Length )

                begin = end;
            } // while( (begin <= max) && ( index < datas.Length ) )

            return ret;
        }
    }
}
