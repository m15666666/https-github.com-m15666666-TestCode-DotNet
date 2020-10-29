using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Moons.Common20.Test
{
    /// <summary>
    /// 测试使用一个线程异步消费队列
    /// </summary>
    public class TestAsyncQueueConsumer
    {
        public BlockingCollection<int> Queue { get;private set; } = new BlockingCollection<int>();
        public void Test_Task_Factory()
        {
            Task.Factory.StartNew(async () => {

                while (!Queue.IsCompleted)
                {
                    Console.WriteLine($"Test_Task_Factory thread: {Thread.CurrentThread.ManagedThreadId}");

                    var v = Queue.Take();
                    Console.WriteLine($"v:{v} thread: {Thread.CurrentThread.ManagedThreadId}");

                    await Task.Delay(10);
                    Console.WriteLine($"delay thread: {Thread.CurrentThread.ManagedThreadId}");
                    await Task.CompletedTask;
                    Console.WriteLine($"complete thread: {Thread.CurrentThread.ManagedThreadId}");
                }
                Console.WriteLine($"finish thread: {Thread.CurrentThread.ManagedThreadId}");

            }, TaskCreationOptions.LongRunning);
        }
        public void Test_Tread()
        {
            Thread t = null;// new Thread(start);
            t = new Thread(start);
            async void start()
            {
                while (!Queue.IsCompleted)
                {
                    Console.WriteLine($"t thread: {t.ManagedThreadId}");
                    Console.WriteLine($"Test_Tread thread: {Thread.CurrentThread.ManagedThreadId}");

                    var v = Queue.Take();
                    Console.WriteLine($"v:{v} thread: {Thread.CurrentThread.ManagedThreadId}");

                    await Task.Delay(10);
                    Console.WriteLine($"delay thread: {Thread.CurrentThread.ManagedThreadId}");
                    await Task.CompletedTask;
                    Console.WriteLine($"complete thread: {Thread.CurrentThread.ManagedThreadId}");
                }
                Console.WriteLine($"finish thread: {Thread.CurrentThread.ManagedThreadId}");
            }
            t.Name = "Test_Tread";
            t.Start();
            Console.WriteLine($"t thread: {t.ManagedThreadId}");
        }

        public static void TestAsyncConsumerQueue()
        {
            Console.WriteLine($"TestAsyncConsumerQueue begin thread: {Thread.CurrentThread.ManagedThreadId}");
            var t = new TestAsyncQueueConsumer();
            t.Test_Task_Factory();
            Console.WriteLine($"TestAsyncConsumerQueue end thread: {Thread.CurrentThread.ManagedThreadId}");
        }
    }
}
