using System;
using System.Collections.Generic;
using AnalysisData.Constants;
using AnalysisData.SampleData;
using Moons.Common20;
using Moons.Common20.Serialization;
using Moons.Common20.ValueWrapper;

namespace AnalysisData.ToFromBytes
{
    /// <summary>
    ///     采集数据的读写类，此文件中函数操作AlmEventData对象
    /// </summary>
    public class SampleDataReadWrite : DataReadWriteBase
    {
        #region 字段的字节长度

        /// <summary>
        ///     字符串字段的默认字节数量
        /// </summary>
        public const int ByteCount_String = 128;

        /// <summary>
        ///     ChannelCD字段的字节数量
        /// </summary>
        public const int ByteCount_ChannelCD = 8;

        /// <summary>
        ///     报警事件唯一标识字段的字节数量
        /// </summary>
        public const int ByteCount_AlmEventUniqueID = 20;

        #endregion

        #region 读取、写入的函数代理

        /// <summary>
        ///     写入IValueWrappersContainer接口对象的函数代理
        /// </summary>
        private static readonly Action20<object, ToFromBytesUtils> _valueWrappersContainerWriter =
            ( data, bytesUtils ) =>
            CreateSampleDataWriter( bytesUtils ).WriteValueWrappersContainer( data as IValueWrappersContainer );

        /// <summary>
        ///     StructTypeID对读取函数的映射
        /// </summary>
        private static readonly HashDictionary<int, Func20<ToFromBytesUtils, object>> _structTypeID2ReadHandler =
            new HashDictionary<int, Func20<ToFromBytesUtils, object>>
                {
                    { StructTypeID.AlmEventData, bytesUtils => CreateSampleDataReader( bytesUtils ).ReadAlmEventData() },
                    { StructTypeID.ErrorMessage, CreateIValueWrappersContainerReader<ErrorMessageData>() },
                    { StructTypeID.SamplerStatusData, CreateIValueWrappersContainerReader<SampleStationStatusData>() },
                    {
                        StructTypeID.TrendData,
                        bytesUtils => CreateSampleDataReader( bytesUtils ).ReadTrendData()
                    },
                    {
                        StructTypeID.TimeWaveData_1D,
                        bytesUtils => CreateSampleDataReader( bytesUtils ).ReadTimeWaveData_1D()
                    },
                    {
                        StructTypeID.TimeWaveData_2D,
                        bytesUtils => CreateSampleDataReader( bytesUtils ).ReadTimeWaveData_2D()
                    },
                    {
                        StructTypeID.AlmTrendData,
                        bytesUtils => CreateSampleDataReader( bytesUtils ).ReadAlmTrendData()
                    },
                    {
                        StructTypeID.AlmTimeWaveData_1D,
                        bytesUtils => CreateSampleDataReader( bytesUtils ).ReadAlmTimeWaveData_1D()
                    },
                    {
                        StructTypeID.WirelessTrendData,
                        bytesUtils => CreateSampleDataReader( bytesUtils ).ReadWirelessTrendData()
                    },
                    {
                        StructTypeID.VariantTrendDataInt16,
                        bytesUtils => CreateSampleDataReader( bytesUtils ).ReadVariantTrendDataInt16()
                    },
                    {
                        StructTypeID.FeatureValueTrendData,
                        bytesUtils => CreateSampleDataReader( bytesUtils ).ReadFeatureValueTrendData()
                    },
                    {
                        StructTypeID.SamplerHardwareInfoData,
                        bytesUtils => CreateSampleDataReader( bytesUtils ).ReadSampleStationHardwareInfoData()
                    },
                    {
                        StructTypeID.ParameterErrorDataCollection,
                        bytesUtils =>
                        CreateSampleDataReader( bytesUtils ).ReadValueWrappersContainerCollection
                            <SampleStationParameterErrorData, SampleStationParameterErrorDataCollection>()
                    },
                };

