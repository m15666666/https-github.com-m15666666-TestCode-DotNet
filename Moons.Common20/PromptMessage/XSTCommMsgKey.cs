namespace Moons.Common20.PromptMessage
{
    /// <summary>
    /// 数据通信及标准化相关Key
    /// </summary>
    public static class XSTCommMsgKey
    {
        /// <summary>
        /// 标准化开始！
        /// </summary>
        public const string EncodeStartMsg = "XSTComm.GetEncodeStartMsg";

        /// <summary>
        /// 标准化结束！
        /// </summary>
        public const string EncodeEndMsg = "XSTComm.GetEncodeEndMsg";

        /// <summary>
        /// 正在标准化{0}信息…
        /// </summary>
        public const string EncodingMsg = "XSTComm.GetEncodingMsg";

        /// <summary>
        /// 标准化{0}信息出错！
        /// </summary>
        public const string EncodeDataErrorMsg = "XSTComm.GetEncodeDataErrorMsg";

        /// <summary>
        /// 没有定义{0}信息！
        /// </summary>
        public const string EncodeNoDefineMsg = "XSTComm.GetEncodeNoDefineMsg";

        /// <summary>
        /// {0}没有定义{1}信息！
        /// </summary>
        public const string EncodeNoDefineSubitemMsg = "XSTComm.GetEncodeNoDefineSubitemMsg";

        /// <summary>
        /// {0}总数不能超过{1}！
        /// </summary>
        public const string EncodeOverTotalCountMsg = "XSTComm.GetEncodeOverTotalCountMsg";

        /// <summary>
        /// {0}下{1}总数不能超过{2}！
        /// </summary>
        public const string EncodeOverSubItemCountMsg = "XSTComm.GetEncodeOverSubItemCountMsg";

        /// <summary>
        /// 硬件不支持{0}！
        /// </summary>
        public const string EncodeHardWareNotSupportMsg = "XSTComm.GetEncodeHardWareNotSupportMsg";

        /// <summary>
        /// {0}总长度超过了硬件允许的长度{1}！
        /// </summary>
        public const string EncodeOverTotalSizeMsg = "XSTComm.GetEncodeOverTotalSizeMsg";

        /// <summary>
        /// {0}数据格式错误！
        /// </summary>
        public const string EncodeDataFormatErrorMsg = "XSTComm.GetEncodeDataFormatErrorMsg";

        /// <summary>
        /// {0}版本信息错误！
        /// </summary>
        public const string EncodeVersionErrorMsg = "XSTComm.GetEncodeVersionErrorMsg";

        /// <summary>
        /// 数据解包开始！
        /// </summary>
        public const string DecodeStartMsg = "XSTComm.GetDecodeStartMsg";

        /// <summary>
        /// 数据解包结束！
        /// </summary>
        public const string DecodeEndMsg = "XSTComm.GetDecodeEndMsg";

        /// <summary>
        /// 数据包格式错误！
        /// </summary>
        public const string DecodeDataPackErrorMsg = "XSTComm.GetDecodeDataPackErrorMsg";

        /// <summary>
        /// 命令包格式错误！
        /// </summary>
        public const string DecodeComandPackErrorMsg = "XSTComm.GetDecodeComandPackErrorMsg";

        /// <summary>
        /// 索罗门校验出错！
        /// </summary>
        public const string DecodeSLMDataErrorMsg = "XSTComm.GetDecodeSLMDataErrorMsg";

        /// <summary>
        /// {0}解包出错！
        /// </summary>
        public const string DecodeDataErrorMsg = "XSTComm.GetDecodeDataErrorMsg";

        /// <summary>
        /// 第{0}包第{0}条{1}数据出错！
        /// </summary>
        public const string DecodePackDataErrorMsg = "XSTComm.GetDecodePackDataErrorMsg";

        /// <summary>
        /// 读取第{0}个包出错！
        /// </summary>
        public const string DecodeReadPackErrorMsg = "XSTComm.GetDecodeReadPackErrorMsg";

        /// <summary>
        /// 数据提交开始！
        /// </summary>
        public const string ReferStartMsg = "XSTComm.GetReferStartMsg";

        /// <summary>
        /// 数据提交结束！
        /// </summary>
        public const string ReferEndMsg = "XSTComm.GetReferEndMsg";

        /// <summary>
        /// 数据提交错误！
        /// </summary>
        public const string ReferDataErrorMsg = "XSTComm.GetReferDataErrorMsg";

        /// <summary>
        /// 上传数据类型不正确！
        /// </summary>
        public const string ReferDataTypeErrorMsg = "XSTComm.GetReferDataTypeErrorMsg";

        /// <summary>
        /// 没有找到关联的{0}信息！
        /// </summary>
        public const string ReferNotExistRefDataMsg = "XSTComm.GetReferNotExistRefDataMsg";
    }
}