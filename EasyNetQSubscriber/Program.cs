using EasyNetQ;
using EasyNetQMessages;
using System;

namespace EasyNetQSubscriber
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var bus = RabbitHutch.CreateBus(TextMessage.ConnectionString))
            {
                #region 测试Send/Receive

                bus.Receive<TextMessage>("my.queue", message => Console.WriteLine("MyMessage: {0}", message.Text));
                bus.Send("my.queue", new TextMessage { Text = "Hello Widgets!" });

                #endregion

                TestSubscribe_Publish(bus);

                TestRequest_Respond( bus );

                Console.WriteLine("Listening for messages. Hit <return> to quit.");
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
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Got message: {0}", textMessage.Text);
            Console.ResetColor();
        }
    }
}
