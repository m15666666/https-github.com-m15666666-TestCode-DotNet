using System;
using System.Runtime.InteropServices;

namespace Moons.Common20.PC
{
    /// <summary>
    /// 与重启计算机相关的实用工具类
    /// </summary>
    public static class ShutdownUtils
    {
        #region 底层api调用

        /// <summary>
        /// 获得当前进程
        /// </summary>
        /// <returns></returns>
        [DllImport( "kernel32.dll", ExactSpelling = true )]
        private static extern IntPtr GetCurrentProcess();

        /// <summary>
        /// 打开令牌
        /// </summary>
        /// <param name="h"></param>
        /// <param name="acc"></param>
        /// <param name="phtok"></param>
        /// <returns></returns>
        [DllImport( "advapi32.dll", ExactSpelling = true, SetLastError = true )]
        private static extern bool OpenProcessToken( IntPtr h, int acc, ref IntPtr phtok );

        /// <summary>
        /// 查找权限值
        /// </summary>
        /// <param name="host"></param>
        /// <param name="name"></param>
        /// <param name="pluid"></param>
        /// <returns></returns>
        [DllImport( "advapi32.dll", SetLastError = true )]
        private static extern bool LookupPrivilegeValue( string host, string name, ref long pluid );

        /// <summary>
        /// 调整令牌权限
        /// </summary>
        /// <param name="htok"></param>
        /// <param name="disall"></param>
        /// <param name="newst"></param>
        /// <param name="len"></param>
        /// <param name="prev"></param>
        /// <param name="relen"></param>
        /// <returns></returns>
        [DllImport( "advapi32.dll", ExactSpelling = true, SetLastError = true )]
        private static extern bool AdjustTokenPrivileges( IntPtr htok, bool disall,
                                                          ref TokPriv1Luid newst, int len, IntPtr prev, IntPtr relen );

        /// <summary>
        /// 退出windows系统
        /// </summary>
        /// <param name="uFlags"></param>
        /// <param name="dwReason"></param>
        /// <returns></returns>
        [DllImport( "user32.dll", ExactSpelling = true, SetLastError = true )]
        private static extern bool ExitWindowsEx( int uFlags, int dwReason );

        /// <summary>
        /// 对当前进程使能关机优先权 
        /// </summary>
        private static void AdjustTokenPrivileges()
        {
            IntPtr hproc = GetCurrentProcess();

            IntPtr htok = IntPtr.Zero;
            const int TOKEN_QUERY = 0x00000008;
            const int TOKEN_ADJUST_PRIVILEGES = 0x00000020;
            bool ok = OpenProcessToken( hproc, TOKEN_ADJUST_PRIVILEGES | TOKEN_QUERY, ref htok );
            if( !ok )
            {
                throw new Exception( "打开当前进程的令牌失败！" );
            }

            const int SE_PRIVILEGE_ENABLED = 0x00000002;
            TokPriv1Luid priv1Luid;
            priv1Luid.Count = 1; // 设置一个优先权
            priv1Luid.Luid = 0;
            priv1Luid.Attr = SE_PRIVILEGE_ENABLED;

            // 获得关机优先权的LUID
            const string SE_SHUTDOWN_NAME = "SeShutdownPrivilege";
            ok = LookupPrivilegeValue( null, SE_SHUTDOWN_NAME, ref priv1Luid.Luid );
            if( !ok )
            {
                throw new Exception( "获得关机优先权的LUID失败！" );
            }

            // 对当前进程使能关机优先权 
            ok = AdjustTokenPrivileges( htok, false, ref priv1Luid, 0, IntPtr.Zero, IntPtr.Zero );
            if( !ok )
            {
                throw new Exception( "对当前进程使能关机优先权失败！" );
            }
        }

