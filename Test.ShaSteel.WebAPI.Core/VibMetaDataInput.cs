using System;

namespace Test.ShaSteel.WebAPI.Core
{
    public class VibMetaDataInput
    {
        public string Code { get; set; }

        public string FullPath { get; set; }

        public string MeasDate { get; set; }
        public float MeasValue { get; set; }
        public int WaveLength { get; set; }
        public int SignalType { get; set; }
        public float SampleRate { get; set; }
        public float RPM { get; set; }
        public string Unit { get; set; }
        public float ConvertCoef { get; set; } = 0.39f;
        public int Resolver { get; set; } = 1;
    }
}
