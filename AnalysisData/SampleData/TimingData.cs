using System;
using Moons.Common20;
using Moons.Common20.ValueWrapper;

namespace AnalysisData.SampleData
{
    /// <summary>
    /// 采集器校时数据类
    /// </summary>
    [Serializable]
    public class TimingData : EntityBase, IValueWrappersContainer
    {
        private ValueWrapper<DateTime> _now = new ValueWrapper<DateTime>();

        /// <summary>
        /// 当前时间
        /// </summary>
        public DateTime Now
        {
            get { return _now; }
            set { _now = value; }
        }

        #region IValueWrappersContainer Members

        /// <summary>
        /// 获得IValueWrapper接口的集合
        /// </summary>
        IValueWrapper[] IValueWrappersContainer.ValueWrappers
        {
            get { return new IValueWrapper[] { _now }; }
        }

        #endregion
    }
}