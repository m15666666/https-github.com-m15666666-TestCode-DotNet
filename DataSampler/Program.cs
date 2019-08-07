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

namespace DataSampler
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using ( var sampler = DataSamplerController.Instance )
            {
                CreateWebHostBuilder(args).Build().Run();
                sampler.StopSample();
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
