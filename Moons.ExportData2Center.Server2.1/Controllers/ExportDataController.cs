using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moons.Common20;
using Moons.ExportData2Center.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Moons.ExportData2Center.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ExportDataController : ControllerBase
    {
        // GET: api/ExportData
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }


        // POST: api/ExportData/ExportDataRequest
        [HttpPost]
        public ExportDataRequestOutput ExportDataRequest([FromBody] ExportDataRequestInput value)
        {
            var content = Newtonsoft.Json.JsonConvert.SerializeObject(value);
            TraceUtils.Info($"ExportDataRequest value:{content}");

            var tableName = value.TableName;
            if (string.IsNullOrEmpty(tableName))
            {
                return new ExportDataRequestOutput
                {
                    TableExportMetaDatas = ServerContext.Instance.TableNames.Select(
                        item => new TableExportMetaData {
                        TableName = item
                    }).ToArray()
                };
            }

            if( ServerContext.Instance.TableName2TableMetaInfos.ContainsKey(tableName))
            {
                var tableInfo = ServerContext.Instance.TableName2TableMetaInfos[tableName];

                return new ExportDataRequestOutput
                {
                    TableExportMetaDatas = new TableExportMetaData[] {
                    new TableExportMetaData{
                        SelectSql = $"SELECT TOP {tableInfo.BatchImportCount} * FROM {tableName} where {tableInfo.PrimaryKeyName} > {tableInfo.MaxPrimaryKeyValue} order by {tableInfo.PrimaryKeyName};",
                        TableName = tableName
                    }
                }
                };
            }

            throw new Exception();
            return new ExportDataRequestOutput
            {
                TableExportMetaDatas = new TableExportMetaData[] { 
                    new TableExportMetaData{ 
                        SelectSql = $"SELECT TOP 10 * FROM {tableName} where Point_ID > 1000399 order by Point_ID;",
                        TableName = tableName
                    }
                }
            };
        }

        // POST: api/ExportData/ImportDataRequest
        [HttpPost]
        public ImportDataRequestOutput ImportDataRequest([FromBody] ImportDataRequestInput value)
        {
            var content = JsonConvert.SerializeObject(value);
            TraceUtils.Info($"ImportDataRequestInput value:{content}");

            var serverContext = ServerContext.Instance;

            using (IDbConnection connection = Config.CreateSqlConnection())
            {
                foreach (TableImportData table in value.TableImportDatas)
                {
                    try
                    {
                        if (!serverContext.TableName2TableMetaInfos.ContainsKey(table.TableName)) continue;
                        var tableInfo = serverContext.TableName2TableMetaInfos[table.TableName];

                        var json = table.JsonOfRowData;
                        JObject jobject = JsonConvert.DeserializeObject<JObject>(json);

                        var p = DapperUtils.GetDynamicParameters(jobject);
                        string insertSql = DapperUtils.Instance.GenerateInsertSQL(table.TableName, jobject);
                        connection.Execute(insertSql, p);

                        // 更新id
                        var v = DapperUtils.GetJTokenValue(jobject[tableInfo.PrimaryKeyName]);
                        if (v != null)
                        {
                            var id = Convert.ToInt64(v);
                            lock (tableInfo)
                            {
                                if (tableInfo.MaxPrimaryKeyValue < id) tableInfo.MaxPrimaryKeyValue = id;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        TraceUtils.Error("ImportDataRequest", ex);
                    }
                }
            }

            return new ImportDataRequestOutput
            {
                SuccessCount = 1
            };
        }
    }
}
