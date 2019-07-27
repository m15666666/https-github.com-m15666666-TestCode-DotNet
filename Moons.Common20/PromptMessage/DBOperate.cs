namespace Moons.Common20.PromptMessage
{
    /// <summary>
    /// 数据库操作相关提示信息类
    /// </summary>
    public class DBOperate : PromptMsgBase, IDBOperate
    {
        #region IDBOperatePromptMsg 成员

        public string GetVisitFailMsg()
        {
            return ReadPromptMsgByKey( DBOperateMsgKey.VisitFailMsg );
        }

        public string GetOpenConnectionFailMsg()
        {
            return ReadPromptMsgByKey( DBOperateMsgKey.OpenConnectionFailMsg );
        }

        public string GetCloseConnectionFailMsg()
        {
            return ReadPromptMsgByKey( DBOperateMsgKey.CloseConnectionFailMsg );
        }

        /// <summary>
        /// 获取"保存成功！"提示信息
        /// </summary>
        public string GetSaveDataSuccMsg()
        {
            return ReadPromptMsgByKey( DBOperateMsgKey.SaveDataSuccMsg );
        }

        /// <summary>
        /// 获取"保存失败！"提示信息
        /// </summary>
        public string GetSaveDataFailMsg()
        {
            return ReadPromptMsgByKey( DBOperateMsgKey.SaveDataFailMsg );
        }

        public string GetReferDataToDBFailMsg( string obj )
        {
            return ReadPromptMsgByKey( DBOperateMsgKey.ReadDataFromDBFailMsg, obj );
        }

        public string GetReadDataFromDBFailMsg( string obj )
        {
            return ReadPromptMsgByKey( DBOperateMsgKey.ReadDataFromDBFailMsg, obj );
        }

        public string GetInsertDataFailMsg( string obj )
        {
            return ReadPromptMsgByKey( DBOperateMsgKey.InsertDataFailMsg, obj );
        }

        public string GetUpdateDataFailMsg( string obj )
        {
            return ReadPromptMsgByKey( DBOperateMsgKey.UpdateDataFailMsg, obj );
        }

        public string GetDeleteDataFailMsg( string obj )
        {
            return ReadPromptMsgByKey( DBOperateMsgKey.DeleteDataFailMsg, obj );
        }

        #endregion
    }
}