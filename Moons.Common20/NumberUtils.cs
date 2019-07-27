namespace Moons.Common20
{
    /// <summary>
    /// 数值的实用工具类
    /// </summary>
    public static class NumberUtils
    {
        #region 格式转换

        /// <summary>
        /// 转换为十六进制表示的字符串
        /// </summary>
        /// <param name="number">数值</param>
        /// <returns>十六进制表示的字符串</returns>
        public static string ToHex( int number )
        {
            return ToHex( number, 0 );
        }

        /// <summary>
        /// 转换为十六进制表示的字符串，如有必要在前面补零
        /// </summary>
        /// <param name="number">数值</param>
        /// <param name="charCount">要求的字符数</param>
        /// <returns>十六进制表示的字符串</returns>
        public static string ToHex( int number, int charCount )
        {
            const string HexFormat = "X";
            if( charCount <= 0 )
            {
                return number.ToString( HexFormat );
            }

            return number.ToString( HexFormat + charCount );
        }

        #endregion
    }
}