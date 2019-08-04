﻿using System;
using System.Collections.Generic;
using System.IO;
using AnalysisData.SampleData;
using AnalysisData.ToFromBytes;
using Moons.Common20;
using Moons.Common20.Serialization;
using SocketLib;

namespace DataSampler.Helper
{
    /// <summary>
    /// 采集工作站代理类
    /// </summary>
    public class SampleStationProxy
    {
        public SampleStationProxy()
        {
            SampleStationData = new SampleStationData
                                    { SampleStationIP = Config.TestSampleStationIP, SampleStationPort = 1283 };
        }

        #region 变量和属性

        /// <summary>
        /// SampleStationData
        /// </summary>
        private SampleStationData _sampleStationData;

        /// <summary>
        /// SampleStationData
        /// </summary>
        public SampleStationData SampleStationData
        {
            get { return _sampleStationData; }
            set
            {
                _sampleStationData = value;
                IPAddress = new IPAddressData { IP = value.SampleStationIP, Port = value.SampleStationPort };
            }
        }

        /// <summary>
        /// IP地址
        /// </summary>
        public IPAddressData IPAddress { get; set; }

        #endregion

        #region 操作采集器

        /// <summary>
        /// 获取采集器状态
        /// </summary>
        public SampleStationStatusData GetSampleStationStatus()
        {
            return GetSampleStationInfo<SampleStationStatusData>( CommandID.GetStatus, "采集器状态" );
        }

        /// <summary>
        /// 获取采集器硬件信息
        /// </summary>
        public SampleStationHardwareInfoData GetHardwareInfo()
        {
            return GetSampleStationInfo<SampleStationHardwareInfoData>( CommandID.GetHardwareInfo, "采集器硬件信息" );
        }

        /// <summary>
        ///     获取采集器调试信息
        /// </summary>
        public string GetHardwareDebugInfo()
        {
            return GetSampleStationInfo<string>( CommandID.GetHardwareDebugInfo, "采集器调试信息" );
        }

        /// <summary>
        /// 获取硬件的相关信息
        /// </summary>
        /// <typeparam name="T">信息对象类型</typeparam>
        /// <param name="commandID">命令ID</param>
        /// <param name="infoMame">信息名</param>
        /// <returns>信息对象</returns>
        private T GetSampleStationInfo<T>( int commandID, string infoMame ) where T : class
        {
            try
            {
                CommandMessage sendCommand = SampleDataReadWrite.CreateCommand( commandID );
                return SendReceiveCommand( sendCommand ).Data as T;
            }
            catch( Exception ex )
            {
                TraceUtils.Error( string.Format( "获取{0}出错！", infoMame ), ex );
                throw;
            }
        }

        /// <summary>
        /// 开始正常采集
        /// </summary>
        public void StartNormalSample()
        {
            try
            {
                CommandMessage sendCommand = SampleDataReadWrite.CreateCommand( CommandID.StartNormalSample );
                SendReceiveCommand( sendCommand );
            }
            catch( Exception ex )
            {
                TraceUtils.Error( "开始正常采集出错！", ex );
                throw;
            }
        }

        /// <summary>
        /// 停止采集
        /// </summary>
        public void StopSample()
        {
            try
            {
                CommandMessage sendCommand = SampleDataReadWrite.CreateCommand( CommandID.StopSample );
                SendReceiveCommand( sendCommand );
            }
            catch( Exception ex )
            {
                TraceUtils.Error( "停止采集出错！", ex );
                throw;
            }
        }

