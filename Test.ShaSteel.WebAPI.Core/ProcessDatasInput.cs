using System;

namespace Test.ShaSteel.WebAPI.Core
{
    public class ProcessDatasInput
    {
        public string Code { get; set; }

        public string FullPath { get; set; }

        public TSDataInput[] TSDatas { get; set; }

        public string Unit { get; set; }
    }

    public class TSDataInput
    {
        public string MeasDate { get; set; }

        public string MeasValue { get; set; }
    }
}
