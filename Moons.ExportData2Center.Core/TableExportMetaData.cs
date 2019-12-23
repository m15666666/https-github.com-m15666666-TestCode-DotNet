using System;
using System.Collections.Generic;
using System.Text;

namespace Moons.ExportData2Center.Core
{
    /// <summary>
    /// 单表导出元数据
    /// </summary>
    public class TableExportMetaData
    {
        /// <summary>
        /// 用于查询导出数据的sql语句
        /// </summary>
        public string SelectSql { get; set; }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// 一次批量导入的数量
        /// </summary>
        public int BatchImportCount { get; set; } = 1;
    }
}
