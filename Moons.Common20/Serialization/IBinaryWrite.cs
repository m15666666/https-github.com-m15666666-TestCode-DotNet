namespace Moons.Common20.Serialization
{
    /// <summary>
    ///     二进制写接口
    /// </summary>
    public interface IBinaryWrite
    {
        /// <summary>
        ///     写入值到输出流
        /// </summary>
        /// <param name="value">值</param>
        void Write( double value );

        /// <summary>
        ///     写入值到输出流
        /// </summary>
        /// <param name="value">值</param>
        void Write( float value );

        /// <summary>
        ///     写入值到输出流
        /// </summary>
        /// <param name="value">值</param>
        void Write( long value );

        /// <summary>
        ///     写入值到输出流
        /// </summary>
        /// <param name="value">值</param>
        void Write( ulong value );

        /// <summary>
        ///     写入值到输出流
        /// </summary>
        /// <param name="value">值</param>
        void Write( int value );

        /// <summary>
        ///     写入值到输出流
        /// </summary>
        /// <param name="value">值</param>
        void Write( uint value );

        /// <summary>
        ///     写入值到输出流
        /// </summary>
        /// <param name="value">值</param>
        void Write( short value );

        /// <summary>
        ///     写入值到输出流
        /// </summary>
        /// <param name="value">值</param>
        void Write( ushort value );

        /// <summary>
        ///     写入值到输出流
        /// </summary>
        /// <param name="value">值</param>
        void Write( byte value );

        /// <summary>
        ///     写入值到输出流
        /// </summary>
        /// <param name="value">值</param>
        void Write( sbyte value );

        /// <summary>
        ///     写入值到输出流
        /// </summary>
        /// <param name="buffer">缓冲区</param>
        void Write( byte[] buffer );

        /// <summary>
        ///     写入值到输出流
        /// </summary>
        /// <param name="buffer">缓冲区</param>
        /// <param name="offset">偏移</param>
        /// <param name="size">字节数</param>
        void Write( byte[] buffer, int offset, int size );
    }
}