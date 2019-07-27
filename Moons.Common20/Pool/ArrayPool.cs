using System.Collections.Generic;

namespace Moons.Common20.Pool
{
    /// <summary>
    /// 数组池
    /// </summary>
    /// <typeparam name="T">数组元素类型，例如：double、int等</typeparam>
    public class ArrayPool<T> : ObjectPoolBase<T[]> where T : struct
    {
        #region 变量和属性

        /// <summary>
        /// 数组长度
        /// </summary>
        public int Length { get; private set; }

        /// <summary>
        /// 从池中获得数组的次数
        /// </summary>
        public long GetArrayFromPoolCount { get; set; }

        /// <summary>
        /// 将数组放到池中的次数
        /// </summary>
        public long PutArray2PoolCount { get; set; }

        /// <summary>
        /// 分配内存的次数
        /// </summary>
        public long AllocateMemoryCount { get; set; }

        /// <summary>
        /// 回收内存的次数
        /// </summary>
        public long CollectMemoryCount { get; set; }

        #endregion

        #region 创建对象

        /// <summary>
        /// 创建数组池
        /// </summary>
        /// <param name="length">数组长度</param>
        /// <returns>数组池</returns>
        public static ArrayPool<T> Create( int length )
        {
            var ret = new ArrayPool<T> {Length = length};
            ret.Reset();

            ret.CreateObjectHandler = () => new T[ret.Length];

            //ret.UnInitializeHandler = ( ( T[] array ) => ret.CollectMemoryCount++ );

            return ret;
        }

        /// <summary>
        /// 从池中获得数组
        /// </summary>
        /// <returns>数组</returns>
        public T[] GetArrayFromPool()
        {
            //GetArrayFromPoolCount++;
            return GetFromPool();
        }

        /// <summary>
        /// 将数组放到池中
        /// </summary>
        /// <param name="array">数组</param>
        /// <returns>是否放到了池中，true：放入池中，false：未放入池</returns>
        public bool PutArray2Pool( T[] array )
        {
            //PutArray2PoolCount++;
            return Put2Pool( array );
        }

        /// <summary>
        /// 获得信息，用于调试
        /// </summary>
        /// <returns></returns>
        public string GetInfo()
        {
            return string.Format(
                "Length:{0}," +
                "Size:{1}," +
                "GetCount:{2}," +
                "PutCount{3}," +
                "AllocateCount{4}," +
                "CollectCount{5}," +
                "AvailableCount{6}",
                Length,
                Size,
                GetArrayFromPoolCount,
                PutArray2PoolCount,
                AllocateMemoryCount,
                CollectMemoryCount,
                AvailableCount );
        }

        /// <summary>
        /// 重置
        /// </summary>
        private void Reset()
        {
            GetArrayFromPoolCount = PutArray2PoolCount = CollectMemoryCount = AllocateMemoryCount = 0;
        }

        #endregion
    }

    /// <summary>
    /// 数组池管理器
    /// </summary>
    /// <typeparam name="T">数组元素类型，例如：double、int等</typeparam>
    public class ArrayPoolManager<T> where T : struct
    {
        #region 变量和属性

        /// <summary>
        /// 数据长度对数组池的映射
        /// </summary>
        private readonly HashDictionary<string, ArrayPool<T>> _length2Pool = new HashDictionary<string, ArrayPool<T>>();

        /// <summary>
        /// 内部锁
        /// </summary>
        private readonly object _lock = new object();

        #endregion

        /// <summary>
        /// 增加池
        /// </summary>
        /// <param name="length">数组长度</param>
        /// <returns>T类型的ArrayPool</returns>
        public ArrayPool<T> AddPool( int length )
        {
            ArrayPool<T> pool = GetByLength( length );
            if( pool == null )
            {
                lock( _lock )
                {
                    pool = GetByLength( length );
                    if( pool == null )
                    {
                        pool = ArrayPool<T>.Create( length );
                        _length2Pool.Add( length.ToString(), pool );
                    }
                }
            }

            return pool;
        }

        /// <summary>
        /// 获得所有池
        /// </summary>
        public IList<ArrayPool<T>> GetAllPool()
        {
            var ret = new List<ArrayPool<T>>();
            lock( _lock )
            {
                foreach( var pool in _length2Pool.Values )
                {
                    ret.Add( pool );
                }
            }
            return ret;
        }

        /// <summary>
        /// 删除所有池
        /// </summary>
        public void RemoveAllPool()
        {
            lock( _lock )
            {
                _length2Pool.Clear();
            }
        }

        /// <summary>
        /// 删除池
        /// </summary>
        /// <param name="length">数组长度</param>
        public void RemovePool( int length )
        {
            if( !PoolExist( length ) )
            {
                return;
            }

            lock( _lock )
            {
                _length2Pool.Remove( length.ToString() );
            }
        }

        /// <summary>
        /// 根据数组长度获得数组池
        /// </summary>
        /// <param name="length">数组长度</param>
        /// <returns>数组池</returns>
        public ArrayPool<T> GetByLength( int length )
        {
            return _length2Pool[length.ToString()];
        }

        /// <summary>
        /// 数组池是否已存在
        /// </summary>
        /// <param name="length">数组长度</param>
        /// <returns>数组池是否已存在</returns>
        public bool PoolExist( int length )
        {
            return GetByLength( length ) != null;
        }
    }
}