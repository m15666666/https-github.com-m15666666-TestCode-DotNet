using System;
using System.Collections.Generic;
using System.Linq;
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


        public override string ToString()
        {
            if (PossibleFaults == null) return EquipmentId?.ToString();

            StringBuilder builder = new StringBuilder(EquipmentId?.ToString());
            Array.ForEach(PossibleFaults, f => builder.Append(",").Append(f));
            return builder.ToString();
        }
    }
}
