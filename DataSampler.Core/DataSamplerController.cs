using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using AnalysisData.SampleData;
using AnalysisData.ToFromBytes;
using DataSampler.Helper;
using Moons.Common20;
using Moons.Common20.Serialization;
using SocketLib;
using System.IO;

namespace DataSampler
{
    /// <summary>
    ///     采集工作站的控制器
    /// </summary>
    public class DataSamplerController : DisposableBase
    {
        #region debug

        ///// <summary>
        ///// 发送测试用的采集工作站命令
        ///// </summary>
        //private void SendDebugSampleStationCommand()
        //{
        //    try
        //    {
        //        var sampleStationProxy = new SampleStationProxy();
        //        sampleStationProxy.SampleStationData.DataSamplerIP = "127.0.0.1";

        //        sampleStationProxy.StartNormalSample();
        //        sampleStationProxy.StopSample();
        //        //sampleStationProxy.SendAlmEventData_Test();
        //        sampleStationProxy.GetSampleStationStatus();
        //        sampleStationProxy.GetHardwareInfo();

        //        SampleStationParameterErrorDataCollection error = null;
        //        sampleStationProxy.DownloadSampleConfig( ref error );

        //        List<TrendData> trendDatas = sampleStationProxy.GetData( new int[1] );
        //        sampleStationProxy.Timing( DateTime.Now );
        //    }
        //    catch( Exception ex )
        //    {
        //        TraceUtils.Error( "发送测试用的采集工作站命令出错！", ex );
        //    }
        //}

        #endregion

        #region 变量和属性

        /// <summary>
        ///     内部使用的锁
        /// </summary>
        private readonly object _lock = new object();

        /// <summary>
        ///     采样定时器
        /// </summary>
        private readonly TimerWrapper _sampleTimer;

        /// <summary>
        ///     Socket连接的计数器
        /// </summary>
        private readonly ArrayList _sockets = ArrayList.Synchronized( new ArrayList() );

        /// <summary>
        /// ip字符串映射socket，使用这个对象作为访问这个对象的锁。
        /// </summary>
        public readonly Dictionary<string, SocketWrapper> _ip2SocketOfControl = new Dictionary<string, SocketWrapper>();

        /// <summary>
        ///     发送队列
        /// </summary>
        private readonly ITaskSendQueue _taskSender = new TasksSender();

        /// <summary>
        ///     内部使用的锁
        /// </summary>
        public object InnerLock
        {
            get { return _lock; }
        }

        /// <summary>
        ///     是否停止采集
        /// </summary>
        public bool IsStop
        {
            get { return State == DataSamplerState.Stop; }
        }

        /// <summary>
        ///     是否正在采集
        /// </summary>
        public bool IsSampling
        {
            get { return EnumUtils.IsAny( State, DataSamplerState.NormalSample, DataSamplerState.StartStopSample ); }
        }

        /// <summary>
        ///     是否正在正常采集
        /// </summary>
        public bool IsNormalSampling
        {
            get { return State == DataSamplerState.NormalSample; }
        }

        /// <summary>
        ///     是否处于忙碌状态，无法处理请求
        /// </summary>
        public bool IsBusy
        {
            get { return State != DataSamplerState.Stop; }
        }

        /// <summary>
        ///     数据采集器的状态
        /// </summary>
        public DataSamplerState State { get; private set; }

        #endregion

        #region 创建Controller对象

        /// <summary>
        ///     SampleStationController单例
        /// </summary>
        private static readonly DataSamplerController _instance = new DataSamplerController();

        /// <summary>
        ///     侦听的Socket，接入接收数据的Socket连接。
        /// </summary>
        private SocketWrapper _listenSocket;

        /// <summary>
        ///     侦听的Socket，接入发送控制命令的Socket连接，侦听端口1284。
        /// </summary>
        private SocketWrapper _listenSocketOfControl;

