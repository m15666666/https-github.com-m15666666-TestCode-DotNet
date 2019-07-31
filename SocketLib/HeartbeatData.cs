using System;

namespace SocketLib
{
    /// <summary>
    /// 心跳包数据类
    /// </summary>
    [Serializable]
    public class HeartbeatData
    {
        public DateTime CreateTime { get; set; }

        public HeartbeatData()
        {
            CreateTime = DateTime.Now;
        }
    }
}
