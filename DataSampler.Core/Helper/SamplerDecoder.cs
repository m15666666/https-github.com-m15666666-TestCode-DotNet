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
            var contextInfo = Config.GetContextInfo(context);
            Config.LogTcp($"{contextInfo}: sampler decoder begin...");
            int headCount = PackageSendReceive.Count_Head;
            int tailCount = PackageSendReceive.Count_Tail;

            if (input.ReadableBytes < headCount + tailCount)
            {
                Config.LogTcp($"{contextInfo}: if (input.ReadableBytes < headCount + tailCount).");
                return;
            }
            input.MarkReaderIndex();

            //var headBytes = new byte[headCount];
            //input.ReadBytes(headBytes);
            //PackageSendReceive.CheckPart1(headBytes);
            //PackageSendReceive.CheckPart2(headBytes, out bodyLength);
            
            var headBuffer = input.ReadBytes(headCount);
            int bodyLength;
            try
            {
                ArraySegment<byte> ioBuf = headBuffer.GetIoBuffer();
                var headBytes = ioBuf.Array;
                int offset = ioBuf.Offset;
                int count = ioBuf.Count;
                PackageSendReceive.CheckPart1(headBytes.AsSpan(offset, count));
                Config.LogTcp($"{contextInfo}: checkpart1", ioBuf);

                PackageSendReceive.CheckPart2(headBytes, offset, count, out bodyLength);
            }
            finally
            {
                headBuffer?.Release();
            }

            if (input.ReadableBytes < bodyLength + tailCount )
            {
                Config.LogTcp($"{contextInfo}: if (input.ReadableBytes < bodyLength + tailCount ).");
                input.ResetReaderIndex();
                return;
            }

            //byte[] bodyBytes = new byte[bodyLength];
            //input.ReadBytes(bodyBytes);

            //byte[] tailBytes = new byte[tailCount];
            //input.ReadBytes(tailBytes);
            //PackageSendReceive.CheckPart3( bodyBytes,0, bodyBytes.Length, tailBytes);

            var bodyBuffer = input.ReadBytes(bodyLength + tailCount);
            {
                ArraySegment<byte> ioBuf = bodyBuffer.GetIoBuffer();
                var bodyBytes = ioBuf.Array;
                int offset = ioBuf.Offset;
                Config.LogTcp($"{contextInfo}: checkpart3",ioBuf);

                var tailBytes = bodyBytes.AsSpan(offset + bodyLength, tailCount);
                PackageSendReceive.CheckPart3( bodyBytes.AsSpan(offset,bodyLength), tailBytes);
                
                output.Add(bodyBuffer);
            }
            Config.LogTcp($"{contextInfo}: sampler decoder end.");

            //output.Add(bodyBytes);
        }
    }
}
