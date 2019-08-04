using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataSampler
{
    /// <summary>
    /// 组合模式的datasampler发送队列
    /// </summary>
    public class CompositeSendQueue : ITaskSendQueue
    {
        /// <summary>
        /// 包含的队列
        /// </summary>
        public List<ITaskSendQueue> TaskSendQueues { get; } = new List<ITaskSendQueue>();

        void ITaskSendQueue.Add(object obj)
        {
            TaskSendQueues.ForEach(queue => queue.Add(obj));
        }

        void ITaskSendQueue.StartSend()
        {
            TaskSendQueues.ForEach(queue => queue.StartSend());
        }

        void ITaskSendQueue.StopSend()
        {
            TaskSendQueues.ForEach(queue => queue.StopSend());
        }
    }
}
