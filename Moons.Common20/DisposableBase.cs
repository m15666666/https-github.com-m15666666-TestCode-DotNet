using System;

namespace Moons.Common20
{
    /// <summary>
    /// 实现IDisposable接口，释放资源的基类
    /// </summary>
    public abstract class DisposableBase : IDisposable
    {
        #region 变量和属性

        /// <summary>
        /// 是否释放过资源
        /// </summary>
        private bool _disposed;

        /// <summary>
        /// 是否释放过资源
        /// </summary>
        protected bool IsDisposed
        {
            get { return _disposed; }
        }

        #endregion

        #region Implementation of IDisposable

        public void Dispose()
        {
            Dispose( true );

            GC.SuppressFinalize( this );
        }

        /// <summary>
        /// Finalize函数
        /// </summary>
        ~DisposableBase()
        {
            Dispose( false );
        }

        /// <summary>
        /// 重载以释放资源
        /// </summary>
        /// <param name="disposing">true:释放托管资源，false:释放非托管资源</param>
        protected virtual void Dispose( bool disposing )
        {
            _disposed = true;
        }

        #endregion
    }
}