        /// <summary>
        /// 下载采集配置参数
        /// </summary>
        /// <param name="error">错误集合</param>
        public void DownloadSampleConfig( ref SampleStationParameterErrorDataCollection error )
        {
            if( EnvironmentUtils.IsDebug )
            {
                TryCatchUtils.Catch(
                    () =>
                    XmlUtils.XmlSerialize2File(
                        "SampleStationData.debug.xml" ,
                        SampleStationData )
                    );
            }

            try
            {
                // 选择结构体ID
                int structTypeId;
                switch( SampleStationData.StationType )
                {
                    case SampleStationType.Ms1000:
                    case SampleStationType.Wg100_780M:
                        structTypeId = StructTypeID.SampleStationConfigData2;
                        break;

                    default:
                    case SampleStationType.Default:
                    case SampleStationType.Wg100:
                        structTypeId = StructTypeID.SampleStationConfigData;
                        break;
                }

                var sendCommand = new CommandMessage
                                      {
                                          CommandID = CommandID.DownloadSampleConfig,
                                          StructTypeID = structTypeId,
                                          Data = SampleStationData
                                      };

                CommandMessage receiveCommand = SendReceiveCommand( sendCommand );
                if( receiveCommand.Data is SampleStationParameterErrorDataCollection )
                {
                    error = receiveCommand.Data as SampleStationParameterErrorDataCollection;
                }
            }
            catch( Exception ex )
            {
                TraceUtils.Error( "下载采集配置参数出错！", ex );
                throw;
            }
        }

        /// <summary>
        /// 采集器校时
        /// </summary>
        /// <param name="now">当前时间</param>
        public void Timing( DateTime now )
        {
            try
            {
                var sendCommand = new CommandMessage
                                      {
                                          CommandID = CommandID.Timing,
                                          StructTypeID = StructTypeID.TimingData,
                                          Data = new TimingData { Now = now }
                                      };

                SendReceiveCommand( sendCommand );
            }
            catch( Exception ex )
            {
                TraceUtils.Error( "采集器校时出错！", ex );
                throw;
            }
        }

        /// <summary>
        /// 采集额外的测点波形数据
        /// </summary>
        /// <param name="sampleExtraPointData">SampleExtraPointData</param>
        public void SampleExtraPointData( SampleExtraPointData sampleExtraPointData )
        {
            try
            {
                var sendCommand = new CommandMessage
                    {
                        CommandID = CommandID.SampleExtraDatas,
                        StructTypeID = StructTypeID.SampleExtraPointData,
                        Data = sampleExtraPointData
                    };

                SendReceiveCommand( sendCommand );
            }
            catch( Exception ex )
            {
                TraceUtils.Error( string.Format( "采集额外的测点({0})波形数据出错！", sampleExtraPointData ), ex );
                throw;
            }
        }

        /// <summary>
        ///     报警灯复位
        /// </summary>
        public void ResetAlmLight()
        {
            try
            {
                CommandMessage sendCommand = SampleDataReadWrite.CreateCommand( CommandID.ResetAlmLight );
                SendReceiveCommand( sendCommand );
            }
            catch( Exception ex )
            {
                TraceUtils.Error( "报警灯复位！", ex );
                throw;
            }
        }

        #region 用于：现场实施工具
        /// <summary>
        ///     现场实施工具使用，设置采集工作站用户IP地址
        /// </summary>
        /// <param name="ipBytes">表示IP地址、掩码、网关、服务器IP的字节数组</param>
        /// <returns>命令是否执行成功</returns>
        public bool SetUserIpAddress(byte[] ipBytes)
        {
            try
            {
                var sendCommand = new CommandMessage
                    {
                        CommandID = CommandID.SetUserIpAddress,
                        StructTypeID = StructTypeID.SampleStationConfigData,
                        Data = ipBytes
                    };
                return SendReceiveCommandBytes(sendCommand);
            }
            catch (Exception ex)
            {
                TraceUtils.Error("设置采集工作站用户IP地址！", ex);
                throw;
            }
        }

        /// <summary>
        ///     现场实施工具使用，采集板校零
        /// </summary>
        /// <returns>命令是否执行成功</returns>
        public bool CalibrateZero()
        {
            try
            {
                var sendCommand = new CommandMessage
                    {
                        CommandID = CommandID.CalibrateZero,
                        StructTypeID = StructTypeID.SampleStationConfigData,
                        Data = null
                    };
                return SendReceiveCommandBytes( sendCommand );
            }
            catch( Exception ex )
            {
                TraceUtils.Error( "采集板校零！", ex );
                throw;
            }
        }

