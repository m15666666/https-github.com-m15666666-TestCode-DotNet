using System;
using System.Runtime.InteropServices;

namespace Moons.Common20
{
    /// <summary>
    /// DLL包装的实用工具类
    /// </summary>
    public static class DLLWrapperUtils
    {
        #region 变量和属性

        /// <summary>
        /// kernel32.dll路径
        /// </summary>
        private const string Kernel32_DLL = "kernel32.dll";

        /// <summary>
        /// 无效的句柄
        /// </summary>
        private const int InvalidHandle = 0;

        #endregion

        #region Kernel32 DllImport

        ///<summary>
        /// API LoadLibrary
        ///</summary>
        [DllImport( Kernel32_DLL )]
        private static extern int LoadLibrary( string libPath );

        ///<summary>
        /// API GetProcAddress
        ///</summary>
        [DllImport( Kernel32_DLL )]
        private static extern IntPtr GetProcAddress( IntPtr handle, string functionName );

        ///<summary>
        /// API FreeLibrary
        ///</summary>
        [DllImport( Kernel32_DLL )]
        private static extern int FreeLibrary( IntPtr handle );

        #endregion

        #region 加载/卸载 DLL

        /// <summary>
        /// 加载DLL
        /// </summary>
        /// <param name="dllPath">DLL路径</param>
        /// <param name="handle">DLL句柄，输出</param>
        /// <returns>是否加载成功</returns>
        public static bool LoadDLL( string dllPath, out IntPtr handle )
        {
            int dllModule;
            bool ret = LoadDLL( dllPath, out dllModule );
            handle = ret ? new IntPtr( dllModule ) : IntPtr.Zero;

            return ret;
        }

        /// <summary>
        /// 加载DLL
        /// </summary>
        /// <param name="dllPath">DLL路径</param>
        /// <param name="handle">DLL句柄，输出</param>
        /// <returns>是否加载成功</returns>
        public static bool LoadDLL( string dllPath, out int handle )
        {
            handle = LoadLibrary( dllPath );

            return !IsNULL( handle );
        }

        /// <summary>
        /// 卸载DLL
        /// </summary>
        /// <param name="handle">DLL句柄</param>
        public static void UnLoadDLL( IntPtr handle )
        {
            if( !IsNULL( handle ) )
            {
                FreeLibrary( handle );
            }
        }

        /// <summary>
        /// 卸载DLL
        /// </summary>
        /// <param name="handle">DLL句柄</param>
        public static void UnLoadDLL( int handle )
        {
            if( !IsNULL( handle ) )
            {
                UnLoadDLL( new IntPtr( handle ) );
            }
        }

        #endregion

        #region 加载函数

        /// <summary>
        /// 通过非托管函数名转换为对应的委托
        /// </summary>
        /// <typeparam name="T">对应的委托类型</typeparam>
        ///<param name="dllModule">DLL句柄</param>
        ///<param name="functionName">非托管函数名</param>
        ///<returns>委托实例</returns>
        public static T GetDelegateByProcName<T>( IntPtr dllModule, string functionName ) where T : class
        {
            return GetDelegateByProcAddress<T>( GetProcAddress( dllModule, functionName ) );
        }

        /// <summary>
        /// 通过非托管函数名转换为对应的委托
        /// </summary>
        /// <typeparam name="T">对应的委托类型</typeparam>
        /// <param name="dllModule">DLL句柄</param>
        /// <param name="functionName">非托管函数名</param>
        /// <returns>委托实例</returns>
        public static T GetDelegateByProcName<T>( int dllModule, string functionName ) where T : class
        {
            return GetDelegateByProcName<T>( new IntPtr( dllModule ), functionName );
        }

        /// <summary>
        /// 将函数地址转换成对应的委托
        /// </summary>
        /// <typeparam name="T">对应的委托类型</typeparam>
        /// <param name="address">函数地址</param>
        /// <returns>委托实例</returns>
        public static T GetDelegateByProcAddress<T>( IntPtr address ) where T : class
        {
            return IsNULL( address ) ? null : Marshal.GetDelegateForFunctionPointer( address, typeof(T) ) as T;
        }

        /// <summary>
        /// 将函数地址转换成对应的委托
        /// </summary>
        /// <typeparam name="T">对应的委托类型</typeparam>
        /// <param name="address">函数地址</param>
        /// <returns>委托实例</returns>
        public static T GetDelegateByProcAddress<T>( int address ) where T : class
        {
            return IsNULL( address ) ? null : GetDelegateByProcAddress<T>( new IntPtr( address ) );
        }

        #endregion

        #region IsNULL

        /// <summary>
        /// 地址/句柄是否为空
        /// </summary>
        /// <param name="handle">地址/句柄</param>
        /// <returns>是否为空</returns>
        public static bool IsNULL( int handle )
        {
            return handle == InvalidHandle;
        }

        /// <summary>
        /// 地址/句柄是否为空
        /// </summary>
        /// <param name="handle">地址/句柄</param>
        /// <returns>是否为空</returns>
        public static bool IsNULL( IntPtr handle )
        {
            return handle == IntPtr.Zero;
        }

        #endregion
    }
}