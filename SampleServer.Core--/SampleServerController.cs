using Moons.Common20;
using SampleServer.Upload2DB;
using System.Collections.Generic;

namespace SampleServer
{
    /// <summary>
    /// �ɼ��������Ŀ�����
    /// </summary>
    public class SampleServerController : DisposableBase
    {
        #region ����������

        /// <summary>
        /// �����ɼ��Ķ��У����ڽ������ɼ�ʱ���ݵĶ����л�
        /// </summary>
        private readonly NormalSampleQueue _normalSampleQueue = new NormalSampleQueue();

        #endregion

        #region ����SampleServerController����

        /// <summary>
        /// SampleServerController����
        /// </summary>
        private static readonly SampleServerController _instance = new SampleServerController();

        private SampleServerController()
        {
        }

        /// <summary>
        /// SampleServerController����
        /// </summary>
        public static SampleServerController Instance
        {
            get { return _instance; }
        }

        #endregion

        #region ��������

        /// <summary>
        /// ���������ɼ�������
        /// </summary>
        /// <param name="data">�ֽ�������߶���</param>
        public void ReceiveNormalSampleData( object data )
        {
            _normalSampleQueue.AddBytes( data );
        }

        #endregion

        #region Dispose

        protected override void Dispose( bool disposing )
        {
            if( IsDisposed )
            {
                return;
            }

            if( disposing )
            {
                _normalSampleQueue.StopUpload2DB();
            }

            //  һ��Ҫ���û����Dispose����
            base.Dispose( disposing );
        }

        #endregion
    }
}