using System;

namespace Moons.Common20.CRC
{
    /// <summary>
    /// CRC8 的摘要说明。
    /// </summary>
    public class CRC8 : ICRC
    {
        #region CRC 8 位校验表

        /// <summary>
        /// CRC 8 位校验表
        /// </summary>
        private static readonly byte[] Crc8Table = new byte[]
                                                       {
                                                           0, 94, 188, 226, 97, 63, 221, 131, 194, 156, 126, 32, 163,
                                                           253, 31, 65,
                                                           157, 195, 33, 127, 252, 162, 64, 30, 95, 1, 227, 189, 62, 96,
                                                           130, 220,
                                                           35, 125, 159, 193, 66, 28, 254, 160, 225, 191, 93, 3, 128,
                                                           222, 60, 98,
                                                           190, 224, 2, 92, 223, 129, 99, 61, 124, 34, 192, 158, 29, 67,
                                                           161, 255,
                                                           70, 24, 250, 164, 39, 121, 155, 197, 132, 218, 56, 102, 229,
                                                           187, 89, 7,
                                                           219, 133, 103, 57, 186, 228, 6, 88, 25, 71, 165, 251, 120, 38
                                                           , 196, 154,
                                                           101, 59, 217, 135, 4, 90, 184, 230, 167, 249, 27, 69, 198,
                                                           152, 122, 36,
                                                           248, 166, 68, 26, 153, 199, 37, 123, 58, 100, 134, 216, 91, 5
                                                           , 231, 185,
                                                           140, 210, 48, 110, 237, 179, 81, 15, 78, 16, 242, 172, 47,
                                                           113, 147, 205,
                                                           17, 79, 173, 243, 112, 46, 204, 146, 211, 141, 111, 49, 178,
                                                           236, 14, 80,
                                                           175, 241, 19, 77, 206, 144, 114, 44, 109, 51, 209, 143, 12,
                                                           82, 176, 238,
                                                           50, 108, 142, 208, 83, 13, 239, 177, 240, 174, 76, 18, 145,
                                                           207, 45, 115,
                                                           202, 148, 118, 40, 171, 245, 23, 73, 8, 86, 180, 234, 105, 55
                                                           , 213, 139,
                                                           87, 9, 235, 181, 54, 104, 138, 212, 149, 203, 41, 119, 244,
                                                           170, 72, 22,
                                                           233, 183, 85, 11, 136, 214, 52, 106, 43, 117, 151, 201, 74,
                                                           20, 246, 168,
                                                           116, 42, 200, 150, 21, 75, 169, 247, 182, 232, 10, 84, 215,
                                                           137, 107, 53
                                                       };

        #endregion

        #region 算法实现

        /// <summary>
        /// The crc data checksum so far.
        /// </summary>
        private uint _crc;

        /// <summary>
        /// 属性 ：返回 CRC8校验结果;
        /// </summary>
        public long Value
        {
            get { return _crc; }
            set { _crc = (uint)value; }
        }


        /// <summary>
        /// CRC校验前重新设置校验值
        /// </summary>
        public void Reset()
        {
            _crc = 0;
        }

        /// <summary>
        /// 8 位 CRC 校验 产生校验码 只要被校验码
        /// </summary>
        /// <param name="byteValue"></param>
        public void Crc( int byteValue )
        {
            _crc = Crc8Table[_crc ^ byteValue];
        }

        /// <summary>
        /// 8 位 CRC 校验 产生校验码 只要被校验的字结数组
        /// </summary>
        /// <param name="buffer"></param>
        public void Crc( byte[] buffer )
        {
            Crc( buffer, 0, buffer.Length );
        }

        /// <summary>
        /// 8 位 CRC 校验 产生校验码 要被校验的字结数组 和起始结果位置
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

            if( offset < 0 || size < 0 || offset + size > buffer.Length )
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
        /// 8 位 CRC 校验 产生校验码 需要被校验码和校验码 
        /// </summary>
        /// <param name="crc"></param>
        /// <param name="oldCrc"> 初始为 0 ,以后为 返回值 ret </param>
        /// <returns> 产生校验码时 ret　为校验码</returns>
        public void Crc( byte crc, byte oldCrc )
        {
            _crc = Crc8Table[oldCrc ^ crc];
        }

        #endregion
    }
}