        /// <summary>
        ///     StructTypeID对写入函数的映射
        /// </summary>
        private static readonly HashDictionary<int, Action20<object, ToFromBytesUtils>> _structTypeID2WriteHandler =
            new HashDictionary<int, Action20<object, ToFromBytesUtils>>
                {
                    { StructTypeID.AlmEventData, _valueWrappersContainerWriter },
                    { StructTypeID.ErrorMessage, _valueWrappersContainerWriter },
                    { StructTypeID.TimingData, _valueWrappersContainerWriter },
                    { StructTypeID.SampleExtraPointData, _valueWrappersContainerWriter },
                    {
                        StructTypeID.PointIDCollectionData,
                        ( data, bytesUtils ) => bytesUtils.WriteInt32Array( data as int[] )
                    },
                    {
                        StructTypeID.SampleStationConfigData,
                        ( data, bytesUtils ) =>
                        CreateSampleDataWriter( bytesUtils ).WriteSampleStationData( data as SampleStationData )
                    },
                    {
                        StructTypeID.SampleStationConfigData2,
                        ( data, bytesUtils ) =>
                        CreateSampleDataWriter( bytesUtils )
                            .WriteSampleStationData( data as SampleStationData, StructTypeID.SampleStationConfigData2 )
                    },
                    {
                        StructTypeID.FileSegmentData,
                        ( data, bytesUtils ) =>
                        CreateSampleDataWriter( bytesUtils ).WriteFileSegmentData( data as FileSegmentData )
                    },
                };

        /// <summary>
        ///     StructTypeID对读取函数的映射
        /// </summary>
        public static HashDictionary<int, Func20<ToFromBytesUtils, object>> StructTypeID2ReadHandler
        {
            get { return _structTypeID2ReadHandler; }
        }

        /// <summary>
        ///     StructTypeID对写入函数的映射
        /// </summary>
        public static HashDictionary<int, Action20<object, ToFromBytesUtils>> StructTypeID2WriteHandler
        {
            get { return _structTypeID2WriteHandler; }
        }

        #endregion

        #region 辅助函数

        #region 成功、错误命令

        /// <summary>
        ///     成功命令
        /// </summary>
        private static readonly CommandMessage _successCommand = CreateCommand( CommandID.CommandSuccess );

        /// <summary>
        ///     错误信息命令：包命令错误
        /// </summary>
        private static readonly CommandMessage _failCommand_PackageCommand =
            CreateFailCommand( SampleStationErrorCode.PackageCommand );

        /// <summary>
        ///     成功命令
        /// </summary>
        public static CommandMessage SuccessCommand
        {
            get { return _successCommand; }
        }

        /// <summary>
        ///     错误信息命令：包命令错误
        /// </summary>
        public static CommandMessage FailCommand_PackageCommand
        {
            get { return _failCommand_PackageCommand; }
        }

        #endregion

        /// <summary>
        ///     创建用于读取的函数代理
        /// </summary>
        /// <typeparam name="T">实现IValueWrappersContainer的类</typeparam>
        /// <returns>函数代理</returns>
        private static Func20<ToFromBytesUtils, object> CreateIValueWrappersContainerReader<T>()
            where T : IValueWrappersContainer, new()
        {
            return bytesUtils => CreateSampleDataReader( bytesUtils ).ReadValueWrappersContainer<T>();
        }

        /// <summary>
        ///     创建用于读取的SampleDataReadWrite
        /// </summary>
        /// <param name="bytesUtils">ToFromBytesUtils</param>
        /// <returns>用于读取的SampleDataReadWrite</returns>
        private static SampleDataReadWrite CreateSampleDataReader( ToFromBytesUtils bytesUtils )
        {
            return new SampleDataReadWrite { ReadBytesUtils = bytesUtils };
        }

        /// <summary>
        ///     创建用于写入的SampleDataReadWrite
        /// </summary>
        /// <param name="bytesUtils">ToFromBytesUtils</param>
        /// <returns>用于写入的SampleDataReadWrite</returns>
        private static SampleDataReadWrite CreateSampleDataWriter( ToFromBytesUtils bytesUtils )
        {
            return new SampleDataReadWrite { WriteBytesUtils = bytesUtils };
        }

