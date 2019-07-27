namespace Moons.Common20.Serialization
{
    /// <summary>
    ///     版本数据类
    /// </summary>
    public class VersionData : CustomDataBase
    {
        public VersionData( int version )
        {
            VersionBytes = ByteUtils.Int32ToBytes( version );
        }

        public VersionData( byte major, byte minor, byte build, byte revision )
        {
            VersionBytes = new[] {major, minor, build, revision};
        }

        /// <summary>
        ///     版本号字节数组
        /// </summary>
        public byte[] VersionBytes { get; private set; }
    }
}