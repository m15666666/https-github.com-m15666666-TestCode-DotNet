﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Moons.ExportData2Center.Core
{
    /// <summary>
    /// 导出数据请求输入类
    /// </summary>
    public class ExportDataRequestInput
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
        /// 表名
        /// </summary>
        public string TableName { get; set; }
    }
}
