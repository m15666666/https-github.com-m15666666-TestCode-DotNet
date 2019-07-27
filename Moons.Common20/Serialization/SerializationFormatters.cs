using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Moons.Common20.Serialization
{
    /// <summary>
    ///     序列化与反序列化
    /// </summary>
    public static class SerializationFormatters
    {
        #region 变量和属性

        /// <summary>
        ///     二进制序列化器
        /// </summary>
        public static BinaryFormatter BinaryFormatter
        {
            get { return new BinaryFormatter(); }
        }

        #endregion

        #region 序列化对象

        /// <summary>
        ///     序列化对象
        /// </summary>
        /// <param name="obj">可序列化对象</param>
        /// <returns>byte数组</returns>
        public static byte[] Serialize( object obj )
        {
            return Serialize( obj, BinaryFormatter );
        }

        /// <summary>
        ///     序列化对象
        /// </summary>
        /// <param name="obj">可序列化对象</param>
        /// <param name="binaryFormatter">BinaryFormatter</param>
        /// <returns>byte数组</returns>
        public static byte[] Serialize( object obj, BinaryFormatter binaryFormatter )
        {
            if( obj == null )
            {
                return null;
            }

            using( var ms = new MemoryStream() )
            {
                binaryFormatter.Serialize( ms, obj );
                return ms.ToArray();
            }
        }

        #endregion

        #region 反序列化对象

        /// <summary>
        ///     反序列化对象
        /// </summary>
        /// <param name="buffer">byte数组</param>
        /// <returns>对象</returns>
        public static object DeserializeBinary( byte[] buffer )
        {
            return DeserializeBinary( buffer, BinaryFormatter );
        }

        /// <summary>
        ///     反序列化对象
        /// </summary>
        /// <param name="buffer">byte数组</param>
        /// <param name="binaryFormatter">BinaryFormatter</param>
        /// <returns>对象</returns>
        public static object DeserializeBinary( byte[] buffer, BinaryFormatter binaryFormatter )
        {
            if( buffer == null || buffer.Length == 0 )
            {
                return null;
            }

            using( var ms = new MemoryStream( buffer, false ) )
            {
                return binaryFormatter.Deserialize( ms );
            }
        }

        /// <summary>
        ///     反序列化对象
        /// </summary>
        /// <param name="buffer">byte数组</param>
        /// <param name="offset">偏移</param>
        /// <param name="count">字节数</param>
        /// <param name="binaryFormatter">BinaryFormatter</param>
        /// <returns>对象</returns>
        public static object DeserializeBinary( byte[] buffer, int offset, int count, BinaryFormatter binaryFormatter )
        {
            if( buffer == null || buffer.Length == 0 )
            {
                return null;
            }

            if( count < 1 || ( buffer.Length < offset + count ) )
            {
                throw new ArgumentNullException(
                    string.Format( " DeserializeBinary buffer(offset:{0}, count:{1}, buffer.Length)！",
                        offset, count ) );
            }

            using( var ms = new MemoryStream( buffer, offset, count, false ) )
            {
                return binaryFormatter.Deserialize( ms );
            }
        }

        #endregion
    }
}