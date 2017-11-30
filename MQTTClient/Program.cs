using OpenNETCF.MQTT;
using System;
using System.Diagnostics;
using System.Threading;

namespace MQTTSubscriber
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            ClientReceiveTest();

            Console.WriteLine("Press key to exit.");
            Console.ReadLine();
        }

        public static void ClientReceiveTest()
        {
            var receivedMessage = false;

            // create the client
            var client = new MQTTClient ("test.mosquitto.org", 1883);

            // hook up the MessageReceived event with a handler
            client.MessageReceived += (topic, qos, payload) =>
            {
                Debug.WriteLine("RX: " + topic);
                Console.WriteLine("RX: " + topic);
                receivedMessage = true;
            };

            var i = 0;
            //var topicName = "OpenNETCF/#";
            var topicName = "OpenNETCF";

            // connect to the MQTT server
            //client.Connect("OpenNETCF");
            client.Connect("OpenNETCF");
            // wait for the connection to complete
            while (!client.IsConnected)
            {
                Thread.Sleep(1000);

                if (i++ > 10) break;
            }

            
            // add a subscription
            client.Subscriptions.Add(new Subscription(topicName + "/#"));

            i = 0;
            while (true)
            {
                if (receivedMessage) break;

                //Thread.Sleep(1000);

                // publish on our own subscribed topic to see if we hear what we send
                client.Publish("OpenNETCF/Test", "Hello", QoS.FireAndForget, false);
                //client.Publish(topicName, "Hello", QoS.FireAndForget, false);

                if (i++ > 10) break;
            }

        }

    }
}
