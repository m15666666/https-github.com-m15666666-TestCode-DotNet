using System.IO;
using AnalysisAlgorithm;

namespace AnalysisData.SampleData
{
    /// <summary>
    /// 采样数据的实用工具
    /// </summary>
    public static class SampleDataUtils
    {
        #region 变量和属性

        /// <summary>
        /// 数据文件的版本，用于：启停机、追忆文件
        /// </summary>
        public const int DataFileVersion = 1;

        #endregion

        #region 创建FeatureData对象

        /// <summary>
        /// 从数组创建FeatureData对象
        /// </summary>
        /// <param name="features">指标数组</param>
        /// <param name="indexs">指标下标，依次是：peak，Average，Amp Average，AverageSqrtAmp，RMS，
        /// S，I，C，L，sigma^2，Skewness，Kurtosis 12个指标</param>
        /// <returns>FeatureData对象</returns>
        public static FeatureData CreateFeatureData( double[] features, int[] indexs )
        {
            var ret = new FeatureData();

            int index = 0;

            // peak
            ret.P = features[indexs[index++]];

            // 没计算PP值，只好从峰值获得
            ret.PP = ret.P * 2;

            // Average
            ret.Mean = features[indexs[index++]];

            // Amp Average
            index++;

            // AverageSqrtAmp
            index++;

            // RMS
            ret.RMS = features[indexs[index++]];

            // S
            ret.ShapeFactor = features[indexs[index++]];

            // I
            ret.ImpulseFactor = features[indexs[index++]];

            // C
            ret.CrestFactor = features[indexs[index++]];

            // L
            ret.ClearanceFactor = features[indexs[index++]];

            // sigma^2
            index++;

            // Skewness
            ret.SkewFactor = features[indexs[index++]];

            // Kurtosis
            ret.KurtoFactor = features[indexs[index++]];

            return ret;
        }

        /// <summary>
        /// 从时间波形创建FeatureData对象
        /// </summary>
        /// <param name="wave">时间波形</param>
        /// <returns>FeatureData对象</returns>
        public static FeatureData CreateFeatureData( double[] wave )
        {
            return new FeatureData
                       {
                           P = DSPBasic.TruePeak( wave ),
                           PP = DSPBasic.TruePeak2Peak( wave ),
                           RMS = DSPBasic.RMS( wave ),
                           Mean = StatisticsUtils.Mean( wave ),
                           ShapeFactor = DSPBasic.ShapeFactor( wave ),
                           ImpulseFactor = DSPBasic.ImpulseFactor( wave ),
                           CrestFactor = DSPBasic.CrestFactor( wave ),
                           ClearanceFactor = DSPBasic.ClearanceFactor( wave ),
                           SkewFactor = DSPBasic.SkewFactor( wave ),
                           KurtoFactor = DSPBasic.KurtoFactor( wave )
                       };
        }

        #endregion

        #region 判断数据类型

        /// <summary>
        /// 是否为2维时间波形数据
        /// </summary>
        /// <param name="trendData">TrendData</param>
        /// <returns>是否为2维时间波形数据</returns>
        public static bool IsTimeWave_2D( TrendData trendData )
        {
            return trendData != null && trendData is TimeWaveData_2D;
        }

        /// <summary>
        /// 是否为1维时间波形数据
        /// </summary>
        /// <param name="trendData">TrendData</param>
        /// <returns>是否为1维时间波形数据</returns>
        public static bool IsTimeWave_1D( TrendData trendData )
        {
            return trendData != null && trendData is TimeWaveData_1D;
        }

        /// <summary>
        /// 是否为时间波形数据
        /// </summary>
        /// <param name="trendData">TrendData</param>
        /// <returns>是否为时间波形数据</returns>
        public static bool IsTimeWave( TrendData trendData )
        {
            return trendData != null && trendData is WaveDataBase;
        }

        /// <summary>
        /// 是否为静态数据
        /// </summary>
        /// <param name="trendData">TrendData</param>
        /// <returns>是否为静态数据</returns>
        public static bool IsStaticData( TrendData trendData )
        {
            return trendData != null && !IsTimeWave( trendData );
        }

        #endregion

        #region 从字节数组读出采样数据对象

        /// <summary>
        /// 从字节数组读出采样数据对象
        /// </summary>
        /// <param name="bytes">字节数组</param>
        public static TrendData ReadFromBytes( byte[] bytes )
        {
            return null;
            //using( var stream = new MemoryStream( bytes ) )
            //{
            //    using( var reader = new BinaryReader( stream ) )
            //    {
            //        TrendData ret = CreateBySampleDataClassID( (SampleDataClassID)reader.ReadInt32() );
            //        if( ret != null )
            //        {
            //            ret.ReadFromBytes( reader );
            //        }

            //        return ret;
            //    }
            //}
        }

        /// <summary>
        /// 根据SampleDataClassID创建采样数据对象
        /// </summary>
        /// <param name="sampleDataClassID">SampleDataClassID</param>
        /// <returns>采样数据对象</returns>
        private static TrendData CreateBySampleDataClassID( SampleDataClassID sampleDataClassID )
        {
            switch( sampleDataClassID )
            {
                case SampleDataClassID.TrendData:
                    return new TrendData();

                case SampleDataClassID.TimeWaveData_1D:
                    return new TimeWaveData_1D();

                case SampleDataClassID.TimeWaveData_2D:
                    return new TimeWaveData_2D();
            }
            return null;
        }

        #endregion
    }
}