using CoAP;
using CoAP.Server;
using System;

namespace COAPSubscriber
{
    class Program
    {
        static void Main(string[] args)
        {
            TestCoapServer();

            TestCoapClient();

            Console.WriteLine("Hello World!");
        }

        private static void TestCoapClient()
        {
            // 创建一个新的客户端实例
            var client = new CoapClient();

            // 设置目标访问资源的Uri地址
            client.Uri = new Uri("coap://localhost/helloworld");

            // 发送GET请求
            var response = client.Get();

            Console.WriteLine(response.PayloadString);  // Hello World!
        }

        private static void TestCoapServer()
        {
            // 创建一个新的服务端实例
            var server = new CoapServer();

            // 添加资源
            server.Add(new HelloWorldResource());

            // 启动服务端
            server.Start();
        }
    }
}
