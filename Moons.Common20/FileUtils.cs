using System;
using System.IO;

namespace Moons.Common20
{
    /// <summary>
    /// 处理文件的使用工具类
    /// </summary>
    public static class FileUtils
    {
        #region 删除文件

        /// <summary>
        /// 删除文件，不抛出异常
        /// </summary>
        /// <param name="paths">文件路径集合</param>
        public static void DeleteFiles( params string[] paths )
        {
            if( paths != null )
            {
                ForUtils.ForEach( paths, path => TryCatchUtils.Catch( File.Delete, path ) );
            }
        }

        /// <summary>
        /// 根据模版文件和目标文件的最新修改时间判断是否删除目标文件，
        /// 如果目标文件时间比模版新则保留，否则删除。
        /// </summary>
        /// <param name="sourcePath">模版文件</param>
        /// <param name="targetPath">目标文件</param>
        /// <returns>true：后续覆盖操作继续进行，false：后续覆盖操作不要进行</returns>
        public static bool DeleteTargetFileByLastWriteTime( string sourcePath, string targetPath )
        {
            if( !File.Exists( sourcePath ) )
            {
                throw new ArgumentException( "sourcePath:" + sourcePath );
            }

            if( File.Exists( targetPath ) )
            {
                // 目标文件修改时间比模版新，返回
                if( File.GetLastWriteTime( sourcePath ) < File.GetLastWriteTime( targetPath ) )
                {
                    return false;
                }

                // 模版比目标文件修改时间新，删除过时的目标文件
                File.Delete( targetPath );
            }
            return true;
        }

        #endregion

        #region 复制文件

        /// <summary>
        /// 尝试复制文件
        /// </summary>
        /// <param name="source">来源文件，不存在则抛出异常</param>
        /// <param name="target">目标文件，已存在则不复制</param>
        /// <returns>true：复制了文件，false：未复制</returns>
        public static bool TryCopy( string source, string target )
        {
            if( !File.Exists( target ) )
            {
                File.Copy( source, target );
                return true;
            }
            return false;
        }

        #endregion

        #region 打开文件

        /// <summary>
        /// 使用默认程序打开文件，例如：打开word(*.doc)文件
        /// </summary>
        /// <param name="path">文件路径</param>
        public static void OpenFileByDefaultApplication( string path )
        {
            ProcessUtils.StartProcess( path );
        }

        #endregion
    }
}