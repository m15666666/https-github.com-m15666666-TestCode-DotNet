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
    public class SamplerDecoder : ByteToMessageDecoder
    {
        protected override void Decode(IChannelHandlerContext context, IByteBuffer input, List<object> output)
        {
            int headCount = PackageSendReceive.Count_Head;
            int tailCount = PackageSendReceive.Count_Tail;

            if (input.ReadableBytes < headCount + tailCount)
            {
                return;
            }
            input.MarkReaderIndex();

            var headBytes = new byte[headCount];
            input.ReadBytes(headBytes);

            PackageSendReceive.CheckPart1(headBytes);

            int bodyLength;
            PackageSendReceive.CheckPart2(headBytes, out bodyLength);

            if (input.ReadableBytes < bodyLength + tailCount )
            {
                input.ResetReaderIndex();
                return;
            }

            byte[] bodyBytes = new byte[bodyLength];
            input.ReadBytes(bodyBytes);

            byte[] tailBytes = new byte[tailCount];
            input.ReadBytes(tailBytes);
            PackageSendReceive.CheckPart3( bodyBytes,0, bodyBytes.Length, tailBytes);

            output.Add(bodyBytes);
        }
    }
}
