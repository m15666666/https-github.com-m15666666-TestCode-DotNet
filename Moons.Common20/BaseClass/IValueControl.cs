namespace Moons.Common20.BaseClass
{
    /// <summary>
    /// ��ʾֵ�Ŀؼ�
    /// </summary>
    public interface IValueControl : ISetControlValue
    {
        /// <summary>
        /// �󶨵�������������ͨ����������ֵ
        /// </summary>
        string BindPropertyName { get; set; }
    }
}