        /// <summary>
        ///     创建命令，默认的StructTypeID、Data指向ErrorMessageData(PackageStructError.None)对象。
        /// </summary>
        /// <param name="commandID">命令ID</param>
        /// <returns>CommandMessage对象</returns>
        public static CommandMessage CreateCommand( int commandID )
        {
            return new CommandMessage
                {
                    CommandID = commandID,
                    StructTypeID = StructTypeID.ErrorMessage,
                    Data = new ErrorMessageData { ErrorCode = SampleStationErrorCode.None }
                };
        }

        /// <summary>
        ///     创建错误信息命令
        /// </summary>
        /// <param name="errorCode">错误码</param>
        /// <returns>错误信息命令</returns>
        public static CommandMessage CreateFailCommand( int errorCode )
        {
            return new CommandMessage
                {
                    CommandID = CommandID.CommandFail,
                    StructTypeID = StructTypeID.ErrorMessage,
                    Data = new ErrorMessageData { ErrorCode = errorCode }
                };
        }

        /// <summary>
        ///     如果CommandMessage对象存在错误则抛出异常
        /// </summary>
        /// <param name="commandMessage">CommandMessage</param>
        /// <returns>CommandMessage</returns>
        public static CommandMessage TryRaiseErrorMessage( CommandMessage commandMessage )
        {
            if( commandMessage.CommandID == CommandID.CommandFail &&
                commandMessage.StructTypeID == StructTypeID.ErrorMessage )
            {
                var errorMessage = (ErrorMessageData)commandMessage.Data;
                if( errorMessage.ErrorCode != SampleStationErrorCode.None )
                {
                    throw new SampleStationException( "SampleStation error." ) { ErrorCode = errorMessage.ErrorCode };
                }
            }

            return commandMessage;
        }


        /// <summary>
        ///     读取CommandMessage对象，如果存在错误则抛出异常
        /// </summary>
        /// <param name="buffer">缓冲区</param>
        /// <returns>CommandMessage</returns>
        public static CommandMessage ReadCommandMessageRaiseErrorMessage( ByteBuffer buffer )
        {
            return TryRaiseErrorMessage( ToFromBytesUtils.ReadCommandMessage( buffer ) );
        }

        #endregion

        #region 具体的读写函数

        #region 留作模板

        ///// <summary>
        ///// 读取AlmEventData对象
        ///// </summary>
        ///// <returns>AlmEventData对象</returns>
        //private AlmEventData ReadAlmEventData()
        //{
        //    return ReadValueWrappersContainer<AlmEventData>();
        //}

        ///// <summary>
        ///// 写入AlmEventData对象
        ///// </summary>
        ///// <param name="data">AlmEventData</param>
        //private void WriteAlmEventData( AlmEventData data )
        //{
        //    WriteValueWrappersContainer( data );
        //}

        ///// <summary>
        ///// 读取AlmEventDataCollection对象
        ///// </summary>
        ///// <returns>AlmEventDataCollection对象</returns>
        //private AlmEventDataCollection ReadAlmEventDataCollection()
        //{
        //    return ReadValueWrappersContainerCollection<AlmEventData, AlmEventDataCollection>();
        //}

        ///// <summary>
        ///// 读取AlmEventDataCollection对象
        ///// </summary>
        ///// <returns>AlmEventDataCollection对象</returns>
        //private void WriteAlmEventDataCollection( AlmEventDataCollection collection )
        //{
        //    WriteValueWrappersContainers( collection );
        //} 

        #endregion

        #region 读硬件的信息

