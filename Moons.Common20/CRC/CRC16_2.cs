using System;

namespace Moons.Common20.CRC
{
    /// <summary>
    /// 金鑫提供的crc16算法，在隧道灯项目中使用，
    /// 对应的多项式为：CRC16-CCITT( x16+x12+x5+1 )
    /// </summary>
    public class CRC16_2 : ICRC
    {
        #region CRC 16 位校验表

        /// <summary>
        /// 16 位校验表
        /// </summary>
        private static readonly ushort[] CrcTable = new ushort[]
                                                        {
                                                            0x0000, 0x1021, 0x2042, 0x3063,
                                                            0x4084, 0x50a5, 0x60c6, 0x70e7,
                                                            0x8108, 0x9129, 0xa14a, 0xb16b,
                                                            0xc18c, 0xd1ad, 0xe1ce, 0xf1ef
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
            Crc( new[] {(byte)byteValue}, 0, 1 );
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

            // 将1个字节的高4位和低4位拆成两个部分
            var partsOfByte = new byte[2];
            int bound = offset + size;
            for( int index = offset; index < bound; index++ )
            {
                const int HighPartIndex = 0;
                const int LowPartIndex = 1;

                partsOfByte[HighPartIndex] = partsOfByte[LowPartIndex] = buffer[index];

                // 高4位
                partsOfByte[HighPartIndex] >>= 4;

                // 低4位
                partsOfByte[LowPartIndex] &= 0xF;

                foreach( byte part in partsOfByte )
                {
                    // 高4位
                    var highPart = (byte)( _crc >> 12 );

                    _crc <<= 4;
                    _crc ^= CrcTable[highPart ^ part];
                }
            }
        }

        #endregion
    }
}