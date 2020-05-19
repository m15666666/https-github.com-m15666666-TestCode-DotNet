using System;
using System.IO;
using Moons.Common20;
using Moons.Common20.CRC;
using Moons.Common20.Serialization;

namespace SocketLib
{
    /// <summary>
    /// 通过Socket发送/接收字节组成包的类
    /// </summary>
    public class PackageSendReceive : SendReceiveBase
    {
        #region 创建对象

        protected PackageSendReceive()
        {
            Receive_PackageHeadBytes =
                new byte[Count_PackageHeads + Count_Version + Count_PackageBodyLength + Count_Crc];
            Receive_PackageTailBytes = new byte[Count_Crc + Count_PackageTails];

            Send_PackageHeadBytes = new byte[Receive_PackageHeadBytes.Length];
            Send_PackageTailBytes = new byte[Receive_PackageTailBytes.Length];

            InitPackageHeadTailBuffer(Send_PackageHeadBytes, Send_PackageTailBytes);

            ReadByteBuffer = new ByteBuffer { CheckReadEnough = false };
        }

        /// <summary>
        /// 初始化包头包尾的缓存
        /// </summary>
        /// <param name="headBytes">包头缓存</param>
        /// <param name="tailBytes">包尾缓存</param>
        private static void InitPackageHeadTailBuffer(byte[] headBytes, byte[] tailBytes)
        {
            PackageHeads.CopyTo(headBytes, PackageHeadBytesIndex);
            VersionBytes.CopyTo(headBytes, VersionBytesIndex);

            PackageTails.CopyTo(tailBytes, PackageTailBytesIndex);
        }

        /// <summary>
        /// 创建PackageSendReceive对象
        /// </summary>
        /// <param name="socketWrapper">SocketWrapper，连接已经打开</param>
        /// <returns>PackageSendReceive对象</returns>
        public static PackageSendReceive CreatePackageSendReceive( SocketWrapper socketWrapper )
        {
            return new PackageSendReceive { InnerSocketWrapper = socketWrapper };
        }

        /// <summary>
        /// 创建客户端PackageSendReceive对象，首先接受ServerReply
        /// </summary>
        /// <param name="socketWrapper">SocketWrapper，连接已经打开</param>
        /// <returns>PackageSendReceive对象</returns>
        public static PackageSendReceive CreatePackageSendReceive_Client( SocketWrapper socketWrapper )
        {
            PackageSendReceive ret = CreatePackageSendReceive( socketWrapper );

            // 客户端不使用3.5特性
            ret.InnerSocketWrapper.UseFramework35Feature = false;

            return ret;
        }

        /// <summary>
        /// 创建服务器端PackageSendReceive对象，首先发送ServerReply
        /// </summary>
        /// <param name="socketWrapper">SocketWrapper，连接已经打开</param>
        /// <returns>PackageSendReceive对象</returns>
        public static PackageSendReceive CreatePackageSendReceive_Server( SocketWrapper socketWrapper )
        {
            return CreatePackageSendReceive( socketWrapper );
        }

        #endregion

        #region 变量和属性

        #region 异步事件的代理

        /// <summary>
        /// 异步读包数据成功时调用的函数
        /// </summary>
        public Action<PackageSendReceive> ReadPackageSuccess { private get; set; }

        /// <summary>
        /// 异步读包数据失败时调用的函数
        /// </summary>
        public Action<PackageSendReceive> ReadPackageFail { private get; set; }

        /// <summary>
        /// 异步读取的字节数组缓存
        /// </summary>
        public ByteBuffer ReadByteBuffer { get; private set; }

        #endregion

        #region 包结构相关

        /// <summary>
        /// 包头分界符第一个字节：b
        /// </summary>
        private static readonly byte PackageHead1 = 0x62;

        /// <summary>
        /// 包头分界符第二个字节：e
        /// </summary>
        private static readonly byte PackageHead2 = 0x65;

        /// <summary>
        /// 包头分界符
        /// </summary>
        private static readonly byte[] PackageHeads = new[] { PackageHead1, PackageHead2 };

        /// <summary>
        /// 包尾分界符第一个字节：n
        /// </summary>
        private static readonly byte PackageTail1 = 0x6E;

        /// <summary>
        /// 包尾分界符第二个字节：d
        /// </summary>
        private static readonly byte PackageTail2 = 0x64;

        /// <summary>
        /// 包尾分界符
        /// </summary>
        private static readonly byte[] PackageTails = new[] { PackageTail1, PackageTail2 };

