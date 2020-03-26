using System;
using System.Collections.Generic;
using System.Text;

namespace Moons.Common20.ObjectMapper
{
    /// <summary>
    /// 对象映射扩展
    /// </summary>
    public static class ObjectMapperExtensions
    {
        /// <summary>
        /// 映射到目标对象
        /// </summary>
        /// <typeparam name="Destination">目标对象类型</typeparam>
        /// <param name="source">源对象</param>
        /// <returns>目标对象</returns>
        public static Destination MapTo<Destination>(this IObjectMapper mapper, object source) where Destination : new()
        {
            Destination ret = new Destination();
            mapper.MapTo<Destination>(source, ret);
            return ret;
        }
    }
}
