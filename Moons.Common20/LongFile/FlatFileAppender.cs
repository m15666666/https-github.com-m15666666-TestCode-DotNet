namespace Moons.Common20.LongFile
{
    /// <summary>
    /// 不循环写文件类
    /// 已测试情况（用.hex文件）:
    /// 1.1个10字节文件写入1个限定大小20字节的文件（可以写入）
    /// 2.1个10字节文件写入1个限定大小10字节的文件（ 4+10 >10无法写入）
    /// 3.2个分别10字节的文件写入1个大小30字节的文件（4+10+4+10 小于30 两个文件写入1）
    /// 4.2个分别10字节的文件写入1个大小20字节的文件（4+10+4+10 > 20 只有一个文件写进去了）
    /// 5.3个10字节的文件写入3个20字节的文件（3个文件分别写入了3个路径）
    /// 6.4个10字节的文件写入3个20字节的文件（3个文件分别写入了3个路径，第四个文件没有写入）
    /// </summary>
    public class FlatFileAppender : FileAppenderBase
    {
        /// <summary>
        /// 是否到了文件列表末尾，如果已到文件列表末尾，则不再写入任何文件。
        /// </summary>
        private bool _isFilesEnd;

        /// <summary>
        /// 写入字节数组，在字节数组之前加入一个int（4字节长）表示该字节数组的长度
        /// </summary>
        /// <param name="bytes">字节数组</param>
        public override void WriteBytes( byte[] bytes )
        {
            if( _isFilesEnd )
            {
                return;
            }

            TryOpenStream();

            if( IsExceedMaxSize_BytesWithLength( bytes ) )
            {
                CloseStream();

                // 如果已到文件列表末尾，则不再写入任何文件。
                if( CycleMoveNextPathIndex() )
                {
                    _isFilesEnd = true;
                    return;
                }

                OpenStream();
            }

            WriteBytesWithLength( bytes );
        }
    }
}