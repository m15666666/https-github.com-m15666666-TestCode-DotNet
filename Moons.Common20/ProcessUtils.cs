using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Moons.Common20
{
    /// <summary>
    /// 进程的实用工具
    /// </summary>
    public static class ProcessUtils
    {
        #region 当前进程

        /// <summary>
        /// 当前进程
        /// </summary>
        public static Process CurrentProcess
        {
            get { return Process.GetCurrentProcess(); }
        }

        /// <summary>
        /// 根据进程名避免具有当前进程名的多个进程实例
        /// </summary>
        public static void AvoidMultiInstance()
        {
            using( Process process = CurrentProcess )
            {
                if( 1 < GetProcessCountByName( GetProcessName( process ) ) )
                {
                    process.Kill();
                }
            }
        }

        /// <summary>
        /// 根据主程序路径避免具有当前进程名的多个进程实例
        /// </summary>
        public static void AvoidMultiInstanceByPath()
        {
            using( Process process = CurrentProcess )
            {
                if( 1 < GetProcessCountByPath( process ) )
                {
                    process.Kill();
                }
            }
        }

        #endregion

        #region 进程信息

        #region 模块信息

        /// <summary>
        /// 获得主模块，失败则返回null
        /// </summary>
        /// <param name="process">Process</param>
        /// <returns>主模块</returns>
        private static ProcessModule GetMainModule( Process process )
        {
            try
            {
                // 枚举模块时，可能会抛出“System.ComponentModel.Win32Exception: Unable to enumerate the process modules.”异常。
                return process.MainModule;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 获得主模块路径，失败则返回null
        /// </summary>
        /// <param name="process">Process</param>
        /// <returns>主模块路径</returns>
        private static string GetMainModulePath( Process process )
        {
            ProcessModule mainModule = GetMainModule( process );
            return mainModule != null ? mainModule.FileName : null;
        }

        /// <summary>
        /// 获得主模块路径，失败则返回null
        /// </summary>
        /// <param name="process">Process</param>
        /// <returns>主模块路径</returns>
        private static string GetProcessName( Process process )
        {
            try
            {
                // 该进程没有标识符，或者没有与 Process 实例关联的进程。- 或 - 关联进程已经退出时，可能抛出异常
                return process.ProcessName;
            }
            catch
            {
                return null;
            }
        }

        #endregion

        /// <summary>
        /// 进程是否存在
        /// </summary>
        /// <param name="processName">进程名（不包括路径和后缀）</param>
        /// <returns>是否存在</returns>
        public static bool ProcessExist( string processName )
        {
            return 0 < GetProcessCountByName( processName );
        }

        /// <summary>
        /// 进程是否存在
        /// </summary>
        /// <param name="processName">进程名（不包括路径和后缀）</param>
        /// <param name="mainModulePath">主程序路径</param>
        /// <returns>是否存在</returns>
        public static bool ProcessExist( string processName, string mainModulePath )
        {
            return 0 < GetProcessCountByPath( processName, mainModulePath );
        }

        /// <summary>
        /// 通过进程名获得进程数
        /// </summary>
        /// <param name="processName">进程名（不包括路径和后缀）</param>
        /// <returns>进程数</returns>
        public static int GetProcessCountByName( string processName )
        {
            if( processName == null )
            {
                return 0;
            }

            Process[] processes = Process.GetProcessesByName( processName );
            Release( processes );
            return processes.Length;
        }

        /// <summary>
        /// 通过主程序路径获得进程数
        /// </summary>
        /// <param name="process">Process</param>
        /// <returns>进程数</returns>
        public static int GetProcessCountByPath( Process process )
        {
            return GetProcessCountByPath( GetProcessName( process ), GetMainModulePath( process ) );
        }

        /// <summary>
        /// 通过主程序路径获得进程数
        /// </summary>
        /// <param name="processName">进程名（不包括路径和后缀）</param>
        /// <param name="mainModulePath">主程序路径</param>
        /// <returns>进程数</returns>
        public static int GetProcessCountByPath( string processName, string mainModulePath )
        {
            if( processName == null || mainModulePath == null )
            {
                return 0;
            }

            Process[] processes = Process.GetProcessesByName( processName );
            int count = Array.FindAll( processes, process => StringUtils.EqualIgnoreCase( GetMainModulePath( process ), mainModulePath ) ).Length;
            Release( processes );
            return count;
        }

        /// <summary>
        /// 释放进程使用的资源
        /// </summary>
        /// <param name="processes">进程数组</param>
        private static void Release( IEnumerable<Process> processes )
        {
            if( processes != null )
            {
                foreach( Process process in processes )
                {
                    process.Close();
                }
            }
        }

        #endregion

        #region 启动进程

        /// <summary>
        /// 启动进程
        /// </summary>
        /// <param name="path">可执行文件路径</param>
        /// <returns>启动的进程对象</returns>
        public static Process StartProcess( string path )
        {
            return StartProcess( path, null );
        }

        /// <summary>
        /// 启动进程
        /// </summary>
        /// <param name="path">可执行文件路径</param>
        /// <param name="arguments">启动该进程时传递的命令行参数</param>
        /// <returns>启动的进程对象</returns>
        public static Process StartProcess( string path, string arguments )
        {
            if( arguments == null || arguments.Trim().Length == 0 )
            {
                return Process.Start( path );
            }

            return Process.Start( path, arguments );
        }

        #endregion

        #region 结束进程

        /// <summary>
        /// 结束当前进程进程
        /// </summary>
        public static void KillCurrentProcess()
        {
            KillProcess( CurrentProcess );
        }

        /// <summary>
        /// 结束进程
        /// </summary>
        /// <param name="process">进程</param>
        public static void KillProcess( Process process )
        {
            process.Kill();
            process.Close();
        }

        /// <summary>
        /// 结束相同进程名的所有进程
        /// </summary>
        /// <param name="processName">进程名（不包括路径和后缀）</param>
        public static void KillProcessesByName( string processName )
        {
            Process[] processes = Process.GetProcessesByName( processName );
            try
            {
                Array.ForEach( processes, process => process.Kill() );
            }
            finally
            {
                Release( processes );
            }
        }

        #endregion
    }
}