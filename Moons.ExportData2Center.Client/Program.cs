using Dapper;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Newtonsoft.Json;

namespace Moons.ExportData2Center.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Moons.ExportData2Center.Client");

            string conn = "server=.;database=jg2642empty;uid=sa;pwd=#db877350;";
            string sql = "SELECT TOP 10 * FROM Pnt_Point where Point_ID > 1000399 order by Point_ID;";
            using (IDbConnection connection = new SqlConnection(conn))
            {
                var datas = connection.Query<dynamic>(sql).ToList();
                //var datas = connection.Query<object>(sql).ToList();
                //var datas = connection.Query(sql).ToList();
                foreach ( var data in datas)
                {
                    var json = JsonConvert.SerializeObject(data);

                    var o2 = JsonConvert.DeserializeObject(json);
                }
                //var jsons = JsonConvert.SerializeObject(datas);
            }

            Console.ReadLine();
        }
    }
}
