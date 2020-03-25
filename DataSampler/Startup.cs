using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataSampler.Core;
using DataSampler.Core.Dto;
using log4net;
using log4net.Config;
using log4net.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moons.Common20;
using Moons.Common20.IOC;
using Moons.Common20.Serialization.Json;
using Moons.Log4net;
using OnlineSampleCommon.SendTask;

namespace DataSampler
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _changeCallbackdisposable = configuration.GetReloadToken().RegisterChangeCallback(ReloadConfig, configuration);
        }

        #region 动态监测配置文件变化

        private const string DatasamplerConfigKey = "DatasamplerConfigDto";
        private IDisposable _changeCallbackdisposable = null;
        private void ReloadConfig(object o)
        {
            if (_changeCallbackdisposable != null)
            {
                _changeCallbackdisposable.Dispose();
                _changeCallbackdisposable = null;
            }

            var configuration = o as IConfiguration;
            var config = IocUtils.Instance.ServiceProvider.GetRequiredService<IOptions<DatasamplerConfigDto>>();
            configuration.GetSection(DatasamplerConfigKey).Bind(config.Value);
            
            _changeCallbackdisposable = configuration.GetReloadToken().RegisterChangeCallback(ReloadConfig, configuration);
            EnvironmentUtils.FireExternalConfigChanged();
        }

        #endregion

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddSingleton<IJsonSerializer, JsonSerializer>();
            services.AddSingleton<ITaskSender, NullTaskSender>();
            services.AddHostedService<DataSamplerHostedService>();
            services.Configure<DatasamplerConfigDto>(Configuration.GetSection(DatasamplerConfigKey));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //app.UseHsts();
            }

            //app.UseHttpsRedirection();
            //app.UseMvc();

            app.UseStaticFiles();

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
