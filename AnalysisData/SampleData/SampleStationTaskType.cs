using System;

namespace AnalysisData.SampleData
{
    /// <summary>
    ///     采集工作站任务类型
    /// </summary>
    [Serializable]
    public enum SampleStationTaskType
    {
        /// <summary>
        ///     报警灯复位
        /// </summary>
        ResetAlmLight,

        /// <summary>
        ///     获取采集器的调试信息
        /// </summary>
        GetHardwareDebugInfo,

        /// <summary>
        ///     上传数据，目前支持：从OPC服务器读取的TrendData、。
        /// </summary>
        UploadData,
    }

    /// <summary>
    ///     采集工作站任务类
    /// </summary>
    [Serializable]
    public class SampleStationTask
    {
        /// <summary>
        ///     采集工作站数据
        /// </summary>
        public SampleStationData SampleStationData { get; set; }

        /// <summary>
        ///     任务类型
        /// </summary>
        public SampleStationTaskType TaskType { get; set; }

        /// <summary>
        ///     输入参数对应的字节数组
        /// </summary>
        public byte[] InputBytes { get; set; }
    }
}