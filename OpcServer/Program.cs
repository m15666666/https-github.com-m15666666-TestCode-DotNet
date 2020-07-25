using Opc.Ua;
using Opc.Ua.Configuration;
using System;

namespace OpcServer
{
    class Program
    {
        static void Main(string[] args)
        {
            ApplicationInstance application = new ApplicationInstance();
            application.ApplicationName = "UA Sample Server";
            application.ApplicationType   = ApplicationType.Server;
            application.ConfigSectionName = "Opc.Ua.SampleServer";

                        try
            {
                application.LoadApplicationConfiguration(false).Wait();

                // check the application certificate.
                bool certOK = application.CheckApplicationInstanceCertificate(false, 0).Result;
                if (!certOK)
                {
                    throw new Exception("Application instance certificate invalid!");
                }

                // start the server.
                application.Start(new SampleServer()).Wait();

            }
            catch (Exception e)
            {
            }


            Console.WriteLine("Hello World!");
        }
    }
}