        /// <summary>
        ///     现场实施工具使用，复位MCM
        /// </summary>
        /// <returns>命令是否执行成功</returns>
        public bool Reset()
        {
            try
            {
                var sendCommand = new CommandMessage
                    {
                        CommandID = CommandID.Reset,
                        StructTypeID = StructTypeID.SampleStationConfigData,
                        Data = null
                    };
                return SendReceiveCommandBytes( sendCommand );
            }
            catch( Exception ex )
            {
                TraceUtils.Error( "复位MCM！", ex );
                throw;
            }
        }

        /// <summary>
        ///     现场实施工具使用，复位SMA
        /// </summary>
        /// <returns>命令是否执行成功</returns>
        public bool ResetSMA()
        {
            try
            {
                var sendCommand = new CommandMessage
                    {
                        CommandID = CommandID.ResetSMA,
                        StructTypeID = StructTypeID.SampleStationConfigData,
                        Data = null
                    };
                return SendReceiveCommandBytes( sendCommand );
            }
            catch( Exception ex )
            {
                TraceUtils.Error( "复位SMA！", ex );
                throw;
            }
        }

        #endregion

        #endregion

        #region 获取数据

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="pointIDs">测点编号</param>
        /// <returns>数据</returns>
        public List<TrendData> GetData( int[] pointIDs )
        {
            try
            {
                var ret = new List<TrendData>();
                using( SocketWrapper socketWrapper = CreateSocket() )
                {
                    PackageSendReceive sendReceive = PackageSendReceive.CreatePackageSendReceive( socketWrapper );

                    bool isOk;
                    ret.AddRange( GetTrendData( sendReceive, out isOk ) );

                    if( !isOk )
                    {
                        return ret;
                    }

                    if( CollectionUtils.IsNotEmpty( pointIDs ) )
                    {
                        ret.AddRange( GetWaveData( sendReceive, pointIDs, out isOk ) );
                    }
                }
                return ret;
            }
            catch( Exception ex )
            {
                TraceUtils.Error( "GetData error.", ex );
                return new List<TrendData>();
            }
        }

        /// <summary>
        /// 获取趋势数据
        /// </summary>
        /// <param name="sendReceive">PackageSendReceive</param>
        /// <param name="isOk">true：未发生异常，false：发生了异常</param>
        /// <returns>趋势数据</returns>
        private static IEnumerable<TrendData> GetTrendData( PackageSendReceive sendReceive, out bool isOk )
        {
            CommandMessage sendCommand = SampleDataReadWrite.CreateCommand( CommandID.UploadTrendData );
            return GetData( sendReceive, sendCommand, out isOk );
        }

