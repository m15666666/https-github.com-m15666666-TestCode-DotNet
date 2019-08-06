using System;
using System.Xml.Serialization;

namespace AnalysisData.SampleData
{
    /// <summary>
    /// 数据采集器配置数据类
    /// </summary>
    [Serializable]
    [XmlRoot( "SamplerConfigData" )]
    public class SamplerConfigData
    {
        /// <summary>
        /// 数据采集器ID
        /// </summary>
        [XmlElement( ElementName = "DataSamplerID" )]
        public int DataSamplerID { get; set; }

        /// <summary>
        /// 在线采集配置服务地址
        /// </summary>
        [XmlElement( ElementName = "OnlineSampleConfigServiceAddress" )]
        public string OnlineSampleConfigServiceAddress { get; set; }

        /// <summary>
        /// 采集服务器服务地址
        /// </summary>
        [XmlElement( ElementName = "SampleServerServiceAddress" )]
        public string SampleServerServiceAddress { get; set; }

        /// <summary>
        /// 是否使用netty
        /// </summary>
        public bool UseNetty { get; set; }
    }
}