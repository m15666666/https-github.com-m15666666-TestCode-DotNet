using DotNetty.Common.Internal.Logging;
using DotNetty.Handlers.Logging;
using DotNetty.Handlers.Tls;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using Microsoft.Extensions.Logging;
using Moons.Common20;
using Moons.Common20.Logging;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataSampler.Core.Helper
{
    /// <summary>
    /// NettyListener
    /// </summary>
    internal class NettyListener : DisposableBase
    {
        internal void Init()
        {
            var f = new LoggerFactory();
            f.AddProvider(new LoggerProvider());
            InternalLoggerFactory.DefaultFactory = f;
        }

        #region Dispose

        ///<summary>
        /// socket事件
        /// </summary>
        private readonly AutoResetEvent _CloseSocketEvent = new AutoResetEvent(false);

        protected override void Dispose(bool disposing)
        {
            TraceUtils.Info($"dispose netty listener : {disposing}");
            if (IsDisposed)
            {
                return;
            }

            if (disposing)
            {
                TraceUtils.Info($"before _boundChannel.CloseAsync().Wait()");
                //_CloseSocketEvent.Set();
                _boundChannel.CloseAsync().Wait();

                TraceUtils.Info($"after _boundChannel.CloseAsync().Wait()");

                //释放工作组线程
                Task.WhenAll(
                    _bossGroup.ShutdownGracefullyAsync(TimeSpan.FromMilliseconds(100), TimeSpan.FromSeconds(1)),
                    _workerGroup.ShutdownGracefullyAsync(TimeSpan.FromMilliseconds(100), TimeSpan.FromSeconds(1))).Wait();
                TraceUtils.Info($"after _bossGroup.ShutdownGracefullyAsync(TimeSpan.FromMilliseconds(100), TimeSpan.FromSeconds(1))");
            }

            //  一定要调用基类的Dispose函数
            base.Dispose(disposing);
        }

        #endregion

        #region 变量
        /// <summary>
        /// 
        /// </summary>
        private MultithreadEventLoopGroup _bossGroup;

        /// <summary>
        /// 工作线程组，默认为内核数*2的线程数
        /// </summary>
        private MultithreadEventLoopGroup _workerGroup;

        private IChannel _boundChannel;

        #endregion

        internal async Task RunServerAsync()
        {
            // 主工作线程组，设置为1个线程
            _bossGroup = new MultithreadEventLoopGroup(1);
            // 工作线程组，默认为内核数*2的线程数
            _workerGroup = new MultithreadEventLoopGroup();
            X509Certificate2 tlsCertificate = null;
            //if (ServerSettings.IsSsl) //如果使用加密通道
            //{
            //    tlsCertificate = new X509Certificate2(Path.Combine("./", "dotnetty.com.pfx"), "password");
            //}
            try
            {

                //声明一个服务端Bootstrap，每个Netty服务端程序，都由ServerBootstrap控制，
                //通过链式的方式组装需要的参数

                var bootstrap = new ServerBootstrap();
                bootstrap
                    .Group(_bossGroup, _workerGroup) // 设置主和工作线程组
                    .Channel<TcpServerSocketChannel>() // 设置通道模式为TcpSocket
                    .Option(ChannelOption.SoBacklog, 100) // 设置网络IO参数等，这里可以设置很多参数，当然你对网络调优和参数设置非常了解的话，你可以设置，或者就用默认参数吧
                    .Handler(new LoggingHandler("SRV-LSTN")) //在主线程组上设置一个打印日志的处理器
                    .ChildHandler(new ActionChannelInitializer<ISocketChannel>(channel =>
                    { //工作线程连接器 是设置了一个管道，服务端主线程所有接收到的信息都会通过这个管道一层层往下传输
                      //同时所有出栈的消息 也要这个管道的所有处理器进行一步步处理
                        IChannelPipeline pipeline = channel.Pipeline;
                        if (tlsCertificate != null) //Tls的加解密
                        {
                            pipeline.AddLast("tls", TlsHandler.Server(tlsCertificate));
                        }
                        //日志拦截器
                        pipeline.AddLast(new LoggingHandler("SRV-CONN"));
                        // 出栈消息通过这个handler
                        pipeline.AddLast("SamplerEncoder", new SamplerEncoder());
                        // 入栈消息通过该Handler
                        pipeline.AddLast("SamplerDecoder", new SamplerDecoder());
                        // 业务handler
                        pipeline.AddLast("SamplerServerHandler", new SamplerServerHandler());
                    }));

                // bootstrap绑定到指定端口的行为 就是服务端启动服务，同样的Serverbootstrap可以bind到多个端口
                _boundChannel = await bootstrap.BindAsync(Config.ListenPortOfData);

                TraceUtils.Info($"before _CloseSocketEvent.WaitOne(); ");
                //_CloseSocketEvent.WaitOne();
                //TraceUtils.Info($"after _CloseSocketEvent.WaitOne(); ");

                //Console.ReadLine();
                //关闭服务
                //await _boundChannel.CloseAsync();
            }
            catch(Exception)
            {
                //释放工作组线程
                await Task.WhenAll(
                    _bossGroup.ShutdownGracefullyAsync(TimeSpan.FromMilliseconds(100), TimeSpan.FromSeconds(1)),
                    _workerGroup.ShutdownGracefullyAsync(TimeSpan.FromMilliseconds(100), TimeSpan.FromSeconds(1)));
            }
            finally
            {
                //释放工作组线程
                //await Task.WhenAll(
                //    _bossGroup.ShutdownGracefullyAsync(TimeSpan.FromMilliseconds(100), TimeSpan.FromSeconds(1)),
                //    _workerGroup.ShutdownGracefullyAsync(TimeSpan.FromMilliseconds(100), TimeSpan.FromSeconds(1)));
            }
        }
    }
}
