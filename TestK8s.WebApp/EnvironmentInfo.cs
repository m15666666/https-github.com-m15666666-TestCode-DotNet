using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace TestK8s.WebApp
{
    /// <summary>
    /// 打印环境信息
    /// </summary>
    public static class EnvironmentInfo
    {
        public static void PrintEnv()
        {
            Print("enter printenv");
            try
            {
                string msg = null;
                msg = $"RuntimeInformation.IsOSPlatform(OSPlatform.Windows):{RuntimeInformation.IsOSPlatform(OSPlatform.Windows)}";
                Print(msg);

                msg = $"RuntimeInformation.IsOSPlatform(OSPlatform.Linux):{RuntimeInformation.IsOSPlatform(OSPlatform.Linux)}";
                Print(msg);
                msg = $"RuntimeInformation.IsOSPlatform(OSPlatform.OSX):{RuntimeInformation.IsOSPlatform(OSPlatform.OSX)}";
                Print(msg);
                msg = $"GCSettings.LatencyMode:{GCSettings.LatencyMode}";
                Print(msg);
                GCSettings.LatencyMode = GCLatencyMode.SustainedLowLatency;
                msg = $"GCSettings.LatencyMode:{GCSettings.LatencyMode}";
                Print(msg);
            }
            catch(Exception ex)
            {
                Print($"error.{ex}");
                Print($"{Environment.StackTrace}");
            }
            Print("exit printenv");
        }

        private static void Print(string msg)
        {
            Console.WriteLine(msg);
        }
    }
}
