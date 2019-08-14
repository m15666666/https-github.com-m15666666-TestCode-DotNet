//using System;
//using System.Collections.Generic;
//using System.Linq;
//using AnalysisData.Constants;
//using AnalysisData.SampleData;
//using AnalysisUtils;
//using Common.Framework;
//using CommonZX.Framework;
//using Model.CachedData;
//using ModelZX;
//using Moons.Common20;
//using Moons.Common20.DataBase;
//using ObjectIDFactory = CommonZX.Framework.ObjectIDFactory;

//namespace SampleServer.Upload2DB
//{
//    /// <summary>
//    /// 上传数据库实用工具类
//    /// </summary>
//    public class UploadHelper
//    {
//        #region 变量和属性

//        private readonly List<List<ZX_History_FeatureValue>> _featureValuesList =
//            new List<List<ZX_History_FeatureValue>>();

//        private readonly List<ZX_History_Summary> _summaryList = new List<ZX_History_Summary>();
//        private readonly List<TrendData> _trendDatas = new List<TrendData>();
//        private readonly List<ZX_History_Waveform> _waveform1List = new List<ZX_History_Waveform>();
//        private readonly List<ZX_History_Waveform> _waveform2List = new List<ZX_History_Waveform>();

//        public UploaderBase Uploader { private get; set; }

//        public ZXEntitiesWrap EntitiesWrap { private get; set; }

//        /// <summary>
//        /// ZX_History_Summary集合
//        /// </summary>
//        public List<ZX_History_Summary> SummaryList
//        {
//            get { return _summaryList; }
//        }

//        /// <summary>
//        /// 第一通道ZX_History_Waveform集合
//        /// </summary>
//        public List<ZX_History_Waveform> Waveform1List
//        {
//            get { return _waveform1List; }
//        }

//        /// <summary>
//        /// 第二通道ZX_History_Waveform集合
//        /// </summary>
//        public List<ZX_History_Waveform> Waveform2List
//        {
//            get { return _waveform2List; }
//        }

//        /// <summary>
//        /// List[ZX_History_FeatureValue]集合
//        /// </summary>
//        public List<List<ZX_History_FeatureValue>> FeatureValuesList
//        {
//            get { return _featureValuesList; }
//        }

//        /// <summary>
//        /// TrendData集合
//        /// </summary>
//        public List<TrendData> TrendDatas
//        {
//            get { return _trendDatas; }
//        }

//        /// <summary>
//        /// 是否有数据需要保存
//        /// </summary>
//        public bool HasData2Save
//        {
//            get { return 0 < _summaryList.Count; }
//        }

//        #endregion

//        #region 自定义方法

//        /// <summary>
//        /// 获取Summary
//        /// </summary>
//        /// <param name="trendData">TrendData</param>
//        /// <param name="pointInfo">PointInfoData</param>
//        /// <returns>ZX_History_Summary</returns>
//        private ZX_History_Summary GetSummary( TrendData trendData, out PointInfoData pointInfo )
//        {
//            try
//            {
//                // 读取测点信息
//                int pointID = trendData.PointID;
//                pointInfo = Uploader.GetPointInfo( pointID );

//                // 测点信息不存在，跳过这条数据
//                if( pointInfo == null )
//                {
//                    TraceUtils.Info( string.Format( "测点({0})信息不存在", pointID ) );
//                    return null;
//                }

//                return new ZX_History_Summary
//                           {
//                               PntDim_NR = pointInfo.PntDim_NR,
//                               DatType_NR = pointInfo.DatType_NR,
//                               SigType_NR = pointInfo.SigType_NR,
//                               Point_ID = pointID,
//                               SampTime_DT = trendData.SampleTime,
//                               SampTimeGMT_DT = trendData.SampleTime.ToUniversalTime(),
//                               DatLen_NR = 0,
//                               RotSpeed_NR = 0,
//                               MinFreq_NR = 0,
//                               SampleFreq_NR = 0,
//                               SampMod_NR = 0,
//                               AlmLevel_ID = trendData.AlmLevelID,
//                               DataGroup_NR = 0,
//                               Alm_ID = trendData.AlmID,
//                               Synch_NR = trendData.SyncID,
//                               CompressType_ID = Config.CompressTypeID,
//                               Compress_ID = trendData.AlmID == 0 ? CompressID.UnCompressed : CompressID.AlmEventData,
//                           };
//            }
//            catch( Exception ex )
//            {
//                TraceUtils.Error( string.Format( "测点({0})获取GetSummary出错", trendData.PointID ), ex );
//                throw;
//            }
//        }

