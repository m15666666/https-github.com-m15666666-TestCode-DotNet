using System;
using System.Collections.Generic;
using System.Text;

namespace Moons.EquipmentDiagnosis.Core.Dto
{
    public class TrendDiagnosisOutputDto
    {
        /// <summary>
        /// 报警事件集合
        /// </summary>
        public List<AlarmEvent> AlarmEvents { get; set; } = new List<AlarmEvent>();
    }
}
