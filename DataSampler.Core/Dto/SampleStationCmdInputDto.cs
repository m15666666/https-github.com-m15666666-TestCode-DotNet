using AnalysisData.SampleData;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataSampler.Core.Dto
{
    /// <summary>
    /// 给SampleStation发命令使用的输入dto对象
    /// </summary>
    public class SampleStationCmdInputDto
    {
        /// <summary>
        /// 采集工作站信息
        /// </summary>
        public SampleStationData SampleStationData { get; set; }
    }

    /// <summary>
    /// 给SampleStation发命令使用的输入dto对象
    /// </summary>
    public class SampleStationCmdInputDto<T>
    {
        /// <summary>
        /// 采集工作站信息
        /// </summary>
        public SampleStationData SampleStationData { get; set; }

        /// <summary>
        /// 额外的参数
        /// </summary>
        public T Data { get; set; }
    }
}
