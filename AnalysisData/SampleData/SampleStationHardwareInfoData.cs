using System;
using System.Xml.Serialization;
using Moons.Common20;
using Moons.Common20.ValueWrapper;

namespace AnalysisData.SampleData
{
    /// <summary>
    /// 采集工作站硬件信息类
    /// </summary>
    [Serializable]
    public class SampleStationHardwareInfoData : EntityBase, IValueWrappersContainer
    {
        public SampleStationHardwareInfoData()
        {
            ChannelHardwareInfos = new SampleStationChannelHardwareInfoDataCollection();
        }

        #region 变量和属性

        /// <summary>
        /// 采集站出厂编号 表示此采集站出厂时的唯一编号
        /// </summary>
        private ValueWrapper<int> _samplerSnNumber = new ValueWrapper<int>();

        /// <summary>
        /// 采集站版本
        /// </summary>
        private ValueWrapper<int> _samplerVersion = new ValueWrapper<int>();


        /// <summary>
        /// 采集站版本
        /// </summary>
        public int SamplerVersion
        {
            get { return _samplerVersion; }
            set { _samplerVersion = value; }
        }

        /// <summary>
        /// 采集站出厂编号 表示此采集站出厂时的唯一编号
        /// </summary>
        public int SamplerSNNumber
        {
            get { return _samplerSnNumber; }
            set { _samplerSnNumber = value; }
        }

        /// <summary>
        /// 通道数
        /// </summary>
        public int ChannelCount
        {
            get { return ChannelHardwareInfos.Count; }
        }

        /// <summary>
        /// 采集工作站通道硬件信息数据集合
        /// </summary>
        public SampleStationChannelHardwareInfoDataCollection ChannelHardwareInfos { get; private set; }

        #endregion

        #region IValueWrappersContainer Members

        /// <summary>
        /// 获得IValueWrapper接口的集合
        /// </summary>
        [XmlIgnore]
        public IValueWrapper[] ValueWrappers
        {
            get { return new IValueWrapper[] { _samplerVersion, _samplerSnNumber }; }
        }

        #endregion
    }

    /// <summary>
    /// 采集工作站通道硬件信息类
    /// </summary>
    [Serializable]
    public class SampleStationChannelHardwareInfoData : EntityBase, IValueWrappersContainer
    {
        /// <summary>
        /// 连续报警次数
        /// </summary>
        private ValueWrapper<int> _almCount = new ValueWrapper<int>();

        /// <summary>
        /// 平均次数
        /// </summary>
        private ValueWrapper<int> _averageCount = new ValueWrapper<int>();

        /// <summary>
        /// 采集卡号，表示哪个采集卡（哪个LPC1765）
        /// </summary>
        private ValueWrapper<int> _cardNumber = new ValueWrapper<int>();

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

        private ValueWrapper<bool> _hasKeyPhaserChannel = new ValueWrapper<bool>();
        private ValueWrapper<bool> _hasRevChannel = new ValueWrapper<bool>();
        private ValueWrapper<bool> _hasSwitchChannel = new ValueWrapper<bool>();

        /// <summary>
        /// 倍频系数
        /// </summary>
        private ValueWrapper<int> _multiFreq = new ValueWrapper<int>();

        /// <summary>
        /// 参考工作转速
        /// </summary>
        private ValueWrapper<int> _referenceRev = new ValueWrapper<int>();

        /// <summary>
        /// 转速上限，高于上限则转速为0，单位是rpm
        /// </summary>
        private ValueWrapper<int> _revHighThreshold = new ValueWrapper<int>();

        /// <summary>
        /// 转速下限，低于下限则转速为0，单位是rpm
        /// </summary>
        private ValueWrapper<int> _revLowThreshold = new ValueWrapper<int>();

        /// <summary>
        /// 采样频率
        /// </summary>
        private ValueWrapper<float> _sampleFreq = new ValueWrapper<float>();

        /// <summary>
        /// 传感器是否失效
        /// </summary>
        private ValueWrapper<bool> _sensorEnable = new ValueWrapper<bool>();

        /// <summary>
        /// 信号类型ID（测量参量）
        /// </summary>
        private ValueWrapper<int> _signalTypeID = new ValueWrapper<int>();

        private ValueWrapper<bool> _switchTriggerStatus = new ValueWrapper<bool>();

        /// <summary>
        /// 表结构版本
        /// </summary>
        private ValueWrapper<int> _tableVersion = new ValueWrapper<int>();

        /// <summary>
        /// 电压偏移
        /// </summary>
        private ValueWrapper<float> _voltageOffset = new ValueWrapper<float>();

        /// <summary>
        /// 表结构版本，表示表结构版本(现在为版本1,0x00000001)
        /// </summary>
        public int TableVersion
        {
            get { return _tableVersion; }
            set { _tableVersion = value; }
        }

        /// <summary>
        /// 采集卡号，表示哪个采集卡（哪个LPC1765）
        /// </summary>
        public int CardNumber
        {
            get { return _cardNumber; }
            set { _cardNumber = value; }
        }

        /// <summary>
        /// 参考转速，1：有转速，0：无转速
        /// </summary>
        public bool HasRevChannel
        {
            get { return _hasRevChannel; }
            set { _hasRevChannel = value; }
        }

        /// <summary>
        /// 参考键相，1：有，0：没有
        /// </summary>
        public bool HasKeyPhaserChannel
        {
            get { return _hasKeyPhaserChannel; }
            set { _hasKeyPhaserChannel = value; }
        }

        /// <summary>
        /// 参考开关量，1：有，0：没有
        /// </summary>
        public bool HasSwitchChannel
        {
            get { return _hasSwitchChannel; }
            set { _hasSwitchChannel = value; }
        }

        /// <summary>
        /// 开关量触发状态，1：开，0：关
        /// </summary>
        public bool SwitchTriggerStatus
        {
            get { return _switchTriggerStatus; }
            set { _switchTriggerStatus = value; }
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
        /// 电压偏移
        /// </summary>
        public float VoltageOffset
        {
            get { return _voltageOffset; }
            set { _voltageOffset = value; }
        }

        /// <summary>
        /// 连续报警次数
        /// </summary>
        public int AlmCount
        {
            get { return _almCount; }
            set { _almCount = value; }
        }

        /// <summary>
        /// 传感器是否失效  1：有效 0：无效
        /// </summary>
        public bool SensorEnable
        {
            get { return _sensorEnable; }
            set { _sensorEnable = value; }
        }

        #region IValueWrappersContainer Members

        /// <summary>
        /// 获得IValueWrapper接口的集合
        /// </summary>
        [XmlIgnore]
        public IValueWrapper[] ValueWrappers
        {
            get
            {
                return new IValueWrapper[]
                           {
                               _tableVersion, _cardNumber, _hasRevChannel, _hasKeyPhaserChannel,
                               _hasSwitchChannel, _switchTriggerStatus, _channelTypeID, _channelNumber,
                               _signalTypeID, _sampleFreq, _multiFreq, _dataLength,
                               _averageCount, _revLowThreshold, _revHighThreshold, _referenceRev,
                               _voltageOffset, _almCount, _sensorEnable
                           };
            }
        }

        #endregion
    }

    /// <summary>
    /// 采集工作站通道硬件信息数据集合
    /// </summary>
    [Serializable]
    public class SampleStationChannelHardwareInfoDataCollection : CollectionBase<SampleStationChannelHardwareInfoData, SampleStationChannelHardwareInfoDataCollection>
    {
    }
}