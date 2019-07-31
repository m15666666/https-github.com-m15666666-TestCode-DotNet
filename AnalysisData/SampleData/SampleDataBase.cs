using System;
using Moons.Common20;

namespace AnalysisData.SampleData
{
    /// <summary>
    /// 采样数据的类型ID
    /// </summary>
    public enum SampleDataClassID
    {
        /// <summary>
        /// 单点数据
        /// </summary>
        TrendData = 1001,

        /// <summary>
        /// 一维时间波形数据
        /// </summary>
        TimeWaveData_1D,

        /// <summary>
        /// 二维时间波形数据
        /// </summary>
        TimeWaveData_2D,
    }

    /// <summary>
    /// 采样数据的基类
    /// </summary>
    [Serializable]
    public abstract class SampleDataBase : EntityBase
    {
    }
}