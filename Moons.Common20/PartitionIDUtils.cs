using System;

namespace Moons.Common20
{
    /// <summary>
    /// 分区编号的实用工具类
    /// </summary>
    public static class PartitionIDUtils
    {
        /// <summary>
        /// 将时间转换为长整形的ID
        /// </summary>
        /// <param name="time">时间</param>
        /// <returns>长整形的ID</returns>
        public static long Time2LongID( DateTime time )
        {
            return long.Parse( time.ToString( "yyyyMMddHHmmss" ) );
        }

        /// <summary>
        /// 将时间转换为长整形的ID
        /// </summary>
        /// <param name="time">时间</param>
        /// <returns>长整形的ID</returns>
        public static long Time2LongID( DateTime? time )
        {
            return Time2LongID( time.Value );
        }
    }
}