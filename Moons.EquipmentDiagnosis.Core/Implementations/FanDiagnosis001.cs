using Moons.EquipmentDiagnosis.Core.Dto;

namespace Moons.EquipmentDiagnosis.Core.Implementations
{
    /// <summary>
    /// 风机、泵诊断模型 抽象类
    /// </summary>
    public abstract class FanAndPumpBase : PartDiagnosisBase001
    {
        public override void DoDiagnosis()
        {
            Points.ClearNDaysCache();

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

                if (PartParameter.IsStiffBase) CalcMISAGN1(almPoint);
                else CalcMISAGN2(almPoint);

                CalcImbalance(almPoint);
            }

            Points.ClearNDaysCache();
        }

        public abstract bool CalcImbalance(PointData point);
    }

    /// <summary>
    /// 风机诊断模型
    /// </summary>
    public class FanDiagnosis001 : FanAndPumpBase
    {
        public override bool CalcImbalance(PointData point)
        {
            var parameter = PartParameter;
            var isTwoEnd = parameter.IsTwoEnd;
            if (parameter.IsStiffBase) // 刚性基础
            {
                if (isTwoEnd) return CalcUNBL1(point);
                else return CalcUNBL2(point);
            }
            else // 弹性基础
            {
                if (isTwoEnd) return CalcUNBL3(point);
                else return CalcUNBL4(point);
            }
        }
    }
    /// <summary>
    /// 泵诊断模型
    /// </summary>
    public class PumpDiagnosis001 : FanAndPumpBase
    {
        public override bool CalcImbalance(PointData point)
        {
            var parameter = PartParameter;
            var isTwoEnd = parameter.IsTwoEnd;
            if (parameter.IsStiffBase) // 刚性基础
            {
                if (isTwoEnd) return CalcUNBL5(point);
                else return CalcUNBL6(point);
            }
            else // 弹性基础
            {
                if (isTwoEnd) return CalcUNBL7(point);
                else return CalcUNBL8(point);
            }
        }
    }
}