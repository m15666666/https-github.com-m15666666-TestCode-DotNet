namespace Moons.Common20
{
    /// <summary>
    ///     字符大小写实用工具类，用于需要统一大小写的场合
    /// </summary>
    public static class CharCaseUtils
    {
        static CharCaseUtils()
        {
            ToLower = true;
        }

        #region 变量和属性

        /// <summary>
        ///     是否将字符全部转换为小写（否则为大写），默认为true
        /// </summary>
        public static bool ToLower { get; set; }

        #endregion

        #region 将字符串转换为大(小)写格式

        /// <summary>
        ///     将字符串转换为固定的（保存在数据库中不能再次更改）小写格式
        /// </summary>
        /// <param name="text">原始字符串</param>
        /// <returns>转换后的字符串</returns>
        public static string ToDBCase( string text )
        {
            return StringUtils.ToLower( text );
        }

        /// <summary>
        ///     将字符串转换为大(小)写格式
        /// </summary>
        /// <param name="text">原始字符串</param>
        /// <returns>转换后的字符串</returns>
        public static string ToCase( string text )
        {
            if( string.IsNullOrEmpty( text ) )
            {
                return text;
            }

            return ToLower ? StringUtils.ToLower( text ) : StringUtils.ToUpper( text );
        }

        /// <summary>
        ///     将字符串转换为大(小)写格式
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>转换后的字符串</returns>
        public static string ToCase( object obj )
        {
            return ToCase( StringUtils.ToString( obj ) );
        }

        #endregion
    }
}