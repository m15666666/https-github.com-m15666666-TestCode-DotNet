using System;
using System.Collections.Generic;
using System.Text;

namespace Moons.ExportData2Center.Core
{
    /// <summary>
    /// 导出数据请求输出类
    /// </summary>
    public class ExportDataRequestOutput
    {
        /// <summary>
        /// 需要导出数据的表的集合
        /// </summary>
        public TableExportMetaData[] TableExportMetaDatas { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage { get; set; }
    }
}
