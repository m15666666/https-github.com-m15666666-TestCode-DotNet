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
                int[] intData = InnerInt32Data;
                if( intData != null )
                {
                    var ret = new double[intData.Length];
                    for( int index = 0; index < ret.Length; index++ )
                    {
                        ret[index] = intData[index] * WaveScale;
                    }
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
        public void InitInnerIntData()
        {
            InnerInt32Data = null;
            InnerFloatData = null;

            switch( ItemLength )
            {
                case ItemType_Int16:
                    short[] int16Data = ByteUtils.Bytes2Int16Array( WaveData );
                    InnerInt32Data = new int[int16Data.Length];
                    int16Data.CopyTo( InnerInt32Data, 0 );
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
    }
}