        private DataSamplerController()
        {
            State = DataSamplerState.Stop;

            _sampleTimer = new TimerWrapper { Callback = SampleData, Interval = new TimeSpan( 0, 0, 1 ) };

            Config.InitStructReadWriteHandler();

            Config.InitFirmware4Upgrade();

            StartListenSocket();

            InitDebugHandler();
        }

        /// <summary>
        ///     SampleStationController单例
        /// </summary>
        public static DataSamplerController Instance
        {
            get { return _instance; }
        }

        /// <summary>
        ///     初始化调试的代理
        /// </summary>
        private static void InitDebugHandler()
        {
            if( EnvironmentUtils.IsDebug )
            {
                SocketLibConfig.SendBytesEvent += SocketLibConfig_SendBytesEvent;
                SocketLibConfig.ReceiveBytesEvent += SocketLibConfig_ReceiveBytesEvent;

                SampleStationProxy.ReceiveCommandMessage = Config.LogCommandMessage;
            }
        }

        /// <summary>
        ///     响应接收了字节的事件
        /// </summary>
        private static void SocketLibConfig_ReceiveBytesEvent( SocketWrapper socket, byte[] buffer, int offset, int size )
        {
            string hex = StringUtils.ToHex( buffer, offset, size );
            TraceUtils.Info( string.Format( "{1} receive data: ({0})", hex, socket.IPPortInfo ) );
        }

        /// <summary>
        ///     响应发送了字节的事件
        /// </summary>
        private static void SocketLibConfig_SendBytesEvent( SocketWrapper socket, byte[] buffer, int offset, int size )
        {
            string hex = StringUtils.ToHex( buffer, offset, size );
            TraceUtils.Info( string.Format( "{1} send data: ({0})", hex, socket.IPPortInfo ) );
        }

        #endregion

        #region 采集数据

        /// <summary>
        ///     当前的活动采集工作站集合
        /// </summary>
        private ActiveSampleStationData[] ActiveSampleStationDatas { get; set; }

        /// <summary>
        ///     采集数据
        /// </summary>
        /// <param name="stateInfo">状态对象</param>
        private void SampleData( object stateInfo )
        {
        }

        /// <summary>
        ///     采集并发送数据到队列
        /// </summary>
        /// <param name="state"></param>
        private void SampleAndSend( object state )
        {
        }

        #endregion

        #region Socket侦听、读取

        /// <summary>
        ///     启动Socket侦听
        /// </summary>
        private void StartListenSocket()
        {
            int port = 1283;//ConfigBase.ListenPort;
            if( port == 0 )
            {
                TraceUtils.Info( string.Format( "Port number is zero, exit." ) );
                return;
            }

            _listenSocket = SocketWrapper.CreateTcpSocket();
            BindTcpPort( _listenSocket, port );

            int portOfControl = Config.ListenPortOfControl;
            _listenSocketOfControl = SocketWrapper.CreateTcpSocket();
            BindTcpPort(_listenSocketOfControl, portOfControl);

            Thread threadListen = ThreadUtils.CreateBackgroundThread( StartAccept, "StartAccept" );
            Thread threadListen2 = ThreadUtils.CreateBackgroundThread( StartAcceptSocketOfControl, "StartAcceptSocketOfControl");
            ThreadUtils.Start( threadListen, threadListen2 );
        }

        private void BindTcpPort( SocketWrapper socketWrapper, int port )
        {
            TraceUtils.Info( $"Binging port:{port}" );
            try
            {
                socketWrapper.Bind(port);
            }
            catch (Exception ex)
            {
                TraceUtils.Error($"Bind port({port}) error.", ex);
                socketWrapper.Dispose();
                throw;
            }

            socketWrapper.Listen(100);
        }

