namespace Moons.Common20.PromptMessage
{
    /// <summary>
    /// 通用相关提示信息接口
    /// </summary>
    public interface ICommonOperate
    {
        /// <summary>
        /// 获取"提示信息"提示信息
        /// </summary>  
        string GetInfoMsg();

        /// <summary>
        /// 获取"警告信息"提示信息
        /// </summary>
        string GetWaringMsg();

        /// <summary>
        /// 获取"错误信息"提示信息
        /// </summary> 
        string GetErrorMsg();

        /// <summary>
        /// 获取"确定要继续操作吗？"提示信息
        /// </summary>
        string GetWaringAskMsg();
        /// <summary>
        /// 获取"操作成功"提示信息
        /// </summary>
        /// <returns></returns>
        string GetSuccMsg();
        /// <summary>
        /// 获取"操作失败"提示信息
        /// </summary>
        /// <returns></returns>
        string GetFailMsg();
    }
}