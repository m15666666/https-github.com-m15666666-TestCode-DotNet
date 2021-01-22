using System;
using System.IO;

namespace Moons.Common20.Serialization
{
    /// <summary>
    /// 实现IBinaryRead接口的类
    /// </summary>
    public class BinaryRead : BinaryReader, IBinaryRead
    {
        #region ctor

        public BinaryRead( Stream stream ) : base( stream )
        {
        }

        public virtual short[] ReadInt16Array(int count)
        {
            var ret = new short[count];
            //var buffer = new byte[ByteUtils.ByteCountPerInt16];//性能更差一些15249ms，差距更大
            //for (int i = 0; i < count; i++)
            //{
            //    Read(buffer, 0, ByteUtils.ByteCountPerInt16);
            //    ret[i] = BitConverter.ToInt16(buffer, 0);
            //}
            for (int i = 0; i < count; i++) ret[i] = ReadInt16();//性能差一些8993ms，差距不大
            return ret;
        }

        public virtual int[] ReadInt32Array(int count)
        {
            var ret = new int[count];
            //var buffer = new byte[ByteUtils.ByteCountPerInt32];
            //for(int i = 0; i < count; i++)
            //{
            //    Read(buffer, 0, ByteUtils.ByteCountPerInt32);
            //    ret[i] = BitConverter.ToInt32(buffer, 0);
            //}
            for(int i = 0; i < count; i++) ret[i] = ReadInt32();
            return ret;
        }

        public virtual float[] ReadSingleArray(int count)
        {
            var ret = new float[count];
            //var buffer = new byte[ByteUtils.ByteCountPerSingle];
            //for(int i = 0; i < count; i++)
            //{
            //    Read(buffer, 0, ByteUtils.ByteCountPerInt32);
            //    ret[i] = BitConverter.ToSingle(buffer, 0);
            //}
            for(int i = 0; i < count; i++) ret[i] = ReadSingle();
            return ret;
        }

        #endregion



    }
}