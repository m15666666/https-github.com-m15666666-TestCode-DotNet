using AnalysisData.Constants;
using AnalysisData.SampleData;
using Moons.Common20;
using OnlineSampleCommon.SendTask;

namespace DataSampler
{
    /// <summary>
    /// �����������
    /// </summary>
    internal class TasksSender : TaskSender, ITaskSendQueue
    {
        public TasksSender()
        {
            MaxQueueLength = Config.MaxQueueLength_NormalSample;
            Sender = Config.SampleDataSender;
        }

        #region ����������

        #region ɸѡ���͵����ݵ�����

        /// <summary>
        /// �Ƿ��ͼ�����ݣ�Ĭ�Ϸ���
        /// </summary>
        public bool SendMonitorData { get; set; } = true;

        /// <summary>
        /// �Ƿ���������ݣ�Ĭ�Ϸ���
        /// </summary>
        public bool SendSaveDbData { get; set; } = true;

        /// <summary>
        /// �Ƿ��ͱ������ݣ�Ĭ�Ϸ���
        /// </summary>
        public bool SendAlmData { get; set; } = true;

        #endregion

        #endregion

        #region ��������

        /// <summary>
        /// �������
        /// </summary>
        /// <param name="obj">���ӵķ����������</param>
        public new void Add( object obj )
        {
            //AddSenderLog("AddData");

            if (obj is TrendData)
            {
                var dataUsageID = (obj as TrendData).DataUsageID;
                if (dataUsageID == DataUsageID.Monitor && !SendMonitorData) {
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