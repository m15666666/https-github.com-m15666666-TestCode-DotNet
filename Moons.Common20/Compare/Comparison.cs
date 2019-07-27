using System.Collections.Generic;

namespace Moons.Common20.Compare
{
    /// <summary>
    /// 辅助实现IComparer接口的类
    /// 参考：http://www.cnblogs.com/ldp615/archive/2011/08/02/quickly-create-instance-of-iequalitycomparer-and-icomparer.html
    /// </summary>
    /// <typeparam name="T">比较的对象类型</typeparam>
    public static class Comparison<T>
    {
        /// <summary>
        /// 创建IComparer接口对象
        /// </summary>
        /// <typeparam name="V">键类型</typeparam>
        /// <param name="keySelector">键选择函数</param>
        /// <returns>IComparer接口对象</returns>
        public static IComparer<T> CreateComparer<V>( Func20<T, V> keySelector )
        {
            return new CommonComparer<V>( keySelector );
        }

        /// <summary>
        /// 创建IComparer接口对象
        /// </summary>
        /// <typeparam name="V">键类型</typeparam>
        /// <param name="keySelector">键选择函数</param>
        /// <param name="comparer">比较键的函数</param>
        /// <returns>IComparer接口对象</returns>
        public static IComparer<T> CreateComparer<V>( Func20<T, V> keySelector, IComparer<V> comparer )
        {
            return new CommonComparer<V>( keySelector, comparer );
        }

        #region Nested type: CommonComparer

        /// <summary>
        /// 内部使用的辅助实现IComparer接口的类
        /// </summary>
        /// <typeparam name="V">键类型</typeparam>
        private class CommonComparer<V> : IComparer<T>
        {
            #region 变量和属性

            /// <summary>
            /// 键选择函数
            /// </summary>
            private readonly IComparer<V> _comparer;

            /// <summary>
            /// 比较键的函数
            /// </summary>
            private readonly Func20<T, V> _keySelector;

            #endregion

            #region ctor

            /// <summary>
            /// ctor
            /// </summary>
            /// <param name="keySelector">键选择函数</param>
            /// <param name="comparer">比较键的函数</param>
            public CommonComparer( Func20<T, V> keySelector, IComparer<V> comparer )
            {
                _keySelector = keySelector;
                _comparer = comparer;
            }

            /// <summary>
            /// ctor
            /// </summary>
            /// <param name="keySelector">键选择函数</param>
            public CommonComparer( Func20<T, V> keySelector )
                : this( keySelector, Comparer<V>.Default )
            {
            }

            #endregion

            #region IComparer<T> Members

            public int Compare( T x, T y )
            {
                return _comparer.Compare( _keySelector( x ), _keySelector( y ) );
            }

            #endregion
        }

        #endregion
    }
}