        /// <summary>
        ///     读取采集工作站硬件信息数据
        /// </summary>
        /// <returns>SampleStationHardwareInfoData对象</returns>
        private SampleStationHardwareInfoData ReadSampleStationHardwareInfoData()
        {
            var ret = new SampleStationHardwareInfoData();
            ReadValueWrappersContainer( ret );

            SampleStationChannelHardwareInfoDataCollection channelHardwareInfos = ret.ChannelHardwareInfos;
            ForUtils.ForCount( ReadBytesUtils.ReadInt32(),
                               () => channelHardwareInfos.Add( new SampleStationChannelHardwareInfoData() ) );

            foreach( SampleStationChannelHardwareInfoData hardwareInfoData in channelHardwareInfos )
            {
                ReadValueWrappersContainer( hardwareInfoData );

                // 读取保留字节
                const int ReservedByteCount = 180;
                ReadBytesUtils.ReadBytes( ReservedByteCount );
            }

            return ret;
        }

        #endregion

        #region 发送配置信息

        /// <summary>
        ///     变长参数类
        /// </summary>
        private class VarParameter
        {
            #region 参数ID常量

            #region 1000系列，用于采集工作站的参数

            #endregion

            #region 2000系列，用于测点的参数

            /// <summary>
            ///     测点的工程单位
            ///     ParameterID: 2001
            ///     ByteLength: 4
            ///     参数内容（依次）: Int32类型（工程单位ID）
            /// </summary>
            public const int VarParameterId_PointEngUnit = 2001;

            #endregion

            #region 3000系列，用于通道的参数

            /// <summary>
            ///     传感器灵敏度的工程单位
            ///     ParameterID: 3001
            ///     ByteLength: 4
            ///     参数内容（依次）: Int32类型（工程单位ID）
            /// </summary>
            public const int VarParameterId_ScaleFactorEngUnit = 3001;

            /// <summary>
            ///     中心点电压（电涡流传感器通道参数）
            ///     ParameterID: 3002
            ///     ByteLength: 8
            ///     参数内容（依次）: Single类型（中心点电压），Int32类型（电压的工程单位ID）
            /// </summary>
            public const int VarParameterId_CenterPositionVoltage = 3002;

            /// <summary>
            ///     中心点位置（电涡流传感器通道参数）
            ///     ParameterID: 3003
            ///     ByteLength: 8
            ///     参数内容（依次）: Single类型（中心点位置），Int32类型（位置的工程单位ID）
            /// </summary>
            public const int VarParameterId_CenterPosition = 3003;

            /// <summary>
            ///     线性范围（电涡流传感器通道参数）
            ///     ParameterID: 3004
            ///     ByteLength: 12
            ///     参数内容（依次）: Single类型（线性范围最小值），Single类型（线性范围最大值），Int32类型（线性范围的工程单位ID）
            /// </summary>
            public const int VarParameterId_LinearRange = 3004;

            #endregion

            #endregion

            /// <summary>
            ///     参数ID
            /// </summary>
            public int ParameterID { get; set; }

            /// <summary>
            ///     参数内容
            /// </summary>
            public byte[] Data { get; set; }

            /// <summary>
            ///     初始化参数内容的函数
            /// </summary>
            public Action<IBinaryWrite> DataInitHandler { private get; set; }

            /// <summary>
            ///     初始化参数内容
            /// </summary>
            public void InitData()
            {
                if( DataInitHandler != null )
                {
                    using( var contentWriter = new MemoryWriter() )
                    {
                        EventUtils.FireEvent( DataInitHandler, contentWriter );

                        Data = contentWriter.ToArray();
                    }
                }
            }
        }

        /// <summary>
        ///     写入变长参数集合
        /// </summary>
        /// <param name="bytesUtils">ToFromBytesUtils</param>
        /// <param name="varParameters">变长参数集合</param>
        private void WriteVarParameters( ToFromBytesUtils bytesUtils, ICollection<VarParameter> varParameters )
        {
            bytesUtils.WriteInt32( varParameters.Count );
            foreach( var varParameter in varParameters )
            {
                varParameter.InitData();

                bytesUtils.WriteInt32( varParameter.ParameterID );
                bytesUtils.WriteByteArray( varParameter.Data );
            }
        }

