using System;

namespace Moons.Common20
{
    /// <summary>
    /// 枚举相关的实用工具类
    /// </summary>
    public static class EnumUtils
    {
        /// <summary>
        /// 解析字符串为枚举
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="text">字符串</param>
        public static T ParseEnum<T>( string text ) where T : struct
        {
            return (T)Enum.Parse( typeof(T), text, true );
        }

        /// <summary>
        /// findValue是否为values数组中的一个值
        /// </summary>
        /// <param name="findValue">查找的值</param>
        /// <param name="values">枚举数组</param>
        /// <returns>true：是values数组中的一个值，false：不是</returns>
        public static bool IsAny( Enum findValue, params Enum[] values )
        {
            foreach( Enum value in values )
            {
                // 此处比较不能直接用“==”，否则结果不正确。
                if( value.Equals( findValue ) )
                {
                    return true;
                }
            }

            return false;
        }
    }
}