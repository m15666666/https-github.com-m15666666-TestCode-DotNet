using System;
using System.Collections.Generic;
using System.Text;

namespace Moons.Common20.PromptMessage
{
    /// <summary>
    /// 数据库操作相关提示信息接口
    /// </summary>
    public interface IDBOperate
    {
        /// <summary>
        /// 获取"访问数据库失败！"提示信息
        /// </summary>
        string GetVisitFailMsg();
        /// <summary>
        /// 获取"打开数据库链接失败！"提示信息
        /// </summary>
        string GetOpenConnectionFailMsg();
        /// <summary>
        /// 获取"关闭数据库链接失败！"提示信息
        /// </summary>
        string GetCloseConnectionFailMsg();
        /// <summary>
        /// 获取"保存成功！"提示信息
        /// </summary>
        string GetSaveDataSuccMsg();
        /// <summary>
        /// 获取"保存失败！"提示信息
        /// </summary>
        string GetSaveDataFailMsg();
        /// <summary>
        /// 获取"提交{0}信息到数据库失败！"提示信息
        /// </summary>
        /// <param name="obj">{0}参数值</param>
        /// <returns></returns>
        string GetReferDataToDBFailMsg(string obj);
        /// <summary>
        /// 获取"获取{0}信息失败！"提示信息
        /// </summary>
        /// <param name="obj">{0}参数值</param>
        /// <returns></returns>
        string GetReadDataFromDBFailMsg(string obj);
        /// <summary>
        /// 获取"添加{0}信息失败！"提示信息
        /// </summary>
        /// <param name="obj">{0}参数值</param>
        /// <returns></returns>
        string GetInsertDataFailMsg(string obj);
        /// <summary>
        /// 获取"修改{0}信息失败！"提示信息
        /// </summary>
        /// <param name="obj">{0}参数值</param>
        /// <returns></returns>
        string GetUpdateDataFailMsg(string obj);
        /// <summary>
        /// 获取"删除{0}信息失败！"提示信息
        /// </summary>
        /// <param name="obj">{0}参数值</param>
        /// <returns></returns>
        string GetDeleteDataFailMsg(string obj);
    }
}
