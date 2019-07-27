using System;
using System.ComponentModel;

namespace Moons.Common20.Attribute
{
    /// <summary>
    /// 日志记录属性
    /// </summary>
    [AttributeUsage( AttributeTargets.Method, AllowMultiple = false, Inherited = false )]
    public class LogIndicatorAttribute : System.Attribute
    {
        public LogIndicatorAttribute()
        {
            IsWriteLog = true;
        }

        /// <summary>
        /// 是否记录日志
        /// </summary>
        [DefaultValue( true )]
        public bool IsWriteLog { get; set; }

        /// <summary>
        /// 操作CD
        /// </summary>
        [DefaultValue( "" )]
        public string ActionCD { get; set; }

        /// <summary>
        /// 操作描述
        /// </summary>
        [DefaultValue( "" )]
        public string ActionDesc { get; set; }

        /// <summary>
        /// 子操作描述Key
        /// </summary>
        [DefaultValue( "" )]
        public string ChildActionDescKey { get; set; }

        /// <summary>
        /// 子操作对象描述Key
        /// </summary>
        [DefaultValue( "" )]
        public string ChildActionObjKey { get; set; }
    }
}