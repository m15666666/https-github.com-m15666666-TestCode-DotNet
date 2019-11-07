using System;

namespace Test.ShaSteel.WebAPI.Core
{
    public class VibMetaDataInput
    {
        public string Code { get; set; }

        public string FullPath { get; set; }

        public string MeasDate { get; set; }
        public string FloatMeasValue { get; set; }
        public string WaveLength { get; set; }
        public string SignalType { get; set; }
        public string SampleRate { get; set; }
        public string RPM { get; set; }
        public string Unit { get; set; }
        public string ConvertCoef { get; set; }
        public string Resolver { get; set; }
    }
}
