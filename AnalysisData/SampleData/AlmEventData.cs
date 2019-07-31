using System;
using System.Xml.Serialization;
using AnalysisData.Constants;
using AnalysisData.Helper;
using Moons.Common20;
using Moons.Common20.ValueWrapper;

namespace AnalysisData.SampleData
{
    /// <summary>
    /// 报警事件数据类
    /// </summary>
    [Serializable]
    public class AlmEventData : EntityBase, IValueWrappersContainer
    {
        /// <summary>
        /// 连续报警次数
        /// </summary>
        private ValueWrapper<int> _almCount = new ValueWrapper<int>();

        private ValueWrapper<int> _almLevel = new ValueWrapper<int>();
        private ValueWrapper<int> _almSourceID = new ValueWrapper<int>();
        private ValueWrapper<DateTime> _almTime = new ValueWrapper<DateTime>();

        /// <summary>
        /// 报警值
        /// </summary>
        private ValueWrapper<float> _almValue = new ValueWrapper<float>();

        /// <summary>
        /// 描述
        /// </summary>
        private string _description;

        private ValueWrapper<int> _pointID = new ValueWrapper<int>();

        /// <summary>
        /// 测点编号
        /// </summary>
        public int PointID
        {
            get { return _pointID; }
            set { _pointID = value; }
        }

        /// <summary>
        /// 报警时间
        /// </summary>
        public DateTime AlmTime
        {
            get { return _almTime; }
            set { _almTime = value; }
        }

        /// <summary>
        /// 报警等级
        /// </summary>
        public int AlmLevel
        {
            get { return _almLevel; }
            set { _almLevel = value; }
        }

        /// <summary>
        /// 报警来源ID
        /// </summary>
        public int AlmSourceID
        {
            get { return _almSourceID; }
            set { _almSourceID = value; }
        }

        /// <summary>
        /// 报警事件的唯一ID
        /// </summary>
        public string AlmEventUniqueID { get; set; }

        /// <summary>
        /// 报警ID
        /// </summary>
        public long AlmID { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description
        {
            get
            {
                // 如果未设置描述，则每次都构造描述，不给_description赋值
                if( _description == null )
                {
                    switch( AlmSourceID )
                    {
                        case FeatureValueID.Error_Transducer:
                        case FeatureValueID.Error_TransducerBatteryLow:
                        case FeatureValueID.Error_SamplerUnknown:
                        case FeatureValueID.Error_SampleBoardConfig:
                        case FeatureValueID.Error_MeasurementValueOutOfRange:
                            return FeatureValueID.GetName( AlmSourceID );

                        case FeatureValueID.Error_SampleBoard485Commumication:
                            return string.Format( "{0}({1}号板卡)", FeatureValueID.GetName( AlmSourceID ), AlmCount );
                    }

                    return string.Format(
                        "{0}报警，连续报警{1}次！报警值：{2}。",
                        FeatureValueID.GetName( AlmSourceID ), AlmCount,
                        AlmUtils.GetThresholdValueString( AlmValue ) );
                }

                return _description;
            }
            set { _description = value; }
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
        /// 报警值
        /// </summary>
        public float AlmValue
        {
            get { return _almValue; }
            set { _almValue = value; }
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
                           { _pointID, _almTime, _almLevel, _almSourceID, _almCount, _almValue };
            }
        }

        #endregion
    }

    /// <summary>
    /// 报警事件数据集合
    /// </summary>
    public class AlmEventDataCollection : CollectionBase<AlmEventData, AlmEventDataCollection>
    {
        /// <summary>
        /// 获得每个测点下报警等级最高的报警事件
        /// </summary>
        /// <returns>报警事件集合</returns>
        public AlmEventDataCollection GetTopAlmEventDataPerPoint()
        {
            SortByAlmLevelDesc();

            var ret = new AlmEventDataCollection();
            foreach( AlmEventData almEventData in this )
            {
                int pointID = almEventData.PointID;
                if( ret.Exists( almEvent => almEvent.PointID == pointID ) )
                {
                    continue;
                }

                ret.Add( almEventData );
            }

            return ret;
        }

        /// <summary>
        /// 按报警等级升序排列
        /// </summary>
        public void SortByAlmLevel()
        {
            Sort( CompareByAlmLevel );
        }

        /// <summary>
        /// 按报警等级降序排列
        /// </summary>
        public void SortByAlmLevelDesc()
        {
            SortByAlmLevel();
            Reverse();
        }

        /// <summary>
        /// 按照报警等级比较
        /// </summary>
        /// <param name="x">AlmEventData</param>
        /// <param name="y">AlmEventData</param>
        /// <returns>x.AlmLevel.CompareTo( y.AlmLevel )</returns>
        private static int CompareByAlmLevel( AlmEventData x, AlmEventData y )
        {
            return x.AlmLevel.CompareTo( y.AlmLevel );
        }
    }
}