using Moons.EquipmentDiagnosis.Core.Abstractions;
using Moons.EquipmentDiagnosis.Core.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moons.EquipmentDiagnosis.Core.Implementations
{
    /// <summary>
    /// 设备诊断实现001
    /// </summary>
    public class EquipmentDiagnosis001 : IEquipmentDiagnosis
    {
        public EquipmentDiagnosisOutputDto DoDiagnosis(EquipmentDiagnosisInputDto equipmentDiagnosisInput)
        {
        }

        public class ED001 : EDBase
        {
            public EquipmentDiagnosisOutputDto DoDiagnosis(EquipmentDiagnosisInputDto equipmentDiagnosisInput)
            {
                Init(equipmentDiagnosisInput);
                return MotorDiagnosis(equipmentDiagnosisInput);
            }


            private EquipmentDiagnosisOutputDto MotorDiagnosis(EquipmentDiagnosisInputDto equipmentDiagnosisInput)
            {

            }
        }
    }

    public class EDBase
    {
        protected EquipmentDiagnosisInputDto _equipmentDiagnosisInput;
        protected EquipmentData _equipmentData;
        protected readonly PointDataCollection _pointDatas = new PointDataCollection();
        protected void Init(EquipmentDiagnosisInputDto equipmentDiagnosisInput)
        {
            object equipmentId = equipmentDiagnosisInput.EquipmentId;
            _equipmentDiagnosisInput = equipmentDiagnosisInput;
            _equipmentData = Context.GetEquipmentData(equipmentId);
            _pointDatas.AddRange( Context.GetPointDatas(equipmentDiagnosisInput) );

        }
        protected IEquipmentDiagnosisContext Context => Config.Context;
    }
}
