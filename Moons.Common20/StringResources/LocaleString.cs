using Moons.Common20.BaseClass;

namespace Moons.Common20.StringResources
{
    /// <summary>
    /// 本地化字符串
    /// </summary>
    public class LocaleString : ITitleControl
    {
        #region ctor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stringResourceKey">字符串资源的键</param>
        public LocaleString( string stringResourceKey ) : this( stringResourceKey, null )
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stringResourceKey">字符串资源的键</param>
        /// <param name="defaultValue">默认值</param>
        public LocaleString( string stringResourceKey, string defaultValue )
        {
            StringResourceKey = stringResourceKey;
            DefaultValue = defaultValue;
        }

        #endregion

        #region 变量和属性

        /// <summary>
        /// 文本
        /// </summary>
        private string _text;

        /// <summary>
        /// 文本
        /// </summary>
        public string Text
        {
            get { return _text ?? StringResourceValues.GetStringResource( StringResourceKey, DefaultValue ); }
            set { _text = value; }
        }

        /// <summary>
        /// 默认值
        /// </summary>
        private string DefaultValue { get; set; }

        #endregion

        #region ITitleControl 成员

        /// <summary>
        /// 字符串资源的键，用于替换显示的字符串
        /// </summary>
        public string StringResourceKey { get; set; }

        #endregion

        #region ISetControlTitle 成员

        string ISetControlTitle.Title
        {
            set { Text = value; }
        }

        #endregion

        /// <summary>
        /// 操作符重载
        /// </summary>
        /// <param name="localeString">LocaleString</param>
        /// <returns>字符串</returns>
        public static implicit operator string( LocaleString localeString )
        {
            return localeString.Text;
        }
    }
}