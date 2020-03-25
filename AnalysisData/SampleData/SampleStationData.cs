using System;
using System.Xml.Serialization;
using Moons.Common20;
using Moons.Common20.ValueWrapper;

namespace AnalysisData.SampleData
{
    /// <summary>
    /// 采集工作站数据类
    /// </summary>
    [Serializable]
    [XmlRoot( "SampleStationData" )]
    public class SampleStationData : EntityBase, IValueWrappersContainer
    {
        #region 变量和属性

        /// <summary>
        /// 数据采集器的端口号（例如：“1181”）
        /// </summary>
        private ValueWrapper<int> _dataSamplerPort = new ValueWrapper<int>();

        /// <summary>
        /// 上传所有保存到数据库的静态通道测点数据的间隔（以秒为单位）
        /// </summary>
        private ValueWrapper<int> _uploadDbStaticIntervalInSecond = new ValueWrapper<int>();

        /// <summary>
        /// 上传所有保存到数据库的波形测点数据的间隔（以秒为单位）
        /// </summary>
        private ValueWrapper<int> _uploadDbWaveIntervalInSecond = new ValueWrapper<int>();

        /// <summary>
        /// 数据采集器的IP（例如：“127.0.0.1”）
        /// </summary>
        public string DataSamplerIP { get; set; }

        /// <summary>
        /// 数据采集器的端口号（例如：“1181”）
        /// </summary>
        public int DataSamplerPort
        {
            get { return _dataSamplerPort; }
            set { _dataSamplerPort = value; }
        }

        /// <summary>
        /// 采集工作站的IP（例如：“127.0.0.1”）
        /// </summary>
        public string SampleStationIP { get; set; }

        /// <summary>
        /// 采集工作站的端口号（例如：“1181”）
        /// </summary>
        public int SampleStationPort { get; set; }

        /// <summary>
        /// 查询工作站数据的间隔（以秒为单位）
        /// </summary>
        public int QueryIntervalInSecond { get; set; }

        /// <summary>
        /// 上传所有保存到数据库的波形测点数据的间隔（以秒为单位）
        /// </summary>
        public int UploadDBWaveIntervalInSecond
        {
            get { return _uploadDbWaveIntervalInSecond; }
            set { _uploadDbWaveIntervalInSecond = value; }
        }

        /// <summary>
        /// 上传所有保存到数据库的静态通道测点数据的间隔（以秒为单位）
        /// </summary>
        public int UploadDBStaticIntervalInSecond
        {
            get { return _uploadDbStaticIntervalInSecond; }
            set { _uploadDbStaticIntervalInSecond = value; }
        }

        /// <summary>
        ///     采集工作站类型（型号）
        /// </summary>
        public SampleStationType StationType { get; set; }

        /// <summary>
        /// 测点数据集合
        /// </summary>
        [XmlElement(ElementName = "PointData")]
        public PointDataCollection PointDatas { get; } = new PointDataCollection();

        /// <summary>
        /// 通道数据集合
        /// </summary>
        [XmlElement(ElementName = "ChannelData")]
        public ChannelDataCollection ChannelDatas { get; } = new ChannelDataCollection();

        #endregion

        #region IValueWrappersContainer Members

        /// <summary>
        /// 获得IValueWrapper接口的集合
        /// </summary>
        [XmlIgnore]
        IValueWrapper[] IValueWrappersContainer.ValueWrappers
        {
            get
            {
                return new IValueWrapper[]
                           {
                               _dataSamplerPort,
                               _uploadDbWaveIntervalInSecond, _uploadDbStaticIntervalInSecond
                           };
            }
        }

        #endregion

        #region 加载xml文件/转化为xml

        #region xml tags

        /// <summary>
        /// 裕度的连续报警次数，没有或为0表示不报警
        /// </summary>
        public const string XmlTag_AlmCount_ClearanceFactor = "AlmCount_ClearanceFactor";

        /// <summary>
        /// 峰值指标的连续报警次数，没有或为0表示不报警
        /// </summary>
        public const string XmlTag_AlmCount_CrestFactor = "AlmCount_CrestFactor";

        /// <summary>
        /// 脉冲指标的连续报警次数，没有或为0表示不报警
        /// </summary>
        public const string XmlTag_AlmCount_ImpulseFactor = "AlmCount_ImpulseFactor";

        /// <summary>
        /// 峭度的连续报警次数，没有或为0表示不报警
        /// </summary>
        public const string XmlTag_AlmCount_KurtoFactor = "AlmCount_KurtoFactor";

        /// <summary>
        /// 均值的连续报警次数，没有或为0表示不报警
        /// </summary>
        public const string XmlTag_AlmCount_Mean = "AlmCount_Mean";

