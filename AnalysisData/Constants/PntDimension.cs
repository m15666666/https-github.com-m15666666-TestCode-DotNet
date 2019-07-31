namespace AnalysisData.Constants
{
    /// <summary>
    /// 测点维数
    /// </summary>
    public static class PntDimension
    {
        #region 变量和属性

        /// <summary>
        /// 一维
        /// </summary>
        public const int One = 1;

        /// <summary>
        /// 二维
        /// </summary>
        public const int Two = 2;

        #endregion

        /// <summary>
        /// 是否是一维测点
        /// </summary>
        /// <param name="pntDimension">测点维数</param>
        /// <returns>是否是一维测点</returns>
        public static bool IsOneDimension( int pntDimension )
        {
            return pntDimension == One;
        }

        /// <summary>
        /// 是否是二维测点
        /// </summary>
        /// <param name="pntDimension">测点维数</param>
        /// <returns>是否是二维测点</returns>
        public static bool IsTwoDimension( int pntDimension )
        {
            return pntDimension == Two;
        }
    }
}
