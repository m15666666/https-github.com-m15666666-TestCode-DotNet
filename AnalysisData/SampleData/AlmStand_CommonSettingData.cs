using System;
using System.Xml.Serialization;
using Moons.Common20;
using Moons.Common20.ValueWrapper;

namespace AnalysisData.SampleData
{
    /// <summary>
    /// 普通报警设置数据类
    /// </summary>
    [Serializable]
    public class AlmStand_CommonSettingData : EntityBase, IValueWrappersContainer
    {
        private ValueWrapper<int> _almSourceID = new ValueWrapper<int>();
        private ValueWrapper<int> _almTypeID = new ValueWrapper<int>();
        private ValueWrapper<float?> _highLimit1NR = new ValueWrapper<float?>();
        private ValueWrapper<float?> _highLimit2NR = new ValueWrapper<float?>();
        private ValueWrapper<float?> _lowLimit1NR = new ValueWrapper<float?>();
        private ValueWrapper<float?> _lowLimit2NR = new ValueWrapper<float?>();

        /// <summary>
        /// 报警来源ID
        /// </summary>
        public int AlmSource_ID
        {
            get { return _almSourceID; }
            set { _almSourceID = value; }
        }

        /// <summary>
        /// 报警类型编号
        /// </summary>
        public int AlmType_ID
        {
            get { return _almTypeID; }
            set { _almTypeID = value; }
        }

        /// <summary>
        /// 报警级别1的下限值
        /// </summary>
        public double? LowLimit1_NR
        {
            get { return _lowLimit1NR.Value; }
            set { _lowLimit1NR = (float?)value; }
        }

        /// <summary>
        /// 报警级别2的下限值
        /// </summary>
        public double? LowLimit2_NR
        {
            get { return _lowLimit2NR.Value; }
            set { _lowLimit2NR = (float?)value; }
        }

        /// <summary>
        /// 报警级别1的上限值
        /// </summary>
        public double? HighLimit1_NR
        {
            get { return _highLimit1NR.Value; }
            set { _highLimit1NR = (float?)value; }
        }

        /// <summary>
        /// 报警级别2的上限值
        /// </summary>
        public double? HighLimit2_NR
        {
            get { return _highLimit2NR.Value; }
            set { _highLimit2NR = (float?)value; }
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
                           {_almSourceID, _almTypeID, _lowLimit1NR, _lowLimit2NR, _highLimit1NR, _highLimit2NR};
            }
        }

        #endregion
    }

    /// <summary>
    /// AlmStand_CommonSettingData集合
    /// </summary>
    [Serializable]
    public class AlmStand_CommonSettingDataCollection : CollectionBase<AlmStand_CommonSettingData, AlmStand_CommonSettingDataCollection>
    {
    }

    /// <summary>
    /// 连续报警次数设置数据
    /// </summary>
    [Serializable]
    public class AlmCountData : EntityBase, IValueWrappersContainer
    {
        /// <summary>
        /// 连续报警次数，为0表示不计算报警
        /// </summary>
        private ValueWrapper<int> _almCount = new ValueWrapper<int>();

        /// <summary>
        /// 报警来源ID
        /// </summary>
        private ValueWrapper<int> _almSourceID = new ValueWrapper<int>();

        /// <summary>
        /// 报警来源ID
        /// </summary>
        public int AlmSource_ID
        {
            get { return _almSourceID; }
            set { _almSourceID = value; }
        }

        /// <summary>
        /// 连续报警次数，为0表示不计算报警
        /// </summary>
        public int AlmCount
        {
            get { return _almCount; }
            set { _almCount = value; }
        }

        #region IValueWrappersContainer Members

        /// <summary>
        /// 获得IValueWrapper接口的集合
        /// </summary>
        [XmlIgnore]
        public IValueWrapper[] ValueWrappers
        {
            get { return new IValueWrapper[] {_almSourceID, _almCount}; }
        }

        #endregion
    }

    /// <summary>
    /// AlmCountData集合
    /// </summary>
    [Serializable]
    public class AlmCountDataCollection : CollectionBase<AlmCountData, AlmCountDataCollection>
    {
    }
}