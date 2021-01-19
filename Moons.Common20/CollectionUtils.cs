using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Moons.Common20.Collections;

namespace Moons.Common20
{
    /// <summary>
    /// 集合实用工具
    /// </summary>
    public static class CollectionUtils
    {
        #region 转换为List

        /// <summary>
        /// Toes the list.
        /// </summary>
        /// <param name="data">集合或是对象.</param>
        /// <returns>list</returns>
        public static IList<T> ToList<T>( object data )
        {
            var ret = new List<T>();
            if( data is IEnumerable<T> )
            {
                ret.AddRange( data as IEnumerable<T> );
            }
            else if( data is T )
            {
                ret.Add( (T)data );
            }
            return ret;
        }

        /// <summary>
        /// Toes the list.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <returns>list</returns>
        public static IList ToList( IEnumerable collection )
        {
            var ret = new List<object>();
            if( collection != null )
            {
                foreach( object item in collection )
                {
                    ret.Add( item );
                }
            }

            return ret;
        }

        /// <summary>
        /// Toes the list.
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="collection">The collection.</param>
        /// <returns>list</returns>
        public static IList<T> ToList<T>( IEnumerable collection )
        {
            var ret = new List<T>();
            if( collection != null )
            {
                foreach( object item in collection )
                {
                    if( item is T )
                    {
                        ret.Add( (T)item );
                    }
                }
            }

            return ret;
        }

        /// <summary>
        /// Toes the list.
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="collection">The collection.</param>
        /// <returns>list</returns>
        public static IList<T> ToList<T>( IEnumerable<T> collection )
        {
            var ret = new List<T>();
            if( collection != null )
            {
                ret.AddRange( collection );
            }
            return ret;
        }

        /// <summary>
        /// 转化为数组
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="collection">The collection.</param>
        /// <returns>数组</returns>
        public static T[] ToArray<T>( IEnumerable<T> collection )
        {
            IList<T> list = ToList( collection );
            var ret = new T[list.Count];
            if( 0 < ret.Length )
            {
                list.CopyTo( ret, 0 );
            }
            return ret;
        }

        #endregion

        #region 转换为字典

        /// <summary>
        /// Toes the Dictionary.
        /// </summary>
        /// <typeparam name="TKey">键类型</typeparam>
        /// <typeparam name="TValue">值类型</typeparam>
        /// <param name="collection">The collection.</param>
        /// <returns>Dictionary</returns>
        public static IDictionary<TKey, TValue> ToDictionary<TKey, TValue>( IDictionary<TKey, TValue> collection )
        {
            var ret = new Dictionary<TKey, TValue>();
            CopyTo( collection, ret );
            return ret;
        }

        #endregion

        #region 复制到系列函数

        /// <summary>
        /// 复制字典
        /// </summary>
        /// <typeparam name="TKey">键类型</typeparam>
        /// <typeparam name="TValue">值类型</typeparam>
        /// <param name="source">来源</param>
        /// <param name="target">目标</param>
        public static void CopyTo<TKey, TValue>( IDictionary<TKey, TValue> source, IDictionary<TKey, TValue> target )
        {
            if( source == null || target == null )
            {
                return;
            }
            foreach( var keyValuePair in source )
            {
                target[keyValuePair.Key] = keyValuePair.Value;
            }
        }

        #endregion

        #region 模拟扩展方法

        /// <summary>
        /// 获得指定个数的元素
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="collection">集合</param>
        /// <param name="count">个数</param>
        /// <returns>集合</returns>
        public static Collection<T> Take<T>( IEnumerable<T> collection, int count )
        {
            var ret = new Collection<T>();
            if( collection != null )
            {
                foreach( T item in collection )
                {
                    if( count-- < 1 )
                    {
                        break;
                    }

                    ret.Add( item );
                }
            }
            return ret;
        }

