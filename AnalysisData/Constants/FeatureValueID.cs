using AnalysisData.SampleData;

namespace AnalysisData.Constants
{
    /// <summary>
    /// 特征值编号
    /// </summary>
    public static class FeatureValueID
    {
        /// <summary>
        /// 测量值
        /// </summary>
        public const int Measurement = 10;

        /// <summary>
        /// 峰值
        /// </summary>
        public const int P = 11;

        /// <summary>
        /// 峰峰值
        /// </summary>
        public const int PP = 12;

        /// <summary>
        /// 有效值
        /// </summary>
        public const int RMS = 13;

        /// <summary>
        /// 总值
        /// </summary>
        public const int Overall = 14;

        /// <summary>
        /// 均值
        /// </summary>
        public const int Mean = 15;

        /// <summary>
        /// 波形指标
        /// </summary>
        public const int ShapeFactor = 16;

        /// <summary>
        /// 脉冲指标
        /// </summary>
        public const int ImpulseFactor = 17;

        /// <summary>
        /// 歪度指标
        /// </summary>
        public const int SkewFactor = 18;

        /// <summary>
        /// 峭度指标
        /// </summary>
        public const int KurtoFactor = 19;

        /// <summary>
        /// 峰值指标
        /// </summary>
        public const int CrestFactor = 29;

        /// <summary>
        /// 裕度指标
        /// </summary>
        public const int ClearanceFactor = 30;

        /// <summary>
        /// 轴位移，用于使用电涡流传感器时显示轴心轨迹
        /// </summary>
        public const int AxisOffset = 40;

        #region 启停机常量

        /// <summary>
        /// 工频双边谱实部
        /// </summary>
        public const int DualSpectrum1X_RealPart = 201;

        /// <summary>
        /// 工频双边谱虚部
        /// </summary>
        public const int DualSpectrum1X_ImaginaryPart = 202;

        #endregion

        /// <summary>
        /// 特征值编号集合
        /// </summary>
        private static readonly byte[] _featureValueIDs = new byte[]
                                                              {
                                                                  Measurement, P,
                                                                  PP,
                                                                  RMS,
                                                                  Mean, ShapeFactor,
                                                                  //ImpulseFactor,
                                                                  //CrestFactor,
                                                                  //ClearanceFactor,
                                                                  //SkewFactor, 
                                                                  KurtoFactor,
                                                              };

        #region 异常代码常量

        #region 趋势报警

        /// <summary>
        /// 月趋势报警
        /// </summary>
        public const int Alm_Trend_30Day = 101;
        /// <summary>
        /// 10天趋势报警
        /// </summary>
        public const int Alm_Trend_10Day = 102;
        /// <summary>
        /// 5天趋势报警
        /// </summary>
        public const int Alm_Trend_5Day = 103;
        /// <summary>
        /// 24小时趋势报警
        /// </summary>
        public const int Alm_Trend_24Hour = 104;
        /// <summary>
        /// 2小时趋势报警
        /// </summary>
        public const int Alm_Trend_2Hour = 105;
        /// <summary>
        /// 瞬时趋势报警
        /// </summary>
        public const int Alm_Trend_Impulse = 111;

        #endregion

        #region 用于无线传感器的常量

        /// <summary>
        /// 传感器失效，只有二级报警
        /// </summary>
        public const int Error_Transducer = 10001;

        /// <summary>
        /// 传感器失效-温度，只有二级报警
        /// </summary>
        public const int Error_Transducer_Temperature = 10002;

        /// <summary>
        /// 传感器失效-振动，只有二级报警
        /// </summary>
        public const int Error_Transducer_Vib = 10003;

        /// <summary>
        ///     传感器电池电量低
        /// </summary>
        public const int Error_TransducerBatteryLow = 10011;

        #endregion

        /// <summary>
        ///     采集仪未知异常
        /// </summary>
        public const int Error_SamplerUnknown = 10100;

        /// <summary>
        ///     通讯板与采集板485通讯异常
        /// </summary>
        public const int Error_SampleBoard485Commumication = 10101;

        /// <summary>
        ///     采集板卡种类数量及总数与通讯板拨码开关不符合
        /// </summary>
        public const int Error_SampleBoardConfig = 10102;

        /// <summary>
        ///     采样值超量程
        /// </summary>
        public const int Error_MeasurementValueOutOfRange = 10105;

        #endregion

        /// <summary>
        /// 特征值编号集合
        /// </summary>
        public static byte[] FeatureValueIDs
        {
            get { return _featureValueIDs; }
        }

        /// <summary>
        /// 根据信号类型编号获得测量值
        /// </summary>
        /// <param name="featureData">FeatureData</param>
        /// <param name="signalTypeId">信号类型编号</param>
        /// <returns>测量值</returns>
        public static double GetMeasurementValueBySignalTypeID( FeatureData featureData, int signalTypeId )
        {
            switch( signalTypeId )
            {
                case SignalTypeID.Velocity:
                    return featureData.RMS;

                case SignalTypeID.Displacement:
                    return featureData.PP;

                default:
                case SignalTypeID.Acceleration:
                    return featureData.P;
            }
        }

