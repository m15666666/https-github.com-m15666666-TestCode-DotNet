using System;

namespace Moons.Common20.Serialization
{
    /// <summary>
    /// 数组项变长字节数组波形
    /// </summary>
    [Serializable]
    public class VarByteArrayWave
    {
        /// <summary>
        /// 波形的比例系数
        /// </summary>
        public float WaveScale { get; set; }

        /// <summary>
        /// 数组每项字节长度，目前选项有：2:表示Int16类型的字节数组, 
        /// 4: 表示Int32类型的字节数组, 101: 表示float类型4个字节的单精度浮点型的字节数组.
        /// </summary>
        public int ItemLength { get; set; }

        /// <summary>
        /// 数组每项字节长度，目前选项有：2:表示Int16类型的字节数组, 4: 表示Int32类型的字节数组, 101: 表示float类型4个字节的单精度浮点型的字节数组.
        /// </summary>
        public const int ItemType_Int16 = 2;
        /// <summary>
        /// 数组每项字节长度，目前选项有：2:表示Int16类型的字节数组, 4: 表示Int32类型的字节数组, 101: 表示float类型4个字节的单精度浮点型的字节数组.
        /// </summary>
        public const int ItemType_Int32 = 4;
        /// <summary>
        /// 数组每项字节长度，目前选项有：2:表示Int16类型的字节数组, 4: 表示Int32类型的字节数组, 101: 表示float类型4个字节的单精度浮点型的字节数组.
        /// </summary>
        public const int ItemType_Float32 = 101;

        /// <summary>
        /// 波形数据
        /// </summary>
        public byte[] WaveData { get; set; }

        /// <summary>
        /// 内部的Int16类型数组
        /// </summary>
        private short[] InnerInt16Data { get; set; }
        /// <summary>
        /// 内部的Int32类型数组
        /// </summary>
        private int[] InnerInt32Data { get; set; }
        /// <summary>
        /// 内部的float类型数组
        /// </summary>
        private float[] InnerFloatData { get; set; }

        /// <summary>
        /// 转换为double类型的波形数组
        /// </summary>
        public double[] ToDoubleWave
        {
            get
            {
                if( InnerInt16Data  != null )
                {
                    var data = InnerInt16Data;
                    int len = data.Length;
                    var ret = new double[len];
                    for( int index = 0; index < len; index++ ) ret[index] = data[index] * WaveScale;
                    return ret;
                }
                if( InnerInt32Data != null )
                {
                    var data = InnerInt32Data;
                    int len = data.Length;
                    var ret = new double[len];
                    for( int index = 0; index < len; index++ ) ret[index] = data[index] * WaveScale;
                    return ret;
                }

                var floatData = InnerFloatData;
                if (floatData != null)
                {
                    var ret = new double[floatData.Length];
                    floatData.CopyTo(ret, 0);
                    return ret;
                }
                return null;
            }
        }

        /// <summary>
        /// 初始化内部整形的数据
        /// </summary>
        private void ClearInnerData()
        {
            InnerInt16Data = null;
            InnerInt32Data = null;
            InnerFloatData = null;
        }

        /// <summary>
        /// 初始化内部整形的数据
        /// </summary>
        public void InitInnerIntData()
        {
            ClearInnerData();

            switch( ItemLength )
            {
                case ItemType_Int16:
                    InnerInt16Data = ByteUtils.Bytes2Int16Array( WaveData );
                    return;

                case ItemType_Int32:
                    InnerInt32Data = ByteUtils.Bytes2Int32Array( WaveData );
                    return;

                case ItemType_Float32:
                    InnerFloatData = ByteUtils.Bytes2SingleArray( WaveData );
                    return;
            }
            throw new ArgumentOutOfRangeException( string.Format( "Invalid ItemLength({0})", ItemLength ) );
        }
        /// <summary>
        /// 初始化内部整形的数据
        /// </summary>
        public void InitInnerIntData(IBinaryRead binaryRead, int byteCount)
        {
            ClearInnerData();

            switch( ItemLength )
            {
                case ItemType_Int16:
                    InnerInt16Data = binaryRead.ReadInt16Array(byteCount / ByteUtils.ByteCountPerInt16);
                    return;

                case ItemType_Int32:
                    InnerInt32Data = binaryRead.ReadInt32Array(byteCount / ByteUtils.ByteCountPerInt32);
                    return;

                case ItemType_Float32:
                    InnerFloatData =  binaryRead.ReadSingleArray(byteCount / ByteUtils.ByteCountPerSingle);
                    return;
            }
            throw new ArgumentOutOfRangeException( string.Format( "Invalid ItemLength({0})", ItemLength ) );
        }
    }
}