        /// <summary>
        ///     开始接入客户端连接
        /// </summary>
        private void StartAccept()
        {
            TraceUtils.Info("StartAccept.");

            var socket = _listenSocket;
            while ( true )
            {
                try
                {
                    if( socket.Disposed )
                    {
                        break;
                    }

                    OnAcceptSuccess( socket.Accept() );
                }
                catch( Exception ex )
                {
                    if( socket.IsListenClose( ex ) )
                    {
                        TraceUtils.Error("Listening socket close(SocketException)", ex );
                        break;
                    }

                    TraceUtils.Error("Listening thread error", ex );
                    break;
                }
            } // while( true )

            TraceUtils.Info( "Listening thread exit." );
        }

        /// <summary>
        ///     开始接入客户端连接
        /// </summary>
        private void StartAcceptSocketOfControl()
        {
            TraceUtils.Info("StartAcceptSocketOfControl.");

            var socket = _listenSocketOfControl;
            while (true)
            {
                try
                {
                    if ( socket.Disposed )
                    {
                        break;
                    }

                    SocketWrapper acceptedSocket = socket.Accept();

                    WaitCallback cacheSocketOfControl = state => {
                        SocketWrapper socketWrapper = null;
                        try {
                            socketWrapper = (SocketWrapper)state;

                            TraceUtils.LogDebugInfo($"StartAcceptSocketOfControl cacheSocketOfControl: {socketWrapper.IPPortInfo}");

                            socketWrapper.SendTimeout = socketWrapper.ReceiveTimeout = Config.SendReceiveTimeoutInMs;

                            byte[] buffer = new byte[128];
                            socketWrapper.ReceiveBytes( buffer );

                            string hex = StringUtils.ToHex(buffer, 0, buffer.Length);
                            TraceUtils.LogDebugInfo($"StartAcceptSocketOfControl cacheSocketOfControl: {socketWrapper.IPPortInfo}, received data: {hex}");

                            List<byte> text = new List<byte>();
                            foreach ( var c in buffer )
                            {
                                if ( c == 0 ) break;
                                text.Add( c );
                            }

                            const string IdPrefix = "msid=";
                            var message = StringUtils.TrimToLower( System.Text.Encoding.UTF8.GetString( text.ToArray() ) );
                            if( !string.IsNullOrWhiteSpace(message))
                            {
                                TraceUtils.LogDebugInfo( $"StartAcceptSocketOfControl receive id: {message}" );

                                var idIndex = message.IndexOf(IdPrefix);
                                if( -1 < idIndex)
                                {
                                    var ipIndex = idIndex + IdPrefix.Length;
                                    var semiColonIndex = message.IndexOf( ";", ipIndex );
                                    var ip = -1 < semiColonIndex ? message.Substring(ipIndex, semiColonIndex - ipIndex ) : message.Substring(ipIndex);
                                    if (!string.IsNullOrWhiteSpace( ip ))
                                    {
                                        var map = _ip2SocketOfControl;
                                        lock (map)
                                        {
                                            if (map.ContainsKey(ip)) {
                                                var oldSocket = map[ip];
                                                oldSocket.KeepSocketWrapperAlive = false;
                                                DisposeUtils.Dispose( oldSocket );
                                            }

                                            socketWrapper.KeepSocketWrapperAlive = true;
                                            map[ip] = socketWrapper;
                                            return;
                                        }
                                    }
                                }
                            }

                            DisposeUtils.Dispose(socketWrapper);
                        }
                        catch( Exception ex )
                        {
                            DisposeUtils.Dispose( socketWrapper );
                            TraceUtils.Error( "Accept control socket error.", ex );
                        }
                    };

                    ThreadPool.QueueUserWorkItem( cacheSocketOfControl, acceptedSocket );
                }
                catch (Exception ex)
                {
                    if ( socket.IsListenClose(ex) )
                    {
                        TraceUtils.Error("Listening socket close(SocketException)", ex);
                        break;
                    }

                    TraceUtils.Error("Listening thread error", ex);
                    break;
                }
            } // while( true )

            TraceUtils.Info("Listening thread exit.");
        }