//        /// <summary>
//        /// 创建ZX_History_Waveform对象
//        /// </summary>
//        /// <param name="channelNumber">通道号</param>
//        /// <param name="wave">波形</param>
//        /// <param name="summary">ZX_History_Summary</param>
//        /// <returns>ZX_History_Waveform对象</returns>
//        private static ZX_History_Waveform CreateWaveForm( byte channelNumber, double[] wave, ZX_History_Summary summary )
//        {
//            CompressionType compressionType;
//            double waveScale;
//            byte[] bytes = AnalysisDataConvertUtils.DoubleToByte( wave, out waveScale, out compressionType );

//            return new ZX_History_Waveform
//                       {
//                           ChnNo_NR = channelNumber,
//                           WaveformType_ID = WaveformTypeID.Normal,
//                           SigType_NR = summary.SigType_NR,
//                           DatLen_NR = wave.Length,
//                           RotSpeed_NR = summary.RotSpeed_NR,
//                           MinFreq_NR = summary.MinFreq_NR,
//                           SampleFreq_NR = summary.SampleFreq_NR,
//                           SampMod_NR = summary.SampMod_NR,
//                           EngUnit_ID = summary.EngUnit_ID,
//                           Demod_YN = DBConventions.FalseOfZeroString,
//                           DMinFreq_NR = 0,
//                           DMaxFreq_NR = 0,
//                           Wave_GR = bytes,
//                           WaveScale_NR = waveScale,
//                           Compress_NR = (byte)compressionType
//                       };
//        }

//        /// <summary>
//        /// 转换特征值
//        /// </summary>
//        /// <param name="featureValueList">输出</param>
//        /// <param name="featureData">特征指标</param>
//        /// <param name="chnNo">通道号</param>
//        /// <param name="pointInfo">PointInfoData</param>
//        /// <param name="summary">ZX_History_Summary</param>
//        private static void GetHistoryFeatureValue( ICollection<ZX_History_FeatureValue> featureValueList,
//                                                    FeatureData featureData, byte chnNo,
//                                                    PointInfoData pointInfo, ZX_History_Summary summary )
//        {
//            byte[] featureValueIDs;
//            double[] featureValues;
//            UploaderBase.GetFeatureValues( featureData, pointInfo, out featureValueIDs, out featureValues );

//            for( int index = 0; index < featureValueIDs.Length; index++ )
//            {
//                featureValueList.Add(
//                    new ZX_History_FeatureValue
//                        {
//                            ChnNo_NR = chnNo,
//                            FeatureValueType_ID = FeatureValueTypeID.Normal,
//                            FeatureValue_ID = featureValueIDs[index],
//                            FeatureValue_NR = featureValues[index],
//                            SigType_NR = summary.SigType_NR,
//                            EngUnit_ID = summary.EngUnit_ID
//                        }
//                    );
//            }
//        }

//        #endregion

//        /// <summary>
//        /// 插入数据
//        /// </summary>
//        public void InsertDatas()
//        {
//            if( !HasData2Save )
//            {
//                return;
//            }

//            int dataCount = _summaryList.Count;

//            // 获得第一个历史数据编号
//            long firstMornitorID = ObjectIDFactory.CreateMornitorID( dataCount );

//            // 计算存在多少个时间波形
//            int waveCount = _waveform1List.Count( item => item != null );
//            waveCount += _waveform2List.Count( item => item != null );

//            // 获得第一个时间波形编号
//            long waveformID = ObjectIDFactory.CreateWaveformID( waveCount );

