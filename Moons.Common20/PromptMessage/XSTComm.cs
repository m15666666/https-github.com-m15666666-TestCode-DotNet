namespace Moons.Common20.PromptMessage
{
    /// <summary>
    /// 数据通信及标准化相关提示信息类
    /// </summary>
    public class XSTComm : PromptMsgBase, IXSTComm
    {
        #region IXSTCommPromptMsg 成员

        public string GetEncodeStartMsg()
        {
            return ReadPromptMsgByKey( XSTCommMsgKey.EncodeStartMsg );
        }

        public string GetEncodeEndMsg( string obj )
        {
            return ReadPromptMsgByKey( XSTCommMsgKey.EncodeEndMsg, obj );
        }

        public string GetEncodingMsg( string obj )
        {
            return ReadPromptMsgByKey( XSTCommMsgKey.EncodingMsg, obj );
        }

        public string GetEncodeDataErrorMsg( string obj )
        {
            return ReadPromptMsgByKey( XSTCommMsgKey.EncodeDataErrorMsg, obj );
        }

        public string GetEncodeNoDefineMsg( string obj )
        {
            return ReadPromptMsgByKey( XSTCommMsgKey.EncodeNoDefineMsg, obj );
        }

        public string GetEncodeNoDefineSubitemMsg( string obj, string subItem )
        {
            return ReadPromptMsgByKey( XSTCommMsgKey.EncodeNoDefineSubitemMsg, obj, subItem );
        }

        public string GetEncodeOverTotalCountMsg( string obj, int maxCount )
        {
            return ReadPromptMsgByKey( XSTCommMsgKey.EncodeOverTotalCountMsg, obj, maxCount.ToString() );
        }

        public string GetEncodeOverSubItemCountMsg( string obj, string subItem, int maxCount )
        {
            return ReadPromptMsgByKey( XSTCommMsgKey.EncodeOverSubItemCountMsg, obj, subItem, maxCount.ToString() );
        }

        public string GetEncodeHardWareNotSupportMsg( string obj )
        {
            return ReadPromptMsgByKey( XSTCommMsgKey.EncodeHardWareNotSupportMsg, obj );
        }

        public string GetEncodeOverTotalSizeMsg( string obj, int maxLength )
        {
            return ReadPromptMsgByKey( XSTCommMsgKey.EncodeOverTotalSizeMsg, obj, maxLength.ToString() );
        }

        public string GetEncodeDataFormatErrorMsg( string obj )
        {
            return ReadPromptMsgByKey( XSTCommMsgKey.EncodeDataFormatErrorMsg, obj );
        }

        public string GetEncodeVersionErrorMsg( string obj )
        {
            return ReadPromptMsgByKey( XSTCommMsgKey.EncodeVersionErrorMsg, obj );
        }

        public string GetDecodeStartMsg()
        {
            return ReadPromptMsgByKey( XSTCommMsgKey.DecodeStartMsg );
        }

        public string GetDecodeEndMsg()
        {
            return ReadPromptMsgByKey( XSTCommMsgKey.DecodeEndMsg );
        }

        public string GetDecodeDataPackErrorMsg()
        {
            return ReadPromptMsgByKey( XSTCommMsgKey.DecodeDataPackErrorMsg );
        }

        public string GetDecodeComandPackErrorMsg()
        {
            return ReadPromptMsgByKey( XSTCommMsgKey.DecodeComandPackErrorMsg );
        }

        public string GetDecodeSLMDataErrorMsg()
        {
            return ReadPromptMsgByKey( XSTCommMsgKey.DecodeSLMDataErrorMsg );
        }

        public string GetDecodeDataErrorMsg( string obj )
        {
            return ReadPromptMsgByKey( XSTCommMsgKey.DecodeDataErrorMsg, obj );
        }

        public string GetDecodePackDataErrorMsg( int packageNo, int dataNo, string obj )
        {
            return ReadPromptMsgByKey( XSTCommMsgKey.DecodePackDataErrorMsg, packageNo.ToString(), dataNo.ToString(),
                                       obj );
        }

        public string GetDecodeReadPackErrorMsg( int packageNo )
        {
            return ReadPromptMsgByKey( XSTCommMsgKey.DecodeReadPackErrorMsg, packageNo.ToString() );
        }

        public string GetReferStartMsg()
        {
            return ReadPromptMsgByKey( XSTCommMsgKey.ReferStartMsg );
        }

        public string GetReferEndMsg()
        {
            return ReadPromptMsgByKey( XSTCommMsgKey.ReferEndMsg );
        }

        public string GetReferDataErrorMsg()
        {
            return ReadPromptMsgByKey( XSTCommMsgKey.ReferDataErrorMsg );
        }

        public string GetReferDataTypeErrorMsg()
        {
            return ReadPromptMsgByKey( XSTCommMsgKey.ReferDataTypeErrorMsg );
        }

        public string GetReferNotExistRefDataMsg( string obj )
        {
            return ReadPromptMsgByKey( XSTCommMsgKey.ReferNotExistRefDataMsg, obj );
        }

        #endregion
    }
}