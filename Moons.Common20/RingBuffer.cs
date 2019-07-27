using System;

namespace Moons.Common20
{
    /// <summary>
    /// 环状缓存
    /// </summary>
    public class RingBuffer<T>
    {
        public RingBuffer()
        {
            EnableOverlap = false;

            RaiseExceptionOnOverflow = false;
        }

        public RingBuffer( int capacity ) : this()
        {
            Capacity = capacity;
        }

        #region 变量和属性

        /// <summary>
        /// 从没有数据到有一个数据时，第一个数据的下标
        /// </summary>
        private const int FirstDataIndex = 0;

        /// <summary>
        /// 内部缓存
        /// </summary>
        private T[] _buffer;

        /// <summary>
        /// 内部缓存的偏移
        /// </summary>
        private int _bufferOffset;

        /// <summary>
        /// 内部缓存的大小
        /// </summary>
        private int _bufferSize;

        /// <summary>
        /// 元素的个数
        /// </summary>
        private int _count;

        /// <summary>
        /// 起始的下标
        /// </summary>
        private int _startIndex;

        /// <summary>
        /// 容量
        /// </summary>
        public int Capacity
        {
            get { return _bufferSize; }
            set
            {
                int capacity = value;
                if( capacity < 2 )
                {
                    throw new ArgumentException( "容量必须大于1！" );
                }

                InitBuffer( new T[capacity], 0, capacity );

                Clear();
            }
        }

        /// <summary>
        /// 元素个数
        /// </summary>
        public int Count
        {
            get { return _count; }
        }

        /// <summary>
        /// true：没有数据
        /// </summary>
        private bool HasNoData
        {
            get { return _count == 0; }
        }

        /// <summary>
        /// 是否允许覆盖，默认为false：不允许覆盖
        /// </summary>
        public bool EnableOverlap { get; set; }

        /// <summary>
        /// EnableOverlap为false情况下，当缓存不足以容纳数据时是否抛出异常，默认false：不抛出异常
        /// </summary>
        public bool RaiseExceptionOnOverflow { get; set; }

        #endregion

        #region 初始化缓存

        /// <summary>
        /// 初始化缓存
        /// </summary>
        /// <param name="buffer">缓存</param>
        /// <param name="offset">缓存偏移</param>
        /// <param name="count">缓存的字节数</param>
        public void InitBuffer( T[] buffer, int offset, int count )
        {
            _buffer = buffer;
            _bufferOffset = offset;
            _bufferSize = count;
        }

        #endregion

        #region 计算下标和元素个数的调整

        /// <summary>
        /// 计算增加元素时，下标和元素个数的调整
        /// </summary>
        /// <param name="dataCount">增加的个数</param>
        /// <param name="iterateStartIndex">迭代的起始下标</param>
        /// <param name="iterateCount">迭代的次数</param>
        private void CalcAddIndexCount( int dataCount, out int iterateStartIndex, out int iterateCount )
        {
            int count = _count;
            int capacity = Capacity;

            // 没有数据时
            if( HasNoData )
            {
                iterateStartIndex = _startIndex = FirstDataIndex;
            }
            else
            {
                iterateStartIndex = _startIndex + 1;
            }

            // 循环次数
            iterateCount = dataCount;

            int totalCount = count + dataCount;
            if( totalCount <= capacity )
            {
                _count += dataCount;
            }
            else
            {
                _count = capacity;
                if( EnableOverlap )
                {
                    _startIndex = ( totalCount - capacity + _startIndex ) % capacity;
                }
                else
                {
                    iterateCount = capacity - count;
                }
            }
        }

        #endregion

        #region 操作缓存

        /// <summary>
        /// 清空
        /// </summary>
        public void Clear()
        {
            _count = 0;
        }

