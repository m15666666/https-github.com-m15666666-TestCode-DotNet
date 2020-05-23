using System;
using System.Collections.Generic;
using System.Text;

namespace Moons.EquipmentDiagnosis.Core.Dto
{
    /// <summary>
    /// 设备诊断输入类
    /// </summary>
    public class EquipmentDiagnosisInputDto
    {
        /// <summary>
        /// 设备id
        /// </summary>
        public object EquipmentId { get; set; }

        /// <summary>
        /// 使用的历史数据起始时间
        /// </summary>
        public DateTime HistoryDataBegin { get; set; }

        /// <summary>
        /// 使用的历史数据截止时间
        /// </summary>
        public DateTime HistoryDataEnd { get; set; }

        public override string ToString()
        {
            return $"{EquipmentId}-{HistoryDataBegin}-{HistoryDataEnd}";
        }
    }
}
