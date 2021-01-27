using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private readonly Stopwatch _sw = new Stopwatch();
        protected override void Decode(IChannelHandlerContext context, IByteBuffer input, List<object> output)
        {
            int headCount = PackageSendReceive.Count_Head;
            int tailCount = PackageSendReceive.Count_Tail;

            int decodeCount = 0;
            while (true)
            {
                if (input.ReadableBytes < headCount + tailCount)
                {
                    //if( decodeCount == 0 ) Config.LogTcp($"{contextInfo}: if (input.ReadableBytes < headCount + tailCount).");
                    break;
                }
                var contextInfo = $"sampdecode({GetHashCode()})@{Config.GetContextInfo(context)} ";
                input.MarkReaderIndex();

                _sw.Restart();

                //var headBytes = new byte[headCount];
                //input.ReadBytes(headBytes);
                //PackageSendReceive.CheckPart1(headBytes);
                //PackageSendReceive.CheckPart2(headBytes, out bodyLength);

                //var headBuffer = input.ReadBytes(headCount);
                var headBuffer = input.ReadSlice(headCount);
                int bodyLength;
                try
                {
                    ArraySegment<byte> ioBuf = headBuffer.GetIoBuffer();
                    var headBytes = ioBuf.Array;
                    int offset = ioBuf.Offset;
                    //int count = ioBuf.Count;
                    int count = headCount;
                    PackageSendReceive.CheckPart1(headBytes.AsSpan(offset, count));

                    _sw.Stop();

                    Config.LogTcp($"{contextInfo}: checkpart1,{_sw.ElapsedMilliseconds} ms", ioBuf);

                    _sw.Restart();

                    PackageSendReceive.CheckPart2(headBytes, offset, count, out bodyLength);
                    //Config.LogTcp($"{contextInfo}: checkpart2,{nameof(bodyLength)}: {bodyLength}");
                }
                finally
                {
                    //headBuffer.Release();
                    //Config.LogTcp($"{contextInfo}: headbuffer?.release");
                }

                if (input.ReadableBytes < bodyLength + tailCount)
                {
                    //Config.LogTcp($"{contextInfo}: if (input.ReadableBytes < bodyLength + tailCount ).");
                    input.ResetReaderIndex();
                    return;
                }

                //byte[] bodyBytes = new byte[bodyLength];
                //input.ReadBytes(bodyBytes);

                //byte[] tailBytes = new byte[tailCount];
                //input.ReadBytes(tailBytes);
                //PackageSendReceive.CheckPart3( bodyBytes,0, bodyBytes.Length, tailBytes);

                //var bodyBuffer = input.ReadBytes(bodyLength + tailCount);
                var bodyBuffer = input.ReadRetainedSlice(bodyLength + tailCount);
                {
                    ArraySegment<byte> ioBuf = bodyBuffer.GetIoBuffer();
                    var bodyBytes = ioBuf.Array;
                    int offset = ioBuf.Offset;
                    //Config.LogTcp($"{contextInfo}: checkpart3", ioBuf);

                    var tailBytes = bodyBytes.AsSpan(offset + bodyLength, tailCount);
                    //PackageSendReceive.CheckPart3(bodyBytes.AsSpan(offset, bodyLength), tailBytes);
                    PackageSendReceive.CheckPart3(bodyBytes, offset, bodyLength, tailBytes);

                    output.Add(bodyBuffer);

                    _sw.Stop();
                }
                Config.LogTcp($"{contextInfo}:dCount:{++decodeCount},{_sw.ElapsedMilliseconds} ms.");

                //output.Add(bodyBytes);
            }
            //Config.LogTcp($"{contextInfo}: end.");
        }
    }
}
