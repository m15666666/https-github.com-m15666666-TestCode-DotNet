namespace AnalysisData.Constants
{
    /// <summary>
    ///     数据类型编号
    /// </summary>
    public static class DataTypeID
    {
        /// <summary>
        ///     时间波形，目前名字改为频谱
        /// </summary>
        public const int Spectrum = 1;

        /// <summary>
        ///     手抄
        /// </summary>
        public const int HandRecord = 2;

        /// <summary>
        ///     仪抄
        /// </summary>
        public const int DeviceRecord = 3;

        /// <summary>
        ///     观察量
        /// </summary>
        public const int Obs = 4;

        /// <summary>
        ///     解调
        /// </summary>
        public const int Demod = 5;

        /// <summary>
        ///     Packs
        /// </summary>
        public const int Packs = 6;

        /// <summary>
        ///     字符串，直接保存字符串作为测量值，保存在HistorySummary.Result_TX字段。
        /// </summary>
        public const int TextValue = 51;

        /// <summary>
        ///     长数据类型(频谱波形)
        /// </summary>
        public const int LongSpectrum = 101;

        /// <summary>
        ///     启停机类型(频谱波形)
        /// </summary>
        public const int StartStopSpectrum = 201;

        /// <summary>
        ///     是否为频谱
        /// </summary>
        /// <param name="dataTypeID">数据类型编号</param>
        /// <returns>是否为频谱</returns>
        public static bool IsSpectrum( int dataTypeID )
        {
            return dataTypeID == Spectrum;
        }

        /// <summary>
        ///     是否为长数据类型(频谱波形)
        /// </summary>
        /// <param name="dataTypeID">数据类型编号</param>
        /// <returns>是否为长数据类型(频谱波形)</returns>
        public static bool IsLongSpectrum( int dataTypeID )
        {
            return dataTypeID == LongSpectrum;
        }

        /// <summary>
        ///     是否为启停机类型(频谱波形)
        /// </summary>
        /// <param name="dataTypeID">数据类型编号</param>
        /// <returns>是否为启停机类型(频谱波形)</returns>
        public static bool IsStartStopSpectrum( int dataTypeID )
        {
            return dataTypeID == StartStopSpectrum;
        }
    }
}