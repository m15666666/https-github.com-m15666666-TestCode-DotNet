using Moons.Caching.Abstractions;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;

namespace Moons.Caching.Implementation
{
    public class DefaultJsonCachingSerializer : ICachingSerializer
    {
        private readonly JsonSerializer jsonSerializer;

        public DefaultJsonCachingSerializer()
        {
            jsonSerializer = new JsonSerializer { };
        }

        public T Deserialize<T>(byte[] bytes)
        {
            if (typeof(T) == typeof(byte[]))
            {
                return (T)(object)bytes;
            }
            using (var ms = new MemoryStream(bytes))
            {
                using (var sr = new StreamReader(ms, Encoding.UTF8))
                {
                    using (var jtr = new JsonTextReader(sr))
                    {
                        return jsonSerializer.Deserialize<T>(jtr);
                    }
                }
            }
        }

        public object Deserialize(byte[] bytes, Type type)
        {
            if (type == typeof(byte[]))
            {
                return bytes;
            }
            using (var ms = new MemoryStream(bytes))
            {
                using (var sr = new StreamReader(ms, Encoding.UTF8))
                {
                    using (var jtr = new JsonTextReader(sr))
                    {
                        return jsonSerializer.Deserialize(jtr, type);
                    }
                }
            }
        }

        public byte[] Serialize<T>(T value)
        {
            if (typeof(T) == typeof(byte[]))
            {
                return (byte[])(object)value;
            }

            using (var ms = new MemoryStream())
            {
                using (var sr = new StreamWriter(ms, Encoding.UTF8))
                {
                    using (var jtr = new JsonTextWriter(sr))
                    {
                        jsonSerializer.Serialize(jtr, value);
                    }
                    return ms.ToArray();
                }
            }
        }
    }
}
