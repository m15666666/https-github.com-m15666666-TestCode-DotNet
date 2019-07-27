using System;

namespace Moons.Common20.Pool
{
    /// <summary>
    /// 数组内存管理器
    /// </summary>
    /// <typeparam name="T">数组元素类型，例如：double、int等</typeparam>
    public class ArrayMemoryManager<T> : ArrayPoolManager<T> where T : struct
    {
        /// <summary>
        /// ctor
        /// </summary>
        public ArrayMemoryManager()
        {
            DefaultPoolSize = 1024;

            ArrayLengths = new[]
                               {
                                   64,
                                   256,
                                   ByteCountPerK,
                                   ByteCountPerK * 4,
                                   ByteCountPerK * 8,
                                   ByteCountPerK * 32,
                                   ByteCountPerK * 128,
                                   ByteCountPerK * 256
                               };
        }

        #region 变量和属性

        /// <summary>
        /// 每K有多少个字节
        /// </summary>
        public const int ByteCountPerK = 1024;

        #region 数组属性

        /// <summary>
        /// 数组长度
        /// </summary>
        private int[] _arrayLengths;

        /// <summary>
        /// 数组长度
        /// </summary>
        public int[] ArrayLengths
        {
            get { return _arrayLengths; }
            set
            {
                _arrayLengths = value;
                if( value != null )
                {
                    ArrayUtils.SortArray( _arrayLengths, true );
                }
            }
        }

        /// <summary>
        /// 默认池的大小
        /// </summary>
        public int DefaultPoolSize { get; set; }

        #endregion

        #endregion

        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
            RemoveAllPool();
            Array.ForEach( ArrayLengths,
                           length =>
                               {
                                   ArrayPool<T> pool = AddPool( length );

                                   pool.Size = DefaultPoolSize;
                                   pool.IncrementCount = 0;
                               } );
        }

        /// <summary>
        /// 获得大于等于给定长度的数组，找不到返回null
        /// </summary>
        /// <param name="length">给定长度</param>
        /// <returns>大于等于给定长度的数组</returns>
        public T[] GetMoreThanLength( int length )
        {
            int targetLength = 0;
            foreach( int arrayLength in ArrayLengths )
            {
                if( length <= arrayLength )
                {
                    targetLength = arrayLength;
                    break;
                }
            }

            if( targetLength == 0 )
            {
                return null;
            }

            return GetByLength( targetLength ).GetArrayFromPool();
        }

        /// <summary>
        /// 返回数组
        /// </summary>
        /// <param name="data">数组</param>
        public void Return( T[] data )
        {
            ArrayPool<T> pool = GetByLength( data.Length );
            if( pool != null )
            {
                pool.PutArray2Pool( data );
            }
        }
    }
}