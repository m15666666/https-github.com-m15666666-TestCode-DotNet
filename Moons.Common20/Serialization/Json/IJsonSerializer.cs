using System;
using System.Collections.Generic;
using System.Text;

namespace Moons.Common20.Serialization.Json
{
    /// <summary>
    /// 使用json字符串序列化、反序列化对象
    /// 可以使用https://www.newtonsoft.com/json库来实现接口
    /// </summary>
    public interface IJsonSerializer
    {
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="json">json字符串</param>
        /// <returns>对象</returns>
        T DeserializeObject<T>(string json);

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="json">json字符串</param>
        /// <returns>对象</returns>
        object DeserializeObject(string json);

        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="value">要序列化的对象</param>
        /// <returns>json字符串</returns>
        string SerializeObject(object value);
    }
}
