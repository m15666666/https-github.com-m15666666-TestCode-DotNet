using System;
using System.Data;
using System.Data.Common;
using System.IO;
using AnalysisAlgorithm;


using Moons.Common20;
using Moons.Common20.LongFile;

namespace SampleServer.Upload2DB
{
    /// <summary>
    /// 起停机数据上传数据库类
    /// </summary>
    //public class StartStopSampleUploader : UploaderBase
    //{
    //    private DbConnection _dbConnection;

    //    /// <summary>
    //    /// ctor
    //    /// </summary>
    //    /// <param name="filePath">起停机文件路径</param>
    //    public StartStopSampleUploader(string filePath)
    //    {
    //        StartStopFilePath = filePath;
    //    }

    //    /// <summary>
    //    /// 起停机文件路径
    //    /// </summary>
    //    public string StartStopFilePath { get; set; }

    //    private DbConnection DbConnection
    //    {
    //        get { return _dbConnection; }
    //    }

    //    /// <summary>
    //    /// 上传起停机数据
    //    /// </summary>
    //    /// <param name="dataLength">数据长度</param>
    //    public void UploadData(int dataLength)
    //    {
    //        // 文件不存在则返回
    //        CheckStartStopFilePath();

    //        // 检查版本号
    //        CheckDataFileVersion(StartStopFilePath);

    //        using (EntityConnection conn = Context.CreateEntityConnection())
    //        {
    //            _dbConnection = conn.StoreConnection;

    //            HashDictionary<string, StartStopData_Point> pointID2StartStopData = GetStartStopData_Points(dataLength);
    //            if (pointID2StartStopData == null || pointID2StartStopData.Count == 0)
    //            {
    //                throw new Exception("不包含有效的数据");
    //            }

    //            using (MEMSEntitiesWrap entitiesWrap = Context.CreateMemsEntities())
    //            {
    //                using (FlatFileReader reader = GetStartStopFileReader())
    //                {
    //                    var pointID2Waves = new HashDictionary<int, PointWaveData>();

    //                    // 批量插入的数量
    //                    const int BatchInsertCount = 100;
    //                    int insertCount = 0;

    //                    while (true)
    //                    {
    //                        byte[] bytes = reader.ReadBytes();

    //                        // 读到末尾，结束
    //                        if (bytes == null)
    //                        {
    //                            break;
    //                        }

    //                        TrendData trendData = SampleDataUtils.ReadFromBytes(bytes);

    //                        StartStopData_Point startStopData_Point = pointID2StartStopData[trendData.PointIDString];

    //                        // 测点信息不存在，跳过这条数据
    //                        if (startStopData_Point == null)
    //                        {
    //                            break;
    //                        }

    //                        int pointID = trendData.PointID;
    //                        PointWaveData waves = pointID2Waves[pointID];
    //                        if (waves == null)
    //                        {
    //                            pointID2Waves[pointID] = waves = new PointWaveData {DataLength = dataLength};
    //                        }

    //                        waves.AddData(trendData);

    //                        while (waves.HasEnoughData)
    //                        {
    //                            // 如果是第一条数据则立即保存
    //                            if (UploadOneData(entitiesWrap, startStopData_Point, waves.Pop()))
    //                            {
    //                                entitiesWrap.SaveChanges();
    //                            }

    //                            insertCount++;

    //                            if (insertCount == BatchInsertCount)
    //                            {
    //                                insertCount = 0;

    //                                entitiesWrap.SaveChanges();
    //                                entitiesWrap.Clear();
    //                            }
    //                        }
    //                    } // while( true )

    //                    // 保存最后插入的数据
    //                    if (0 < insertCount)
    //                    {
    //                        entitiesWrap.SaveChanges();
    //                        entitiesWrap.Clear();
    //                    }
    //                } // using( var reader = GetStartStopFileReader() )
    //            } // using( MEMSEntitiesWrap entitiesWrap = MEMSEntitiesWrap.Create() )
    //        } // using ( var conn = SqlService.CreateEntityConnection() )
    //    }

    //    /// <summary>
    //    /// 检查追忆文件路径
    //    /// </summary>
    //    private void CheckStartStopFilePath()
    //    {
    //        if (!File.Exists(StartStopFilePath))
    //        {
    //            throw new ArgumentException(string.Format("文件({0})不存在", StartStopFilePath));
    //        }
    //    }

    //    /// <summary>
    //    /// 创建StartStop_Waveform对象
    //    /// </summary>
    //    /// <param name="channelNumber">通道号</param>
    //    /// <param name="wave">波形</param>
    //    /// <returns>StartStop_Waveform对象</returns>
    //    private static StartStop_Waveform CreateWaveForm(byte channelNumber, double[] wave)
    //    {
    //        CompressionType compressionType;
    //        double waveScale;
    //        byte[] bytes = AnalysisDataConvertUtils.DoubleToByte(wave, out waveScale, out compressionType);

