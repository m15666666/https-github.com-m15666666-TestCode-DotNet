using System;
using AnalysisAlgorithm;

namespace AnalysisUtils
{
    /// <summary>
    /// 格式值的实用工具
    /// </summary>
    public class FormatUtils
    {
        static FormatUtils()
        {
            MeasurementValueUnitPattern = "{0} {1}";
        }

        #region 格式化测量值、时间、频率、角度

        #region 与数值格式化相关的变量和属性

        /// <summary>
        /// 格式转速的字符串
        /// </summary>
        private const string RevFormat = "#";

        public const string FreqXValueFormat = "0.##";
        public const string TimeWaveXValueFormat = "0.#";
        private static readonly double[] LowThreshold = new double[] {0, 1};
        private static readonly double[] HighThreshold = new double[] {1, 10000};
        private static readonly string[] Formats = new[] {"0.####", "#.##"};

        /// <summary>
        /// 测量值和单位的格式化字符串
        /// </summary>
        public static string MeasurementValueUnitPattern { get; set; }

        #endregion

        /// <summary>
        /// 将测量值格式化为字符串
        /// </summary>
        /// <param name="value">测量值</param>
        /// <returns>字符串</returns>
        public static string MeasurementValue2String( double value )
        {
            string format = GetMeasurementValueFormat( value );
            return value.ToString( format );
        }

        /// <summary>
        /// 将测量值格式化为字符串
        /// </summary>
        /// <param name="value">测量值</param>
        /// <param name="unit">工程单位</param>
        /// <returns>字符串</returns>
        public static string MeasurementValue2String( double value, string unit )
        {
            return MeasurementValue2String( MeasurementValue2String( value ), unit );
        }

        /// <summary>
        /// 将测量值格式化为字符串
        /// </summary>
        /// <param name="value">测量值</param>
        /// <param name="unit">工程单位</param>
        /// <returns>字符串</returns>
        public static string MeasurementValue2String( string value, string unit )
        {
            return string.Format( MeasurementValueUnitPattern, value, unit );
        }

        /// <summary>
        /// 将测量值格式化为字符串
        /// </summary>
        /// <param name="value">测量值</param>
        /// <param name="isRevFormat">是否要求转速格式</param>
        /// <returns>字符串</returns>
        public static string MeasurementValue2String( double value, bool isRevFormat )
        {
            if( isRevFormat )
            {
                return MeasurementValue2String_Rev( value );
            }

            return MeasurementValue2String( value );
        }

        /// <summary>
        /// 将转速测量值格式化为字符串
        /// </summary>
        /// <param name="value">测量值</param>
        /// <returns>字符串</returns>
        public static string MeasurementValue2String_Rev( double value )
        {
            return value.ToString( RevFormat );
        }

        /// <summary>
        /// 将测量值格式化为字符串
        /// </summary>
        /// <param name="value">测量值</param>
        /// <param name="unit">工程单位</param>
        /// <param name="isRevFormat">是否要求转速格式</param>
        /// <returns>字符串</returns>
        public static string MeasurementValue2String( double value, string unit, bool isRevFormat )
        {
            return MeasurementValue2String( MeasurementValue2String( value, isRevFormat ), unit );
        }

        /// <summary>
        /// 将测量值格式化为字符串
        /// </summary>
        /// <param name="isFreqWave">是否是频谱</param>
        /// <param name="value">测量值</param>
        /// <returns>字符串</returns>
        public static string XValue2String( double value, bool isFreqWave )
        {
            return isFreqWave ? Freq2String( value ) : MSec2String( value );
        }

        /// <summary>
        /// 将频率格式化为字符串
        /// </summary>
        /// <param name="value">频率</param>
        /// <returns>字符串</returns>
        private static string Freq2String( double value )
        {
            return value.ToString( FreqXValueFormat );
        }

        /// <summary>
        /// 将毫秒格式化为字符串
        /// </summary>
        /// <param name="value">毫秒</param>
        /// <returns>字符串</returns>
        private static string MSec2String( double value )
        {
            return value.ToString( TimeWaveXValueFormat );
        }

        /// <summary>
        /// 将频率格式化为字符串
        /// </summary>
        /// <param name="value">频率</param>
        /// <param name="unit">工程单位</param>
        /// <returns>字符串</returns>
        public static string Freq2String( double value, string unit )
        {
            return MeasurementValue2String( Freq2String( value ), unit );
        }

