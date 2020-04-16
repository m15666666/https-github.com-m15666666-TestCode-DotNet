using Moons.EquipmentDiagnosis.Core.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moons.EquipmentDiagnosis.Core.Abstractions
{
    /// <summary>
    /// 设备诊断接口
    /// </summary>
    public interface IEquipmentDiagnosis
    {
        /// <summary>
        /// 执行诊断
        /// </summary>
        /// <param name="equipmentDiagnosisInput"></param>
        /// <returns></returns>
        EquipmentDiagnosisOutputDto DoDiagnosis(EquipmentDiagnosisInputDto equipmentDiagnosisInput);
    }
}
