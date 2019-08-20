using System;
using System.IO;
using AnalysisData.SampleData;
using AnalysisData.ToFromBytes;
using DataSampler.Core.Dto;
using DataSampler.Helper;
using Microsoft.Extensions.Options;
using Moons.Common20;
using Moons.Common20.IOC;
using Moons.Common20.Serialization;
using OnlineSampleCommon.SendTask;
using SocketLib;

namespace DataSampler
{
    /// <summary>
    /// ���ù�����
    /// </summary>
    public static class Config
    {
        #region ���Բ���

        /// <summary>
        /// ���ڲ��ԵĲɼ�����վ��IP
        /// </summary>
        internal static string TestSampleStationIP => "127.0.0.1";

        /// <summary>
        /// ��¼CommandMessage����
        /// </summary>
        /// <param name="commandMessage">CommandMessage</param>
        internal static void LogCommandMessage( CommandMessage commandMessage )
        {
            if(EnvironmentUtils.IsDebug)
            {
                string info = string.Format( "commandMessage: {0}", commandMessage );
                TraceUtils.Info( info );
            }
        }

        /// <summary>
        ///     ��ʼ�����ԵĴ���
        /// </summary>
        public static void InitDebugHandler()
        {
            if (EnvironmentUtils.IsDebug)
            {
                SocketLibConfig.SendBytesEvent += SocketLibConfig_SendBytesEvent;
                SocketLibConfig.ReceiveBytesEvent += SocketLibConfig_ReceiveBytesEvent;

                SampleStationProxy.ReceiveCommandMessage = Config.LogCommandMessage;
            }
        }

        /// <summary>
        ///     ��Ӧ�������ֽڵ��¼�
        /// </summary>
        private static void SocketLibConfig_ReceiveBytesEvent(SocketWrapper socket, byte[] buffer, int offset, int size)
        {
            string hex = StringUtils.ToHex(buffer, offset, size);
            TraceUtils.Info(string.Format("{1} receive data: ({0})", hex, socket.IPPortInfo));
        }

        /// <summary>
        ///     ��Ӧ�������ֽڵ��¼�
        /// </summary>
        private static void SocketLibConfig_SendBytesEvent(SocketWrapper socket, byte[] buffer, int offset, int size)
        {
            string hex = StringUtils.ToHex(buffer, offset, size);
            TraceUtils.Info(string.Format("{1} send data: ({0})", hex, socket.IPPortInfo));
        }

        #endregion

        #region ̽�����ݶ���

        /// <summary>
        /// �ɼ�����վ��̽�����ݶ���
        /// </summary>
        private static readonly DataSamplerProbeData _probe = new DataSamplerProbeData();

        /// <summary>
        /// �ɼ�����վ��̽�����ݶ���
        /// </summary>
        internal static DataSamplerProbeData Probe
        {
            get { return _probe; }
        }

        #endregion

        #region �����������Ϣ

        /// <summary>
        /// �ɼ����������ļ�
        /// </summary>
        private const string DataSamplerConfigFileName = "DataSamplerConfig.xml";

        /// <summary>
        /// �ɼ����������ļ�·��
        /// </summary>
        private static string DataSamplerConfigFilePath
        {
            get { return DataSamplerConfigFileName; }
        }

        /// <summary>
        /// true���ɼ����������ļ����ڣ�false��������
        /// </summary>
        internal static bool DataSamplerConfigFileExists
        {
            get { return File.Exists( DataSamplerConfigFilePath ); }
        }

        #region ���ݲɼ������������ļ����

        /// <summary>
        /// �������ݲɼ��������ļ�
        /// </summary>
        /// <param name="samplerConfigData">SamplerConfigData</param>
        internal static void UpdateSamplerConfigFile( SamplerConfigData samplerConfigData )
        {
            XmlUtils.XmlSerialize2File( DataSamplerConfigFilePath, samplerConfigData );
        }

        /// <summary>
        /// ɾ�����ݲɼ��������ļ�
        /// </summary>
        internal static void DeleteSamplerConfigFile()
        {
            File.Delete( DataSamplerConfigFilePath );
        }

        /// <summary>
        /// ��ȡ���ݲɼ���������Ϣ
        /// </summary>
        /// <returns>SamplerConfigData</returns>
        internal static SamplerConfigData ReadSamplerConfig()
        {
            return SamplerConfig;
            //return XmlUtils.XmlDeserializeFromFile<SamplerConfigData>( DataSamplerConfigFilePath );
        }

        internal static SamplerConfigData SamplerConfig { get; set; }

