using System;
using System.Collections.Generic;
using System.Text;

namespace AnalysisData.Dto
{
    /// <summary>
    /// 测点数据类
    /// </summary>
    [Serializable]
    public class PointDataDto
    {
        /// <summary>
        /// 测点编号
        /// </summary>
        public int PointID { get; set; }

        /// <summary>
        /// 测量值类型ID，13：有效值、11：峰值、12：峰峰值、15：平均值、16：波形指标
        /// </summary>
        public int MeasValueTypeID { get; set; }

        /// <summary>
        /// 测点维数
        /// </summary>
        public int Dimension { get; set; }

        ///// <summary>
        ///// 测点名
        ///// </summary>
        //public string PointName { get; set; }

        /// <summary>
        /// 工程单位ID
        /// </summary>
        public int EngUnitID { get; set; }

        ///// <summary>
        ///// 工程单位
        ///// </summary>
        //public string EngUnit { get; set; }

        /// <summary>
        /// 采集器的通道CD，一维测点有一个元素，二维测点有两个元素
        /// </summary>
        public string[] ChannelCDs { get; set; }


        #region 普通报警门限值

        /// <summary>
        /// 普通报警门限集合
        /// </summary>
        public List<AlmStand_CommonSettingDataDto> AlmStand_CommonSettingDatas { get; set; } = new List<AlmStand_CommonSettingDataDto>();

        #endregion
    }
}
