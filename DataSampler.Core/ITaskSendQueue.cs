using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataSampler
{
    /// <summary>
    /// datasampler发送队列接口
    /// </summary>
    public interface ITaskSendQueue
    {
        /// <summary>
        /// 任务进队
        /// </summary>
        /// <param name="obj">进队的发送任务对象</param>
        void Add(object obj);

        /// <summary>
        /// 开始发送
        /// </summary>
        void StartSend();

        /// <summary>
        /// 停止发送
        /// </summary>
        void StopSend();
    }
}
