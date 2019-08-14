using System;
using System.Collections.Generic;
using System.Text;

namespace Moons.DataSample.Shared.Dto
{
    /// <summary>
    /// 报警事件数据类
    /// </summary>
    [Serializable]
    public class AlmEventDataDto
    {
        /// <summary>
        /// 测点编号
        /// </summary>
        public int PointID { get; set; }

        /// <summary>
        /// 报警时间
        /// </summary>
        public DateTime AlmTime { get; set; }

        /// <summary>
        /// 报警等级
        /// </summary>
        public int AlmLevel { get; set; }

        /// <summary>
        /// 报警来源ID
        /// </summary>
        public int AlmSourceID { get; set; }

        /// <summary>
        /// 报警事件的唯一ID
        /// </summary>
        public string AlmEventUniqueID { get; set; }

        /// <summary>
        /// 报警ID
        /// </summary>
        public long AlmID { get; set; }

        /// <summary>
        /// 连续报警次数
        /// </summary>
        public int AlmCount { get; set; }

        /// <summary>
        /// 报警值
        /// </summary>
        public float AlmValue { get; set; }
    }
}