        /// <summary>
        /// 将角度格式化为字符串
        /// </summary>
        /// <param name="angle">角度</param>
        /// <returns>字符串</returns>
        public static string Angle2String( double angle )
        {
            return angle.ToString( "#.#" ) + "°";
        }

        #endregion

        #region 特征值

        /// <summary>
        /// 获得波形的特征值，第一列是描述，第二列是特征值。
        /// </summary>
        /// <returns>波形的特征值</returns>
        public static string[,] GetWaveFeatureValue( double[] wave, string unit )
        {
            var ret = new[,]
                          {
                              {"平均幅值", null},
                              {"方根幅值", null},
                              {"均值", null},
                              {"有效值", null},
                              {"波形指标", null},
                              {"脉冲指标", null},
                              {"峰值指标", null},
                              {"裕度指标", null},
                              {"歪度", null},
                              {"峭度", null}
                          };

            int index = 0;

            // 平均幅值
            ret[index++, 1] = MeasurementValue2String( StatisticsUtils.AbsMean( wave ), unit );

            // 方根幅值
            ret[index++, 1] = MeasurementValue2String( StatisticsUtils.SMR( wave ), unit );

            // 均值
            ret[index++, 1] = MeasurementValue2String( StatisticsUtils.Mean( wave ), unit );

            // 有效值
            ret[index++, 1] = MeasurementValue2String( DSPBasic.RMS( wave ), unit );

            // 波形指标
            ret[index++, 1] = MeasurementValue2String( DSPBasic.ShapeFactor( wave ) );

            // 脉冲指标
            ret[index++, 1] = MeasurementValue2String( DSPBasic.ImpulseFactor( wave ) );

            // 峰值指标
            ret[index++, 1] = MeasurementValue2String( DSPBasic.CrestFactor( wave ) );

            // 裕度指标
            ret[index++, 1] = MeasurementValue2String( DSPBasic.ClearanceFactor( wave ) );

            // 歪度指标
            ret[index++, 1] = MeasurementValue2String( DSPBasic.SkewFactor( wave ) );

            // 峭度指标
            ret[index, 1] = MeasurementValue2String( DSPBasic.KurtoFactor( wave ) );

            return ret;
        }

        #endregion

        #region 频谱峰值

        /// <summary>
        /// 获得频谱的10个峰值列表，峰值从高到低排列。
        /// </summary>
        /// <param name="xData">频率</param>
        /// <param name="yData">幅值</param>
        /// <param name="xUnit">x轴的单位</param>
        /// <param name="yUnit">y轴的单位</param>
        /// <returns>频谱的峰值列表</returns>
        public static string[,] GetFreqPeaks( double[] xData, double[] yData, string xUnit, string yUnit )
        {
            const int FreqPeakCount = 10;
            const int HalfFreqPeakCount = FreqPeakCount / 2;
            string[,] freqPeaks = GetFreqPeaks( xData, yData, FreqPeakCount );
            if( freqPeaks == null )
            {
                return null;
            }

            int length = freqPeaks.GetLength( 0 );
            bool isTwoRow = ( length <= HalfFreqPeakCount );
            int columnCount = ( isTwoRow ? length : HalfFreqPeakCount ) + 1;
            int rowCount = isTwoRow ? 2 : 4;

            var ret = new string[rowCount,columnCount];
            int peakIndex = 0;
            for( int index = 0; index < rowCount; index += 2 )
            {
                int xIndex = index;
                int yIndex = xIndex + 1;

                ret[xIndex, 0] = xUnit;
                ret[yIndex, 0] = yUnit;

                for( int columnIndex = 1;
                     ( columnIndex < columnCount ) &&
                     ( peakIndex < length );
                     columnIndex++ )
                {
                    ret[xIndex, columnIndex] = freqPeaks[peakIndex, 0];
                    ret[yIndex, columnIndex] = freqPeaks[peakIndex, 1];
                    peakIndex++;
                }
            }

            return ret;
        }

