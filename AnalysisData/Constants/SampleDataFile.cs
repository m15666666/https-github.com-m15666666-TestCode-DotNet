using System;
using System.IO;
using Moons.Common20;

namespace AnalysisData.Constants
{
    /// <summary>
    /// 与采集数据文件相关的常量
    /// </summary>
    public static class SampleDataFile
    {
        #region 常量

        /// <summary>
        /// 追忆文件的后缀
        /// </summary>
        public const string RetrospectFileExt = ".rst";

        /// <summary>
        /// 起停车文件的后缀
        /// </summary>
        public const string StartStopFileExt = ".sts";

        /// <summary>
        /// 起停车文件名
        /// </summary>
        public const string StartStopFileName = "StartStop" + StartStopFileExt;

        #endregion

        /// <summary>
        /// 排序追忆文件，按照文件的创建时间升序排列
        /// </summary>
        /// <param name="paths">追忆文件路径集合</param>
        /// <returns>按照文件的创建时间升序排列的追忆文件路径集合</returns>
        public static string[] SortRetrospectFiles( string[] paths )
        {
            if( paths == null || paths.Length == 0 )
            {
                return paths;
            }

            string[] ret = ArrayUtils.Clone( paths );

            DateTime[] createTimes = Array.ConvertAll( ret, path => GetDataFileCreateTime( path ) );
            Array.Sort( createTimes, ret );

            return ret;
        }

        /// <summary>
        /// 获得数据文件的创建时间
        /// </summary>
        /// <param name="path">数据文件路径</param>
        /// <returns>数据文件的创建时间</returns>
        private static DateTime GetDataFileCreateTime( string path )
        {
            using( var reader = new BinaryReader( File.Open( path, FileMode.Open, FileAccess.Read, FileShare.None ) ) )
            {
                // 读出版本号
                reader.ReadInt32();

                // 读出文件的创建时间
                return TimeUtils.DoubleToTime( reader.ReadDouble() );
            }
        }
    }
}