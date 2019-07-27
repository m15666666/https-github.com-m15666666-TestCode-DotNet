namespace Moons.Common20.LongFile
{
    /// <summary>
    /// Null写文件类，不向任何文件输出
    /// </summary>
    public class NullFileAppender : FileAppenderBase
    {
        public override void WriteBytes( byte[] bytes )
        {
            // 不向任何文件输出
        }
    }
}