using System;

namespace Moons.Common20.Probes
{
    /// <summary>
    /// 代表资源的数据类
    /// </summary>
    public class ResourceData : EntityBase
    {
        /// <summary>
        /// 该资源的键
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 该资源的描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 该资源是否已经被释放
        /// </summary>
        public bool Disposed { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 释放时间
        /// </summary>
        public DateTime DisposeTime { get; set; }
    }
}