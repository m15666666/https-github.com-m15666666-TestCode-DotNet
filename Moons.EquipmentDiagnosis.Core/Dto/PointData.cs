using System;
using System.Collections.Generic;
using System.Linq;

namespace Moons.EquipmentDiagnosis.Core.Dto
{
    /// <summary>
    /// 测点数据
    /// </summary>
    [Serializable]
    public class PointData
    {
        /// <summary>
        /// 测点编号
        /// </summary>
        public object Point_ID { get; set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        public int DatType_ID { get; set; }

        /// <summary>
        /// 信号类型
        /// </summary>
        public int SigType_ID { get; set; }

        /// <summary>
        /// 工程单位编号
        /// </summary>
        public int EngUnit_ID { get; set; }

        /// <summary>
        /// 测点维数(说明见Notes)
        /// </summary>
        public int PntDim_NR { get; set; }

        /// <summary>
        /// 旋转方向ID
        /// </summary>
        public int Rotation_ID { get; set; }

        /// <summary>
        /// 位置号
        /// </summary>
        public int PositionNo { get; set; }

        /// <summary>
        /// 超上限报警门限，目前最多2个，可以不填表示没有报警
        /// </summary>
        public double[] HighLimits { get; set; }

        /// <summary>
        /// 历史summary数据
        /// </summary>
        public HistorySummaryData HistorySummaryData { get; set; }

        /// <summary>
        /// 时间波形数据
        /// </summary>
        public TimewaveData TimewaveData { get; set; }

        public bool IsAlm => HistorySummaryData != null && 0 < HistorySummaryData.AlmLevel_ID;
    }
    
    public class PointDataCollection : List<PointData>
    {
        public PointDataCollection()
        {
        }
        public PointDataCollection(IEnumerable<PointData> points) : base(points)
        {
        }

        public void Init()
        {
            foreach( var p in this )
            {
                switch( (SignalTypeIdEnum)p.SigType_ID )
                {
                    case SignalTypeIdEnum.Acc:
                        {
                            if(p.HighLimits == null || p.HighLimits.Length == 0)
                            {
                                p.HighLimits = new double[] { 40 };
                            }
                        }
                        break;

                    case SignalTypeIdEnum.Vel:
                        break;

                    case SignalTypeIdEnum.Temperature:
                        break;
                }
                CalcAlm(p);
            }
        }

        public PointDataCollection GetAccPoints => new PointDataCollection(this.Where(item => item.SigType_ID == (int)SignalTypeIdEnum.Acc));
        public PointDataCollection GetVelPoints => new PointDataCollection(this.Where(item => item.SigType_ID == (int)SignalTypeIdEnum.Vel));

        private void CalcAlm(PointData p)
        {
            var highLimits = p.HighLimits;
            if (highLimits == null || highLimits.Length == 0) return;

            var summary = p.HistorySummaryData;
            if (summary == null) return;

            if (highLimits[0] < summary.MeasureValue) summary.AlmLevel_ID = 2;
        }
    }

    /// <summary>
    /// 历史summary数据
    /// </summary>
    [Serializable]
    public class HistorySummaryData
    {
        /// <summary>
        ///     采样频率
        /// </summary>
        public double SampleFreq { get; set; }

        /// <summary>
        ///     倍频系数
        /// </summary>
        public int MultiFreq { get; set; }

        /// <summary>
        ///     数据类型编号
        /// </summary>
        public byte DatType_NR { get; set; }

        /// <summary>
        ///     信号类型编号
        /// </summary>
        public int SigType_NR { get; set; }

        /// <summary>
        ///     数据长度
        /// </summary>
        public int DatLen_NR { get; set; }

        public long Partition_ID { get; set; }

        public object History_ID { get; set; }

        public object Point_ID { get; set; }

        public DateTime SampTime_DT { get; set; }

        public DateTime SampTimeGMT_DT { get; set; }

        public int RotSpeed_NR { get; set; }

        public double MinFreq_NR { get; set; }

        public int AlmLevel_ID { get; set; }

        public int DataGroup_NR { get; set; }

        public int AvgNum_NR { get; set; }

        public long Alm_ID { get; set; }

        public int EngUnit_ID { get; set; }

        public long Synch_NR { get; set; }

        public int OrbitAxis_ID { get; set; }

        public int MasterSlave_ID { get; set; }

        /// <summary>
        /// 测量值
        /// </summary>
        public double MeasureValue { get; set; }

        /// <summary>
        /// 峰值
        /// </summary>
        public double P { get; set; }

        /// <summary>
        /// 峰峰值
        /// </summary>
        public double PP { get; set; }

        /// <summary>
        /// 有效值
        /// </summary>
        public double RMS { get; set; }

        /// <summary>
        /// 均值
        /// </summary>
        public double Mean { get; set; }

        /// <summary>
        /// 波形指标
        /// </summary>
        public double ShapeFactor { get; set; }

        /// <summary>
        /// 峭度
        /// </summary>
        public double KurtoFactor { get; set; }

    }

    public class HistorySummaryDataCollection : List<HistorySummaryData>
    {

    }

    /// <summary>
    /// 时间波形数据类
    /// </summary>
    public class TimewaveData
    {
        /// <summary>
        /// 时间波形
        /// </summary>
        public double[] Timewave { get; set; }

        /// <summary>
        /// 幅值谱*0.707转换为有效值谱
        /// </summary>
        public double[] FFT { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double Overall { get; set; }
        public double HighestRMS { get; set; }
        public double X0_5 { get; set; }
        public double X1_5 { get; set; }
        public double X2_5 { get; set; }
        public double X3_5 { get; set; }
        public double X4_5 { get; set; }
        public double X5_5 { get; set; }
        public double X1 { get; set; }
        public double X2 { get; set; }
        public double X3 { get; set; }
        public double X4 { get; set; }
        public double X5 { get; set; }
        public double X6 { get; set; }
    }
}