        /// <summary>
        /// 版本号对应的字节数组
        /// </summary>
        private static readonly byte[] VersionBytes = ByteUtils.Int32ToBytes( 1 );

        #region 包各个部分的字节个数

        /// <summary>
        /// 包体长度的字节个数
        /// </summary>
        private const int Count_PackageBodyLength = 4;

        /// <summary>
        /// crc校验的字节个数
        /// </summary>
        private const int Count_Crc = 1;

        /// <summary>
        /// 包头分界符的字节个数
        /// </summary>
        private static readonly int Count_PackageHeads = PackageHeads.Length;

        /// <summary>
        /// 包尾分界符的字节个数
        /// </summary>
        private static readonly int Count_PackageTails = PackageTails.Length;

        /// <summary>
        /// 版本号的字节个数
        /// </summary>
        private static readonly int Count_Version = VersionBytes.Length;

        /// <summary>
        /// 包头字节数
        /// </summary>
        public static int Count_Head
        {
            get
            {
                return Count_PackageHeads + Count_Version + Count_PackageBodyLength + Count_Crc;
            }
        }

        /// <summary>
        /// 包尾字节数
        /// </summary>
        public static int Count_Tail
        {
            get
            {
                return Count_Crc + Count_PackageTails;
            }
        }

        #endregion

        #region 包头下标

        /// <summary>
        /// 包头分界符字节位于的下标
        /// </summary>
        private const int PackageHeadBytesIndex = 0;

        /// <summary>
        /// 版本字节位于的下标
        /// </summary>
        private static readonly int VersionBytesIndex = PackageHeadBytesIndex + Count_PackageHeads;

        /// <summary>
        /// 包体长度字节位于的下标
        /// </summary>
        private static readonly int PackageBodyLengthBytesIndex = VersionBytesIndex + Count_Version;

        /// <summary>
        /// 包头crc字节位于的下标
        /// </summary>
        private static readonly int PackageHeadCrcIndex = PackageBodyLengthBytesIndex + Count_PackageBodyLength;

        #endregion

        #region 包尾下标

        /// <summary>
        /// 包尾分界符字节位于的下标
        /// </summary>
        private const int PackageTailBytesIndex = 0 + Count_Crc;

        #endregion

        #region 接收相关

        /// <summary>
        /// 接收用的包头字节数组，包括：0x62(b)+ 0x65(e)+版本号(四个字节，目前为：0x00000001)+包体长度(四个字节)+ CRC校验码(一个字节，只校验版本号和包体长度)。
        /// </summary>
        private readonly byte[] Receive_PackageHeadBytes;

        /// <summary>
        /// 接收用的包尾字节数组，包括：CRC校验码(一个字节，只校验包体数据)+0x6E(n)+0x64(d)。
        /// </summary>
        private readonly byte[] Receive_PackageTailBytes;

        #endregion

        #region 发送相关

        /// <summary>
        /// 发送用的包头字节数组，包括：0x62(b)+ 0x65(e)+版本号(四个字节，目前为：0x00000001)+包体长度(四个字节)+ CRC校验码(一个字节，只校验版本号和包体长度)。
        /// </summary>
        private readonly byte[] Send_PackageHeadBytes;

        /// <summary>
        /// 发送用的包尾字节数组，包括：CRC校验码(一个字节，只校验包体数据)+0x6E(n)+0x64(d)。
        /// </summary>
        private readonly byte[] Send_PackageTailBytes;

        #endregion

        #endregion

        #endregion

        #region 数据发送

        /// <summary>
        /// 发送数据包
        /// </summary>
        /// <param name="packageBody">包内容</param>
        public void SendPackage( byte[] packageBody )
        {
            byte[] headBytes = Send_PackageHeadBytes;
            ByteUtils.Int32ToBytes( packageBody.Length ).CopyTo( headBytes, PackageBodyLengthBytesIndex );

            // 只校验版本号和包体长度
            byte headCrc = CrcUtils.CrcDefault8;
            XORCRC8.Crc( headBytes, VersionBytesIndex, Count_Version + Count_PackageBodyLength, ref headCrc );

            headBytes[PackageHeadCrcIndex] = headCrc;

            // 只校验包体数据
            byte bodyCrc = CrcUtils.CrcDefault8;
            XORCRC8.Crc( packageBody, ref bodyCrc );

            byte[] tailBytes = Send_PackageTailBytes;
            tailBytes[0] = bodyCrc;

            var sendBytes = new byte[headBytes.Length + packageBody.Length + tailBytes.Length];
            headBytes.CopyTo( sendBytes, 0 );
            packageBody.CopyTo( sendBytes, headBytes.Length );
            tailBytes.CopyTo( sendBytes, headBytes.Length + packageBody.Length );

            SendBytes( sendBytes );

            //SendBytes( headBytes );
            //SendBytes( packageBody );
            //SendBytes( tailBytes );
        }

