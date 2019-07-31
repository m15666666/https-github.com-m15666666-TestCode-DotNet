using System;
using System.Xml.Serialization;
using Moons.Common20;
using Moons.Common20.ValueWrapper;

namespace AnalysisData.SampleData
{
    /// <summary>
    /// 采集工作站参数ID
    /// </summary>
    public enum SampleStationParameterID
    {
        /// <summary>
        /// 0x00000001 采集卡号
        /// </summary>
        SampleCard = 1,
        
        /// <summary>
        /// 0x00000002 通道号
        /// </summary>
        StationChannel,
        
        /// <summary>
        /// 0x00000003 信号类型ID
        /// </summary>
        SignalyType,
        
        /// <summary>
        /// 0x00000004 通道类型ID
        /// </summary>
        ChannelType,
        
        /// <summary>
        /// 0x00000005 采样频率值
        /// </summary>
        SampleFreq,
        
        /// <summary>
        /// 0x00000006 倍频系数值
        /// </summary>
        MultiFreq,

        /// <summary>
        /// 0x00000007 采样长度值
        /// </summary>
        DataLength
    }

    /// <summary>
    /// 采集工作站错误信息ID
    /// </summary>
    public enum SampleStationErrorMessageID
    {
        /// <summary>
        /// 0x00000001 无效
        /// </summary>
        Invalid = 1
    }

    /// <summary>
    /// 采集工作站参数错误数据类
    /// </summary>
    [Serializable]
    public class SampleStationParameterErrorData : EntityBase, IValueWrappersContainer
    {
        /// <summary>
        /// 采集工作站错误信息ID
        /// </summary>
        private readonly ValueWrapper<int> _errorMessageID = new ValueWrapper<int>();

        /// <summary>
        /// 采集工作站参数ID
        /// </summary>
        private readonly ValueWrapper<int> _parameterID = new ValueWrapper<int>();

        /// <summary>
        /// 卡号
        /// </summary>
        private ValueWrapper<int> _cardNumber = new ValueWrapper<int>();

        /// <summary>
        /// 通道号
        /// </summary>
        private ValueWrapper<int> _channelNumber = new ValueWrapper<int>();

        /// <summary>
        /// 卡号
        /// </summary>
        public int CardNumber
        {
            get { return _cardNumber; }
            set { _cardNumber = value; }
        }

        /// <summary>
        /// 通道号
        /// </summary>
        public int ChannelNumber
        {
            get { return _channelNumber; }
            set { _channelNumber = value; }
        }

        /// <summary>
        /// 采集工作站参数ID
        /// </summary>
        public SampleStationParameterID ParameterID
        {
            get { return (SampleStationParameterID)_parameterID.Value; }
            set { _parameterID.Value = (int)value; }
        }

        /// <summary>
        /// 采集工作站错误信息ID
        /// </summary>
        public SampleStationErrorMessageID ErrorMessageID
        {
            get { return (SampleStationErrorMessageID)_errorMessageID.Value; }
            set { _errorMessageID.Value = (int)value; }
        }

        #region IValueWrappersContainer Members

        /// <summary>
        /// 获得IValueWrapper接口的集合
        /// </summary>
        [XmlIgnore]
        public IValueWrapper[] ValueWrappers
        {
            get { return new IValueWrapper[] { _cardNumber, _channelNumber, _parameterID, _errorMessageID }; }
        }

        #endregion
    }

    /// <summary>
    /// 采集工作站参数错误数据集合
    /// </summary>
    [Serializable]
    public class SampleStationParameterErrorDataCollection : CollectionBase<SampleStationParameterErrorData, SampleStationParameterErrorDataCollection>
    {
    }
}