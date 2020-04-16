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
    }
}
