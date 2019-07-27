using Moons.Common20.CRC;

namespace Moons.Common20.Serialization
{
    /// <summary>
    /// 针对 IBinaryWrite 接口的实用工具类
    /// </summary>
    public static class IBinaryWriteUtils
    {
        /// <summary>
        /// 写人流并进行crc校验
        /// </summary>
        /// <param name="writer">BinaryWriter</param>
        /// <param name="crc">ICRC</param>
        /// <param name="bytes">写入的字节数组</param>
        public static void WriteAndCrc( IBinaryWrite writer, ICRC crc, byte[] bytes )
        {
            crc.Crc( bytes );
            writer.Write( bytes );
        }
    }
}