        /// <summary>
        /// 获取波形数据
        /// </summary>
        /// <param name="sendReceive">PackageSendReceive</param>
        /// <param name="pointIDs">测点编号集合</param>
        /// <param name="isOk">true：未发生异常，false：发生了异常</param>
        /// <returns>波形数据</returns>
        private static IEnumerable<TrendData> GetWaveData( PackageSendReceive sendReceive, int[] pointIDs, out bool isOk )
        {
            var sendCommand = new CommandMessage
                                  {
                                      CommandID = CommandID.UploadWaveData,
                                      StructTypeID = StructTypeID.PointIDCollectionData,
                                      Data = pointIDs
                                  };
            return GetData( sendReceive, sendCommand, out isOk );
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="sendReceive">PackageSendReceive</param>
        /// <param name="sendCommand">发送的命令</param>
        /// <param name="isOk">true：未发生异常，false：发生了异常</param>
        /// <returns>数据</returns>
        private static IEnumerable<TrendData> GetData( PackageSendReceive sendReceive, CommandMessage sendCommand,
                                                       out bool isOk )
        {
            try
            {
                isOk = true;

                sendReceive.SendPackage( ToFromBytesUtils.ToBytes( sendCommand ) );

                var ret = new List<TrendData>();
                bool isContinue = true;
                while( isContinue )
                {
                    // 是否回复成功，大部分都回复，只有集合结束标记不回复。
                    bool sendSuccessReplay = true;
                    CommandMessage command = ReceiveCommand( sendReceive );
                    switch( command.CommandID )
                    {
                        case CommandID.SampleDataCollectionBegin:
                            break;

                        case CommandID.SampleDataCollectionEnd:
                            sendSuccessReplay = false;
                            isContinue = false;
                            break;

                        case CommandID.SampleDatas:
                            var data = command.Data as TrendData;
                            ret.Add( data );
                            break;

                        default:
                            sendReceive.SendPackage(
                                ToFromBytesUtils.ToBytes( SampleDataReadWrite.FailCommand_PackageCommand ) );
                            throw new SampleStationException( "获取数据错误！" )
                                      { ErrorCode = SampleStationErrorCode.PackageCommand };
                    }

                    if( sendSuccessReplay )
                    {
                        sendReceive.SendPackage( ToFromBytesUtils.ToBytes( SampleDataReadWrite.SuccessCommand ) );
                    }
                }
                return ret;
            }
            catch( Exception ex )
            {
                isOk = false;
                TraceUtils.Error( "获取数据错误！", ex );
                return new List<TrendData>();
            }
        }

        #endregion

        #region 固件升级相关函数

        /// <summary>
        ///     推送(固件)文件的字节数组
        /// </summary>
        /// <param name="data">FirmwareFileInfoData</param>
        public void PushFirmwareFileBytes( FirmwareFileInfoData data )
        {
            try
            {
                // 由于下位机缓存的限制，纪秀范提出每次发出的文件字节数长度为16K。
                const int PackageSize = 32768;

                Action<PackageSendReceive> handler = sendReceive =>
                {
                    var fileBytes = data.FirmwareFileBytes;
                    var fileLength = fileBytes.Length;
                    var restFileLength = fileLength;
                    var segmentIndex = 0;
                    var segmentOffset = 0;

                    while( 0 < restFileLength )
                    {
                        var segmentLength = PackageSize < restFileLength ? PackageSize : restFileLength;
                        var segmentData = ArrayUtils.Slice( fileBytes, segmentOffset, segmentLength );

                        var sendCommand = new CommandMessage
                        {
                            CommandID = CommandID.PushFileBytes,
                            StructTypeID = StructTypeID.FileSegmentData,
                            Data = new FileSegmentData
                            {
                                Parameters = data.FirmwareFileName,
                                FileLength = fileLength,
                                SegmentIndex = segmentIndex,
                                SegmentOffset = segmentOffset,
                                SegmentData = segmentData
                            }
                        };

                        segmentIndex++;
                        segmentOffset += segmentLength;
                        restFileLength -= segmentLength;

                        sendReceive.SendPackage( ToFromBytesUtils.ToBytes( sendCommand ) );

                        ReceiveCommand( sendReceive );
                    }
                };

                HandlePackageSendReceive( handler );
            }
            catch( Exception ex )
            {
                TraceUtils.Error( "推送(固件)文件的字节数组出错！", ex );
                throw;
            }
        }

        #endregion

        #region 辅助函数

        /// <summary>
        ///     发送并接收一个命令消息对象，只用于兼容辛磊编写的“HDInitUtility”工具中的指令
        /// </summary>
        /// <param name="sendCommand">发送的命令消息对象</param>
        /// <returns>命令是否执行成功</returns>
        private bool SendReceiveCommandBytes( CommandMessage sendCommand )
        {
            using( SocketWrapper socketWrapper = CreateSocket() )
            {
                PackageSendReceive sendReceive = PackageSendReceive.CreatePackageSendReceive( socketWrapper );

                using( var memoryStream = new MemoryStream() )
                {
                    using( var writer = new BinaryWriter( memoryStream ) )
                    {
                        writer.Write( sendCommand.CommandID );
                        writer.Write( sendCommand.StructTypeID );

                        if( sendCommand.Data != null )
                        {
                            writer.Write( (byte[])sendCommand.Data );
                        }
                    }
                    sendReceive.SendPackage( memoryStream.ToArray() );
                }

                sendReceive.ReceivePackage();
                using( MemoryStream readStream = sendReceive.ReadByteBuffer.CreateReadStream() )
                {
                    using( var reader = new BinaryReader( readStream ) )
                        return CommandID.CommandSuccess == reader.ReadInt32();
                }
            }
        }

        /// <summary>
        ///     发送并接收一个命令消息对象
        /// </summary>
        /// <param name="sendCommand">发送的命令消息对象</param>
        /// <returns>接收的命令消息对象</returns>
        private CommandMessage SendReceiveCommand( CommandMessage sendCommand )
        {
            CommandMessage ret = null;
            Action<PackageSendReceive> handler = sendReceive =>
            {
                sendReceive.SendPackage( ToFromBytesUtils.ToBytes( sendCommand ) );

                ret = ReceiveCommand( sendReceive );
            };
            HandlePackageSendReceive( handler );
            return ret;
        }

        /// <summary>
        /// 处理包的发送与接收
        /// </summary>
        /// <param name="handler">Action[PackageSendReceive]</param>
        private void HandlePackageSendReceive( Action<PackageSendReceive> handler )
        {
            using( var socketWrapper = CreateSocket() )
            {
                var sendReceive = PackageSendReceive.CreatePackageSendReceive( socketWrapper );
                EventUtils.FireEvent( handler, sendReceive );
            }
        }

        /// <summary>
        /// 接收一个命令消息对象
        /// </summary>
        /// <param name="sendReceive">PackageSendReceive</param>
        /// <returns>接收的命令消息对象</returns>
        private static CommandMessage ReceiveCommand( PackageSendReceive sendReceive )
        {
            sendReceive.ReceivePackage();

            return ReadCommandFromBuffer( sendReceive );
        }

        /// <summary>
        /// 从缓冲区读取一个命令消息对象
        /// </summary>
        /// <param name="sendReceive">PackageSendReceive</param>
        /// <returns>接收的命令消息对象</returns>
        public static CommandMessage ReadCommandFromBuffer( PackageSendReceive sendReceive )
        {
            CommandMessage command = ToFromBytesUtils.ReadCommandMessage( sendReceive.ReadByteBuffer );

            OnReceiveCommandMessage( command );

            SampleDataReadWrite.TryRaiseErrorMessage( command );

            return command;
        }

        /// <summary>
        /// 创建Socket对象
        /// </summary>
        private SocketWrapper CreateSocket()
        {
            var ip = IPAddress.IP;
            if( ip.StartsWith( "127." ) )
            {
                var map = DataSamplerController.Instance._ip2SocketOfControl;
                lock (map)
                {
                    if ( map.ContainsKey( ip ) ) {
                        var socket = map[ip];
                        socket.UseSocket();
                        return socket;
                    }
                }
                throw new ApplicationException( $"No tcp connection({ip})." );
            }

            return Config.CreateSocket( IPAddress );
        }

        #endregion

        #region 调试信息

        /// <summary>
        /// 收到CommandMessage调用的函数代理
        /// </summary>
        public static Action<CommandMessage> ReceiveCommandMessage { private get; set; }

        /// <summary>
        /// 收到CommandMessage调用的函数
        /// </summary>
        private static void OnReceiveCommandMessage( CommandMessage commandMessage )
        {
            Action<CommandMessage> handler = ReceiveCommandMessage;
            if( handler != null )
            {
                handler( commandMessage );
            }
        }

        /// <summary>
        /// 发送AlmEventData对象，用于测试
        /// </summary>
        public void SendAlmEventData_Test()
        {
            try
            {
                using( SocketWrapper socketWrapper = CreateSocket() )
                {
                    var almEventData = new AlmEventData
                                           {
                                               AlmLevel = 1,
                                               AlmSourceID = 2,
                                               AlmTime = DateTime.Now,
                                               AlmEventUniqueID = "UniqueID"
                                           };

                    var sendCommand = new CommandMessage
                                          {
                                              CommandID = CommandID.AlmEventDatas,
                                              StructTypeID = StructTypeID.AlmEventData,
                                              Data = almEventData
                                          };

                    PackageSendReceive sendReceive = PackageSendReceive.CreatePackageSendReceive( socketWrapper );
                    sendReceive.SendPackage( ToFromBytesUtils.ToBytes( sendCommand ) );
                }
            }
            catch( Exception ex )
            {
                TraceUtils.Error( "发送AlmEventData对象出错！", ex );
            }
        }

        #endregion
    }
}