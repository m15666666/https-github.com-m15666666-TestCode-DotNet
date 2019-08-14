using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using AnalysisAlgorithm;
using AnalysisData;
using AnalysisData.Constants;
using AnalysisData.SampleData;
using Moons.Common20;
using SocketLib;
using CollectionUtils = Moons.Common20.CollectionUtils;
using TrendData = AnalysisData.SampleData.TrendData;
using AnalysisData.Helper;

namespace SampleServer.Upload2DB
{
    /// <summary>
    /// 正常采集的队列，用于将正常采集时传递的对象串行化。
    /// </summary>
    public class NormalSampleQueue
    {
        #region 变量和属性

        /// <summary>
        /// 序列化器
        /// </summary>
        private readonly BinaryFormatter _formatter = SerializationFormatters.BinaryFormatter;

        #region 缓存

        /// <summary>
        /// 报警事件缓存
        /// </summary>
        private readonly LockQueue<List<AlmEventData>> _almBuffer = new LockQueue<List<AlmEventData>>();

        /// <summary>
        /// 字节数组缓存
        /// </summary>
        private readonly LockQueue<object> _bytesBuffer = new LockQueue<object>();

        /// <summary>
        /// 测点ID对时间的映射，避免无线传感器/网关发送重复数据。
        /// 前提：每个无线传感器/测点上传的数据的采样时间都是升序的。
        /// </summary>
        private readonly Dictionary<int,long> _pointId2TimeForAvoidingSameData = new Dictionary<int, long>(4096);

        /// <summary>
        /// 入库数据缓存
        /// </summary>
        private readonly LockQueue<List<TrendData>> _dataBuffer = new LockQueue<List<TrendData>>();

        /// <summary>
        /// 监测数据缓存
        /// </summary>
        private readonly LockQueue<List<TrendData>> _monitorDataBuffer = new LockQueue<List<TrendData>>();

        #endregion

        #region 上传对象

        /// <summary>
        /// 正常采集报警上传数据库对象
        /// </summary>
        private readonly NormalAlmUploader _almUploader = new NormalAlmUploader();

        /// <summary>
        /// 正常采集数据上传数据库对象
        /// </summary>
        private readonly NormalSampleUploader _dataUploader = new NormalSampleUploader();

        #endregion

        #region 线程相关

        /// <summary>
        /// 将数组反序列化为对象的线程
        /// </summary>
        private readonly Thread _deserializeThread;

        /// <summary>
        /// 反序列化线程事件
        /// </summary>
        private readonly AutoResetEvent _deserializeThreadEvent = new AutoResetEvent( false );

        /// <summary>
        /// 线程状态对象
        /// </summary>
        private readonly ThreadStatusUtils _threadStatus = new ThreadStatusUtils();

        /// <summary>
        /// 上传报警到数据库线程
        /// </summary>
        private readonly Thread _uploadAlm2DBThread;

        /// <summary>
        /// 上传报警到数据库线程事件
        /// </summary>
        private readonly AutoResetEvent _uploadAlm2DBThreadEvent = new AutoResetEvent( false );

        /// <summary>
        /// 上传数据到数据库线程
        /// </summary>
        private readonly Thread _uploadData2DBThread;

        /// <summary>
        /// 上传数据到数据库线程事件
        /// </summary>
        private readonly AutoResetEvent _uploadData2DBThreadEvent = new AutoResetEvent( false );

        #endregion

        #endregion

        #region 加入队列

        /// <summary>
        /// 加入队列
        /// </summary>
        /// <param name="bytes">字节数组或者对象</param>
        public void AddBytes( object bytes )
        {
            int maxNormalSampleQueue = Config.MaxNormalSampleQueue;
            int count = _bytesBuffer.Count;
            if( maxNormalSampleQueue <= count )
            {
                TraceUtils.Warn( string.Format("NormalSampleQueue full ({0})/({1})", count, maxNormalSampleQueue ) );
                return;
            }

            _bytesBuffer.Add( bytes );
            _deserializeThreadEvent.Set();
        }

