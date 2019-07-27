using System.Collections.Generic;

namespace Moons.Common20.Compare
{
    /// <summary>
    /// 辅助实现IEqualityComparer接口的类
    /// 参考：http://www.cnblogs.com/ldp615/archive/2011/08/02/quickly-create-instance-of-iequalitycomparer-and-icomparer.html
    /// </summary>
    /// <typeparam name="T">比较的对象类型</typeparam>
    public static class Equality<T>
    {
        /// <summary>
        /// 创建IEqualityComparer接口对象
        /// </summary>
        /// <typeparam name="V">键类型</typeparam>
        /// <param name="keySelector">键选择函数</param>
        /// <returns>IEqualityComparer接口对象</returns>
        public static IEqualityComparer<T> CreateComparer<V>( Func20<T, V> keySelector )
        {
            return new CommonEqualityComparer<V>( keySelector );
        }

        /// <summary>
        /// 创建IEqualityComparer接口对象
        /// </summary>
        /// <typeparam name="V">键类型</typeparam>
        /// <param name="keySelector">键选择函数</param>
        /// <param name="comparer">比较键的函数</param>
        /// <returns>IEqualityComparer接口对象</returns>
        public static IEqualityComparer<T> CreateComparer<V>( Func20<T, V> keySelector, IEqualityComparer<V> comparer )
        {
            return new CommonEqualityComparer<V>( keySelector, comparer );
        }

        #region Nested type: CommonEqualityComparer

        /// <summary>
        /// 内部使用的辅助实现IEqualityComparer接口的类
        /// </summary>
        /// <typeparam name="V">键类型</typeparam>
        private class CommonEqualityComparer<V> : IEqualityComparer<T>
        {
            #region 变量和属性

            /// <summary>
            /// 比较键的函数
            /// </summary>
            private readonly IEqualityComparer<V> _comparer;

            /// <summary>
            /// 键选择函数
            /// </summary>
            private readonly Func20<T, V> _keySelector;

            #endregion

            #region ctor

            /// <summary>
            /// ctor
            /// </summary>
            /// <param name="keySelector">键选择函数</param>
            /// <param name="comparer">比较键的函数</param>
            public CommonEqualityComparer( Func20<T, V> keySelector, IEqualityComparer<V> comparer )
            {
                _keySelector = keySelector;
                _comparer = comparer;
            }

            /// <summary>
            /// ctor
            /// </summary>
            /// <param name="keySelector">键选择函数</param>
            public CommonEqualityComparer( Func20<T, V> keySelector )
                : this( keySelector, EqualityComparer<V>.Default )
            {
            }

            #endregion

            #region IEqualityComparer<T> Members

            public bool Equals( T x, T y )
            {
                return _comparer.Equals( _keySelector( x ), _keySelector( y ) );
            }

            public int GetHashCode( T obj )
            {
                return _comparer.GetHashCode( _keySelector( obj ) );
            }

            #endregion
        }

        #endregion
    }
}