        /// <summary>
        ///     接入成功
        /// </summary>
        private void OnAcceptSuccess( SocketWrapper socketWrapper )
        {
            try
            {
                TraceUtils.Info( string.Format( "OnAcceptSuccess ({0}).", socketWrapper.IPPortInfo ) );

                PackageSendReceive packageSendReceive =
                    PackageSendReceive.CreatePackageSendReceive_Server( socketWrapper );
                socketWrapper.LinkedObject = packageSendReceive;

                packageSendReceive.ReadPackageSuccess = OnReadPackageSuccess;

                packageSendReceive.ReadPackageFail = OnReadPackageFail;

                packageSendReceive.BeginReceivePackage();

                _sockets.Add( socketWrapper );
            }
            catch( Exception ex )
            {
                string error = "OnAcceptSuccess error";
                if( socketWrapper.IsDisconnect( ex ) )
                {
                    error = string.Format( "OnAcceptSuccess Socket closed({0})", socketWrapper.SocketErrorInfo );
                }
                TraceUtils.Error( error, ex );

                DisposeUtils.Dispose( socketWrapper );
            }
        }

        /// <summary>
        ///     读取包成功
        /// </summary>
        private void OnReadPackageSuccess( PackageSendReceive sendReceive )
        {
            try
            {
                try
                {
                    CommandMessage commandMessage = SampleStationProxy.ReadCommandFromBuffer( sendReceive );

                    OnReceiveCommandMessage( sendReceive, commandMessage );
                }
                catch( Exception ex )
                {
                    string ipPortInfo = sendReceive.SafeIPPortInfo;
                    TraceUtils.Error( string.Format( "Parse command package error({0})", ipPortInfo ), ex );

                    try
                    {
                        // 回一条命令通知错误
                        sendReceive.SendPackage(
                            ToFromBytesUtils.ToBytes( SampleDataReadWrite.FailCommand_PackageCommand ) );
                    }
                    catch( Exception ex4Send )
                    {
                        TraceUtils.Error( string.Format( "Send ack and notify error({0})", ipPortInfo ), ex4Send );
                    }
                }

                sendReceive.BeginReceivePackage();
            }
            catch( Exception ex )
            {
                TryCloseSocket( sendReceive, "OnReadPackageSuccess异常" );
                TraceUtils.Error( "OnReadPackageSuccess异常！", ex );
            }
        }

        #region 变量与测点映射

        /// <summary>
        ///     变量名对测点映射
        /// </summary>
        private HashDictionary<string, List<Variant2PointData>> _variant2PointData =
            new HashDictionary<string, List<Variant2PointData>>();

        #endregion

