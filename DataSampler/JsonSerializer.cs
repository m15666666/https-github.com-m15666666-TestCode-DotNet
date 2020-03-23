using Moons.Common20.Serialization.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataSampler
{
    /// <summary>
    /// 使用json字符串序列化、反序列化对象
    /// </summary>
    public class JsonSerializer : IJsonSerializer
    {
        public T DeserializeObject<T>(string json)
        {
            return (T)Newtonsoft.Json.JsonConvert.DeserializeObject(json); ;
        }

        public object DeserializeObject(string json)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject(json); ;
        }

        public string SerializeObject(object value)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(value); ;
        }
    }
}