        /// <summary>
        /// 是否包含查找项
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="collection">集合</param>
        /// <param name="findItem">查找项</param>
        /// <returns>true：包含，false：不包含</returns>
        public static bool Contains<T>( IEnumerable<T> collection, T findItem )
        {
            if( collection != null )
            {
                EqualityComparer<T> comparer = EqualityComparer<T>.Default;
                foreach( T value in collection )
                {
                    if( comparer.Equals( value, findItem ) )
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 检测集合元素满足条件的数量
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="collection">集合</param>
        /// <param name="predicate">检测函数</param>
        /// <returns>集合元素满足条件的数量</returns>
        public static int Count<T>( IEnumerable<T> collection, Predicate<T> predicate )
        {
            return Where( collection, predicate ).Count;
        }

        /// <summary>
        /// 检测集合任意元素是否满足条件
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="collection">集合</param>
        /// <param name="predicate">检测函数</param>
        /// <returns>true：至少一个元素满足条件，false：都不满足</returns>
        public static bool Any<T>( IEnumerable<T> collection, Predicate<T> predicate )
        {
            return IsNotEmpty( Where( collection, predicate ) );
        }

        /// <summary>
        /// 过滤集合
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="collection">集合</param>
        /// <param name="predicate">检测函数</param>
        /// <returns>集合</returns>
        public static Collection<T> Where<T>( IEnumerable<T> collection, Predicate<T> predicate )
        {
            var ret = new Collection<T>();
            if( collection != null && predicate != null )
            {
                foreach( T item in collection )
                {
                    if( predicate( item ) )
                    {
                        ret.Add( item );
                    }
                }
            }
            return ret;
        }

        /// <summary>
        /// 找出类型为T的元素
        /// </summary>
        /// <typeparam name="T">输出类型</typeparam>
        /// <param name="collection">集合</param>
        /// <returns>集合</returns>
        public static Collection<T> OfType<T>( IEnumerable collection )
        {
            var ret = new Collection<T>();
            if( collection != null )
            {
                foreach( object item in collection )
                {
                    if( item is T )
                    {
                        ret.Add( (T)item );
                    }
                }
            }
            return ret;
        }

        /// <summary>
        /// 转变为另一个类型的集合
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <typeparam name="TOutput">另一个类型</typeparam>
        /// <param name="collection">集合</param>
        /// <param name="converter">转换函数</param>
        /// <returns>另一个类型的集合</returns>
        public static Collection<TOutput> ConvertAll<T, TOutput>( IEnumerable<T> collection,
                                                                  Converter<T, TOutput> converter )
        {
            if( collection == null || converter == null )
            {
                throw new ArgumentNullException();
            }

            var ret = new Collection<TOutput>();
            foreach( T item in collection )
            {
                ret.Add( converter( item ) );
            }
            return ret;
        }

        #region FirstOrDefault、LastOrDefault等操作

        /// <summary>
        /// 返回第一项
        /// </summary>
        /// <param name="collection">集合</param>
        /// <returns>第一项</returns>
        public static object First( IList collection )
        {
            if( IsNullOrEmpty( collection ) )
            {
                throw new InvalidOperationException();
            }
            return collection[0];
        }

        /// <summary>
        /// 返回第一项
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="collection">集合</param>
        /// <returns>第一项</returns>
        public static T First<T>( IList<T> collection )
        {
            if( IsNullOrEmptyG( collection ) )
            {
                throw new InvalidOperationException();
            }
            return collection[0];
        }

        /// <summary>
        /// 返回最后一项
        /// </summary>
        /// <param name="collection">集合</param>
        /// <returns>最后一项</returns>
        public static object Last( IList collection )
        {
            if( IsNullOrEmpty( collection ) )
            {
                throw new InvalidOperationException();
            }
            return collection[collection.Count - 1];
        }

        /// <summary>
        /// 返回最后一项
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="collection">集合</param>
        /// <returns>最后一项</returns>
        public static T Last<T>( IList<T> collection )
        {
            if( IsNullOrEmptyG( collection ) )
            {
                throw new InvalidOperationException();
            }
            return collection[collection.Count - 1];
        }

        /// <summary>
        /// 返回第一项或默认值
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="collection">集合</param>
        /// <returns>第一项或默认值</returns>
        public static T FirstOrDefault<T>( IEnumerable<T> collection )
        {
            return FirstOrDefault( collection, null );
        }

        /// <summary>
        /// 返回第一项或默认值
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="collection">集合</param>
        /// <param name="predicate">检测函数</param>
        /// <returns>第一项或默认值</returns>
        public static T FirstOrDefault<T>( IEnumerable<T> collection, Predicate<T> predicate )
        {
            if( collection != null )
            {
                predicate = predicate ?? ( item => true );
                foreach( T item in collection )
                {
                    if( predicate( item ) )
                    {
                        return item;
                    }
                }
            }
            return default( T );
        }

        public static T FirstOrDefault<T>(Func<T,bool> predicate, params IEnumerable<T>[] datas) where T : class
        {
            foreach( var data in datas)
            {
                var first = data.FirstOrDefault(predicate);
                if (first != default) return first;
            }
            return default;
        }

        /// <summary>
        /// 返回最后一项或默认值
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="collection">集合</param>
        /// <returns>最后一项或默认值</returns>
        public static T LastOrDefault<T>( IList<T> collection )
        {
            return LastOrDefault( collection, null );
        }

        /// <summary>
        /// 返回最后一项或默认值
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="collection">集合</param>
        /// <param name="predicate">检测函数</param>
        /// <returns>最后一项或默认值</returns>
        public static T LastOrDefault<T>( IList<T> collection, Predicate<T> predicate )
        {
            if( collection != null )
            {
                predicate = predicate ?? ( item => true );
                for( int index = collection.Count - 1; 0 <= index; --index )
                {
                    T item = collection[index];
                    if( predicate( item ) )
                    {
                        return item;
                    }
                }
            }
            return default( T );
        }

        #endregion

        #endregion

        #region 是否为空

        /// <summary>
        /// 是否任一个集合为空
        /// </summary>
        /// <param name="collections">集合的集合</param>
        /// <returns>true：至少有一个为空，false：都不为空</returns>
        public static bool IsAnyNullOrEmpty( params ICollection[] collections )
        {
            if( IsNullOrEmptyG( collections ) )
            {
                return false;
            }

            return ForUtils.Any( collections, IsNullOrEmpty );
        }

        /// <summary>
        /// 是否为空
        /// </summary>
        /// <param name="collection">集合</param>
        /// <returns>true：为空，false：不为空</returns>
        public static bool IsNullOrEmpty( ICollection collection )
        {
            return collection == null || collection.Count == 0;
        }

        /// <summary>
        /// 是否非空
        /// </summary>
        /// <param name="collection">集合</param>
        /// <returns>true：非空，false：空</returns>
        public static bool IsNotEmpty( ICollection collection )
        {
            return !IsNullOrEmpty( collection );
        }

        /// <summary>
        /// 是否为空
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="collection">集合</param>
        /// <returns>true：为空，false：不为空</returns>
        public static bool IsNullOrEmptyG<T>( ICollection<T> collection )
        {
            return collection == null || collection.Count == 0;
        }

        /// <summary>
        /// 是否非空
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="collection">集合</param>
        /// <returns>true：非空，false：空</returns>
        public static bool IsNotEmptyG<T>( ICollection<T> collection )
        {
            return !IsNullOrEmptyG( collection );
        }

        #endregion

        #region 长度是否相等

        /// <summary>
        /// 长度是否相等
        /// </summary>
        /// <param name="a">集合1</param>
        /// <param name="b">集合2</param>
        /// <returns>true：长度相等，false：不等</returns>
        public static bool IsLengthEqual( ICollection a, ICollection b )
        {
            if( a == null && b == null )
            {
                return true;
            }

            if( a == null || b == null )
            {
                return false;
            }

            return a.Count == b.Count;
        }

        /// <summary>
        /// 长度是否相等
        /// </summary>
        /// <typeparam name="T1">集合1元素类型</typeparam>
        /// <typeparam name="T2">集合2元素类型</typeparam>
        /// <param name="a">集合1</param>
        /// <param name="b">集合2</param>
        /// <returns>true：长度相等，false：不等</returns>
        public static bool IsLengthEqualG<T1, T2>( ICollection<T1> a, ICollection<T2> b )
        {
            if( a == null && b == null )
            {
                return true;
            }

            if( a == null || b == null )
            {
                return false;
            }

            return a.Count == b.Count;
        }

        #endregion
    }
}