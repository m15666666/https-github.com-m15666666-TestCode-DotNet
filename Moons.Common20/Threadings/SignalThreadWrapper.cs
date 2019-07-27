using System;
using System.Threading;

namespace Moons.Common20.Threadings
{
    /// <summary>
    /// 带信号(事件)通知的线程包装器类
    /// </summary>
    public class SignalThreadWrapper
    {
        #region ctor

        public SignalThreadWrapper()
        {
            ThreadEndSignal = new ManualResetEvent( false );
        }

        #endregion

        #region 变量和属性

        /// <summary>
        /// 内部异常
        /// </summary>
        public Exception InnerException { get; private set; }

        /// <summary>
        /// 线程结束信号
        /// </summary>
        public EventWaitHandle ThreadEndSignal { get; private set; }

        #endregion

        #region 启动线程

        /// <summary>
        /// 启动线程
        /// </summary>
        /// <param name="threadAction">线程函数</param>
        public void StartThread( Action20 threadAction )
        {
            InnerException = null;
            EventWaitHandle signal = ThreadEndSignal;
            signal.Reset();

            WaitCallback waitCallback = state =>
                                            {
                                                try
                                                {
                                                    EventUtils.FireEvent( threadAction );
                                                }
                                                catch( Exception ex )
                                                {
                                                    InnerException = ex;
                                                }
                                                finally
                                                {
                                                    signal.Set();
                                                }
                                            };
            ThreadPool.QueueUserWorkItem( waitCallback );
        }

        /// <summary>
        /// 启动线程
        /// </summary>
        /// <typeparam name="T">参数类型</typeparam>
        /// <param name="threadAction">线程函数</param>
        /// <param name="arg">参数</param>
        public void StartThread<T>( Action<T> threadAction, T arg )
        {
            StartThread( () => EventUtils.FireEvent( threadAction, arg ) );
        }

        #endregion
    }
}