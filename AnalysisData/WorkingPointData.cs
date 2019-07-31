using System;

namespace AnalysisData
{
    /// <summary>
    ///     工况测点数据
    /// </summary>
    [Serializable]
    public class WorkingPointData
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
        public int PointEngUnit_ID { get; set; }

        /// <summary>
        ///     工程单位名称
        /// </summary>
        public string EngUnitName_TX { get; set; }
    }
}