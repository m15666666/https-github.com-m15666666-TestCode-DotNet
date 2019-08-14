using Moons.Common20;
using SampleServer.Upload2DB;
using System.Collections.Generic;

namespace SampleServer
{
    /// <summary>
    /// 采集服务器的控制器
    /// </summary>
    public class SampleServerController : DisposableBase
    {
        #region 变量和属性

        /// <summary>
        /// 正常采集的队列，用于将正常采集时传递的对象串行化
        /// </summary>
        private readonly NormalSampleQueue _normalSampleQueue = new NormalSampleQueue();

        #endregion

        #region 创建SampleServerController对象

        /// <summary>
        /// SampleServerController单例
        /// </summary>
        private static readonly SampleServerController _instance = new SampleServerController();

        private SampleServerController()
        {
        }

        /// <summary>
        /// SampleServerController单例
        /// </summary>
        public static SampleServerController Instance
        {
            get { return _instance; }
        }

        #endregion

        #region 接收数据

        /// <summary>
        /// 接收正常采集的数据
        /// </summary>
        /// <param name="data">字节数组或者对象</param>
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

            //  一定要调用基类的Dispose函数
            base.Dispose( disposing );
        }

        #endregion
    }
}