//            // 计算存在多少个特征值
//            int featureValueCount = _featureValuesList.Sum( featureValues => featureValues.Count( item => item != null ) );

//            // 获得第一个特征值编号
//            long featureValuePKID = ObjectIDFactory.CreateFeatureValuePKID( featureValueCount );

//            for( int index = 0; index < dataCount; index++ )
//            {
//                ZX_History_Summary summary = _summaryList[index];
//                ZX_History_Waveform waveform1 = _waveform1List[index];
//                ZX_History_Waveform waveform2 = _waveform2List[index];
//                List<ZX_History_FeatureValue> featurValues = _featureValuesList[index];

//                long historyID = summary.History_ID = firstMornitorID + index;

//                long partitionID = PartitionIDUtils.Time2LongID( summary.SampTime_DT );

//                summary.Partition_ID = partitionID;

//                foreach( ZX_History_FeatureValue featurValue in featurValues )
//                {
//                    featurValue.Partition_ID = partitionID;
//                    featurValue.FeatureValuePK_ID = featureValuePKID++;
//                    featurValue.History_ID = historyID;

//                    summary.ZX_History_FeatureValue.Add( featurValue );
//                }

//                if( waveform1 != null )
//                {
//                    waveform1.Partition_ID = partitionID;
//                    waveform1.Waveform_ID = waveformID++;

//                    summary.ZX_History_Waveform.Add( waveform1 );

//                    if( waveform2 != null )
//                    {
//                        waveform2.Partition_ID = partitionID;
//                        waveform2.Waveform_ID = waveformID++;

//                        summary.ZX_History_Waveform.Add( waveform2 );
//                    }
//                }

//                EntitiesWrap.AddToZX_History_Summary( summary );
//            } // for( int index = 0; index < dataCount; index++ )
//        }

//        /// <summary>
//        /// 初始化TrendData
//        /// </summary>
//        /// <param name="trendData">TrendData</param>
//        public void InitData( TrendData trendData )
//        {
//            InitDatas( new List<TrendData> { trendData } );
//        }

//        /// <summary>
//        /// 初始化TrendData集合
//        /// </summary>
//        /// <param name="trendDatas">TrendData集合</param>
//        public void InitDatas( List<TrendData> trendDatas )
//        {
//            foreach( TrendData trendData in trendDatas )
//            {
//                PointInfoData pointInfo;
//                ZX_History_Summary summary = GetSummary( trendData, out pointInfo );
//                if( summary == null )
//                {
//                    continue;
//                }

//                // 数据类型是字符串类型
//                bool isTextValue = summary.DatType_NR == DataTypeID.TextValue;

//                if( !isTextValue )
//                {
//                    // 根据单位调整数据
//                    int defaultEngUnitID;
//                    AdjustDataByEngUnit( pointInfo.EngUnit_ID, trendData, out defaultEngUnitID );
//                    summary.EngUnit_ID = defaultEngUnitID;
//                }

//                _trendDatas.Add( trendData );

//                if( trendData is WaveDataBase )
//                {
//                    var waveData = trendData as WaveDataBase;

//                    summary.DatLen_NR = waveData.DataLength;
//                    summary.RotSpeed_NR = waveData.Rev;
//                    summary.SampleFreq_NR = waveData.SampleFreq;
//                    summary.SampMod_NR = (byte)waveData.MultiFreq;
//                }

//                ZX_History_Waveform waveform1 = null;
//                ZX_History_Waveform waveform2 = null;
//                var featureValues = new List<ZX_History_FeatureValue>();

//                string result;
//                // 二维时间波形数据
//                if( trendData is TimeWaveData_2D )
//                {
//                    var timeWaveData = trendData as TimeWaveData_2D;

//                    //通道1波形
//                    waveform1 = CreateWaveForm( ChannelNumber.One, timeWaveData.Wave_1D, summary );

//                    //通道2波形
//                    waveform2 = CreateWaveForm( ChannelNumber.Two, timeWaveData.Wave_2D, summary );

