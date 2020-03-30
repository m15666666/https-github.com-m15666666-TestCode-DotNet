using System;
using System.Collections.Generic;
using System.Text;

namespace Moons.Plugin.Infra
{
    /// <summary>
    /// 从代码、dll等加载程序集的dto对象
    /// </summary>
    public class LoadAssemblyDto
    {
        /// <summary>
        /// 加载程序集的方式
        /// </summary>
        public LoadAssemblyMethodType LoadAssemblyMethodType { get; set; }

        /// <summary>
        /// 源代码
        /// </summary>
        public List<string> SourceCodes { get; set; }

        /// <summary>
        /// dll的字节数组
        /// </summary>
        public byte[] DLLBytes { get; set; }

        /// <summary>
        /// dll的字节数组的base64字符串
        /// </summary>
        public string DLLBytesBase64 { get; set; }

        /// <summary>
        /// dll的文件路径
        /// </summary>
        public string DLLFilePath { get; set; }
    }

    /// <summary>
    /// 加载程序集的方式
    /// </summary>
    public enum LoadAssemblyMethodType
    {
        /// <summary>
        /// 源代码
        /// </summary>
        SourceCodes = 1,

        /// <summary>
        /// dll的字节数组
        /// </summary>
        DLLBytes,

        /// <summary>
        /// dll的字节数组的base64字符串
        /// </summary>
        DLLBytesBase64,

        /// <summary>
        /// dll的文件路径
        /// </summary>
        DLLFilePath,
    }
}
