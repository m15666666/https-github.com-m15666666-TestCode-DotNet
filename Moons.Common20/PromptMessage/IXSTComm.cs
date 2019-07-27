using System;
using System.Collections.Generic;
using System.Text;

namespace Moons.Common20.PromptMessage
{
    /// <summary>
    /// 数据通信及标准化相关提示信息接口
    /// </summary>
    public interface IXSTComm
    {
        /// <summary>
        /// 获取"标准化开始！"提示信息
        /// </summary>
        string GetEncodeStartMsg();
        /// <summary>
        /// 获取"标准化结束！"提示信息
        /// </summary>
        /// <param name="obj">{0}参数值</param>
        /// <returns></returns>
        string GetEncodeEndMsg(string obj);
        /// <summary>
        /// 获取"正在标准化{0}信息…"提示信息
        /// </summary>
        /// <param name="obj">{0}参数值</param>
        /// <returns></returns>
        string GetEncodingMsg(string obj);
        /// <summary>
        /// 获取"标准化{0}信息出错！"提示信息
        /// </summary>
        /// <param name="obj">{0}参数值</param>
        /// <returns></returns>
        string GetEncodeDataErrorMsg(string obj);
        /// <summary>
        /// 获取"没有定义{0}信息！"提示信息
        /// </summary>
        /// <param name="obj">{0}参数值</param>
        /// <returns></returns>
        string GetEncodeNoDefineMsg(string obj);
        /// <summary>
        /// 获取"{0}没有定义{1}信息！"提示信息
        /// </summary>
        /// <param name="obj">{0}参数值</param>
        /// <param name="subItem"></param>
        /// <returns></returns>
        string GetEncodeNoDefineSubitemMsg(string obj, string subItem);
        /// <summary>
        /// 获取"{0}总数不能超过{1}！"提示信息
        /// </summary>
        /// <param name="obj">{0}参数值</param>
        /// <param name="maxCount">{1}参数值</param>
        /// <returns></returns>
        string GetEncodeOverTotalCountMsg(string obj, int maxCount);
        /// <summary>
        /// 获取"{0}下{1}总数不能超过{2}！"提示信息
        /// </summary>
        /// <param name="obj">{0}参数值</param>
        /// <param name="subItem">{1}参数值</param>
        /// <param name="maxCount">{2}参数值</param>
        /// <returns></returns>
        string GetEncodeOverSubItemCountMsg(string obj, string subItem, int maxCount);
        /// <summary>
        /// 获取"硬件不支持{0}！"提示信息
        /// </summary>
        /// <param name="obj">{0}参数值</param>
        /// <returns></returns>
        string GetEncodeHardWareNotSupportMsg(string obj);
        /// <summary>
        /// 获取"{0}总长度超过了硬件允许的长度{1}！"提示信息
        /// </summary>
        /// <param name="obj">{0}参数值</param>
        /// <param name="maxLength">{1}参数值</param>
        /// <returns></returns>
        string GetEncodeOverTotalSizeMsg(string obj,int maxLength);
        /// <summary>
        /// 获取"{0}数据格式错误！"提示信息
        /// </summary>
        /// <param name="obj">{0}参数值</param>
        /// <returns></returns>
        string GetEncodeDataFormatErrorMsg(string obj);
        /// <summary>
        /// 获取"{0}版本信息错误！"提示信息
        /// </summary>
        /// <param name="obj">{0}参数值</param>
        /// <returns></returns>
        string GetEncodeVersionErrorMsg(string obj);
        /// <summary>
        /// 获取"数据解包开始！"提示信息
        /// </summary>
        string GetDecodeStartMsg();
        /// <summary>
        /// 获取"数据解包结束！"提示信息
        /// </summary>
        string GetDecodeEndMsg();
        /// <summary>
        /// 获取"数据包格式错误！"提示信息
        /// </summary>
        string GetDecodeDataPackErrorMsg();
        /// <summary>
        /// 获取"命令包格式错误！"提示信息
        /// </summary>
        string GetDecodeComandPackErrorMsg();
        /// <summary>
        /// 获取"索罗门校验出错！"提示信息
        /// </summary>
        string GetDecodeSLMDataErrorMsg();
        /// <summary>
        /// 获取"{0}解包出错！"提示信息
        /// </summary>
        /// <param name="obj">{0}参数值</param>
        /// <returns></returns>
        string GetDecodeDataErrorMsg(string obj);
        /// <summary>
        /// 获取"第{0}包第{0}条{1}数据出错！"提示信息
        /// </summary>
        /// <param name="packageNo">{0}参数值</param>
        /// <param name="DataNo">{1}参数值</param>
        /// <param name="obj">{2}参数值</param>
        /// <returns></returns>
        string GetDecodePackDataErrorMsg(int packageNo, int dataNo, string obj);
        /// <summary>
        /// 获取"读取第{0}个包出错！"提示信息
        /// </summary>
        /// <param name="packageNo">{0}参数值</param>
        /// <returns></returns>
        string GetDecodeReadPackErrorMsg(int packageNo);
        /// <summary>
        /// 获取"数据提交开始！"提示信息
        /// </summary>
        string GetReferStartMsg();
        /// <summary>
        /// 获取"数据提交结束！"提示信息
        /// </summary>
        string GetReferEndMsg();
        /// <summary>
        /// 获取"数据提交错误！"提示信息
        /// </summary>
        string GetReferDataErrorMsg();
        /// <summary>
        /// 获取"上传数据类型不正确！"提示信息
        /// </summary>
        string GetReferDataTypeErrorMsg();
        /// <summary>
        /// 获取"没有找到关联的{0}信息！"提示信息
        /// </summary>
        string GetReferNotExistRefDataMsg(string obj);
    }
}
