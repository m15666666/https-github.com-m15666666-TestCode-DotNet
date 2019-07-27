using System;

namespace Moons.Common20
{
    /// <summary>
    /// Guid的实用工具
    /// 参考：ms-help://MS.VSCC.v90/MS.MSDNQTR.v90.chs/fxref_mscorlib/html/30cb1580-6cda-42a7-1615-3022171352cc.htm
    /// </summary>
    public static class GuidUtils
    {
        /// <summary>
        /// 创建一个新的Guid
        /// </summary>
        public static Guid NewGuid
        {
            get { return System.Guid.NewGuid(); }
        }

        #region Guid 字符串

        /// <summary>
        /// 32 位： xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx 
        /// </summary>
        public static string Guid
        {
            get { return NewGuid.ToString( "N" ); }
        }

        /// <summary>
        /// 由连字符分隔的 32 位数字：xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx 
        /// </summary>
        public static string Guid_D
        {
            get { return NewGuid.ToString( "D" ); }
        }

        /// <summary>
        /// 括在大括号中、由连字符分隔的 32 位数字：{xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx}
        /// </summary>
        public static string Guid_B
        {
            get { return NewGuid.ToString( "B" ); }
        }

        /// <summary>
        /// 括在圆括号中、由连字符分隔的 32 位数字：(xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx)
        /// </summary>
        public static string Guid_P
        {
            get { return NewGuid.ToString( "P" ); }
        }

        #endregion
    }
}