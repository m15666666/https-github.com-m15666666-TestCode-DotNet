namespace Moons.Common20.BaseClass
{
    /// <summary>
    /// ��ѯ�����ؼ��ӿ�
    /// </summary>
    public interface IQueryConditionControl : IGetControlValue
    {
        /// <summary>
        /// ���ݵ��߼���ϵ
        /// </summary>
        DataLogicRelation LogicRelation { get; set; }

        /// <summary>
        /// �ֶ���
        /// </summary>
        string FieldName { get; set; }
    }
}