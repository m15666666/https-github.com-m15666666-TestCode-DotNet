namespace AnalysisAlgorithm
{
    /// <summary>
    /// 用于EMD分解的算法异常类，内在模态函数的数量变小！
    /// </summary>
    public class ImfNumberException : AlgorithmException
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="imfNumber">内在模态函数的数量</param>
        public ImfNumberException( int imfNumber )
            : base( "内在模态函数的数量变小！imfNumber = " + imfNumber )
        {
        }
    }
}