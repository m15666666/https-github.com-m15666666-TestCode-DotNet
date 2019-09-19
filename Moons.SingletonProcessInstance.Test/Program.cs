using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;

namespace Moons.SingletonProcessInstance.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var assembly = Assembly.GetExecutingAssembly();
            Console.WriteLine(assembly.Location);
            Console.WriteLine(assembly.CodeBase);
            FileStream fs = null;
            string name = typeof(Program).FullName;
            string path = Path.Combine(Path.GetDirectoryName(assembly.Location), name);
            Console.WriteLine(path);
            try
            {
                fs = File.OpenWrite(path);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ex: {ex}");
                return;
            }

            using (fs)
            {

                //Console.WriteLine(System.IO.Directory.GetCurrentDirectory());
                Console.WriteLine($"typeof(Program).FullName: {name}");
                foreach (var arg in args)
                {
                    Console.WriteLine(arg);
                }

                bool existed;
                //EventWaitHandle eventWaitHandle = new EventWaitHandle(false, EventResetMode.ManualReset, name, out existed);
                //Mutex mutex = new Mutex(false, name, out existed);
                //Console.WriteLine($"{name} existed: {!existed}");

                Console.WriteLine("Hello World Singleton process instance.");
                Console.ReadLine();
                //mutex.Dispose();
                //eventWaitHandle.Dispose();
            }
        }
    }
}
