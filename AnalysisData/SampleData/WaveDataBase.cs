using System;
using System.IO;

namespace AnalysisData.SampleData
{
    [Serializable]
    public abstract class WaveDataBase : TrendData
    {
        #region 自定义函数

        /// <summary>
        /// 写入FeatureData对象
        /// </summary>
        /// <param name="writer">BinaryWriter</param>
        /// <param name="feature">FeatureData对象</param>
        protected static void WriteFeature( BinaryWriter writer, FeatureData feature )
        {
            var features = new[]
                               {
                                   feature.P,
                                   feature.PP,
                                   feature.Mean,
                                   feature.RMS,
                                   feature.ShapeFactor,
                                   feature.ImpulseFactor,
                                   feature.CrestFactor,
                                   feature.ClearanceFactor,
                                   feature.SkewFactor,
                                   feature.KurtoFactor,
                               };
            WriteDoubleArray( writer, features );
        }

        /// <summary>
        /// 读出FeatureData对象
        /// </summary>
        /// <param name="reader">BinaryReader</param>
        /// <returns>FeatureData对象</returns>
        protected static FeatureData ReadFeature( BinaryReader reader )
        {
            var ret = new FeatureData();

            ret.P = reader.ReadDouble();
            ret.PP = reader.ReadDouble();
            ret.Mean = reader.ReadDouble();
            ret.RMS = reader.ReadDouble();
            ret.ShapeFactor = reader.ReadDouble();
            ret.ImpulseFactor = reader.ReadDouble();
            ret.CrestFactor = reader.ReadDouble();
            ret.ClearanceFactor = reader.ReadDouble();
            ret.SkewFactor = reader.ReadDouble();
            ret.KurtoFactor = reader.ReadDouble();

            return ret;
        }

        /// <summary>
        /// 写入double数组
        /// </summary>
        /// <param name="writer">BinaryWriter</param>
        /// <param name="values">double[]</param>
        protected static void WriteDoubleArray( BinaryWriter writer, double[] values )
        {
            foreach( double value in values )
            {
                writer.Write( value );
            }
        }

        /// <summary>
        /// 读出double数组
        /// </summary>
        /// <param name="reader">BinaryReader</param>
        /// <param name="length">数组长度</param>
        /// <returns>double数组</returns>
        protected static double[] ReadDoubleArray( BinaryReader reader, int length )
        {
            var ret = new double[length];
            for( int index = 0; index < length; index++ )
            {
                ret[index] = reader.ReadDouble();
            }
            return ret;
        }

        /// <summary>
        /// 写入short数组
        /// </summary>
        /// <param name="writer">BinaryWriter</param>
        /// <param name="values">short[]</param>
        protected static void WriteShortArray( BinaryWriter writer, short[] values )
        {
            foreach( short value in values )
            {
                writer.Write( value );
            }
        }

        /// <summary>
        /// 读出short数组
        /// </summary>
        /// <param name="reader">BinaryReader</param>
        /// <param name="length">数组长度</param>
        /// <returns>short数组</returns>
        protected static short[] ReadShortArray( BinaryReader reader, int length )
        {
            var ret = new short[length];
            for( int index = 0; index < length; index++ )
            {
                ret[index] = reader.ReadInt16();
            }
            return ret;
        }

        /// <summary>
        /// 写ShortWaveData对象
        /// </summary>
        /// <param name="writer">BinaryWriter</param>
        /// <param name="shortWaveData">ShortWaveData对象</param>
        protected static void Write( BinaryWriter writer, ShortWaveData shortWaveData )
        {
            shortWaveData.InitShortWave();

            writer.Write( shortWaveData.WaveScale );

            WriteShortArray( writer, shortWaveData.ShortWave );
        }

        /// <summary>
        /// 读出ShortWaveData对象
        /// </summary>
        /// <param name="reader">BinaryReader</param>
        /// <param name="shortWaveData">ShortWaveData</param>
        protected void Read( BinaryReader reader, ShortWaveData shortWaveData )
        {
            shortWaveData.WaveScale = reader.ReadDouble();

            shortWaveData.ShortWave = ReadShortArray( reader, DataLength );
        }

        #endregion
    }
}