using System;
using System.Collections.Generic;
using System.Text;

namespace AnalysisData.Dto
{
    /// <summary>
    /// 连续报警次数设置数据
    /// </summary>
    [Serializable]
    public class AlmCountDataDto
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
        /// 连续报警次数，为0表示不计算报警
        /// </summary>
        public int AlmCount
        {
            get;
            set;
        }
    }
}
