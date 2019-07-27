using System.IO;

namespace Moons.Common20.LongFile
{
    /// <summary>
    /// 打开二进制文件用于写入时调用的代理
    /// </summary>
    /// <param name="writer">BinaryWriter</param>
    public delegate void OnOpenBinary4WriteHandler( BinaryWriter writer );

    /// <summary>
    /// 写文件类的基类
    /// </summary>
    public abstract class FileAppenderBase : MultiFileReadAppendBase
    {
        #region 变量和属性

        /// <summary>
        /// 打开二进制文件用于写入时调用的事件
        /// </summary>
        public event OnOpenBinary4WriteHandler OnOpenBinary4Write;

        /// <summary>
        /// 文件的最大尺寸
        /// </summary>
        public long MaxSizeInBytes { get; set; }

        /// <summary>
        /// 打开的文件流
        /// </summary>
        private BinaryWriter _stream;

        /// <summary>
        /// 打开文件流的长度
        /// </summary>
        private long _streamLength;

        #endregion

        #region 自定义函数

        /// <summary>
        /// 写入字节数组和数组长度后是否超出了文件的最大尺寸
        /// </summary>
        /// <param name="bytes">字节数组</param>
        /// <returns>文件的长度</returns>
        protected bool IsExceedMaxSize_BytesWithLength( byte[] bytes )
        {
            return MaxSizeInBytes < GetFileLength_BytesWithLength( bytes );
        }

        /// <summary>
        /// 获得写入字节数组和数组长度后文件的长度
        /// </summary>
        /// <param name="bytes">字节数组</param>
        /// <returns>文件的长度</returns>
        private long GetFileLength_BytesWithLength( byte[] bytes )
        {
            return _streamLength + IntSize + bytes.Length;
        }

        /// <summary>
        /// 写入字节数组，在字节数组之前加入一个int（4字节长）表示该字节数组的长度
        /// </summary>
        /// <param name="bytes">字节数组</param>
        protected void WriteBytesWithLength( byte[] bytes )
        {
            int length = bytes.Length;
            int addCount = IntSize + length;

            _streamLength += addCount;
            _stream.Write( length );
            _stream.Write( bytes );
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
        protected void TryOpenStream()
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
            _stream = new BinaryWriter( File.Open( CurrentPath, FileMode.Create ) );

            var handler = OnOpenBinary4Write;
            if( handler != null )
            {
                handler( _stream );
            }

            _streamLength = _stream.BaseStream.Length;
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
        /// 把缓冲全部写入文件
        /// </summary>
        public void Flush()
        {
            if( IsStreamOpen )
            {
                _stream.Flush();
            }
        }

        /// <summary>
        /// 写入字节
        /// </summary>
        /// <param name="bytes">字节数组</param>
        public virtual void WriteBytes( byte[] bytes )
        {
        }
    }
}
