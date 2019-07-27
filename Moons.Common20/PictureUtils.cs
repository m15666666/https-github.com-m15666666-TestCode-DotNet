

namespace Moons.Common20
{
    /// <summary>
    /// 图片的实用工具类
    /// <ref>http://www.cnblogs.com/wendy_yu/archive/2011/12/27/2303118.html</ref>
    /// </summary>
    public static class PictureUtils
    {
        #region 图片编码

        /// <summary>
        /// BMP图片的特征字节数组
        /// </summary>
        private static readonly byte[] BmpHeader = new byte[] {0x42, 0x4D};

        /// <summary>
        /// JPG图片的特征字节数组
        /// </summary>
        private static readonly byte[] Jpeg = new byte[] {0xff, 0xd8}; //endwith 0xff, 0xd9

        /// <summary>
        /// GIF图片的特征字节数组
        /// </summary>
        private static readonly byte[] Gif87Header = new byte[] {0x47, 0x49, 0x46, 0x38, 0x39, 0x61};

        /// <summary>
        /// GIF图片的特征字节数组
        /// </summary>
        private static readonly byte[] Gif89Header = new byte[] {0x47, 0x49, 0x46, 0x38, 0x37, 0x61};

        /// <summary>
        /// Png图片的特征字节数组
        /// </summary>
        private static readonly byte[] PngHeader = new byte[] {0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A};

        /// <summary>
        /// ICO图片的特征字节数组
        /// </summary>
        private static readonly byte[] IcoHeader = new byte[] {0x00, 0x00, 0x01, 0x00, 0x01, 0x00, 0x20, 0x20};

        /// <summary>
        /// Tiff图片的特征字节数组
        /// </summary>
        private static readonly byte[] TiffMotoHeader = new byte[] {0x4D, 0x4D};

        /// <summary>
        /// Tiff图片的特征字节数组
        /// </summary>
        private static readonly byte[] TiffIntelHeader = new byte[] {0x49, 0x49};

        /// <summary>
        /// Pcx图片的特征字节数组
        /// </summary>
        private static readonly byte[] PcxHeader = new byte[] {0x0A};

        /// <summary>
        /// CUR图片的特征字节数组
        /// </summary>
        private static readonly byte[] CurHeader = new byte[] {0x00, 0x00, 0x02, 0x00, 0x01, 0x00, 0x20, 0x20};

        /// <summary>
        /// IFF图片的特征字节数组
        /// </summary>
        private static readonly byte[] IffHeader = new byte[] {0x46, 0x4F, 0x52, 0x4D};

        /// <summary>
        /// ANI图片的特征字节数组
        /// </summary>
        private static readonly byte[] AniHeader = new byte[] {0x52, 0x49, 0x46, 0x46};

        /// <summary>
        /// TGA未压缩的前5字节
        /// </summary>
        private static readonly byte[] TgaNormalHeader = new byte[] {0x00, 0x00, 0x02, 0x00, 0x00};

        /// <summary>
        /// TGA RLE压缩的前5字节
        /// </summary>
        private static readonly byte[] TgaRleHeader = new byte[] {0x00, 0x00, 0x10, 0x00, 0x00};

        #endregion

        #region 是否是图片

        /// <summary>
        /// 是否是GIF图片
        /// </summary>
        /// <param name="source">图片的字节数组</param>
        /// <returns>是否是GIF图片</returns>
        public static bool IsGif( byte[] source )
        {
            if( source == null || source.Length < Gif87Header.Length )
            {
                return false;
            }

            return IsStartsWith(source, Gif89Header) || IsStartsWith( source, Gif87Header );
        }

        /// <summary>
        /// 是否Png格式图片
        /// </summary>
        /// <param name="source">图片的字节数组</param>
        /// <returns>是否Png格式图片</returns>
        public static bool IsPng(byte[] source)
        {
            if (!(PreCheck(source)))
            {
                return false;
            }

            return IsStartsWith(source, PngHeader);
        }

