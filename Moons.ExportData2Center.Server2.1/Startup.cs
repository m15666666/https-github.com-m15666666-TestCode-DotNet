using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moons.ExportData2Center.Server;
using Moons.ExportData2Center.Core;
using System.Data;
using Dapper;
using Moons.Common20;

namespace Moons.ExportData2Center.Server2._1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            Config.DbConnectionString = configuration["ConnectionStrings:Default"];
            Config.DatabaseType = configuration["ConnectionStrings:DatabaseType"];
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            InitServerContext();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        private void InitServerContext()
        {
            const string Pnt_Point = "Pnt_Point";

            ServerContext.Instance.AddTableMetaInfo(new TableMetaInfo
            {
                TableName = Pnt_Point,
                BatchImportCount = 100,
                PrimaryKeyName = "Point_ID",
            }
            );

            using (IDbConnection connection = Config.CreateSqlConnection())
            {
                foreach (var tableInfo in ServerContext.Instance.TableName2TableMetaInfos.Values)
                {
                    try
                    {
                        var tableName = tableInfo.TableName;
                        var primaryKeyName = tableInfo.PrimaryKeyName;
                        var o = connection.Query($"select max({primaryKeyName}) from {tableName};").First();
                        var d = o as IDictionary<string,object>;
                        if( d != null && 0 < d.Count )
                        {
                            var idValue = d.Values.First();
                            if(idValue != null && !(idValue is DBNull))
                            {
                                var id = Convert.ToInt64(idValue);
                                lock (tableInfo)
                                {
                                    if (tableInfo.MaxPrimaryKeyValue < id) tableInfo.MaxPrimaryKeyValue = id;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        TraceUtils.Error("", ex);
                    }
                }
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
