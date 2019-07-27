using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Moons.Common20
{
    /// <summary>
    /// 带事件通知的集合的基类
    /// </summary>
    /// <typeparam name="T">元素的类型</typeparam>
    /// <typeparam name="TCollection">子类集合类型</typeparam>
    [Serializable]
    public class CollectionWithEventBase<T, TCollection> : CollectionBase<T, TCollection>
        where TCollection : CollectionWithEventBase<T, TCollection>, new()
    {
        #region ctor

        public CollectionWithEventBase()
        {
        }

        public CollectionWithEventBase( int capacity )
            : base( capacity )
        {
        }

        public CollectionWithEventBase( IEnumerable<T> collection ) : base( collection )
        {
        }

        #endregion

        #region  事件

        /// <summary>
        /// 集合发生改变事件
        /// </summary>
        public event CollectionChangeEventHandler CollectionChanged;

        /// <summary>
        /// 激发集合改变事件
        /// </summary>
        /// <param name="e">事件参数</param>
        private void RaiseCollectionChangedEvent( CollectionChangeEventArgs e )
        {
            EventUtils.FireEvent( CollectionChanged, this, e );
        }

        #endregion

        #region 隐藏实现基类方法

        public new void Add( T item )
        {
            base.Add( item );
            RaiseCollectionChangedEvent( new CollectionChangeEventArgs( CollectionChangeAction.Add, item ) );
        }

        public new void AddRange( IEnumerable<T> collection )
        {
            // 如果collection是通过Linq语法产生，并且是惰性加载的话，每次访问collection获得的元素对象都不相同，
            // 这里临时生成一个集合addItems来避免这个问题。
            var addItems = new List<T>( collection );

            base.AddRange( addItems );
            RaiseCollectionChangedEvent( new CollectionChangeEventArgs( CollectionChangeAction.Add, addItems ) );
        }

        public new void Clear()
        {
            base.Clear();
            RaiseCollectionChangedEvent( new CollectionChangeEventArgs( CollectionChangeAction.Refresh, null ) );
        }

        public new void Insert( int index, T item )
        {
            base.Insert( index, item );
            RaiseCollectionChangedEvent( new CollectionChangeEventArgs( CollectionChangeAction.Add, item ) );
        }

        public new bool Remove( T item )
        {
            bool ret = base.Remove( item );
            if( ret )
            {
                RaiseCollectionChangedEvent( new CollectionChangeEventArgs( CollectionChangeAction.Remove, item ) );
            }

            return ret;
        }

        public new void RemoveAll( Predicate<T> match )
        {
            base.RemoveAll( match );
            RaiseCollectionChangedEvent( new CollectionChangeEventArgs( CollectionChangeAction.Remove, null ) );
        }

        public new void RemoveRange( int index, int count )
        {
            base.RemoveRange( index, count );
            RaiseCollectionChangedEvent( new CollectionChangeEventArgs( CollectionChangeAction.Remove, null ) );
        }

        public new void RemoveAt( int index )
        {
            T item = this[index];
            base.RemoveAt( index );
            RaiseCollectionChangedEvent( new CollectionChangeEventArgs( CollectionChangeAction.Remove, item ) );
        }

        public new void Reverse()
        {
            base.Reverse();
            RaiseCollectionChangedEvent( new CollectionChangeEventArgs( CollectionChangeAction.Refresh, null ) );
        }

        public new void Sort()
        {
            base.Sort();
            RaiseCollectionChangedEvent( new CollectionChangeEventArgs( CollectionChangeAction.Refresh, null ) );
        }

        public new void Sort( Comparison<T> comparison )
        {
            base.Sort( comparison );
            RaiseCollectionChangedEvent( new CollectionChangeEventArgs( CollectionChangeAction.Refresh, null ) );
        }

        public new void Sort( IComparer<T> comparer )
        {
            base.Sort( comparer );
            RaiseCollectionChangedEvent( new CollectionChangeEventArgs( CollectionChangeAction.Refresh, null ) );
        }

        public new void Sort( int index, int count, IComparer<T> comparer )
        {
            base.Sort( index, count, comparer );
            RaiseCollectionChangedEvent( new CollectionChangeEventArgs( CollectionChangeAction.Refresh, null ) );
        }

        #endregion
    }
}