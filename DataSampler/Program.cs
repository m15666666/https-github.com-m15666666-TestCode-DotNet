using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moons.Common20;
using Moons.Common20.IOC;
using Moons.Log4net;
using log4net;
using log4net.Config;
using log4net.Repository;

namespace DataSampler
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            EnvironmentUtils.IsDebug = true;

            ILoggerRepository repository = LogManager.CreateRepository("NETCoreRepository");
            // 默认简单配置，输出至控制台
            //BasicConfigurator.Configure(repository);
            XmlConfigurator.Configure(repository, new System.IO.FileInfo("log4net.config"));
            ILog log = LogManager.GetLogger(repository.Name, "Datasampler");

            EnvironmentUtils.Logger = new Log4netWrapper(log);
            TraceUtils.Info("Starting Datasampler. time stamp: 2019-08-02.");

            try
            {
                var host = CreateWebHostBuilder(args).Build();
                IocUtils.Instance.ServiceProvider = host.Services;

                var json = DataSamplerController.Instance.TestJson();
                //using (var sampler = DataSamplerController.Instance)
                {
                    //DataSampler.Config.DatasamplerConfigDto.UseNetty = true;

                    //sampler.Init();

                    //sampler.StartNormalSample();
                
                    await host.RunAsync();
                    //host.Run();
                    //sampler.StopSample();
                }
            }
            catch (Exception ex)
            {
                TraceUtils.Error("Init datasampler error.", ex);
            }
            TraceUtils.Info("DataSampler exit.");

            Console.WriteLine("Press enter to exit.");
            Console.ReadLine();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
