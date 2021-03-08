using AnalysisAlgorithm.FFT;
using Moons.EquipmentDiagnosis.Core.Abstractions;
using Moons.EquipmentDiagnosis.Core.Dto;
using System.Collections.Generic;
using System.Linq;

namespace Moons.EquipmentDiagnosis.Core.Implementations
{
    /// <summary>
    /// 设备诊断实现002
    /// </summary>
    public class EquipmentDiagnosis002 : IEquipmentDiagnosis
    {
        public EquipmentDiagnosisOutputDto DoDiagnosis(EquipmentDiagnosisInputDto equipmentDiagnosisInput)
        {
            return new ED002().DoDiagnosis(equipmentDiagnosisInput);
        }

        public class ED002 : EDBase002
        {
            public EquipmentDiagnosisOutputDto DoDiagnosis(EquipmentDiagnosisInputDto equipmentDiagnosisInput)
            {
                Init(equipmentDiagnosisInput);
                Diagnosis();
                return GetEquipmentDiagnosisOutputDto();
            }

            private void Diagnosis()
            {
                var equipType = _equipmentData.EquipmentType;
                var partParameter = _equipmentData.PartParameters.FirstOrDefault();
                if (partParameter == null) return;


                PartData motor = null;
                PartData pump = null;
                PartData fan = null;
                if (equipType == Core.EquipmentType.Pump.Generic001)
                {
                    partParameter.Point_I = _pointDatas.FirstOrDefault(p => p.SigType_ID == AnalysisData.Constants.SignalTypeID.Current);
                    partParameter.Point_QI = _pointDatas.FirstOrDefault(p => p.PositionNo == PositionNumber.No_5 && p.SigType_ID == AnalysisData.Constants.SignalTypeID.LiquidFlow);
                    partParameter.Point_QO = _pointDatas.FirstOrDefault(p => p.PositionNo == PositionNumber.No_6 && p.SigType_ID == AnalysisData.Constants.SignalTypeID.LiquidFlow);

                    HashSet<int> motorPositions = new HashSet<int> { PositionNumber.No_1, PositionNumber.No_2 };
                    HashSet<int> pumpPositions = new HashSet<int> { PositionNumber.No_3, PositionNumber.No_4 };

                    motor = new PartData { PartParameter = partParameter, };
                    motor.Points.AddRange(_pointDatas.Where(p => motorPositions.Contains(p.PositionNo)));
                    GroupPoints(motor, PositionNumber.No_1, PositionNumber.No_2);

                    pump = new PartData { PartParameter = partParameter, };
                    pump.Points.AddRange(_pointDatas.Where(p => pumpPositions.Contains(p.PositionNo)));
                    GroupPoints(pump, PositionNumber.No_4, PositionNumber.No_3);
                }
                if (equipType == Core.EquipmentType.Fan.Generic001)
                {
                    partParameter.Point_I = _pointDatas.FirstOrDefault(p => p.SigType_ID == AnalysisData.Constants.SignalTypeID.Current);
                    partParameter.Point_QI = _pointDatas.FirstOrDefault(p => p.PositionNo == PositionNumber.No_5 && p.SigType_ID == AnalysisData.Constants.SignalTypeID.LiquidFlow);
                    partParameter.Point_QO = _pointDatas.FirstOrDefault(p => p.PositionNo == PositionNumber.No_6 && p.SigType_ID == AnalysisData.Constants.SignalTypeID.LiquidFlow);

                    HashSet<int> motorPositions = new HashSet<int> { PositionNumber.No_1, PositionNumber.No_2 };
                    HashSet<int> fanPositions = new HashSet<int> { PositionNumber.No_3, PositionNumber.No_4 };

                    motor = new PartData { PartParameter = partParameter, };
                    motor.Points.AddRange(_pointDatas.Where(p => motorPositions.Contains(p.PositionNo)));
                    GroupPoints(motor, PositionNumber.No_1, PositionNumber.No_2);

                    fan = new PartData { PartParameter = partParameter, };
                    fan.Points.AddRange(_pointDatas.Where(p => fanPositions.Contains(p.PositionNo)));
                    GroupPoints(fan, PositionNumber.No_4, PositionNumber.No_3);
                }

                DoDiagnosis<MotorDiagnosis001>(motor);
                DoDiagnosis<PumpDiagnosis001>(pump);
                DoDiagnosis<FanDiagnosis001>(fan);

                // 诊断
                void DoDiagnosis<T>(PartData partData) where T : PartDiagnosisBase001, new()
                {
                    if (partData == null) return;
                    var diagnosis = new T { Part = partData };
                    diagnosis.DoDiagnosis();
                    _possibleFaults.AddRange(diagnosis.PossibleFaults);
                }

                // 测点分组
                void GroupPoints(PartData partData, int ndeNo, int deNo)
                {
                    var ndes = partData.NDEPoints;
                    var des = partData.DEPoints;
                    foreach (var p in partData.Points)
                        if (p.PositionNo == ndeNo) ndes.Add(p);
                        else if (p.PositionNo == deNo) des.Add(p);
                }
            }
        }
    }

