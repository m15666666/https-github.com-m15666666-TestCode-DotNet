using System;
using System.Collections.Generic;

namespace Moons.Common20.Collections
{
    /// <summary>
    /// 列表类型的集合
    /// </summary>
    /// <typeparam name="T">元素的类型</typeparam>
#if !SILVERLIGHT
    [Serializable]
#endif
    public class Collection<T> : CollectionBase<T, Collection<T>>, IEnumerable<T>
    {
        #region ctor

        public Collection()
        {
        }

        public Collection( int capacity ) : base( capacity )
        {
        }

        public Collection( IEnumerable<T> collection ) : base( collection )
        {
        }

        #endregion
    }
}