        /// <summary>
        ///     写入SampleStationData对象
        /// </summary>
        /// <param name="data">SampleStationData</param>
        /// <param name="structTypeId">结构类型ID</param>
        private void WriteSampleStationData( SampleStationData data,
                                             int structTypeId = StructTypeID.SampleStationConfigData )
        {
            // 是否为：采集工作站配置数据，版本2
            bool isSampleStationConfigVersion2 = structTypeId == StructTypeID.SampleStationConfigData2;

            ToFromBytesUtils bytesUtils = WriteBytesUtils;

            bytesUtils.WriteIP( data.DataSamplerIP );
            WriteValueWrappersContainer( data );

            if( isSampleStationConfigVersion2 )
            {
                WriteExtraStationConfig_V2( data, bytesUtils );
            }

            PointDataCollection pointDatas = data.PointDatas;
            bytesUtils.WriteInt32( pointDatas.Count );
            foreach( SampleData.PointData pointData in pointDatas )
            {
                WriteValueWrappersContainer( pointData );

                bytesUtils.WriteString( pointData.ChannelCD_1, ByteCount_ChannelCD );
                if( PntDimension.IsTwoDimension( pointData.Dimension ) )
                {
                    bytesUtils.WriteString( pointData.ChannelCD_2, ByteCount_ChannelCD );
                }

                WriteValueWrappersContainers( pointData.AlmStand_CommonSettingDatas );

                if( isSampleStationConfigVersion2 )
                {
                    WriteExtraStationConfig_V2( pointData, bytesUtils );
                }
            }

            ChannelDataCollection channelDatas = data.ChannelDatas;
            bytesUtils.WriteInt32( channelDatas.Count );
            foreach( ChannelData channelData in channelDatas )
            {
                WriteValueWrappersContainer( channelData );

                WriteValueWrappersContainers( channelData.AlmCountDatas );

                if( isSampleStationConfigVersion2 )
                {
                    WriteExtraStationConfig_V2( channelData, bytesUtils );
                }
            }
        }

        /// <summary>
        ///     写入 采集工作站配置数据 版本2 额外的数据
        /// </summary>
        /// <param name="data">SampleStationData</param>
        /// <param name="bytesUtils">ToFromBytesUtils</param>
        private void WriteExtraStationConfig_V2( SampleStationData data, ToFromBytesUtils bytesUtils )
        {
            // 目前采集工作站没有额外的数据，传入空集合
            WriteVarParameters( bytesUtils, new List<VarParameter> {} );
        }

        /// <summary>
        ///     写入 采集工作站配置数据 版本2 额外的数据
        /// </summary>
        /// <param name="pointData">PointData</param>
        /// <param name="bytesUtils">ToFromBytesUtils</param>
        private void WriteExtraStationConfig_V2( SampleData.PointData pointData, ToFromBytesUtils bytesUtils )
        {
            int engUnitId = pointData.EngUnitID;
            WriteVarParameters(
                bytesUtils, new List<VarParameter>
                    {
                        new VarParameter
                            {
                                ParameterID = VarParameter.VarParameterId_PointEngUnit,
                                DataInitHandler = writer => writer.Write( engUnitId )
                            },
                    } );
        }

