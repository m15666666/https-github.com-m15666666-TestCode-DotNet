using System;
using System.Collections.Generic;
using System.Text;

namespace Moons.ExportData2Center.Core
{
    /// <summary>
    /// 表的元信息
    /// </summary>
    public class TableMetaInfo
    {
        /// <summary>
        /// 表名
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// 主键名
        /// </summary>
        public string PrimaryKeyName { get; set; }

        /// <summary>
        /// 主键名
        /// </summary>
        public long MaxPrimaryKeyValue { get; set; } = 0;

        /// <summary>
        /// 一次批量导入的数量
        /// </summary>
        public int BatchImportCount { get; set; } = 10;


    }
}
