using System;
using System.Collections.Generic;
using System.Text;

namespace AnalysisData.Dto
{
    /// <summary>
    /// 普通报警设置数据类
    /// </summary>
    [Serializable]
    public class AlmStand_CommonSettingDataDto
    {
        /// <summary>
        /// 报警来源ID
        /// </summary>
        public int AlmSource_ID
        {
            get;
            set;
        }

        /// <summary>
        /// 报警类型编号
        /// </summary>
        public int AlmType_ID
        {
            get;
            set;
        }

        /// <summary>
        /// 报警级别1的下限值
        /// </summary>
        public double? LowLimit1_NR
        {
            get;
            set;
        }

        /// <summary>
        /// 报警级别2的下限值
        /// </summary>
        public double? LowLimit2_NR
        {
            get;
            set;
        }

        /// <summary>
        /// 报警级别1的上限值
        /// </summary>
        public double? HighLimit1_NR
        {
            get;
            set;
        }

        /// <summary>
        /// 报警级别2的上限值
        /// </summary>
        public double? HighLimit2_NR
        {
            get;
            set;
        }
    }
}
