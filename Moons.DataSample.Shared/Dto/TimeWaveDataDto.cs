using System;
using System.Collections.Generic;
using System.Text;

namespace Moons.DataSample.Shared.Dto
{
    /// <summary>
    ///     波形数据
    /// </summary>
    public class TimeWaveDataDto : TrendDataDto
    {
        /// <summary>
        /// 时间波形
        /// </summary>
        public double[] Wave { get; set; }
    }
}
