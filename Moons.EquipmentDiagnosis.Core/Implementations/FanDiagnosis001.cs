using System;
using System.Collections.Generic;
using System.Text;

namespace Moons.EquipmentDiagnosis.Core.Implementations
{
    /// <summary>
    /// 风机、泵诊断模型
    /// </summary>
    public class FanDiagnosis001 : PartDiagnosisBase001
    {
        public override void DoDiagnosis()
        {
            CalcLoose();
            CalcRub();
        }
    }
}
