using System;
using System.Collections.Generic;
using System.Text;

namespace Moons.EquipmentDiagnosis.Core.Dto
{
    /// <summary>
    /// 设备诊断输出类
    /// </summary>
    public class EquipmentDiagnosisOutputDto
    {
        /// <summary>
        /// 设备id
        /// </summary>
        public object EquipmentId { get; set; }
        /// <summary>
        /// 可能的故障
        /// </summary>
        public PossibleFault[] PossibleFaults { get; set; }
    }
}
