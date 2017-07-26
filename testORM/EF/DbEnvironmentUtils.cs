using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testORM.EF
{
    public static class DbEnvironmentUtils
    {
        /// <summary>
        /// 数据库的默认schema，用于postgresql（public）
        /// </summary>
        public static string DefaultDatabaseSchema { get; set; }
    }
}
