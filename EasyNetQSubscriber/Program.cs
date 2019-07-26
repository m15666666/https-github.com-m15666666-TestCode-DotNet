using EasyNetQ;
using EasyNetQMessages;
using System;

namespace EasyNetQSubscriber
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("EasyNetQSubscriber Listening for messages. Hit <return> to quit.");
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Red;

            using (var bus = RabbitHutch.CreateBus(TextMessage.ConnectionString))
            {
                #region 测试Send/Receive

                bus.Receive<TextMessage>("my.queue", message => Console.WriteLine("my.queue message: {0}", message.Text));
                bus.Receive<string>("my.queue.string", message => Console.WriteLine("my.queue.string message: {0}", message));
                bus.Receive<byte[]>("my.queue.bytes", message => Console.WriteLine("my.queue.bytes message: {0}", string.Join(",", message)));
                bus.Send("my.queue", new TextMessage { Text = "Hello Widgets!" });

                #endregion

                TestSubscribe_Publish(bus);

                TestRequest_Respond( bus );

                
                Console.ReadLine();
            }
        }

        static void TestRequest_Respond(IBus bus)
        {
            #region 测试Request/Respond

            var request = new TextMessage { Text = "Hello Server" };
            var response = bus.Request<TextMessage, TextMessage>(request);
            Console.WriteLine($"Request: {request.Text}, Response: {response.Text}");

            #endregion
        }

        static void TestSubscribe_Publish(IBus bus)
        {
            #region 测试Subscribe/Publish

            bus.Subscribe<TextMessage>("test", m => HandleTextMessage(m));

            #endregion
        }

        static void HandleTextMessage(TextMessage textMessage)
        {
            //Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Got message: {0}", textMessage.Text);
            //Console.ResetColor();
        }
    }
}
