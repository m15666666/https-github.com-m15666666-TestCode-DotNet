using System;

namespace AnalysisData
{
    [Serializable]
    public  class MobjectStatusData
    {
        public string name { set; get; }
        /// <summary>
        /// 设备总数
        /// </summary>
        public int total { set; get; }
        /// <summary>
        /// 报警数量
        /// </summary>
        public int alarm { set; get; }

        public string alarmrate {  set; get;   }
    }

   
}
