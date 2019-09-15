using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using MQTTnet.Server;
using OpenNETCF.MQTT;
using System;
using System.Diagnostics;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MQTTSubscriber
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            ClientReceiveTest_MQTTnet();
            //ClientReceiveTest_OpenNETCF();

            Console.WriteLine("Press key to exit.");
            Console.ReadLine();
        }

        private static async Task ClientReceiveTest_MQTTnet()
        {
            var receivedMessage = false;

            //var options = new MqttClientOptions
            //{
            //    ClientId = new Guid().ToString(),
            //    CleanSession = true,
            //    ChannelOptions = new MqttClientTcpOptions
            //    {
            //        //Server = "localhost",
            //        Server = "127.0.1.1"
            //    },
            //    //ChannelOptions = new MqttClientWebSocketOptions
            //    //{
            //    //    Uri = "ws://localhost:59690/mqtt"
            //    //}
            //};
            var options = new MqttClientOptionsBuilder()
                 .WithTcpServer("127.0.0.1", 1883)
                 .WithClientId("client123")
                 .WithTls(new MqttClientOptionsBuilderTlsParameters
                 {
                     UseTls = true,
                     AllowUntrustedCertificates = true,
                     SslProtocol = System.Security.Authentication.SslProtocols.Tls11,
                     CertificateValidationCallback = (X509Certificate x, X509Chain y, SslPolicyErrors z, IMqttClientOptions o) =>
                     {
                         // TODO: Check conditions of certificate by using above parameters.
                         return true;
                     }
                 })
                 //.WithTls()
                 .Build();


            var factory = new MqttFactory();
            var mqttClient = factory.CreateMqttClient();

            mqttClient.UseApplicationMessageReceivedHandler( e => {
                Console.WriteLine("### RECEIVED APPLICATION MESSAGE ###");
                Console.WriteLine($"+ Topic = {e.ApplicationMessage.Topic}");
                Console.WriteLine($"+ Payload = {Encoding.UTF8.GetString(e.ApplicationMessage.Payload)}");
                Console.WriteLine($"+ QoS = {e.ApplicationMessage.QualityOfServiceLevel}");
                Console.WriteLine($"+ Retain = {e.ApplicationMessage.Retain}");
                Console.WriteLine();
            });

            mqttClient.UseConnectedHandler(async (e) =>
            {
                Console.WriteLine("### CONNECTED WITH SERVER ###");

                await mqttClient.SubscribeAsync(new TopicFilterBuilder().WithTopic("#").Build());

                Console.WriteLine("### SUBSCRIBED ###");
            });

            mqttClient.UseDisconnectedHandler(async (e) =>
            {
                Console.WriteLine("### DISCONNECTED FROM SERVER ###");
                await Task.Delay(TimeSpan.FromSeconds(5));

                try
                {
                    await mqttClient.ConnectAsync(options);
                }
                catch
                {
                    Console.WriteLine("### RECONNECTING FAILED ###");
                }
            });

            try
            {
                await mqttClient.ConnectAsync(options);

                var message = new MqttApplicationMessageBuilder();
                message.WithTopic("TopicTest");
                message.WithPayload("topictest001");

                message.WithExactlyOnceQoS();
                message.WithRetainFlag();
                await mqttClient.PublishAsync(message.Build());

                Console.WriteLine("### Publish finish ###");
            }
            catch (Exception exception)
            {
                Console.WriteLine("### CONNECTING FAILED ###" + Environment.NewLine + exception);
            }

            Console.WriteLine("### WAITING FOR APPLICATION MESSAGES ###");
        }

        private static void ClientReceiveTest_OpenNETCF()
        {
            var receivedMessage = false;

            // create the client
            //var client = new MQTTClient ("test.mosquitto.org", 1883);
            var client = new MQTTClient ("localhost", 1883); // mosquitto
            //client.CertValidationProc = null;
            //var client = new MQTTClient("localhost", 8883); // mosquitto

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
