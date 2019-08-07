using Moons.Common20.Serialization;

namespace AnalysisData.ToFromBytes
{
    /// <summary>
    ///     命令ID
    /// </summary>
    public static class CommandID
    {
        /// <summary>
        ///     命令ID的起始
        /// </summary>
        private const int CommandIDBase = CommandIDs.CommandIDBase;

        /// <summary>
        ///     开始正常采集
        /// </summary>
        public const int StartNormalSample = CommandIDBase + 1;

        /// <summary>
        ///     开始启停机采集
        /// </summary>
        public const int StartStartStopSample = CommandIDBase + 2;

        /// <summary>
        ///     停止采集
        /// </summary>
        public const int StopSample = CommandIDBase + 20;

        /// <summary>
        ///     下载采集配置文件
        /// </summary>
        public const int DownloadSampleConfig = CommandIDBase + 30;

        /// <summary>
        ///     获取采集器的状态
        /// </summary>
        public const int GetStatus = CommandIDBase + 40;

        /// <summary>
        ///     获取采集器的硬件信息
        /// </summary>
        public const int GetHardwareInfo = CommandIDBase + 41;

        /// <summary>
        ///     采集器校时
        /// </summary>
        public const int Timing = CommandIDBase + 42;

        /// <summary>
        ///     获取采集器的调试信息
        /// </summary>
        public const int GetHardwareDebugInfo = CommandIDBase + 43;

        /// <summary>
        ///     上传追忆文件
        /// </summary>
        public const int UploadRetrospectFile = CommandIDBase + 50;

        /// <summary>
        ///     上传启停机文件
        /// </summary>
        public const int UploadStartStopFile = CommandIDBase + 51;

        /// <summary>
        ///     上传报警数据文件
        /// </summary>
        public const int UploadAlmDataFile = CommandIDBase + 55;

        /// <summary>
        ///     上传趋势数据，包括：趋势数据（温度、电流等没有波形测点数据）。
        /// </summary>
        public const int UploadTrendData = CommandIDBase + 70;

        /// <summary>
        ///     上传波形数据，包括：波形数据（1维，2维等振动测点数据）。
        /// </summary>
        public const int UploadWaveData = CommandIDBase + 71;

        /// <summary>
        ///     上传数据到数据库，包括：趋势数据（温度、电流等没有波形测点数据）、波形数据（1维，2维等振动测点数据）、报警事件、报警数据。
        /// </summary>
        public const int UploadData2DB = CommandIDBase + 72;

        /// <summary>
        ///     数据集合的开始，用于传输多组数据时，表示集合的开始
        /// </summary>
        public const int SampleDataCollectionBegin = CommandIDBase + 75;

        /// <summary>
        ///     数据集合的结束，用于传输多组数据时，表示集合的结束
        /// </summary>
        public const int SampleDataCollectionEnd = CommandIDBase + 76;

        /// <summary>
        ///     文件片段数据集合的开始，用于传输多组数据时，表示集合的开始
        /// </summary>
        public const int FileSegmentDataCollectionBegin = CommandIDBase + 79;

        /// <summary>
        ///     文件片段数据集合的结束，用于传输多组数据时，表示集合的结束
        /// </summary>
        public const int FileSegmentDataCollectionEnd = CommandIDBase + 80;

        /// <summary>
        ///     现场实施工具使用，设置用户IP地址
        /// </summary>
        public const int SetUserIpAddress = CommandIDBase + 81;

        /// <summary>
        ///     现场实施工具使用，采集板校零
        /// </summary>
        public const int CalibrateZero = CommandIDBase + 84;

        /// <summary>
        ///     现场实施工具使用，通讯板复位命令
        /// </summary>
        public const int Reset = CommandIDBase + 87;

        /// <summary>
        ///     现场实施工具使用，采集板复位命令
        /// </summary>
        public const int ResetSMA = CommandIDBase + 88;

        /// <summary>
        ///     一组或多组报警事件数据
        /// </summary>
        public const int AlmEventDatas = CommandIDBase + 91;

        /// <summary>
        ///     一组或多组采集的数据
        /// </summary>
        public const int SampleDatas = CommandIDBase + 93;

        /// <summary>
        ///     一组或多组文件片段数据
        /// </summary>
        public const int FileSegmentDatas = CommandIDBase + 95;

        /// <summary>
        ///     推送(固件)文件的版本
        /// </summary>
        public const int PushFileVersion = CommandIDBase + 101;

        /// <summary>
        ///     推送(固件)文件的字节数组
        /// </summary>
        public const int PushFileBytes = CommandIDBase + 102;

        /// <summary>
        ///     采集额外的数据
        /// </summary>
        public const int SampleExtraDatas = CommandIDBase + 201;

        /// <summary>
        ///     重置电池状态（用于无线传感器+无线网关）
        /// </summary>
        public const int ResetBattery = CommandIDBase + 202;

        /// <summary>
        ///     报警灯复位
        /// </summary>
        public const int ResetAlmLight = CommandIDBase + 301;

        /// <summary>
        ///     命令执行成功，用于命令回复
        /// </summary>
        public const int CommandSuccess = CommandIDBase + 1000;

        /// <summary>
        ///     命令执行失败，用于命令回复，可搭配ErrorMessage结构
        /// </summary>
        public const int CommandFail = CommandIDBase + 1001;
    }
}