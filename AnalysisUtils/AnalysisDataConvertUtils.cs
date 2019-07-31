using AnalysisData.Helper;
using Moons.Common20;

namespace AnalysisUtils
{
    /// <summary>
    /// 历史数据转换的实用工具类
    /// </summary>
    public static class AnalysisDataConvertUtils
    {
        #region 变量和属性

        /// <summary>
        /// 压缩数据对象
        /// </summary>
        private static readonly ZipCompressionUtils _zipCompressionUtils = ZipCompressionUtils.Create();

        #endregion

        #region 压缩相关

        /// <summary>
        /// 压缩字节数组
        /// </summary>
        /// <param name="bytes">初始的字节数组</param>
        /// <param name="compressionType">压缩类型，输出</param>
        /// <returns>压缩后的字节数组</returns>
        private static byte[] Compress( byte[] bytes, out CompressionType compressionType )
        {
            compressionType = _zipCompressionUtils.CompressionProvider;

            return _zipCompressionUtils.Compress( bytes );
        }

        /// <summary>
        /// 解压缩
        /// </summary>
        /// <param name="bytes">字节数组</param>
        /// <param name="compressionType">压缩类型</param>
        /// <returns>解压缩的字节数组</returns>
        private static byte[] DeCompress( byte[] bytes, CompressionType compressionType )
        {
            switch( compressionType )
            {
                case CompressionType.GZip:
                    return _zipCompressionUtils.DeCompress( bytes );

                    //case CompressionType.NoCompress: // do nothing
                default:
                    return bytes;
            }
        }

        /// <summary>
        /// 获得CompressionType
        /// </summary>
        /// <param name="compressionType">byte?</param>
        /// <returns>CompressionType</returns>
        private static CompressionType GetCompressionType( byte? compressionType )
        {
            return compressionType.HasValue
                       ? (CompressionType)compressionType
                       : CompressionType.NoCompress;
        }

        #endregion

        #region 波形数据转换

        #region 解压波形

        /// <summary>
        /// 将字节数组转换为double数组
        /// </summary>
        /// <param name="bytes">字节数组</param>
        /// <param name="compressionType">压缩类型</param>
        /// <returns>double数组</returns>
        private static double[] Byte2Double( byte[] bytes, CompressionType compressionType )
        {
            return ByteUtils.Bytes2DoubleArray( DeCompress( bytes, compressionType ) );
        }

        /// <summary>
        /// 将字节数组转换为double数组
        /// </summary>
        /// <param name="bytes">字节数组</param>
        /// <param name="waveScale">波形比例因子</param>
        /// <param name="compressionType">压缩类型</param>
        /// <returns>double数组</returns>
        public static double[] Byte2Double( byte[] bytes, double? waveScale, byte? compressionType )
        {
            CompressionType type = GetCompressionType( compressionType );

            // 字段内部保存的是double数组
            if( waveScale == null )
            {
                return Byte2Double( bytes, type );
            }

            // 字段内部保存的是未压缩的float数组
            if( type == CompressionType.NoCompressSingle )
            {
                return ArrayUtils.Single2Double( ByteUtils.Bytes2SingleArray( bytes ) );
            }

            // 字段内部保存的是short数组
            return CompressDataUtils.ShortWave2Double(
                ByteUtils.Bytes2Int16Array( DeCompress( bytes, type ) ), waveScale.Value );
        }

        #endregion

        #region 压缩波形

        /// <summary>
        /// 将double[]转换为byte[]，目前用于502等离线采集仪
        /// </summary>
        /// <param name="wave">double[]</param>
        /// <param name="compressionType">压缩类型，输出</param>
        /// <returns>byte[]</returns>
        public static byte[] To502Bytes( double[] wave, out CompressionType compressionType )
        {
            compressionType = CompressionType.NoCompressSingle;
            float[] singleArray = ArrayUtils.Double2Single( wave );
            return ByteUtils.SinglesToBytes( singleArray );
        }

        /// <summary>
        /// 将double[]转换为byte[]
        /// </summary>
        /// <param name="wave">>double[]</param>
        /// <param name="waveScale">波形比例因子，输出</param>
        /// <param name="compressionType">压缩类型，输出</param>
        /// <returns>byte[]</returns>
        public static byte[] DoubleToByte( double[] wave, out double waveScale, out CompressionType compressionType )
        {
            short[] shortWave;
            CompressDataUtils.DoubleWave2Short( wave, out shortWave, out waveScale );

            byte[] bytes = ByteUtils.Int16sToBytes( shortWave );
            byte[] compressBytes = Compress( bytes, out compressionType );

            // 如果压缩后，字节数组的长度的确变小了，则返回压缩后的数组
            if( compressBytes.Length < bytes.Length )
            {
                return compressBytes;
            }

            // 否则返回未压缩的数组，并将压缩类型设置为CompressionType.NoCompress
            compressionType = CompressionType.NoCompress;
            return bytes;
        }

        #endregion

        #endregion
    }
}