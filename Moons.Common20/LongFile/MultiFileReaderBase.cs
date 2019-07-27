using System.IO;

namespace Moons.Common20.LongFile
{
    /// <summary>
    /// 打开二进制文件用于读取时调用的代理
    /// </summary>
    /// <param name="reader">BinaryReader</param>
    public delegate void OnOpenBinary4ReadHandler( BinaryReader reader );

    /// <summary>
    /// 读多个文件的基类
    /// </summary>
    public abstract class MultiFileReaderBase : MultiFileReadAppendBase
    {
        #region 变量和属性

        /// <summary>
        /// 打开二进制文件用于读取时调用的事件
        /// </summary>
        public event OnOpenBinary4ReadHandler OnOpenBinary4Read;

        /// <summary>
        /// 打开的文件流
        /// </summary>
        private BinaryReader _stream;

        #endregion

        #region 自定义函数

        /// <summary>
        /// 读出字节数组，如果读到末尾或失败则返回null。在字节数组之前读出一个int（4字节长）表示该字节数组的长度
        /// </summary>
        /// <returns>字节数组</returns>
        protected byte[] ReadBytesWithLength()
        {
            try
            {
                int length = _stream.ReadInt32();
                return _stream.ReadBytes( length );
            }
            catch( EndOfStreamException )
            {
                return null;
            }
        }

        /// <summary>
        /// 流是否打开
        /// </summary>
        protected bool IsStreamOpen
        {
            get { return _stream != null; }
        }

        /// <summary>
        /// 流是否关闭
        /// </summary>
        protected bool IsStreamClose
        {
            get { return _stream == null; }
        }

        /// <summary>
        /// 尝试打开文件流
        /// </summary>
        public void TryOpenStream()
        {
            if( IsStreamClose )
            {
                OpenStream();
            }
        }

        /// <summary>
        /// 打开文件流
        /// </summary>
        protected void OpenStream()
        {
            _stream = new BinaryReader( File.Open( CurrentPath, FileMode.Open, FileAccess.Read, FileShare.None ) );

            var handler = OnOpenBinary4Read;
            if( handler != null )
            {
                handler( _stream );
            }
        }

        /// <summary>
        /// 关闭文件流
        /// </summary>
        public void CloseStream()
        {
            if( IsStreamOpen )
            {
                _stream.Close();
                _stream = null;
            }
        }

        #endregion

        #region 需要重载的成员

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose( bool disposing )
        {
            if( disposing )
            {
                CloseStream();
            }
        }

        #endregion

        /// <summary>
        /// 读出字节数组，如果读到末尾或失败则返回null
        /// </summary>
        /// <returns>字节数组</returns>
        public virtual byte[] ReadBytes()
        {
            return null;
        }
    }
}
