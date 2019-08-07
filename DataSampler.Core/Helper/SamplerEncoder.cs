using System;
using System.Collections.Generic;
using System.Text;
using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;
using SocketLib;

namespace DataSampler.Core.Helper
{
    /// <summary>
    /// 解码程序
    /// </summary>
    public class SamplerEncoder : MessageToMessageEncoder<byte[]>
    {
        protected override void Encode(IChannelHandlerContext context, byte[] packageBody, List<object> output)
        {
            int headCount = PackageSendReceive.Count_Head;
            int tailCount = PackageSendReceive.Count_Tail;

            var headBytes = new byte[headCount];
            var tailBytes = new byte[tailCount];
            PackageSendReceive.InitPackage4Sending(headBytes, tailBytes, packageBody);

            IByteBuffer buffer = context.Allocator.Buffer();

            buffer.WriteBytes(headBytes);
            buffer.WriteBytes(packageBody);
            buffer.WriteBytes(tailBytes);

            output.Add(buffer);
        }
    }
}
