using System;
using System.Collections.Generic;
using System.Text;

namespace Moons.Common20.ObjectMapper
{
    /// <summary>
    /// 对象映射,类似automapper，MapsterMapper
    /// https://github.com/MapsterMapper/Mapster
    /// </summary>
    public interface IObjectMapper
    {
        /// <summary>
        /// 映射到目标对象
        /// </summary>
        /// <typeparam name="Destination">目标对象类型</typeparam>
        /// <param name="source">源对象</param>
        /// <param name="destination">目标对象</param>
        void MapTo<Destination>(object source, Destination destination);
    }
}
