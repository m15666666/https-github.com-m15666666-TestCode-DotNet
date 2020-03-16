using System;
using System.Collections.Generic;
using System.Text;

namespace AnalysisData.Dto
{
    /// <summary>
    ///     趋势数据
    /// </summary>
    [Serializable]
    public class TrendDataDto
    {
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
            get;
            set;
        }

        /// <summary>
        ///     数据用途ID，0：监测、1：存储到数据库
        /// </summary>
        public int DataUsageID
        {
            get;
            set;
        }

        /// <summary>
        ///     报警级别ID
        /// </summary>
        public int AlmLevelID
        {
            get;
            set;
        }

        /// <summary>
        ///     采样时间
        /// </summary>
        public DateTime SampleTime
        {
            get;
            set;
        }

        /// <summary>
        ///     数据长度
        /// </summary>
        public int DataLength
        {
            get;
            set;
        }

        /// <summary>
        ///     分钟转速
        /// </summary>
        public int Rev
        {
            get;
            set;
        }

        /// <summary>
        ///     采样频率
        /// </summary>
        public float SampleFreq
        {
            get;
            set;
        }

        /// <summary>
        ///     倍频系数
        /// </summary>
        public int MultiFreq
        {
            get;
            set;
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
            get;
            set;
        }

        /// <summary>
        ///     第二个测量值
        /// </summary>
        public float MeasurementValue2
        {
            get;
            set;
        }

        /// <summary>
        ///     字符串类型的测量值，用于字符串类型的OPC变量
        /// </summary>
        public string MeasurementValueString4Opc { get; set; }

        /// <summary>
        ///     true：数据类型是字符串，false：不是
        /// </summary>
        public bool DataTypeIsTextValue
        {
            get { return !string.IsNullOrEmpty(MeasurementValueString4Opc); }
        }

        #endregion

        #region 轴位移相关


        /// <summary>
        ///     第一个（默认的）轴位移值
        /// </summary>
        public float AxisOffsetValue
        {
            get;
            set;
        }

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
    }
}
