namespace AnalysisData.Constants
{
    /// <summary>
    /// 旋转方向ID
    /// </summary>
    public static class RotationID
    {
        /// <summary>
        /// 逆时针
        /// </summary>
        public const int AntiClockwise = 0;

        /// <summary>
        /// 顺时针
        /// </summary>
        public const int Clockwise = 1;

        /// <summary>
        /// 是否是顺时针方向
        /// </summary>
        /// <param name="rotationID">旋转方向ID</param>
        /// <returns>是否是顺时针方向</returns>
        public static bool IsClockwise(int rotationID)
        {
            return Clockwise == rotationID;
        }

        /// <summary>
        /// 是否是逆时针方向
        /// </summary>
        /// <param name="rotationID">旋转方向ID</param>
        /// <returns>是否是逆时针方向</returns>
        public static bool IsAntiClockwise(int rotationID)
        {
            return AntiClockwise == rotationID;
        }
    }
}
