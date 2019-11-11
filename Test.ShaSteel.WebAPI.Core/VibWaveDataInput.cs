using System;

namespace Test.ShaSteel.WebAPI.Core
{
    public class VibWaveDataInput
    {
        public string WaveTag { get; set; }

        public int Length { get; set; }

        public int CurrIndex { get; set; } = 0;

        public int BlockSize { get; set; }
    }
}
