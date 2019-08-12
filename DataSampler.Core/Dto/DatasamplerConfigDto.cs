using System;
using System.Collections.Generic;
using System.Text;

namespace DataSampler.Core.Dto
{
    /// <summary>
    /// datasampler 配置数据类
    /// </summary>
    public class DatasamplerConfigDto
    {
        /// <summary>
        /// 是否使用netty
        /// </summary>
        public bool UseNetty { get; set; }

        /// <summary>
        /// 本地侦听的端口，用于接受数据，默认1283
        /// </summary>
        public int ListenPortOfData { get; set; } = 1283;

        /// <summary>
        /// 本地侦听的端口，用于采集工作站TCP控制链接的接入，默认1284
        /// </summary>
        public int ListenPortOfControl { get; set; } = 1284;

        /// <summary>
        /// 发送和接收超时，以毫秒表示，默认为20000（20秒）
        /// </summary>
        public int SendReceiveTimeoutInMs { get; set; } = 20000;

        /// <summary>
        /// 正常采集时，队列中对象数量的最大值
        /// </summary>
        public int MaxQueueLength_NormalSample { get; set; } = 1000;
    }
}
