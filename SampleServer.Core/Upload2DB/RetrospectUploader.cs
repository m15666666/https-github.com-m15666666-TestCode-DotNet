using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;


using Moons.Common20;
using Moons.Common20.LongFile;

namespace SampleServer.Upload2DB
{
    /// <summary>
    /// 追忆数据上传数据库类
    /// </summary>
    //public class RetrospectUploader : UploaderBase
    //{
    //    private DbConnection _dbConnection;

    //    /// <summary>
    //    /// ctor
    //    /// </summary>
    //    /// <param name="filePaths">追忆文件路径数组</param>
    //    public RetrospectUploader( string[] filePaths )
    //    {
    //        RetrospectFilePaths = filePaths;
    //    }

    //    /// <summary>
    //    /// 起停机文件路径
    //    /// </summary>
    //    public string[] RetrospectFilePaths { get; set; }

    //    private DbConnection DbConnection
    //    {
    //        get { return _dbConnection; }
    //    }

    //    /// <summary>
    //    /// 创建Retrospect_Waveform对象
    //    /// </summary>
    //    /// <param name="channelNumber">通道号</param>
    //    /// <param name="wave">波形</param>
    //    /// <returns>Retrospect_Waveform对象</returns>
    //    private static Retrospect_Waveform CreateWaveForm( byte channelNumber, double[] wave )
    //    {
    //        CompressionType compressionType;
    //        double waveScale;
    //        byte[] bytes = AnalysisDataConvertUtils.DoubleToByte( wave, out waveScale, out compressionType );

    //        return new Retrospect_Waveform
    //                   {
    //                       ChnNo_NR = channelNumber,
    //                       Wave_GR = bytes,
    //                       WaveScale_NR = waveScale,
    //                       Compress_NR = (byte)compressionType
    //                   };
    //    }

    //    /// <summary>
    //    /// 检查追忆文件路径
    //    /// </summary>
    //    private void CheckRetrospectFilePaths()
    //    {
    //        foreach( string path in RetrospectFilePaths )
    //        {
    //            if( !File.Exists( path ) )
    //            {
    //                throw new ArgumentException( string.Format( "文件({0})不存在", path ) );
    //            }
    //        }
    //    }

    //    /// <summary>
    //    /// 上传追忆数据
    //    /// </summary>
    //    /// <param name="dataLength">数据长度</param>
    //    /// <param name="beginTime">起始时间</param>
    //    /// <param name="endTime">结束时间</param>
    //    public void UploadData( int dataLength, DateTime beginTime, DateTime endTime )
    //    {
    //        // 文件不存在则返回
    //        CheckRetrospectFilePaths();

    //        // 检查版本号
    //        CheckDataFileVersion( RetrospectFilePaths );

    //        //上传追忆数据
    //        using( EntityConnection conn = Context.CreateEntityConnection() )
    //        {
    //            _dbConnection = conn.StoreConnection;

    //            using( MEMSEntitiesWrap entitiesWrap = Context.CreateMemsEntities() )
    //            {
    //                using( var reader = new FlatFileReader { Paths = RetrospectFilePaths } )
    //                {
    //                    reader.OnOpenBinary4Read += RemoveFileHead;

    //                    var pointID2Waves = new HashDictionary<int, PointWaveData>();
    //                    var pointID2PointInfo = new HashDictionary<int, PointInfoData>();

    //                    // 批量插入的数量
    //                    const int BatchInsertCount = 100;
    //                    int insertCount = 0;
    //                    while( true )
    //                    {
    //                        byte[] bytes = reader.ReadBytes();

    //                        // 读到末尾，结束
    //                        if( bytes == null )
    //                        {
    //                            break;
    //                        }

    //                        TrendData trendData = SampleDataUtils.ReadFromBytes( bytes );

    //                        // 字节数组出错，返回
    //                        if( trendData == null )
    //                        {
    //                            throw new Exception( "字节数组出错" );
    //                        }