    /// <summary>
    /// 诊断基类
    /// </summary>
    public class EDBase002
    {
        protected EquipmentDiagnosisInputDto _equipmentDiagnosisInput;
        protected EquipmentData _equipmentData;
        protected readonly PointDataCollection _pointDatas = new PointDataCollection();

        /// <summary>
        /// 初始化数据
        /// </summary>
        /// <param name="equipmentDiagnosisInput"></param>
        protected void Init(EquipmentDiagnosisInputDto equipmentDiagnosisInput)
        {
            object equipmentId = equipmentDiagnosisInput.EquipmentId;
            _equipmentDiagnosisInput = equipmentDiagnosisInput;
            _equipmentData = Context.GetEquipmentData(equipmentId);
            _pointDatas.AddRange(Context.GetPointDatas(equipmentDiagnosisInput));
            foreach (var p in _pointDatas)
                InitPointData(p);
        }

        /// <summary>
        /// 初始化测点的测量数据
        /// </summary>
        /// <param name="p"></param>
        private void InitPointData(PointData p)
        {
            // 加速度和速度进一步初始化波形
            bool needWave = p.IsAcc || p.IsVel;

            var summary = p.HistorySummaryData;
            if (summary == null)
            {
                var histories = Context.GetHistorySummaryData(p, new HistoryQueryConditionData
                {
                    HistoryDataBegin = _equipmentDiagnosisInput.HistoryDataBegin,
                    HistoryDataEnd = _equipmentDiagnosisInput.HistoryDataEnd,
                    //MeasurementValueLowLimit = p.GetMeasurementValueLowLimit(),
                    //Count = 1,
                    //MustHasTimewave = true,
                    //MaxFirst = true,
                });
                if (0 == histories.Count) return;
                summary = needWave ? histories.FirstOrDefault(h => 0 < h.DatLen_NR) : histories[0];
                if (summary == null) return;

                p.HistorySummaryData = summary;
            }

            var timewave = p.TimewaveData;
            if (timewave == null) timewave = p.TimewaveData = Context.GetTimewaveData(summary);

            if (timewave == null) return;
            var rev = summary.RotSpeed_NR;
            if (rev <= 0) return;

            SpectrumUtils spectrumUtils = new SpectrumUtils();
            spectrumUtils.InitByTimewave(summary.SampleFreq_NR, summary.F0, timewave.Timewave, true);

            timewave.Init(spectrumUtils);
        }

        protected IEquipmentDiagnosisContext Context => Config.Context;

        protected string EquipmentType => _equipmentData.EquipmentType;

        protected List<PossibleFault> _possibleFaults = new List<PossibleFault>();
        protected bool HasPossibleFaults => 0 < _possibleFaults.Count;

        protected void AddPossibleFault(PointData p, string code)
        {
            PossibleFault possibleFault = new PossibleFault { FaultCode = code, Point_ID = p.Point_ID };
            _possibleFaults.Add(possibleFault);
            Config.Logger.Info($"Add possible fault: {p.Point_ID},{p.PointPath},{code}");
        }

        protected EquipmentDiagnosisOutputDto GetEquipmentDiagnosisOutputDto()
        {
            return new EquipmentDiagnosisOutputDto
            {
                EquipmentId = _equipmentDiagnosisInput.EquipmentId,
                PossibleFaults = _possibleFaults.ToArray(),
            };
        }
    }
}