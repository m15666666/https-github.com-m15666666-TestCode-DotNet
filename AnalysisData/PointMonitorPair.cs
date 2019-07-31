using System;

namespace AnalysisData
{
    [Serializable]
    public class PointMonitorPair
    {
        public int PointID { get; set; }
        public int MObjectID { get; set; }
        public string MonitorID { get; set; }
    }
}
