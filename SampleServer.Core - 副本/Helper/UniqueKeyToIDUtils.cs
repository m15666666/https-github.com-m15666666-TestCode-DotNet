using System.Collections.Generic;
using Moons.Common20;

namespace SampleServer.Helper
{
    /// <summary>
    /// 将唯一的字符串键转换为唯一ID的实用工具类
    /// </summary>
    /// <typeparam name="TID">ID类型</typeparam>
    public class UniqueStringKeyToIDUtils<TID> : UniqueKeyToIDUtils<string, TID>
    {
        #region ctor

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="maxCount">ID缓存的最大数量</param>
        /// <param name="createIDHandler">创建ID的函数</param>
        public UniqueStringKeyToIDUtils( int maxCount, Func20<TID> createIDHandler )
            : base( maxCount, createIDHandler )
        {
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="maxCount">ID缓存的最大数量</param>
        /// <param name="createIDHandler">创建ID的函数</param>
        public UniqueStringKeyToIDUtils( int maxCount, Func20<string, TID> createIDHandler )
            : base( maxCount, createIDHandler )
        {
        }

        #endregion

        /// <summary>
        /// 根据唯一键获得ID
        /// </summary>
        /// <param name="uniqueKey">唯一键</param>
        /// <returns>ID</returns>
        public override TID GetIDByUniqueKey( string uniqueKey )
        {
            return string.IsNullOrEmpty( uniqueKey ) ? default( TID ) : base.GetIDByUniqueKey( uniqueKey );
        }
    }

    /// <summary>
    /// 将唯一的键转换为唯一ID的实用工具类
    /// </summary>
    /// <typeparam name="TKey">键类型</typeparam>
    /// <typeparam name="TID">ID类型</typeparam>
    public class UniqueKeyToIDUtils<TKey, TID>
    {
        #region ctor

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="maxCount">ID缓存的最大数量</param>
        /// <param name="createIDHandler">创建ID的函数</param>
        public UniqueKeyToIDUtils( int maxCount, Func20<TID> createIDHandler )
            : this( maxCount, key => createIDHandler() )
        {
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="maxCount">ID缓存的最大数量</param>
        /// <param name="createIDHandler">创建ID的函数</param>
        public UniqueKeyToIDUtils( int maxCount, Func20<TKey, TID> createIDHandler )
        {
            _uniqueKeys = FixSizeQueue<TKey>.Create( maxCount );
            _uniqueKeys.RemoveByMaxCount = item =>
                                               {
                                                   if( _uniqueKey2ID.ContainsKey( item ) )
                                                   {
                                                       _uniqueKey2ID.Remove( item );
                                                   }
                                               };

            _createIDHandler = createIDHandler;
        }

        #endregion

        #region 变量和属性

        /// <summary>
        /// 创建ID的函数
        /// </summary>
        private readonly Func20<TKey, TID> _createIDHandler;

        /// <summary>
        /// 锁
        /// </summary>
        private readonly object _lock = new object();

        /// <summary>
        /// 唯一键对ID的映射
        /// </summary>
        private readonly Dictionary<TKey, TID> _uniqueKey2ID = new Dictionary<TKey, TID>();

        /// <summary>
        /// 最多保存n个键，超出的则覆盖。
        /// </summary>
        private readonly FixSizeQueue<TKey> _uniqueKeys;

        #endregion

        /// <summary>
        /// 根据唯一键获得ID
        /// </summary>
        /// <param name="uniqueKey">唯一键</param>
        /// <returns>ID</returns>
        public virtual TID GetIDByUniqueKey( TKey uniqueKey )
        {
            lock( _lock )
            {
                if( _uniqueKey2ID.ContainsKey( uniqueKey ) )
                {
                    return _uniqueKey2ID[uniqueKey];
                }

                _uniqueKeys.Add( uniqueKey );
                return _uniqueKey2ID[uniqueKey] = _createIDHandler( uniqueKey );
            }
        }
    }
}