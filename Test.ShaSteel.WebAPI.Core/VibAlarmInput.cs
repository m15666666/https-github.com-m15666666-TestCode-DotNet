using System;

namespace Test.ShaSteel.WebAPI.Core
{
    public class VibAlarmInput
    {
        public string Code { get; set; }

        public string FullPath { get; set; }

        public string AlarmDate { get; set; }
        public string AlarmDec { get; set; }
        public int AlarmLevel { get; set; }
    }
}
