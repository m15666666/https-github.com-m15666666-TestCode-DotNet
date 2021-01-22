using System.IO;

namespace Moons.Common20.Serialization
{
    /// <summary>
    /// 将对象与字节数组相互转换的接口
    /// </summary>
    public interface IToFromBytes
    {
        /// <summary>
        /// 将对象转换为字节
        /// </summary>
        /// <param name="writer">BinaryWriter</param>
        void ToBytes( BinaryWriter writer );

        /// <summary>
        /// 从字节数组读出属性
        /// </summary>
        /// <param name="reader">BinaryReader</param>
        void FromBytes( IBinaryRead reader );
    }

    /// <summary>
    /// 将对象与字节数组相互转换的接口
    /// </summary>
    /// <typeparam name="TReaderWriter">读写的接口</typeparam>
    public interface IToFromBytes<TReaderWriter>
    {
        /// <summary>
        /// 将对象转换为字节
        /// </summary>
        /// <param name="writer">BinaryWriter</param>
        void ToBytes( TReaderWriter writer );

        /// <summary>
        /// 从字节数组读出属性
        /// </summary>
        /// <param name="reader">BinaryReader</param>
        void FromBytes( TReaderWriter reader );
    }
}