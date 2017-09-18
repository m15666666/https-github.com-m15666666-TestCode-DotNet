using EasyNetQ;
using EasyNetQMessages;
using System;
using System.Threading.Tasks;

namespace EasyNetQPublisher
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var bus = RabbitHutch.CreateBus(TextMessage.ConnectionString))
            {
                #region 测试Request/Respond

                bus.RespondAsync<TextMessage, TextMessage>(
                    request =>
                        Task.Factory.StartNew(
                            () => new TextMessage { Text = "Response from: " + request.Text }
                        )
                );

                //bus.Respond<TextMessage, TextMessage>(request => new TextMessage { Text = "Response from: " + request.Text } );

                #endregion

                var input = "";
                Console.WriteLine("Enter a message. 'Quit' to quit.");
                while ((input = Console.ReadLine()) != "Quit")
                {
                    #region 测试Subscribe/Publish

                    bus.Publish(new TextMessage
                    {
                        Text = input
                    });

                    #endregion
                }
            }
        }
    }
}
