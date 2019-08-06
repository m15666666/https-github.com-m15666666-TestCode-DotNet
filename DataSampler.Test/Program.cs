using AnalysisData.SampleData;
using DataSampler.Helper;
using log4net;
using log4net.Config;
using log4net.Repository;
using Moons.Common20;
using Moons.Log4net;
using System;
using System.Collections.Generic;

namespace DataSampler.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            ILoggerRepository repository = LogManager.CreateRepository("NETCoreRepository");
            // 默认简单配置，输出至控制台
            //BasicConfigurator.Configure(repository);
            XmlConfigurator.Configure(repository, new System.IO.FileInfo("log4net.config"));
            ILog log = LogManager.GetLogger(repository.Name, "Datasampler");

            EnvironmentUtils.Logger = new Log4netWrapper(log);
            TraceUtils.Info("Starting Datasampler.test. time stamp: 2019-08-05.");

            EnvironmentUtils.IsDebug = true;

            try
            {
                Config.InitStructReadWriteHandler();
                Config.InitDebugHandler();

                var sampleStationProxy = new SampleStationProxy();
                sampleStationProxy.SampleStationData.DataSamplerIP = "127.0.0.1";

                Do("StartNormalSample", () => sampleStationProxy.StartNormalSample() );
                Do("StopSample", () => sampleStationProxy.StopSample());
                //Do("SendAlmEventData_Test", () => sampleStationProxy.SendAlmEventData_Test());
                Do("GetSampleStationStatus", () => sampleStationProxy.GetSampleStationStatus());
                Do("GetHardwareInfo", () => sampleStationProxy.GetHardwareInfo());
                //sampleStationProxy.StopSample();
                //sampleStationProxy.SendAlmEventData_Test();
                //sampleStationProxy.GetSampleStationStatus();
                //sampleStationProxy.GetHardwareInfo();

                SampleStationParameterErrorDataCollection error = null;
                sampleStationProxy.DownloadSampleConfig(ref error);

                List<TrendData> trendDatas = sampleStationProxy.GetData(new int[1]);
                sampleStationProxy.Timing(DateTime.Now);
            }
            catch (Exception ex)
            {
                TraceUtils.Error("error", ex);
            }

            Console.WriteLine("DataSampler.Test");
            Console.ReadLine();
        }

        private static void Do(string title, Action action)
        {
            TraceUtils.Info($"{title} ...");
            try
            {
                action();
            }
            catch (Exception ex)
            {
                TraceUtils.Error($"{title} error.", ex);
            }
        }
    }
}
