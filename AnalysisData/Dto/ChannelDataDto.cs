using System;
using System.Collections.Generic;
using System.Text;

namespace AnalysisData.Dto
{
    /// <summary>
    /// 通道数据类
    /// </summary>
    [Serializable]
    public class ChannelDataDto
    {
        #region 连续报警次数（超过这个次数才报警，次数为0则不报警）

        /// <summary>
        /// 连续报警次数
        /// </summary>

        /// <summary>
        /// 连续报警次数
        /// </summary>
        public List<AlmCountDataDto> AlmCountDatas { get; set; } = new List<AlmCountDataDto>();

        #endregion

        #region 变量和属性

        /// <summary>
        /// 以字符串表示的通道标识符
        /// </summary>
        public string ChannelIdentifier { get; set; }

        /// <summary>
        /// 通道CD
        /// </summary>
        public string ChannelCD
        {
            get;
            set;
        }

        /// <summary>
        /// 转速通道CD，为空字符串表示未引用，参考转速通道。
        /// </summary>
        public string RevChannelCD
        {
            get;
            set;
        }

        /// <summary>
        /// 转速通道CD，为空字符串表示未引用，参考键相通道。
        /// </summary>
        public string KeyPhaserRevChannelCD
        {
            get;
            set;
        }

        /// <summary>
        /// 参考开关量通道，为空字符串表示未引用。
        /// </summary>
        public string SwitchChannelCD
        {
            get;
            set;
        }

        /// <summary>
        /// 开关量触发状态
        /// </summary>
        public bool SwitchTriggerStatus
        {
            get;
            set;
        }

        /// <summary>
        /// 开关量触发方式，1：高电平触发，0：低电平触发。
        /// </summary>
        public int SwitchTriggerMethod
        {
            get;
            set;
        }

        /// <summary>
        /// 通道类型ID，1：动态通道，2：静态通道，3：转速通道，5：开关量通道
        /// </summary>
        public int ChannelTypeID
        {
            get;
            set;
        }

        /// <summary>
        /// 通道号，例如：1，2，3，4，5，6，7，...，32
        /// </summary>
        public int ChannelNumber
        {
            get;
            set;
        }

        /// <summary>
        /// 信号类型ID（测量参量），102：加速度、101：速度、119：冲击、103：位移
        /// </summary>
        public int SignalTypeID
        {
            get;
            set;
        }

        /// <summary>
        /// 采样频率
        /// </summary>
        public float SampleFreq
        {
            get;
            set;
        }

        /// <summary>
        /// 倍频系数
        /// </summary>
        public int MultiFreq
        {
            get;
            set;
        }

        /// <summary>
        /// 数据长度
        /// </summary>
        public int DataLength
        {
            get;
            set;
        }

        /// <summary>
        /// 平均次数
        /// </summary>
        public int AverageCount
        {
            get;
            set;
        }

        /// <summary>
        /// 转速下限，低于下限则转速为0，单位是rpm
        /// </summary>
        public int RevLowThreshold
        {
            get;
            set;
        }

        /// <summary>
        /// 转速上限，高于上限则转速为0，单位是rpm
        /// </summary>
        public int RevHighThreshold
        {
            get;
            set;
        }

        /// <summary>
        /// 参考工作转速
        /// </summary>
        public int ReferenceRev
        {
            get;
            set;
        }

        /// <summary>
        /// 转速类型（只用于转速通道）：1：单脉冲，2： 多脉冲 
        /// </summary>
        public int RevTypeID
        {
            get;
            set;
        }

        /// <summary>
        /// 转速比例因子（只用于转速通道），默认为1
        /// </summary>
        public float RevRatio
        {
            get;
            set;
        }

        /// <summary>
        /// 工程单位与电压单位的比例因子，电压乘上因子变成工程单位
        /// </summary>
        public float ScaleFactor
        {
            get;
            set;
        }

        /// <summary>
        /// 电压偏移
        /// </summary>
        public float VoltageOffset
        {
            get;
            set;
        }

        /// <summary>
        ///     工程单位与电压单位的比例因子的工程单位ID
        /// </summary>
        public int ScaleFactorEngUnitID { get; set; }

        #region 电涡流传感器通道参数

        #region 中心点电压(电涡流传感器通道参数)

        /// <summary>
        ///     中心点电压(电涡流传感器通道参数)
        /// </summary>
        public float CenterPositionVoltage { get; set; }

        /// <summary>
        ///     中心点电压(电涡流传感器通道参数)的工程单位ID
        /// </summary>
        public int CenterPositionVoltageEngUnitID { get; set; }

        #endregion

        #region 中心点位置(电涡流传感器通道参数)

        /// <summary>
        ///     中心点位置(电涡流传感器通道参数)
        /// </summary>
        public float CenterPosition { get; set; }

        /// <summary>
        ///     中心点位置(电涡流传感器通道参数)的工程单位ID
        /// </summary>
        public int CenterPositionEngUnitID { get; set; }

        #endregion

        #region 线性范围(电涡流传感器通道参数)

        /// <summary>
        ///     线性范围最小值(电涡流传感器通道参数)
        /// </summary>
        public float LinearRangeMin { get; set; }

        /// <summary>
        ///     线性范围最大值(电涡流传感器通道参数)
        /// </summary>
        public float LinearRangeMax { get; set; }

        /// <summary>
        ///     线性范围(电涡流传感器通道参数)的工程单位ID
        /// </summary>
        public int LinearRangeEngUnitID { get; set; }

        #endregion

        #endregion

        #endregion
    }
}
