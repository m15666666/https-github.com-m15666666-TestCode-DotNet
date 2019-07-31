using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using AnalysisData.Constants;
using AnalysisData.ToFromBytes;
using Moons.Common20;
using Moons.Common20.ValueWrapper;

namespace AnalysisData.SampleData
{
    /// <summary>
    /// 测点数据类
    /// </summary>
    [Serializable]
    public class PointData : EntityBase, IValueWrappersContainer
    {
        /// <summary>
        /// 测点维数
        /// </summary>
        private ValueWrapper<int> _dimension = new ValueWrapper<int> { Value = PntDimension.One };

        /// <summary>
        /// 工程单位
        /// </summary>
        private ValueWrapper<string> _engUnit = new ValueWrapper<string>();

        /// <summary>
        /// 测量值类型ID，13：有效值、11：峰值、12：峰峰值、15：平均值、16：波形指标
        /// </summary>
        private ValueWrapper<int> _measValueTypeID = new ValueWrapper<int>();

        /// <summary>
        /// 测点编号
        /// </summary>
        private ValueWrapper<int> _pointID = new ValueWrapper<int>();

        /// <summary>
        /// 测点名
        /// </summary>
        private ValueWrapper<string> _pointName = new ValueWrapper<string>();

        /// <summary>
        /// 测点编号
        /// </summary>
        public int PointID
        {
            get { return _pointID; }
            set { _pointID = value; }
        }

        /// <summary>
        /// 测量值类型ID，13：有效值、11：峰值、12：峰峰值、15：平均值、16：波形指标
        /// </summary>
        public int MeasValueTypeID
        {
            get { return _measValueTypeID; }
            set { _measValueTypeID = value; }
        }

        /// <summary>
        /// 测点维数
        /// </summary>
        public int Dimension
        {
            get { return _dimension; }
            set { _dimension = value; }
        }

        /// <summary>
        /// 测点编号字符串
        /// </summary>
        [XmlIgnore]
        public string PointIDString
        {
            get { return PointID.ToString(); }
        }

        /// <summary>
        /// 测点名
        /// </summary>
        public string PointName
        {
            get { return _pointName; }
            set { _pointName = value; }
        }

        /// <summary>
        /// 工程单位ID
        /// </summary>
        public int EngUnitID { get; set; }

        /// <summary>
        /// 工程单位
        /// </summary>
        public string EngUnit
        {
            get { return _engUnit; }
            set { _engUnit = value; }
        }

        /// <summary>
        /// 采集器的通道CD，一维测点有一个元素，二维测点有两个元素
        /// </summary>
        [XmlElement( ElementName = SampleStationData.XmlTag_ChannelCD )]
        public string[] ChannelCDs { get; set; }

        /// <summary>
        /// 通道1的CD
        /// </summary>
        [XmlIgnore]
        public string ChannelCD_1
        {
            get { return ChannelCDs[0]; }
        }

        /// <summary>
        /// 通道2的CD
        /// </summary>
        [XmlIgnore]
        public string ChannelCD_2
        {
            get { return 1 < ChannelCDs.Length ? ChannelCDs[1] : null; }
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
                _pointName.Size = _engUnit.Size = SampleDataReadWrite.ByteCount_String;
                return new IValueWrapper[] { _pointID, _measValueTypeID, _dimension };
            }
        }

        #endregion

        #region 普通报警门限值

        /// <summary>
        /// 普通报警门限集合
        /// </summary>
        private readonly AlmStand_CommonSettingDataCollection _almStand_CommonSettingDatas =
            new AlmStand_CommonSettingDataCollection();

        /// <summary>
        /// 普通报警门限集合
        /// </summary>
        public AlmStand_CommonSettingDataCollection AlmStand_CommonSettingDatas
        {
            get { return _almStand_CommonSettingDatas; }
        }

        #endregion
    }

    /// <summary>
    /// 测点数据集合
    /// </summary>
    [Serializable]
    public class PointDataCollection : CollectionBase<PointData, PointDataCollection>
    {
        /// <summary>
        /// 根据测点编号获得测点对象
        /// </summary>
        /// <param name="pointID">测点编号</param>
        /// <returns>测点对象</returns>
        public PointData GetByID( int pointID )
        {
            return Find( p => p.PointID == pointID );
        }

        /// <summary>
        /// 根据通道CD获得测点对象
        /// </summary>
        /// <param name="cd">通道CD</param>
        /// <returns>测点对象</returns>
        public PointData GetByChannelCD( string cd )
        {
            return Find( p => Array.Exists( p.ChannelCDs, channelCD => channelCD == cd ) );
        }

        /// <summary>
        /// 获得积分通道的测点对象集合
        /// </summary>
        /// <returns>积分通道的测点对象集合</returns>
        public List<PointData> GetIntChannelPointDatas()
        {
            return FindAll( p => ChannelType.IsChannelCD_Int( p.ChannelCD_1 ) );
        }

        /// <summary>
        /// 根据测点编号获得测点对象，如果不存在则创建
        /// </summary>
        /// <param name="pointID">测点编号</param>
        /// <returns>测点对象</returns>
        public PointData GetOrCreate( int pointID )
        {
            PointData point = GetByID( pointID );
            if( point == null )
            {
                point = new PointData { PointID = pointID };
                Add( point );
            }

            return point;
        }

        /// <summary>
        /// 是否包含指定测点编号的测点对象
        /// </summary>
        /// <param name="pointID">测点编号</param>
        /// <returns>是否包含指定测点编号的测点对象</returns>
        public bool Contain( int pointID )
        {
            return GetByID( pointID ) != null;
        }
    }
}