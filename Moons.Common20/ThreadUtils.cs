using System;
using System.Threading;

namespace Moons.Common20
{
    /// <summary>
    /// 线程的实用工具类
    /// </summary>
    public static class ThreadUtils
    {
        #region 创建线程

        /// <summary>
        /// 创建后台线程
        /// </summary>
        /// <param name="start">ThreadStart</param>
        /// <param name="threadName">线程名</param>
        /// <returns>Thread对象</returns>
        public static Thread CreateBackgroundThread( ThreadStart start, string threadName )
        {
            return new Thread( start ) { Name = threadName, Priority = ThreadPriority.Normal, IsBackground = true };
        }

        /// <summary>
        /// 创建后台线程
        /// </summary>
        /// <param name="start">ParameterizedThreadStart</param>
        /// <param name="threadName">线程名</param>
        /// <returns>Thread对象</returns>
        public static Thread CreateBackgroundThread( ParameterizedThreadStart start, string threadName )
        {
            return new Thread( start ) { Name = threadName, Priority = ThreadPriority.Normal, IsBackground = true };
        }

        /// <summary>
        /// 创建线程
        /// </summary>
        /// <param name="start">ThreadStart</param>
        /// <param name="threadName">线程名</param>
        /// <returns>Thread对象</returns>
        public static Thread CreateThread( ThreadStart start, string threadName )
        {
            return new Thread( start ) { Name = threadName };
        }

        /// <summary>
        /// 创建线程
        /// </summary>
        /// <param name="start">ParameterizedThreadStart</param>
        /// <param name="threadName">线程名</param>
        /// <returns>Thread对象</returns>
        public static Thread CreateThread( ParameterizedThreadStart start, string threadName )
        {
            return new Thread( start ) { Name = threadName };
        }

        #endregion

        #region 线程睡眠

        /// <summary>
        /// 让当前线程睡眠，使用Thread.Interrupt唤醒
        /// 参考：ms-help://MS.VSCC.v90/MS.MSDNQTR.v90.chs/fxref_mscorlib/html/4edbcb9f-8e93-65b1-23e2-49877d968c90.htm
        /// </summary>
        public static void ThreadSleep()
        {
            ThreadSleep( Timeout.Infinite );
        }

        /// <summary>
        /// 让当前线程睡眠，使用Thread.Interrupt唤醒
        /// </summary>
        /// <param name="millisecondsTimeout">超时（毫秒）</param>
        public static void ThreadSleep( int millisecondsTimeout )
        {
            try
            {
                Thread.Sleep( millisecondsTimeout );
            }
            catch( ThreadInterruptedException )
            {
                // 被唤醒
            }
        }

        #endregion

        #region 开始/停止线程

        /// <summary>
        /// 停止线程
        /// </summary>
        /// <param name="threads">线程集合</param>
        public static void InterruptAndJoin( params Thread[] threads )
        {
            foreach( Thread thread in threads )
            {
                thread.Interrupt();
            }

            Join( threads );
        }

        /// <summary>
        /// 停止线程
        /// </summary>
        /// <param name="threads">线程集合</param>
        public static void Join( params Thread[] threads )
        {
            foreach( Thread thread in threads )
            {
                if (thread == null) continue;

                try
                {
                    thread.Join();

                    TraceUtils.Debug(GetThreadDescription(thread) + "end.");
                }
                catch( Exception )
                {

                }
            }
        }

        /// <summary>
        /// 开始线程运行
        /// </summary>
        /// <param name="threads">线程集合</param>
        public static void Start( params Thread[] threads )
        {
            foreach( Thread thread in threads )
            {
                thread.Start();

                TraceUtils.Debug( GetThreadDescription( thread ) + "start." );
            }
        }

        /// <summary>
        /// 开始线程运行
        /// </summary>
        /// <param name="thread">线程</param>
        /// <param name="parameter">开始参数</param>
        public static void Start( Thread thread, object parameter )
        {
            thread.Start( parameter );

            TraceUtils.Debug( GetThreadDescription( thread ) + "start." );
        }

        /// <summary>
        /// 获得描述线程的字符串
        /// </summary>
        /// <param name="thread">Thread</param>
        /// <returns>描述线程的字符串</returns>
        private static string GetThreadDescription( Thread thread )
        {
            return string.Format( "Thread({0},{1})", thread.Name, thread.ManagedThreadId );
        }

        #endregion

        #region 与同步对象相关

        /// <summary>
        /// 设置EventWaitHandle对象状态为终止状态
        /// </summary>
        /// <param name="eventWaitHandles">EventWaitHandle对象集合</param>
        public static void SetEvents2Signaled( params EventWaitHandle[] eventWaitHandles )
        {
            if( eventWaitHandles != null )
            {
                foreach( EventWaitHandle eventWaitHandle in eventWaitHandles )
                {
                    eventWaitHandle.Set();
                }
            }
        }

        #endregion
    }
}