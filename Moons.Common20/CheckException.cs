using System;

namespace Moons.Common20
{
    /// <summary>
    /// 校验异常类
    /// </summary>
    public class CheckException : Exception
    {
        public CheckException( string message ) : base( message )
        {
        }

        public CheckException( string message, Exception innerException ) : base( message, innerException )
        {
        }
    }
}