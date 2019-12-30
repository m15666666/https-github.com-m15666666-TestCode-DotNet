using System;
using System.Collections.Generic;
using System.Text;

namespace Moons.EquipmentDiagnosis.Core.Dto
{
    /// <summary>
    /// 报警事件
    /// </summary>
    public class AlarmEvent
    {
        public AlarmTypeIdEnum AlarmTypeId { get; set; }

        public string Description { get; set; }
    }
}
