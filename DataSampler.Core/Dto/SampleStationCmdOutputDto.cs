using System;
using System.Collections.Generic;
using System.Text;

namespace DataSampler.Core.Dto
{
    /// <summary>
    /// 给SampleStation发命令返回的dto对象
    /// </summary>
    public class SampleStationCmdOutputDto<T>
    {
        /// <summary>
        /// true: 成功
        /// </summary>
        public bool Succeed { get; set; } = true;

        /// <summary>
        /// 内部异常
        /// </summary>
        public Exception InnerException { get; set; }

        /// <summary>
        /// 输出数据
        /// </summary>
        public T Data { get; set; }
    }
}
