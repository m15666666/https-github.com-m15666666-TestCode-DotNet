using System;
using System.Xml.Serialization;
using Moons.Common20;
using Moons.Common20.ValueWrapper;

namespace AnalysisData.SampleData
{
    /// <summary>
    /// 采集工作站的状态数据类
    /// </summary>
    [Serializable]
    public class SampleStationStatusData : EntityBase, IValueWrappersContainer
    {
        /// <summary>
        /// 采集工作站的状态
        /// </summary>
        private readonly ValueWrapper<int> _stationState = new ValueWrapper<int>
                                                               { Value = (int)SampleStationState.Stop };

        /// <summary>
        /// 采集工作站的状态
        /// </summary>
        public SampleStationState StationState
        {
            get { return (SampleStationState)_stationState.Value; }
            set { _stationState.Value = (int)value; }
        }

        #region IValueWrappersContainer Members

        /// <summary>
        /// 获得IValueWrapper接口的集合
        /// </summary>
        [XmlIgnore]
        IValueWrapper[] IValueWrappersContainer.ValueWrappers
        {
            get { return new IValueWrapper[] { _stationState }; }
        }

        #endregion
    }
}