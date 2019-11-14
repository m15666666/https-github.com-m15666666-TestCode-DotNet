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
                            Code = "01030200061410152",
                            FullPath = "沙钢集团\\三车间\\1#线\\加热炉鼓风机电机（2）\\自由侧轴承振动\\4K加速度波形(0~5000)",
                            MeasDate = "2019-11-12 16:19:22",//DateTime.Now.ToString(TimePattern),
                            MeasValue = 186.27f,
                            WaveLength = 1024, // 波形长度，采样点数
                            SignalType = 1, //信号类型 0-加速度 1-速度 2-位移 
                            SampleRate = 5120,
                            RPM = 0,
                            Unit = "mm/s", // 工程单位，速度：mm/s，加速度：m/s²，位移：um
                            ConvertCoef = 0.39f,
                            Resolver = 1,
                        });// ;


                        //WaveTag=9e3e009a-f138-dcd6-6323-c768dc533b2f&Length=131072&CurrIndex=0&BlockSize=131072
                        VibWaveDataInput waveDataInput = new VibWaveDataInput
                        {
                            WaveTag = metaOutput.Data.WaveTag,
                            Length = 1024 * 2,
                            CurrIndex = 0,
                            BlockSize = 1024 * 2
                        };
                        var waveDataOutput = await client.VibWaveDataAsync(waveDataInput, new byte[2048]);

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
                        if(processDatasOutput != null && processDatasOutput.Data != null && processDatasOutput.Data.Code == -1)
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
