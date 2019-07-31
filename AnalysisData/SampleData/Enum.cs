namespace AnalysisData.SampleData
{
    /// <summary>
    /// 采集工作站的状态
    /// </summary>
    public enum SampleStationState
    {
        /// <summary>
        /// 停止
        /// </summary>
        Stop = 0,

        /// <summary>
        /// 正常采集
        /// </summary>
        NormalSample = 1,

        /// <summary>
        /// 起停机采集
        /// </summary>
        StartStopSample = 2,

        /// <summary>
        /// 上传追忆文件
        /// </summary>
        UploadRetrospectFile = 3,

        /// <summary>
        /// 上传启停机文件
        /// </summary>
        UploadStartStopFile = 4,

        /// <summary>
        /// 正在启动中
        /// </summary>
        Booting = 101,

        /// <summary>
        /// 正在(固件)升级中
        /// </summary>
        Upgrading = 111,
    }

    /// <summary>
    /// 数据采集器的状态
    /// </summary>
    public enum DataSamplerState
    {
        /// <summary>
        /// 停止
        /// </summary>
        Stop,

        /// <summary>
        /// 正常采集
        /// </summary>
        NormalSample,

        /// <summary>
        /// 起停机采集
        /// </summary>
        StartStopSample,

        /// <summary>
        /// 上传追忆文件
        /// </summary>
        UploadRetrospectFile,

        /// <summary>
        /// 上传启停机文件
        /// </summary>
        UploadStartStopFile,
    }
}