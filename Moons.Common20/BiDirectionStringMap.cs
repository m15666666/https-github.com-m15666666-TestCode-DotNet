using System;

namespace Moons.Common20
{
    /// <summary>
    /// 字符串双向映射类
    /// </summary>
    public class BiDirectionStringMap
    {
        #region 变量和属性

        /// <summary>
        /// 键对值的映射
        /// </summary>
        private HashDictionary<string, string> _key2Value = new HashDictionary<string, string>();

        /// <summary>
        /// 值对键的映射
        /// </summary>
        private HashDictionary<string, string> _value2Key = new HashDictionary<string, string>();

        #endregion

        #region 获取数据

        /// <summary>
        /// 根据键获得值
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        public string GetValueByKey( string key )
        {
            key = GetKey( key );
            return key != null ? _key2Value[key] : null;
        }

        /// <summary>
        /// 根据值获得键
        /// </summary>
        /// <param name="value">值</param>
        /// <returns>键</returns>
        public string GetKeyByValue( string value )
        {
            string key = GetKey( value );
            return key != null ? _value2Key[key] : null;
        }

        /// <summary>
        /// 获取键
        /// </summary>
        /// <param name="key">初始的键</param>
        /// <returns>键</returns>
        private static string GetKey( string key )
        {
            return CharCaseUtils.ToCase( key );
        }

        #endregion

        #region 设置数据

        /// <summary>
        /// 同时设置键和值之间的相互映射
        /// </summary>
        /// <param name="keys">键</param>
        /// <param name="values">值</param>
        public void SetKeyValues( string[] keys, string[] values )
        {
            SetKey2ValueMap( keys, values );
            SetValue2KeyMap( keys, values );
        }

        /// <summary>
        /// 设置键对值的映射
        /// </summary>
        /// <param name="keys">键</param>
        /// <param name="values">值</param>
        public void SetKey2ValueMap( string[] keys, string[] values )
        {
            _key2Value = GetKey2ValueMap( keys, values );
        }

        /// <summary>
        /// 设置值对键的映射
        /// </summary>
        /// <param name="keys">键</param>
        /// <param name="values">值</param>
        public void SetValue2KeyMap( string[] keys, string[] values )
        {
            _value2Key = GetKey2ValueMap( values, keys );
        }

        /// <summary>
        /// 获得键与值的映射对象
        /// </summary>
        /// <param name="keys">键</param>
        /// <param name="values">值</param>
        /// <returns>键与值的映射对象</returns>
        private HashDictionary<string, string> GetKey2ValueMap( string[] keys, string[] values )
        {
            CheckKeyValues( keys, values );

            var ret = new HashDictionary<string, string>();

            int index = 0;
            foreach( string key in keys )
            {
                string value = values[index++];
                if (string.IsNullOrEmpty(key))
                {
                    continue;
                }

                ret[GetKey( key )] = value;
            }

            return ret;
        }

        /// <summary>
        /// 检查输入参数
        /// </summary>
        /// <param name="keys">键</param>
        /// <param name="values">值</param>
        private void CheckKeyValues( string[] keys, string[] values )
        {
            if( keys == null || values == null || keys.Length != values.Length )
            {
                throw new ArgumentException( "键和值为空或长度不匹配！" );
            }
        }

        #endregion
    }
}