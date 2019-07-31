using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Moons.Common20;
using Moons.Common20.ValueWrapper;

namespace AnalysisData.SampleData
{
    /// <summary>
    ///     趋势数据
    /// </summary>
    [Serializable]
    public class TrendData : SampleDataBase, IValueWrappersContainer
    {
        private ValueWrapper<int> _almLevelID = new ValueWrapper<int>();
        private ValueWrapper<int> _dataLength = new ValueWrapper<int>();
        private ValueWrapper<int> _dataUsageID = new ValueWrapper<int>();

        private ValueWrapper<int> _multiFreq = new ValueWrapper<int>();
        private ValueWrapper<int> _pointID = new ValueWrapper<int>();
        private ValueWrapper<int> _rev = new ValueWrapper<int>();
        private ValueWrapper<float> _sampleFreq = new ValueWrapper<float>();
        private ValueWrapper<DateTime> _sampleTime = new ValueWrapper<DateTime>();

        #region 同步采集的唯一ID

        /// <summary>
        ///     同步采集的唯一ID
        /// </summary>
        public string SyncUniqueID { get; set; }

        /// <summary>
        ///     同步ID
        /// </summary>
        public long SyncID { get; set; }

        #endregion

        /// <summary>
        ///     测点编号
        /// </summary>
        public int PointID
        {
            get { return _pointID; }
            set { _pointID = value; }
        }

        /// <summary>
        ///     数据用途ID，0：监测、1：存储到数据库
        /// </summary>
        public int DataUsageID
        {
            get { return _dataUsageID; }
            set { _dataUsageID = value; }
        }

        /// <summary>
        ///     报警级别ID
        /// </summary>
        public int AlmLevelID
        {
            get { return _almLevelID; }
            set { _almLevelID = value; }
        }

        /// <summary>
        ///     采样时间
        /// </summary>
        public DateTime SampleTime
        {
            get { return _sampleTime; }
            set { _sampleTime = value; }
        }

        /// <summary>
        ///     数据长度
        /// </summary>
        public int DataLength
        {
            get { return _dataLength; }
            set { _dataLength = value; }
        }

        /// <summary>
        ///     分钟转速
        /// </summary>
        public int Rev
        {
            get { return _rev; }
            set { _rev = value; }
        }

        /// <summary>
        ///     采样频率
        /// </summary>
        public float SampleFreq
        {
            get { return _sampleFreq; }
            set { _sampleFreq = value; }
        }

        /// <summary>
        ///     倍频系数
        /// </summary>
        public int MultiFreq
        {
            get { return _multiFreq; }
            set { _multiFreq = value; }
        }

        #region 报警相关

        /// <summary>
        ///     报警事件的唯一ID
        /// </summary>
        public string AlmEventUniqueID { get; set; }

        /// <summary>
        ///     报警ID
        /// </summary>
        public long AlmID { get; set; }

        #endregion

        #region 测量值相关

        /// <summary>
        ///     第一个（默认的）测量值
        /// </summary>
        public float MeasurementValue
        {
            get { return MeasurementValues != null ? MeasurementValues[0] : 0; }
            set
            {
                if( MeasurementValues == null )
                {
                    MeasurementValues = new float[1];
                }

                MeasurementValues[0] = value;
            }
        }

        /// <summary>
        ///     第二个测量值
        /// </summary>
        public float MeasurementValue2
        {
            get { return MeasurementValues[1]; }
            set { MeasurementValues[1] = value; }
        }

        /// <summary>
        ///     测量值数组
        /// </summary>
        public float[] MeasurementValues { get; set; }

        /// <summary>
        ///     字符串类型的测量值，用于字符串类型的OPC变量
        /// </summary>
        public string MeasurementValueString4Opc { get; set; }

        /// <summary>
        ///     true：数据类型是字符串，false：不是
        /// </summary>
        public bool DataTypeIsTextValue
        {
            get { return !string.IsNullOrEmpty( MeasurementValueString4Opc ); }
        }

        #endregion

        #region 轴位移相关

        /// <summary>
        ///     是否包含轴位移数据
        /// </summary>
        public bool HasAxisOffsetValue
        {
            get { return CollectionUtils.IsNotEmptyG( AxisOffsetValues ); }
        }

        /// <summary>
        ///     第一个（默认的）轴位移值
        /// </summary>
        public float AxisOffsetValue
        {
            get { return AxisOffsetValues[0]; }
            set { AxisOffsetValues[0] = value; }
        }

        /// <summary>
        ///     轴位移值的数组，使用电涡流传感器时，显示轴心位置
        /// </summary>
        public float[] AxisOffsetValues { get; set; }

        #endregion

        #region 无线传感器相关

        /// <summary>
        ///     无线通讯信号强度百分比，范围：0 ~ 1。
        /// </summary>
        public float? WirelessSignalIntensity { get; set; }

        /// <summary>
        ///     剩余电池电量百分比，范围：0 ~ 1。
        /// </summary>
        public float? BatteryPercent { get; set; }

        #endregion

        #region 变量相关

        /// <summary>
        ///     变量名，用于变量与测点对应
        /// </summary>
        public string VariantName { get; set; }

        #endregion

        #region 额外传递的特征值

        /// <summary>
        ///     额外传递的特征值ID对特征值的映射
        /// </summary>
        public Dictionary<int, double> AdditionalFeatureID2Values { get; set; }

        #endregion

        #region IValueWrappersContainer Members

        /// <summary>
        ///     获得IValueWrapper接口的集合
        /// </summary>
        [XmlIgnore]
        public IValueWrapper[] ValueWrappers
        {
            get
            {
                return new IValueWrapper[]
                {
                    _pointID, _dataUsageID, _almLevelID, _sampleTime,
                    _dataLength, _rev, _sampleFreq, _multiFreq
                };
            }
        }

        #endregion
    }
}