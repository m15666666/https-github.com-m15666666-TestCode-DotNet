namespace Moons.Common20.BaseClass
{
    /// <summary>
    /// ��ʾ����Ŀؼ�
    /// </summary>
    public interface ITitleControl : ISetControlTitle
    {
        /// <summary>
        /// �ַ�����Դ�ļ��������滻��ʾ���ַ���
        /// </summary>
        string StringResourceKey { get; set; }
    }

    /// <summary>
    /// ʵ��ITitleControl�ӿڵĻ���
    /// </summary>
    public abstract class TitleControlBase : ITitleControl
    {
        #region Implementation of ISetControlTitle

        /// <summary>
        /// ����
        /// </summary>
        public abstract string Title { set; }

        #endregion

        #region Implementation of ITitleControl

        /// <summary>
        /// �ַ�����Դ�ļ��������滻��ʾ���ַ���
        /// </summary>
        public virtual string StringResourceKey { get; set; }

        #endregion
    }
}