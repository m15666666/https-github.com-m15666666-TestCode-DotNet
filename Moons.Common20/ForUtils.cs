using System;
using System.Collections;
using System.Collections.Generic;

namespace Moons.Common20
{
    /// <summary>
    /// 用于循环的实用工具类
    /// </summary>
    public static class ForUtils
    {
        #region ForCount

        /// <summary>
        /// 循环count指定的次数，并执行函数
        /// </summary>
        /// <param name="count">次数</param>
        /// <param name="handler">函数</param>
        public static void ForCount( int count, Action20 handler )
        {
            for( int index = 0; index < count; index++ )
            {
                handler();
            }
        }

        /// <summary>
        /// 循环count指定的次数，并执行函数
        /// </summary>
        /// <param name="count">次数</param>
        /// <param name="handler">函数</param>
        public static void ForCount( int count, Action<int> handler )
        {
            for( int index = 0; index < count; index++ )
            {
                handler( index );
            }
        }

        #endregion

        #region ForEach

        /// <summary>
        /// 遍历集合执行函数
        /// </summary>
        /// <param name="collection">集合</param>
        /// <param name="handler">函数</param>
        public static void ForEach( IEnumerable collection, Action<object> handler )
        {
            if( collection != null )
            {
                foreach( object item in collection )
                {
                    if( item != null )
                    {
                        handler( item );
                    }
                }
            }
        }

        /// <summary>
        /// 遍历集合执行函数
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="collection">集合</param>
        /// <param name="handler">函数</param>
        public static void ForEach<T>( IEnumerable<T> collection, Action<T> handler )
        {
            if( collection != null )
            {
                foreach( T item in collection )
                {
                    handler( item );
                }
            }
        }

        /// <summary>
        /// 遍历集合执行函数
        /// </summary>
        /// <typeparam name="TKey">键类型</typeparam>
        /// <typeparam name="TValue">值类型</typeparam>
        /// <param name="collection">集合</param>
        /// <param name="handler">函数</param>
        public static void ForEach<TKey, TValue>( IDictionary<TKey, TValue> collection, Action20<TKey, TValue> handler )
        {
            if( collection != null )
            {
                foreach( var item in collection )
                {
                    handler( item.Key, item.Value );
                }
            }
        }

        /// <summary>
        /// 遍历两个集合的对应元素执行函数，执行次数为长度短的集合的长度
        /// </summary>
        /// <typeparam name="T1">集合1元素类型</typeparam>
        /// <typeparam name="T2">集合2元素类型</typeparam>
        /// <param name="collection1">集合1</param>
        /// <param name="collection2">集合2</param>
        /// <param name="handler">函数</param>
        public static void ForEach<T1, T2>( IEnumerable<T1> collection1, IEnumerable<T2> collection2,
                                            Action20<T1, T2> handler )
        {
            if( collection1 == null || collection2 == null )
            {
                return;
            }

            using( IEnumerator<T1> enumerator1 = collection1.GetEnumerator() )
            {
                using( IEnumerator<T2> enumerator2 = collection2.GetEnumerator() )
                {
                    while( true )
                    {
                        if( !enumerator1.MoveNext() )
                        {
                            return;
                        }

                        if( !enumerator2.MoveNext() )
                        {
                            return;
                        }

                        handler( enumerator1.Current, enumerator2.Current );
                    }
                }
            }
        }

        /// <summary>
        /// 遍历集合执行函数，函数返回true：继续执行，返回false：停止执行。
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="collection">集合</param>
        /// <param name="handler">函数，返回true：继续执行，返回false：停止执行</param>
        public static void UntilFalse4Each<T>( IEnumerable<T> collection, Predicate<T> handler )
        {
            if( collection != null )
            {
                foreach( T item in collection )
                {
                    if( !handler( item ) )
                    {
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// 遍历集合执行函数，任何一个元素执行handler函数后返回true则Any函数返回true，否则Any函数返回false。
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="collection">集合</param>
        /// <param name="handler">函数</param>
        /// <returns>任何一个元素执行handler函数后返回true则Any函数返回true，否则Any函数返回false。</returns>
        public static bool Any<T>( IEnumerable<T> collection, Predicate<T> handler )
        {
            if( collection != null )
            {
                foreach( T item in collection )
                {
                    if( handler( item ) )
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// 遍历集合执行函数，任何一个元素执行handler函数后返回false则All函数返回false，否则All函数返回true。
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="collection">集合</param>
        /// <param name="handler">函数</param>
        /// <returns>任何一个元素执行handler函数后返回false则All函数返回false，否则All函数返回true。</returns>
        public static bool All<T>( ICollection<T> collection, Predicate<T> handler )
        {
            if( CollectionUtils.IsNullOrEmptyG( collection ) )
            {
                return false;
            }

            return !Any( collection, item => !handler( item ) );
        }

        #endregion
    }
}