        /// <summary>
        /// 退出系统
        /// </summary>
        /// <param name="flags">ExitWindows</param>
        private static bool ExitWindows( ShutdownFlag flags )
        {
            const int shutdownReason = (int)( ShutdownReason.MajorOther & ShutdownReason.MinorOther );
            //var shutdownFlags = (int)flags;
            //if( ExitWindowsEx( shutdownFlags, shutdownReason ) )
            //{
            //    return true;
            //}

            var forceFlags = (int)( ShutdownFlag.Force | flags );
            if( ExitWindowsEx( forceFlags, shutdownReason ) )
            {
                return true;
            }

            // 由于缺少权限而失败，因此对当前进程提升权限。
            AdjustTokenPrivileges();

            //if( ExitWindowsEx( shutdownFlags, shutdownReason ) )
            //{
            //    return true;
            //}

            return ExitWindowsEx( forceFlags, shutdownReason );
        }

        #region Nested type: ShutdownFlag

        /// <summary>
        /// 重启标志(flag)
        /// </summary>
        [Flags]
        private enum ShutdownFlag : uint
        {
            /// <summary>
            /// 注销
            /// </summary>
            LogOff = 0x00,

            /// <summary>
            /// 关机
            /// </summary>
            ShutDown = 0x01,

            /// <summary>
            /// 重启
            /// </summary>
            Reboot = 0x02,

            Force = 0x04,

            PowerOff = 0x08,

            ForceIfHung = 0x10
        }

        #endregion

        #region Nested type: ShutdownReason

        /// <summary>
        /// 重启原因
        /// </summary>
        [Flags]
        private enum ShutdownReason : uint
        {
            MajorApplication = 0x00040000,
            MajorHardware = 0x00010000,
            MajorLegacyApi = 0x00070000,
            MajorOperatingSystem = 0x00020000,
            MajorOther = 0x00000000,
            MajorPower = 0x00060000,
            MajorSoftware = 0x00030000,
            MajorSystem = 0x00050000,

            MinorBlueScreen = 0x0000000F,
            MinorCordUnplugged = 0x0000000b,
            MinorDisk = 0x00000007,
            MinorEnvironment = 0x0000000c,
            MinorHardwareDriver = 0x0000000d,
            MinorHotfix = 0x00000011,
            MinorHung = 0x00000005,
            MinorInstallation = 0x00000002,
            MinorMaintenance = 0x00000001,
            MinorMMC = 0x00000019,
            MinorNetworkConnectivity = 0x00000014,
            MinorNetworkCard = 0x00000009,
            MinorOther = 0x00000000,
            MinorOtherDriver = 0x0000000e,
            MinorPowerSupply = 0x0000000a,
            MinorProcessor = 0x00000008,
            MinorReconfig = 0x00000004,
            MinorSecurity = 0x00000013,
            MinorSecurityFix = 0x00000012,
            MinorSecurityFixUninstall = 0x00000018,
            MinorServicePack = 0x00000010,
            MinorServicePackUninstall = 0x00000016,
            MinorTermSrv = 0x00000020,
            MinorUnstable = 0x00000006,
            MinorUpgrade = 0x00000003,
            MinorWMI = 0x00000015,

            FlagUserDefined = 0x40000000,
            FlagPlanned = 0x80000000
        }

        #endregion

        #region Nested type: TokPriv1Luid

        /// <summary>
        /// 权限令牌的结构体
        /// </summary>
        [StructLayout( LayoutKind.Sequential, Pack = 1 )]
        internal struct TokPriv1Luid
        {
            public int Count;
            public long Luid;
            public int Attr;
        }

        #endregion

        #endregion

        /// <summary>
        /// 注销
        /// </summary>
        /// <returns>true：成功，false：不成功</returns>
        public static bool LogOff()
        {
            return ExitWindows( ShutdownFlag.LogOff );
        }

        /// <summary>
        /// 关机
        /// </summary>
        /// <returns>true：成功，false：不成功</returns>
        public static bool Shutdown()
        {
            return ExitWindows( ShutdownFlag.ShutDown );
        }

        /// <summary>
        /// 重启计算机
        /// </summary>
        /// <returns>true：成功，false：不成功</returns>
        public static bool Reboot()
        {
            return ExitWindows( ShutdownFlag.Reboot );
        }
    }
}