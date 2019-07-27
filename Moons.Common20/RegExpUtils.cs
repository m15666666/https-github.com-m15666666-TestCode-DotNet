using System.Text.RegularExpressions;

namespace Moons.Common20
{
    /// <summary>
    /// 正则表达式实用工具类
    /// </summary>
    public static class RegExpUtils
    {
        /// <summary>
        /// 正式表达式验证
        /// </summary>
        /// <param name="input">验证字符</param>
        /// <param name="pattern">正式表达式</param>
        /// <returns>符合true不符合false</returns>
        public static bool IsMatch( string input, string pattern )
        {
            return ( new Regex( pattern, RegexOptions.Compiled ) ).Match( input ).Success;
        }
    }
}