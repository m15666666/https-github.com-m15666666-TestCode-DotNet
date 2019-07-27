using System;
using System.Collections.Generic;
using System.Net;

namespace Moons.Common20
{
    /// <summary>
    /// 与字节操作相关的实用工具类
    /// </summary>
    public static class ByteUtils
    {
        #region 常量

        /// <summary>
        /// 每个double是几个字节
        /// </summary>
        public const int ByteCountPerDouble = 8;

        /// <summary>
        /// 每个float是几个字节
        /// </summary>
        public const int ByteCountPerSingle = 4;

        /// <summary>
        /// 每个long是几个字节
        /// </summary>
        public const int ByteCountPerInt64 = 8;

        /// <summary>
        /// 每个int是几个字节
        /// </summary>
        public const int ByteCountPerInt32 = 4;

        /// <summary>
        /// 每个short是几个字节
        /// </summary>
        public const int ByteCountPerInt16 = 2;

        /// <summary>
        /// 每个byte是几个字节
        /// </summary>
        public const int ByteCountPerByte = 1;

        /// <summary>
        /// 每个byte是几个二进制位
        /// </summary>
        public const int BitCountPerByte = 8;

        #endregion

        #region 布尔值

        /// <summary>
        /// 真
        /// </summary>
        public const byte True = 1;

        /// <summary>
        /// 假
        /// </summary>
        public const byte False = 0;

        #endregion

        #region 转为字节数组

        /// <summary>
        /// 将short转为字节数组
        /// </summary>
        /// <param name="value">值</param>
        /// <returns>字节数组</returns>
        public static byte[] Int16ToBytes( short value )
        {
            return BitConverter.GetBytes( value );
        }

        /// <summary>
        /// 将ushort转为字节数组
        /// </summary>
        /// <param name="value">值</param>
        /// <returns>字节数组</returns>
        public static byte[] UInt16ToBytes( ushort value )
        {
            return BitConverter.GetBytes( value );
        }

        /// <summary>
        /// 将int转为字节数组
        /// </summary>
        /// <param name="value">值</param>
        /// <returns>字节数组</returns>
        public static byte[] Int32ToBytes( int value )
        {
            return BitConverter.GetBytes( value );
        }

        /// <summary>
        /// 将uint转为字节数组
        /// </summary>
        /// <param name="value">值</param>
        /// <returns>字节数组</returns>
        public static byte[] UInt32ToBytes( uint value )
        {
            return BitConverter.GetBytes( value );
        }

        /// <summary>
        /// 将long转为字节数组
        /// </summary>
        /// <param name="value">值</param>
        /// <returns>字节数组</returns>
        public static byte[] Int64ToBytes( long value )
        {
            return BitConverter.GetBytes( value );
        }

        /// <summary>
        /// 将ulong转为字节数组
        /// </summary>
        /// <param name="value">值</param>
        /// <returns>字节数组</returns>
        public static byte[] UInt64ToBytes( ulong value )
        {
            return BitConverter.GetBytes( value );
        }

        /// <summary>
        /// 将float转为字节数组
        /// </summary>
        /// <param name="value">值</param>
        /// <returns>字节数组</returns>
        public static byte[] SingleToBytes( float value )
        {
            return BitConverter.GetBytes( value );
        }

        /// <summary>
        /// 将double转为字节数组
        /// </summary>
        /// <param name="value">值</param>
        /// <returns>字节数组</returns>
        public static byte[] DoubleToBytes( double value )
        {
            return BitConverter.GetBytes( value );
        }

        /// <summary>
        /// 将bool转为字节数组
        /// </summary>
        /// <param name="value">值</param>
        /// <returns>字节数组</returns>
        public static byte[] BooleanToBytes( bool value )
        {
            return BitConverter.GetBytes( value );
        }

        /// <summary>
        /// 将bool转为字节
        /// </summary>
        /// <param name="value">值</param>
        /// <returns>字节</returns>
        public static byte BooleanToByte( bool value )
        {
            return value ? True : False;
        }

