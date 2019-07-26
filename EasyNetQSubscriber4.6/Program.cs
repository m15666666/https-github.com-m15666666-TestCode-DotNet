using EasyNetQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyNetQSubscriber4._6
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("EasyNetQSubscriber4.6 Listening for messages. Hit <return> to quit." + Environment.NewLine);
            Console.ForegroundColor = ConsoleColor.Green;

            using (var bus = RabbitHutch.CreateBus("host=localhost;virtualHost=/;username=admin;password=admin"))
            {
                #region 测试Send/Receive

                //bus.Receive<string>("my.queue.string", message => Console.WriteLine("my.queue.string message: {0}", message));
                bus.Receive<byte[]>("my.queue.bytes", message => Console.WriteLine("my.queue.bytes message: {0}", string.Join(",", message)));

                #endregion

                //TestSubscribe_Publish(bus);
                
                Console.ReadLine();
            }
        }

        static void TestSubscribe_Publish(IBus bus)
        {
            #region 测试Subscribe/Publish

            bus.Subscribe<object>("test", m => HandleTextMessage(m));

            #endregion
        }

        static void HandleTextMessage(object textMessage)
        {
            //Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Got message: {0}", textMessage.ToString());
            //Console.ResetColor();
        }
    }
}
