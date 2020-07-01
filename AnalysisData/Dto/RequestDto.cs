using System;
using System.Collections.Generic;

namespace AnalysisData.Dto
{
    /// <summary>
    ///  采集工作站发(json)命令类
    /// </summary>
    [Serializable]
    public class RequestDto
    {
        public string Name { get; set; }

        public string Version { get; set; } = "1.0.0";

        public bool Succeed { get; set; } = true;

        public Dictionary<string, object> Datas { get; set; }// = new Dictionary<string, object>();
    }

    /// <summary>
    ///  采集工作站收(json)命令类
    /// </summary>
    [Serializable]
    public class ResponseDto
    {
        public string Name { get; set; }

        public string Version { get; set; } = "1.0.0";

        public bool Succeed { get; set; } = true;

        public Dictionary<string, object> Datas { get; set; }// = new Dictionary<string, object>();
    }
}