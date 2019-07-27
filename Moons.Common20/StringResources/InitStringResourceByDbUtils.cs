using Moons.Common20.QueryData;

namespace Moons.Common20.StringResources
{
    /// <summary>
    /// 通过数据库来初始化字符串资源的实用工具类
    /// </summary>
    public static class InitStringResourceByDbUtils
    {
        /// <summary>
        /// 初始化字符串资源
        /// </summary>
        /// <param name="queryData">IQueryData</param>
        /// <param name="languageCode">语言代码</param>
        /// <param name="stringResource">IGetString</param>
        public static void InitStringResource( IQueryData queryData, string languageCode, IGetString stringResource )
        {
            NameValueRowCollection languageRows = GetLanguageList( queryData );

            NameValueRow row =
                languageRows.FirstOrDefault(
                    item =>
                    StringUtils.EqualIgnoreCase( item[BS_LanguageUtils.LanguageCode_TX] as string, languageCode ) ) ??
                languageRows[0];

            var columnName = row[BS_LanguageUtils.LanguageItemColumn_TX] as string;
            var languageItemBag = new QueryInfoBag();
            languageItemBag.AddTable( BS_LanguageItemUtils.Table );
            languageItemBag.AddReturnFields( BS_LanguageItemUtils.Key_TX, columnName );

            NameValueRowCollection languageItems = queryData.Query( languageItemBag );
            foreach( NameValueRow languageItem in languageItems )
            {
                var key = (string)languageItem[BS_LanguageItemUtils.Key_TX];
                stringResource[key] = languageItem[columnName] as string;
            }
        }

        /// <summary>
        /// 语言列表
        /// </summary>
        /// <param name="queryData">IQueryData</param>
        /// <returns>语言列表</returns>
        public static NameValueRowCollection GetLanguageList( IQueryData queryData )
        {
            var languageBag = new QueryInfoBag();
            languageBag.AddTable( BS_LanguageUtils.Table );
            return queryData.Query( languageBag );
        }

        #region Nested type: BS_LanguageItemUtils

        /// <summary>
        /// 表 BS_LanguageItem 的实用工具类
        /// </summary>
        public static class BS_LanguageItemUtils
        {
            #region 常量

            /// <summary>
            ///  表名
            /// </summary>
            public const string Table = "BS_LanguageItem";

            public const string Key_TX = "Key_TX";

            #endregion
        }

        #endregion

        #region Nested type: BS_LanguageUtils

        /// <summary>
        /// 表 BS_Language 的实用工具类
        /// </summary>
        public static class BS_LanguageUtils
        {
            #region 常量

            /// <summary>
            ///  表名
            /// </summary>
            public const string Table = "BS_Language";

            public const string LanguageCode_TX = "LanguageCode_TX";
            public const string LanguageItemColumn_TX = "LanguageItemColumn_TX";

            #endregion
        }

        #endregion
    }
}