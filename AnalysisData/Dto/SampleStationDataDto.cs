using System;
using System.Collections.Generic;
using System.Text;

namespace AnalysisData.Dto
{
    /// <summary>
    /// 采集工作站数据类
    /// </summary>
    [Serializable]
    public class SampleStationDataDto
    {
        #region 变量和属性

        public string Name { get; } = "SampleStationConfigData";

        public string Version { get; } = "1.0.0";

        /// <summary>
        /// 数据采集器的IP（例如：“127.0.0.1”）
        /// </summary>
        public string DataSamplerIP { get; set; }

        /// <summary>
        /// 数据采集器的端口号（例如：“1181”）
        /// </summary>
        public int DataSamplerPort { get; set; }

        ///// <summary>
        ///// 采集工作站的IP（例如：“127.0.0.1”）
        ///// </summary>
        //public string SampleStationIP { get; set; }

        ///// <summary>
        ///// 采集工作站的端口号（例如：“1181”）
        ///// </summary>
        //public int SampleStationPort { get; set; }

        /// <summary>
        /// 查询工作站数据的间隔（以秒为单位）
        /// </summary>
        public int QueryIntervalInSecond { get; set; }

        /// <summary>
        /// 上传所有保存到数据库的波形测点数据的间隔（以秒为单位）
        /// </summary>
        public int UploadDBWaveIntervalInSecond { get; set; }

        /// <summary>
        /// 上传所有保存到数据库的静态通道测点数据的间隔（以秒为单位）
        /// </summary>
        public int UploadDBStaticIntervalInSecond { get; set; }

        /// <summary>
        /// 测点数据集合
        /// </summary>
        public List<PointDataDto> PointDatas { get; } = new List<PointDataDto>();

        /// <summary>
        /// 通道数据集合
        /// </summary>
        public List<ChannelDataDto> ChannelDatas { get; set; } = new List<ChannelDataDto>();

        #endregion
    }
}
