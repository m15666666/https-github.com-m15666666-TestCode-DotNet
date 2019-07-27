using System;
using System.Collections.Generic;

namespace Moons.Common20.Collections
{
    /// <summary>
    /// 带事件通知的集合
    /// </summary>
    /// <typeparam name="T">元素的类型</typeparam>
    [Serializable]
    public class CollectionWithEvent<T> : CollectionWithEventBase<T, CollectionWithEvent<T>>
    {
        #region ctor

        public CollectionWithEvent()
        {
        }

        public CollectionWithEvent( int capacity )
            : base( capacity )
        {
        }

        public CollectionWithEvent( IEnumerable<T> collection ) : base( collection )
        {
        }

        #endregion
    }
}