        /// <summary>
        ///     写入 采集工作站配置数据 版本2 额外的数据
        /// </summary>
        /// <param name="channelData">ChannelData</param>
        /// <param name="bytesUtils">ToFromBytesUtils</param>
        private void WriteExtraStationConfig_V2( ChannelData channelData, ToFromBytesUtils bytesUtils )
        {
            int scaleFactorEngUnitId = channelData.ScaleFactorEngUnitID;

            float centerPositionVoltage = channelData.CenterPositionVoltage;
            int centerPositionVoltageEngUnitId = channelData.CenterPositionVoltageEngUnitID;

            float centerPosition = channelData.CenterPosition;
            int centerPositionEngUnitId = channelData.CenterPositionEngUnitID;

            float linearRangeMin = channelData.LinearRangeMin;
            float linearRangeMax = channelData.LinearRangeMax;
            int linearRangeEngUnitId = channelData.LinearRangeEngUnitID;

            WriteVarParameters(
                bytesUtils, new List<VarParameter>
                    {
                        new VarParameter
                            {
                                ParameterID = VarParameter.VarParameterId_ScaleFactorEngUnit,
                                DataInitHandler = writer => writer.Write( scaleFactorEngUnitId )
                            },
                        new VarParameter
                            {
                                ParameterID = VarParameter.VarParameterId_CenterPositionVoltage,
                                DataInitHandler = writer =>
                                    {
                                        writer.Write( centerPositionVoltage );
                                        writer.Write( centerPositionVoltageEngUnitId );
                                    }
                            },
                        new VarParameter
                            {
                                ParameterID = VarParameter.VarParameterId_CenterPosition,
                                DataInitHandler = writer =>
                                    {
                                        writer.Write( centerPosition );
                                        writer.Write( centerPositionEngUnitId );
                                    }
                            },
                        new VarParameter
                            {
                                ParameterID = VarParameter.VarParameterId_LinearRange,
                                DataInitHandler = writer =>
                                    {
                                        writer.Write( linearRangeMin );
                                        writer.Write( linearRangeMax );
                                        writer.Write( linearRangeEngUnitId );
                                    }
                            },
                    } );
        }

        #endregion

        #region 报警相关的函数

        /// <summary>
        ///     读取AlmEventData对象
        /// </summary>
        /// <returns>SampleData.AlmEventData对象</returns>
        private AlmEventData ReadAlmEventData()
        {
            var ret = new AlmEventData { AlmEventUniqueID = ReadAlmEventUniqueID() };
            ReadValueWrappersContainer( ret );
            return ret;
        }

        /// <summary>
        ///     读取报警的趋势数据（用于数值量报警）
        /// </summary>
        /// <returns>SampleData.TrendData对象</returns>
        private SampleData.TrendData ReadAlmTrendData()
        {
            // 必须要先读报警事件id
            string almEventUniqueID = ReadAlmEventUniqueID();

            SampleData.TrendData ret = ReadTrendData();
            ret.AlmEventUniqueID = almEventUniqueID;
            return ret;
        }

        /// <summary>
        ///     读取报警的一维时间波形数据
        /// </summary>
        /// <returns>TimeWaveData_1D对象</returns>
        private TimeWaveData_1D ReadAlmTimeWaveData_1D()
        {
            // 必须要先读报警事件id
            string almEventUniqueID = ReadAlmEventUniqueID();

            TimeWaveData_1D ret = ReadTimeWaveData_1D();
            ret.AlmEventUniqueID = almEventUniqueID;
            return ret;
        }

        /// <summary>
        ///     读取报警事件唯一ID
        /// </summary>
        /// <returns>报警事件唯一ID</returns>
        private string ReadAlmEventUniqueID()
        {
            byte[] bytes = ReadBytesUtils.ReadBytes( ByteCount_AlmEventUniqueID );
            return CollectionUtils.Any( bytes, b => b != 0 ) ? StringUtils.ToHex( bytes ) : null;
        }

        #endregion

        #region 测量数据相关的函数

        /// <summary>
        ///     读取TimeWaveData_1D对象
        /// </summary>
        /// <returns>TimeWaveData_1D对象</returns>
        private TimeWaveData_1D ReadTimeWaveData_1D()
        {
            var ret = ReadTrendData<TimeWaveData_1D>();
            ret.ShortWaveData.IntArrayWave = ReadBytesUtils.ReadVarByteArrayWave();
            return ret;
        }

        /// <summary>
        ///     读取TimeWaveData_2D对象
        /// </summary>
        /// <returns>TimeWaveData_1D对象</returns>
        private TimeWaveData_2D ReadTimeWaveData_2D()
        {
            var ret = ReadTrendData<TimeWaveData_2D>();
            ret.ShortWaveData_1D.IntArrayWave = ReadBytesUtils.ReadVarByteArrayWave();
            ret.ShortWaveData_2D.IntArrayWave = ReadBytesUtils.ReadVarByteArrayWave();
            return ret;
        }

