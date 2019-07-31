namespace AnalysisData.SampleData
{
    /// <summary>
    ///     文件片段数据
    /// </summary>
    public class FileSegmentData
    {
        /// <summary>
        ///     参数，参数之间以半角逗号分隔，只有一个参数时，末尾没有逗号。
        ///     目前的参数是：文件名。
        /// </summary>
        public string Parameters { get; set; }

        /// <summary>
        ///     文件长度(以字节为单位)
        /// </summary>
        public int FileLength { get; set; }

        /// <summary>
        ///     片段下标(从0开始)
        /// </summary>
        public int SegmentIndex { get; set; }

        /// <summary>
        ///     片段偏移(以字节为单位，从0开始)
        /// </summary>
        public int SegmentOffset { get; set; }

        /// <summary>
        ///     片段数据
        /// </summary>
        public byte[] SegmentData { get; set; }
    }
}