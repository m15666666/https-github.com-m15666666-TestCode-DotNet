namespace AnalysisUtils
{
    /// <summary>
    /// 数据分析的实用工具类
    /// 起名AnalyseUtils，与AnalysisUtils命名空间有些区别
    /// </summary>
    public static class AnalyseUtils
    {
        static AnalyseUtils()
        {
            RevUnit = "RPM";
            XAxisUnitFreqX = "X";
            XAxisUnitFreqHz = "Hz";
            XAxisUnitTimePoint = "点数";
            XAxisUnitTimeMillisecond = "ms";
            XAxisUnitTimeScale2Second = 0.001f;
        }

        #region 与单位相关

        #region 变量和属性

        /// <summary>
        /// 以毫秒表示的X轴时间单位
        /// </summary>
        public static string XAxisUnitTimeMillisecond { get; set; }

        /// <summary>
        /// X轴时间单位换算为秒的比例因子
        /// </summary>
        public static float XAxisUnitTimeScale2Second { get; set; }

        /// <summary>
        ///  以点数表示的X轴时间单位
        /// </summary>
        public static string XAxisUnitTimePoint { get; set; }

        /// <summary>
        /// 以赫兹表示的X轴频率单位
        /// </summary>
        public static string XAxisUnitFreqHz { get; set; }

        /// <summary>
        /// 以阶次表示的X轴频率单位
        /// </summary>
        public static string XAxisUnitFreqX { get; set; }

        /// <summary>
        /// 转速单位
        /// </summary>
        public static string RevUnit { get; set; }

        #endregion

        /// <summary>
        /// 获得时间波形X轴的单位
        /// </summary>
        /// <param name="isOrder">是否等角度采样</param>
        /// <returns>时间波形X轴的单位</returns>
        public static string GetXAxisUnit_Time( bool isOrder )
        {
            return isOrder ? XAxisUnitTimePoint : XAxisUnitTimeMillisecond;
        }

        /// <summary>
        /// 获得频谱X轴的单位
        /// </summary>
        /// <param name="isOrder">是否等角度采样</param>
        /// <returns>频谱X轴的单位</returns>
        public static string GetXAxisUnit_Freq( bool isOrder )
        {
            return isOrder ? XAxisUnitFreqX : XAxisUnitFreqHz;
        }

        #endregion

        #region 是否是等角度采样

        /// <summary>
        /// 是否是等角度采样
        /// </summary>
        /// <param name="multiFreq">倍频系数</param>
        /// <returns>是否是等角度采样</returns>
        public static bool IsOrder( int multiFreq )
        {
            return 0 < multiFreq;
        }

        #endregion
    }
}