namespace Moons.Common20.BaseClass
{
    /// <summary>
    /// ��ȡ�ؼ�ֵ
    /// </summary>
    public interface IGetControlValue
    {
        /// <summary>
        /// ֵ
        /// </summary>
        object Value { get; }

        /// <summary>
        /// �ؼ���������
        /// </summary>
        ControlDataType DataType { get; set; }
    }
}