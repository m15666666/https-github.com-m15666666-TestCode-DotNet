namespace AnalysisData.SampleData
{
    /// <summary>
    ///     采集工作站类型（型号）
    /// </summary>
    public enum SampleStationType
    {
        /// <summary>
        /// </summary>
        Default = 0,

        /// <summary>
        ///     有线采集工作站MS1000
        /// </summary>
        Ms1000 = 100,

        /// <summary>
        ///     无线网关WG100(无线网关的2.4G版本)
        /// </summary>
        Wg100 = 200,

        /// <summary>
        ///     无线网关WG100-780(无线网关的780M版本)
        /// </summary>
        Wg100_780M = 201,
    }
}