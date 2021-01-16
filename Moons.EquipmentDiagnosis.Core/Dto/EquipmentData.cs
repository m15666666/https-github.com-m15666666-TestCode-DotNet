using System;
using System.Collections.Generic;
using System.Text;
using AnalysisData.FeatureFreq;

namespace Moons.EquipmentDiagnosis.Core.Dto
{
    /// <summary>
    /// 设备数据
    /// </summary>
    public class EquipmentData
    {
        /// <summary>
        /// 设备id
        /// </summary>
        public object EquipmentId { get; set; }

        /// <summary>
        /// 设备类型
        /// </summary>
        public string EquipmentType { get; set; }

        /// <summary>
        /// 测点
        /// </summary>
        public PointDataCollection Points { get; set; } = new PointDataCollection();

        /// <summary>
        /// 部件
        /// </summary>
        public List<EquipmentData> Parts { get; set; } = new List<EquipmentData>();
    }

    /// <summary>
    /// 部件数据
    /// </summary>
    public class PartData
    {
        /// <summary>
        /// 设备id
        /// </summary>
        public object EquipmentId { get; set; }

        /// <summary>
        /// 设备类型
        /// </summary>
        public string EquipmentType { get; set; }

        /// <summary>
        /// 测点
        /// </summary>
        public PointDataCollection Points { get; set; } = new PointDataCollection();

        /// <summary>
        /// 连轴端测点
        /// </summary>
        public PointDataCollection DEPoints { get; set; } = new PointDataCollection();

        /// <summary>
        /// 非连轴端测点
        /// </summary>
        public PointDataCollection NDEPoints { get; set; } = new PointDataCollection();

        /// <summary>
        /// 设备(部件)参数
        /// </summary>
        public PartParameter PartParameter { get; set; }
    }
}
