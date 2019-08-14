using System;
using System.Collections.Generic;
using System.Text;

namespace SampleServer.Core.Dto
{
    /// <summary>
    /// SampleServer 配置数据类
    /// </summary>
    public class SampleServerConfigDto
    {
        /// <summary>
        /// 是否为调试模式
        /// </summary>
        public bool Debug { get; set; }

        /// <summary>
        /// 定制模式，目前支持：WineSteel（酒钢）
        /// WineSteel：从速度测点数据计算出位移测点的测量值，并在总貌图和历史趋势中显示，不计算报警，不显示波形频谱。
        /// 使用测点编码字段设置，例子：123[v-d]（速度测点编码），123[d]（位移测点编码）。
        /// WEPEC(大连西太平洋石油化工有限公司) : 特别的报警描述生成方式。
        /// </summary>
        public string CustomMode { get; set; }
    }
}