        /// <summary>
        /// 初始化用于发送的包
        /// </summary>
        /// <param name="headBytes"></param>
        /// <param name="tailBytes"></param>
        /// <param name="packageBody"></param>
        public static void InitPackage4Sending( byte[] headBytes, byte[] tailBytes, byte[] packageBody )
        {
            InitPackageHeadTailBuffer(headBytes, tailBytes);

            ByteUtils.Int32ToBytes(packageBody.Length).CopyTo(headBytes, PackageBodyLengthBytesIndex);

            // 只校验版本号和包体长度
            byte headCrc = CrcUtils.CrcDefault8;
            XORCRC8.Crc(headBytes, VersionBytesIndex, Count_Version + Count_PackageBodyLength, ref headCrc);

            headBytes[PackageHeadCrcIndex] = headCrc;

            // 只校验包体数据
            byte bodyCrc = CrcUtils.CrcDefault8;
            XORCRC8.Crc(packageBody, ref bodyCrc);

            tailBytes[0] = bodyCrc;
        }

        #endregion

        #region 数据接收

        #region 校验数据包

        /// <summary>
        /// 检查第一部分：包头分界符、包头版本号。
        /// </summary>
        private void CheckPart1()
        {
            CheckPart1( Receive_PackageHeadBytes );
        }

        /// <summary>
        /// 检查第一部分：包头分界符、包头版本号。
        /// </summary>
        public static void CheckPart1(Span<byte> headBytes)
        {
            bool ok = ArrayUtils.Equal(PackageHeads.AsSpan(0,PackageHeads.Length), headBytes.Slice(PackageHeadBytesIndex, PackageHeads.Length));
            if (!ok)
            {
                throw new PackageStructException(string.Format("Package head split error({0})", StringUtils.ToHex(headBytes)))
                { StructError = PackageStructError.PackageHead };
            }

            ok = ArrayUtils.Equal(VersionBytes.AsSpan(0,VersionBytes.Length), headBytes.Slice(VersionBytesIndex, VersionBytes.Length));
            if (!ok)
            {
                throw new PackageStructException(string.Format("Package head version error({0})", StringUtils.ToHex(headBytes)))
                { StructError = PackageStructError.PackageHeadVersion };
            }
        }
        /// <summary>
        /// 检查第一部分：包头分界符、包头版本号。
        /// </summary>
        public static void CheckPart1(byte[] headBytes)
        {
            CheckPart1(headBytes.AsSpan());
            //bool ok = ArrayUtils.Equal(PackageHeads, 0, headBytes, PackageHeadBytesIndex, PackageHeads.Length);
            //if (!ok)
            //{
            //    throw new PackageStructException(string.Format("Package head split error({0})", StringUtils.ToHex(headBytes)))
            //    { StructError = PackageStructError.PackageHead };
            //}

            //ok = ArrayUtils.Equal(VersionBytes, 0, headBytes, VersionBytesIndex, VersionBytes.Length);
            //if (!ok)
            //{
            //    throw new PackageStructException(string.Format("Package head version error({0})", StringUtils.ToHex(headBytes)))
            //    { StructError = PackageStructError.PackageHeadVersion };
            //}
        }

        /// <summary>
        /// 检查第二部分：包头crc。
        /// </summary>
        /// <param name="bodyLength">包体长度</param>
        private void CheckPart2( out int bodyLength )
        {
            CheckPart2(Receive_PackageHeadBytes, 0, Receive_PackageHeadBytes.Length, out bodyLength);
        }

        /// <summary>
        /// 检查第二部分：包头crc。
        /// </summary>
        /// <param name="bodyLength">包体长度</param>
        public static void CheckPart2(byte[] headBytes, int offset, int size, out int bodyLength)
        {
            byte headCrc = CrcUtils.CrcDefault8;
            XORCRC8.Crc(headBytes, offset + VersionBytesIndex, Count_Version + Count_PackageBodyLength, ref headCrc);

            byte transferHeadCrc = headBytes[offset+size-1];
            bool ok = headCrc == transferHeadCrc;
            if (!ok)
            {
                throw new PackageStructException(string.Format("Package head crc error, {0} != {1}({2}).", headCrc, transferHeadCrc,
                                                                 StringUtils.ToHex(headBytes,offset,size)))
                { StructError = PackageStructError.PackageHeadCrc };
            }

            bodyLength = BitConverter.ToInt32(headBytes, offset + PackageBodyLengthBytesIndex);
        }