    //        return new StartStop_Waveform
    //                   {
    //                       ChnNo_NR = channelNumber,
    //                       Wave_GR = bytes,
    //                       WaveScale_NR = waveScale,
    //                       Compress_NR = (byte) compressionType
    //                   };
    //    }

    //    /// <summary>
    //    /// 上传一条数据
    //    /// </summary>
    //    /// <param name="entitiesWrap">MEMSEntitiesWrap</param>
    //    /// <param name="startStopData_Point">StartStopData_Point</param>
    //    /// <param name="trendData">TrendData</param>
    //    /// <returns>是否加入了第一条数据</returns>
    //    private bool UploadOneData(MEMSEntitiesWrap entitiesWrap, StartStopData_Point startStopData_Point,
    //                               TrendData trendData)
    //    {
    //        bool isFirstData = false;

    //        var startStopData = new StartStop_Data
    //                                {
    //                                    // 生成新的波形序号
    //                                    Order_NR = startStopData_Point.NewOrder_NR,
    //                                    SampTime_DT = trendData.SampleTime
    //                                };

    //        // 第一条数据时加入摘要数据
    //        if (startStopData_Point.Summary == null)
    //        {
    //            isFirstData = true;

    //            startStopData_Point.StartStop_ID = GetStartStopID(DbConnection);

    //            //插入启停机摘要数据
    //            startStopData_Point.Summary = new StartStop_Summary
    //                                              {
    //                                                  StartStop_ID =
    //                                                      startStopData_Point.StartStop_ID,
    //                                                  DJ_SpecImplement_ID =
    //                                                      startStopData_Point.DJ_SpecImplement_ID,
    //                                                  BgnTime_DT = startStopData_Point.BgnTime_DT,
    //                                                  EndTime_DT = startStopData_Point.EndTime_DT,
    //                                                  BgnSpeed_NR =
    //                                                      startStopData_Point.BgnSpeed_NR,
    //                                                  EndSpeed_NR =
    //                                                      startStopData_Point.EndSpeed_NR,
    //                                                  PntDim_NR = startStopData_Point.PntDim_NR,
    //                                                  DatType_NR = startStopData_Point.DatType_NR,
    //                                                  SigType_NR = startStopData_Point.SigType_NR,
    //                                                  DatLen_NR = startStopData_Point.DatLen_NR,
    //                                                  SampleFreq_NR =
    //                                                      startStopData_Point.SampleFreq_NR,
    //                                                  SampMod_NR = startStopData_Point.SampMod_NR
    //                                              };

    //            entitiesWrap.AddToStartStop_Summary(startStopData_Point.Summary);
    //        }

    //        //各个通道的工频下的实.虚部值
    //        var waveData = (WaveDataBase) trendData;
    //        bool isOrder = 0 < waveData.MultiFreq;
    //        var rev = (int) (waveData.Rev*startStopData_Point.RevRatio);
    //        double f0 = isOrder ? 1 : rev/60f;
    //        double fs = isOrder ? waveData.MultiFreq : waveData.SampleFreq;

    //        float? re1 = null, im1 = null, re2 = null, im2 = null;
    //        double re, im;

    //        StartStop_Waveform startStopWaveform1 = null;
    //        StartStop_Waveform startStopWaveform2 = null;

    //        if (trendData is TimeWaveData_1D)
    //        {
    //            var timeWaveData = trendData as TimeWaveData_1D;

    //            startStopWaveform1 = CreateWaveForm(ChannelNumber.One, timeWaveData.Wave);

    //            SpectrumBasic.GetBaseFreqReIm(fs, f0, timeWaveData.Wave, out re, out im);

    //            re1 = (float) re;
    //            im1 = (float) im;
    //        }
    //        else if (trendData is TimeWaveData_2D)
    //        {
    //            var timeWaveData = trendData as TimeWaveData_2D;

    //            startStopWaveform1 = CreateWaveForm(ChannelNumber.One, timeWaveData.Wave_1D);

    //            startStopWaveform2 = CreateWaveForm(ChannelNumber.Two, timeWaveData.Wave_2D);

    //            SpectrumBasic.GetBaseFreqReIm(fs, f0, timeWaveData.Wave_1D, out re, out im);
    //            re1 = (float) re;
    //            im1 = (float) im;

    //            SpectrumBasic.GetBaseFreqReIm(fs, f0, timeWaveData.Wave_2D, out re, out im);
    //            re2 = (float) re;
    //            im2 = (float) im;
    //        }

    //        startStopData.SpRePart1_NR = re1;
    //        startStopData.SpImPart1_NR = im1;

    //        startStopData.SpRePart2_NR = re2;
    //        startStopData.SpImPart2_NR = im2;

