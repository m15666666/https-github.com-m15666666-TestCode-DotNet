﻿using log4net;
using log4net.Config;
using log4net.Repository;
using Moons.Common20;
using Moons.Log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Test.ShaSteel.WebAPI.Core;
using System.Diagnostics;
using System.Threading;

namespace Test.ShaSteel.WebAPI.Client
{
    class Program
    {
        const string TimePattern = "yyyy-MM-dd HH:mm:ss";

        static async Task Main(string[] args)
        {
            ILoggerRepository repository = LogManager.CreateRepository("NETCoreRepository");
            // 默认简单配置，输出至控制台
            //BasicConfigurator.Configure(repository);
            XmlConfigurator.Configure(repository, new System.IO.FileInfo("log4net.config"));
            ILog log = LogManager.GetLogger(repository.Name, "1");

            EnvironmentUtils.Logger = new Log4netWrapper(log);
            TraceUtils.Info("Starting Test.ShaSteel.WebAPI.Client. time stamp: 2019-11-11.");

            var url = ConfigurationManager.AppSettings["url"];
            TraceUtils.Info($"url:{url}.");

            
            int WaveLength = 4096;
            int ByteWaveLength = WaveLength*2;
            Stopwatch watch = new Stopwatch();

            // 并行
            //int httpClientCount = 40;
            //Client[] clients = new Client[httpClientCount];
            //for( int i = 0; i < clients.Length; i++)
            //    clients[i] = new Client(url, new HttpClient());

            using (HttpClient httpClient = new HttpClient())
            {
                var client = new Client(url, httpClient);
                while (true)
                {
                    Console.WriteLine("Enter q to quit.");
                    var line = Console.ReadLine();
                    
                    if ("q".Equals(line, StringComparison.InvariantCultureIgnoreCase)) break;

                    switch (line)
                    {
                        case "512":
                        case "1024":
                        case "2048":
                        case "4096":
                        case "8192":
                            WaveLength = int.Parse(line);
                            ByteWaveLength = WaveLength * 2;
                            break;
                    }

                    string[] parts = new string[0];
                    parts = line.Split(new char[]{ ' '}, StringSplitOptions.RemoveEmptyEntries);
                    int callCount = 1;
                    const string CallPrefix = "call=";
                    if (-1 < line.IndexOf(CallPrefix)) 
                    {
                        callCount = int.Parse(parts.First(item => item.StartsWith(CallPrefix)).Substring(CallPrefix.Length));
                    }

                    TraceUtils.Info($"CallWebApi start. callCount: {callCount}");
                    int count = 0;
                    Action recordTime = () => {
                        watch.Stop();
                        TraceUtils.Info($"CallWebApi stop. callCount: {callCount}, ElapsedMilliseconds: {watch.ElapsedMilliseconds},{watch.Elapsed}");
                    };
                    watch.Restart();

                    // 并行
                    //Parallel.For(0, callCount, async p =>
                    //    {
                    //        await CallWebApi(clients[p % clients.Length], WaveLength);

                    //        Interlocked.Increment(ref count);
                    //        if (callCount == count) recordTime();
                    //    }
                    //);

                    // 异步串行
                    for (int i = 0; i < callCount; i++)
                    {
                        await CallWebApi(client, WaveLength);
                    }
                    recordTime();



                    Console.WriteLine($"WaveLength: {WaveLength},ByteWaveLength:{ByteWaveLength}.");
                }

                httpClient.Dispose();
            }
        }

        private static async Task CallWebApi(Client client, int WaveLength)
        {
            int ByteWaveLength = WaveLength * 2;
            try
            {
                VibMetaDataOutputDto metaOutput = null;
                metaOutput = await client.VibMetaDataAsync(new VibMetaDataInput
                {
                    Code = "01030200061410152",
                    FullPath = "沙钢集团\\三车间\\1#线\\加热炉鼓风机电机（2）\\自由侧轴承振动\\4K加速度波形(0~5000)",
                    MeasDate = "2019-11-12 16:19:22",//DateTime.Now.ToString(TimePattern),
                    MeasValue = 186.27f,
                    WaveLength = WaveLength, // 波形长度，采样点数
                    SignalType = 1, //信号类型 0-加速度 1-速度 2-位移 
                    SampleRate = 5120,
                    RPM = 0,
                    Unit = "mm/s", // 工程单位，速度：mm/s，加速度：m/s²，位移：um
                    ConvertCoef = 0.39f,
                    Resolver = 1,
                });// ;

                if (metaOutput == null)
                {
                    metaOutput = new VibMetaDataOutputDto { Data = new VibMetaDataOutput { WaveTag = "9e3e009a - f138 - dcd6 - 6323 - c768dc533b2f" } };
                }

                //WaveTag=9e3e009a-f138-dcd6-6323-c768dc533b2f&Length=131072&CurrIndex=0&BlockSize=131072
                VibWaveDataInput waveDataInput = new VibWaveDataInput
                {
                    WaveTag = metaOutput.Data.WaveTag,
                    Length = ByteWaveLength,
                    CurrIndex = 0,
                    BlockSize = ByteWaveLength
                };
                var bytes = new byte[ByteWaveLength];
                for (int i = 0; i < bytes.Length; i++) bytes[i] = 0;
                var waveDataOutput = await client.VibWaveDataAsync(waveDataInput, bytes);

                ProcessDatasInput processDatasInput = new ProcessDatasInput
                {
                    Code = "01030200061410152",
                    FullPath = "沙钢集团\\三车间\\1#线\\加热炉鼓风机电机（2）\\自由侧轴承振动\\4K加速度波形(0~5000)",
                    TSDatas = new TSDataInput[] {
                                new TSDataInput{
                                    MeasDate = DateTime.Now.ToString(TimePattern),
                                    MeasValue = 1.1f,
                                }
                            },
                    Unit = "℃",//工程单位，转速：rpm，温度：℃
                };

                var processDatasOutput = await client.ProcessDatasAsync(processDatasInput);
                if (processDatasOutput != null && processDatasOutput.Data != null && processDatasOutput.Data.Code == -1)
                {
                    // error
                }
            }
            catch (Exception ex)
            {
                TraceUtils.Info(ex.ToString());
            }
        }
    }

}
