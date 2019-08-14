using OnlineSampleCommon.SendTask;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineSampleCommon.SendTask
{
    /// <summary>
    /// 空的ITaskSender实现类
    /// </summary>
    public class NullTaskSender : ITaskSender
    {
        public void Send(object data, ref string emsg)
        {
        }

        public void StopSend()
        {
        }
    }
}
