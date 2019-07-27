using System;
using System.Runtime.InteropServices;

namespace Moons.Common20
{
    /// <summary>
    /// 关于Com对象的实用工具
    /// </summary>
    public static class ComUtils
    {
        /// <summary>
        /// 释放Com对象引用
        /// </summary>
        /// <param name="comObjects">Com对象数组</param>
        public static void ReleaseComObject( params object[] comObjects )
        {
            if( comObjects == null )
            {
                return;
            }

            Action<object> releaseCom =
                comObject =>
                    {
                        if( comObject != null )
                        {
                            Marshal.ReleaseComObject( comObject );
                        }
                    };
            Array.ForEach( comObjects, releaseCom );
        }
    }

    /// <summary>
    /// 自动释放的Com对象包装类
    /// </summary>
    public class AutoReleaseComWrapper : IDisposable
    {
        /// <summary>
        /// Com对象数组
        /// </summary>
        public object[] ComObjects { get; set; }

        /// <summary>
        /// 创建AutoReleaseComWrapper对象
        /// </summary>
        /// <param name="comObjects">Com对象数组</param>
        /// <returns>AutoReleaseComWrapper对象</returns>
        public static AutoReleaseComWrapper Create( params object[] comObjects )
        {
            return new AutoReleaseComWrapper { ComObjects = comObjects };
        }

        private AutoReleaseComWrapper()
        {
        }

        public void Dispose()
        {
            ComUtils.ReleaseComObject( ComObjects );
        }
    }
}