        #endregion

        #region 从字节数组转化

        /// <summary>
        /// 转化为bool值，非零为true，零为false。
        /// </summary>
        /// <param name="value">值</param>
        /// <returns>bool值</returns>
        public static bool ToBoolean( byte value )
        {
            return value != False;
        }

        /// <summary>
        /// 转化为bool值，非零为true，零为false。
        /// </summary>
        /// <param name="value">字节数组</param>
        /// <param name="startIndex">起始下标，递增响应的字节数</param>
        /// <returns>bool值</returns>
        public static bool ToBoolean( byte[] value, ref int startIndex )
        {
            bool ret = BitConverter.ToBoolean( value, startIndex );
            startIndex += ByteCountPerByte;
            return ret;
        }

        /// <summary>
        /// 转化为float值。
        /// </summary>
        /// <param name="value">字节数组</param>
        /// <param name="startIndex">起始下标，递增响应的字节数</param>
        /// <returns>float值</returns>
        public static float ToSingle( byte[] value, ref int startIndex )
        {
            float ret = BitConverter.ToSingle( value, startIndex );
            startIndex += ByteCountPerSingle;
            return ret;
        }

        /// <summary>
        /// 转化为double值。
        /// </summary>
        /// <param name="value">字节数组</param>
        /// <param name="startIndex">起始下标，递增响应的字节数</param>
        /// <returns>double值</returns>
        public static double ToDouble( byte[] value, ref int startIndex )
        {
            double ret = BitConverter.ToDouble( value, startIndex );
            startIndex += ByteCountPerDouble;
            return ret;
        }

        /// <summary>
        /// 转化为short值。
        /// </summary>
        /// <param name="value">字节数组</param>
        /// <param name="startIndex">起始下标，递增响应的字节数</param>
        /// <returns>short值</returns>
        public static short ToInt16( byte[] value, ref int startIndex )
        {
            short ret = BitConverter.ToInt16( value, startIndex );
            startIndex += ByteCountPerInt16;
            return ret;
        }

        /// <summary>
        /// 转化为ushort值。
        /// </summary>
        /// <param name="value">字节数组</param>
        /// <param name="startIndex">起始下标，递增响应的字节数</param>
        /// <returns>ushort值</returns>
        public static ushort ToUInt16( byte[] value, ref int startIndex )
        {
            ushort ret = BitConverter.ToUInt16( value, startIndex );
            startIndex += ByteCountPerInt16;
            return ret;
        }

        /// <summary>
        /// 转化为int值。
        /// </summary>
        /// <param name="value">字节数组</param>
        /// <param name="startIndex">起始下标，递增响应的字节数</param>
        /// <returns>int值</returns>
        public static int ToInt32( byte[] value, ref int startIndex )
        {
            int ret = BitConverter.ToInt32( value, startIndex );
            startIndex += ByteCountPerInt32;
            return ret;
        }

        /// <summary>
        /// 转化为uint值。
        /// </summary>
        /// <param name="value">字节数组</param>
        /// <param name="startIndex">起始下标，递增响应的字节数</param>
        /// <returns>uint值</returns>
        public static uint ToUInt32( byte[] value, ref int startIndex )
        {
            uint ret = BitConverter.ToUInt32( value, startIndex );
            startIndex += ByteCountPerInt32;
            return ret;
        }

        /// <summary>
        /// 转化为long值。
        /// </summary>
        /// <param name="value">字节数组</param>
        /// <param name="startIndex">起始下标，递增响应的字节数</param>
        /// <returns>long值</returns>
        public static long ToInt64( byte[] value, ref int startIndex )
        {
            long ret = BitConverter.ToInt64( value, startIndex );
            startIndex += ByteCountPerInt64;
            return ret;
        }

