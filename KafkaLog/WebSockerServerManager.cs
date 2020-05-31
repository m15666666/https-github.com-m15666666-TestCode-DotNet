using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace KafkaLog
{
    /// <summary>
    /// 创建，管理WebSocketServer
    /// </summary>
    public static class WebSockerServerManager
    {
        private static WebSocketSharp.Server.WebSocketServer _webSocketServer = null;
        private static object _lock = new object();

        public static void StartWebSockerServer( int port )
        {
            lock (_lock) {
                if (_webSocketServer != null) StopWebSockerServer();

                var wssv = new WebSocketSharp.Server.WebSocketServer(port);
                wssv.AddWebSocketService<LogProducer>("/logproducer");
                wssv.AddWebSocketService<LogConsumer>("/logconsumer");
                wssv.AddWebSocketService<Echo>("/echo");
                wssv.Start();

                _webSocketServer = wssv;
            }
        }

        public static void StopWebSockerServer()
        {
            lock (_lock)
            {
                if (_webSocketServer == null) return;

                var wssv = _webSocketServer;
                _webSocketServer = null;

                wssv.Stop();
            }
        }
    }

    public abstract class LogBase : BehaviorBase<LogBase>
    {
        protected string _sourceId;
        protected string _apiKey;
        protected bool IsValidConnecton { get; set; } = false;

        protected override void OnOpen()
        {
            var sourceId = Context.QueryString["sourceId"];
            var apiKey = Context.QueryString["apiKey"];
            _sourceId = sourceId;
            _apiKey = apiKey;

            if ( apiKey == "a94f88f1-d5ae-467c-b36c-5357774c7dff")
            {
                //Console.WriteLine($"{sourceId}:{apiKey} connected.  Sessions.Count: {Sessions.Count}.");
                base.OnOpen();
                PrintDebugMessage($"{IdString} OnOpen");
                IsValidConnecton = true;
                return;
            }

            Console.WriteLine($"{IdString} connection is cut off. Sessions.Count: {Sessions.Count}.");
            Context.WebSocket.Close();
        }

        protected override void OnMessage(MessageEventArgs e)
        {
        }

        protected override void OnClose(CloseEventArgs e)
        {
        }

        protected string IdString => "";//$"{_sourceId}:{_apiKey}";
    }

    public class LogConsumer : LogBase
    {
        private IConsumer<Ignore, string> _consumer;
        private CancellationTokenSource _cts;
        protected override void OnOpen()
        {
            base.OnOpen();
            if (!IsValidConnecton) return;

            var c = KafkaManager.CreateConsumer<Ignore, string>();
            c.Subscribe(_sourceId);
            var cts = _cts = new CancellationTokenSource();
            //Task.Run(() => { 
                try
                {
                    while (!cts.IsCancellationRequested)
                    {
                        try
                        {
                            var cr = c.Consume(cts.Token);
                            var v = cr.Message.Value;
                            Send(v);
                            Console.WriteLine($"Consumed message '{v}' at: '{cr.TopicPartitionOffset}'.");
                        }
                        catch (ConsumeException e)
                        {
                            Console.WriteLine($"Error occured: {e.Error.Reason}");
                        }
                    }
                }
                catch (OperationCanceledException)
                {
                }
                finally
                {
                    c.Close();
                }
            //});
        }

        protected override void OnMessage(MessageEventArgs e)
        {
            base.OnMessage(e);
            PrintDebugMessage($"Log Consumer {IdString} OnMessage");
        }

        protected override void OnClose(CloseEventArgs e)
        {
            _cts?.Cancel();
            _cts = null;
            base.OnClose(e);
            PrintDebugMessage($"LogConsumer {IdString} OnClose");
        }
    }

    public class LogProducer : LogBase
    {
        private IProducer<Null, string> _producer;
        protected override void OnOpen()
        {
            base.OnOpen();
            if (!IsValidConnecton) return;

            _producer = KafkaManager.CreateProducer<Null, string>();
            Send("LogProducer");
        }

        protected override void OnMessage(MessageEventArgs e)
        {
            var message = e.Data;

            _producer.Produce(_sourceId, new Message<Null, string> { Value = message });
            base.OnMessage(e);
            //PrintDebugMessage($"LogProducer {IdString} OnMessage: {message}");
        }

        protected override void OnClose(CloseEventArgs e)
        {
            _producer?.Dispose();
            _producer = null;
            base.OnClose(e);
           // PrintDebugMessage($"LogProducer {IdString} OnClose");
        }

    }

    public class Echo : BehaviorBase<Echo>
    {
        protected override void OnOpen()
        {
            base.OnOpen();
            PrintDebugMessage("echo OnOpen");
        }

        protected override void OnMessage(MessageEventArgs e)
        {
            Send(e.Data);
            base.OnMessage(e);
            PrintDebugMessage("echo OnMessage");
        }

        protected override void OnClose(CloseEventArgs e)
        {
            base.OnClose(e);
            PrintDebugMessage("echo OnClose");
        }
    }

    public class BehaviorBase<T> : WebSocketBehavior {

        private static int _openConnectionCount = 0;
        private static int _receivedMessageCount = 0;
        private static int _closeConnectionCount = 0;
        private static int _errorCount = 0;
        private static string _lastError;

        protected override void OnOpen()
        {
            Interlocked.Increment(ref _openConnectionCount);
            base.OnOpen();
        }

        protected override void OnMessage(MessageEventArgs e)
        {
            Interlocked.Increment(ref _receivedMessageCount);
            base.OnMessage(e);
        }

        protected override void OnError(ErrorEventArgs e)
        {
            Interlocked.Increment(ref _errorCount);
            _lastError = e.Message + ", " + e.Exception?.ToString();
            base.OnError(e);
        }

        protected override void OnClose(CloseEventArgs e)
        {
            Interlocked.Increment(ref _closeConnectionCount);
            base.OnClose(e);
        }

        protected virtual string GetDebugMessage()
        {
            return $"OpenedConnectionCount:{_openConnectionCount}, ReceivedMessageCount:{_receivedMessageCount}, ClosedConnectionCount: {_closeConnectionCount}, ErrorCount: {_errorCount}, {_lastError}";
        }

        protected virtual void PrintDebugMessage(string prefix)
        {
            Console.WriteLine($"{prefix}");
            //Console.WriteLine($"{prefix}. Sessions.Count: {Sessions.Count}. {GetDebugMessage()}");
        }
    }
}
