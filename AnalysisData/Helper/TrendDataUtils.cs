using System;
using System.Collections.Generic;
using System.Text;

namespace AnalysisData.Helper
{
    /// <summary>
    /// 趋势数据的实用工具类
    /// </summary>
    public static class TrendDataUtils
    {
        /// <summary>
        /// 抽取数据
        /// </summary>
        /// <param name="trendDatas">趋势数据</param>
        /// <param name="begin">起始的时间点</param>
        /// <param name="interval">时间间隔</param>
        /// <param name="max">最新的采样时间</param>
        /// <returns>抽取的数据</returns>
        public static List<TrendData> FilterByInterval(List<TrendData> trendDatas, DateTime begin, TimeSpan interval, DateTime max)
        {
            if (interval <= TimeSpan.Zero)
            {
                return trendDatas;
            }

            TrendData[] datas = trendDatas.ToArray();
            var ret = new List<TrendData>();
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
