using Dapper;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moons.ExportData2Center.Core
{
    /// <summary>
    /// 使用dapper写入数据库的实用工具类
    /// 
    /// https://blog.darkthread.net/blog/jobject-dapper-batch-insert/
    /// </summary>
    public class DapperUtils
    {
        public static readonly DapperUtils Instance = new DapperUtils();

        public static object GetJTokenValue(JToken token)
        {
            var t = token.Type;
            switch (t)
            {
                case JTokenType.Boolean:
                    return token.Value<bool>();
                case JTokenType.Bytes:
                    return token.Value<byte[]>();
                case JTokenType.Date:
                    return token.Value<DateTime>();
                case JTokenType.Float:
                    return token.Value<double>();
                case JTokenType.Integer:
                    return token.Value<int>();
                case JTokenType.String:
                    return token.Value<string>();
                case JTokenType.Null:
                    return null;
                default:
                    throw new ApplicationException(
                        t.ToString() + " is not supported");
            }
        }

        /// <summary>
        /// 将JObject对象转换为dapper可以使用的DynamicParameters
        /// </summary>
        /// <param name="jobject"></param>
        /// <returns></returns>
        public static DynamicParameters GetDynamicParameters(JObject jobject)
        {
            var ret = new DynamicParameters();
            var properties = jobject.Properties();
            foreach (JProperty item in properties)
            {
                ret.Add(item.Name, GetJTokenValue(item.Value));
            }
            return ret;
        }

        /// <summary>
        /// 表名对insertsql的缓存
        /// </summary>
        private readonly Dictionary<string, string> _tableName2InsertSQL = new Dictionary<string, string>();

        /// <summary>
        /// 生成insertsql语句
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="jobject"></param>
        /// <returns></returns>
        public string GenerateInsertSQL( string tableName, JObject jobject)
        {
            if (_tableName2InsertSQL.ContainsKey(tableName)) return _tableName2InsertSQL[tableName];

            lock (_tableName2InsertSQL)
            {
                if (_tableName2InsertSQL.ContainsKey(tableName)) return _tableName2InsertSQL[tableName];

                var fields = GetFieldsName(jobject);
                var values = GetFieldsName(jobject, "@");

                string insertSql = $"INSERT INTO {tableName}({string.Join(",", fields)}) VALUES({string.Join(",", values)}); ";
                _tableName2InsertSQL[tableName] = insertSql;
                return insertSql;
            }
        }

        private static List<string> GetFieldsName(JObject jobject, string prefix = null)
        {
            var ret = new List<string>();
            var properties = jobject.Properties();
            var noPrefix = string.IsNullOrEmpty(prefix);
            foreach (JProperty item in properties)
            {
                ret.Add(noPrefix ? item.Name : prefix + item.Name );
            }
            return ret;
        }
    }
}
