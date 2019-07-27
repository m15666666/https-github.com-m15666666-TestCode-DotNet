using System;
using System.Collections.Generic;
using Moons.Common20.Collections;

namespace Moons.Common20
{
    /// <summary>
    /// 集合的基类
    /// </summary>
    /// <typeparam name="T">元素的类型</typeparam>
    /// <typeparam name="TCollection">子类集合类型</typeparam>
#if !SILVERLIGHT
    [Serializable]
#endif
    public class CollectionBase<T, TCollection> : List<T> where TCollection : CollectionBase<T, TCollection>, new()
    {
        #region ctor

        public CollectionBase()
        {
        }

        public CollectionBase( int capacity ) : base( capacity )
        {
        }

        public CollectionBase( IEnumerable<T> collection )
        {
            AddRange( collection );
        }

        #endregion

        #region 变量和属性

        /// <summary>
        /// 集合是否为空
        /// </summary>
        public bool IsEmpty
        {
            get { return Count == 0; }
        }

        #endregion

        #region FirstOrDefault、LastOrDefault等操作

        /// <summary>
        /// 返回第一项
        /// </summary>
        /// <returns>第一项</returns>
        public T First()
        {
            return CollectionUtils.First( this as IList<T> );
        }

        /// <summary>
        /// 返回最后一项
        /// </summary>
        /// <returns>最后一项</returns>
        public T Last()
        {
            return CollectionUtils.Last( this as IList<T> );
        }

        /// <summary>
        /// 返回第一项或默认值
        /// </summary>
        /// <returns>第一项或默认值</returns>
        public T FirstOrDefault()
        {
            return CollectionUtils.FirstOrDefault( this );
        }

        /// <summary>
        /// 返回第一项或默认值
        /// </summary>
        /// <param name="predicate">检测函数</param>
        /// <returns>第一项或默认值</returns>
        public T FirstOrDefault( Predicate<T> predicate )
        {
            return CollectionUtils.FirstOrDefault( this, predicate );
        }

        /// <summary>
        /// 返回最后一项或默认值
        /// </summary>
        /// <returns>最后一项或默认值</returns>
        public T LastOrDefault()
        {
            return CollectionUtils.LastOrDefault( this );
        }

        /// <summary>
        /// 返回最后一项或默认值
        /// </summary>
        /// <param name="predicate">检测函数</param>
        /// <returns>最后一项或默认值</returns>
        public T LastOrDefault( Predicate<T> predicate )
        {
            return CollectionUtils.LastOrDefault( this, predicate );
        }

        #endregion

        #region 过滤集合

        /// <summary>
        /// 获得指定个数的元素
        /// </summary>
        /// <param name="count">个数</param>
        /// <returns>集合</returns>
        public TCollection Take( int count )
        {
            var output = new TCollection();
            output.AddRange( CollectionUtils.Take( this, count ) );

            return output;
        }

        /// <summary>
        /// 检测集合元素满足条件的数量
        /// </summary>
        /// <param name="predicate">检测函数</param>
        /// <returns>集合元素满足条件的数量</returns>
        public int CalcCount( Predicate<T> predicate )
        {
            return CollectionUtils.Count( this, predicate );
        }

        /// <summary>
        /// 检测集合任意元素是否满足条件
        /// </summary>
        /// <param name="predicate">检测函数</param>
        /// <returns>true：至少一个元素满足条件，false：都不满足</returns>
        public bool Any( Predicate<T> predicate )
        {
            return CollectionUtils.Any( this, predicate );
        }

        /// <summary>
        /// 过滤集合
        /// </summary>
        /// <param name="handler">过滤函数代理，返回true表示加入输出集合，false：表示不加入输出集合</param>
        /// <param name="output">输出集合</param>
        public void Where( Predicate<T> handler, TCollection output )
        {
            ForUtils.ForEach( this, item =>
                                        {
                                            if( handler( item ) )
                                            {
                                                output.Add( item );
                                            }
                                        }
                );
        }

        /// <summary>
        /// 过滤集合
        /// </summary>
        /// <param name="handler">过滤函数代理，返回true表示加入输出集合，false：表示不加入输出集合</param>
        /// <returns>输出集合</returns>
        public TCollection Where( Predicate<T> handler )
        {
            var output = new TCollection();
            Where( handler, output );

            return output;
        }

        /// <summary>
        /// 转变为另一个类型的集合
        /// </summary>
        /// <typeparam name="TOutput">另一个类型</typeparam>
        /// <param name="converter">转换函数</param>
        /// <returns>另一个类型的集合</returns>
        public new Collection<TOutput> ConvertAll<TOutput>( Converter<T, TOutput> converter )
        {
            return CollectionUtils.ConvertAll( this, converter );
        }

        #endregion

        #region ToString

        public override string ToString()
        {
            return string.Format( "[{0}]", StringUtils.Join( this ) );
        }

        #endregion
    }
}