    //        startStopData.RotSpeed_NR = rev;

    //        long startStopDataID = startStopData.StartStopData_ID = GetStartStopDataID(DbConnection);

    //        StartStop_Summary startStop_Summary = startStopData_Point.Summary;
    //        if (startStop_Summary.EntityState == EntityState.Detached)
    //        {
    //            entitiesWrap.Attach(startStop_Summary);
    //        }

    //        startStop_Summary.StartStop_Data.Add(startStopData);

    //        startStopWaveform1.StartStopData_ID = startStopDataID;
    //        startStopData.StartStop_Waveform.Add(startStopWaveform1);

    //        if (startStopWaveform2 != null)
    //        {
    //            startStopWaveform2.StartStopData_ID = startStopDataID;
    //            startStopData.StartStop_Waveform.Add(startStopWaveform2);
    //        }

    //        return isFirstData;
    //    }

    //    /// <summary>
    //    /// 返回起停机文件的阅读器
    //    /// </summary>
    //    /// <returns>起停机文件的阅读器</returns>
    //    private FlatFileReader GetStartStopFileReader()
    //    {
    //        var ret = new FlatFileReader {Paths = new[] {StartStopFilePath}};

    //        ret.OnOpenBinary4Read += RemoveFileHead;

    //        return ret;
    //    }

    //    /// <summary>
    //    /// 扫描文件，返回PointID对StartStopData_Point对象的字典，出错返回null
    //    /// </summary>
    //    /// <param name="dataLength">数据长度</param>
    //    /// <returns>PointID对StartStopData_Point对象的字典</returns>
    //    private HashDictionary<string, StartStopData_Point> GetStartStopData_Points(int dataLength)
    //    {
    //        var ret = new HashDictionary<string, StartStopData_Point>();

    //        using (FlatFileReader reader = GetStartStopFileReader())
    //        {
    //            while (true)
    //            {
    //                byte[] bytes = reader.ReadBytes();

    //                // 读到末尾，结束
    //                if (bytes == null)
    //                {
    //                    break;
    //                }

    //                TrendData trendData = SampleDataUtils.ReadFromBytes(bytes);

    //                // 字节数组出错，返回
    //                if (trendData == null)
    //                {
    //                    throw new Exception("字节数组出错");
    //                }

    //                int pointID = trendData.PointID;

    //                // 读取测点信息
    //                PointInfoData pointInfo = GetPointInfo(DbConnection, pointID);

    //                // 测点信息不存在，跳过这条数据
    //                if (pointInfo == null)
    //                {
    //                    TraceUtils.Info(string.Format("测点({0})信息不存在", pointID));
    //                    continue;
    //                }

    //                double revRatio = pointInfo.RevRatio;

    //                // 起停机上传的必须都是振动数据
    //                var waveData = trendData as WaveDataBase;
    //                if (waveData == null)
    //                {
    //                    throw new Exception("启停机上传的必须都是振动数据");
    //                }

    //                string key = trendData.PointIDString;
    //                StartStopData_Point startStopData_Point = ret[key];
    //                if (startStopData_Point == null)
    //                {
    //                    startStopData_Point = new StartStopData_Point();
    //                    ret[key] = startStopData_Point;
    //                }

    //                // 转速
    //                {
    //                    var rev = (int) (waveData.Rev*revRatio);
    //                    if (!startStopData_Point.BgnSpeed_NR.HasValue)
    //                    {
    //                        startStopData_Point.BgnSpeed_NR = rev;
    //                    }

    //                    startStopData_Point.EndSpeed_NR = rev;
    //                }

    //                // 采样时间
    //                {
    //                    DateTime sampleTime = waveData.SampleTime;
    //                    if (!startStopData_Point.BgnTime_DT.HasValue)
    //                    {
    //                        startStopData_Point.BgnTime_DT = sampleTime;
    //                    }

    //                    startStopData_Point.EndTime_DT = sampleTime;
    //                }

    //                // 数据长度
    //                startStopData_Point.DatLen_NR = dataLength;

    //                // 采样频率
    //                startStopData_Point.SampleFreq_NR = waveData.SampleFreq;

    //                // 倍频系数
    //                startStopData_Point.SampMod_NR = (byte) waveData.MultiFreq;

    //                // 数据类型
    //                startStopData_Point.DatType_NR = pointInfo.DatType_NR;

    //                // 实施方
    //                startStopData_Point.DJ_SpecImplement_ID = pointID;

    //                // 维数
    //                startStopData_Point.PntDim_NR = pointInfo.PntDim_NR;

    //                // 信号类型
    //                startStopData_Point.SigType_NR = pointInfo.SigType_NR;

    //                startStopData_Point.RevRatio = revRatio;
    //            }
    //        } // using( FlatFileReader reader = GetStartStopFileReader() )

    //        return ret;
    //    }
    //}
}