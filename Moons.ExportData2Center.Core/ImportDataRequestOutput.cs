using System;
using System.Collections.Generic;
using System.Text;

namespace Moons.ExportData2Center.Core
{
    /// <summary>
    /// 导入数据请求输出类
    /// </summary>
    public class ImportDataRequestOutput
    {
        /// <summary>
        /// 成功导入的数量
        /// </summary>
        public int SuccessCount { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage { get; set; }
    }
}
