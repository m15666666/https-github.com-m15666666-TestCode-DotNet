using log4net;
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
using Test.ShaSteel.WebAPI.Core;

namespace Test.ShaSteel.WebAPI.Client
{
    class Program
    {
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

            const string TimePattern = "yyyy-MM-dd HH:mm:ss";
            using (System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient())
            {
                var client = new Client(url, httpClient);
                while (true)
                {
                    Console.WriteLine("Enter q to quit.");
                    var line = Console.ReadLine();
                    if ("q".Equals(line, StringComparison.InvariantCultureIgnoreCase)) break;

                    try
                    {
                        var metaOutput = await client.VibMetaDataAsync(new VibMetaDataInput
                        {
                            Code = "abcd",
                            FullPath = "a-b-c-d",
                            MeasDate = DateTime.Now.ToString(TimePattern),
                            MeasValue = 1.1f,
                            WaveLength = 2, // 波形长度，采样点数
                            SignalType = 0, //信号类型 0-加速度 1-速度 2-位移 
                            SampleRate = 256,
                            RPM = 1000,
                            Unit = "mm/s", // 工程单位，速度：mm/s，加速度：m/s²，位移：um
                            ConvertCoef = 0.39f,
                            Resolver = 1,
                        });


                        //WaveTag=9e3e009a-f138-dcd6-6323-c768dc533b2f&Length=131072&CurrIndex=0&BlockSize=131072
                        VibWaveDataInput waveDataInput = new VibWaveDataInput
                        {
                            WaveTag = metaOutput.WaveTag,
                            Length = 4,
                            CurrIndex = 0,
                            BlockSize = 4
                        };
                        var waveDataOutput = await client.VibWaveDataAsync(waveDataInput, new byte[] { 1, 0, 1, 0 });

                        ProcessDatasInput processDatasInput = new ProcessDatasInput
                        {
                            Code = "abcd",
                            FullPath = "a-b-c-d",
                            TSDatas = new TSDataInput[] { 
                                new TSDataInput{
                                    MeasDate = DateTime.Now.ToString(TimePattern),
                                    MeasValue = 1.1f,
                                }
                            },
                            Unit = "℃",//工程单位，转速：rpm，温度：℃
                        };

                        var processDatasOutput = await client.ProcessDatasAsync(processDatasInput);
                        if(processDatasOutput != null && processDatasOutput.ToString() == "-1")
                        {
                            // error
                        }
                    }
                    catch (Exception ex)
                    {
                        TraceUtils.Info(ex.ToString());
                    }
                }

                httpClient.Dispose();
            }
        }
    }

}