        /// <summary>
        ///     响应命令包
        /// </summary>
        private void OnReceiveCommandMessage( PackageSendReceive sendReceive, CommandMessage commandMessage )
        {
            switch( commandMessage.CommandID )
            {
                case CommandID.UploadData2DB:
                {
                    Upload2DB( commandMessage.Data );
                    break;
                }

                case CommandID.Timing:
                {
                    var sendCommand = new CommandMessage
                    {
                        CommandID = CommandID.CommandSuccess,
                        StructTypeID = StructTypeID.TimingData,
                        Data = new TimingData {Now = DateTime.Now}
                    };
                    sendReceive.SendPackage( ToFromBytesUtils.ToBytes( sendCommand ) );
                    return;
                }

                case CommandID.PushFileVersion:
                {
                    // 参数是：固件文件名 + 半角逗号 + 版本号 + 半角逗号 + 采集工作站IP  + 半角逗号 + 采集工作站端口，例如：”MS1000-A,1010,192.168.1.100 ,1283”
                    var text = commandMessage.Data as string;

                    Action errorAction = () => sendReceive.SendPackage(
                        ToFromBytesUtils.ToBytes( SampleDataReadWrite.FailCommand_PackageCommand ) );
                    try
                    {
                        var parameters = StringUtils.SplitByComma( text );

                        var firmFileName = parameters[0];
                        var version = parameters[1];
                        var sampleStationIp = parameters[2];
                        var sampleStationPort = Convert.ToInt32( parameters[3] );

                        var firmwareFileInfoData = Config.GetFirmwareFileInfoDataByName( firmFileName );
                        if( firmwareFileInfoData == null )
                        {
                            string error = string.Format( "{0} firmwarefile not exist.", firmFileName );
                            TraceUtils.Error( error );

                            errorAction();
                            return;
                        }

                        // 需要升级
                        var targetVersion = firmwareFileInfoData.FirmwareFileVersion;
                        if( VersionUtils.IsVersionGreater( targetVersion, version ) )
                        {
                            TraceUtils.Info( string.Format( "{3}:{4} Upgrade firmware ({0}, {1} => {2}) ...",
                                firmFileName,
                                version,
                                targetVersion, sampleStationIp, sampleStationPort ) );

                            var sampleStationProxy = new SampleStationProxy
                            {
                                SampleStationData =
                                    new SampleStationData
                                    {
                                        SampleStationIP = sampleStationIp,
                                        SampleStationPort = sampleStationPort
                                    }
                            };

                            TraceUtils.LogDebugInfo( string.Format( "PushFirmwareFileBytes to {0}...",
                                sampleStationProxy.IPAddress ) );

                            sampleStationProxy.PushFirmwareFileBytes( firmwareFileInfoData );

                            TraceUtils.Info( string.Format( "{3}:{4} Upgrade firmware ({0}, {1} => {2}) succeed.",
                                firmFileName,
                                version,
                                targetVersion, sampleStationIp, sampleStationPort ) );

                            // 返回表示需要升级的包
                            sendReceive.SendPackage( ToFromBytesUtils.ToBytes( new CommandMessage
                            {
                                CommandID = CommandID.CommandSuccess,
                                StructTypeID = StructTypeID.ErrorMessage,
                                Data = new ErrorMessageData
                                {
                                    ErrorCode = SampleStationErrorCode.InfoCode_NeedUpgradeFireware
                                }
                            } ) );
                            return;
                        }
                    }
                    catch( Exception )
                    {
                        errorAction();
                        throw;
                    }
                    break;
                }

                default:
                    throw new SampleStationException( "Receive wrong data." ) {ErrorCode = SampleStationErrorCode.PackageCommand};
            }

            sendReceive.SendPackage( ToFromBytesUtils.ToBytes( SampleDataReadWrite.SuccessCommand ) );
        }

        /// <summary>
        ///     将数据放到上传队列中
        /// </summary>
        /// <param name="data">数据</param>
        internal void Upload2DB( object data )
        {
            if( data is TrendData )
            {
                var trendData = data as TrendData;

                #region 通过变量方式上传数据

                // 根据变量名获得测点ID，并转换测量值
                if( !string.IsNullOrEmpty( trendData.VariantName ) )
                {
                    var key = CharCaseUtils.ToCase( trendData.VariantName );
                    var variant2PointData = _variant2PointData;
                    if( !variant2PointData.ContainsKey( key ) )
                    {
                        // 没有找到对应的测点，抛弃这条数据
                        return;
                    }

                    // 一个变量可以为多个测点提供数据
                    foreach( var variant in variant2PointData[key] )
                    {
                        var variantData = new TrendData
                        {
                            VariantName = trendData.VariantName,
                            DataUsageID = trendData.DataUsageID,
                            SampleTime = trendData.SampleTime,
                            PointID = variant.PointID
                        };
                        if( trendData.DataTypeIsTextValue )
                        {
                            variantData.MeasurementValueString4Opc = trendData.MeasurementValueString4Opc;
                            variantData.MeasurementValue = 0;
                        }
                        else
                        {
                            variantData.MeasurementValue = ( trendData.MeasurementValue + variant.Offset ) *
                                                           variant.Scale;
                        }
                        _taskSender.Add( variantData );
                    }
                    return;
                }

                #endregion
            }

            var isCorrectData = ( data is TrendData ) || ( data is AlmEventData );
            if( isCorrectData )
            {
                Put2Queue( data );
            }
        }

