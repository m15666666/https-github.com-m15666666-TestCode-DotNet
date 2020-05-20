using System;
using System.IO;
using AnalysisData.SampleData;
using AnalysisData.ToFromBytes;
using DataSampler.Core.Dto;
using DataSampler.Helper;
using Microsoft.Extensions.Options;
using Moons.Common20;
using Moons.Common20.IOC;
using Moons.Common20.ObjectMapper;
using Moons.Common20.Serialization;
using Moons.Common20.Serialization.Json;
using OnlineSampleCommon.SendTask;
using SocketLib;

namespace DataSampler
{
    /// <summary>
    /// 配置工具类
    /// </summary>
    public static class Config
    {
        #region mapper

        /// <summary>
        /// datasampler 配置数据
        /// </summary>
        internal static IObjectMapper ObjectMapper => _objectMapper ??
            (_objectMapper = IocUtils.Instance.GetRequiredService<IObjectMapper>());

        private static IObjectMapper _objectMapper;

        #endregion

        #region 测试参数

        /// <summary>
        /// 用于测试的采集工作站的IP
        /// </summary>
        internal static string TestSampleStationIP => "127.0.0.1";

        /// <summary>
        /// 记录CommandMessage对象
        /// </summary>
        /// <param name="commandMessage">CommandMessage</param>
        internal static void LogCommandMessage( CommandMessage commandMessage )
        {
            if (!DatasamplerConfigDto.PrintTcpMessage) return;
            //if (EnvironmentUtils.IsDebug)
            {
                string info = commandMessage.ToString();
                Config.TcpLogger.Debug( info );
            }
        }
        /// <summary>
        /// 记录CommandMessage对象
        /// </summary>
        /// <param name="commandMessage">CommandMessage</param>
        internal static void LogCommandMessage( CommandMessage commandMessage, string prefix )
        {
            if (!DatasamplerConfigDto.PrintTcpMessage) return;
            //if (EnvironmentUtils.IsDebug)
            {
                string info = $"{prefix}: {commandMessage}";
                Config.TcpLogger.Debug( info );
            }
        }

        /// <summary>
        ///     初始化调试的代理
        /// </summary>
        public static void InitFake4Test()
        {
            _datasamplerConfigDto = new DatasamplerConfigDto {  };
        }
        /// <summary>
        ///     初始化调试的代理
        /// </summary>
        public static void InitDebugHandler()
        {
            //if (EnvironmentUtils.IsDebug)
            {
                SocketLibConfig.SendBytesEvent += SocketLibConfig_SendBytesEvent;
                SocketLibConfig.ReceiveBytesEvent += SocketLibConfig_ReceiveBytesEvent;

                SampleStationProxy.SendCommandMessage = Config.LogCommandMessage;
                SampleStationProxy.ReceiveCommandMessage = Config.LogCommandMessage;
            }
        }

        /// <summary>
        ///     响应接收了字节的事件
        /// </summary>
        private static void SocketLibConfig_ReceiveBytesEvent(SocketWrapper socket, byte[] buffer, int offset, int size)
        {
            if (!DatasamplerConfigDto.PrintTcpMessage) return;

            string hex = StringUtils.ToHex(buffer, offset, size);
            Config.TcpLogger.Debug(string.Format("{1} receive data: ({0})", hex, socket.IPPortInfo));
        }

        /// <summary>
        ///     响应发送了字节的事件
        /// </summary>
        private static void SocketLibConfig_SendBytesEvent(SocketWrapper socket, byte[] buffer, int offset, int size)
        {
            if (!DatasamplerConfigDto.PrintTcpMessage) return;

            string hex = StringUtils.ToHex(buffer, offset, size);
            Config.TcpLogger.Debug(string.Format("{1} send data: ({0})", hex, socket.IPPortInfo));
        }

        #endregion

        #region 探针数据对象

        /// <summary>
        /// 采集工作站的探针数据对象
        /// </summary>
        private static readonly DataSamplerProbeData _probe = new DataSamplerProbeData();

        /// <summary>
        /// 采集工作站的探针数据对象
        /// </summary>
        public static DataSamplerProbeData Probe
        {
            get { return _probe; }
        }

        /// <summary>
        /// Tcp Logger Name
        /// </summary>
        public const string TcpLoggerName = "TcpLog";

        /// <summary>
        /// 记录tcp通讯
        /// </summary>
        public static ILogNet TcpLogger { get; set; }

        /// <summary>
        /// 记录一般日志
        /// </summary>
        public static ILogNet Logger { get; set; }

