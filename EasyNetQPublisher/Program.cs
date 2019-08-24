using EasyNetQ;
using EasyNetQMessages;
using RabbitMQ.Client;
using System;
using System.Threading.Tasks;

namespace EasyNetQPublisher
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("EasyNetQPublisher Enter a message. 'Quit' to quit.");
            Console.WriteLine("");

            // 参考：https://www.cnblogs.com/MuNet/p/8546192.html
            //1.1.实例化连接工厂
            var factory = new ConnectionFactory() { HostName = "localhost", UserName="admin", Password="admin" };
            //2. 建立连接
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())//3. 创建信道
            using (var bus = RabbitHutch.CreateBus(TextMessage.ConnectionString))
            {
                //4. 申明队列
                var queueNameOfRabbitRaw = "rabbit-raw-queue";
                channel.QueueDeclare(queue: queueNameOfRabbitRaw, durable: false, exclusive: false, autoDelete: false, arguments: null);

                #region 测试Request/Respond

                bus.RespondAsync<TextMessage, TextMessage>(
                    request =>
                        Task.Factory.StartNew(
                            () => new TextMessage { Text = "Response from: " + request.Text }
                        )
                );

                bus.Respond<TextMessage, TextMessage>(request => new TextMessage { Text = "Response from: " + request.Text });

                #endregion

                var input = "";
                int count = 1;
                while ((input = Console.ReadLine()) != "Quit")
                {
                    Console.WriteLine($"Begin {DateTime.Now}.");
                    for (int i = 0; i < count; i++)
                    {
                        #region 测试Subscribe/Publish，EasyNetQ库

                        bus.Publish(new TextMessage
                        {
                            Text = input
                        });

                        string msg = input;

                        bus.Send("my.queue", new TextMessage { Text = msg });
                        bus.Send("my.queue.string", msg);
                        bus.Send("my.queue.bytes", System.Text.Encoding.UTF8.GetBytes(msg));

                        #endregion

                        #region rabbitmq 自己的客户端库

                        //5. 构建byte消息数据包
                        var body = System.Text.Encoding.UTF8.GetBytes(msg);

                        //6. 发送数据包
                        channel.BasicPublish(exchange: "", routingKey: queueNameOfRabbitRaw, basicProperties: null, body: body);

                        #endregion
                    }
                    Console.WriteLine($"End {DateTime.Now}.");
                }
            }
        }
    }
}
