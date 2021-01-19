using Moons.EquipmentDiagnosis.Core.Dto;
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
            CalcBearingFTF();
            PointData almPoint = CalcIsAccAlarm();
            if (almPoint != null)
            {
                CalcBearingBSF();
                CalcBearingBPFO();
                CalcBearingBPFI();
                CalcFL_EXCIT1();
                CalcFL_EXCIT2();
            }
            almPoint = CalcIsVelAlarm();
            if (almPoint != null)
            {
                CalcCLEARANCE();
                CalcFDLOOSE();
                CalcSTRESS();
                CalcRotor_ecc1(almPoint);
            }
        }
    }
}