        /// <summary>
        /// 抛出数据超出容量异常
        /// </summary>
        /// <param name="dataCount">数据长度</param>
        private void ThrowOverflowException( int dataCount )
        {
            throw new Exception( string.Format( "数据超出容量(Capacity:{0}, Count:{1}, dataCount:{2})！", Capacity, Count,
                                                dataCount ) );
        }

        /// <summary>
        /// 增加元素
        /// </summary>
        /// <param name="array">缓存</param>
        /// <param name="offset">缓存偏移</param>
        /// <param name="dataCount">缓存的字节数</param>
        public void Add( T[] array, int offset, int dataCount )
        {
            if( array == null || array.Length < dataCount )
            {
                throw new ArgumentException( string.Format(
                    "数组长度不合法(array:{0}, array.Length:{1}, dataCount:{2})！",
                    array, array.Length, dataCount ) );
            }

            int iterateStartIndex;
            int iterateCount;
            CalcAddIndexCount( dataCount, out iterateStartIndex, out iterateCount );

            if( iterateCount < dataCount && RaiseExceptionOnOverflow )
            {
                ThrowOverflowException( dataCount );
            }

            if( iterateCount == 0 )
            {
                return;
            }

            int capacity = Capacity;
            int count = 0;
            for( int index = 0; index < dataCount; index++ )
            {
                _buffer[_bufferOffset + iterateStartIndex % capacity] = array[offset + index];
                iterateStartIndex++;

                count++;
                if( count == iterateCount )
                {
                    break;
                }
            }
        }

        /// <summary>
        /// 增加元素
        /// </summary>
        /// <param name="data">数据</param>
        public void Add( T data )
        {
            bool hasNoData = HasNoData;
            int capacity = Capacity;
            int count = _count;
            bool lessThanCapacity = count < capacity;

            if( lessThanCapacity )
            {
                _count++;
            }

            // 没有数据
            if( hasNoData )
            {
                int index = _startIndex = FirstDataIndex;
                _buffer[_bufferOffset + index] = data;
                return;
            }

            // 容量未满
            if( lessThanCapacity )
            {
                int index = ( _startIndex + count ) % capacity;
                _buffer[_bufferOffset + index] = data;
                return;
            }

            // 不支持覆盖，容量已满，返回
            if( !EnableOverlap )
            {
                if( RaiseExceptionOnOverflow )
                {
                    ThrowOverflowException( 1 );
                }
                return;
            }

            // 覆盖最老的一个数据
            _buffer[_bufferOffset + _startIndex] = data;
            _startIndex = ( _startIndex + 1 ) % capacity;
        }

        /// <summary>
        /// 从队列开头移除target.Length个元素到数组target中
        /// </summary>
        /// <param name="target">目标数组</param>
        public void RemoveStart( T[] target )
        {
            if( target == null || target.Length == 0 || _count < target.Length )
            {
                return;
            }

            RemoveStart( target, 0, target.Length );
        }

        /// <summary>
        /// 从队列开头移除target.Length个元素到数组target中
        /// </summary>
        /// <param name="target">目标数组</param>
        public void RemoveStart( T[] target, int offset, int count )
        {
            if( Count < count )
            {
                throw new ArgumentOutOfRangeException( string.Format( "没有足够的数据(Count:{0}, count:{1})！", Count, count ) );
            }

            int capacity = Capacity;
            int startIndex = _startIndex;

            // 需要复制的数据个数
            int dataCount = count;

            // 需要从数组头开始复制的个数
            int restCount = startIndex + dataCount - capacity;

            bool hasRestCount = 0 < restCount;

            // 第一段复制的个数
            int firstCount = dataCount - ( hasRestCount ? restCount : 0 );

            Array.Copy( _buffer, _bufferOffset + startIndex, target, offset, firstCount );
            if( hasRestCount )
            {
                Array.Copy( _buffer, _bufferOffset, target, offset + firstCount, restCount );
            }

            _count -= dataCount;
            if( 0 < _count )
            {
                _startIndex = ( startIndex + dataCount ) % capacity;
            }
        }

        #endregion
    }
}