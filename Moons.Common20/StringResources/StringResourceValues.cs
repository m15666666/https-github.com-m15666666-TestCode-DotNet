namespace Moons.Common20.StringResources
{
    /// <summary>
    /// 字符串资源值的集合
    /// </summary>
    public static class StringResourceValues
    {
        #region 变量和属性

        /// <summary>
        /// 是
        /// </summary>
        public static string Yes
        {
            get { return EnvironmentUtils.StringResource[StringResourceKeys.ButtonTitle_Yes]; }
        }

        /// <summary>
        /// 否
        /// </summary>
        public static string No
        {
            get { return EnvironmentUtils.StringResource[StringResourceKeys.ButtonTitle_No]; }
        }

        /// <summary>
        /// 确定
        /// </summary>
        public static string Ok
        {
            get { return EnvironmentUtils.StringResource[StringResourceKeys.ButtonTitle_OK]; }
        }

        /// <summary>
        /// 取消
        /// </summary>
        public static string Cancel
        {
            get { return EnvironmentUtils.StringResource[StringResourceKeys.ButtonTitle_Cancel]; }
        }

        #endregion

        #region 获得字符串资源

        /// <summary>
        /// 获得字符串资源
        /// </summary>
        /// <param name="stringResourceKey">字符串资源的键</param>
        /// <returns>字符串资源</returns>
        public static string GetStringResource( string stringResourceKey )
        {
            return EnvironmentUtils.StringResource[stringResourceKey];
        }

        /// <summary>
        /// 获得字符串资源
        /// </summary>
        /// <param name="stringResourceKey">字符串资源的键</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>字符串资源</returns>
        public static string GetStringResource( string stringResourceKey, string defaultValue )
        {
            return EnvironmentUtils.StringResource[stringResourceKey] ?? defaultValue;
        }

        #endregion
    }
}