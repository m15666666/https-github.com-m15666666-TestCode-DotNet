using System;

namespace MQTTBroker
{
    class Program
    {
        static void Main(string[] args)
        {
            var a = new RxMqtt.Broker.MqttBroker();
            a.StartListening();
            Console.WriteLine("Hello World!");
        }
    }
}
