using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Moons.Common20
{
    /// <summary>
    /// 像Hash表一样的字典，使用key访问数据时，如果key不存在只返回null，而不是抛出异常
    /// </summary>
    /// <typeparam name="TKey">键类型</typeparam>
    /// <typeparam name="TValue">值类型</typeparam>
    public class HashDictionary<TKey, TValue> : Dictionary<TKey, TValue>, IDictionary<TKey, TValue>
    {
        #region IDictionary<TKey,TValue> Members

        public new virtual TValue this[ TKey key ]
        {
            get { return ContainsKey( key ) ? base[key] : default( TValue ); }
            set { base[key] = value; }
        }

        #endregion
    }

    /// <summary>
    /// 像Hash表一样的字典，使用key访问数据时，如果key不存在只返回null，而不是抛出异常
    /// </summary>
    /// <typeparam name="TKey">键类型</typeparam>
    /// <typeparam name="TValue">值类型</typeparam>
    public class ConcurrentHashDictionary<TKey, TValue> : ConcurrentDictionary<TKey, TValue>, IDictionary<TKey, TValue>
    {
        #region IDictionary<TKey,TValue> Members

        public new virtual TValue this[TKey key]
        {
            get { return ContainsKey(key) ? base[key] : default(TValue); }
            set { base[key] = value; }
        }

        #endregion
    }

    /// <summary>
    /// 像Hash表一样的字典，使用key访问数据时，如果key不存在只返回null，而不是抛出异常，并且键是不区分大小写的字符串
    /// </summary>
    /// <typeparam name="TValue">值类型</typeparam>
    public class IgnoreCaseKeyDictionary<TValue> : HashDictionary<string, TValue>, IDictionary<string, TValue>
    {
        #region IDictionary<string,TValue> Members

        /// <summary>
        /// 获取/设置数据
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        public override TValue this[ string key ]
        {
            get { return base[CharCaseUtils.ToCase( key )]; }
            set { base[CharCaseUtils.ToCase( key )] = value; }
        }

        /// <summary>
        /// 覆盖基类函数
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public new bool ContainsKey( string key )
        {
            return base.ContainsKey( CharCaseUtils.ToCase( key ) );
        }

        #endregion
    }

    /// <summary>
    /// 像Hash表一样的字典，使用key访问数据时，如果key不存在只返回null，而不是抛出异常，并且键是不区分大小写的字符串
    /// </summary>
    /// <typeparam name="TValue">值类型</typeparam>
    public class ConcurrentIgnoreCaseKeyDictionary<TValue> : ConcurrentHashDictionary<string, TValue>, IDictionary<string, TValue>
    {
        #region IDictionary<string,TValue> Members

        /// <summary>
        /// 获取/设置数据
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        public override TValue this[string key]
        {
            get { return base[CharCaseUtils.ToCase(key)]; }
            set { base[CharCaseUtils.ToCase(key)] = value; }
        }

        /// <summary>
        /// 覆盖基类函数
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public new bool ContainsKey(string key)
        {
            return base.ContainsKey(CharCaseUtils.ToCase(key));
        }

        #endregion
    }
}