        /// <summary>
        /// datasampler ��������
        /// </summary>
        internal static DatasamplerConfigDto DatasamplerConfigDto => _datasamplerConfigDto ??
            (_datasamplerConfigDto = IocUtils.Instance.GetRequiredService<IOptions<DatasamplerConfigDto>>().Value);

        private static DatasamplerConfigDto _datasamplerConfigDto;

        #endregion

        #region �������ݶ���

        /// <summary>
        /// �������ݶ���
        /// </summary>
        internal static ITaskSender SampleDataSender => _sampleDataSender ??
            (_sampleDataSender = IocUtils.Instance.GetRequiredService<ITaskSender>());

        private static ITaskSender _sampleDataSender;

        #endregion

        #region �Ƿ�ʹ���������ת��

        /// <summary>
        /// True������ת�٣�False��������
        /// </summary>
        internal static bool CalculateRev
        {
            get { return false; }
        }

        #endregion

        #endregion

        #region ��Socket���

        /// <summary>
        /// ���������Ķ˿ڣ����ڽ������ݣ�Ĭ��1283
        /// </summary>
        internal static int ListenPortOfData => DatasamplerConfigDto.ListenPortOfData;

        /// <summary>
        /// ���������Ķ˿ڣ����ڲɼ�����վTCP�������ӵĽ��룬Ĭ��1284
        /// </summary>
        internal static int ListenPortOfControl => DatasamplerConfigDto.ListenPortOfControl;

        /// <summary>
        /// ���ͺͽ��ճ�ʱ���Ժ����ʾ��Ĭ��Ϊ20000��20�룩
        /// </summary>
        internal static int SendReceiveTimeoutInMs => DatasamplerConfigDto.SendReceiveTimeoutInMs;

        /// <summary>
        /// �����ɼ�ʱ�������ж������������ֵ
        /// </summary>
        internal static int MaxQueueLength_NormalSample => DatasamplerConfigDto.MaxQueueLength_NormalSample;

        /// <summary>
        /// ����Socket����
        /// </summary>
        /// <param name="ipAddress">IP��ַ</param>
        /// <returns>SocketWrapper</returns>
        internal static SocketWrapper CreateSocket( IPAddressData ipAddress )
        {
            SocketWrapper socketWrapper = SocketWrapper.CreateTcpSocket();
            try
            {
                socketWrapper.DirectConnect( ipAddress.IP, ipAddress.Port );
                socketWrapper.SendTimeout = socketWrapper.ReceiveTimeout = SendReceiveTimeoutInMs;

                TraceUtils.LogDebugInfo( string.Format( "Config.CreateSocket succeed({0}, {1})", socketWrapper.IPPortInfo,
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

        #region ��ʼ����д�ṹ�Ĵ���

        /// <summary>
        ///     ��ʼ����д�ṹ�Ĵ������������Ӳ�����Թ�����Ҳ������
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

        #region ��ʼ���̼������ṹ

        /// <summary>
        /// �̼��ļ����Թ̼��ļ���Ϣ�����ӳ��
        /// </summary>
        private static readonly IgnoreCaseKeyDictionary<FirmwareFileInfoData> _firmwareFileName2InfoDatas = new IgnoreCaseKeyDictionary<FirmwareFileInfoData>();

        /// <summary>
        ///     ��ʼ���̼������ṹ
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
        /// ���ݹ̼��ļ�����ù̼��ļ���Ϣ����
        /// </summary>
        /// <param name="firmwareFileName">�̼��ļ���</param>
        /// <returns>�̼��ļ���Ϣ����</returns>
        internal static FirmwareFileInfoData GetFirmwareFileInfoDataByName( string firmwareFileName )
        {
            lock( _firmwareFileName2InfoDatas )
            {
                return _firmwareFileName2InfoDatas[firmwareFileName];
            }
        }

        #endregion

        #region ���͹̼��ķ�ʽ

        /// <summary>
        /// Ĭ��ֵ������
        /// </summary>
        internal static string PushFirmwareFileMode_Push = "Push";

        /// <summary>
        /// ����վ��Ϊ�ͻ���������ȡ
        /// </summary>
        internal static string PushFirmwareFileMode_Pull = "Pull";

        /// <summary>
        /// Push��Ĭ��ֵ�����ͣ�Pull������վ��Ϊ�ͻ���������ȡ
        /// </summary>
        internal static string PushFirmwareFileMode
        {
            get { return DatasamplerConfigDto.PushFirmwareFileMode ?? PushFirmwareFileMode_Push; }
        }

        #endregion
    }
}