using Mapster;
using Moons.Common20.ObjectMapper;
using System;

namespace Moons.ObjectMapper
{
    /// <summary>
    /// 使用mapster实现IObjectMapper
    /// https://github.com/MapsterMapper/Mapster
    /// </summary>
    public class ObjectMapperOfMapster : IObjectMapper
    {
        void IObjectMapper.MapTo<Destination>(object source, Destination destination)
        {
            source.Adapt(destination);
        }
    }
}
