using AnalysisData.Constants;

namespace SampleServer.Upload2DB
{
    /// <summary>
    /// 测点信息数据类
    /// </summary>
    public class PointInfoData
    {
        public PointInfoData()
        {
            RevRatio = 1;
            MeasurementValueFeatureValueID = FeatureValueID.Measurement;
        }

        /// <summary>
        /// 测点维数
        /// </summary>
        public byte PntDim_NR { get; set; }

        /// <summary>
        /// 数据类型编号
        /// </summary>
        public byte DatType_NR { get; set; }

        /// <summary>
        /// 信号类型编号
        /// </summary>
        public short SigType_NR { get; set; }

        /// <summary>
        /// 单位ID
        /// </summary>
        public int EngUnit_ID { get; set; }

        /// <summary>
        /// 测点的转速比例因子，默认为1
        /// </summary>
        public double RevRatio { get; set; }

        /// <summary>
        /// 测量值对应的指标ID，默认值是测量值
        /// </summary>
        public int MeasurementValueFeatureValueID { get; set; }
    }
}