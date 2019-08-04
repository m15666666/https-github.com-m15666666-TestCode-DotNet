using System;
using System.Text;

namespace DataSampler.Helper
{
    /// <summary>
    /// �ɼ�����վ��̽��������
    /// </summary>
    public class DataSamplerProbeData
    {
        #region ����������

        /// <summary>
        /// ��������ʱ��
        /// </summary>
        public DateTime ProgramStartTime { get; set; }

        /// <summary>
        /// ��ʼ����ʱ��
        /// </summary>
        public DateTime StartSampleTime { get; private set; }

        /// <summary>
        /// ֹͣ����ʱ��
        /// </summary>
        public DateTime StopSampleTime { get; private set; }

        /// <summary>
        /// �����Ͷ�����Ԫ����
        /// </summary>
        public int TasksSendQueueCount { get; private set; }

        /// <summary>
        /// ���һ�θ��������Ͷ��е�ʱ��
        /// </summary>
        public DateTime TasksSendQueueUpdateTime { get; private set; }

        #region ���������ɼ��Ĵ��������ڳ����״������״������ɼ�ʧ������£��ٴγ��������ɼ���

        /// <summary>
        /// ���������ɼ��Ĵ��������ڳ����״������״������ɼ�ʧ������£��ٴγ��������ɼ���
        /// </summary>
        public int StartSampleSuccessCount { get; set; }

        /// <summary>
        /// �´γ���������ʱ��
        /// </summary>
        public DateTime NextTryStartSampleTime { get; set; }

        #endregion

        #endregion

        #region ctor

        #endregion

        /// <summary>
        /// ���ÿ�ʼ����ʱ��
        /// </summary>
        public void SetStartSampleTime()
        {
            StartSampleTime = DateTime.Now;
        }

        /// <summary>
        /// ����ֹͣ����ʱ��
        /// </summary>
        public void SetStopSampleTime()
        {
            StopSampleTime = DateTime.Now;
        }

        /// <summary>
        /// ���������Ͷ�����Ԫ����
        /// </summary>
        /// <param name="count">�����Ͷ�����Ԫ����</param>
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