using Newtonsoft.Json;

namespace DataSampler
{
    /// <summary>
    /// 使用json字符串序列化、反序列化对象
    /// 
    /// 参考：https://www.cnblogs.com/huangenai/p/7513235.html
    /// </summary>
    public class JsonSerializer : Moons.Common20.Serialization.Json.IJsonSerializer
    {
        private readonly bool _removeNull = false;
        private readonly JsonSerializerSettings _jsonSetting;
        public JsonSerializer( bool removeNull = false )
        {
            _removeNull = removeNull;
            _jsonSetting = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
        }

        public T DeserializeObject<T>(string json)
        {
            return (T)JsonConvert.DeserializeObject(json,typeof(T));
        }

        public object DeserializeObject(string json)
        {
            return JsonConvert.DeserializeObject(json); ;
        }

        public string SerializeObject(object value)
        {
            return _removeNull ? JsonConvert.SerializeObject(value, _jsonSetting) : JsonConvert.SerializeObject(value);
        }
    }
}
