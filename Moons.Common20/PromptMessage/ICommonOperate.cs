namespace Moons.Common20.PromptMessage
{
    /// <summary>
    /// ͨ�������ʾ��Ϣ�ӿ�
    /// </summary>
    public interface ICommonOperate
    {
        /// <summary>
        /// ��ȡ"��ʾ��Ϣ"��ʾ��Ϣ
        /// </summary>  
        string GetInfoMsg();

        /// <summary>
        /// ��ȡ"������Ϣ"��ʾ��Ϣ
        /// </summary>
        string GetWaringMsg();

        /// <summary>
        /// ��ȡ"������Ϣ"��ʾ��Ϣ
        /// </summary> 
        string GetErrorMsg();

        /// <summary>
        /// ��ȡ"ȷ��Ҫ����������"��ʾ��Ϣ
        /// </summary>
        string GetWaringAskMsg();
        /// <summary>
        /// ��ȡ"�����ɹ�"��ʾ��Ϣ
        /// </summary>
        /// <returns></returns>
        string GetSuccMsg();
        /// <summary>
        /// ��ȡ"����ʧ��"��ʾ��Ϣ
        /// </summary>
        /// <returns></returns>
        string GetFailMsg();
    }
}