        /// <summary>
        /// 测量值的连续报警次数，没有或为0表示不报警
        /// </summary>
        public const string XmlTag_AlmCount_MeasurementValue = "AlmCount_MeasurementValue";

        /// <summary>
        /// 峰值的连续报警次数，没有或为0表示不报警
        /// </summary>
        public const string XmlTag_AlmCount_P = "AlmCount_P";

        /// <summary>
        /// 峰峰值的连续报警次数，没有或为0表示不报警
        /// </summary>
        public const string XmlTag_AlmCount_PP = "AlmCount_PP";

        /// <summary>
        /// RMS的连续报警次数，没有或为0表示不报警
        /// </summary>
        public const string XmlTag_AlmCount_RMS = "AlmCount_RMS";

        /// <summary>
        /// 波形指标的连续报警次数，没有或为0表示不报警
        /// </summary>
        public const string XmlTag_AlmCount_ShapeFactor = "AlmCount_ShapeFactor";

        /// <summary>
        /// 歪度的连续报警次数，没有或为0表示不报警
        /// </summary>
        public const string XmlTag_AlmCount_SkewFactor = "AlmCount_SkewFactor";

        /// <summary>
        /// 裕度的报警设置，没有表示不报警
        /// </summary>
        private const string XmlTag_AlmSetting_ClearanceFactor = "AlmSetting_ClearanceFactor";

        /// <summary>
        /// 峰值指标的报警设置，没有表示不报警
        /// </summary>
        private const string XmlTag_AlmSetting_CrestFactor = "AlmSetting_CrestFactor";

        /// <summary>
        /// 脉冲指标的报警设置，没有表示不报警
        /// </summary>
        private const string XmlTag_AlmSetting_ImpulseFactor = "AlmSetting_ImpulseFactor";

        /// <summary>
        /// 峭度的报警设置，没有表示不报警
        /// </summary>
        private const string XmlTag_AlmSetting_KurtoFactor = "AlmSetting_KurtoFactor";

        /// <summary>
        /// 均值的报警设置，没有表示不报警
        /// </summary>
        private const string XmlTag_AlmSetting_Mean = "AlmSetting_Mean";

        /// <summary>
        /// 测量值的报警设置，没有表示不报警
        /// </summary>
        private const string XmlTag_AlmSetting_MeasurementValue = "AlmSetting_MeasurementValue";

        /// <summary>
        /// 峰值的报警设置，没有表示不报警
        /// </summary>
        private const string XmlTag_AlmSetting_P = "AlmSetting_P";

        /// <summary>
        /// 峰峰值的报警设置，没有表示不报警
        /// </summary>
        private const string XmlTag_AlmSetting_PP = "AlmSetting_PP";

        /// <summary>
        /// RMS的报警设置，没有表示不报警
        /// </summary>
        private const string XmlTag_AlmSetting_RMS = "AlmSetting_RMS";

        /// <summary>
        /// 波形因子的报警设置，没有表示不报警
        /// </summary>
        private const string XmlTag_AlmSetting_ShapeFactor = "AlmSetting_ShapeFactor";

        /// <summary>
        /// 歪度的报警设置，没有表示不报警
        /// </summary>
        private const string XmlTag_AlmSetting_SkewFactor = "AlmSetting_SkewFactor";

        /// <summary>
        /// 报警类型ID，0	超上限、1超下限、2窗内、3窗外
        /// </summary>
        private const string XmlTag_AlmType_ID = "AlmType_ID";

        /// <summary>
        /// 通道编码
        /// </summary>
        public const string XmlTag_ChannelCD = "ChannelCD";

        /// <summary>
        /// 通道对象
        /// </summary>
        public const string XmlTag_ChannelData = "ChannelData";

        /// <summary>
        /// 数据库ID
        /// </summary>
        public const string XmlTag_DatabaseID = "DatabaseID";

        /// <summary>
        /// 数据长度
        /// </summary>
        public const string XmlTag_DataLength = "DataLength";

        /// <summary>
        /// 平均次数
        /// </summary>
        public const string XmlTag_AvgTime = "AvgTime";

        /// <summary>
        /// 工程单位
        /// </summary>
        private const string XmlTag_EngUnit = "EngUnit";

        /// <summary>
        /// 第1级超上限或窗报警门限值的上限，对超下限报警无效
        /// </summary>
        private const string XmlTag_HighLimit1_NR = "HighLimit1_NR";

        /// <summary>
        /// 第2级超上限或窗报警门限值的上限，对超下限报警无效
        /// </summary>
        private const string XmlTag_HighLimit2_NR = "HighLimit2_NR";

        /// <summary>
        /// 第3级超上限或窗报警门限值的上限，对超下限报警无效
        /// </summary>
        private const string XmlTag_HighLimit3_NR = "HighLimit3_NR";

