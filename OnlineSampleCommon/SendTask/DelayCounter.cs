using System;

namespace OnlineSampleCommon.SendTask
{
    /// <summary>
    /// 延时计数器
    /// </summary>
    public class DelayCounter
    {
        public DelayCounter()
        {
            Delays = new[] { 1 };
        }

        #region 变量和属性

        /// <summary>
        /// 当前延时的下标
        /// </summary>
        private int _delayIndex;

        /// <summary>
        /// 延时数组
        /// </summary>
        public int[] Delays { private get; set; }

        /// <summary>
        /// 当前延时
        /// </summary>
        public int Delay
        {
            get
            {
                _delayIndex = Math.Min( Math.Max( 0, _delayIndex ), Delays.Length - 1 );
                return Delays[_delayIndex];
            }
        }

        #endregion

        /// <summary>
        /// 重置延时
        /// </summary>
        public void ResetDelay()
        {
            _delayIndex = 0;
        }

        /// <summary>
        /// 增加延时
        /// </summary>
        public void IncreaseDelay()
        {
            _delayIndex++;
        }
    }
}