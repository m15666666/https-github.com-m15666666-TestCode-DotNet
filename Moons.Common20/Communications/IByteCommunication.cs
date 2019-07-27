using System;

namespace Moons.Common20.Communications
{
    /// <summary>
    /// 通过字节传输通讯的接口
    /// </summary>
    public interface IByteCommunication : IDisposable
    {
        /// <summary>
        /// has data can read
        /// </summary>
        bool HasData { get; }

        /// <summary>
        /// 发送字节数组
        /// </summary>
        /// <param name="buffer">缓冲区</param>
        /// <param name="offset">偏移量</param>
        /// <param name="size">字节数</param>
        /// <returns>发送的字节数</returns>
        int Send( byte[] buffer, int offset, int size );

        /// <summary>
        /// 接收字节数组
        /// </summary>
        /// <param name="buffer">缓冲区</param>
        /// <param name="offset">偏移量</param>
        /// <param name="size">字节数</param>
        /// <returns>接收的字节数</returns>
        int Receive( byte[] buffer, int offset, int size );
    }
}