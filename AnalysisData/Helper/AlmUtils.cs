using AnalysisData.Constants;
using AnalysisData.SampleData;

namespace AnalysisData.Helper
{
    /// <summary>
    /// 报警的实用工具类
    /// </summary>
    public static class AlmUtils
    {
        /// <summary>
        /// 计算普通报警的等级
        /// </summary>
        /// <param name="value">输入值</param>
        /// <param name="setting">AlmStand_CommonSettingData</param>
        /// <returns>普通报警的等级</returns>
        public static int CalcCommonAlmLevel( double value, AlmStand_CommonSettingData setting )
        {
            if( setting == null )
            {
                return AlmLevelID.Level_Normal;
            }

            // 只保留level4和level2
            int[] almLevels = new[] { AlmLevelID.AlmLevelIDs_Desc[2], AlmLevelID.AlmLevelIDs_Desc[3]};

            var highThresholds = new[]
                                     {
                                         setting.HighLimit2_NR, setting.HighLimit1_NR
                                     };

            var lowThresholds = new[]
                                    {
                                        setting.LowLimit2_NR, setting.LowLimit1_NR
                                    };

            switch( setting.AlmType_ID )
            {
                case AlmTypeID.High:
                    {
                        int levelIndex = 0;
                        foreach( var threshold in highThresholds )
                        {
                            if( threshold.HasValue && threshold.Value <= value )
                            {
                                return almLevels[levelIndex];
                            }
                            levelIndex++;
                        }
                    }
                    break;

                case AlmTypeID.Low:
                    {
                        int levelIndex = 0;
                        foreach( var threshold in lowThresholds )
                        {
                            if( threshold.HasValue && value <= threshold.Value )
                            {
                                return almLevels[levelIndex];
                            }
                            levelIndex++;
                        }
                    }
                    break;

                case AlmTypeID.InWindow:
                    {
                        for( int index = 0; index < lowThresholds.Length; index++ )
                        {
                            double? high = highThresholds[index];
                            double? low = lowThresholds[index];

                            if( low.HasValue && high.HasValue && low.Value <= value && value <= high.Value )
                            {
                                return almLevels[index];
                            }
                        }
                    }
                    break;

                case AlmTypeID.OutWindow:
                    {
                        for( int index = 0; index < lowThresholds.Length; index++ )
                        {
                            double? high = highThresholds[index];
                            double? low = lowThresholds[index];

                            if( low.HasValue && high.HasValue && ( value <= low.Value || high.Value <= value ) )
                            {
                                return almLevels[index];
                            }
                        }
                    }
                    break;
            }

            return AlmLevelID.Level_Normal;
        }

        /// <summary>
        /// 获得门限值的描述
        /// </summary>
        /// <param name="setting">AlmStand_CommonSettingData</param>
        /// <returns>门限值的描述</returns>
        public static string GetThresholdDescription( AlmStand_CommonSettingData setting )
        {
            string threshold = AlmTypeID.GetNameByID( setting.AlmType_ID );

            if( setting.HighLimit1_NR != null )
            {
                threshold += ",预警的上限值：" + GetThresholdValueString( setting.HighLimit1_NR );
            }
            if( setting.LowLimit1_NR != null )
            {
                threshold += ",预警的下限值：" + GetThresholdValueString( setting.LowLimit1_NR );
            }
            if( setting.HighLimit2_NR != null )
            {
                threshold += ",警告的上限值：" + GetThresholdValueString( setting.HighLimit2_NR );
            }
            if( setting.LowLimit2_NR != null )
            {
                threshold += ",警告的下限值：" + GetThresholdValueString( setting.LowLimit2_NR );
            }

            return threshold;
        }

        /// <summary>
        /// 获得门限值的字符串
        /// </summary>
        /// <param name="threshold">门限值</param>
        /// <returns>门限值的字符串</returns>
        public static string GetThresholdValueString( double? threshold )
        {
            return threshold.Value.ToString( "0.####" );
        }
    }
}