        /// <summary>
        /// 第4级超上限或窗报警门限值的上限，对超下限报警无效
        /// </summary>
        private const string XmlTag_HighLimit4_NR = "HighLimit4_NR";

        /// <summary>
        /// 第1级超下限或窗报警门限值的下限，对超上限报警无效
        /// </summary>
        private const string XmlTag_LowLimit1_NR = "LowLimit1_NR";

        /// <summary>
        /// 第2级超下限或窗报警门限值的下限，对超上限报警无效
        /// </summary>
        private const string XmlTag_LowLimit2_NR = "LowLimit2_NR";

        /// <summary>
        /// 第3级超下限或窗报警门限值的下限，对超上限报警无效
        /// </summary>
        private const string XmlTag_LowLimit3_NR = "LowLimit3_NR";

        /// <summary>
        /// 第4级超下限或窗报警门限值的下限，对超上限报警无效
        /// </summary>
        private const string XmlTag_LowLimit4_NR = "LowLimit4_NR";

        /// <summary>
        /// 倍频系数
        /// </summary>
        public const string XmlTag_MultiFreq = "MultiFreq";

        /// <summary>
        /// 测点对象
        /// </summary>
        private const string XmlTag_PointData = "PointData";

        /// <summary>
        /// 测点ID
        /// </summary>
        private const string XmlTag_PointID = "PointID";

        /// <summary>
        /// 测点名
        /// </summary>
        private const string XmlTag_PointName = "PointName";

        /// <summary>
        /// 转速通道编码
        /// </summary>
        private const string XmlTag_RevChannelCD = "RevChannelCD";

        /// <summary>
        /// 转速通道ID
        /// </summary>
        public const string XmlTag_RevChannelID = "RevChannelID";

        /// <summary>
        /// 转速上限，高于上限则转速为0，单位是rpm
        /// </summary>
        public const string XmlTag_RevHighThreshold = "RevHighThreshold";

        /// <summary>
        /// 转速下限，低于下限则转速为0，单位是rpm
        /// </summary>
        public const string XmlTag_RevLowThreshold = "RevLowThreshold";

        private const string XmlTag_Root = "SampleConfig";

        /// <summary>
        /// 采样频率
        /// </summary>
        public const string XmlTag_SampleFreq = "SampleFreq";

        /// <summary>
        /// 采集服务器IP
        /// </summary>
        private const string XmlTag_SampleServerIP = "SampleServerIP";

        /// <summary>
        /// 采集服务器端口
        /// </summary>
        private const string XmlTag_SampleServerPort = "SampleServerPort";

        /// <summary>
        /// 采集工作站IP
        /// </summary>
        private const string XmlTag_SampleStationIP = "SampleStationIP";

        /// <summary>
        /// 采集工作站端口
        /// </summary>
        private const string XmlTag_SampleStationPort = "SampleStationPort";

        /// <summary>
        /// 分析频率，必须能被120000整除
        /// </summary>
        private const string XmlTag_AnalysisFreq = "AnalysisFreq";

        public const string XmlTag_SampleStation = "SampleStation";
        public const string XmlTag_SampMod = "SampMod";

        /// <summary>
        /// 比例因子
        /// </summary>
        public const string XmlTag_ScaleFactor = "ScaleFactor";

        /// <summary>
        /// 引用的动态通道CD，仅用于积分通道
        /// </summary>
        public const string XmlTag_RefChannelCD = "RefChannelCD";

        /// <summary>
        /// 积分次数，仅用于积分通道，目前只有1有效，1：积分一次
        /// </summary>
        public const string XmlTag_IntCount = "IntCount";

        public const string XmlTag_TimeSpan = "TimeSpan";

        /// <summary>
        /// 电压偏移
        /// </summary>
        public const string XmlTag_VoltageOffset = "VoltageOffset";

        /// <summary>
        /// 波形数据的转速下限，低于下限则不上传或保持该波形数据，单位是rpm
        /// </summary>
        public const string XmlTag_WaveRevLowThreshold = "WaveRevLowThreshold";

        #endregion

        #region 转化为xml

        /// <summary>
        /// 转化为xml字符串
        /// </summary>
        /// <returns>xml字符串</returns>
        public string ToXml()
        {
            return XmlUtils.XmlSerialize2Xml( this );
        }

        #endregion

        #region 加载xml文件

        /// <summary>
        /// 解析xml，返回SampleStationData对象
        /// </summary>
        /// <param name="path">xml文件路径</param>
        /// <returns>SampleStationData对象</returns>
        public static SampleStationData ParseXml( string path )
        {
            return XmlUtils.XmlDeserializeFromFile<SampleStationData>( path );
        }

        #endregion

        #endregion
    }
}