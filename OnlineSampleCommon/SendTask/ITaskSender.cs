namespace OnlineSampleCommon.SendTask
{
    /// <summary>
    /// 底层发送任务的接口
    /// </summary>
    public interface ITaskSender
    {
        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="emsg">返回出错信息，为空表示没出错</param>
        void Send( object data, ref string emsg );

        /// <summary>
        /// 将在较长一段时间内不发送数据
        /// </summary>
        void StopSend();
    }
}