        public static void LogTcp(string prefix, ArraySegment<byte> ioBuf )
        {
            TcpLogger.Info($"{prefix}: iobuf:offset:{ioBuf.Offset},count:{ioBuf.Count}.");
        }
        public static void LogTcp(string message)
        {
            TcpLogger.Info(message);
        }
        public static string GetContextInfo(DotNetty.Transport.Channels.IChannelHandlerContext context)
        {
            return context.Channel.ToString();
        }

        #endregion

        #region 更多的配置信息

        /// <summary>
        /// 采集参数配置文件
        /// </summary>
        private const string DataSamplerConfigFileName = "DataSamplerConfig.xml";

        /// <summary>
        /// 采集参数配置文件路径
        /// </summary>
        private static string DataSamplerConfigFilePath
        {
            get { return DataSamplerConfigFileName; }
        }

        /// <summary>
        /// true：采集参数配置文件存在，false：不存在
        /// </summary>
        internal static bool DataSamplerConfigFileExists
        {
            get { return File.Exists( DataSamplerConfigFilePath ); }
        }

        #region 数据采集器参数配置文件相关

        /// <summary>
        /// 更新数据采集器配置文件
        /// </summary>
        /// <param name="samplerConfigData">SamplerConfigData</param>
        internal static void UpdateSamplerConfigFile( SamplerConfigData samplerConfigData )
        {
            XmlUtils.XmlSerialize2File( DataSamplerConfigFilePath, samplerConfigData );
        }

        /// <summary>
        /// 删除数据采集器配置文件
        /// </summary>
        internal static void DeleteSamplerConfigFile()
        {
            File.Delete( DataSamplerConfigFilePath );
        }

        /// <summary>
        /// 读取数据采集器配置信息
        /// </summary>
        /// <returns>SamplerConfigData</returns>
        internal static SamplerConfigData ReadSamplerConfig()
        {
            return SamplerConfig;
            //return XmlUtils.XmlDeserializeFromFile<SamplerConfigData>( DataSamplerConfigFilePath );
        }

        internal static SamplerConfigData SamplerConfig { get; set; }

        /// <summary>
        /// datasampler 配置数据
        /// </summary>
        internal static DatasamplerConfigDto DatasamplerConfigDto => _datasamplerConfigDto ??
            (_datasamplerConfigDto = IocUtils.Instance.GetRequiredService<IOptions<DatasamplerConfigDto>>().Value);

        private static DatasamplerConfigDto _datasamplerConfigDto;

        #endregion

        #region 发送数据对象

        /// <summary>
        /// 发送数据对象
        /// </summary>
        internal static ITaskSender SampleDataSender => _sampleDataSender ??
            (_sampleDataSender = IocUtils.Instance.GetRequiredService<ITaskSender>());

        private static ITaskSender _sampleDataSender;

        #endregion

        #region json 序列化接口

        /// <summary>
        /// json 序列化接口对象
        /// </summary>
        internal static IJsonSerializer JsonSerializer => _jsonSerializer ??
            (_jsonSerializer = IocUtils.Instance.GetRequiredService<IJsonSerializer>());

        private static IJsonSerializer _jsonSerializer;

        #endregion

        #region 是否使用软件计算转速

        /// <summary>
        /// True：计算转速，False：不计算
        /// </summary>
        internal static bool CalculateRev
        {
            get { return false; }
        }

        #endregion

        #endregion

        #region 与Socket相关

        /// <summary>
        /// 本地侦听的端口，用于接受数据，默认1283
        /// </summary>
        internal static int ListenPortOfData => DatasamplerConfigDto.ListenPortOfData;

        /// <summary>
        /// 本地侦听的端口，用于采集工作站TCP控制链接的接入，默认1284
        /// </summary>
        internal static int ListenPortOfControl => DatasamplerConfigDto.ListenPortOfControl;

        /// <summary>
        /// 发送和接收超时，以毫秒表示，默认为20000（20秒）
        /// </summary>
        internal static int SendReceiveTimeoutInMs => DatasamplerConfigDto.SendReceiveTimeoutInMs;

        /// <summary>
        /// 正常采集时，队列中对象数量的最大值
        /// </summary>
        internal static int MaxQueueLength_NormalSample => DatasamplerConfigDto.MaxQueueLength_NormalSample;

        /// <summary>
        /// 创建Socket连接
        /// </summary>
        /// <param name="ipAddress">IP地址</param>
        /// <returns>SocketWrapper</returns>
        internal static SocketWrapper CreateSocket( IPAddressData ipAddress )
        {
            SocketWrapper socketWrapper = SocketWrapper.CreateTcpSocket();
            try
            {
                socketWrapper.DirectConnect( ipAddress.IP, ipAddress.Port );
                socketWrapper.SendTimeout = socketWrapper.ReceiveTimeout = SendReceiveTimeoutInMs;

                Config.TcpLogger.Debug( string.Format( "Config.CreateSocket succeed({0}, {1})", socketWrapper.IPPortInfo,
                    socketWrapper.SendReceiveTimeoutInfo ) );

                return socketWrapper;
            }
            catch
            {
                using( socketWrapper )
                {
                }
                throw;
            }
        }

