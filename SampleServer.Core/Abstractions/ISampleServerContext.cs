using AnalysisData.SampleData;

namespace SampleServer.Core.Abstractions
{
    /// <summary>
    /// 提供sampleserver的运行环境
    /// </summary>
    public interface ISampleServerContext
    {
        /// <summary>
        /// 根据测点id获得测点编码
        /// </summary>
        /// <param name="pointId">测点id</param>
        /// <returns>测点编码</returns>
        string GetPointCodeById(int pointId);

        /// <summary>
        /// 根据测点id获得测点编码
        /// </summary>
        /// <param name="pointCode">测点编码</param>
        /// <returns>测点id</returns>
        int GetPointIdByCode(string pointCode);

        /// <summary>
        /// 获取报警设置
        /// </summary>
        /// <param name="pointID">测点id</param>
        /// <param name="featureValueId">特征id</param>
        /// <returns>报警设置</returns>
        AlmStand_CommonSettingData GetAlmStand_CommonSettingDataByID(int pointID, int featureValueId);

        /// <summary>
        ///     根据报警事件唯一ID获得报警ID
        /// </summary>
        /// <param name="almEventUniqueID">报警事件唯一ID</param>
        /// <returns>报警ID</returns>
        long GetAlmIDByAlmEventUniqueID(string almEventUniqueID);

        /// <summary>
        ///     根据同步采集唯一ID获得同步ID
        /// </summary>
        /// <param name="syncUniqueID">同步采集唯一ID</param>
        /// <returns>同步ID</returns>
        long GetSyncIDBySyncUniqueID(string syncUniqueID);

        /// <summary>
        /// 发送报警事件
        /// </summary>
        /// <param name="almData">报警事件</param>

        void SendAlmEvent(object almData);

        /// <summary>
        /// 发送实时监测数据
        /// </summary>
        /// <param name="data">实时监测数据</param>
        void SendMonitorData(object data);

        /// <summary>
        /// 发送入库数据
        /// </summary>
        /// <param name="data">实时监测数据</param>
        void SendSave2DBData(object data);
    }
}
