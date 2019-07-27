using System;
using System.Collections.Generic;
using System.Text;

namespace Moons.Common20.PromptMessage
{
    /// <summary>
    /// 输入验证相关提示信息接口
    /// </summary>
    public interface IInputValidation
    {
        /// <summary>
        /// 获取"{0}输入不正确！"提示信息
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        string GetInputWrongMsg(string item);
        /// <summary>
        /// 获取"{0}或{1}输入不正确！"提示信息
        /// </summary>
        /// <param name="item1">{0}参数值</param>
        /// <param name="item2">{1}参数值</param>
        /// <returns></returns>
        string GetInputWrongOrMsg(string item1,string item2);
        /// <summary>
        /// 获取"{0}超过系统允许长度{1}！"提示信息
        /// </summary>
        /// <param name="item">{0}参数值</param>
        /// <param name="maxLength">{1}参数值</param>
        /// <returns></returns>
        string GetLengthOverMsg(string item,int maxLength);
        /// <summary>
        /// 获取"{0}低于系统允许长度{1}！"提示信息
        /// </summary>
        /// <param name="item"></param>
        /// <param name="minLength">{0}参数值</param>
        /// <returns></returns>
        string GetLengthBelowMsg(string item,int minLength);
        /// <summary>
        /// 获取"{0}数据类型必须为英文或数字！"提示信息
        /// </summary>
        /// <param name="item">{0}参数值</param>
        /// <returns></returns>
        string GetDataTypeENMsg(string item);
        /// <summary>
        /// 获取"{0}数据类型必须为英文或数字或中文！"提示信息
        /// </summary>
        /// <param name="item">{0}参数值</param>
        /// <returns></returns>
        string GetDataTypeENCMsg(string item);
        /// <summary>
        /// 获取"{0}数据类型必须为邮件地址！"提示信息
        /// </summary>
        /// <param name="item">{0}参数值</param>
        /// <returns></returns>
        string GetDataTypeEmailMsg(string item);
        /// <summary>
        /// 获取"{0}数据类型必须为日期型！"提示信息
        /// </summary>
        /// <param name="item">{0}参数值</param>
        /// <returns></returns>
        string GetDataTypeDateMsg(string item);
        /// <summary>
        /// 获取"{0}数据类型必须为整型！"提示信息
        /// </summary>
        /// <param name="item">{0}参数值</param>
        /// <returns></returns>
        string GetDataTypeNMsg(string item);
        /// <summary>
        /// 获取"{0}必须大于{1}！"提示信息
        /// </summary>
        /// <param name="item">{0}参数值</param>
        /// <param name="minValue">{1}参数值</param>
        /// <returns></returns>
        string GetDataOverMsg(string item,object minValue);
        /// <summary>
        /// 获取"{0}必须小于{1}！"提示信息
        /// </summary>
        /// <param name="item">{0}参数值</param>
        /// <param name="maxValue">{1}参数值</param>
        /// <returns></returns>
        string GetDataBelowMsg(string item,object maxValue);
        /// <summary>
        /// 获取"{0}必须介于{1}和{2}之间！"提示信息
        /// </summary>
        /// <param name="item">{0}参数值</param>
        /// <param name="minValue">{1}参数值</param>
        /// <param name="maxValue">{2}参数值</param>
        /// <returns></returns>
        string GetDataBetweenMsg(string item,object minValue,object maxValue);
        /// <summary>
        /// 获取"{0}不能为空！"提示信息
        /// </summary>
        /// <param name="item">{0}参数值</param>
        /// <returns></returns>
        string GetNonEmptyMsg(string item);
        /// <summary>
        /// 获取"系统中已经存在相同{0}的{1}信息！"提示信息
        /// </summary>
        /// <param name="item">{0}参数值</param>
        /// <param name="obj">{1}参数值</param>
        /// <returns></returns>
        string GetDataExistMsg(string item,string obj);
        /// <summary>
        /// 获取"系统中存在与{0}关联的{1}信息！"提示信息
        /// </summary>
        /// <param name="item">{0}参数值</param>
        /// <param name="obj">{1}参数值</param>
        /// <returns></returns>
        string GetRefDataExistMsg(string item,string obj);

        /// <summary>
        /// 获取"日期格式错误"提示信息
        /// </summary>
        /// <returns></returns>
        string GetDateFormatErrorMsg();
    }
}
