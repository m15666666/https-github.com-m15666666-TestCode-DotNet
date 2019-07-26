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

            //1.1.实例化连接工厂
            var factory = new ConnectionFactory() { HostName = "localhost", UserName="admin", Password="admin" };
            //2. 建立连接
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())//3. 创建信道
            using (var bus = RabbitHutch.CreateBus(TextMessage.ConnectionString))
            {
                //4. 申明队列
                var queueName = "rabbit-raw-queue";
                channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

                #region 测试Request/Respond

                //bus.RespondAsync<TextMessage, TextMessage>(
                //    request =>
                //        Task.Factory.StartNew(
                //            () => new TextMessage { Text = "Response from: " + request.Text }
                //        )
                //);

                //bus.Respond<TextMessage, TextMessage>(request => new TextMessage { Text = "Response from: " + request.Text } );

                #endregion

                var input = "";
                
                while ((input = Console.ReadLine()) != "Quit")
                {
                    #region 测试Subscribe/Publish

                    //bus.Publish(new TextMessage
                    //{
                    //    Text = input
                    //});

                    string msg = input;
                    
                    //bus.Send("my.queue", new TextMessage { Text = msg });
                    //bus.Send("my.queue.string", msg );
                    //bus.Send("my.queue.bytes", System.Text.Encoding.UTF8.GetBytes(msg));

                    channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: System.Text.Encoding.UTF8.GetBytes(msg));

                    #endregion
                }
            }
        }
    }
}
