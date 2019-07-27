using System;
using System.Collections.Generic;
using System.Text;

namespace Moons.Common20.PromptMessage
{
    /// <summary>
    /// 文件操作相关提示信息接口
    /// </summary>
    public interface IFileOperate
    {
        /// <summary>
        /// 获取"文件不存在！"提示信息
        /// </summary>
        string GetNotExistMsg();
        /// <summary>
        /// 获取"文件格式错误！"提示信息
        /// </summary>
        string GetFormatErrorMsg();
        /// <summary>
        /// 获取"文件路径格式错误！"提示信息
        /// </summary>
        string GetDirFormatErrorMsg();
        /// <summary>
        /// 获取"打开文件失败！"提示信息
        /// </summary>
        string GetOpenFileFailMsg();
        /// <summary>
        /// 获取"关闭文件失败！"提示信息
        /// </summary>
        string GetCloseFileFailMsg();
        /// <summary>
        /// 获取"备份文件失败！"提示信息
        /// </summary>
        string GetBackupFileFailMsg();
    }
}
