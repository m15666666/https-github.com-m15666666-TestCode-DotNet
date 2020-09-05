using System;
using System.Xml.Serialization;
using AnalysisData.ToFromBytes;
using Moons.Common20;
using Moons.Common20.ValueWrapper;

namespace AnalysisData.SampleData
{
    /// <summary>
    /// 通道数据类
    /// </summary>
    [Serializable]
    public class ChannelData : EntityBase, IValueWrappersContainer
    {
        #region IValueWrappersContainer Members

        /// <summary>
        /// 获得IValueWrapper接口的集合
        /// </summary>
        [XmlIgnore]
        IValueWrapper[] IValueWrappersContainer.ValueWrappers
        {
            get
            {
                _channelCd.Size =
                    _revChannelCd.Size =
                    _keyPhaserRevChannelCd.Size =
                    _switchChannelCd.Size =
                    SampleDataReadWrite.ByteCount_ChannelCD;

                return new IValueWrapper[]
                           {
                               _channelIdentifier, _sampleOrder, _reservedBytes,
                               _channelCd, _revChannelCd, _keyPhaserRevChannelCd, _switchChannelCd,
                               _switchTriggerStatus, _switchTriggerMethod,
                               _channelTypeID, _channelNumber, _signalTypeID,
                               _sampleFreq, _multiFreq, _dataLength, _averageCount,
                               _revLowThreshold, _revHighThreshold, _referenceRev, _revTypeID,
                               _revRatio, _scaleFactor, _voltageOffset
                           };
            }
        }

        #endregion

        #region 连续报警次数（超过这个次数才报警，次数为0则不报警）

        /// <summary>
        /// 连续报警次数
        /// </summary>

        /// <summary>
        /// 连续报警次数
        /// </summary>
        public AlmCountDataCollection AlmCountDatas { get; } = new AlmCountDataCollection();

        #endregion

        public static ChannelData ParseXml( string xml )
        {
            return XmlUtils.XmlDeserializeFromXml<ChannelData>( xml );
        }

        #region 变量和属性

        /// <summary>
        /// 平均次数
        /// </summary>
        private ValueWrapper<int> _averageCount = new ValueWrapper<int>();

        /// <summary>
        /// 通道CD
        /// </summary>
        private ValueWrapper<string> _channelCd = new ValueWrapper<string>();

        /// <summary>
        /// 通道号
        /// </summary>
        private ValueWrapper<int> _channelNumber = new ValueWrapper<int>();

        /// <summary>
        /// 通道类型ID
        /// </summary>
        private ValueWrapper<int> _channelTypeID = new ValueWrapper<int>();

        /// <summary>
        /// 数据长度
        /// </summary>
        private ValueWrapper<int> _dataLength = new ValueWrapper<int>();

        /// <summary>
        /// 转速通道CD，为空字符串表示未引用，参考键相通道。
        /// </summary>
        private ValueWrapper<string> _keyPhaserRevChannelCd = new ValueWrapper<string>();

        /// <summary>
        /// 倍频系数
        /// </summary>
        private ValueWrapper<int> _multiFreq = new ValueWrapper<int>();

        /// <summary>
        /// 参考工作转速
        /// </summary>
        private ValueWrapper<int> _referenceRev = new ValueWrapper<int>();

        /// <summary>
        /// 转速通道CD，为空字符串表示未引用，参考转速通道。
        /// </summary>
        private ValueWrapper<string> _revChannelCd = new ValueWrapper<string>();

        /// <summary>
        /// 转速上限，高于上限则转速为0，单位是rpm
        /// </summary>
        private ValueWrapper<int> _revHighThreshold = new ValueWrapper<int>();

        /// <summary>
        /// 转速下限，低于下限则转速为0，单位是rpm
        /// </summary>
        private ValueWrapper<int> _revLowThreshold = new ValueWrapper<int>();

        /// <summary>
        /// 转速比例因子（只用于转速通道），默认为1
        /// </summary>
        private ValueWrapper<float> _revRatio = new ValueWrapper<float>();

        /// <summary>
        /// 转速类型（只用于转速通道）：1：单脉冲，2： 多脉冲 
        /// </summary>
        private ValueWrapper<int> _revTypeID = new ValueWrapper<int>();

        /// <summary>
        /// 采样频率
        /// </summary>
        private ValueWrapper<float> _sampleFreq = new ValueWrapper<float>();

        /// <summary>
        /// 工程单位与电压单位的比例因子，电压乘上因子变成工程单位
        /// </summary>
        private ValueWrapper<float> _scaleFactor = new ValueWrapper<float>();

        /// <summary>
        /// 信号类型ID（测量参量）
        /// </summary>
        private ValueWrapper<int> _signalTypeID = new ValueWrapper<int>();

        /// <summary>
        /// 参考开关量通道，为空字符串表示未引用。
        /// </summary>
        private ValueWrapper<string> _switchChannelCd = new ValueWrapper<string>();

