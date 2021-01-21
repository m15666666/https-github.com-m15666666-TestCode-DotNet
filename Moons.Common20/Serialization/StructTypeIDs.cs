namespace Moons.Common20.Serialization
{
    /// <summary>
    ///     结构类型ID
    /// </summary>
    public static class StructTypeIDs
    {
        /// <summary>
        ///     空的自定义数据的结构类型ID
        /// </summary>
        public const int EmptyCustomData = 0;

        /// <summary>
        ///     变长结构体数组
        /// </summary>
        public const int VarStructArray = 91;

        /// <summary>
        ///     变长字符串
        /// </summary>
        public const int VarString = 101;

        /// <summary>
        ///     变长字符串包含json
        /// </summary>
        public const int VarStringOfJson = 102;

        /// <summary>
        ///     不带长度标识的字节数组
        /// </summary>
        public const int ByteArray = 111;

        /// <summary>
        ///     版本数据(版本号)
        /// </summary>
        public const int VersionData = 121;

        /// <summary>
        ///     结构体类型ID的起始
        /// </summary>
        public const int StructTypeIDBase = 1000;
    }
}