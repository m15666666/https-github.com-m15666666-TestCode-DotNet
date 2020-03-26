using AutoMapper;
using Moons.Common20.ObjectMapper;
using System;
using System.Collections.Generic;
using System.Text;
using IObjectMapper = Moons.Common20.ObjectMapper.IObjectMapper;

namespace Moons.ObjectMapper
{
    /// <summary>
    /// 使用automapper实现IObjectMapper
    /// https://automapper.readthedocs.io/en/latest/Getting-started.html#where-do-i-configure-automapper
    /// </summary>
    public class ObjectMapperOfAutomapper : IObjectMapper
    {
        private IMapper _mapper;
        public ObjectMapperOfAutomapper(MapperConfiguration config)
        {
            _mapper = config.CreateMapper();
            // or var mapper = new Mapper(config);
        }

        void IObjectMapper.MapTo<Destination>(object source, Destination destination)
        {
            _mapper.Map(source, destination);
        }
    }
}