//                    result = string.Format( "{0},{1}",
//                                            FormatUtils.MeasurementValue2String( timeWaveData.MeasurementValue ),
//                                            FormatUtils.MeasurementValue2String( timeWaveData.MeasurementValue2 ) );

//                    if( timeWaveData.Feature_1D == null )
//                    {
//                        timeWaveData.Feature_1D = SampleDataUtils.CreateFeatureData( timeWaveData.Wave_1D );
//                    }
//                    if( timeWaveData.Feature_2D == null )
//                    {
//                        timeWaveData.Feature_2D = SampleDataUtils.CreateFeatureData( timeWaveData.Wave_2D );
//                    }

//                    timeWaveData.Feature_1D.MeasurementValue = timeWaveData.MeasurementValue;
//                    timeWaveData.Feature_2D.MeasurementValue = timeWaveData.MeasurementValue2;

//                    //特征值
//                    GetHistoryFeatureValue( featureValues, timeWaveData.Feature_1D, ChannelNumber.One,
//                                            pointInfo, summary );
//                    GetHistoryFeatureValue( featureValues, timeWaveData.Feature_2D, ChannelNumber.Two,
//                                            pointInfo, summary );
//                }
//                else if( trendData is TimeWaveData_1D ) // 一维时间波形数据
//                {
//                    var timeWaveData = trendData as TimeWaveData_1D;

//                    waveform1 = CreateWaveForm( ChannelNumber.One, timeWaveData.Wave, summary );

//                    result = FormatUtils.MeasurementValue2String( timeWaveData.MeasurementValue );

//                    if( timeWaveData.Feature == null )
//                    {
//                        timeWaveData.Feature = SampleDataUtils.CreateFeatureData( timeWaveData.Wave );
//                    }

//                    timeWaveData.Feature.MeasurementValue = timeWaveData.MeasurementValue;

//                    GetHistoryFeatureValue( featureValues, timeWaveData.Feature, ChannelNumber.One,
//                                            pointInfo, summary );
//                }
//                else if( isTextValue )
//                {
//                    result = trendData.MeasurementValueString4Opc;
//                }
//                else
//                {
//                    result = FormatUtils.MeasurementValue2String( trendData.MeasurementValue );

//                    featureValues.Add(
//                        new ZX_History_FeatureValue
//                            {
//                                ChnNo_NR = ChannelNumber.One,
//                                FeatureValueType_ID = FeatureValueTypeID.Normal,
//                                FeatureValue_ID = FeatureValueID.Measurement,
//                                FeatureValue_NR = trendData.MeasurementValue,
//                                SigType_NR = summary.SigType_NR,
//                                EngUnit_ID = summary.EngUnit_ID
//                            }
//                        );

//                    // 加入额外的特征值，目前只有无线传感器的、非波形的历史数据才携带额外的特征值
//                    if( CollectionUtils.IsNotEmptyG( trendData.AdditionalFeatureID2Values ) )
//                    {
//                        featureValues.AddRange(
//                            trendData.AdditionalFeatureID2Values.Select( pair => new ZX_History_FeatureValue
//                            {
//                                ChnNo_NR = ChannelNumber.One,
//                                FeatureValueType_ID = FeatureValueTypeID.Normal,
//                                FeatureValue_ID = pair.Key,
//                                FeatureValue_NR = pair.Value,
//                                SigType_NR = summary.SigType_NR,
//                                EngUnit_ID = summary.EngUnit_ID
//                            } ) );
//                    }
//                }

//                // 如果有轴位移，则保存到特征值表里，目前因为只可能有一维测点的数据，所以只在此处写一次
//                // 轴位移只有位移测点才有，因此此处直接使用信号类型和单位，因为在下位机中如果不是位移测点，则不会上传轴位移值
//                if( trendData.HasAxisOffsetValue )
//                {
//                    featureValues.Add(
//                        new ZX_History_FeatureValue
//                            {
//                                ChnNo_NR = ChannelNumber.One,
//                                FeatureValueType_ID = FeatureValueTypeID.Normal,
//                                FeatureValue_ID = FeatureValueID.AxisOffset,
//                                FeatureValue_NR = trendData.AxisOffsetValue,
//                                SigType_NR = summary.SigType_NR,
//                                EngUnit_ID = summary.EngUnit_ID
//                            }
//                        );
//                }

