using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Moons.ExportData2Center.Server
{
    public class Config
    {
        public static string DbConnectionString { get; set; }

        public static string DatabaseType { get; set; }

        public static IDbConnection CreateSqlConnection()
        {
            return new SqlConnection(DbConnectionString);
        }
        
    }
}
