using System;

namespace Moons.Common20.CRC
{
    /// <summary>
    /// CRC16 的摘要说明。
    /// </summary>
    public class CRC16 : ICRC
    {
        #region CRC 16 位校验表

        /// <summary>
        /// 16 位校验表 Lower 表
        /// </summary>
        private static readonly ushort[] LowerCrcTable = new ushort[]
                                                             {
                                                                 0x0000, 0x1021, 0x2042, 0x3063, 0x4084, 0x50a5, 0x60c6,
                                                                 0x70e7,
                                                                 0x8108, 0x9129, 0xa14a, 0xb16b, 0xc18c, 0xd1ad, 0xe1ce,
                                                                 0xf1ef
                                                             };

        /// <summary>
        /// 16 位校验表 Upper 表
        /// </summary>
        private static readonly ushort[] UpperCrcTable = new ushort[]
                                                             {
                                                                 0x0000, 0x1231, 0x2462, 0x3653, 0x48c4, 0x5af5, 0x6ca6,
                                                                 0x7e97,
                                                                 0x9188, 0x83b9, 0xb5ea, 0xa7db, 0xd94c, 0xcb7d, 0xfd2e,
                                                                 0xef1f
                                                             };

        #endregion

        #region 算法实现

        /// <summary>
        /// The crc data checksum so far.
        /// </summary>
        private ushort _crc;

        /// <summary>
        /// 属性 作为校验后的结果
        /// </summary>
        public long Value
        {
            get { return _crc; }
            set { _crc = (ushort)value; }
        }

        /// <summary>
        /// 重新设置crc 初始值
        /// </summary>
        public void Reset()
        {
            _crc = 0;
        }

        /// <summary>
        /// Crc16
        /// </summary>
        /// <param name="byteValue"></param>
        public void Crc( int byteValue )
        {
            var h = (ushort)( ( _crc >> 12 ) & 0x0f );
            var l = (ushort)( ( _crc >> 8 ) & 0x0f );
            ushort temp = _crc;
            temp = (ushort)( ( ( temp & 0x00ff ) << 8 ) | byteValue );
            temp = (ushort)( temp ^ ( UpperCrcTable[( h - 1 ) + 1] ^ LowerCrcTable[( l - 1 ) + 1] ) );
            _crc = temp;
        }


        /// <summary>
        /// Crc16
        /// </summary>
        /// <param name="buffer"></param>
        public void Crc( byte[] buffer )
        {
            Crc( buffer, 0, buffer.Length );
        }

        /// <summary>
        /// Crc16
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="size"></param>
        public void Crc( byte[] buffer, int offset, int size )
        {
            if( buffer == null )
            {
                throw new ArgumentNullException( "buffer" );
            }

            if( offset < 0 || size < 0 || size > buffer.Length )
            {
                throw new ArgumentOutOfRangeException();
            }

            int bound = offset + size;
            for( int index = offset; index < bound; index++ )
            {
                Crc( buffer[index] );
            }
        }

        /// <summary>
        /// Crc16 
        /// </summary>
        /// <param name="ucrc"></param>
        /// <param name="buf"></param>
        public void Crc( ushort ucrc, byte[] buf )
        {
            _crc = ucrc;
            Crc( buf );
        }

        #endregion
    }
}