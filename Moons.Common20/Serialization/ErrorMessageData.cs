using Moons.Common20.ValueWrapper;

namespace Moons.Common20.Serialization
{
    /// <summary>
    ///     错误信息数据
    /// </summary>
    public class ErrorMessageData : CustomDataBase, IValueWrappersContainer
    {
        private ValueWrapper<int> _errorCode = new ValueWrapper<int>();

        /// <summary>
        ///     错误码
        /// </summary>
        public int ErrorCode
        {
            get { return _errorCode; }
            set { _errorCode = value; }
        }

        /// <summary>
        ///     错误信息
        /// </summary>
        public string ErrorMessage { get; set; }

        #region IValueWrappersContainer 成员

        IValueWrapper[] IValueWrappersContainer.ValueWrappers
        {
            get { return new IValueWrapper[] {_errorCode}; }
        }

        #endregion
    }
}