        /// <summary>
        /// 获得频谱的峰值列表，峰值从高到低排列，第一列是频率，第二列是峰值。没有峰值返回null。
        /// </summary>
        /// <param name="xData">频率</param>
        /// <param name="yData">幅值</param>
        /// <param name="count">峰值的个数</param>
        /// <returns>频谱的峰值列表</returns>
        public static string[,] GetFreqPeaks( double[] xData, double[] yData, int count )
        {
            int[] peakIndexs = SpectrumBasic.MaxPeaksIndex( yData, false );
            if( peakIndexs == null )
            {
                return null;
            }

            int length = Math.Min( count, peakIndexs.Length );
            var ret = new string[length,2];
            for( int index = 0; index < length; index++ )
            {
                int peakIndex = peakIndexs[index];
                ret[index, 0] = Freq2String( xData[peakIndex] );
                ret[index, 1] = MeasurementValue2String( yData[peakIndex] );
            }
            return ret;
        }

        /// <summary>
        /// 获得频谱峰值对应的xData索引
        /// </summary>
        /// <param name="xData">频率</param>
        /// <param name="yData">赋值</param>
        /// <returns>频谱峰值对应的xData索引</returns>
        public static int[] GetFreqPeaksIndex( double[] xData, double[] yData )
        {
            const int FreqPeakCount = 10;
            int[] peakIndexs = SpectrumBasic.MaxPeaksIndex( yData, false );
            if( peakIndexs == null )
            {
                return null;
            }

            int length = Math.Min( FreqPeakCount, peakIndexs.Length );
            var ret = new int[length];
            for( int index = 0; index < length; index++ )
            {
                ret[index] = peakIndexs[index];
            }
            return ret;
        }

        #endregion

        #region 格式时间

        /// <summary>
        /// 将时间转化为标准的时间字符串
        /// 参考：ms-help://MS.VSCC.v90/MS.MSDNQTR.v90.chs/dv_fxfund/html/98b374e3-0cc2-4c78-ab44-efb671d71984.htm
        /// </summary>
        /// <param name="time">时间</param>
        /// <returns>标准的时间字符串</returns>
        public static string Time2String( DateTime time )
        {
            return time.ToString( "yyyy-MM-dd HH:mm:ss" );
        }

        /// <summary>
        /// 将时间转化为日期字符串
        /// </summary>
        public static string Time2DateString( DateTime time )
        {
            return time.ToString( "yyyy-MM-dd" );
        }

        /// <summary>
        /// 将时间转化为时间字符串
        /// </summary>
        public static string Time2TimeString( DateTime time )
        {
            return time.ToString( "HH:mm:ss" );
        }

        #endregion

        #region 获得格式串

        /// <summary>
        /// 获取格式串
        /// </summary>
        /// <param name="value">测量值</param>
        /// <returns>格式串</returns>
        public static string GetMeasurementValueFormat( double value )
        {
            double abs = Math.Abs( value );
            if( abs <= 1.0E-10 )
            {
                const string Zero = "0";
                return Zero;
            }

            const string ExponentFormat = "0.#E+0";
            string format = ExponentFormat;

            for( int index = 0; index < Formats.Length; index++ )
            {
                if( ( LowThreshold[index] <= abs ) &&
                    ( abs < HighThreshold[index] ) )
                {
                    format = Formats[index];
                    break;
                }
            }

            return format;
        }

        /// <summary>
        /// 获取格式串
        /// </summary>
        /// <param name="value">测量值</param>
        /// <param name="userRevFormat">是否使用转速格式</param>
        /// <returns>格式串</returns>
        public static string GetMeasurementValueFormat( double value, bool userRevFormat )
        {
            if( userRevFormat )
            {
                return null;
            }
            return GetMeasurementValueFormat( value );
        }

        #endregion

        ///// <summary>
        ///// 获得频谱的峰值列表，峰值从高到低排列，第一列是频率，第二列是峰值。没有峰值返回null。
        ///// </summary>
        ///// <param name="xData">频率</param>
        ///// <param name="yData">幅值</param>
        ///// <param name="count">峰值的个数</param>
        ///// <returns>频谱的峰值列表</returns>
        //public static string[,] GetFreqPeaks(double[] xData, double[] yData, int count)
        //{
        //    int[] peakIndexs = SpectrumBasic.MaxPeaksIndex( yData, false );
        //    if( peakIndexs == null )
        //    {
        //        return null;
        //    }

        //    int length = Math.Min( count, peakIndexs.Length );
        //    var ret = new string[length,2];
        //    for( int index = 0; index < length; index++ )
        //    {
        //        int peakIndex = peakIndexs[index];
        //        ret[index, 0] = MeasurementValue2String( xData[peakIndex] );
        //        ret[index, 1] = MeasurementValue2String( yData[peakIndex] );
        //    }
        //    return ret;
        //}
    }
}