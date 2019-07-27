namespace Moons.Common20.CRC
{
    /// <summary>
    /// 只是简单地通过异或校验，不查表
    /// </summary>
    public class XORCRC8
    {
        #region 静态校验函数

        /// <summary>
        /// crc校验
        /// </summary>
        /// <param name="value">校验的值</param>
        /// <param name="ret">输入和输出</param>
        public static void Crc( byte value, ref byte ret )
        {
            ret ^= value;
        }

        /// <summary>
        /// crc校验
        /// </summary>
        /// <param name="buffer">缓冲区</param>
        /// <param name="ret">输入和输出</param>
        public static void Crc( byte[] buffer, ref byte ret )
        {
            Crc( buffer, 0, buffer.Length, ref ret );
        }

        /// <summary>
        /// crc校验
        /// </summary>
        /// <param name="buffer">缓冲区</param>
        /// <param name="offset">偏移量</param>
        /// <param name="size">字节数</param>
        /// <param name="ret">输入和输出</param>
        public static void Crc( byte[] buffer, int offset, int size, ref byte ret )
        {
            int bound = offset + size;
            for( int index = offset; index < bound; index++ )
            {
                ret ^= buffer[index];
            }
        }

        #endregion
    }
}