    //                        // 追忆数据上传的必须都是振动数据
    //                        if( !( trendData is WaveDataBase ) )
    //                        {
    //                            throw new Exception( "追忆数据上传的必须都是振动数据" );
    //                        }

    //                        // 采样时间不在指定的时间范围之内，跳过这条数据
    //                        if( trendData.SampleTime < beginTime || endTime < trendData.SampleTime )
    //                        {
    //                            continue;
    //                        }

    //                        // 读取测点信息
    //                        int pointID = trendData.PointID;
    //                        if( !pointID2PointInfo.ContainsKey( pointID ) )
    //                        {
    //                            PointInfoData info = GetPointInfo( DbConnection, pointID );
    //                            if( info == null )
    //                            {
    //                                TraceUtils.Info( string.Format( "测点({0})信息不存在", pointID ) );
    //                            }

    //                            pointID2PointInfo[pointID] = info;
    //                        }

    //                        // 测点信息不存在，跳过这条数据
    //                        PointInfoData pointInfo = pointID2PointInfo[pointID];
    //                        if( pointInfo == null )
    //                        {
    //                            continue;
    //                        }

    //                        PointWaveData waves = pointID2Waves[pointID];
    //                        if( waves == null )
    //                        {
    //                            pointID2Waves[pointID] = waves = new PointWaveData { DataLength = dataLength };
    //                        }

    //                        waves.AddData( trendData );

    //                        while( waves.HasEnoughData )
    //                        {
    //                            InsertRetrospectData( entitiesWrap, pointInfo, waves.Pop() );
    //                            insertCount++;

    //                            if( insertCount == BatchInsertCount )
    //                            {
    //                                insertCount = 0;

    //                                entitiesWrap.SaveChanges();
    //                                entitiesWrap.Clear();
    //                            }
    //                        }
    //                    } // while( true )

    //                    // 保存最后插入的数据
    //                    if( 0 < insertCount )
    //                    {
    //                        entitiesWrap.SaveChanges();
    //                        entitiesWrap.Clear();
    //                    }
    //                } // using( var reader = GetRetrospectFileReader() )
    //            } // using( MEMSEntitiesWrap entitiesWrap = MEMSEntitiesWrap.Create() )
    //        } // using ( var conn = SqlService.CreateEntityConnection() )
    //    }

    //    /// <summary>
    //    /// 向数据库中插入追忆数据
    //    /// </summary>
    //    /// <param name="entitiesWrap">MEMSEntitiesWrap</param>
    //    /// <param name="pointInfo">PointInfoData</param>
    //    /// <param name="trendData">TrendData</param>
    //    private void InsertRetrospectData( MEMSEntitiesWrap entitiesWrap, PointInfoData pointInfo, TrendData trendData )
    //    {
    //        Retrospect_Summary retrospectSummary = GetRetrospectSummary( trendData, pointInfo );

    //        var waveData = trendData as WaveDataBase;
    //        if( waveData != null )
    //        {
    //            retrospectSummary.RotSpeed_NR = (int)( waveData.Rev * pointInfo.RevRatio );

    //            retrospectSummary.DatLen_NR = waveData.DataLength;
    //            retrospectSummary.SampleFreq_NR = waveData.SampleFreq;
    //            retrospectSummary.SampMod_NR = (byte)waveData.MultiFreq;
    //        }

    //        var signalTypeID = (int)retrospectSummary.SigType_NR;

    //        Retrospect_Waveform retrospectWaveform1 = null;
    //        Retrospect_Waveform retrospectWaveform2 = null;
    //        var featureValues = new List<Retrospect_FeatureValue>();

    //        // 二维时间波形数据
    //        if( trendData is TimeWaveData_2D )
    //        {
    //            var timeWaveData = trendData as TimeWaveData_2D;

    //            //通道1波形
    //            retrospectWaveform1 = CreateWaveForm( ChannelNumber.One, timeWaveData.Wave_1D );

    //            //通道2波形
    //            retrospectWaveform2 = CreateWaveForm( ChannelNumber.Two, timeWaveData.Wave_2D );

