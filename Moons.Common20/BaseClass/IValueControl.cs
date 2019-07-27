namespace Moons.Common20.BaseClass
{
    /// <summary>
    /// 显示值的控件
    /// </summary>
    public interface IValueControl : ISetControlValue
    {
        /// <summary>
        /// 绑定的属性名，用于通过反射设置值
        /// </summary>
        string BindPropertyName { get; set; }
    }
}