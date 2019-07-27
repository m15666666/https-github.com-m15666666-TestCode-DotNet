namespace Moons.Common20.PromptMessage
{
    /// <summary>
    /// 文件操作相关提示信息类
    /// </summary>
    public class FileOperate : PromptMsgBase, IFileOperate
    {
        #region IFilePromptMsg 成员

        public string GetNotExistMsg()
        {
            return ReadPromptMsgByKey( FileOperateMsgKey.NotExistMsg );
        }

        public string GetFormatErrorMsg()
        {
            return ReadPromptMsgByKey( FileOperateMsgKey.FormatErrorMsg );
        }

        public string GetDirFormatErrorMsg()
        {
            return ReadPromptMsgByKey( FileOperateMsgKey.DirFormatErrorMsg );
        }

        public string GetOpenFileFailMsg()
        {
            return ReadPromptMsgByKey( FileOperateMsgKey.OpenFileFailMsg );
        }

        public string GetCloseFileFailMsg()
        {
            return ReadPromptMsgByKey( FileOperateMsgKey.CloseFileFailMsg );
        }

        public string GetBackupFileFailMsg()
        {
            return ReadPromptMsgByKey( FileOperateMsgKey.BackupFileFailMsg );
        }

        #endregion
    }
}