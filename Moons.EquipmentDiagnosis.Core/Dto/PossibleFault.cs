using System;
using System.Collections.Generic;
using System.Text;

namespace Moons.EquipmentDiagnosis.Core.Dto
{
    /// <summary>
    /// 可能的故障
    /// </summary>
    public class PossibleFault
    {
        /// <summary>
        /// 故障代码，例如：-Generic-Misalign001:轴承座或联轴器不同心
        /// </summary>
        public string FaultCode { get; set; }

        /// <summary>
        /// 测点编号
        /// </summary>
        public object Point_ID { get; set; }

        public override string ToString()
        {
            return $"(PossbileFault:{FaultCode}-{Point_ID})";
        }
    }
}
