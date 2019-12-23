using System;
using System.Collections.Generic;
using System.Text;

namespace Moons.ExportData2Center.Core
{
    /// <summary>
    /// 单表导入的实际数据
    /// </summary>
    public class TableImportData
    {
        /// <summary>
        /// 单条记录的json格式表示
        /// </summary>
        public string JsonOfRowData { get; set; }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName { get; set; }
    }
}
