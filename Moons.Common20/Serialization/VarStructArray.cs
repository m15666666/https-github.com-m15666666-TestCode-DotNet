using System;
using System.Collections.Generic;
using System.Text;

namespace Moons.Common20.Serialization
{
    /// <summary>
    /// 变长结构体数组
    /// </summary>
    public class VarStructArray : List<object>
    {
        /// <summary>
        /// 结构类型ID
        /// </summary>
        public int StructTypeID { get; set; }

        public override string ToString() => $"VSA:STID={StructTypeID}:{Count}";
    }
}
