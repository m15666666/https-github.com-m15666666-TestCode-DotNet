using System;

namespace AnalysisData
{
    /// <summary>
    ///     工况数据
    /// </summary>
    [Serializable]
    public class WorkingData
    {
        /// <summary>
        ///     查询用的设备编号
        /// </summary>
        public int QueryMobject_ID { get; set; }

        /// <summary>
        ///     查询用的设备路径
        /// </summary>
        public string QueryParentList_TX { get; set; }

        /// <summary>
        ///     查询用的设备名称路径
        /// </summary>
        public string QueryMobjectPath_TX { get; set; }

        /// <summary>
        ///     分区编号
        /// </summary>
        public long Partition_ID { get; set; }

        /// <summary>
        ///     历史数据编号
        /// </summary>
        public long History_ID { get; set; }

        /// <summary>
        ///     设备编号
        /// </summary>
        public int Mobject_ID { get; set; }

        /// <summary>
        ///     设备路径
        /// </summary>
        public string ParentList_TX { get; set; }

        /// <summary>
        ///     设备名称路径
        /// </summary>
        public string MobjectPath_TX { get; set; }

        /// <summary>
        ///     测点编号
        /// </summary>
        public int Point_ID { get; set; }

        /// <summary>
        ///     测点名称
        /// </summary>
        public string PointName_TX { get; set; }

        /// <summary>
        ///     变量编号
        /// </summary>
        public int Var_ID { get; set; }

        /// <summary>
        ///     变量名
        /// </summary>
        public string VarName_TX { get; set; }

        /// <summary>
        ///     采样时间
        /// </summary>
        public DateTime SampTime_DT { get; set; }

        /// <summary>
        ///     特征值
        /// </summary>
        public double FeatureValue_NR { get; set; }

        /// <summary>
        ///     结果值（格式化之后的FeatureValue_NR）
        /// </summary>
        public string Result_TX { get; set; }

        /// <summary>
        ///     数据类型
        /// </summary>
        public int DatType_NR { get; set; }

        /// <summary>
        ///     信号类型
        /// </summary>
        public int SigType_NR { get; set; }

        /// <summary>
        ///     工程单位
        /// </summary>
        public int EngUnit_ID { get; set; }

        /// <summary>
        ///     工程单位
        /// </summary>
        public int PointEngUnit_ID { get; set; }

        /// <summary>
        ///     工程单位名称
        /// </summary>
        public string EngUnitName_TX { get; set; }

        /// <summary>
        ///     压缩ID
        /// </summary>
        public int Compress_ID { get; set; }

        /// <summary>
        ///     报警编号
        /// </summary>
        public long Alm_ID { get; set; }

        /// <summary>
        ///     报警等级颜色
        /// </summary>
        public int AlmLevel_ID { get; set; }

        /// <summary>
        ///     数据长度
        /// </summary>
        public int DatLen_NR { get; set; }

        /// <summary>
        ///     转速
        /// </summary>
        public int RotSpeed_NR { get; set; }

        /// <summary>
        ///     基准历史数据的采样时间
        /// </summary>
        public DateTime HistoryBasedSampTime_DT { get; set; }

        /// <summary>
        ///     基准历史数据的采样时间差值绝对值
        /// </summary>
        public double SampTimeIntervalAbs_NR
        {
            get { return Math.Abs( ( SampTime_DT - HistoryBasedSampTime_DT ).TotalMilliseconds ); }
        }

        /// <summary>
        ///     最终显示的测量值（可以是数值加单位，也可以是状态描述）
        /// </summary>
        public string MeasureValue_TX { get; set; }
    }
}