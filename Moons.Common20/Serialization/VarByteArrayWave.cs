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
        /// 数组每项字节长度，目前有：2，4两个选项，分别表示Int16、Int32类型的数组。
        /// </summary>
        public int ItemLength { get; set; }

        /// <summary>
        /// 波形数据
        /// </summary>
        public byte[] WaveData { get; set; }

        /// <summary>
        /// 内部的Int32类型数组
        /// </summary>
        private int[] InnerInt32Data { get; set; }

        /// <summary>
        /// 转换为double类型的波形数组
        /// </summary>
        public double[] ToDoubleWave
        {
            get
            {
                int[] intData = InnerInt32Data;
                if( intData == null )
                {
                    return null;
                }

                var ret = new double[intData.Length];
                for( int index = 0; index < ret.Length; index++ )
                {
                    ret[index] = intData[index] * WaveScale;
                }
                return ret;
            }
        }

        /// <summary>
        /// 初始化内部整形的数据
        /// </summary>
        public void InitInnerIntData()
        {
            InnerInt32Data = null;

            switch( ItemLength )
            {
                case 2:
                    short[] int16Data = ByteUtils.Bytes2Int16Array( WaveData );
                    InnerInt32Data = new int[int16Data.Length];
                    int16Data.CopyTo( InnerInt32Data, 0 );
                    return;

                case 4:
                    InnerInt32Data = ByteUtils.Bytes2Int32Array( WaveData );
                    return;
            }
            throw new ArgumentOutOfRangeException( string.Format( "Invalid ItemLength({0})", ItemLength ) );
        }
    }
}