        /// <summary>
        /// 开关量触发方式，1：高电平触发，0：低电平触发。
        /// </summary>
        private ValueWrapper<int> _switchTriggerMethod = new ValueWrapper<int>();

        /// <summary>
        /// 开关量触发状态
        /// </summary>
        private ValueWrapper<bool> _switchTriggerStatus = new ValueWrapper<bool>();

        /// <summary>
        /// 电压偏移
        /// </summary>
        private ValueWrapper<float> _voltageOffset = new ValueWrapper<float>();

        #region 通道标识符

        /// <summary>
        /// 通道标识符长度
        /// </summary>
        private const int ChannelIdentifierLength = 6;

        /// <summary>
        /// 通道标识符，用于唯一标识一个通道，在无线传感器项目中引入。长度为6个字节
        /// </summary>
        private ValueWrapper<byte[]> _channelIdentifier = new byte[ChannelIdentifierLength];

        /// <summary>
        /// 以字符串表示的通道标识符
        /// </summary>
        public string ChannelIdentifier { get; set; }

        /// <summary>
        /// 设置以整形表示的通道标识符
        /// </summary>
        public int ChannelIdentifierOfInt
        {
            set
            {
                ChannelIdentifier = value.ToString();
                var text = value.ToString("X").PadLeft(12, '0');
                byte[] channelIdentifier = StringUtils.HexString2Bytes(text);
                //if (channelIdentifier.Length != ChannelIdentifierLength)
                //{
                //    throw new ArgumentOutOfRangeException(string.Format("Invalid ChannelIdentifier({0}) length", value));
                //}
                ChannelIdentifierOfBytes = channelIdentifier;
            }
        }

        /// <summary>
        /// 以字节表示的通道标识符
        /// </summary>
        public byte[] ChannelIdentifierOfBytes
        {
            get { return _channelIdentifier; }
            set { _channelIdentifier = value; }
        }

        /// <summary>
        /// 无线传感器多种信号类型的采集次序
        /// </summary>
        public byte[] SampleOrder
        {
            set
            {
                if( value.Length != _sampleOrder.Value.Length )
                {
                    throw new ArgumentOutOfRangeException( string.Format( "Invalid SampleOrder({0}) length",
                        StringUtils.ToHex( value ) ) );
                }

                _sampleOrder = value;
            }
        }

        /// <summary>
        /// 无线传感器多种信号类型的采集次序，目前包括：速度、加速度、位移、冲击四种类型。在无线传感器项目中引入。
        /// 长度为3个字节，表示24位（24个采集周期），每位1表示采集，0表示不采集，先发送的字节（位）先采集。
        /// </summary>
        private ValueWrapper<byte[]> _sampleOrder= new byte[3];

        /// <summary>
        /// 保留字节。长度为3个字节
        /// </summary>
        private readonly ValueWrapper<byte[]> _reservedBytes = new byte[3];

        #endregion

        /// <summary>
        /// 通道CD
        /// </summary>
        public string ChannelCD
        {
            get { return _channelCd; }
            set { _channelCd = value; }
        }

        /// <summary>
        /// 转速通道CD，为空字符串表示未引用，参考转速通道。
        /// </summary>
        public string RevChannelCD
        {
            get { return _revChannelCd; }
            set { _revChannelCd = value; }
        }

        /// <summary>
        /// 转速通道CD，为空字符串表示未引用，参考键相通道。
        /// </summary>
        public string KeyPhaserRevChannelCD
        {
            get { return _keyPhaserRevChannelCd; }
            set { _keyPhaserRevChannelCd = value; }
        }

        /// <summary>
        /// 参考开关量通道，为空字符串表示未引用。
        /// </summary>
        public string SwitchChannelCD
        {
            get { return _switchChannelCd; }
            set { _switchChannelCd = value; }
        }

        /// <summary>
        /// 开关量触发状态
        /// </summary>
        public bool SwitchTriggerStatus
        {
            get { return _switchTriggerStatus; }
            set { _switchTriggerStatus = value; }
        }

        /// <summary>
        /// 开关量触发方式，1：高电平触发，0：低电平触发。
        /// </summary>
        public int SwitchTriggerMethod
        {
            get { return _switchTriggerMethod; }
            set { _switchTriggerMethod = value; }
        }

        /// <summary>
        /// 通道类型ID，1：动态通道，2：静态通道，3：转速通道，5：开关量通道
        /// </summary>
        public int ChannelTypeID
        {
            get { return _channelTypeID; }
            set { _channelTypeID = value; }
        }

        /// <summary>
        /// 通道号，例如：1，2，3，4，5，6，7，...，32
        /// </summary>
        public int ChannelNumber
        {
            get { return _channelNumber; }
            set { _channelNumber = value; }
        }

        /// <summary>
        /// 信号类型ID（测量参量），102：加速度、101：速度、119：冲击、103：位移
        /// </summary>
        public int SignalTypeID
        {
            get { return _signalTypeID; }
            set { _signalTypeID = value; }
        }

