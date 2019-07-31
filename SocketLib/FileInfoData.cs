using System;

namespace SocketLib
{
    /// <summary>
    /// 文件信息类
    /// </summary>
    [Serializable]
    public class FileInfoData
    {
        /// <summary>
        /// 文件长度
        /// </summary>
        public long Length { get; set; }
    }
}