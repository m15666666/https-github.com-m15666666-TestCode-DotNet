using System;
using System.IO;

namespace Moons.Common20.Serialization
{
    /// <summary>
    /// 将对象与字节数组相互转换的基类
    /// </summary>
    [Serializable]
    public abstract class ToFromBytesBase : IToFromBytes, IToFromBytes<ToFromBytesUtils>
    {
        #region 变量和属性

        /// <summary>
        /// 写入的字节数
        /// </summary>
        public int WriteCount { get; protected set; }

        /// <summary>
        /// 读出的字节数
        /// </summary>
        public int ReadCount { get; protected set; }

        /// <summary>
        /// 读字节的工具对象
        /// </summary>
        protected ToFromBytesUtils ReadBytesUtils { get; private set; }

        #endregion

        #region IToFromBytes Members

        /// <summary>
        /// 将对象转换为字节
        /// </summary>
        /// <param name="writer">BinaryWriter</param>
        public void ToBytes( BinaryWriter writer )
        {
        }

        /// <summary>
        /// 从字节数组读出属性
        /// </summary>
        /// <param name="reader">BinaryReader</param>
        public void FromBytes( BinaryReader reader )
        {
            ReadBytesUtils = new ToFromBytesUtils( reader );
        }

        #endregion

        /// <summary>
        /// 将对象转换为字节
        /// </summary>
        /// <param name="writer">BinaryWriter</param>
        public virtual void WriteToBytes( BinaryWriter writer )
        {
        }

        /// <summary>
        /// 从字节数组读出属性
        /// </summary>
        /// <param name="reader">BinaryReader</param>
        public virtual void ReadFromBytes( BinaryReader reader )
        {
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
            WriteCount = ReadCount = 0;
        }

        #region IToFromBytes<ToFromBytesUtils> 成员

        public void ToBytes( ToFromBytesUtils writer )
        {
            throw new NotImplementedException();
        }

        public void FromBytes( ToFromBytesUtils reader )
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}