        /// <summary>
        /// 转化为ulong值。
        /// </summary>
        /// <param name="value">字节数组</param>
        /// <param name="startIndex">起始下标，递增响应的字节数</param>
        /// <returns>ulong值</returns>
        public static ulong ToUInt64( byte[] value, ref int startIndex )
        {
            ulong ret = BitConverter.ToUInt64( value, startIndex );
            startIndex += ByteCountPerInt64;
            return ret;
        }

        #endregion

        #region 字节数组转化为其他数组

        /// <summary>
        /// 把字节数组转化为T类型的数组
        /// </summary>
        /// <typeparam name="T">目标数组类型</typeparam>
        /// <param name="bytes">字节数组</param>
        /// <param name="byteCountPerT">每个目标元素占的字节数</param>
        /// <param name="handler">转换函数</param>
        /// <returns>T类型的数组</returns>
        private static T[] Bytes2Array<T>( byte[] bytes, int byteCountPerT, Func20<byte[], int, T> handler )
            where T : struct
        {
            var ret = new T[bytes.Length / byteCountPerT];

            int byteIndex = 0;
            for( int index = 0; index < ret.Length; index++ )
            {
                ret[index] = handler( bytes, byteIndex );
                byteIndex += byteCountPerT;
            }

            return ret;
        }

        /// <summary>
        /// 将字节数组转换为float数组
        /// </summary>
        /// <param name="bytes">字节数组</param>
        /// <returns>float数组</returns>
        public static double[] Bytes2DoubleArray( byte[] bytes )
        {
            return Bytes2Array<double>( bytes, ByteCountPerDouble, BitConverter.ToDouble );
        }

        /// <summary>
        /// 将字节数组转换为float数组
        /// </summary>
        /// <param name="bytes">字节数组</param>
        /// <returns>float数组</returns>
        public static float[] Bytes2SingleArray( byte[] bytes )
        {
            return Bytes2Array<float>( bytes, ByteCountPerSingle, BitConverter.ToSingle );
        }

        /// <summary>
        /// 将字节数组转换为int数组
        /// </summary>
        /// <param name="bytes">字节数组</param>
        /// <returns>int数组</returns>
        public static int[] Bytes2Int32Array( byte[] bytes )
        {
            return Bytes2Array<int>( bytes, ByteCountPerInt32, BitConverter.ToInt32 );
        }

        /// <summary>
        /// 将字节数组转换为short数组
        /// </summary>
        /// <param name="bytes">字节数组</param>
        /// <returns>short数组</returns>
        public static short[] Bytes2Int16Array( byte[] bytes )
        {
            return Bytes2Array<short>( bytes, ByteCountPerInt16, BitConverter.ToInt16 );
        }

        #endregion

        #region 其他数组转化为字节数组

        /// <summary>
        /// 其他数组转化为字节数组
        /// </summary>
        /// <typeparam name="T">数组类型</typeparam>
        /// <param name="datas">数组</param>
        /// <param name="byteCountPerT">每个数组元素占的字节数</param>
        /// <param name="handler">转换函数</param>
        /// <returns>字节数组</returns>
        private static byte[] Array2Bytes<T>( IList<T> datas, int byteCountPerT, Func20<T, byte[]> handler )
            where T : struct
        {
            var ret = new byte[datas.Count * byteCountPerT];
            for( int index = 0, byteIndex = 0;
                 index < datas.Count;
                 index++, byteIndex += byteCountPerT )
            {
                handler( datas[index] ).CopyTo( ret, byteIndex );
            }

            return ret;
        }

        /// <summary>
        /// 将double[]转换为byte[]
        /// </summary>
        /// <param name="datas">double[]</param>
        /// <returns>byte[]</returns>
        public static byte[] DoublesToBytes( double[] datas )
        {
            return Array2Bytes( datas, ByteCountPerDouble, BitConverter.GetBytes );
        }

        /// <summary>
        /// 将float[]转换为byte[]
        /// </summary>
        /// <param name="datas">float[]</param>
        /// <returns>byte[]</returns>
        public static byte[] SinglesToBytes( float[] datas )
        {
            return Array2Bytes( datas, ByteCountPerSingle, BitConverter.GetBytes );
        }

