using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataSampler.Core
{
    /// <summary>
    /// 负责DataSamplerController的启动和停止 
    /// </summary>
    public class DataSamplerHostedService : IHostedService
    {
        Task IHostedService.StartAsync(CancellationToken cancellationToken)
        {
            var sampler = DataSamplerController.Instance;

            sampler.Init();
            sampler.StartNormalSample();

            return Task.CompletedTask;
        }

        Task IHostedService.StopAsync(CancellationToken cancellationToken)
        {
            var sampler = DataSamplerController.Instance;
            sampler.StopSample();

            return Task.CompletedTask;
        }
    }
}