        /// <summary>
        /// 加入队列
        /// </summary>
        /// <param name="data">数据对象</param>
        public void Add( object data )
        {
            int maxNormalSampleQueue = Config.MaxNormalSampleQueue;

            var alms = data as List<AlmEventData>;
            if( alms != null )
            {
                int almCount = _almBuffer.Count;
                if( maxNormalSampleQueue <= almCount )
                {
                    TraceUtils.Warn( string.Format("NormalSampleQueue full ({0})/({1})", almCount, maxNormalSampleQueue ) );
                    return;
                }

                _almBuffer.Add( alms );
                _uploadAlm2DBThreadEvent.Set();
            }
            else if( data is List<TrendData> )
            {
                var trendDatas = data as List<TrendData>;

                bool isMonitorData = false;
                foreach ( var trendData in trendDatas )
                {
                    if( trendData.DataUsageID == DataUsageID.Monitor )
                    {
                        isMonitorData = true;
                        break;
                    }
                }

                #region 酒钢模式处理

                if ( Config.IsWineSteelCustomMode )
                {
                    AddVelocity2DistanceDatas( trendDatas );
                    AddAlmEventOfOpcDatas(trendDatas);
                }

                #endregion

                if( isMonitorData )
                {
                    int count = _monitorDataBuffer.Count;
                    if( maxNormalSampleQueue <= count )
                    {
                        TraceUtils.Warn( string.Format( "Normal monitoring queue reach threshold .({0})/({1})", count, maxNormalSampleQueue ) );
                        return;
                    }

                    _monitorDataBuffer.Add( trendDatas );
                }
                else
                {
                    int count = _dataBuffer.Count;
                    if( maxNormalSampleQueue <= count )
                    {
                        TraceUtils.Warn( string.Format("Normal saving queue reach threshold .({0})/({1})", count, maxNormalSampleQueue ) );
                        return;
                    }

                    _dataBuffer.Add( trendDatas );
                }
                
                _uploadData2DBThreadEvent.Set();
            } // else if( data is List<TrendData> )
        }

