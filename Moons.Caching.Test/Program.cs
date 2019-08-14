using Moons.Caching.Extensions;
using Moons.Caching.InMemory;
using Moons.Caching.StackExchangeRedis;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Moons.Caching.Abstractions;

namespace Moons.Caching.Test
{
    static class Program
    {
        private static IHost host;
        static async Task Main(string[] args)
        {
            host = new HostBuilder()
                   .UseContentRoot(Directory.GetCurrentDirectory())
                   .ConfigureHostConfiguration(config =>
                   {
                       if (args != null)
                       {
                           config.AddCommandLine(args);
                       }
                   })
                   .ConfigureAppConfiguration((hostingContext, config) =>
                   {
                       config
                         .SetBasePath(Directory.GetCurrentDirectory())
                         .AddJsonFile("appsettings.json", true, true)
                         .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true)
                         .AddEnvironmentVariables();
                   })
                   .ConfigureServices((hostContext, services) =>
                   {
                       services.AddMemoryCache();
                       services.AddCaching(build =>
                       {
                           //build.UseInMemory("default");
                           build.UseStackExchangeRedis(new Caching.StackExchangeRedis.Abstractions.StackExchangeRedisOption
                           {
                               //Configuration = "127.0.0.1:6379,allowadmin=true",
                               Configuration = "127.0.0.1:6379,password=#db877350,connectTimeout=1000,connectRetry=1,syncTimeout=1000",
                               DbProviderName = "redis"
                           });
                       });
                   })
                   .ConfigureLogging((hostingContext, logging) =>
                   {
                       logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging")).AddConsole().AddDebug();
                   })
                   .UseConsoleLifetime()
                   .Build();

            TestRedisWrite();
            await host.RunAsync();
        }

        private static T GetService<T>()
        {
            return host.Services.GetRequiredService<T>();
        }

        private static string CacheKey = "CacheKey";
        private static byte[] CachedMessage = new byte[1024 * 16];
        private static void TestRedisWrite()
        {
            

            CachedMessage[0] = 11;
            CachedMessage[CachedMessage.Length - 1] = 12;
            int count = 10000;
            var cache = GetService<ICachingProvider>();

            Console.WriteLine($"TestRedisWrite begin {DateTime.Now}");
            for (int i = 0; i < count; i++)
            {
                CachedMessage[1] = (byte)(i%127);
                cache.Set(CacheKey, CachedMessage, new TimeSpan(2, 0, 0));
                //var c = cache.Get<byte[]>(CacheKey);
                //var s = c.ToString();
            }

            Console.WriteLine($"TestRedisWrite end {DateTime.Now}");

            Console.WriteLine($"TestRedisRead begin {DateTime.Now}");
            for (int i = 0; i < count; i++)
            {
                var c = cache.Get<byte[]>(CacheKey);
                var s = c.Length;
            }

            Console.WriteLine($"TestRedisRead end {DateTime.Now}");
        }
    }
}
