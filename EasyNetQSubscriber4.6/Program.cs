using EasyNetQ;
using EasyNetQMessages;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
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

            // 参考：https://www.cnblogs.com/MuNet/p/8546192.html
            //1.1.实例化连接工厂
            var factory = new ConnectionFactory() { HostName = "localhost", UserName = "admin", Password = "admin" };
            //2. 建立连接
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())//3. 创建信道
            using (var bus = RabbitHutch.CreateBus("host=localhost;virtualHost=/;username=admin;password=admin"))
            {
                #region rabbitmq 自己的客户端库

                //4. 申明队列
                var queueNameOfRabbitRaw = "rabbit-raw-queue";
                channel.QueueDeclare(queue: queueNameOfRabbitRaw, durable: false, exclusive: false, autoDelete: false, arguments: null);

                //5. 构造消费者实例
                var consumer = new EventingBasicConsumer(channel);

                //6. 绑定消息接收后的事件委托
                consumer.Received += (model, ea) =>
                {
                    Console.WriteLine($"{queueNameOfRabbitRaw} message: {string.Join(",", ea.Body)}");
                    var message = Encoding.UTF8.GetString(ea.Body);
                    Console.WriteLine($"{queueNameOfRabbitRaw} message: {message}");
                };

                //7. 启动消费者
                channel.BasicConsume(queue: queueNameOfRabbitRaw, autoAck: true, consumer: consumer);

                #endregion

                #region 测试Send/Receive，EasyNetQ库

                //bus.Receive<string>("my.queue.string", message => Console.WriteLine("my.queue.string message: {0}", message));
                //bus.Receive<byte[]>("my.queue.bytes", message => Console.WriteLine("my.queue.bytes message: {0}", string.Join(",", message)));

                TestSubscribe_Publish(bus);
                #endregion

                //TestSubscribe_Publish(bus);

                Console.ReadLine();
            }
        }

        static void TestSubscribe_Publish(IBus bus)
        {
            #region 测试Subscribe/Publish

            bus.Subscribe<TextMessage>("test4.6", m => HandleTextMessage(m, ": test4.6"));
            bus.Subscribe<TextMessage>("test4.6", m => HandleTextMessage(m, ": test4.6"));
            bus.Subscribe<TextMessage>("test4.6.1", m => HandleTextMessage(m, ": test4.6.1"));

            #endregion
        }

        static void HandleTextMessage(TextMessage textMessage, string suffix)
        {
            //Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Got message: {0}", textMessage.Text + suffix);
            //Console.ResetColor();
        }
    }
}