        /// <summary>
        ///     添加从速度计算出位移测量值的数据
        /// </summary>
        /// <param name="trendDatas">传给SampleServer的数据</param>
        private void AddVelocity2DistanceDatas( List<TrendData> trendDatas )
        {
            var distanceTrendDatas = new List<TrendData>();

            // 速度转位移的测点编码后缀
            const string V2dSuffix = "[v-d]";

            // 速度转位移的测点对应的位移的测点编码后缀
            const string DSuffix = "[d]";

            var sampleServerContext = Config.SampleServerContext;
            foreach( var trendData in trendDatas )
            {
                var timeWaveData = trendData as TimeWaveData_1D;
                if( timeWaveData == null )
                {
                    continue;
                }

                var pointCode = sampleServerContext.GetPointCodeById( trendData.PointID );
                if( string.IsNullOrWhiteSpace( pointCode ) || !pointCode.EndsWith( V2dSuffix ) )
                {
                    continue;
                }

                var distancePointCode = pointCode.Substring( 0, pointCode.Length - V2dSuffix.Length ) + DSuffix;

                var distancePointId = sampleServerContext.GetPointIdByCode( distancePointCode );
                if ( distancePointId == 0 )
                {
                    continue;
                }

                var waveArray = new double[timeWaveData.Wave.Length];
                DigitInt.IntN( timeWaveData.Wave, 1, 10, timeWaveData.SampleFreq, waveArray );

                // 酒钢的位移测点只要测量值，不要时间波形和频谱。
                distanceTrendDatas.Add( new TrendData
                {
                    DataUsageID = trendData.DataUsageID,
                    SampleTime = trendData.SampleTime,
                    PointID = distancePointId,
                    MeasurementValue = (float)DSPBasic.Peak2Peak( waveArray ) // 根据酒钢的要求：位移测量值使用有效峰峰值
                } );
            } // foreach (var trendData in trendDatas)

            #region 计算报警

            List<AlmEventData> alms = null;
            foreach( var distanceTrendData in distanceTrendDatas )
            {
                var pp = distanceTrendData.MeasurementValue;

                var almStandCommonSetting = Config.GetAlmStand_CommonSettingDataByID(distanceTrendData.PointID, FeatureValueID.PP);
                if (almStandCommonSetting == null)
                {
                    TraceUtils.LogDebugInfo($"Alarm standards not exist, pointId:{distanceTrendData.PointID}.");
                    continue;
                }

                // 计算报警等级
                var almLevel = AlmUtils.CalcCommonAlmLevel(pp, almStandCommonSetting);

                distanceTrendData.AlmLevelID = almLevel;

                if ( distanceTrendData.DataUsageID == DataUsageID.Save2DB && AlmLevelID.Level_Normal < almLevel )
                {
                    if ( alms == null )
                    {
                        alms = new List<AlmEventData>();
                    }

                    alms.Add( new AlmEventData
                    {
                        AlmID = 0,// 外部程序赋值
                        PointID = distanceTrendData.PointID,
                        AlmTime = distanceTrendData.SampleTime,
                        AlmLevel = almLevel,
                        AlmSourceID = FeatureValueID.PP,
                        AlmCount = 1,
                        AlmValue = pp
                    } );
                }
            } // foreach( var distanceTrendData in distanceTrendDatas )

            var distanceDatas2Db = distanceTrendDatas.Where( data => data.DataUsageID == DataUsageID.Save2DB );
            var almPointIds = alms != null ? alms.Select( alm => alm.PointID ).ToList() : new List<int>();
            var normalPointIds =
                distanceDatas2Db.Where( item => !almPointIds.Contains( item.PointID ) )
                    .Select( item => item.PointID );
            foreach( var pointId in normalPointIds )
            {
                if( _pointId2AlmCount.ContainsKey( pointId ) )
                {
                    // 中间出现正常的测点，将连续报警次数清零。
                    _pointId2AlmCount[pointId] = 0;

                    TraceUtils.LogDebugInfo($"Clear alm count: point({pointId}).");
                }
            }

            if( alms != null )
            {
                // 连续报警次数10次
                const int ContinualAlarmCount = 3;
                var alarms = new List<AlmEventData>();
                foreach( var alm in alms )
                {
                    var pointId = alm.PointID;
                    if( !_pointId2AlmCount.ContainsKey( pointId ) )
                    {
                        _pointId2AlmCount[pointId] = 0;
                    }
                    var almCount = _pointId2AlmCount[pointId] + 1;

                    TraceUtils.LogDebugInfo( $"Increase alm count: point({pointId}) - almCount({almCount} - alm({alm.AlmTime})).");

                    _pointId2AlmCount[pointId] = almCount;
                    if ( almCount == ContinualAlarmCount )
                    {
                        _pointId2AlmCount[pointId] = 0;

                        TraceUtils.LogDebugInfo($"Generate almevent: point({pointId}) - almCount({almCount} - alm({alm.AlmTime})).");

                        alm.AlmCount = almCount;
                        alarms.Add( alm );
                    }
                }
                //Add( alms );
                if( 0 < alarms.Count )
                {
                    Add( alarms );
                }
            }

            #endregion

            trendDatas.AddRange( distanceTrendDatas );
        }

