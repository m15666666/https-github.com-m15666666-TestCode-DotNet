using System;
using System.IO;
using System.Text;
using ICSharpCode.SharpZipLib.BZip2;
using ICSharpCode.SharpZipLib.GZip;
using ICSharpCode.SharpZipLib.Zip;

namespace Moons.Common20
{
    /// <summary>
    /// 使用 SharpZipLib 进行压缩的辅助类，简化压缩字节数组和字符串的操作。
    /// </summary>
    public class ZipCompressionUtils
    {
        #region 变量和属性

        /// <summary>
        /// 压缩供应者，默认为 GZip。
        /// </summary>
        public CompressionType CompressionProvider { get; set; }

        #endregion

        #region 创建对象

        private ZipCompressionUtils()
        {
            CompressionProvider = CompressionType.GZip;
        }

        public static ZipCompressionUtils Create()
        {
            return Create( CompressionType.GZip );
        }

        public static ZipCompressionUtils Create( CompressionType compressionType )
        {
            return new ZipCompressionUtils { CompressionProvider = compressionType };
        }

        #endregion

        #region Public methods

        /// <summary>
        /// 从原始字节数组生成已压缩的字节数组。
        /// </summary>
        /// <param name="bytesToCompress">原始字节数组。</param>
        /// <returns>返回已压缩的字节数组</returns>
        public byte[] Compress( byte[] bytesToCompress )
        {
            using( var outStream = new MemoryStream() )
            {
                using( Stream compressStream = OutputStream( outStream ) )
                {
                    compressStream.Write( bytesToCompress, 0, bytesToCompress.Length );
                }

                return outStream.ToArray();
            }
        }

        /// <summary>
        /// 从原始字符串生成已压缩的字符串。
        /// </summary>
        /// <param name="stringToCompress">原始字符串。</param>
        /// <returns>返回已压缩的字符串。</returns>
        public string Compress( string stringToCompress )
        {
            return Convert.ToBase64String( CompressToByte( stringToCompress ) );
        }

        /// <summary>
        /// 从原始字符串生成已压缩的字节数组。
        /// </summary>
        /// <param name="stringToCompress">原始字符串。</param>
        /// <returns>返回已压缩的字节数组。</returns>
        public byte[] CompressToByte( string stringToCompress )
        {
            return Compress( Encoding.Unicode.GetBytes( stringToCompress ) );
        }

        /// <summary>
        /// 从已压缩的字符串生成原始字符串。
        /// </summary>
        /// <param name="stringToDecompress">已压缩的字符串。</param>
        /// <returns>返回原始字符串。</returns>
        public string DeCompress( string stringToDecompress )
        {
            if( stringToDecompress == null )
            {
                throw new ArgumentNullException( "stringToDecompress", "You tried to use an empty string" );
            }

            try
            {
                byte[] inArr = Convert.FromBase64String( stringToDecompress.Trim() );
                return Encoding.Unicode.GetString( DeCompress( inArr ) );
            }
            catch( NullReferenceException ex )
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// 从已压缩的字节数组生成原始字节数组。
        /// </summary>
        /// <param name="bytesToDecompress">已压缩的字节数组。</param>
        /// <returns>返回原始字节数组。</returns>
        public byte[] DeCompress( byte[] bytesToDecompress )
        {
            using( var outStream = new MemoryStream() )
            {
                using( Stream inStream = InputStream( new MemoryStream( bytesToDecompress ) ) )
                {
                    var writeData = new byte[4096];
                    while( true )
                    {
                        int size = inStream.Read( writeData, 0, writeData.Length );
                        if( size == 0 )
                        {
                            break;
                        }

                        outStream.Write( writeData, 0, size );
                    }
                }

                return outStream.ToArray();
            }
        }

        #endregion

        #region Private methods

        /// <summary>
        /// 从给定的流生成压缩输出流。
        /// </summary>
        /// <param name="inputStream">原始流。</param>
        /// <returns>返回压缩输出流。</returns>
        private Stream OutputStream( Stream inputStream )
        {
            switch( CompressionProvider )
            {
                case CompressionType.BZip2:
                    return new BZip2OutputStream( inputStream );

                case CompressionType.GZip:
                    return new GZipOutputStream( inputStream );

                case CompressionType.Zip:
                    return new ZipOutputStream( inputStream );

                default:
                    return new GZipOutputStream( inputStream );
            }
        }

        /// <summary>
        /// 从给定的流生成压缩输入流。
        /// </summary>
        /// <param name="inputStream">原始流。</param>
        /// <returns>返回压缩输入流。</returns>
        private Stream InputStream( Stream inputStream )
        {
            switch( CompressionProvider )
            {
                case CompressionType.BZip2:
                    return new BZip2InputStream( inputStream );

                case CompressionType.GZip:
                    return new GZipInputStream( inputStream );

                case CompressionType.Zip:
                    return new ZipInputStream( inputStream );

                default:
                    return new GZipInputStream( inputStream );
            }
        }

        #endregion
    }

    /// <summary>
    /// 压缩方式。
    /// </summary>    
    public enum CompressionType
    {
        /// <summary>
        /// 不压缩
        /// </summary>
        NoCompress = 0,

        /// <summary>
        /// GZip
        /// </summary>
        GZip = 1,

        /// <summary>
        /// BZip
        /// </summary>
        BZip2 = 2,

        /// <summary>
        /// Zip
        /// </summary>
        Zip = 3,

        /// <summary>
        /// 不压缩，字节数组为单精度浮点型（4个字节）数组
        /// </summary>
        NoCompressSingle = 101,
    }
}