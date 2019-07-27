using System;

namespace Moons.Common20.LongFile
{
    /// <summary>
    /// 如果文件列表已循环时是否继续的代理
    /// </summary>
    /// <returns>true--继续,false--不继续</returns>
    public delegate bool ContinueOnCycleHandler();

    /// <summary>
    /// 多文件读写的基类
    /// </summary>
    public class MultiFileReadAppendBase : IDisposable
    {
        #region 变量和属性

        /// <summary>
        /// Int32变量的长度
        /// </summary>
        protected const int IntSize = sizeof(int);

        /// <summary>
        /// 如果文件列表已循环时是否继续
        /// </summary>
        protected bool _continueOnCycle = true;

        /// <summary>
        /// 当前文件路径的下标
        /// </summary>
        private int _currentPathIndex;

        /// <summary>
        /// 文件路径的集合
        /// </summary>
        public string[] Paths { get; set; }

        /// <summary>
        /// 当前的文件路径
        /// </summary>
        protected string CurrentPath
        {
            get { return Paths[_currentPathIndex]; }
        }

        /// <summary>
        /// 如果文件列表已循环时是否继续的代理
        /// </summary>
        public ContinueOnCycleHandler ContinueOnCycle { get; set; }

        #endregion

        #region 自定义函数

        /// <summary>
        /// 循环移动当前文件路径的下标到下一个
        /// </summary>
        /// <returns>下标是否循环</returns>
        protected bool CycleMoveNextPathIndex()
        {
            _currentPathIndex++;

            bool isCycle = ( Paths.Length <= _currentPathIndex );
            if( isCycle )
            {
                _currentPathIndex = 0;

                ContinueOnCycleHandler continueOnCycle = ContinueOnCycle;
                if( continueOnCycle != null )
                {
                    _continueOnCycle = continueOnCycle();
                }
            }

            return isCycle;
        }

        #endregion

        #region 需要重载的成员

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected virtual void Dispose( bool disposing )
        {
        }

        #endregion

        #region IDisposable 成员

        public void Dispose()
        {
            Dispose( true );
        }

        #endregion
    }
}