    //            //特征值
    //            GetRetrospectFeatureValue( featureValues,
    //                                       timeWaveData.Feature_1D, ChannelNumber.One,
    //                                       pointInfo );

    //            GetRetrospectFeatureValue( featureValues,
    //                                       timeWaveData.Feature_2D, ChannelNumber.Two,
    //                                       pointInfo );
    //        }
    //        else if( trendData is TimeWaveData_1D ) // 一维时间波形数据
    //        {
    //            var timeWaveData = trendData as TimeWaveData_1D;

    //            //通道1波形
    //            retrospectWaveform1 = CreateWaveForm( ChannelNumber.One, timeWaveData.Wave );

    //            //特征值
    //            GetRetrospectFeatureValue( featureValues,
    //                                       timeWaveData.Feature, ChannelNumber.One,
    //                                       pointInfo );
    //        }

    //        retrospectSummary.Retrospect_ID = GetRetrospectID( DbConnection );

    //        if( retrospectWaveform1 != null )
    //        {
    //            retrospectWaveform1.Retrospect_ID = retrospectSummary.Retrospect_ID;
    //            retrospectSummary.Retrospect_Waveform.Add( retrospectWaveform1 );
    //        }

    //        if( retrospectWaveform2 != null )
    //        {
    //            retrospectWaveform2.Retrospect_ID = retrospectSummary.Retrospect_ID;
    //            retrospectSummary.Retrospect_Waveform.Add( retrospectWaveform2 );
    //        }

    //        foreach( Retrospect_FeatureValue featureValue in featureValues )
    //        {
    //            featureValue.Retrospect_ID = retrospectSummary.Retrospect_ID;
    //            retrospectSummary.Retrospect_FeatureValue.Add( featureValue );
    //        }

    //        entitiesWrap.AddToRetrospect_Summary( retrospectSummary );
    //    }

    //    /// <summary>
    //    /// 获取追忆数据摘要
    //    /// </summary>
    //    /// <returns></returns>
    //    private static Retrospect_Summary GetRetrospectSummary( TrendData trendData, PointInfoData pointInfo )
    //    {
    //        return new Retrospect_Summary
    //                   {
    //                       DJ_SpecImplement_ID = trendData.PointID,
    //                       SampTime_DT = trendData.SampleTime,
    //                       PntDim_NR = pointInfo.PntDim_NR,
    //                       DatType_NR = pointInfo.DatType_NR,
    //                       SigType_NR = pointInfo.SigType_NR,
    //                       AvgNum_NR = 1
    //                   };
    //    }

    //    /// <summary>
    //    /// 获取追忆库特征值
    //    /// </summary>
    //    /// <param name="featureValueList">输出</param>
    //    /// <param name="featureData">特征指标</param>
    //    /// <param name="chnNoNR">通道号</param>
    //    /// <param name="pointInfo">PointInfoData</param>
    //    private static void GetRetrospectFeatureValue( List<Retrospect_FeatureValue> featureValueList,
    //                                                   FeatureData featureData, byte chnNoNR, PointInfoData pointInfo )
    //    {
    //        byte[] featureValueIDs;
    //        double[] featureValues;
    //        GetFeatureValues( featureData, pointInfo, out featureValueIDs, out featureValues );

    //        for( int index = 0; index < featureValueIDs.Length; index++ )
    //        {
    //            featureValueList.Add(
    //                new Retrospect_FeatureValue
    //                    {
    //                        ChnNo_NR = chnNoNR,
    //                        FeatureValue_ID = featureValueIDs[index],
    //                        FeatureValue_NR = featureValues[index]
    //                    }
    //                );
    //        }
    //    }
    //}

    /// <summary>
    /// 测点的波形数据对象
    /// </summary>
    //public class PointWaveData
    //{
    //    /// <summary>
    //    /// 通道1波形
    //    /// </summary>
    //    private readonly List<double> _wave1D = new List<double>();

    //    /// <summary>
    //    /// 最新的TrendData
    //    /// </summary>
    //    private WaveDataBase _lastTrendData;

