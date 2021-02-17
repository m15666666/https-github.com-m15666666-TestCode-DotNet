using Microsoft.AspNetCore.SignalR.Client;
using Moons.Common20.TestTools;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace TestSignalR.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            HubConnection connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5000/messagehub")
                .Build();

            Stopwatch sw = new Stopwatch();
            int count = 0;

            connection.On<string, string>("ReceiveMessage", (user, message) =>
            {
                sw.Stop();
                count++;
                var newMessage = $"{user}: {message},{count}, {sw.ElapsedMilliseconds} ms";

                Console.WriteLine(newMessage);
            });

            await connection.StartAsync();

            while (!string.Equals("q", ConsoleUtils.EnterQ2Exit()))
            {
                sw.Restart();
                await connection.InvokeAsync("SendMessage", "jack", "hello,world");
            }

            ConsoleUtils.Enter2Continue();
        }
    }
}