        /// <summary>
        ///     针对OPC入库数据计算报警
        /// </summary>
        /// <param name="trendDatas">传给SampleServer的数据</param>
        private void AddAlmEventOfOpcDatas( List<TrendData> trendDatas)
        {
            List<AlmEventData> alms = null;
            foreach (var trendData in trendDatas)
            {
                if( trendData.DataUsageID == DataUsageID.Monitor || string.IsNullOrEmpty( trendData.VariantName ) )
                {
                    continue;
                }

                var pointId = trendData.PointID;

                var almStandCommonSetting = Config.GetAlmStand_CommonSettingDataByID(pointId, FeatureValueID.Measurement);
                if (almStandCommonSetting == null)
                {
                    continue;
                }

                // 计算报警等级
                var value = trendData.MeasurementValue;
                var almLevel = AlmUtils.CalcCommonAlmLevel( value, almStandCommonSetting );

                trendData.AlmLevelID = almLevel;

                if ( AlmLevelID.Level_Normal < almLevel )
                {
                    if (alms == null)
                    {
                        alms = new List<AlmEventData>();
                    }

                    alms.Add(new AlmEventData
                    {
                        AlmID = 0, // 外部程序赋值
                        PointID = pointId,
                        AlmTime = trendData.SampleTime,
                        AlmLevel = almLevel,
                        AlmSourceID = FeatureValueID.Measurement,
                        AlmCount = 1,
                        AlmValue = value
                    });
                }
            } // foreach (var trendData in trendDatas)

            if (alms != null)
            {
                Add( alms );
            }
        }

        private Dictionary<int, int> _pointId2AlmCount = new Dictionary<int, int>();

        #endregion

        #region 上传数据库

        public NormalSampleQueue()
        {
            _deserializeThread = ThreadUtils.CreateThread( DeserializeBytes, "NormalSampleQueue_DeserializeBytes" );
            _uploadData2DBThread = ThreadUtils.CreateThread( UploadData2DB, "NormalSampleQueue_UploadData2DB" );
            _uploadAlm2DBThread = ThreadUtils.CreateThread( UploadAlm2DB, "NormalSampleQueue_UploadAlm2DB" );

            ThreadUtils.Start( _deserializeThread, _uploadData2DBThread, _uploadAlm2DBThread );
        }

        /// <summary>
        /// 停止上传数据库
        /// </summary>
        public void StopUpload2DB()
        {
            TraceUtils.Info("StopUpload2DB...");

            if( _uploadData2DBThread != null )
            {
                _threadStatus.Stop();

                ThreadUtils.SetEvents2Signaled(
                    _deserializeThreadEvent,
                    _uploadData2DBThreadEvent,
                    _uploadAlm2DBThreadEvent );

                ThreadUtils.Join(
                    _deserializeThread,
                    _uploadData2DBThread,
                    _uploadAlm2DBThread );
            }

            _dataUploader.Dispose();
            _almUploader.Dispose();

            TraceUtils.Info("StopUpload2DB finish.");
        }

