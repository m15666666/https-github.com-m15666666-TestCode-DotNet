namespace Moons.Common20
{
    /// <summary>
    /// 线程状态的实用工具类
    /// </summary>
    public class ThreadStatusUtils
    {
        #region 变量和属性

        /// <summary>
        /// 是否停止线程
        /// </summary>
        private bool _stopThread;

        /// <summary>
        /// 是否停止了线程
        /// </summary>
        public bool IsStopped
        {
            get { return _stopThread; }
        }

        /// <summary>
        /// 是否开始了线程
        /// </summary>
        public bool IsStarted
        {
            get { return !_stopThread; }
        }

        #endregion

        #region 设置状态

        /// <summary>
        /// 设置线程停止
        /// </summary>
        public void Stop()
        {
            _stopThread = true;
        }

        /// <summary>
        /// 设置线程开始
        /// </summary>
        public void Start()
        {
            _stopThread = false;
        }

        #endregion
    }
}