//                summary.Result_TX = result;

//                _summaryList.Add( summary );
//                _waveform1List.Add( waveform1 );
//                _waveform2List.Add( waveform2 );
//                _featureValuesList.Add( featureValues );
//            } // foreach( TrendData trendData in trendDatas )
//        }

//        /// <summary>
//        /// 根据单位调整数据
//        /// </summary>
//        /// <param name="engUnitID">单位ID</param>
//        /// <param name="trendData">数据</param>
//        /// <param name="defaultEngUnitID">输出，默认的单位ID</param>
//        private static void AdjustDataByEngUnit( int engUnitID, TrendData trendData, out int defaultEngUnitID )
//        {
//            EngUnitData engUnit, defaultEngUnit;
//            ValueTransformUtils.GetEngUnitsByID( engUnitID, out engUnit, out defaultEngUnit );
//            defaultEngUnitID = defaultEngUnit.EngUnit_ID;

//            // 调整轴位移的单位
//            if( trendData.HasAxisOffsetValue )
//            {
//                trendData.AxisOffsetValue =
//                    (float)
//                    ValueTransformUtils.TransformFeatureValue( engUnit, defaultEngUnit, FeatureValueID.AxisOffset,
//                                                               trendData.AxisOffsetValue );
//            }

//            // 调整额外的特征值的单位（加入额外的特征值，目前只有无线传感器的、非波形的历史数据才携带额外的特征值）
//            var additionalFeatureId2Values = trendData.AdditionalFeatureID2Values;
//            if( CollectionUtils.IsNotEmptyG( additionalFeatureId2Values ) )
//            {
//                trendData.AdditionalFeatureID2Values = additionalFeatureId2Values.ToDictionary( pair => pair.Key,
//                    pair => ValueTransformUtils.TransformFeatureValue( engUnit, defaultEngUnit, pair.Key, pair.Value ) );
//            }

//            // 第一个测量值
//            trendData.MeasurementValue =
//                (float)ValueTransformUtils.TransformFeatureValue( engUnit, defaultEngUnit, FeatureValueID.Measurement,
//                                                                  trendData.MeasurementValue );
//            // 二维时间波形数据
//            if( trendData is TimeWaveData_2D )
//            {
//                var timeWaveData = trendData as TimeWaveData_2D;

//                ValueTransformUtils.TransformValues( engUnit, defaultEngUnit, timeWaveData.Wave_1D );
//                ValueTransformUtils.TransformValues( engUnit, defaultEngUnit, timeWaveData.Wave_2D );

//                // 第二个测量值
//                timeWaveData.MeasurementValue2 =
//                    (float)
//                    ValueTransformUtils.TransformFeatureValue( engUnit, defaultEngUnit, FeatureValueID.Measurement,
//                                                               timeWaveData.MeasurementValue2 );
//            }
//            else if( trendData is TimeWaveData_1D ) // 一维时间波形数据
//            {
//                var timeWaveData = trendData as TimeWaveData_1D;

//                ValueTransformUtils.TransformValues( engUnit, defaultEngUnit, timeWaveData.Wave );
//            }
//        }

//        /// <summary>
//        /// 从集合中删除指定下标的数据，从高的下标开始删除
//        /// </summary>
//        /// <param name="index4Remove">指定下标集合</param>
//        public void RemoveIndices( List<int> index4Remove )
//        {
//            index4Remove.Sort();
//            index4Remove.Reverse();

//            foreach( int index in index4Remove )
//            {
//                _summaryList.RemoveAt( index );
//                _waveform1List.RemoveAt( index );
//                _waveform2List.RemoveAt( index );
//                _featureValuesList.RemoveAt( index );
//            }
//        }
//    }
//}