        /// <summary>
        ///     将数据放到上传队列中
        /// </summary>
        /// <param name="data">数据</param>
        internal void Put2Queue(object data)
        {
            if( Config.CalculateRev &&  data is TimeWaveData_1D )
            {
                CalculateRevManager.AddRev( data as TimeWaveData_1D );
            }

            _taskSender.Add(data);
        }

        /// <summary>
        ///     读取包失败
        /// </summary>
        private void OnReadPackageFail( PackageSendReceive sendReceive )
        {
            string ipPortInfo = sendReceive.SafeIPPortInfo;
            try
            {
                if( TryCloseSocket( sendReceive, "OnReadPackageFail" ) )
                {
                    return;
                }

                TraceUtils.Error( string.Format( "Read package error({0})", ipPortInfo ), sendReceive.InnerException );

                try
                {
                    // 回一条命令希望客户端关闭
                    sendReceive.SendPackage(
                        ToFromBytesUtils.ToBytes( SampleDataReadWrite.FailCommand_PackageCommand ) );

                    ThreadUtils.ThreadSleep( 500 );
                }
                catch( Exception ex )
                {
                    TraceUtils.Error( string.Format( "Send ack and hope client to close({0}).", ipPortInfo ), ex );
                }
                finally
                {
                    CloseSocket( sendReceive );
                }
            }
            catch( Exception ex )
            {
                TraceUtils.Error( string.Format( "OnReadPackageFail error({0}).", ipPortInfo ), ex );
            }
        }

        /// <summary>
        ///     尝试关闭Socket
        /// </summary>
        /// <param name="sendReceive">PackageSendReceive</param>
        /// <param name="message">附加信息</param>
        /// <returns>true：已关闭，false：未关闭</returns>
        private bool TryCloseSocket( PackageSendReceive sendReceive, string message )
        {
            SocketWrapper socket = sendReceive.InnerSocketWrapper;
            if( sendReceive.IsDisconnect() )
            {
                TraceUtils.Info( string.Format( "{0} connection closed ({1}).", message, socket.SocketErrorInfo ) );
                CloseSocket( sendReceive );
                return true;
            }

            return false;
        }

        /// <summary>
        ///     关闭Socket
        /// </summary>
        private void CloseSocket( PackageSendReceive sendReceive )
        {
            RemoveSocketFromList( sendReceive );
            DisposeUtils.Dispose( sendReceive.InnerSocketWrapper );
        }

        /// <summary>
        ///     从列表移除Socket
        /// </summary>
        private void RemoveSocketFromList( PackageSendReceive sendReceive )
        {
            TryCatchUtils.Catch( _sockets.Remove, sendReceive.InnerSocketWrapper );
        }

        #endregion

        #region 控制工作站

        /// <summary>
        ///     获得数据采集器配置信息
        /// </summary>
        /// <returns>SamplerConfigData</returns>
        private static SamplerConfigData GetSamplerConfig()
        {
            SamplerConfigData config = Config.ReadSamplerConfig();

            // 没有本地配置文件，抛出异常
            if( config == null )
            {
                throw new Exception( "No local config file." );
            }

            return config;
        }

