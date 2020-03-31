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
using Serilog;
using Moons.Serilogs;

namespace DataSampler
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            EnvironmentUtils.IsDebug = true;
            
            //InitLog4net();
            InitSerilog();

            TraceUtils.Info("Starting Datasampler. time stamp: 2019-08-02.");

            try
            {
                var host = CreateWebHostBuilder(args).Build();
                IocUtils.Instance.ServiceProvider = host.Services;

                DataSamplerController.Instance.TestConvertTrendData();
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

        private static void InitLog4net()
        {
            ILoggerRepository repository = LogManager.CreateRepository("NETCoreRepository");
            // 默认简单配置，输出至控制台
            //BasicConfigurator.Configure(repository);
            //XmlConfigurator.Configure(repository, new System.IO.FileInfo("log4net.config"));
            XmlConfigurator.ConfigureAndWatch(repository, new System.IO.FileInfo("log4net.config"));
            //ILog log = LogManager.GetLogger(repository.Name, "Datasampler");

            var logRepository = EnvironmentUtils.LogRepository = new Log4netRepositoryWrapper(repository);
            EnvironmentUtils.Logger = logRepository.GetLogger("Datasampler");
            TraceUtils.Info("InitLog4net");
        }

        private static void InitSerilog()
        {
            var logger = new Serilog.LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File("log/log-.txt",
                 outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
                rollingInterval: Serilog.RollingInterval.Day)
            .CreateLogger();

            var loggerTcp = new Serilog.LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File("log/tcp/log-.txt",
                 outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
                rollingInterval: Serilog.RollingInterval.Day)
            .CreateLogger();

            Serilog.Log.Logger = logger;
            Dictionary<string, Serilog.ILogger> name2Loggers = new Dictionary<string, Serilog.ILogger> {
                { "Datasampler", logger },
                {DataSampler.Config.TcpLoggerName,loggerTcp },
            };
            var logRepository = EnvironmentUtils.LogRepository = new SerilogRepositoryWrapper(name2Loggers);
            EnvironmentUtils.Logger = logRepository.GetLogger("Datasampler");
            TraceUtils.Info("InitSerilog");
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
