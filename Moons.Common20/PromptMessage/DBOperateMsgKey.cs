namespace Moons.Common20.PromptMessage
{
    /// <summary>
    /// 数据库操作相关Key
    /// </summary>
    public static class DBOperateMsgKey
    {
        /// <summary>
        /// 访问数据库失败！
        /// </summary>
        public const string VisitFailMsg = "DBOperate.GetVisitFailMsg";

        /// <summary>
        /// 打开数据库链接失败！
        /// </summary>
        public const string OpenConnectionFailMsg = "DBOperate.GetOpenConnectionFailMsg";

        /// <summary>
        /// 关闭数据库链接失败！
        /// </summary>
        public const string CloseConnectionFailMsg = "DBOperate.GetCloseConnectionFailMsg";

        /// <summary>
        /// 保存成功
        /// </summary> 
        public const string SaveDataSuccMsg = "DBOperate.GetSaveDataSuccMsg";

        /// <summary>
        /// 保存失败
        /// </summary> 
        public const string SaveDataFailMsg = "DBOperate.GetSaveDataFailMsg";

        /// <summary>
        /// 提交{0}信息到数据库失败！
        /// </summary> 
        public const string ReferDataToDBFailMsg = "DBOperate.GetReferDataToDBFailMsg";

        /// <summary>
        /// 获取{0}信息失败！
        /// </summary>
        public const string ReadDataFromDBFailMsg = "DBOperate.GetReadDataFromDBFailMsg";

        /// <summary>
        /// 添加{0}信息失败！
        /// </summary>
        public const string InsertDataFailMsg = "DBOperate.GetInsertDataFailMsg";

        /// <summary>
        /// 修改{0}信息失败！
        /// </summary>
        public const string UpdateDataFailMsg = "DBOperate.GetUpdateDataFailMsg";

        /// <summary>
        /// 删除{0}信息失败！
        /// </summary>
        public const string DeleteDataFailMsg = "DBOperate.GetDeleteDataFailMsg";
    }
}