using System;
using System.IO;
using AnalysisData.Constants;
using AnalysisData.SampleData;
using AnalysisUtils;
using Moons.Common20;

namespace SampleServer.Upload2DB
{
    /// <summary>
    /// 上传数据库类的基类
    /// </summary>
    public abstract class UploaderBase : DisposableBase
    {
        /// <summary>
        /// 检查数据文件的版本号
        /// </summary>
        /// <param name="paths">数据文件路径</param>
        protected void CheckDataFileVersion( params string[] paths )
        {
            foreach( string path in paths )
            {
                using(
                    var reader = new BinaryReader( File.Open( path, FileMode.Open, FileAccess.Read, FileShare.None ) ) )
                {
                    // 比较版本号
                    int version = reader.ReadInt32();
                    const int currentVersion = SampleDataUtils.DataFileVersion;
                    if( version != currentVersion )
                    {
                        throw new ArgumentException( string.Format( "File version error ({0}), {1} != {2}", path, version,
                                                                    currentVersion ) );
                    }
                }
            }
        }

        /// <summary>
        /// 移除文件头
        /// </summary>
        /// <param name="reader">BinaryReader</param>
        protected void RemoveFileHead( BinaryReader reader )
        {
            // 读出版本号
            reader.ReadInt32();

            // 读出文件的创建时间
            reader.ReadDouble();
        }

        /// <summary>
        /// 获得测量值
        /// </summary>
        /// <param name="featureData">FeatureData</param>
        /// <param name="pointInfo">PointInfoData</param>
        /// <returns>测量值</returns>
        protected static double GetMeasurementValue( FeatureData featureData, PointInfoData pointInfo )
        {
            return FeatureValueID.GetMeasurementValueByMeasurementValueFeatureValueID( featureData, pointInfo.SigType_NR,
                                                                                       pointInfo.
                                                                                           MeasurementValueFeatureValueID );
        }

        /// <summary>
        /// 获得指标
        /// </summary>
        /// <param name="featureData">特征指标</param>
        /// <param name="pointInfo">PointInfoData</param>
        /// <param name="featureValueIDs">指标编号</param>
        /// <param name="featureValues">指标</param>
        public static void GetFeatureValues( FeatureData featureData, PointInfoData pointInfo,
                                             out byte[] featureValueIDs,
                                             out double[] featureValues )
        {
            featureValueIDs = FeatureValueID.FeatureValueIDs;

            featureValues = new[]
                                {
                                    featureData.MeasurementValue, featureData.P,
                                    featureData.PP, featureData.RMS, featureData.Mean, featureData.ShapeFactor, featureData.KurtoFactor
                                };
        }

        #region GetObjectID

        ///// <summary>
        ///// 获得追忆数据ID
        ///// </summary>
        ///// <param name="conn">DbConnection</param>
        ///// <returns>第一个追忆数据ID</returns>
        //protected long GetRetrospectID(DbConnection conn)
        //{
        //    return GetBigObjectID(conn, "GetRetrospectID", "Retrospect_Summary", "Retrospect_ID", 1);
        //}

        ///// <summary>
        ///// 获得起停机数据ID
        ///// </summary>
        ///// <param name="conn">DbConnection</param>
        ///// <returns>第一个起停机数据ID</returns>
        //protected long GetStartStopDataID(DbConnection conn)
        //{
        //    return GetBigObjectID(conn, "GetStartStopDataID", "StartStop_Data", "StartStopData_ID", 1);
        //}

        ///// <summary>
        ///// 获得起停机ID
        ///// </summary>
        ///// <param name="conn">DbConnection</param>
        ///// <returns>第一个起停机ID</returns>
        //protected int GetStartStopID(DbConnection conn)
        //{
        //    return GetObjectID(conn, "GetStartStopID", "StartStop_Summary", "StartStop_ID", 1);
        //}

        #endregion

        #region Dispose

        protected override void Dispose( bool disposing )
        {
            if( IsDisposed )
            {
                return;
            }

            if( disposing )
            {
            }

            //  一定要调用基类的Dispose函数
            base.Dispose( disposing );
        }

        #endregion
    }
}