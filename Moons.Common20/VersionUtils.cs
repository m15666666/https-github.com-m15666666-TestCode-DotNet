using System;

namespace Moons.Common20
{
    /// <summary>
    /// 关于版本的实用工具类
    /// </summary>
    public static class VersionUtils
    {
        /// <summary>
        /// 获得版本描述
        /// </summary>
        /// <param name="version">版本号</param>
        /// <returns>版本描述</returns>
        public static string GetVersionDescription( string version )
        {
            return string.Format( "版本：{0}", version );
        }

        #region 比较版本

        /// <summary>
        /// 比较版本号，version1 高于 version2 返回1，相等返回0，低于返回-1
        /// </summary>
        /// <param name="version1">版本号1，例如：1.0.1.20110801</param>
        /// <param name="version2">版本号2，例如：1.0.1.20110801</param>
        /// <returns>version1 高于 version2 返回1，相等返回0，低于返回-1</returns>
        public static int CompareVersion( string version1, string version2 )
        {
            string[] numbers1 = StringUtils.Split( version1, StringUtils.PointChar );
            string[] numbers2 = StringUtils.Split( version2, StringUtils.PointChar );

            int count = Math.Min( numbers1.Length, numbers2.Length );
            for( int index = 0; index < count; index++ )
            {
                long number1 = long.Parse( numbers1[index] );
                long number2 = long.Parse( numbers2[index] );

                if( number1 == number2 )
                {
                    continue;
                }

                return number2 < number1 ? 1 : -1;
            }
            return 0;
        }

        /// <summary>
        /// version1是否比version2版本更高
        /// </summary>
        /// <param name="version1">版本号1，例如：1.0.1.20110801</param>
        /// <param name="version2">版本号2，例如：1.0.1.20110801</param>
        /// <returns>true：version1 高于 version2 ，否则返回false</returns>
        public static bool IsVersionGreater( string version1, string version2 )
        {
            return CompareVersion( version1, version2 ) == 1;
        }

        /// <summary>
        /// version1是否与version2版本相等
        /// </summary>
        /// <param name="version1">版本号1，例如：1.0.1.20110801</param>
        /// <param name="version2">版本号2，例如：1.0.1.20110801</param>
        /// <returns>true：version1与version2版本相等 ，否则返回false</returns>
        public static bool IsVersionEqual( string version1, string version2 )
        {
            return CompareVersion( version1, version2 ) == 0;
        }

        #endregion
    }
}