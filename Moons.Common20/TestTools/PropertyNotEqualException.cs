using System;
using System.Reflection;

namespace Moons.Common20.TestTools
{
    /// <summary>
    /// 属性值不等异常
    /// </summary>
    public class PropertyNotEqualException : Exception
    {
        public PropertyNotEqualException( string message ) : base( message )
        {
        }

        public PropertyNotEqualException( string message, Exception innerException )
            : base( message, innerException )
        {
        }

        /// <summary>
        /// 属性
        /// </summary>
        public PropertyInfo PropertyInfo { get; set; }

        /// <summary>
        /// 期望的对象
        /// </summary>
        public object Expected { get; set; }

        /// <summary>
        /// 实际的对象
        /// </summary>
        public object Actual { get; set; }
    }
}