        #endregion

        #region 初始化读写结构的代理

        /// <summary>
        ///     初始化读写结构的代理，这个函数在硬件测试工具中也被调用
        /// </summary>
        public static void InitStructReadWriteHandler()
        {
            HashDictionary<int, Func20<ToFromBytesUtils, object>> readHandlers =
                SampleDataReadWrite.StructTypeID2ReadHandler;
            ToFromBytesUtils.SetStructTypeID2CustomDataReaders( CollectionUtils.ToArray( readHandlers.Keys ),
                                                                CollectionUtils.ToArray( readHandlers.Values ) );

            HashDictionary<int, Action20<object, ToFromBytesUtils>> writeHandlers =
                SampleDataReadWrite.StructTypeID2WriteHandler;
            ToFromBytesUtils.SetStructTypeID2CustomDataWriters( CollectionUtils.ToArray( writeHandlers.Keys ),
                                                                CollectionUtils.ToArray( writeHandlers.Values ) );
        }

        #endregion

        #region 初始化固件升级结构

        /// <summary>
        /// 固件文件名对固件文件信息对象的映射
        /// </summary>
        private static readonly IgnoreCaseKeyDictionary<FirmwareFileInfoData> _firmwareFileName2InfoDatas = new IgnoreCaseKeyDictionary<FirmwareFileInfoData>();

        /// <summary>
        ///     初始化固件升级结构
        /// </summary>
        internal static void InitFirmware4Upgrade()
        {
            var firmware4UpgradeDir = AppPath.GetPath( "Firmware4Upgrade" );
            if (!Directory.Exists(firmware4UpgradeDir)) return;

            foreach ( var dir in Directory.EnumerateDirectories( firmware4UpgradeDir ) )
            {
                try
                {
                    var firmwareFileName = CharCaseUtils.ToCase( Path.GetFileName( dir ) );

                    FirmwareFileInfoData fileInfoData = null;
                    foreach( var filePath in Directory.EnumerateFiles( dir ) )
                    {
                        var version = Path.GetFileName( filePath );

                        if( ( fileInfoData == null ) ||
                            ( VersionUtils.IsVersionGreater( version, fileInfoData.FirmwareFileVersion ) ) )
                        {
                            fileInfoData = new FirmwareFileInfoData
                            {
                                FirmwareFileName = firmwareFileName,
                                FirmwareFileVersion = version,
                                FirmwareFilePath = filePath
                            };
                        }
                    }

                    if( fileInfoData != null )
                    {
                        var filePath = fileInfoData.FirmwareFilePath;
                        fileInfoData.FirmwareFileBytes = File.ReadAllBytes( filePath );

                        lock( _firmwareFileName2InfoDatas )
                        {
                            _firmwareFileName2InfoDatas[firmwareFileName] = fileInfoData;
                        }

                        TraceUtils.Info( string.Format( "Init firmwarefile({0}) done.", filePath ) );
                    }
                }
                catch( Exception ex )
                {
                    TraceUtils.Error( string.Format( "Init firmwarefile({0}) error.", dir ), ex );
                }
            }
        }

        /// <summary>
        /// 根据固件文件名获得固件文件信息对象
        /// </summary>
        /// <param name="firmwareFileName">固件文件名</param>
        /// <returns>固件文件信息对象</returns>
        internal static FirmwareFileInfoData GetFirmwareFileInfoDataByName( string firmwareFileName )
        {
            lock( _firmwareFileName2InfoDatas )
            {
                return _firmwareFileName2InfoDatas[firmwareFileName];
            }
        }

        #endregion

        #region 推送固件的方式

        /// <summary>
        /// 默认值，推送
        /// </summary>
        internal static string PushFirmwareFileMode_Push = "Push";

        /// <summary>
        /// 工作站作为客户端主动获取
        /// </summary>
        internal static string PushFirmwareFileMode_Pull = "Pull";

        /// <summary>
        /// Push：默认值，推送，Pull：工作站作为客户端主动获取
        /// </summary>
        internal static string PushFirmwareFileMode
        {
            get { return DatasamplerConfigDto.PushFirmwareFileMode ?? PushFirmwareFileMode_Push; }
        }

        #endregion
    }
}