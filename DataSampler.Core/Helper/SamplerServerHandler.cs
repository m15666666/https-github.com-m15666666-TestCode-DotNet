using System;
using System.Collections.Generic;
using System.Text;
using AnalysisData.ToFromBytes;
using DotNetty.Buffers;
using DotNetty.Transport.Channels;
using Moons.Common20;
using Moons.Common20.Serialization;
using SocketLib;

namespace DataSampler.Core.Helper
{
    /// <summary>
    /// 数据采集器的netty服务类
    /// </summary>
    public sealed class SamplerServerHandler : SimpleChannelInboundHandler<IByteBuffer>
    {
        //public override void ChannelActive(IChannelHandlerContext context) => context.WriteAndFlushAsync(this.initialMessage);

        protected override void ChannelRead0(IChannelHandlerContext context, IByteBuffer message)
        {
            var contextInfo = Config.GetContextInfo(context);
            Config.LogTcp($"{contextInfo}: read command messge begin...");
            ArraySegment<byte> ioBuf = message.GetIoBuffer();
            var bodyBytes = ioBuf.Array;
            int offset = ioBuf.Offset;
            int count = ioBuf.Count;
            Config.LogTcp($"{contextInfo}: SamplerServerHandler", ioBuf);

            CommandMessage command = ToFromBytesUtils.ReadCommandMessage(bodyBytes, offset, count - PackageSendReceive.Count_Tail);
            //Config.LogTcp("read command messge end.");
            Config.LogCommandMessage(command, contextInfo);
            //Config.LogTcp("log command messge end.");
            Action<byte[]> sendHandler = bytes => context.WriteAsync(bytes);
            DataSamplerController.Instance.OnReceiveCommandMessage(command, sendHandler);
            Config.LogTcp($"{contextInfo}: on receive command messge end.");
            context.Flush();
            //Config.LogTcp("context.flush end.");
        }

        public override void ChannelReadComplete(IChannelHandlerContext context) => context.Flush();

        public override void ExceptionCaught(IChannelHandlerContext context, Exception exception)
        {
            var contextInfo = Config.GetContextInfo(context);
            Config.TcpLogger.Error($"{contextInfo}: SamplerServerHandler ExceptionCaught", exception);
            Console.WriteLine("Exception: " + exception);
            context.CloseAsync();
        }
    }
}
