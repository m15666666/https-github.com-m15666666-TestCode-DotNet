using System.Collections.Generic;
using System.Reflection;

namespace Moons.Common20.Reflection
{
    /// <summary>
    /// PropertyInfo的集合类
    /// </summary>
    public class PropertyInfoCollection : CollectionBase<PropertyInfo, PropertyInfoCollection>
    {
        #region ctor

        public PropertyInfoCollection()
        {
        }

        public PropertyInfoCollection( IEnumerable<PropertyInfo> collection )
            : base( collection )
        {
        }

        #endregion

        #region 过滤集合

        /// <summary>
        /// 获得可读的属性集合
        /// </summary>
        /// <returns>属性集合</returns>
        public PropertyInfoCollection GetCanRead()
        {
            return Where( item => item.CanRead );
        }

        /// <summary>
        /// 获得可写的属性集合
        /// </summary>
        /// <returns>属性集合</returns>
        public PropertyInfoCollection GetCanWrite()
        {
            return Where( item => item.CanWrite );
        }

        /// <summary>
        /// 获得可读可写的属性集合
        /// </summary>
        /// <returns>属性集合</returns>
        public PropertyInfoCollection GetCanReadWrite()
        {
            return Where( item => item.CanRead && item.CanWrite );
        }

        #endregion
    }
}