        /// <summary>
        ///     开始正常采集
        /// </summary>
        public void StartNormalSample()
        {
            lock( _lock )
            {
                if( IsSampling )
                {
                    return;
                }

                //#region debug

                //SendDebugSampleStationCommand();
                //return;

                //#endregion

                SamplerConfigData samplerConfig = GetSamplerConfig();
                SampleStationData[] sampleStationDatas = null;
                try
                {
                    //OnlineSampleServiceClient onlineSampleServiceClient = GetOnlineSampleServiceClient();

                    //string emsg = null;
                    //byte[] sampleStationDatasBytes = null;
                    //Action<OnlineSampleServiceClient> handler = client =>
                    //                                            client.GetDataSamplerConfig(
                    //                                                samplerConfig.DataSamplerID,
                    //                                                ref sampleStationDatasBytes, ref emsg );

                    //WCFUtils.UsingWcf( onlineSampleServiceClient, handler );
                    //if( ErrorUtils.IsError( emsg ) )
                    //{
                    //    throw new Exception( emsg );
                    //}

                    //if( sampleStationDatasBytes != null )
                    //{
                    //    DataSamplerConfigData dataSamplerConfigData =
                    //        (DataSamplerConfigData)SocketLib.SerializationFormatters.DeserializeBinary(sampleStationDatasBytes);

                    //    sampleStationDatas = dataSamplerConfigData.SampleStationDatas;

                    //    // 初始化_variant2PointData
                    //    var variant2PointData = new HashDictionary<string, List<Variant2PointData>>();
                    //    if( dataSamplerConfigData.Variant2PointDatas != null )
                    //    {
                    //        foreach( Variant2PointData variant in dataSamplerConfigData.Variant2PointDatas )
                    //        {
                    //            var key = CharCaseUtils.ToCase( variant.VariantName );
                    //            if( !variant2PointData.ContainsKey( key ) )
                    //            {
                    //                variant2PointData[key] = new List<Variant2PointData>();
                    //            }
                    //            variant2PointData[key].Add( variant );
                    //        }
                    //    }

                    //    _variant2PointData = variant2PointData;
                    //}

                    Config.InitFirmware4Upgrade();
                }
                catch( Exception ex )
                {
                    const string ErrorMessage = "Get sample station config error.";
                    TraceUtils.Error( ErrorMessage, ex );
                    throw new Exception( ErrorMessage, ex );
                }

                //sampleStationDatas = sampleStationDatas.Where( sampleStationData => !sampleStationData.SampleStationIP.StartsWith("127.") ).ToArray();
                //TraceUtils.Info($"StartNormalSample, sampleStationDatas length: {sampleStationDatas.Length}.");
                //if ( CollectionUtils.IsNotEmptyG( sampleStationDatas ) )
                //{
                //    //const string ErrorMessage = "Sample station collection empty.";
                //    //TraceUtils.Error( ErrorMessage );
                //    //throw new Exception( ErrorMessage );
                //    foreach (var a in sampleStationDatas)
                //        TraceUtils.Info($"StartNormalSample, {a.SampleStationIP},{a.SampleStationPort},{a.QueryIntervalInSecond}");

                //    var sampleItems = new SampleItemCollection();
                //    sampleItems.AddRange(
                //        // ReSharper disable AssignNullToNotNullAttribute
                //        sampleStationDatas.Select(
                //            // ReSharper restore AssignNullToNotNullAttribute
                //            sampleStationData => new SampleItem
                //            {
                //                SampleInterval =
                //                        new TimeSpan(0, 0,
                //                                      sampleStationData.QueryIntervalInSecond),
                //                InnerData = sampleStationData
                //            }
                //            )
                //        );

                //    _sampleItems = sampleItems;
                //}

                _taskSender.StartSend();

                State = DataSamplerState.NormalSample;
                if( Config.Probe.StartSampleSuccessCount < int.MaxValue )
                {
                    Config.Probe.StartSampleSuccessCount += 1;
                }

                Config.Probe.SetStartSampleTime();
            }
        }

        /// <summary>
        ///     开始起停车采集
        /// </summary>
        public void StartStartStopSample()
        {
        }

        /// <summary>
        ///     停止采集
        /// </summary>
        public void StopSample()
        {
            lock( _lock )
            {
                if( IsStop )
                {
                    return;
                }

                _taskSender.StopSend();

                State = DataSamplerState.Stop;

                Config.Probe.SetStopSampleTime();
            }
        }

        #endregion

        #region Dispose

        protected override void Dispose( bool disposing )
        {
            if( IsDisposed )
            {
                return;
            }

            if( disposing )
            {
                DisposeUtils.Dispose( _listenSocket );
            }

            //  一定要调用基类的Dispose函数
            base.Dispose( disposing );
        }

        #endregion
    }
}