        /// <summary>
        /// 根据信号类型编号和测量值对应的指标ID获得测量值
        /// </summary>
        /// <param name="featureData">FeatureData</param>
        /// <param name="signalTypeId">信号类型编号</param>
        /// <param name="measurementValueFeatureValueId">测量值对应的指标ID</param>
        /// <returns>测量值</returns>
        public static double GetMeasurementValueByMeasurementValueFeatureValueID( FeatureData featureData,
                                                                                  int signalTypeId,
                                                                                  int measurementValueFeatureValueId )
        {
            switch( measurementValueFeatureValueId )
            {
                case P:
                    return featureData.P;

                case RMS:
                    return featureData.RMS;

                case PP:
                    return featureData.PP;

                case Mean:
                    return featureData.Mean;

                case ShapeFactor:
                    return featureData.ShapeFactor;

                case ImpulseFactor:
                    return featureData.ImpulseFactor;

                case CrestFactor:
                    return featureData.CrestFactor;

                case ClearanceFactor:
                    return featureData.ClearanceFactor;

                case SkewFactor:
                    return featureData.SkewFactor;

                case KurtoFactor:
                    return featureData.KurtoFactor;

                case AxisOffset:
                    return featureData.AxisOffset;

                default:
                case Measurement:
                    return GetMeasurementValueBySignalTypeID( featureData, signalTypeId );
            }
        }

        /// <summary>
        /// 根据指标ID获得单位幂次编号
        /// </summary>
        /// <param name="featureValueId">指标ID</param>
        /// <returns>单位幂次编号</returns>
        public static int GetUnitPowerIDByFeatureValueID( int featureValueId )
        {
            switch( featureValueId )
            {
                case P:
                case PP:
                case RMS:
                case Mean:
                case Measurement:
                case AxisOffset:
                default:
                    return UnitPowerID.Power1;

                case ShapeFactor:
                    //case ImpulseFactor:
                    //case CrestFactor:
                    //case ClearanceFactor:
                    //case SkewFactor:
                case KurtoFactor:
                    return UnitPowerID.Power0;
            }
        }

        /// <summary>
        /// 根据特征值编号获取特征值
        /// </summary>
        /// <param name="featureValueId">特征参数编号</param>
        /// <returns>特征参数名称</returns>
        public static string GetName( int featureValueId )
        {
            switch( featureValueId )
            {
                case Measurement:
                    return FeatureValueName.Measurement;
                case P:
                    return FeatureValueName.P;
                case PP:
                    return FeatureValueName.PP;
                case RMS:
                    return FeatureValueName.RMS;
                case Mean:
                    return FeatureValueName.Mean;
                case ShapeFactor:
                    return FeatureValueName.ShapeFactor;
                    //case ImpulseFactor:
                    //    return FeatureValueName.ImpulseFactor;
                    //case CrestFactor:
                    //    return FeatureValueName.CrestFactor;
                    //case ClearanceFactor:
                    //    return FeatureValueName.ClearanceFactor;
                    //case SkewFactor:
                    //    return FeatureValueName.SkewFactor;
                case KurtoFactor:
                    return FeatureValueName.KurtoFactor;
                case AxisOffset:
                    return FeatureValueName.AxisOffset;

                case DualSpectrum1X_RealPart:
                    return FeatureValueName.DualSpectrum1X_RealPart;

                case DualSpectrum1X_ImaginaryPart:
                    return FeatureValueName.DualSpectrum1X_ImaginaryPart;

                case Error_Transducer:
                    return "传感器故障";

                case Error_Transducer_Temperature:
                    return "温度传感器故障";

                case Error_Transducer_Vib:
                    return "振动传感器故障";

                case Error_TransducerBatteryLow:
                    return "传感器电池电量低";

                case Error_SamplerUnknown:
                    return "采集仪未知异常";

                case Error_SampleBoard485Commumication:
                    return "通讯板与采集板485通讯异常";

                case Error_SampleBoardConfig:
                    return "采集板卡种类数量及总数与通讯板拨码开关不符合";

                case Error_MeasurementValueOutOfRange:
                    return "采样值超量程";

                default:
                    return string.Empty;
            }
        }
    }

    /// <summary>
    /// 特征值名称
    /// </summary>
    public static class FeatureValueName
    {
        /// <summary>
        /// 测量值
        /// </summary>
        public const string Measurement = "测量值";

        /// <summary>
        /// 峰值
        /// </summary>
        public const string P = "零峰值";

        /// <summary>
        /// 峰峰值
        /// </summary>
        public const string PP = "峰峰值";

        /// <summary>
        /// 有效值
        /// </summary>
        public const string RMS = "有效值";

        /// <summary>
        /// 总值
        /// </summary>
        public const string Overall = "总值";

        /// <summary>
        /// 均值
        /// </summary>
        public const string Mean = "均值";

        /// <summary>
        /// 波形指标
        /// </summary>
        public const string ShapeFactor = "波形指标";

        /// <summary>
        /// 脉冲指标
        /// </summary>
        public const string ImpulseFactor = "脉冲指标";

        /// <summary>
        /// 歪度指标
        /// </summary>
        public const string SkewFactor = "歪度";

        /// <summary>
        /// 峭度指标
        /// </summary>
        public const string KurtoFactor = "峭度";

        /// <summary>
        /// 峰值指标
        /// </summary>
        public const string CrestFactor = "峰值指标";

        /// <summary>
        /// 裕度指标
        /// </summary>
        public const string ClearanceFactor = "裕度指标";

        /// <summary>
        /// 轴位移
        /// </summary>
        public const string AxisOffset = "轴位移";

        #region 启停机常量

        /// <summary>
        /// 工频双边谱实部
        /// </summary>
        public const string DualSpectrum1X_RealPart = "工频双边谱实部";

        /// <summary>
        /// 工频双边谱虚部
        /// </summary>
        public const string DualSpectrum1X_ImaginaryPart = "工频双边谱虚部";

        #endregion
    }
}