        /// <summary>
        /// 是否是JPG图片
        /// </summary>
        /// <param name="source">图片的字节数组</param>
        /// <returns>是否是JPG图片</returns>
        public static bool IsJpeg(byte[] source)
        {
            if (source == null || source.Length < 10)
            {
                return false;
            }

            return IsStartsWith( source, Jpeg );
        }

        /// <summary>
        /// 是否是Bmp图片
        /// </summary>
        /// <param name="source">图片的字节数组</param>
        /// <returns>是否是Bmp图片</returns>
        public static bool IsBmp(byte[] source)
        {
            if (!(PreCheck(source)))
            {
                return false;
            }

            return IsStartsWith(source, BmpHeader);
        }

        /// <summary>
        /// 是否是TGA图片
        /// </summary>
        /// <param name="source">TGA图片的字节数组</param>
        /// <returns>是否是TGA图片</returns>
        public static bool IsTGA(byte[] source)
        {
            if (!(PreCheck(source)))
            {
                return false;
            }

            return IsStartsWith(source, TgaNormalHeader) || IsStartsWith( source, TgaRleHeader );
        }

        /// <summary>
        /// 是否是PCX图片
        /// </summary>
        /// <param name="source">PCX图片的字节数组</param>
        /// <returns>是否是PCX图片</returns>
        public static bool IsPCX(byte[] source)
        {
            if (!(PreCheck(source)))
            {
                return false;
            }

            return IsStartsWith(source, PcxHeader);
        }

        /// <summary>
        /// 是否是Tiff图片
        /// </summary>
        /// <param name="source">Tiff图片的字节数组</param>
        /// <returns>是否是Tiff图片</returns>
        public static bool IsTIFF(byte[] source)
        {
            if (!(PreCheck(source)))
            {
                return false;
            }

            return IsStartsWith(source, TiffIntelHeader) || IsStartsWith(source, TiffMotoHeader);
        }

        /// <summary>
        /// 是否是Ico图片
        /// </summary>
        /// <param name="source">Ico图片的字节数组</param>
        /// <returns>是否是Ico图片</returns>
        public static bool IsICO(byte[] source)
        {
            if (!(PreCheck(source)))
            {
                return false;
            }

            return IsStartsWith(source, IcoHeader);
        }

        /// <summary>
        /// 是否是Cur图片
        /// </summary>
        /// <param name="source">Cur图片的字节数组</param>
        /// <returns>是否是Cur图片</returns>
        public static bool IsCUR(byte[] source)
        {
            if (!(PreCheck(source)))
            {
                return false;
            }

            return IsStartsWith(source, CurHeader);
        }

        /// <summary>
        /// 是否是ANI图片
        /// </summary>
        /// <param name="source">ANI图片的字节数组</param>
        /// <returns>是否是ANI图片</returns>
        public static bool IsANI(byte[] source)
        {
            if (!(PreCheck(source)))
            {
                return false;
            }

            return IsStartsWith(source, AniHeader);
        }

        /// <summary>
        /// 是否是Iff图片
        /// </summary>
        /// <param name="source">Iff图片的字节数组</param>
        /// <returns>是否是Iff图片</returns>
        public static bool IsIff(byte[] source)
        {
            if (!(PreCheck(source)))
            {
                return false;
            }

            return IsStartsWith(source, IffHeader);
        }

        /// <summary>
        /// 检查是否满足格式规范
        /// </summary>
        /// <param name="source">>图片的字节数组</param>
        private static bool PreCheck(byte[] source)
        {
            return source != null && source.Length >= 2;
        }

        #endregion

        #region 辅助函数

        /// <summary>
        /// 比较字节数组是否以某一组字节起始
        /// </summary>
        /// <param name="thisBytes">被比较的字节</param>
        /// <param name="thatBytes">作为参照的字节</param>
        /// <returns>匹配返回 True，否则返回 False</returns>
        private static bool IsStartsWith(byte[] thisBytes, byte[] thatBytes)
        {
            for (var i = 0; i < thatBytes.Length; i += 1)
            {
                if (thisBytes[i] != thatBytes[i])
                {
                    return false;
                }
            }

            return true;
        }

        #endregion

    }
}