        /// <summary>
        /// 将int[]转换为byte[]
        /// </summary>
        /// <param name="datas">int[]</param>
        /// <returns>byte[]</returns>
        public static byte[] Int32sToBytes( int[] datas )
        {
            return Array2Bytes( datas, ByteCountPerInt32, BitConverter.GetBytes );
        }

        /// <summary>
        /// 将short[]转换为byte[]
        /// </summary>
        /// <param name="datas">short[]</param>
        /// <returns>byte[]</returns>
        public static byte[] Int16sToBytes( short[] datas )
        {
            return Array2Bytes( datas, ByteCountPerInt16, BitConverter.GetBytes );
        }

        #endregion

        #region 将位标志数组转化为字节(数组)

        /// <summary>
        /// 将位标志数组转化为字节
        /// </summary>
        /// <param name="bitFlags">位标志数组</param>
        /// <returns>字节</returns>
        public static byte BitFlagsToByte( bool[] bitFlags )
        {
            return BitFlagsToByte( bitFlags, 0, bitFlags.Length );
        }

        /// <summary>
        /// 将位标志数组转化为字节
        /// </summary>
        /// <param name="bitFlags">位标志数组</param>
        /// <param name="offset">位标志数组偏移</param>
        /// <param name="size">位标志数组个数</param>
        /// <returns>字节</returns>
        public static byte BitFlagsToByte( bool[] bitFlags, int offset, int size )
        {
            if( bitFlags == null || bitFlags.Length == 0 )
            {
                throw new ArgumentNullException( "bitFlags" );
            }

            if( offset < 0 || ( bitFlags.Length - 1 < offset ) )
            {
                throw new ArgumentOutOfRangeException( string.Format( "length:{0},offset:{1}", bitFlags.Length, offset ) );
            }

            if( size < 0 || BitCountPerByte < size || ( bitFlags.Length < offset + size ) )
            {
                throw new ArgumentOutOfRangeException( string.Format( "length:{0},offset:{1},size:{2}", bitFlags.Length,
                                                                      offset, size ) );
            }

            byte ret = 0;
            for( int index = 0; index < size; index++ )
            {
                // 左移一位，第一次因为是0，移位后还是0，所以共移位次数：size-1
                ret <<= 1;
                if( bitFlags[offset + index] )
                {
                    // 将最低位设置为1
                    ret |= 1;
                }
            }

            return ret;
        }

        /// <summary>
        /// 将位标志数组转化为字节数组
        /// </summary>
        /// <param name="bitFlags">位标志数组</param>
        /// <returns>字节数组</returns>
        public static byte[] BitFlagsToBytes( bool[] bitFlags )
        {
            if( bitFlags == null || bitFlags.Length == 0 )
            {
                throw new ArgumentNullException( "bitFlags" );
            }

            int remainder = bitFlags.Length % BitCountPerByte;
            bool hasRemainder = 0 < remainder;

            int byteCount = bitFlags.Length / BitCountPerByte;
            if( hasRemainder )
            {
                byteCount += 1;
            }

            int retStartIndex = 0;
            var ret = new byte[byteCount];
            if( hasRemainder )
            {
                ret[0] = BitFlagsToByte( bitFlags, 0, remainder );
                retStartIndex += 1;
            }

            for( int index = retStartIndex, flagsIndex = remainder;
                 index < ret.Length;
                 index++, flagsIndex += BitCountPerByte )
            {
                ret[index] = BitFlagsToByte( bitFlags, flagsIndex, BitCountPerByte );
            }

            return ret;
        }

        #endregion

        #region 组合字节

