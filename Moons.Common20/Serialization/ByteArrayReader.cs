using System;
using System.IO;

namespace Moons.Common20.Serialization
{
    /// <summary>
    /// 字节数组的读出类
    /// </summary>
    public class ByteArrayReader : BinaryRead
    {
        #region ctor

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="buffer"></param>
        public ByteArrayReader( byte[] buffer ) : this( buffer, 0, buffer.Length )
        {
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="size"></param>
        public ByteArrayReader( byte[] buffer, int offset, int size )
            : base( new MemoryStream( buffer, offset, size ) )
        {
            Buffer = buffer;
            Offset = offset;
            Size = size;
        }

        /// <summary>
        /// 通过数组长度创建对象
        /// </summary>
        /// <param name="length">数组长度</param>
        /// <returns>对象</returns>
        public static ByteArrayReader CreateByArrayLength( int length )
        {
            return new ByteArrayReader( new byte[length] );
        }

        public override short[] ReadInt16Array(int count)// 性能好一些4334ms
        {
            const int Size = ByteUtils.ByteCountPerInt16;
            
            long pos = base.BaseStream.Position;
            var ret = new short[count];
            var buffer = Buffer;
            for(int i = 0, start = Offset + (int)pos; i < count; i++, start += Size)
                ret[i] = BitConverter.ToInt16(buffer, start);

            base.BaseStream.Position = pos + Size * count;
            return ret;
        }

        public override int[] ReadInt32Array(int count)
        {
            const int Size = ByteUtils.ByteCountPerInt32;
            
            long pos = base.BaseStream.Position;
            var ret = new int[count];
            var buffer = Buffer;
            for(int i = 0, start = Offset + (int)pos; i < count; i++, start += Size)
                ret[i] = BitConverter.ToInt32(buffer, start);

            base.BaseStream.Position = pos + Size * count;
            return ret;
        }

        public override float[] ReadSingleArray(int count)
        {
            const int Size = ByteUtils.ByteCountPerSingle;
            
            long pos = base.BaseStream.Position;
            var ret = new float[count];
            var buffer = Buffer;
            for(int i = 0, start = Offset + (int)pos; i < count; i++, start += Size)
                ret[i] = BitConverter.ToSingle(buffer, start);

            base.BaseStream.Position = pos + Size * count;
            return ret;
        }

        #endregion

        #region 变量和属性

        /// <summary>
        /// 缓冲区
        /// </summary>
        public byte[] Buffer { get; private set; }

        /// <summary>
        /// 偏移
        /// </summary>
        public int Offset { get; private set; }

        /// <summary>
        /// 字节数
        /// </summary>
        public int Size { get; private set; }

        #endregion
    }
}