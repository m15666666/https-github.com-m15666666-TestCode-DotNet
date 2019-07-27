namespace Moons.Common20.Serialization
{
    /// <summary>
    /// 短整型数组波形
    /// </summary>
    public class ShortArrayWave
    {
        /// <summary>
        /// 波形的比例系数
        /// </summary>
        public float WaveScale { get; set; }

        /// <summary>
        /// 波形数据
        /// </summary>
        public short[] WaveData { get; set; }
    }
}