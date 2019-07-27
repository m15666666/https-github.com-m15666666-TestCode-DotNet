namespace Moons.Common20.PromptMessage
{
    /// <summary>
    ///  通用相关提示信息类
    /// </summary>
    public class CommonOperate : PromptMsgBase, ICommonOperate
    {
        #region ICommonMsg 成员

        public string GetInfoMsg()
        {
            return ReadPromptMsgByKey( CommonMsgKey.InfoMsg );
        }

        public string GetWaringMsg()
        {
            return ReadPromptMsgByKey( CommonMsgKey.WaringMsg );
        }

        public string GetErrorMsg()
        {
            return ReadPromptMsgByKey( CommonMsgKey.ErrorMsg );
        }

        public string GetWaringAskMsg()
        {
            return ReadPromptMsgByKey( CommonMsgKey.WaringAskMsg );
        }

        public string GetSuccMsg()
        {
            return ReadPromptMsgByKey( CommonMsgKey.SuccMsg );
        }

        public string GetFailMsg()
        {
            return ReadPromptMsgByKey( CommonMsgKey.FailMsg );
        }

        #endregion
    }
}