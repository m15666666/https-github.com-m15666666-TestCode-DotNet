using System;
using Moons.Common20;

namespace AnalysisData.ToFromBytes
{
    /// <summary>
    /// 采集工作站的错误码
    /// </summary>
    public static class SampleStationErrorCode
    {
        #region 出错时候的错误码

        /// <summary>
        /// 没有错误
        /// </summary>
        public const int None = 0xA5;

        /// <summary>
        /// 包头分界符错误
        /// </summary>
        public const int PackageHead = 0x00;

        /// <summary>
        /// 包头版本号错误
        /// </summary>
        public const int PackageHeadVersion = 0x07;

        /// <summary>
        /// 包头crc错误
        /// </summary>
        public const int PackageHeadCrc = 0x02;

        /// <summary>
        /// 包尾crc错误
        /// </summary>
        public const int PackageTailCrc = 0x03;

        /// <summary>
        /// 包尾分界符错误
        /// </summary>
        public const int PackageTail = 0x01;

        /// <summary>
        /// 包长度出错
        /// </summary>
        public const int PackageLength = 0x04;

        /// <summary>
        /// 数据包命令出错
        /// </summary>
        public const int PackageCommand = 0x05;

        /// <summary>
        /// 数据包结构体定义出错
        /// </summary>
        public const int PackageStruct = 0x06;

        #endregion

        #region 正常返回时表示信息的信息码

        /// <summary>
        /// 正常返回时表示信息的信息码的基准
        /// </summary>
        private const int InfoCodeBase = 1000;

        /// <summary>
        /// 需要升级固件
        /// </summary>
        public const int InfoCode_NeedUpgradeFireware = InfoCodeBase + 1;

        #endregion

        /// <summary>
        /// 错误码对描述的映射
        /// </summary>
        private static readonly HashDictionary<int, string> _errorCode2Description =
            new HashDictionary<int, string>
                {
                    { PackageHead, "包头分界符错误" },//Package head split error
                    { PackageHeadVersion, "包头版本号错误" },//Package head version error
                    { PackageHeadCrc, "包头crc错误" },
                    { PackageTailCrc, "包尾crc错误" },
                    { PackageTail, "包尾分界符错误" },
                    { PackageLength, "包长度出错" },
                    { PackageCommand, "数据包命令出错" },
                    { PackageStruct, "数据包结构体定义出错" },
                };

        /// <summary>
        /// 通过错误码获得错误描述
        /// </summary>
        /// <param name="errorCode">错误码</param>
        /// <returns>错误描述</returns>
        public static string GetDescription( int errorCode )
        {
            return _errorCode2Description[errorCode];
        }
    }

    /// <summary>
    /// 对应SampleStationErrorCode的采集工作站异常
    /// </summary>
    public class SampleStationException : Exception
    {
        public SampleStationException( string message ) : base( message )
        {
        }

        /// <summary>
        /// 错误码
        /// </summary>
        public int ErrorCode { get; set; }

        public override string ToString()
        {
            string message = base.Message;
            string error = string.Format( "SampleStationException({0},{1})", ErrorCode,
                                          SampleStationErrorCode.GetDescription( ErrorCode ) );
            return string.IsNullOrEmpty( message ) ? error : message + " " + error;
        }
    }
}