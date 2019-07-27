using System;
using System.Threading;

namespace Moons.Common20
{
    /// <summary>
    /// 定时器包装器类
    /// </summary>
    public class TimerWrapper : IDisposable
    {
        /// <summary>
        /// ctor
        /// </summary>
        public TimerWrapper()
        {
            Interval = new TimeSpan( 0, 0, 1 );
        }

        #region 变量和属性

        /// <summary>
        /// 用于停止的DueTime
        /// </summary>
        private static readonly TimeSpan StopDueTime = new TimeSpan( 0, 0, 0, 0, -1 );

        /// <summary>
        /// 用于停止的Period
        /// </summary>
        private static readonly TimeSpan StopPeriod = new TimeSpan( 0, 0, 0, 0, -1 );

        /// <summary>
        /// 内部锁
        /// </summary>
        private readonly object _lock = new object();

        /// <summary>
        /// 是否已经释放了资源
        /// </summary>
        private bool _disposed;

        /// <summary>
        /// 内部定时器
        /// </summary>
        private Timer _innerTimer;

        /// <summary>
        /// 是否已经开启了定时器
        /// </summary>
        private bool _started;

        /// <summary>
        /// 回调函数
        /// </summary>
        public TimerCallback Callback { get; set; }

        /// <summary>
        /// 状态对象
        /// </summary>
        public object StateObject { get; set; }

        /// <summary>
        /// 定时执行的间隔
        /// </summary>
        public TimeSpan Interval { get; set; }

        #endregion

        /// <summary>
        /// 开始定时器
        /// </summary>
        public void Start()
        {
            lock( _lock )
            {
                if( _innerTimer == null )
                {
                    _innerTimer = new Timer( OnTick );
                }

                StartTimer();
            }
        }

        /// <summary>
        /// 停止定时器
        /// </summary>
        public void Stop()
        {
            lock( _lock )
            {
                StopTimer();
            }
        }

        /// <summary>
        /// 响应tick事件
        /// </summary>
        /// <param name="stateInfo">状态对象</param>
        private void OnTick( object stateInfo )
        {
            lock( _lock )
            {
                if( _disposed || !_started )
                {
                    return;
                }

                try
                {
                    Callback( StateObject );
                }
                catch
                {
                    // 不抛出异常
                }

                // 开始下一次计时
                if( _started )
                {
                    try
                    {
                        StartTimer( _innerTimer );
                    }
                    catch
                    {
                        // 不抛出异常
                    }
                }
            } // lock( _lock )
        }

        /// <summary>
        /// 开启定时器
        /// </summary>
        private void StartTimer()
        {
            if( _started )
            {
                return;
            }

            _started = true;
            StartTimer( _innerTimer );
        }

        /// <summary>
        /// 开启定时器
        /// </summary>
        /// <param name="timer">定时器</param>
        private void StartTimer( Timer timer )
        {
            if( timer != null )
            {
                timer.Change( Interval, StopPeriod );
            }
        }

        /// <summary>
        /// 停止定时器
        /// </summary>
        private void StopTimer()
        {
            if( _started )
            {
                _started = false;
            }
        }

        /// <summary>
        /// 停止定时器
        /// </summary>
        /// <param name="timer">定时器</param>
        private void StopTimer( Timer timer )
        {
            if( timer != null )
            {
                timer.Change( StopDueTime, StopPeriod );
            }
        }

        #region IDisposable 成员

        /// <summary>
        /// dispose函数
        /// </summary>
        public void Dispose()
        {
            lock( _lock )
            {
                if( _disposed )
                {
                    return;
                }

                _disposed = true;

                StopTimer();

                Timer timer = _innerTimer;
                if( timer != null )
                {
                    _innerTimer = null;
                    timer.Dispose();
                }
            }
        }

        #endregion
    }
}