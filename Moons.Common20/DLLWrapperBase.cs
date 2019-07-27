using System;

namespace Moons.Common20
{
    /// <summary>
    /// DLL（动态链接库）包装器的基类
    /// </summary>
    public abstract class DLLWrapperBase : DisposableBase
    {
        #region 变量和属性

        /// <summary>
        /// DLL路径
        /// </summary>
        public string DLLPath { get; set; }

        /// <summary>
        /// DLL句柄
        /// </summary>
        private IntPtr _dllHandle = IntPtr.Zero;

        #endregion

        #region 操作DLL

        /// <summary>
        /// 加载DLL，失败则抛出异常
        /// </summary>
        public void LoadDLL_Exception()
        {
            bool success = DLLWrapperUtils.LoadDLL( DLLPath, out _dllHandle );
            if( !success )
            {
                throw new ArgumentException( string.Format( "加载DLL失败：{0}", DLLPath ) );
            }
        }

        /// <summary>
        /// 通过非托管函数名转换为对应的委托
        /// </summary>
        /// <typeparam name="T">对应的委托类型</typeparam>
        ///<param name="functionName">非托管函数名</param>
        ///<returns>委托实例，可强制转换为适当的委托类型</returns>
        public T GetDelegateByProcName<T>( string functionName ) where T : class
        {
            return DLLWrapperUtils.GetDelegateByProcName<T>( _dllHandle, functionName );
        }

        #endregion

        #region Dispose

        /// <summary>
        /// 卸载DLL
        /// </summary>
        private void UnLoadDLL()
        {
            if( !DLLWrapperUtils.IsNULL( _dllHandle ) )
            {
                DLLWrapperUtils.UnLoadDLL( _dllHandle );
                _dllHandle = IntPtr.Zero;
            }
        }

        protected override void Dispose( bool disposing )
        {
            if( IsDisposed )
            {
                return;
            }

            UnLoadDLL();

            //  一定要调用基类的Dispose函数
            base.Dispose( disposing );
        }

        #endregion
    }
}