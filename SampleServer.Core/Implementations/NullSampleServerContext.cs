using AnalysisData.SampleData;
using SampleServer.Core.Abstractions;

namespace SampleServer.Core.Implementations
{
    public class NullSampleServerContext : ISampleServerContext
    {
        public long GetAlmIDByAlmEventUniqueID(string almEventUniqueID)
        {
            return 0;
        }

        public AlmStand_CommonSettingData GetAlmStand_CommonSettingDataByID(int pointID, int featureValueId)
        {
            return null;
        }

        public string GetPointCodeById(int pointId)
        {
            return string.Empty;
        }

        public int GetPointIdByCode(string pointCode)
        {
            return 0;
        }

        public long GetSyncIDBySyncUniqueID(string syncUniqueID)
        {
            return 0;
        }

        public void SendAlmEvent(object almData)
        {
        }

        public void SendMonitorData(object data)
        {
        }

        public void SendSave2DBData(object data)
        {
        }
    }
}
