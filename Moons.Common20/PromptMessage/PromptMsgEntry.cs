using System.Collections.Generic;

namespace Moons.Common20.PromptMessage
{
    /// <summary>
    /// 提示信息入口
    /// </summary>
    public static class PromptMsgEntry
    {
        #region 变量和属性

        /// <summary>
        /// 定义公共相关提示信息入口
        /// </summary>
        public static ICommonOperate CommonOperate { get; set; }

        /// <summary>
        /// 定义数据库操作相关提示信息接口
        /// </summary>
        public static IDBOperate DBOperate { get; set; }

        /// <summary>
        /// 定义文件操作相关提示信息接口
        /// </summary>
        public static IFileOperate FileOperate { get; set; }

        /// <summary>
        /// 定义输入验证相关提示信息接口
        /// </summary>
        public static IInputValidation InputValidation { get; set; }

        /// <summary>
        /// 定义数据通信及标准化提示信息接口
        /// </summary>
        public static IXSTComm XSTComm { get; set; }

        #endregion

        #region 初始化提示信息

        /// <summary>
        /// 初始化提示信息
        /// </summary>
        /// <param name="promptMsgs">提示信息健值对集合数据</param>
        public static void InitPromptMsgs( IDictionary<string, string> promptMsgs )
        {
            var commonOperate = new CommonOperate();
            commonOperate.Init( promptMsgs );

            var dbOperate = new DBOperate();
            dbOperate.Init( promptMsgs );

            var fileOperate = new FileOperate();
            fileOperate.Init( promptMsgs );

            var inputValidation = new InputValidation();
            inputValidation.Init( promptMsgs );

            var xstComm = new XSTComm();
            xstComm.Init( promptMsgs );

            CommonOperate = commonOperate;
            DBOperate = dbOperate;
            FileOperate = fileOperate;
            InputValidation = inputValidation;
            XSTComm = xstComm;
        }

        #endregion
    }
}