using AnalysisData.Constants;
using AnalysisData.SampleData;
using Moons.Common20;
using OnlineSampleCommon.SendTask;

namespace DataSampler
{
    /// <summary>
    /// 发送任务的类
    /// </summary>
    internal class TasksSender : TaskSender, ITaskSendQueue
    {
        public TasksSender()
        {
            MaxQueueLength = Config.MaxQueueLength_NormalSample;
            Sender = Config.SampleDataSender;
        }

        #region 变量和属性

        #region 筛选发送的数据的属性

        /// <summary>
        /// 是否发送监测数据，默认发送
        /// </summary>
        public bool SendMonitorData { get; set; } = true;

        /// <summary>
        /// 是否发送入库数据，默认发送
        /// </summary>
        public bool SendSaveDbData { get; set; } = true;

        /// <summary>
        /// 是否发送报警数据，默认发送
        /// </summary>
        public bool SendAlmData { get; set; } = true;

        #endregion

        #endregion

        #region 操作函数

        /// <summary>
        /// 任务进队
        /// </summary>
        /// <param name="obj">进队的发送任务对象</param>
        public new void Add( object obj )
        {
            //AddSenderLog("AddData");

            if (obj is TrendData)
            {
                var dataUsageID = (obj as TrendData).DataUsageID;
                if (dataUsageID == DataUsageID.Mornitor && !SendMonitorData) {
                    //AddSenderLog("!SendMonitorData");
                    return;
                }
                if (dataUsageID == DataUsageID.Save2DB && !SendSaveDbData) {
                    //AddSenderLog("!SendSaveDbData");
                    return;
                }
            }
            else if (obj is AlmEventData && !SendAlmData)
            {
                //AddSenderLog("!SendAlmData");
                return;
            }

            base.Add( obj );
            Config.Probe.UpdateTasksSendQueueCount( SendQueueCount );
        }

        private void AddSenderLog( string msg )
        {
        }

        #endregion
    }
}