        /// <summary>
        /// 将字节数组反序列化
        /// </summary>
        private void DeserializeBytes()
        {
            while( _threadStatus.IsStarted )
            {
                if( !_bytesBuffer.HasData )
                {
                    _deserializeThreadEvent.WaitOne();
                    continue;
                }

                var trendDatas = new List<TrendData>();
                var almEventDatas = new List<AlmEventData>();

                var datas = _bytesBuffer.PopAll();
                foreach( var bytes in datas )
                {
                    try
                    {
                        object obj = bytes;
                        if(bytes is byte[] b)
                        {
                            obj = SerializationFormatters.DeserializeBinary(b, _formatter);
                        }
                        var trendData = obj as TrendData;
                        if( trendData != null )
                        {
                            if(trendData.DataUsageID == DataUsageID.Save2DB)
                            {
                                var time = PartitionIDUtils.Time2LongID(trendData.SampleTime);
                                var pointId = trendData.PointID;
                                if (_pointId2TimeForAvoidingSameData.ContainsKey(pointId) && _pointId2TimeForAvoidingSameData[pointId] == time)
                                {
                                    TraceUtils.Error($"Duplicated data detected. pointId:{pointId}, {time}, {trendData.MeasurementValue}");
                                    continue;
                                }
                                _pointId2TimeForAvoidingSameData[pointId] = time;
                            }

                            trendData.AlmID = Config.GetAlmIDByAlmEventUniqueID( trendData.AlmEventUniqueID );
                            trendDatas.Add( trendData );
                            continue;
                        }

                        var almEventData = obj as AlmEventData;
                        if( almEventData != null )
                        {
                            string almEventUniqueID = almEventData.AlmEventUniqueID;
                            if( string.IsNullOrEmpty( almEventUniqueID ) )
                            {
                                TraceUtils.Error( string.Format("Empty almEventUniqueID({0})", almEventUniqueID ) );
                                continue;
                            }


                            //// 电池电量报警不入库
                            //if( almEventData.AlmSourceID == FeatureValueID.Error_TransducerBatteryLow )
                            //{
                            //    string message = $"AlmEvent Skip: {almEventData.AlmTime}, " +
                            //                     $"{almEventData.PointID}, " +
                            //                     $"{almEventData.AlmSourceID}, " +
                            //                     $"{FeatureValueID.GetName(almEventData.AlmSourceID)}";
                            //    TraceUtils.Info( message );
                            //    continue;
                            //}

                            almEventData.AlmID = Config.GetAlmIDByAlmEventUniqueID( almEventUniqueID );
                            almEventDatas.Add( almEventData );
                            continue;
                        }
                    }
                    catch( Exception ex )
                    {
                        TraceUtils.Error( string.Format("DeserializeBytes error, (bytes == null:{0})", bytes == null ), ex );
                    }
                }

                if( CollectionUtils.IsNotEmptyG( trendDatas ) )
                {
                    Add( trendDatas );
                }

                if( CollectionUtils.IsNotEmptyG( almEventDatas ) )
                {
                    Add( almEventDatas );
                }
            } // while( _threadStatus.IsStarted )
        }

        /// <summary>
        /// 上传采集的数据到数据库
        /// </summary>
        private void UploadData2DB()
        {
            while( _threadStatus.IsStarted )
            {
                Config.Probe.SetNormalDataQueueCount( _dataBuffer.Count );

                if( !_dataBuffer.HasData && !_monitorDataBuffer.HasData )
                {
                    _uploadData2DBThreadEvent.WaitOne();
                    continue;
                }

                if( _monitorDataBuffer.HasData )
                {
                    // 先处理全部的监测数据
                    foreach( var trendDatas in _monitorDataBuffer.PopAll() )
                    {
                        _dataUploader.UploadData( trendDatas );
                    }
                }

                if( _dataBuffer.HasData )
                {
                    // 每次处理10个
                    List<TrendData>[] datas = _dataBuffer.Pop( 10 );

                    DateTime uploadBegin = DateTime.Now;
                    foreach( var trendDatas in datas )
                    {
                        _dataUploader.UploadData( trendDatas );

                        Config.Probe.UploadNormalSampleDataTime = DateTime.Now;
                    }

                    Config.Probe.LastUploadNormalDataTimeSpan = DateTime.Now - uploadBegin;
                }
            } // while( _threadStatus.IsStarted )
        }

        /// <summary>
        /// 上传报警到数据库
        /// </summary>
        private void UploadAlm2DB()
        {
            while( _threadStatus.IsStarted )
            {
                Config.Probe.SetNormalAlmQueueCount( _almBuffer.Count );

                if( !_almBuffer.HasData )
                {
                    _uploadAlm2DBThreadEvent.WaitOne();
                    continue;
                }

                // 每次处理10个
                List<AlmEventData>[] datas = _almBuffer.Pop( 10 );

                DateTime uploadBegin = DateTime.Now;
                foreach( var almEventDatas in datas )
                {
                    _almUploader.UploadAlm( almEventDatas );

                    Config.Probe.UploadNormalSampleAlmTime = DateTime.Now;
                }

                Config.Probe.LastUploadNormalAlmTimeSpan = DateTime.Now - uploadBegin;
            }
        }

        #endregion
    }
}