        /// <summary>
        /// 向左移位，对使用0掩码，然后用或组合
        /// </summary>
        /// <param name="leftByte">向左移位的字节</param>
        /// <param name="rightByte">不移位的字节</param>
        /// <param name="shiftLeftCount">向左移位的次数</param>
        /// <returns>组合后的字节</returns>
        public static byte ShiftLeft_Mask_Combine( byte leftByte, byte rightByte, byte shiftLeftCount )
        {
            if( shiftLeftCount < 1 || 7 < shiftLeftCount )
            {
                throw new ArgumentOutOfRangeException( "shiftLeftCount:" + shiftLeftCount );
            }

            var rightMask = (byte)( ~( byte.MaxValue << shiftLeftCount ) );
            return (byte)( ( leftByte << shiftLeftCount ) | ( rightMask & rightByte ) );
        }

        #endregion

        #region 大端、小端相关

        #region HostToNetworkOrder

        /// <summary>
        /// 从当前计算机字节序变为网络(大端)字节序
        /// </summary>
        /// <param name="value">初始值</param>
        /// <returns>改变后的值</returns>
        public static short HostToNetworkOrder( short value )
        {
            return IPAddress.HostToNetworkOrder( value );
        }

        /// <summary>
        /// 从当前计算机字节序变为网络(大端)字节序
        /// </summary>
        /// <param name="value">初始值</param>
        /// <returns>改变后的值</returns>
        public static int HostToNetworkOrder( int value )
        {
            return IPAddress.HostToNetworkOrder( value );
        }

        /// <summary>
        /// 从当前计算机字节序变为网络(大端)字节序
        /// </summary>
        /// <param name="value">初始值</param>
        /// <returns>改变后的值</returns>
        public static long HostToNetworkOrder( long value )
        {
            return IPAddress.HostToNetworkOrder( value );
        }

        #endregion

        #region NetworkToHostOrder

        /// <summary>
        /// 从网络(大端)字节序变为当前计算机字节序
        /// </summary>
        /// <param name="value">初始值</param>
        /// <returns>改变后的值</returns>
        public static short NetworkToHostOrder( short value )
        {
            return IPAddress.NetworkToHostOrder( value );
        }

        /// <summary>
        /// 从网络(大端)字节序变为当前计算机字节序
        /// </summary>
        /// <param name="value">初始值</param>
        /// <returns>改变后的值</returns>
        public static int NetworkToHostOrder( int value )
        {
            return IPAddress.NetworkToHostOrder( value );
        }

        /// <summary>
        /// 从网络(大端)字节序变为当前计算机字节序
        /// </summary>
        /// <param name="value">初始值</param>
        /// <returns>改变后的值</returns>
        public static long NetworkToHostOrder( long value )
        {
            return IPAddress.NetworkToHostOrder( value );
        }

        #endregion

        #region HostToLittleEndian

        /// <summary>
        /// 从当前计算机字节序变为小端字节序
        /// </summary>
        /// <param name="value">初始值</param>
        /// <returns>改变后的值</returns>
        public static short HostToLittleEndian( short value )
        {
            return BitConverter.IsLittleEndian ? value : SwapEndian( value );
        }

        /// <summary>
        /// 从当前计算机字节序变为小端字节序
        /// </summary>
        /// <param name="value">初始值</param>
        /// <returns>改变后的值</returns>
        public static int HostToLittleEndian( int value )
        {
            return BitConverter.IsLittleEndian ? value : SwapEndian( value );
        }

        /// <summary>
        /// 从当前计算机字节序变为小端字节序
        /// </summary>
        /// <param name="value">初始值</param>
        /// <returns>改变后的值</returns>
        public static long HostToLittleEndian( long value )
        {
            return BitConverter.IsLittleEndian ? value : SwapEndian( value );
        }

        #endregion

        #region HostToBigEndian

        /// <summary>
        /// 从当前计算机字节序变为大端字节序
        /// </summary>
        /// <param name="value">初始值</param>
        /// <returns>改变后的值</returns>
        public static short HostToBigEndian( short value )
        {
            return BitConverter.IsLittleEndian ? HostToNetworkOrder( value ) : value;
        }

