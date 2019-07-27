namespace Moons.Common20.PromptMessage
{
    /// <summary>
    /// 输入验证相关提示信息类
    /// </summary>
    public class InputValidation : PromptMsgBase, IInputValidation
    {
        #region IInputValidationPromptMsg 成员

        public string GetInputWrongMsg( string item )
        {
            return ReadPromptMsgByKey( InputValidationMsgKey.InputWrong, item );
        }

        public string GetInputWrongOrMsg( string item1, string item2 )
        {
            return ReadPromptMsgByKey( InputValidationMsgKey.InputWrongOr, item1, item2 );
        }

        public string GetLengthOverMsg( string item, int maxLength )
        {
            return ReadPromptMsgByKey( InputValidationMsgKey.LengthGreatError, item, maxLength.ToString() );
        }

        public string GetLengthBelowMsg( string item, int minLength )
        {
            return ReadPromptMsgByKey( InputValidationMsgKey.LengthLessError, item, minLength.ToString() );
        }

        public string GetDataTypeENMsg( string item )
        {
            return ReadPromptMsgByKey( InputValidationMsgKey.DataTypeENMsg, item );
        }

        public string GetDataTypeENCMsg( string item )
        {
            return ReadPromptMsgByKey( InputValidationMsgKey.DataTypeENCMsg, item );
        }

        public string GetDataTypeEmailMsg( string item )
        {
            return ReadPromptMsgByKey( InputValidationMsgKey.DataTypeEmailMsg, item );
        }

        public string GetDataTypeDateMsg( string item )
        {
            return ReadPromptMsgByKey( InputValidationMsgKey.DataTypeDateMsg, item );
        }

        public string GetDataTypeNMsg( string item )
        {
            return ReadPromptMsgByKey( InputValidationMsgKey.MustIntegerError, item );
        }

        public string GetDataOverMsg( string item, object minValue )
        {
            return ReadPromptMsgByKey( InputValidationMsgKey.MustGreatError, item, minValue.ToString() );
        }

        public string GetDataBelowMsg( string item, object maxValue )
        {
            return ReadPromptMsgByKey( InputValidationMsgKey.MustLessError, item, maxValue.ToString() );
        }

        public string GetDataBetweenMsg( string item, object minValue, object maxValue )
        {
            return ReadPromptMsgByKey( InputValidationMsgKey.DataBetweenMsg, item, minValue,
                                       maxValue.ToString() );
        }

        public string GetNonEmptyMsg( string item )
        {
            return ReadPromptMsgByKey( InputValidationMsgKey.EmptyError, item );
        }

        public string GetDataExistMsg( string item, string obj )
        {
            return ReadPromptMsgByKey( InputValidationMsgKey.DataExistMsg, item, obj );
        }

        public string GetRefDataExistMsg( string item, string obj )
        {
            return ReadPromptMsgByKey( InputValidationMsgKey.RefDataExistMsg, item, obj );
        }

        public string GetDateFormatErrorMsg()
        {
            return ReadPromptMsgByKey( InputValidationMsgKey.DateFormatErrorMsg );
        }

        #endregion
    }
}