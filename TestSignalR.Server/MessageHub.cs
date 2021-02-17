using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestSignalR.Server
{
    /// <summary>
    /// 这是一个类似echo的SignalR hub
    /// </summary>
    public class MessageHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            Console.WriteLine($"receive: {user}, {message}");
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