        /// <summary>
        /// 从当前计算机字节序变为大端字节序
        /// </summary>
        /// <param name="value">初始值</param>
        /// <returns>改变后的值</returns>
        public static int HostToBigEndian( int value )
        {
            return BitConverter.IsLittleEndian ? HostToNetworkOrder( value ) : value;
        }

        /// <summary>
        /// 从当前计算机字节序变为大端字节序
        /// </summary>
        /// <param name="value">初始值</param>
        /// <returns>改变后的值</returns>
        public static long HostToBigEndian( long value )
        {
            return BitConverter.IsLittleEndian ? HostToNetworkOrder( value ) : value;
        }

        #endregion

        #region LittleEndianToHost

        /// <summary>
        /// 从小端字节序变为当前计算机字节序
        /// </summary>
        /// <param name="value">初始值</param>
        /// <returns>改变后的值</returns>
        public static short LittleEndianToHost( short value )
        {
            return BitConverter.IsLittleEndian ? value : SwapEndian( value );
        }

        /// <summary>
        /// 从小端字节序变为当前计算机字节序
        /// </summary>
        /// <param name="value">初始值</param>
        /// <returns>改变后的值</returns>
        public static int LittleEndianToHost( int value )
        {
            return BitConverter.IsLittleEndian ? value : SwapEndian( value );
        }

        /// <summary>
        /// 从小端字节序变为当前计算机字节序
        /// </summary>
        /// <param name="value">初始值</param>
        /// <returns>改变后的值</returns>
        public static long LittleEndianToHost( long value )
        {
            return BitConverter.IsLittleEndian ? value : SwapEndian( value );
        }

        #endregion

        #region BigEndianToHost

        /// <summary>
        /// 从大端字节序变为当前计算机字节序
        /// </summary>
        /// <param name="value">初始值</param>
        /// <returns>改变后的值</returns>
        public static short BigEndianToHost( short value )
        {
            return BitConverter.IsLittleEndian ? NetworkToHostOrder( value ) : value;
        }

        /// <summary>
        /// 从大端字节序变为当前计算机字节序
        /// </summary>
        /// <param name="value">初始值</param>
        /// <returns>改变后的值</returns>
        public static int BigEndianToHost( int value )
        {
            return BitConverter.IsLittleEndian ? NetworkToHostOrder( value ) : value;
        }

        /// <summary>
        /// 从大端字节序变为当前计算机字节序
        /// </summary>
        /// <param name="value">初始值</param>
        /// <returns>改变后的值</returns>
        public static long BigEndianToHost( long value )
        {
            return BitConverter.IsLittleEndian ? NetworkToHostOrder( value ) : value;
        }

        #endregion

        #region SwapEndian

        /// <summary>
        /// 通过将输入值的字节数组反转调整字节序
        /// </summary>
        /// <param name="value">初始值</param>
        /// <returns>改变后的值</returns>
        public static short SwapEndian( short value )
        {
            byte[] bytes = BitConverter.GetBytes( value );
            Array.Reverse( bytes );
            return BitConverter.ToInt16( bytes, 0 );
        }

        /// <summary>
        /// 通过将输入值的字节数组反转调整字节序
        /// </summary>
        /// <param name="value">初始值</param>
        /// <returns>改变后的值</returns>
        public static int SwapEndian( int value )
        {
            byte[] bytes = BitConverter.GetBytes( value );
            Array.Reverse( bytes );
            return BitConverter.ToInt32( bytes, 0 );
        }

        /// <summary>
        /// 通过将输入值的字节数组反转调整字节序
        /// </summary>
        /// <param name="value">初始值</param>
        /// <returns>改变后的值</returns>
        public static long SwapEndian( long value )
        {
            byte[] bytes = BitConverter.GetBytes( value );
            Array.Reverse( bytes );
            return BitConverter.ToInt64( bytes, 0 );
        }

        #endregion

        #endregion
    }
}