        /// <summary>
        /// 采样频率
        /// </summary>
        public float SampleFreq
        {
            get { return _sampleFreq; }
            set { _sampleFreq = value; }
        }

        /// <summary>
        /// 倍频系数
        /// </summary>
        public int MultiFreq
        {
            get { return _multiFreq; }
            set { _multiFreq = value; }
        }

        /// <summary>
        /// 数据长度
        /// </summary>
        public int DataLength
        {
            get { return _dataLength; }
            set { _dataLength = value; }
        }

        /// <summary>
        /// 平均次数
        /// </summary>
        public int AverageCount
        {
            get { return _averageCount; }
            set { _averageCount = value; }
        }

        /// <summary>
        /// 转速下限，低于下限则转速为0，单位是rpm
        /// </summary>
        public int RevLowThreshold
        {
            get { return _revLowThreshold; }
            set { _revLowThreshold = value; }
        }

        /// <summary>
        /// 转速上限，高于上限则转速为0，单位是rpm
        /// </summary>
        public int RevHighThreshold
        {
            get { return _revHighThreshold; }
            set { _revHighThreshold = value; }
        }

        /// <summary>
        /// 参考工作转速
        /// </summary>
        public int ReferenceRev
        {
            get { return _referenceRev; }
            set { _referenceRev = value; }
        }

        /// <summary>
        /// 转速类型（只用于转速通道）：1：单脉冲，2： 多脉冲 
        /// </summary>
        public int RevTypeID
        {
            get { return _revTypeID; }
            set { _revTypeID = value; }
        }

        /// <summary>
        /// 转速比例因子（只用于转速通道），默认为1
        /// </summary>
        public float RevRatio
        {
            get { return _revRatio; }
            set { _revRatio = value; }
        }

        /// <summary>
        /// 工程单位与电压单位的比例因子，电压乘上因子变成工程单位
        /// </summary>
        public float ScaleFactor
        {
            get { return _scaleFactor; }
            set { _scaleFactor = value; }
        }

        /// <summary>
        /// 电压偏移
        /// </summary>
        public float VoltageOffset
        {
            get { return _voltageOffset; }
            set { _voltageOffset = value; }
        }

        /// <summary>
        ///     工程单位与电压单位的比例因子的工程单位ID
        /// </summary>
        public int ScaleFactorEngUnitID { get; set; }

        #region 电涡流传感器通道参数

        #region 中心点电压(电涡流传感器通道参数)

        /// <summary>
        ///     中心点电压(电涡流传感器通道参数)
        /// </summary>
        public float CenterPositionVoltage { get; set; }

        /// <summary>
        ///     中心点电压(电涡流传感器通道参数)的工程单位ID
        /// </summary>
        public int CenterPositionVoltageEngUnitID { get; set; }

        #endregion

        #region 中心点位置(电涡流传感器通道参数)

        /// <summary>
        ///     中心点位置(电涡流传感器通道参数)
        /// </summary>
        public float CenterPosition { get; set; }

        /// <summary>
        ///     中心点位置(电涡流传感器通道参数)的工程单位ID
        /// </summary>
        public int CenterPositionEngUnitID { get; set; }

        #endregion

        #region 线性范围(电涡流传感器通道参数)

        /// <summary>
        ///     线性范围最小值(电涡流传感器通道参数)
        /// </summary>
        public float LinearRangeMin { get; set; }

        /// <summary>
        ///     线性范围最大值(电涡流传感器通道参数)
        /// </summary>
        public float LinearRangeMax { get; set; }

        /// <summary>
        ///     线性范围(电涡流传感器通道参数)的工程单位ID
        /// </summary>
        public int LinearRangeEngUnitID { get; set; }

        #endregion

        #endregion


        /// <summary>
        ///     传感器轴ID，0:不使用，1:主轴，2:副轴1,3:副轴2，用于无线三轴传感器
        /// </summary>
        public int SensorAxisID { get; set; }

        /// <summary>
        /// 高通滤波截止频率，目前用于二代在线速度通道，可以设置为2HZ,5HZ,10HZ
        /// </summary>
        public float HighPassFreq { get; set; }

        /// <summary>
        /// 冲击测量参数: 带宽：3: 1k~2k, 5: 2K~4K, 11: 高通5Khz, 15: 高通12Khz
        /// </summary>
        public int ShockBandPassID { get; set; }

        /// <summary>
        /// 等角度采样: 跟踪转速范围: 3: 5HZ~100HZ 5: 跟踪转速:50HZ~1000HZ
        /// </summary>
        public int MultiFreqRevRangeID { get; set; }

        #endregion
    }

    /// <summary>
    /// 通道数据集合
    /// </summary>
    [Serializable]
    public class ChannelDataCollection : CollectionBase<ChannelData, ChannelDataCollection>
    {
        public ChannelData GetByChannelCD( string channelCD )
        {
            return Find( channelData => channelData.ChannelCD == channelCD );
        }
    }
}