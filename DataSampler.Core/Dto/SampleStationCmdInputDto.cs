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
        public SampleStationData SampleStationData { get; set; }
    }
}
