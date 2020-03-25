#region using

using System;
using Moons.Common20.StringResources;

#endregion

namespace Moons.Common20
{
    /// <summary>
    /// 应用程序类型
    /// </summary>
    public enum ApplicationType
    {
        /// <summary>
        /// 未知
        /// </summary>
        Unknow,

        /// <summary>
        /// winform
        /// </summary>
        Winform,

        /// <summary>
        /// 运行在iis上的web程序
        /// </summary>
        Web,

        /// <summary>
        /// Silverlight程序
        /// </summary>
        Silverlight,
    }

    /// <summary>
    /// 表示环境的实用工具类
    /// </summary>
    public static class EnvironmentUtils
    {
        #region ctor

        static EnvironmentUtils()
        {
            StringResource = new DictionaryGetString();

            ExternalParameters = new DictionaryGetString();

            DesignMode = true;
        }

        #endregion

        #region 运行模式/设计模式

        /// <summary>
        /// 是否为设计时
        /// </summary>
        public static bool DesignMode { get; set; }

        /// <summary>
        /// 是否为运行时
        /// </summary>
        public static bool RunMode
        {
            get { return !DesignMode; }
            set { DesignMode = !value; }
        }

        #endregion

        #region 调试模式

        /// <summary>
        /// 是否是调试模式
        /// </summary>
        public static bool IsDebug { get; set; }

        /// <summary>
        /// 是否是发布模式（非调试模式）
        /// </summary>
        public static bool IsRelease
        {
            get { return !IsDebug; }
        }

        #endregion

        #region 是否程序隐藏主窗口

        /// <summary>
        /// 是否程序隐藏主窗口。True：隐藏主窗口，False：不隐藏
        /// </summary>
        public static bool IsHideWindow { get; set; }

        #endregion

        #region 平台、操作系统信息

        /// <summary>
        /// 平台是否为64位。注意不是操作系统，64位操作系统上按照32位运行的话，平台还是32位的。
        /// </summary>
        public static bool IsPlatform64
        {
            get { return IntPtr.Size == 8; }
        }

        #endregion

        #region 应用程序类型

        /// <summary>
        /// 应用程序类型
        /// </summary>
        private static ApplicationType _applicationType = ApplicationType.Unknow;

        /// <summary>
        /// 应用程序类型
        /// </summary>
        public static ApplicationType ApplicationType
        {
            get
            {
                // 参考：http://topic.csdn.net/u/20081230/23/f01355cf-697c-4965-97c0-649155e3b88e.html
                if( _applicationType == ApplicationType.Unknow )
                {
                }
                return _applicationType;
            }
            set { _applicationType = value; }
        }

        /// <summary>
        /// 是否为web应用程序
        /// </summary>
        public static bool IsWebApplication
        {
            get { return ApplicationType == ApplicationType.Web; }
        }

        /// <summary>
        /// 是否为Winform应用程序
        /// </summary>
        public static bool IsWinformApplication
        {
            get { return ApplicationType == ApplicationType.Winform; }
        }

        /// <summary>
        /// 是否为Silverlight应用程序
        /// </summary>
        public static bool IsSilverlightApplication
        {
            get { return ApplicationType == ApplicationType.Silverlight; }
        }

        #endregion

        #region 字符串资源

        /// <summary>
        /// 以字符串表示的语言编码，例如：en-US、zh-CN
        /// </summary>
        private static string _languageCode;

        /// <summary>
        /// 以字符串表示的语言编码，例如：en-US、zh-CN
        /// </summary>
        public static string LanguageCode
        {
            get { return _languageCode; }
            set { _languageCode = CharCaseUtils.ToCase( !string.IsNullOrEmpty( value ) ? value : LanguageCodes.zh_Cn ); }
        }

        /// <summary>
        /// 字符串资源接口
        /// </summary>
        public static IGetString StringResource { get; set; }

        /// <summary>
        /// 字符串资源改变事件
        /// </summary>
        public static event Action20 StringResourceChanged;
#if !SILVERLIGHT
        /// <summary>
        /// 激发字符串资源改变事件
        /// </summary>
        public static void FireStringResourceChanged()
        {
            EventUtils.FireEvent( StringResourceChanged );
        }
#endif
        #endregion

        #region 输出日志

        /// <summary>
        /// 日志对象
        /// </summary>
        private static readonly LogNetWrapper _logger = new LogNetWrapper();

        /// <summary>
        /// Logger对象
        /// </summary>
        public static ILogNet Logger
        {
            get { return _logger; }
            set { _logger.Logger = value; }
        }

        #endregion

        #region 数据库超时相关

        /// <summary>
        /// 数据库命令超时时间，以秒为单位
        /// </summary>
        public static int DbCmdTimeoutInSecond { get; set; }

        #endregion

        #region 外部参数

        /// <summary>
        /// 外部参数
        /// </summary>
        public static IGetString ExternalParameters { get; set; }

        /// <summary>
        /// 外部配置改变
        /// </summary>
        public static event Action ExternalConfigChanged;

        #endregion
    }
}