    //    /// <summary>
    //    /// 通道2波形
    //    /// </summary>
    //    private List<double> _wave2D;

    //    /// <summary>
    //    /// 数据长度
    //    /// </summary>
    //    public int DataLength { get; set; }

    //    /// <summary>
    //    /// 已包含足够的数据
    //    /// </summary>
    //    public bool HasEnoughData
    //    {
    //        get { return DataLength <= _wave1D.Count; }
    //    }

    //    /// <summary>
    //    /// 是否为2维测点
    //    /// </summary>
    //    private bool IsTwoDimension
    //    {
    //        get { return _wave2D != null; }
    //    }

    //    /// <summary>
    //    /// 添加数据
    //    /// </summary>
    //    /// <param name="trendData">TrendData</param>
    //    public void AddData( TrendData trendData )
    //    {
    //        _lastTrendData = (WaveDataBase)trendData;

    //        // 二维时间波形数据
    //        if( trendData is TimeWaveData_2D )
    //        {
    //            var timeWaveData = trendData as TimeWaveData_2D;

    //            _wave1D.AddRange( timeWaveData.Wave_1D );

    //            if( _wave2D == null )
    //            {
    //                _wave2D = new List<double>();
    //            }
    //            _wave2D.AddRange( timeWaveData.Wave_2D );
    //        }
    //            // 一维时间波形数据
    //        else if( trendData is TimeWaveData_1D )
    //        {
    //            var timeWaveData = trendData as TimeWaveData_1D;

    //            //通道1波形
    //            _wave1D.AddRange( timeWaveData.Wave );
    //        }
    //    }

    //    /// <summary>
    //    /// 如果包含足够的数据则返回最老的TrendData，否则返回null
    //    /// </summary>
    //    /// <returns>最老的TrendData</returns>
    //    public TrendData Pop()
    //    {
    //        if( !HasEnoughData )
    //        {
    //            return null;
    //        }

    //        int dataLength = DataLength;
    //        TrendData ret;
    //        if( IsTwoDimension )
    //        {
    //            var timeWaveData_2D = new TimeWaveData_2D();
    //            ret = timeWaveData_2D;

    //            timeWaveData_2D.Wave_1D = Pop( _wave1D, dataLength );
    //            timeWaveData_2D.Feature_1D = SampleDataUtils.CreateFeatureData( timeWaveData_2D.Wave_1D );

    //            timeWaveData_2D.Wave_2D = Pop( _wave2D, dataLength );
    //            timeWaveData_2D.Feature_2D = SampleDataUtils.CreateFeatureData( timeWaveData_2D.Wave_2D );
    //        }
    //        else
    //        {
    //            var timeWaveData_1D = new TimeWaveData_1D();
    //            ret = timeWaveData_1D;

    //            timeWaveData_1D.Wave = Pop( _wave1D, dataLength );
    //            timeWaveData_1D.Feature = SampleDataUtils.CreateFeatureData( timeWaveData_1D.Wave );
    //        }

    //        var wave = (WaveDataBase)ret;
    //        wave.PointID = _lastTrendData.PointID;
    //        wave.SampleTime = _lastTrendData.SampleTime;
    //        wave.Rev = _lastTrendData.Rev;
    //        wave.DataLength = dataLength;
    //        wave.SampleFreq = _lastTrendData.SampleFreq;
    //        wave.MultiFreq = _lastTrendData.MultiFreq;

    //        return ret;
    //    }

    //    /// <summary>
    //    /// 截取前dataLength个数据
    //    /// </summary>
    //    /// <param name="wave">波形集合</param>
    //    /// <param name="dataLength">数据长度</param>
    //    /// <returns>dataLength个数据</returns>
    //    private static double[] Pop( List<double> wave, int dataLength )
    //    {
    //        var ret = new double[dataLength];
    //        wave.CopyTo( 0, ret, 0, ret.Length );

    //        wave.RemoveRange( 0, ret.Length );

    //        return ret;
    //    }
    //}
}