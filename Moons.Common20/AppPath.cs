using System;
using System.IO;

namespace Moons.Common20
{
    /// <summary>
    ///     关于应用程序路径的实用工具类
    /// </summary>
    public class AppPath
    {
        #region winform程序路径相关

        /// <summary>
        ///     获取应用程序(.exe)文件的所在路径
        /// </summary>
        /// <returns>应用程序(.exe)文件的所在路径</returns>
        public static string GetAppPath()
        {
            return AppContext.BaseDirectory;
        }

        /// <summary>
        ///     根据绝对路径和相对路径 获取文件路径
        /// </summary>
        /// <param name="path">相对路径</param>
        /// <returns>组合路径</returns>
        public static string GetPath( string path )
        {
            return Path.Combine( GetAppPath(), path );
        }

        /// <summary>
        ///     获得包含路径的主窗体标题
        /// </summary>
        /// <param name="appName">程序名</param>
        /// <returns>包含版本号的主窗体标题</returns>
        public static string GetTitleWithPath( string appName )
        {
            return string.Format( "{0}-{1}", appName, GetAppPath() );
        }

        #region 我的文档目录路径相关

        /// <summary>
        ///     我的文档目录路径
        /// </summary>
        private static string _myDocumentDirPath;

        /// <summary>
        ///     系统的我的文档目录
        /// </summary>
        public static string SysMyDocumentsDirectory
        {
            get { return Environment.GetFolderPath( Environment.SpecialFolder.MyDocuments ); }
        }

        /// <summary>
        ///     我的文档目录路径
        /// </summary>
        public static string MyDocumentDirPath
        {
            get
            {
                if( _myDocumentDirPath == null )
                {
                    string applicationDataName = ApplicationDataName;
                    string myDocuments = SysMyDocumentsDirectory;
                    MyDocumentDirPath = string.IsNullOrEmpty( applicationDataName )
                                            ? myDocuments
                                            : Path.Combine( myDocuments, applicationDataName );
                }
                return _myDocumentDirPath;
            }
            set { EnsureDirExist( _myDocumentDirPath = value ); }
        }

        /// <summary>
        ///     根据我的文档目录和相对路径获取文件路径
        /// </summary>
        /// <param name="path">相对路径</param>
        /// <returns>组合路径</returns>
        public static string GetPathByMyDocument( string path )
        {
            return Path.Combine( MyDocumentDirPath, path );
        }

        #endregion

        #region 应用程序数据目录路径相关

        /// <summary>
        ///     应用程序数据目录路径
        /// </summary>
        private static string _applicationDataDirPath;

        /// <summary>
        ///     应用程序数据名，用于标识应用程序的目录，空表示当前目录、无目录
        /// </summary>
        public static string ApplicationDataName { get; set; }

        /// <summary>
        ///     系统的应用程序数据目录
        /// </summary>
        public static string SysApplicationDataDirectory
        {
            get { return Environment.GetFolderPath( Environment.SpecialFolder.ApplicationData ); }
        }

        /// <summary>
        ///     应用程序数据目录路径
        /// </summary>
        public static string ApplicationDataDirPath
        {
            get
            {
                if( _applicationDataDirPath == null )
                {
                    string applicationDataName = ApplicationDataName;
                    ApplicationDataDirPath = string.IsNullOrEmpty( applicationDataName )
                                                 ? GetAppPath()
                                                 : Path.Combine( SysApplicationDataDirectory, applicationDataName );
                }
                return _applicationDataDirPath;
            }
            set { EnsureDirExist( _applicationDataDirPath = value ); }
        }

        /// <summary>
        ///     根据应用程序数据目录和相对路径获取文件路径
        /// </summary>
        /// <param name="path">相对路径</param>
        /// <returns>组合路径</returns>
        public static string GetPathByApplicationData( string path )
        {
            return Path.Combine( ApplicationDataDirPath, path );
        }

        #endregion

        #region 临时文件目录

        /// <summary>
        ///     临时文件目录
        /// </summary>
        private static string _tempDirPath;

        /// <summary>
        ///     临时文件目录
        /// </summary>
        public static string TempDirPath
        {
            get
            {
                const string TempDirName = "Temp";
                if( _tempDirPath == null )
                {
                    TempDirPath = GetPathByApplicationData( TempDirName );
                }
                return _tempDirPath;
            }
            set { EnsureDirExist( _tempDirPath = value ); }
        }

        /// <summary>
        ///     获取临时目录下的文件路径
        /// </summary>
        /// <param name="path">相对路径</param>
        /// <returns>临时目录下的文件路径</returns>
        public static string GetTempPath( string path )
        {
            return Path.Combine( TempDirPath, path );
        }

        #endregion

        #endregion

        #region 目录相关

        /// <summary>
        ///     目录分隔符
        /// </summary>
        public static readonly string DirectorySeparator = Path.DirectorySeparatorChar.ToString();

        /// <summary>
        ///     尝试在路径末尾补全目录分隔符，如果存在则不添加
        /// </summary>
        /// <param name="dirPath">目录路径</param>
        /// <returns>补全后的路径</returns>
        public static string TryAppendDirectorySeparator( string dirPath )
        {
            if( string.IsNullOrEmpty( dirPath ) )
            {
                return null;
            }

            return dirPath.EndsWith( DirectorySeparator ) ? dirPath : dirPath + DirectorySeparator;
        }

        /// <summary>
        ///     获得目录
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns>目录</returns>
        public static string GetDirectory( string path )
        {
            if( string.IsNullOrEmpty( path ) || path.EndsWith( DirectorySeparator ) )
            {
                return path;
            }

            string dir = Path.GetDirectoryName( path );
            return dir ?? path;
        }

