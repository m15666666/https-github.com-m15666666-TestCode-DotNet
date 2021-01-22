namespace Moons.Common20.Serialization
{
    /// <summary>
    ///     二进制读接口
    /// </summary>
    public interface IBinaryRead
    {
        /// <summary>
        ///     读值
        /// </summary>
        /// <returns>值</returns>
        byte ReadByte();

        /// <summary>
        ///     读值
        /// </summary>
        /// <param name="count">字节个数</param>
        /// <returns>值</returns>
        byte[] ReadBytes( int count );

        /// <summary>
        ///     读值
        /// </summary>
        /// <param name="buffer">缓冲区</param>
        /// <param name="offset">偏移</param>
        /// <param name="size">字节数</param>
        /// <returns>实际读的字节数，0表示读到了末尾</returns>
        int Read( byte[] buffer, int offset, int size );

        /// <summary>
        ///     读值
        /// </summary>
        /// <returns>值</returns>
        short ReadInt16();

        /// <summary>
        ///     读值
        /// </summary>
        /// <returns>值</returns>
        ushort ReadUInt16();

        /// <summary>
        ///     读值
        /// </summary>
        /// <returns>值</returns>
        int ReadInt32();

        /// <summary>
        ///     读值
        /// </summary>
        /// <returns>值</returns>
        uint ReadUInt32();

        /// <summary>
        ///     读值
        /// </summary>
        /// <returns>值</returns>
        long ReadInt64();

        /// <summary>
        ///     读值
        /// </summary>
        /// <returns>值</returns>
        ulong ReadUInt64();

        /// <summary>
        ///     读值
        /// </summary>
        /// <returns>值</returns>
        sbyte ReadSByte();

        /// <summary>
        ///     读值
        /// </summary>
        /// <returns>值</returns>
        float ReadSingle();

        /// <summary>
        ///     读值
        /// </summary>
        /// <returns>值</returns>
        double ReadDouble();

        /// <summary>
        /// 读取指定长度的int16类型的数组
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        short[] ReadInt16Array(int count);

        /// <summary>
        /// 读取指定长度的int32类型的数组
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        int[] ReadInt32Array(int count);

        /// <summary>
        /// 读取指定长度的Single类型的数组
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        float[] ReadSingleArray(int count);
    }
}