        #region 读取TrendData对象

        /// <summary>
        ///     读取TrendData对象
        /// </summary>
        /// <returns>SampleData.TrendData对象</returns>
        private SampleData.TrendData ReadTrendData()
        {
            return ReadTrendData<SampleData.TrendData>();
        }

        /// <summary>
        ///     读取无线传感器趋势数据
        /// </summary>
        /// <returns>SampleData.TrendData对象</returns>
        private SampleData.TrendData ReadWirelessTrendData()
        {
            SampleData.TrendData ret = ReadTrendData();
            ret.WirelessSignalIntensity = ReadBytesUtils.ReadNullableSingle();
            ret.BatteryPercent = ReadBytesUtils.ReadNullableSingle();
            return ret;
        }

        /// <summary>
        ///     读取变量趋势数据Int16
        /// </summary>
        /// <returns>SampleData.TrendData对象</returns>
        private SampleData.TrendData ReadVariantTrendDataInt16()
        {
            var ret = new SampleData.TrendData
                {
                    VariantName = ReadBytesUtils.ReadVarString(),
                    DataUsageID = ReadBytesUtils.ReadInt32(),
                    SampleTime = ReadBytesUtils.ReadDateTime8()
                };

            bool isSigned = ReadBytesUtils.ReadInt32() != 0;
            short value = ReadBytesUtils.ReadInt16();

            ret.MeasurementValue = isSigned ? (float)value : (ushort)value;

            return ret;
        }

        /// <summary>
        ///     读取带特征值的趋势数据，目前无线传感器使用
        /// </summary>
        /// <returns>SampleData.TrendData对象</returns>
        private SampleData.TrendData ReadFeatureValueTrendData()
        {
            SampleData.TrendData ret = ReadTrendData();
            int featureValueCount = ReadBytesUtils.ReadInt32();
            if( 0 < featureValueCount )
            {
                var additionalFeatureId2Values = new Dictionary<int, double>();
                for( int index = 0; index < featureValueCount; index++ )
                {
                    additionalFeatureId2Values[ReadBytesUtils.ReadInt32()] = ReadBytesUtils.ReadSingle();
                }

                ret.AdditionalFeatureID2Values = additionalFeatureId2Values;
            }
            return ret;
        }

        /// <summary>
        ///     读取TrendData对象
        /// </summary>
        private T ReadTrendData<T>() where T : SampleData.TrendData, new()
        {
            var ret = new T();
            ReadTrendData( ret );
            return ret;
        }

        /// <summary>
        ///     读取TrendData对象
        /// </summary>
        private void ReadTrendData( SampleData.TrendData trendData )
        {
            trendData.SyncUniqueID = ReadAlmEventUniqueID();
            ReadValueWrappersContainer( trendData );
            trendData.MeasurementValues = ReadBytesUtils.ReadSingleArray();
            trendData.AxisOffsetValues = ReadBytesUtils.ReadSingleArray();
        }

        #endregion

        #endregion

        #region 固件升级相关函数

        /// <summary>
        ///     写入 文件片段数据
        /// </summary>
        /// <param name="data">FileSegmentData</param>
        private void WriteFileSegmentData( FileSegmentData data )
        {
            var bytesUtils = WriteBytesUtils;

            // 参数，参数之间以半角逗号分隔，只有一个参数时，末尾没有逗号。
            // 目前的参数是：文件名。
            bytesUtils.WriteVarString( data.Parameters, StringEncodingID.ASCII );

            // 文件长度(以字节为单位)
            bytesUtils.WriteInt32( data.FileLength );

            // 片段下标(从0开始)
            bytesUtils.WriteInt32( data.SegmentIndex );

            // 片段偏移(以字节为单位，从0开始)
            bytesUtils.WriteInt32( data.SegmentOffset );

            // 片段长度(以字节为单位)
            // 片段数据
            bytesUtils.WriteByteArray( data.SegmentData );
        }

        #endregion

        #endregion
    }
}