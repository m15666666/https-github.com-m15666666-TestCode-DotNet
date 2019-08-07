using System;
using System.Collections.Generic;
using System.Text;
using AnalysisData.ToFromBytes;
using DotNetty.Buffers;
using DotNetty.Transport.Channels;
using Moons.Common20;
using Moons.Common20.Serialization;

namespace DataSampler.Core.Helper
{
    /// <summary>
    /// 数据采集器的netty服务类
    /// </summary>
    public sealed class SamplerServerHandler : SimpleChannelInboundHandler<byte[]>
    {
        //public override void ChannelActive(IChannelHandlerContext context) => context.WriteAndFlushAsync(this.initialMessage);

        protected override void ChannelRead0(IChannelHandlerContext context, byte[] message)
        {
            CommandMessage command = ToFromBytesUtils.ReadCommandMessage(message);
            Config.LogCommandMessage(command);
            Action<byte[]> sendHandler = bytes => context.WriteAsync(bytes);
            DataSamplerController.Instance.OnReceiveCommandMessage(command, sendHandler);
            context.Flush();
        }

        public override void ChannelReadComplete(IChannelHandlerContext context) => context.Flush();

        public override void ExceptionCaught(IChannelHandlerContext context, Exception exception)
        {
            TraceUtils.Error("SamplerServerHandler ExceptionCaught", exception);
            Console.WriteLine("Exception: " + exception);
            context.CloseAsync();
        }
    }
}
