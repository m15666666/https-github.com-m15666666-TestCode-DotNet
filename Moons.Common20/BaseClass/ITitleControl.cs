namespace Moons.Common20.BaseClass
{
    /// <summary>
    /// 显示标题的控件
    /// </summary>
    public interface ITitleControl : ISetControlTitle
    {
        /// <summary>
        /// 字符串资源的键，用于替换显示的字符串
        /// </summary>
        string StringResourceKey { get; set; }
    }

    /// <summary>
    /// 实现ITitleControl接口的基类
    /// </summary>
    public abstract class TitleControlBase : ITitleControl
    {
        #region Implementation of ISetControlTitle

        /// <summary>
        /// 标题
        /// </summary>
        public abstract string Title { set; }

        #endregion

        #region Implementation of ITitleControl

        /// <summary>
        /// 字符串资源的键，用于替换显示的字符串
        /// </summary>
        public virtual string StringResourceKey { get; set; }

        #endregion
    }
}