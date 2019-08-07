using System;
using System.IO;

namespace Moons.Common20
{
    /// <summary>
    /// 字节数组缓存类
    /// </summary>
    public class ByteBuffer
    {
        public ByteBuffer()
        {
            CheckReadEnough = true;
        }

        #region 变量和属性

        /// <summary>
        /// 初始化的时候是否检查已经读取了足够的数据，默认为true。
        /// </summary>
        public bool CheckReadEnough { get; set; }

        ///// <summary>
        ///// 内部锁
        ///// </summary>
        //private readonly object _lock = new object();

        /// <summary>
        /// 缓存
        /// </summary>
        public byte[] Buffer { get; private set; }

        /// <summary>
        /// 缓存偏移
        /// </summary>
        public int Offset { get; private set; }

        /// <summary>
        /// 用于写入的缓存偏移
        /// </summary>
        public int WriteOffset
        {
            get { return Offset + ReadCount; }
        }

        /// <summary>
        /// 需要读入的字节数量
        /// </summary>
        public int NeedCount
        {
            get { return Size2Read - ReadCount; }
        }

        /// <summary>
        /// 已经读取的字节数
        /// </summary>
        public int ReadCount { get; private set; }

        /// <summary>
        /// 需要读取的字节数
        /// </summary>
        public int Size2Read { get; private set; }

        /// <summary>
        /// true：已经读取了足够的字节，false：不够
        /// </summary>
        public bool ReadEnough
        {
            get { return 0 < Size2Read && Size2Read == ReadCount; }
        }

        #endregion

        /// <summary>
        /// 清空（重置）
        /// </summary>
        public void Clear()
        {
            Init( null, 0, 0 );
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="buffer">缓存</param>
        /// <param name="offset">缓存偏移</param>
        /// <param name="count">缓存的字节数</param>
        public void Init( byte[] buffer, int offset, int count )
        {
            //lock( _lock )
            {
                if( CheckReadEnough && ( ReadCount < Size2Read ) )
                {
                    throw new Exception(
                        string.Format(
                            "Not read full(Size2Read:{0}, ReadCount:{1}, Offset:{2})！",
                            Size2Read, ReadCount, Offset ) );
                }

                Buffer = buffer;
                Offset = offset;
                Size2Read = count;
                ReadCount = 0;
            }
        }

        /// <summary>
        /// 读入数据
        /// </summary>
        /// <param name="data">读入数据的缓存</param>
        /// <param name="offset">缓存偏移</param>
        /// <param name="readCount">读取的字节数</param>
        public void Read( byte[] data, int offset, int readCount )
        {
            //lock( _lock )
            {
                if( Size2Read < ReadCount + readCount )
                {
                    throw new ArgumentOutOfRangeException(
                        string.Format(
                            "数据超出缓存长度(Size2Read:{0}, ReadCount:{1}, Offset:{2}, offset:{3}, readCount:{4})！",
                            Size2Read, ReadCount, Offset, offset, readCount ) );
                }

                ArrayUtils.CopyBytes( data, offset, Buffer, Offset + ReadCount, readCount );
                //Array.Copy( data, offset, Buffer, Offset + ReadCount, readCount );
                ReadCount += readCount;
            }
        }

        /// <summary>
        /// 增加读取的字节数
        /// </summary>
        /// <param name="readCount">读取的字节数</param>
        public void AddReadCount( int readCount )
        {
            //lock( _lock )
            {
                if( Size2Read < ReadCount + readCount )
                {
                    throw new ArgumentOutOfRangeException(
                        string.Format(
                            "数据超出缓存长度(Size2Read:{0}, ReadCount:{1}, Offset:{2}, readCount:{3})！",
                            Size2Read, ReadCount, Offset, readCount ) );
                }

                ReadCount += readCount;
            }
        }

        /// <summary>
        /// 创建用于读取的MemoryStream
        /// </summary>
        /// <returns>用于读取的MemoryStream</returns>
        public MemoryStream CreateReadStream()
        {
            return new MemoryStream( Buffer, Offset, Size2Read, false );
        }
    }
}