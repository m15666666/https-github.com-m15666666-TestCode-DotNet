using System;
using System.Collections.Generic;
using AnalysisData.Constants;
using AnalysisData.SampleData;
using Moons.Common20;
using SampleServer.Helper;
using AnalysisData.Helper;
using SampleServer.Core.Abstractions;
using Moons.Common20.IOC;
using Microsoft.Extensions.Options;
using SampleServer.Core.Dto;

namespace SampleServer
{
    /// <summary>
    ///     配置工具类
    /// </summary>
    public sealed class Config
    {
        /// <summary>
        ///     正常采集队列中记录数的最大值
        /// </summary>
        public static int MaxNormalSampleQueue => 60000;

        /// <summary>
        ///     压缩(方式)类型编号
        /// </summary>
        public static int CompressTypeID => 1;

        #region 初始化

        /// <summary>
        ///     初始化
        /// </summary>
        public static void Init()
        {
        }

        #endregion

        #region 采集服务器的探针数据对象

        /// <summary>
        ///     采集工作站的探针数据对象
        /// </summary>
        private static readonly SampleServerProbeData _probe = new SampleServerProbeData();

        /// <summary>
        ///     采集工作站的探针数据对象
        /// </summary>
        public static SampleServerProbeData Probe
        {
            get { return _probe; }
        }

        /// <summary>
        ///     重置探针
        /// </summary>
        public static void ResetProbe()
        {
            Probe.Reset();
        }

        #endregion

        #region 同步采集ID相关

        /// <summary>
        ///     根据同步采集唯一ID获得同步ID
        /// </summary>
        /// <param name="syncUniqueID">同步采集唯一ID</param>
        /// <returns>同步ID</returns>
        public static long GetSyncIDBySyncUniqueID(string syncUniqueID)
        {
            return SampleServerContext.GetSyncIDBySyncUniqueID(syncUniqueID);
        }

        #endregion

        #region 报警ID相关

        /// <summary>
        ///     根据报警事件唯一ID获得报警ID
        /// </summary>
        /// <param name="almEventUniqueID">报警事件唯一ID</param>
        /// <returns>报警ID</returns>
        public static long GetAlmIDByAlmEventUniqueID( string almEventUniqueID )
        {
            return SampleServerContext.GetAlmIDByAlmEventUniqueID(almEventUniqueID);
            //var ret = _almEventUniqueKey2ID.GetIDByUniqueKey( almEventUniqueID );
            //if( !string.IsNullOrWhiteSpace( almEventUniqueID ) )
            //{
            //    TraceUtils.LogDebugInfo( $" {ret} = GetAlmIDByAlmEventUniqueID( {almEventUniqueID} )." );
            //}
            //return ret;
        }

        #endregion

        #region 获取数据库信息

        private static ISampleServerContext _sampleServerContext;
        internal static ISampleServerContext SampleServerContext => _sampleServerContext ??
            (_sampleServerContext = IocUtils.Instance.GetRequiredService<ISampleServerContext>());

        /// <summary>
        /// datasampler 配置数据
        /// </summary>
        internal static SampleServerConfigDto SampleServerConfigDto => _sampleServerConfigDto ?? 
            (_sampleServerConfigDto = IocUtils.Instance.GetRequiredService<IOptions<SampleServerConfigDto>>().Value);

        private static SampleServerConfigDto _sampleServerConfigDto;

        public static AlmStand_CommonSettingData GetAlmStand_CommonSettingDataByID( int pointID, int featureValueId )
        {
            return SampleServerContext.GetAlmStand_CommonSettingDataByID(pointID, featureValueId);
            //AlmStand_CommonSetting almStand_CommonSetting = GetPntAlmStandardsByID( pointID, featureValueId);
            //if (almStand_CommonSetting == null) return null;

            //AlmStand_CommonSettingData ret = new AlmStand_CommonSettingData {
            //    AlmType_ID = (int)almStand_CommonSetting.AlmType_ID,
            //    HighLimit1_NR = almStand_CommonSetting.HighLimit1_NR,
            //    HighLimit2_NR = almStand_CommonSetting.HighLimit2_NR,
            //    LowLimit1_NR = almStand_CommonSetting.LowLimit1_NR,
            //    LowLimit2_NR = almStand_CommonSetting.LowLimit2_NR,
            //};

            //return ret;
        }

        #endregion

        #region 定制模式

        /// <summary>
        ///     是否是酒钢模式
        /// </summary>
        private static bool? _isWineSteel;

        /// <summary>
        ///     是否是酒钢模式
        /// </summary>
        public static bool IsWineSteelCustomMode
        {
            get { return _isWineSteel ?? ( _isWineSteel = StringUtils.EqualIgnoreCase( CustomMode, "WineSteel" ) ).Value; }
        }

        /// <summary>
        ///     是否是大连西太平洋石油化工有限公司模式
        /// </summary>
        private static bool? _isWEPEC;

        /// <summary>
        ///     是否是大连西太平洋石油化工有限公司模式
        /// </summary>
        public static bool IsWEPECCustomMode
        {
            get { return _isWEPEC ?? ( _isWEPEC = StringUtils.EqualIgnoreCase( CustomMode, "WEPEC" ) ).Value; }
        }

        /// <summary>
        /// 为了指明报警设备节点（该节点上设置报警负责人，该节点名称中包含设备位号。该节点下所有测点报警均归为该设备报警），需要该节点的设备编码以“M-”开头，且不继承父节点编码。
        /// </summary>
        public const string WEPEC_Prefix_Machine = "M-";

        /// <summary>
        /// 为了指明报警描述中设备路径的起始节点，需要起始节点的设备编码以“P-”开头，且不继承父节点编码。
        /// </summary>
        public const string WEPEC_Prefix_FirstParent = "P-";

        /// <summary>
        ///     定制模式
        /// </summary>
        private static string CustomMode
        {
            get { return SampleServerConfigDto.CustomMode; }
        }

        #endregion
    }
}