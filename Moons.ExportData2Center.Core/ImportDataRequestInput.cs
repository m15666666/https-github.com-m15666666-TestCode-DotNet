using System;
using System.Collections.Generic;
using System.Text;

namespace Moons.ExportData2Center.Core
{
    /// <summary>
    /// 导入数据请求输入类
    /// </summary>
    public class ImportDataRequestInput
    {
        /// <summary>
        /// 客户id，用于区分数据来源
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// 数据库类型
        /// </summary>
        public string DatabaseType { get; set; }

        /// <summary>
        /// 导入的数据
        /// </summary>
        public TableImportData[] TableImportDatas { get; set; }
    }
}
