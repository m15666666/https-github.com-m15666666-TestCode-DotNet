namespace AnalysisData.SampleData
{
    /// <summary>
    ///     固件文件信息数据类
    /// </summary>
    public class FirmwareFileInfoData
    {
        /// <summary>
        ///     固件文件名
        /// </summary>
        public string FirmwareFileName { get; set; }

        /// <summary>
        ///     固件文件版本号
        /// </summary>
        public string FirmwareFileVersion { get; set; }

        /// <summary>
        ///     固件文件路径
        /// </summary>
        public string FirmwareFilePath { get; set; }

        /// <summary>
        ///     固件文件字节数组
        /// </summary>
        public byte[] FirmwareFileBytes { get; set; }
    }
}