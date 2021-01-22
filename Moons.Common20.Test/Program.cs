using Moons.Plugin.Infra;
using NLua;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;

namespace Moons.Common20.Test
{
    class Program
    {
        #region 实用工具

        /// <summary>
        /// 读文件返回字节数组Base64编码的字符串
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private static string ReadFileToBase64(string path)
        {
            if (!File.Exists(path)) return string.Empty;
            return Convert.ToBase64String(File.ReadAllBytes(path));
        }

        #endregion
        
        static void Main(string[] args)
        {
            TestStream.TestBinaryRead();
            TestAsyncConsumerQueue();
            Console.Read();
            #region 为了调用roslyn时，能从默认的程序域中加载所有需要的程序集，需要先预先调用“需要的程序集”一下

            Console.WriteLine("Test_Roslyn");
            using (Lua lua = new Lua()) { }

            #endregion

            Test_PlugInLoader();

            return; // 以下不做测试
            Test_PluginController();
            Test_Roslyn();
            
            Test_SingletonOfExe(args);
        }

        /// <summary>
        /// 测试如何将同步调用传递的数据放到队列，然后异步消费队列中的数据
        /// </summary>
        static void TestAsyncConsumerQueue()
        {
            Console.WriteLine($"TestAsyncConsumerQueue begin thread: {Thread.CurrentThread.ManagedThreadId}");
            var t = new TestAsyncQueueConsumer();
            //t.Test_Task_Factory();
            t.Test_Tread();
            int i = 1;
            string s;
            while((s = Console.ReadLine()) != "quit")
            {
                Console.WriteLine($"{s} thread: {Thread.CurrentThread.ManagedThreadId}");
                t.Queue.Add(i++);
            }

            Console.WriteLine($"TestAsyncConsumerQueue end thread: {Thread.CurrentThread.ManagedThreadId}");
        }

        /// <summary>
        /// 测试生成dll字节的base64，然后动态加载调用dll
        /// </summary>
        static void Test_PlugInLoader()
        {
            using AssemblyLoader assemblyLoader = new AssemblyLoader();
            var bytes = assemblyLoader.CompileToBytes(new List<string> { File.ReadAllText("LuaScript/PlugInDemo1.cs") }, "test_assembly");
            var base64 = Convert.ToBase64String(bytes);

            using PlugInLoader plugInLoader = new PlugInLoader();
            var plugIn1 = plugInLoader.LoadPlugIn(
                new LoadAssemblyDto {
                    LoadAssemblyMethodType = LoadAssemblyMethodType.DLLBytesBase64,
                    DLLBytesBase64 = base64
                }, true);

            plugIn1.SetValueByKey("hello", "world");
            plugIn1.GetValueByKey("hi");
        }

        /// <summary>
        /// 测试 Moons.Plugin.Infra.PluginController，插件控制器 demo
        /// </summary>
        static void Test_PluginController()
        {
            using (var controller = new PluginController("MyPlugin", "LuaScript"))
            {
                bool keepRunning = true;
                Console.CancelKeyPress += (sender, e) => {
                    e.Cancel = true;
                    keepRunning = false;
                };
                while (keepRunning)
                {
                    try
                    {
                        Console.WriteLine(controller.GetMessage());
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"{ex.GetType()}: {ex.Message}");
                    }
                    Thread.Sleep(1000);
                }
            }
        }

        /// <summary>
        /// 测试reslyn编译引擎
        /// </summary>
        static void Test_Roslyn()
        {
            TestRoslyn.SyntaxTreeDemo3();
        }

        /// <summary>
        /// 通过锁定文件的方式保证只有一个exe的进程在运行
        /// </summary>
        /// <param name="args"></param>
        static void Test_SingletonOfExe(string[] args)
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

                Console.WriteLine("Hello World Singleton instance.");
                Console.ReadLine();
                //mutex.Dispose();
                //eventWaitHandle.Dispose();
            }
        }
    }
}