        ///// <summary>
        ///// 检查第二部分：包头crc。
        ///// </summary>
        ///// <param name="bodyLength">包体长度</param>
        ////public static void CheckPart2(byte[] headBytes, out int bodyLength)
        ////{
        //    //CheckPart2(headBytes, 0, headBytes.Length, out bodyLength);
        //    //byte headCrc = CrcUtils.CrcDefault8;
        //    //XORCRC8.Crc(headBytes, VersionBytesIndex, Count_Version + Count_PackageBodyLength, ref headCrc);

        //    //byte transferHeadCrc = ArrayUtils.Last(headBytes);
        //    //bool ok = headCrc == transferHeadCrc;
        //    //if (!ok)
        //    //{
        //    //    throw new PackageStructException(string.Format("Package head crc error, {0} != {1}({2}).", headCrc, transferHeadCrc,
        //    //                                                     StringUtils.ToHex(headBytes)))
        //    //    { StructError = PackageStructError.PackageHeadCrc };
        //    //}

        //    //bodyLength = BitConverter.ToInt32(headBytes, PackageBodyLengthBytesIndex);
        ////}

        /// <summary>
        /// 检查第三部分：包体、包尾crc，包尾分界符。
        /// </summary>
        private void CheckPart3()
        {
            ByteBuffer readByteBuffer = ReadByteBuffer;
            CheckPart3(readByteBuffer.Buffer.AsSpan(readByteBuffer.Offset, readByteBuffer.Size2Read), Receive_PackageTailBytes.AsSpan());
            //CheckPart3(readByteBuffer.Buffer, readByteBuffer.Offset, readByteBuffer.Size2Read, Receive_PackageTailBytes);
        }

        /// <summary>
        /// 检查第三部分：包体、包尾crc，包尾分界符。
        /// </summary>
        public static void CheckPart3(Span<byte> buffer, Span<byte> tailBytes)
        {
            byte bodyCrc = CrcUtils.CrcDefault8;
            XORCRC8.Crc(buffer, 0, buffer.Length, ref bodyCrc);

            byte transferBodyCrc = tailBytes[0];
            bool ok = bodyCrc == transferBodyCrc;
            if (!ok)
            {
                throw new PackageStructException(string.Format("Package tail crc error, {0} != {1}({2}).", bodyCrc, transferBodyCrc,
                                                                 StringUtils.ToHex(tailBytes)))
                { StructError = PackageStructError.PackageTailCrc };
            }

            ok = ArrayUtils.Equal(PackageTails.AsSpan(0, PackageTails.Length), tailBytes.Slice(PackageTailBytesIndex,PackageTails.Length));
            if (!ok)
            {
                throw new PackageStructException(string.Format("Package tail split error({0}).", StringUtils.ToHex(tailBytes)))
                { StructError = PackageStructError.PackageTail };
            }
        }
        /// <summary>
        /// 检查第三部分：包体、包尾crc，包尾分界符。
        /// </summary>
        public static void CheckPart3(byte[] buffer, int offset, int size, byte[] tailBytes)
        {
            CheckPart3(buffer.AsSpan(offset, size), tailBytes.AsSpan());
            //byte bodyCrc = CrcUtils.CrcDefault8;
            //XORCRC8.Crc(buffer, offset, size, ref bodyCrc);

            //byte transferBodyCrc = ArrayUtils.First(tailBytes);
            //bool ok = bodyCrc == transferBodyCrc;
            //if (!ok)
            //{
            //    throw new PackageStructException(string.Format("Package tail crc error, {0} != {1}({2}).", bodyCrc, transferBodyCrc,
            //                                                     StringUtils.ToHex(tailBytes)))
            //    { StructError = PackageStructError.PackageTailCrc };
            //}

            //ok = ArrayUtils.Equal(PackageTails, 0, tailBytes, PackageTailBytesIndex, PackageTails.Length);
            //if (!ok)
            //{
            //    throw new PackageStructException(string.Format("Package tail split error({0}).", StringUtils.ToHex(tailBytes)))
            //    { StructError = PackageStructError.PackageTail };
            //}
        }

