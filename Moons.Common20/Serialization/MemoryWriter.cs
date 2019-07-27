using System.IO;

namespace Moons.Common20.Serialization
{
    /// <summary>
    /// 基于MemoryStream的二进制写入类
    /// </summary>
    public class MemoryWriter : BinaryWrite
    {
        #region ctor

        public MemoryWriter() : base( new MemoryStream() )
        {
        }

        public MemoryWriter( int capacity ) : base( new MemoryStream( capacity ) )
        {
        }

        #endregion

        #region 变量和属性

        /// <summary>
        /// 底层流的长度
        /// </summary>
        public long StreamLength
        {
            get { return BaseStream.Length; }
            set { BaseStream.SetLength( value ); }
        }

        #endregion

        #region 截断流

        /// <summary>
        /// 截断流，并将流当前位置设置为起始位置
        /// </summary>
        public void TruncateStream()
        {
            Seek( 0, SeekOrigin.Begin );
            StreamLength = 0;
        }

        #endregion

        #region 返回字节数组

        /// <summary>
        /// 返回字节数组
        /// </summary>
        /// <returns>字节数组</returns>
        public byte[] ToArray()
        {
            return ( (MemoryStream)BaseStream ).ToArray();
        }

        #endregion
    }
}