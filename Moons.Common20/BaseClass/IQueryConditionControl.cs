namespace Moons.Common20.BaseClass
{
    /// <summary>
    /// 查询条件控件接口
    /// </summary>
    public interface IQueryConditionControl : IGetControlValue
    {
        /// <summary>
        /// 数据的逻辑关系
        /// </summary>
        DataLogicRelation LogicRelation { get; set; }

        /// <summary>
        /// 字段名
        /// </summary>
        string FieldName { get; set; }
    }
}