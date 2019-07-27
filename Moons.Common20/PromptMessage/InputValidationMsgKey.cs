namespace Moons.Common20.PromptMessage
{
    /// <summary>
    /// 输入验证相关Key
    /// </summary>
    public static class InputValidationMsgKey
    {
        /// <summary>
        /// {0}输入不正确！
        /// </summary>
        public const string InputWrong = "InputValidation.GetInputWrongMsg";

        /// <summary>
        /// {0}或{1}输入不正确！
        /// </summary>
        public const string InputWrongOr = "InputValidation.GetInputWrongOrMsg";

        /// <summary>
        /// {0}超过系统允许长度{1}！
        /// </summary>
        public const string LengthGreatError = "InputValidation.GetLengthOverMsg";

        /// <summary>
        /// {0}低于系统允许长度{1}！
        /// </summary>
        public const string LengthLessError = "InputValidation.GetLengthBelowMsg";

        /// <summary>
        /// {0}数据类型必须为英文或数字！
        /// </summary>
        public const string DataTypeENMsg = "InputValidation.GetDataTypeENMsg";

        /// <summary>
        /// {0}数据类型必须为英文或数字或中文！
        /// </summary>
        public const string DataTypeENCMsg = "InputValidation.GetDataTypeENCMsg";

        /// <summary>
        /// {0}数据类型必须为邮件地址！
        /// </summary>
        public const string DataTypeEmailMsg = "InputValidation.GetDataTypeEmailMsg";

        /// <summary>
        /// {0}数据类型必须为日期型！
        /// </summary>
        public const string DataTypeDateMsg = "InputValidation.GetDataTypeDateMsg";

        /// <summary>
        /// {0}数据类型必须为整型！
        /// </summary>
        public const string MustIntegerError = "InputValidation.GetDataTypeNMsg";

        /// <summary>
        /// {0}数据类型必须为浮点型！
        /// </summary>
        public const string MustFloatError = "InputValidation.MustFloatError";

        /// <summary>
        /// {0}必须大于{1}！
        /// </summary>
        public const string MustGreatError = "InputValidation.GetDataOverMsg";

        /// <summary>
        /// {0}必须小于{1}！
        /// </summary>
        public const string MustLessError = "InputValidation.GetDataBelowMsg";

        /// <summary>
        /// {0}必须介于{1}和{2}之间！
        /// </summary>
        public const string DataBetweenMsg = "InputValidation.GetDataBetweenMsg";

        /// <summary>
        /// {0}不能为空！
        /// </summary>
        public const string EmptyError = "InputValidation.GetNonEmptyMsg";

        /// <summary>
        /// 系统中已经存在相同{0}的{1}信息！
        /// </summary>
        public const string DataExistMsg = "InputValidation.GetDataExistMsg";

        /// <summary>
        /// 系统中存在与{0}关联的{1}信息！
        /// </summary>
        public const string RefDataExistMsg = "InputValidation.GetRefDataExistMsg";

        /// <summary>
        /// 日期格式不正确！
        /// </summary>
        public const string DateFormatErrorMsg = "InputValidation.GetDateFormatErrorMsg";
    }
}