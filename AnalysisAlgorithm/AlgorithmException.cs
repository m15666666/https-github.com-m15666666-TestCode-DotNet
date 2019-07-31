using System;

namespace AnalysisAlgorithm
{
    /// <summary>
    /// 算法异常类
    /// </summary>
    public class AlgorithmException : Exception
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="message"></param>
        public AlgorithmException( string message ) : base( message )
        {
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public AlgorithmException( string message, Exception innerException ) : base( message, innerException )
        {
        }
    }
}