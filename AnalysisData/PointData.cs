using System;

namespace AnalysisData
{
    /// <summary>
    /// 测点数据
    /// </summary>
    [Serializable]
    public class PointData : NodeData
    {
        /// <summary>
        /// 测点编号
        /// </summary>
        public int Point_ID { get; set; }

        /// <summary>
        /// 设备编号
        /// </summary>
        public int MObject_ID { get; set; }

        /// <summary>
        /// 测点名称
        /// </summary>
        public string PointName_TX { get; set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        public int DatType_ID { get; set; }

        /// <summary>
        /// 信号类型
        /// </summary>
        public int SigType_ID { get; set; }

        /// <summary>
        /// 信号类型名称
        /// </summary>
        public string SigTypeName { get; set; }

        /// <summary>
        /// 工程单位编号
        /// </summary>
        public int EngUnit_ID { get; set; }

        /// <summary>
        /// 工程单位名称
        /// </summary>
        public string EngUnitName { get; set; }

        /// <summary>
        /// 测点维数(说明见Notes)
        /// </summary>
        public int PntDim_NR { get; set; }

        /// <summary>
        /// 测点关联的通道标识符
        /// </summary>
        public string ChannelIdentifier_TX { get; set; }

        /// <summary>
        /// 测点关联的变量ID
        /// </summary>
        public int Var_ID { get; set; }

        /// <summary>
        /// 旋转方向ID
        /// </summary>
        public int Rotation_ID { get; set; }

        public int? Rotation_ID_Db
        {
            set
            {
                Rotation_ID = (value == null) ? 0 : (int)value;                
            }
        }

        public int? PntDim_NR_Db
        {
            set
            {
                 PntDim_NR = (value == null) ? 0 : (int)value;               
            }
        }

        /// <summary>
        /// 特征指标
        /// </summary>
        public int FeatureValue_ID { get; set; }
    }

    /// <summary>
    /// 测点数据
    /// </summary>
    [Serializable]
    public class HistorySummaryData
    {
        /// <summary>
        ///     采样频率
        /// </summary>
        public double SampleFreq { get; set; }

        /// <summary>
        ///     倍频系数
        /// </summary>
        public int MultiFreq { get; set; }

        /// <summary>
        ///     信号类型
        /// </summary>
        public string SigType_TX { get; set; }

        /// <summary>
        ///     数据类型编号
        /// </summary>
        public byte? DatType_NR { get; set; }

        /// <summary>
        ///     信号类型编号
        /// </summary>
        public int? SigType_NR { get; set; }

        /// <summary>
        ///     数据长度
        /// </summary>
        public int? DatLen_NR { get; set; }

        public long Partition_ID { get; set; }

        public long History_ID { get; set; }

        public int DJResult_ID { get; set; }

        public int Point_ID { get; set; }

        public byte Compress_ID { get; set; }

        public int CompressType_ID { get; set; }

        public byte PntDim_NR { get; set; }

        public DateTime SampTime_DT { get; set; }

        public DateTime SampTimeGMT_DT { get; set; }

        public int RotSpeed_NR { get; set; }

        public double MinFreq_NR { get; set; }

        public string Result_TX { get; set; }

        public int AlmLevel_ID { get; set; }

        public int DataGroup_NR { get; set; }

        public int AvgNum_NR { get; set; }

        public long Alm_ID { get; set; }

        public int EngUnit_ID { get; set; }

        public long Synch_NR { get; set; }

        public int OrbitAxis_ID { get; set; }

        public int MasterSlave_ID { get; set; }
    }

    [Serializable]
    public class HistoryFeatureData
    {
        public long Partition_ID { get; set; }

        public long FeatureValuePK_ID { get; set; }

        public long History_ID { get; set; }

        public int ChnNo_NR { get; set; }

        public int FeatureValueType_ID { get; set; }

        public int FeatureValue_ID { get; set; }

        public double FeatureValue_NR { get; set; }

        public int SigType_NR { get; set; }

        public int EngUnit_ID { get; set; }
    }
}