        /// <summary>
        ///     确保目录存在，不存在则创建
        /// </summary>
        /// <param name="dirPath">目录路径</param>
        public static void EnsureDirExist( string dirPath )
        {
            if( !string.IsNullOrEmpty( dirPath ) && !Directory.Exists( dirPath ) )
            {
                Directory.CreateDirectory( dirPath );
            }
        }

        /// <summary>
        ///     合并多个路径
        /// </summary>
        /// <param name="parentPath">父路径</param>
        /// <param name="subPaths">子路径集合</param>
        /// <returns>合并后的路径</returns>
        public static string CombinePath( string parentPath, params string[] subPaths )
        {
            if( subPaths == null || subPaths.Length == 0 )
            {
                return parentPath;
            }

            string ret = parentPath;
            foreach( string subPath in subPaths )
            {
                ret = Path.Combine( ret, subPath );
            }
            return ret;
        }

        /// <summary>
        ///     合并相对路径
        /// </summary>
        /// <param name="parentPath">父路径</param>
        /// <param name="relativePath">相对路径</param>
        /// <returns>合并后的路径</returns>
        public static string CombineRelativePath(string parentPath, string relativePath)
        {
            if (string.IsNullOrEmpty(relativePath))
            {
                return parentPath;
            }

            // 不是相对路径，则直接合并路径
            if( !relativePath.StartsWith( StringUtils.Point ) )
            {
                return CombinePath( parentPath, relativePath );
            }

            if( !StringUtils.EqualIgnoreCase( Environment.CurrentDirectory, parentPath ) )
            {
                Environment.CurrentDirectory = parentPath;
            }
            return Path.GetFullPath( relativePath );
        }

        /// <summary>
        ///     合并多个Url路径
        /// </summary>
        /// <param name="parentPath">父路径</param>
        /// <param name="subPaths">子路径集合</param>
        /// <returns>合并后的Url路径</returns>
        public static string CombineUrlPath( string parentPath, params string[] subPaths )
        {
            if( subPaths == null || subPaths.Length == 0 )
            {
                return parentPath;
            }

            string slash = StringUtils.Slash;
            string ret = parentPath ?? string.Empty;
            foreach( string subPath in subPaths )
            {
                if( !ret.EndsWith( slash ) )
                {
                    ret += slash;
                }
                ret += subPath;
            }
            return ret;
        }

        #endregion

        #region 基路径相关

        /// <summary>
        ///     根据基路径绝对路径和相对路径 获取文件路径
        /// </summary>
        /// <param name="path">相对路径</param>
        /// <returns>组合路径</returns>
        public static string GetPathByBasePath( string path )
        {
            string baseDir = GetBasePath();
            return Path.Combine( baseDir, path );
        }

        /// <summary>
        ///     根据基路径下的bin目录路径 获取文件路径
        /// </summary>
        /// <param name="path">相对路径</param>
        /// <returns>组合路径</returns>
        public static string GetPathByBinPath( string path )
        {
            string binDir = GetBinPath();
            return Path.Combine( binDir, path );
        }

        /// <summary>
        ///     获得基路径
        /// </summary>
        /// <returns>基路径</returns>
        public static string GetBasePath()
        {
            return AppDomain.CurrentDomain.BaseDirectory;
        }

        /// <summary>
        ///     获得基路径下的bin目录路径，bin目录不存在则返回基路径
        /// </summary>
        /// <returns>基路径下的bin目录路径，bin目录不存在则返回基路径</returns>
        public static string GetBinPath()
        {
            string baseDir = GetBasePath();
            string binDir = Path.Combine( baseDir, "bin" );
            return Directory.Exists( binDir ) ? binDir : baseDir;
        }

        #endregion

        #region 校验路径

        /// <summary>
        ///     校验路径，对不合法的部分使用replace替换
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="replace">替换的字符串</param>
        /// <returns>校验后的路径</returns>
        public static string ValidPath( string path, string replace )
        {
            return StringUtils.Filter( path, replace, Path.GetInvalidPathChars() );
        }

        /// <summary>
        ///     校验路径，对不合法的部分使用空字符串替换
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns>校验后的路径</returns>
        public static string ValidPath( string path )
        {
            return ValidPath( path, null );
        }

        /// <summary>
        ///     校验文件名，对不合法的部分使用replace替换
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="replace">替换的字符串</param>
        /// <returns>校验后的文件名</returns>
        public static string ValidFileName( string fileName, string replace )
        {
            return StringUtils.Filter( fileName, replace, Path.GetInvalidFileNameChars() );
        }

        /// <summary>
        ///     校验文件名，对不合法的部分使用空字符串替换
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns>校验后的文件名</returns>
        public static string ValidFileName( string fileName )
        {
            return ValidFileName( fileName, null );
        }

        #endregion

        #region 目录下的文件

        /// <summary>
        ///     获得根目录下的所有文件路径
        /// </summary>
        /// <param name="rootDir">根目录</param>
        /// <param name="recursive">是否递归搜索，默认为true</param>
        /// <param name="relativePath">是否返回相对路径，默认为true</param>
        /// <param name="fileType">文件类型，例如：“*.css”，null表示搜索全部类型文件</param>
        /// <returns>文件路径集合</returns>
        public static string[] GetFilePaths( string rootDir, bool recursive, bool relativePath, string fileType )
        {
            if( fileType == null )
            {
                fileType = "*";
            }

            rootDir = CharCaseUtils.ToCase( TryAppendDirectorySeparator( rootDir ) );

            SearchOption searchOption = recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;

            string[] filePaths = Directory.GetFiles( rootDir, fileType, searchOption );
            return !relativePath
                       ? filePaths
                       : Array.ConvertAll( filePaths,
                                           path => CharCaseUtils.ToCase( path ).Replace( rootDir, string.Empty ) );
        }

        #endregion
    }
}