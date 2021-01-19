using Moons.EquipmentDiagnosis.Core.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moons.EquipmentDiagnosis.Core.Abstractions
{
    /// <summary>
    /// 设备诊断上下文接口
    /// </summary>
    public interface IEquipmentDiagnosisContext
    {
        /// <summary>
        /// 获得设备数据
        /// </summary>
        /// <param name="id">设备id</param>
        /// <returns></returns>
        EquipmentData GetEquipmentData(object id);
        
        /// <summary>
        /// 获得测点数据集合
        /// </summary>
        /// <param name="equipmentDiagnosisInput">EquipmentDiagnosisInputDto</param>
        /// <returns></returns>
        IEnumerable<PointData> GetPointDatas(EquipmentDiagnosisInputDto equipmentDiagnosisInput);

        /// <summary>
        /// 获得summary数据
        /// </summary>
        /// <param name="point">PointData</param>
        /// <param name="condition">查询条件</param>
        /// <returns></returns>
        HistorySummaryDataCollection GetHistorySummaryData(PointData point, HistoryQueryConditionData condition);

        /// <summary>
        /// 获得summary数据
        /// </summary>
        /// <param name="point">PointData</param>
        /// <param name="condition">查询条件</param>
        /// <returns></returns>
        Int32MinuteSummary[] GetInt32MinuteSummary(PointData point, HistoryQueryConditionData condition);

        /// <summary>
        /// 获得测点数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TimewaveData GetTimewaveData(HistorySummaryData id);
    }
}
