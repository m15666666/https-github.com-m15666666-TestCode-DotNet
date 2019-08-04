using System;
using System.Threading;
using Moons.Common20;
using System.Collections.Generic;

namespace OnlineSampleCommon.SendTask
{
    /// <summary>
    /// 发送任务的类
    /// </summary>
    public class TaskSender
    {
        public TaskSender()
        {
            TryDelayInSecond = new[] { 1, 5, 60 * 5 };
        }

        #region 变量和属性

        /// <summary>
        /// 发送任务线程名
        /// </summary>
        private const string SendTaskThreadName = "SendTask.TaskSender.SendTask";

        ///<summary>
        /// 发送队列
        /// </summary>
        private readonly LockQueue _sendQueue = new LockQueue();

        ///<summary>
        /// 发送任务线程事件
        /// </summary>
        private readonly AutoResetEvent _sendThreadEvent = new AutoResetEvent( false );

        /// <summary>
        /// 线程状态对象
        /// </summary>
        private readonly ThreadStatusUtils _threadStatus = new ThreadStatusUtils();

        ///<summary>
        /// 发送任务线程
        /// </summary>
        private Thread _sendThread;

        /// <summary>
        /// 最大队列长度
        /// </summary>
        public int MaxQueueLength { private get; set; }

        /// <summary>
        /// 底层发送任务的接口
        /// </summary>
        public ITaskSender Sender { private get; set; }

        ///<summary>
        /// 发送队列中元素的数量
        /// </summary>
        public int SendQueueCount
        {
            get { return _sendQueue.Count; }
        }

        #region 以秒表示的尝试延时

        /// <summary>
        /// 延时计数器
        /// </summary>
        private readonly DelayCounter _delayCounter = new DelayCounter();

        /// <summary>
        /// 以秒表示的尝试延时
        /// </summary>
        public int[] TryDelayInSecond
        {
            set { _delayCounter.Delays = value; }
        }

        #endregion

        #endregion

        #region 操作函数

        /// <summary>
        /// 任务进队
        /// </summary>
        /// <param name="obj">进队的发送任务对象</param>
        public void Add( object obj )
        {
            if( _threadStatus.IsStopped )
            {
                return;
            }

            // 队列已满，返回。
            if( MaxQueueLength <= SendQueueCount )
            {
                TraceUtils.Info( "Sending queue is full." );
            }
            else
            {
                _sendQueue.Add( obj );
                _sendThreadEvent.Set();
            }
        }

        /// <summary>
        /// 开始发送
        /// </summary>
        public void StartSend()
        {
            _threadStatus.Start();
            StartSendThread();
        }

        /// <summary>
        /// 停止发送
        /// </summary>
        public void StopSend()
        {
            _threadStatus.Stop();
            _sendThreadEvent.Set();

            ClearQueue();
            StopSendThread();

            Sender.StopSend();
        }

        /// <summary>
        /// 清空队列
        /// </summary>
        private void ClearQueue()
        {
            _sendQueue.Clear();
        }

        #endregion

        #region 任务发送线程相关

        /// <summary>
        /// 创建并开始任务发送线程
        /// </summary>
        private void StartSendThread()
        {
            _sendThread = ThreadUtils.CreateBackgroundThread( SendTask, SendTaskThreadName );
            ThreadUtils.Start( _sendThread );
        }

        /// <summary>
        /// 停止任务发送线程
        /// </summary>
        private void StopSendThread()
        {
            ThreadUtils.Join( _sendThread );
            _sendThread = null;
        }

        /// <summary>
        /// 发送任务
        /// </summary>
        private void SendTask()
        {
            // 延迟检查的毫秒数
            const int Delay4Check = 500;

            // 下次尝试的时间，DateTime.MinValue表示立即可以尝试
            DateTime nextTryTime = DateTime.MinValue;

            // 每次从队列取出的元素个数
            const int PopCount = 10;

            // 发送失败队列，在发送失败的情况下，用于再次加入到发送队列
            var reAdd2SendQueue = new List<object>( PopCount );

            while( _threadStatus.IsStarted )
            {
                // 如果没有需要发送的数据则等待
                if( !_sendQueue.HasData )
                {
                    _sendThreadEvent.WaitOne();
                    continue;
                }

                // 如果没到下次尝试的时间则睡眠等待
                if( DateTime.Now < nextTryTime )
                {
                    // 等待期间，不清空队列，以免无线传感器丢失数据(无线传感器2个小时才上传一组数据)
                    //ClearQueue();

                    ThreadUtils.ThreadSleep( Delay4Check );
                    continue;
                }

                // 发送数据
                try
                {
                    object[] items = _sendQueue.Pop( PopCount );

                    reAdd2SendQueue.Clear();
                    reAdd2SendQueue.AddRange( items );

                    foreach( object item in items )
                    {
                        string emsg = null;
                        Sender.Send( item, ref emsg );

                        // 通讯成功，重置延时
                        _delayCounter.ResetDelay();

                        if( ErrorUtils.IsError( emsg ) )
                        {
                            TraceUtils.Error( SendTaskThreadName + "send data error: " + emsg );
                        }
                        else
                        {
                            // 发送成功，从失败队列中移除数据
                            reAdd2SendQueue.Remove( item );
                        }
                    }
                }
                catch( Exception ex )
                {
                    // 将失败数据再次加入发送队列
                    foreach( var item in reAdd2SendQueue )
                    {
                        _sendQueue.Add( item );
                    }

                    Sender.StopSend();

                    int delay = _delayCounter.Delay;
                    nextTryTime = DateTime.Now.AddSeconds( delay );

                    // 增加延时
                    _delayCounter.IncreaseDelay();

                    // 发送数据失败
                    TraceUtils.Error( string.Format( "Fail to send data, wait{0} seconds, resend data.", delay ), ex );

                    continue;
                }
            } // while( _threadStatus.IsStarted )

            // 记录停止发送的时间
            TraceUtils.Info( SendTaskThreadName + "stop sending data." );
        }

        #endregion
    }
}