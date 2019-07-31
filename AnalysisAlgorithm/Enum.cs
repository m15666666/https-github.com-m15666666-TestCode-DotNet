namespace AnalysisAlgorithm
{
    /// <summary>
    /// 数据类型
    /// </summary>
    public enum WaveType
    {
        /// <summary>
        /// 时间波形
        /// </summary>
        TimeWave,

        /// <summary>
        /// 频谱
        /// </summary>
        Spectrum,
    } ;

    /// <summary>
    /// 振动量类型
    /// </summary>
    public enum VibQtyType
    {
        /// <summary>
        /// 振动力
        /// </summary>
        Force,

        /// <summary>
        /// 振动位移
        /// </summary>
        Displacement,

        /// <summary>
        /// 振动速度
        /// </summary>
        Velocity,

        /// <summary>
        /// 振动加速度
        /// </summary>
        Acceleration,

        /// <summary>
        /// 通用的方式，即基准值为1，系数为10
        /// </summary>
        Generic,
    } ;
}