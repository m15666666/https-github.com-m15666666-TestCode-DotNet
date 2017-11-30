using CoAP.Server.Resources;
using System;
using System.Collections.Generic;
using System.Text;

namespace COAPSubscriber
{
    public class HelloWorldResource : Resource
    {
        // 设置当前资源的路径为 "helloworld"
        public HelloWorldResource() : base("helloworld")
        {
            // 设置资源的标题
            Attributes.Title = "GET a friendly greeting!";
        }

        // 重写 DoGet 方法来处理 GET 请求
        protected override void DoGet(CoapExchange exchange)
        {
            // 收到一次请求，回复 "Hello World!"
            exchange.Respond("HelloWorldResource.DoGet: Hello World!");
        }
    }
}