        #endregion

        #region 接收数据包

        /// <summary>
        ///     接收数据包
        /// </summary>
        public void ReceivePackage()
        {
            while( true )
            {
                // 0x62(b)+ 0x65(e)+版本号(四个字节，目前为：0x00000001)
                ReceiveBytes( Receive_PackageHeadBytes, 0, Count_PackageHeads + Count_Version );

                ContinueReceivePackage();

                using( MemoryStream readStream = ReadByteBuffer.CreateReadStream() )
                {
                    using( var reader = new BinaryReader( readStream ) )
                    {
                        // 如果读到了心跳包，则继续读下一个包。
                        if( CommandIDs.Heartbeat == reader.ReadInt32() )
                        {
                            continue;
                        }

                        // 非心跳包则返回
                        return;
                    }
                }
            } // 一直循环读取，直到读取一个非心跳包
        }

        /// <summary>
        /// 收到包头分界符和版本后，继续接收包体
        /// </summary>
        private void ContinueReceivePackage()
        {
            CheckPart1();

            // 包体长度(四个字节)+ CRC校验码(一个字节，只校验版本号和包体长度)
            ReceiveBytes( Receive_PackageHeadBytes, PackageBodyLengthBytesIndex, Count_PackageBodyLength + Count_Crc );

            int bodyLength;
            CheckPart2( out bodyLength );

            var packageBody = new byte[bodyLength];
            ReadByteBuffer.Init( packageBody, 0, packageBody.Length );

            ReceiveBytes( packageBody );

            ReceiveBytes( Receive_PackageTailBytes );

            CheckPart3();
        }

        #endregion

        #region 异步接收数据包

        /// <summary>
        /// 异步接收数据包
        /// </summary>
        public void BeginReceivePackage()
        {
            BeginReceiveBytes( Receive_PackageHeadBytes, 0, Count_PackageHeads + Count_Version,
                               OnReceiveHeadSuccess_Part1, OnReceivePackageFail );
        }

        /// <summary>
        /// 响应异步接收数据包头第一部分的成功事件
        /// </summary>
        private void OnReceiveHeadSuccess_Part1( SocketWrapper socket )
        {
            try
            {
                CheckPart1();

                BeginReceiveBytes( Receive_PackageHeadBytes, PackageBodyLengthBytesIndex,
                                   Count_PackageBodyLength + Count_Crc,
                                   OnReceiveHeadSuccess_Part2, OnReceivePackageFail );
            }
            catch( Exception ex )
            {
                OnReceivePackageFail( ex );
            }
        }

        /// <summary>
        /// 响应异步接收数据包头第二部分的成功事件
        /// </summary>
        private void OnReceiveHeadSuccess_Part2( SocketWrapper socket )
        {
            try
            {
                int bodyLength;
                CheckPart2( out bodyLength );

                var packageBody = new byte[bodyLength];
                ReadByteBuffer.Init( packageBody, 0, packageBody.Length );

                BeginReceiveBytes( packageBody, OnReceivePackageBodySuccess, OnReceivePackageFail );
            }
            catch( Exception ex )
            {
                OnReceivePackageFail( ex );
            }
        }

        /// <summary>
        /// 响应异步接收数据包体的成功事件
        /// </summary>
        private void OnReceivePackageBodySuccess( SocketWrapper socket )
        {
            try
            {
                BeginReceiveBytes( Receive_PackageTailBytes,
                                   OnReceivePackageTailSuccess, OnReceivePackageFail );
            }
            catch( Exception ex )
            {
                OnReceivePackageFail( ex );
            }
        }

        /// <summary>
        /// 响应异步接收数据包尾的成功事件
        /// </summary>
        private void OnReceivePackageTailSuccess( SocketWrapper socket )
        {
            try
            {
                CheckPart3();

                FireEvent( ReadPackageSuccess );
            }
            catch( Exception ex )
            {
                OnReceivePackageFail( ex );
            }
        }

        /// <summary>
        /// 响应异步接收数据包的失败事件
        /// </summary>
        private void OnReceivePackageFail( SocketWrapper socketWrapper )
        {
            OnReceivePackageFail( socketWrapper.InnerException );
        }

        /// <summary>
        /// 响应异步接收数据包的失败事件
        /// </summary>
        private void OnReceivePackageFail( Exception ex )
        {
            InnerException = ex;
            FireEvent( ReadPackageFail );
        }

        #endregion

        #endregion
    }
}