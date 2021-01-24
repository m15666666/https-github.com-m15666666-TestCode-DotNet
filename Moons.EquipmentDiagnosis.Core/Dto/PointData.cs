using AnalysisAlgorithm.FFT;
using Moons.Common20;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Moons.EquipmentDiagnosis.Core.Dto
{
    /// <summary>
    /// PointData 集合扩展类
    /// </summary>
    public static class PointDatasExtensions
    {
        /// <summary>
        /// 返回最大测量值对应的测点
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public static PointData MaxMeasureValueP(this IEnumerable<PointData> points) => points.MaxItem(p => p.MeasureValue);

        /// <summary>
        /// 清空N天查询记录缓存
        /// </summary>
        /// <param name="points"></param>
        public static void ClearNDaysCache(this IEnumerable<PointData> points) { foreach (var p in points) p.NDaysRecordsCache = null; }

        /// <summary>
        /// 根据directionID返回若干测点
        /// </summary>
        /// <param name="points">测点集合</param>
        /// <param name="directionIds"></param>
        /// <returns>指定的测点，不存在返回null</returns>
        public static PointData[] GetFirstByDirectionIds(this IEnumerable<PointData> points, params int[] directionIds)
        {
            PointData[] ret = new PointData[directionIds.Length];
            for (int i = 0; i < ret.Length; i++) ret[i] = points.FirstOrDefault(p => p.PntDirect_NR == directionIds[i]);
            return ret;
        }
        /// <summary>
        /// 根据directionID返回测点
        /// </summary>
        /// <param name="points">测点集合</param>
        /// <param name="directionId"></param>
        /// <returns>指定的测点，不存在返回null</returns>
        public static PointData GetFirstByDirectionId(this IEnumerable<PointData> points, int directionId) =>
            points.FirstOrDefault(p => p.PntDirect_NR == directionId);
    }

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
        /// 测点唯一编码
        /// </summary>
        public string OnlyCode { get; set; }

        /// <summary>
        /// 测点路径
        /// </summary>
        public string PointPath { get; set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        public int DatType_ID { get; set; }

        /// <summary>
        /// 信号类型
        /// </summary>
        public int SigType_ID { get; set; }
        public bool IsAcc => SigType_ID == (int)SignalTypeIdEnum.Acc;
        public bool IsVel => SigType_ID == (int)SignalTypeIdEnum.Vel;

        /// <summary>
        /// 工程单位编号
        /// </summary>
        public int EngUnit_ID { get; set; }

        /// <summary>
        /// 测点维数(说明见Notes)
        /// </summary>
        public int PntDim_NR { get; set; }

        /// <summary>
        /// 测点方向编号	tinyint		Not null	
        /// 0-水平，1-垂直，2-轴向，3-45度，4-135度。
        /// 对于一维测点表示测点方向；
        /// 对于二维测点表示起始方向，即Chn1的方向，而Chn2方向按照“注”推测
        /// </summary>
        public int PntDirect_NR { get; set; }
        public bool IsAxial => PntDirect_NR == AnalysisData.Constants.PntDirectionID.Axial;
        public bool IsHorizontal => PntDirect_NR == AnalysisData.Constants.PntDirectionID.Horizontal;
        public bool IsVertical => PntDirect_NR == AnalysisData.Constants.PntDirectionID.Vertical;

        /// <summary>
        /// 旋转方向编号	tinyint		Not null	0-逆时针，1-顺时针 默认为逆时针
        /// </summary>
        public int Rotation_NR { get; set; }

        /// <summary>
        /// 位置号
        /// </summary>
        public int PositionNo { get; set; }

        /// <summary>
        /// 默认转速
        /// </summary>
        public int DefaultRotSpeed { get; set; }


        /// <summary>
        /// 超上限报警门限，目前最多2个，可以不填表示没有报警
        /// </summary>
        public double[] HighLimits { get; set; }

        /// <summary>
        /// 测量值上限
        /// </summary>
        public double? MeasurementValueHighLimit { get; set; }

        /// <summary>
        /// 测量值下限
        /// </summary>
        public double? MeasurementValueLowLimit { get; set; }

        /// <summary>
        /// 获得测量值下限，用于过滤掉停机数据
        /// A < 2 m/g^2
        /// V < 0.3 mm/s
        /// </summary>
        /// <returns></returns>
        public double? GetMeasurementValueLowLimit()
        {
            if (MeasurementValueLowLimit.HasValue) return MeasurementValueLowLimit.Value;

            switch( (SignalTypeIdEnum)SigType_ID )
            {
                case SignalTypeIdEnum.Acc:
                    return 2;

                case SignalTypeIdEnum.Vel:
                    return 0.3;
            }
            return null;
        }

        /// <summary>
        /// 历史summary数据
        /// </summary>
        public HistorySummaryData HistorySummaryData { get; set; }
        public double MeasureValue => HistorySummaryData.MeasureValue;

        /// <summary>
        /// 时间波形数据
        /// </summary>
        public TimewaveData TimewaveData { get; set; }

        public bool IsAlm => HistorySummaryData != null && 0 < HistorySummaryData.AlmLevel_ID;

        public bool HasTimewaveData()
        {
            return HistorySummaryData != null && TimewaveData != null;
        }

        /// <summary>
        /// 引用的代表数据库记录的测点对象
        /// </summary>
        public object RefPointOfDb { get; set; }

        /// <summary>
        /// N天查询记录的缓存
        /// </summary>
        public Int32MinuteSummary[] NDaysRecordsCache { get; set; }
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

        public PointDataCollection AccPoints => new PointDataCollection(this.Where(item => item.SigType_ID == (int)SignalTypeIdEnum.Acc));
        public PointDataCollection VelPoints => new PointDataCollection(this.Where(item => item.SigType_ID == (int)SignalTypeIdEnum.Vel));
        public PointDataCollection VelPoints_HasTimewave => new PointDataCollection(this.Where(item => item.SigType_ID == (int)SignalTypeIdEnum.Vel && item.HasTimewaveData() ));

        public IEnumerable<PointData> GetByDirectionId(int directionId) => this.Where(p => p.PntDirect_NR == directionId);
        public int GetMaxPositionNo()
        {
            return this.Max(item => item.PositionNo);
        }

        public PointData GetByPositionNoAndDirectionId( int positionNo, int directionId)
        {
            return this.FirstOrDefault(item => item.PositionNo == positionNo && item.PntDirect_NR == directionId );
        }
        public PointData GetByPositionNoAndDirectionId( int positionNo, int positionNo2, int directionId)
        {
            var ret = GetByPositionNoAndDirectionId(positionNo, directionId);
            return ret ?? GetByPositionNoAndDirectionId(positionNo2, directionId);
        }

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
    /// 历史数据查询条件数据
    /// </summary>
    public class HistoryQueryConditionData
    {
        /// <summary>
        /// 使用的历史数据起始时间
        /// </summary>
        public DateTime HistoryDataBegin { get; set; }

        /// <summary>
        /// 使用的历史数据截止时间
        /// </summary>
        public DateTime HistoryDataEnd { get; set; }

        /// <summary>
        /// 测量值上限
        /// </summary>
        public double? MeasurementValueHighLimit { get; set; }

        /// <summary>
        /// 测量值下限
        /// </summary>
        public double? MeasurementValueLowLimit { get; set; }

        /// <summary>
        /// 查询的记录条数
        /// </summary>
        public int? Count { get; set; }

        /// <summary>
        /// 是否必须包含时间波形数据
        /// </summary>
        public bool MustHasTimewave { get; set; }

        /// <summary>
        /// 是否要测量值最大的数据
        /// </summary>
        public bool MaxFirst { get; set; }
    }

    /// <summary>
    /// 历史summary数据
    /// </summary>
    [Serializable]
    public class HistorySummaryData
    {
        //public object Id { get; set; }

        public object PartitionId { get; set; }

        //public string PointOnlyCode { get; set; }

        /// <summary>
        /// 是MongoDB的外键
        /// </summary>
        public string HistoryDataRef_Id { get; set; }


        //public object Point_ID { get; set; }

        //public DateTime SampTime_DT { get; set; }

        public DateTime SampTimeGMT_DT { get; set; }

        /// <summary>
        ///     采样频率
        /// </summary>
        public double SampleFreq_NR { get; set; }

        ///// <summary>
        /////     倍频系数
        ///// </summary>
        //public int SampMod_NR { get; set; }

        ///// <summary>
        /////     数据类型编号
        ///// </summary>
        //public byte DatType_NR { get; set; }

        ///// <summary>
        /////     信号类型编号
        ///// </summary>
        //public int SigType_NR { get; set; }

        ///// <summary>
        /////     数据长度
        ///// </summary>
        //public int DatLen_NR { get; set; }

        public int RotSpeed_NR { get; set; }
        public float F0 => RotSpeed_NR / 60f;

        //public double MinFreq_NR { get; set; }

        public int AlmLevel_ID { get; set; }

        //public int DataGroup_NR { get; set; }

        //public int AvgNum_NR { get; set; }

        //public long Alm_ID { get; set; }

        //public int EngUnit_ID { get; set; }

        //public long Synch_NR { get; set; }

        //public int OrbitAxis_ID { get; set; }

        //public int MasterSlave_ID { get; set; }

        /// <summary>
        /// 测量值
        /// </summary>
        public double MeasureValue { get; set; }

        ///// <summary>
        ///// 峰值
        ///// </summary>
        //public double P { get; set; }

        ///// <summary>
        ///// 峰峰值
        ///// </summary>
        //public double PP { get; set; }

        ///// <summary>
        ///// 有效值
        ///// </summary>
        //public double RMS { get; set; }

        ///// <summary>
        ///// 均值
        ///// </summary>
        //public double Mean { get; set; }

        ///// <summary>
        ///// 波形指标
        ///// </summary>
        //public double ShapeFactor { get; set; }

        ///// <summary>
        ///// 峭度
        ///// </summary>
        //public double KurtoFactor { get; set; }

    }

    public class HistorySummaryDataCollection : List<HistorySummaryData>
    {

    }

    /// <summary>
    /// 时间波形数据类
    /// </summary>
    public class TimewaveData
    {
        public void Init(AnalysisAlgorithm.FFT.SpectrumUtils spectrumUtils)
        {
            SpectrumUtils = spectrumUtils;
        }

        public SpectrumUtils SpectrumUtils { get; private set; }

        /// <summary>
        /// 时间波形
        /// </summary>
        public double[] Timewave { get; set; }

        /// <summary>
        /// 频率分辨率
        /// </summary>
        public double FrequencyResolution => SpectrumUtils.FrequencyResolution;

        /// <summary>
        /// 频率分辨率小于等于0.5hz，可以诊断电气故障
        /// </summary>
        public bool IsFreqResolutionMatchELECTRCFault => FrequencyResolution <= 0.5;

        /// <summary>
        /// 幅值谱或有效值谱中的整数分频，下标为0对应1X
        /// </summary>
        public double[] XFFT => SpectrumUtils.XFFT;

        /// <summary>
        /// 幅值谱或有效值谱中的0.5整数分频，下标为0对应0.5X
        /// </summary>
        public double[] XHalfFFT => SpectrumUtils.XHalfFFT;

        /// <summary>
        /// 幅值谱或有效值谱中的100hz整数分频，下标为0对应100hz
        /// </summary>
        public double[] XHz100 => SpectrumUtils.XHz100;

        /// <summary>
        /// 100Hz分量
        /// </summary>
        public double Hz100 => SpectrumUtils.Hz100;

        /// <summary>
        /// 
        /// </summary>
        public double Overall => SpectrumUtils.Overall;
        public double HighestPeak => SpectrumUtils.HighestPeak;

        public double X0_5 => SpectrumUtils.X0_5;
        public double X1_5 => SpectrumUtils.X1_5;
        public double X2_5 => SpectrumUtils.X2_5;
        public double X3_5 => SpectrumUtils.X3_5;
        public double X4_5 => SpectrumUtils.X4_5;
        public double X5_5 => SpectrumUtils.X5_5;

        public double X1 => SpectrumUtils.X1;
        public double X2 => SpectrumUtils.X2;
        public double X3 => SpectrumUtils.X3;
        public double X4 => SpectrumUtils.X4;
        public double X5 => SpectrumUtils.X5;
        public double X6 => SpectrumUtils.X6;
    }

    /// <summary>
    /// 使用int32表示采样时间对应的分钟数
    /// </summary>
    public class Int32MinuteSummary
    {
        /// <summary>
        /// 采样时间
        /// </summary>
        public int SampleMinute { get; set; }

        /// <summary>
        /// 分钟转速
        /// </summary>
        public int Rev { get; set; }

        /// <summary>
        /// 测量值
        /// </summary>
        public double MeasureValue { get; set; }
    }
}
