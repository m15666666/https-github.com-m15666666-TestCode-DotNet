using System;
using System.Xml.Serialization;
using Moons.Common20;
using Moons.Common20.ValueWrapper;

namespace AnalysisData.SampleData
{
    /// <summary>
    ///     采集额外的测点波形数据的类
    /// </summary>
    [Serializable]
    public class SampleExtraPointData : EntityBase, IValueWrappersContainer
    {
        /// <summary>
        ///     采集次数，默认是1次
        /// </summary>
        private ValueWrapper<int> _count = new ValueWrapper<int>();

        private ValueWrapper<int> _pointID = new ValueWrapper<int>();

        /// <summary>
        ///     测点编号
        /// </summary>
        public int PointID
        {
            get { return _pointID; }
            set { _pointID = value; }
        }

        /// <summary>
        ///     采集次数，默认是1次
        /// </summary>
        public int Count
        {
            get { return _count; }
            set { _count = value; }
        }

        #region IValueWrappersContainer Members

        /// <summary>
        ///     获得IValueWrapper接口的集合
        /// </summary>
        [XmlIgnore]
        public IValueWrapper[] ValueWrappers
        {
            get { return new IValueWrapper[] {_pointID, _count}; }
        }

        #endregion
    }

    /// <summary>
    ///     采集额外的测点波形数据集合
    /// </summary>
    [Serializable]
    public class SampleExtraPointDataCollection : CollectionBase<SampleExtraPointData, SampleExtraPointDataCollection>
    {
    }
}