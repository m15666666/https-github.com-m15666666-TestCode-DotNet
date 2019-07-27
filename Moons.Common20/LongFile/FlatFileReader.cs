namespace Moons.Common20.LongFile
{
    /// <summary>
    /// 不循环读文件类
    /// </summary>
    public class FlatFileReader : MultiFileReaderBase
    {
        /// <summary>
        /// 是否到了文件列表末尾，如果已到文件列表末尾，则不再写入任何文件。
        /// </summary>
        private bool _isFilesEnd;

        /// <summary>
        /// 读出字节数组，如果读到末尾或失败则返回null。在字节数组之前读出一个int（4字节长）表示该字节数组的长度
        /// </summary>
        /// <returns>字节数组</returns>
        public override byte[] ReadBytes()
        {
            if( _isFilesEnd )
            {
                return null;
            }

            TryOpenStream();

            while( true )
            {
                byte[] ret = ReadBytesWithLength();

                // 已到文件尾，换下一个文件
                if( ret == null )
                {
                    CloseStream();

                    // 如果已到文件列表末尾，则不再写入任何文件。
                    if( CycleMoveNextPathIndex() )
                    {
                        _isFilesEnd = true;
                        return null;
                    }

                    OpenStream();
                    continue;
                }

                return ret;
            }
        }
    }
}
