using Dapper;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Newtonsoft.Json;
using log4net;
using log4net.Repository;
using log4net.Config;
using Moons.Common20;
using Moons.Log4net;
using Moons.ExportData2Center.Core;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace Moons.ExportData2Center.Client
{
    /// <summary>
    /// 配置文件参考：https://blog.csdn.net/yenange/article/details/82457761
    /// </summary>
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
            TraceUtils.Info("Starting Moons.ExportData2Center.Client. time stamp: 2019-12-20.");

            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
            var configuration = builder.Build();

            var url = configuration["AppSettings:WebApiUrl"];//http://localhost:60611/api/ExportData/
            Config.Instance.Init(url);

            //string conn = "server=.;database=jg2642empty;uid=sa;pwd=#db877350;";
            string conn = configuration["ConnectionStrings:Default"];
            //string sql = "SELECT TOP 10 * FROM Pnt_Point where Point_ID > 1000399 order by Point_ID;";
            using (IDbConnection connection = new SqlConnection(conn))
            {

                while (true)
                {
                    Console.WriteLine("Input q to quit");
                    var input = Console.ReadLine();
                    if (input == "q") break;

                    var list = await Config.Instance.Client.ExportDataRequestAsync(new ExportDataRequestInput
                    {
                    });

                    foreach( TableExportMetaData t in list.TableExportMetaDatas )
                    {
                        var tableName = t.TableName;
                        while (true)
                        {
                            var singleTable = await Config.Instance.Client.ExportDataRequestAsync(new ExportDataRequestInput
                            {
                                TableName = tableName
                            });
                            if (singleTable.TableExportMetaDatas == null || singleTable.TableExportMetaDatas.Length == 0) break;
                            var table = singleTable.TableExportMetaDatas.First();
                            var datas = connection.Query<dynamic>(table.SelectSql).ToList();
                            if (datas.Count == 0) break;

                            foreach (var data in datas)
                            {
                                var json = JsonConvert.SerializeObject(data);

                                await Config.Instance.Client.ImportDataRequestAsync(
                                    new ImportDataRequestInput
                                    {
                                        TableImportDatas = new TableImportData[] {
                                new TableImportData{
                                TableName = tableName,
                                JsonOfRowData = json }
                                    }
                                    });

                                //JObject o2 = JsonConvert.DeserializeObject<JObject>(json);
                                //IEnumerable<JProperty> properties = o2.Properties();
                                //foreach (JProperty item in properties)
                                //{
                                //    Console.WriteLine(item.Name + ":" + item.Value);
                                //}
                            }
                        }
                    }

                    //var datas = connection.Query<dynamic>(sql).ToList();
                    ////var datas = connection.Query<object>(sql).ToList();
                    ////var datas = connection.Query(sql).ToList();
                    //foreach (var data in datas)
                    //{
                    //    var json = JsonConvert.SerializeObject(data);

                    //    await Config.Instance.Client.ImportDataRequestAsync(new Core.ImportDataRequestInput
                    //    {
                    //        TableImportDatas = new Core.TableImportData[] {
                    //        new TableImportData{
                    //            TableName = "Pnt_Point",
                    //            JsonOfRowData = json }
                    //    }
                    //    });

                    //    //JObject o2 = JsonConvert.DeserializeObject<JObject>(json);
                    //    //IEnumerable<JProperty> properties = o2.Properties();
                    //    //foreach (JProperty item in properties)
                    //    //{
                    //    //    Console.WriteLine(item.Name + ":" + item.Value);
                    //    //}
                    //}
                    ////var jsons = JsonConvert.SerializeObject(datas);
                }
            }

            Console.ReadLine();
        }
    }
}
