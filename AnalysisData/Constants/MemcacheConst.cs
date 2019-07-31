namespace AnalysisData.Constants
{
    /// <summary>
    /// 缓存的前缀
    /// </summary>
    public static class MemcacheConst
    {
        /// <summary>
        /// 测点波形的缓存前缀
        /// </summary>
        public static readonly string CachePrefix_PointWave = "PointWave_";

        /// <summary>
        /// 测点监测数据(趋势数据)的缓存前缀
        /// </summary>
        public static readonly string CachePrefix_PointMornitorData = "PointMornitorData_";

        /// <summary>
        /// 用户权限的缓存前缀
        /// </summary>
        public static readonly string CachePrefix_UserActions = "UserActions_";

        /// <summary>
        /// 设备是否采集数据前缀
        /// </summary>
        public static readonly string CachePrefix_MachineSampleMode = "MachineSampleMode_";
    }
}