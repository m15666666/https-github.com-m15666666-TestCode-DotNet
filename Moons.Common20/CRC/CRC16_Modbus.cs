using System;

namespace Moons.Common20.CRC
{
    /// <summary>
    /// Modbus协议使用的crc16算法
    /// 参考：http://www.cnitblog.com/zfly/archive/2010/05/12/5334.html
    /// </summary>
    public class CRC16_Modbus : ICRC
    {
        /// <summary>
        /// ctor
        /// </summary>
        public CRC16_Modbus()
        {
            Reset();
        }

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
            _crc = 0xFFFF;
        }

        /// <summary>
        /// Crc16
        /// </summary>
        /// <param name="byteValue"></param>
        public void Crc( int byteValue )
        {
            Crc( new[] { (byte)byteValue }, 0, 1 );
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
                _crc ^= buffer[index];

                for( int indexBit = 0; indexBit < 8; indexBit++ )
                {
                    if( ( _crc & 0x1 ) != 0 )
                    {
                        _crc = (ushort)( ( _crc >> 1 ) ^ 0xA001 );
                    }
                    else
                    {
                        _crc >>= 1;
                    }
                }
            }
        }

        #endregion
    }
}