using System;
using System.Text;

namespace DataSampler.Helper
{
    /// <summary>
    /// 采集工作站的探针数据类
    /// </summary>
    public class DataSamplerProbeData
    {
        #region 变量和属性

        /// <summary>
        /// 程序启动时间
        /// </summary>
        public DateTime ProgramStartTime { get; set; }

        /// <summary>
        /// 开始采样时间
        /// </summary>
        public DateTime StartSampleTime { get; private set; }

        /// <summary>
        /// 停止采样时间
        /// </summary>
        public DateTime StopSampleTime { get; private set; }

        /// <summary>
        /// 任务发送队列中元素数
        /// </summary>
        public int TasksSendQueueCount { get; private set; }

        /// <summary>
        /// 最后一次更新任务发送队列的时间
        /// </summary>
        public DateTime TasksSendQueueUpdateTime { get; private set; }

        #region 正常启动采集的次数，用于程序首次运行首次启动采集失败情况下，再次尝试启动采集。

        /// <summary>
        /// 正常启动采集的次数，用于程序首次运行首次启动采集失败情况下，再次尝试启动采集。
        /// </summary>
        public int StartSampleSuccessCount { get; set; }

        /// <summary>
        /// 下次尝试重启的时间
        /// </summary>
        public DateTime NextTryStartSampleTime { get; set; }

        #endregion

        #endregion

        #region ctor

        #endregion

        /// <summary>
        /// 设置开始采样时间
        /// </summary>
        public void SetStartSampleTime()
        {
            StartSampleTime = DateTime.Now;
        }

        /// <summary>
        /// 设置停止采样时间
        /// </summary>
        public void SetStopSampleTime()
        {
            StopSampleTime = DateTime.Now;
        }

        /// <summary>
        /// 更新任务发送队列中元素数
        /// </summary>
        /// <param name="count">任务发送队列中元素数</param>
        public void UpdateTasksSendQueueCount( int count )
        {
            TasksSendQueueCount = count;

            TasksSendQueueUpdateTime = DateTime.Now;
        }

        public override string ToString()
        {
            var buffer = new StringBuilder();

            buffer.AppendFormat( "ProgramStartTime:{0}", ProgramStartTime );
            buffer.AppendLine();

            buffer.AppendFormat( "StartSampleTime:{0}", StartSampleTime );
            buffer.AppendLine();

            buffer.AppendFormat( "StopSampleTime:{0}", StopSampleTime );
            buffer.AppendLine();

            buffer.AppendFormat( "TasksSendQueueCount:{0}({1})", TasksSendQueueCount, TasksSendQueueUpdateTime );
            buffer.AppendLine();

            return buffer.ToString();
        }
    }
}