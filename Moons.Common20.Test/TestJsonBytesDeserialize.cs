using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Moons.Common20.Test
{
    /// <summary>
    /// 测试相同的json字符串utf8字节数组，不同方式序列化为对象的性能
    /// </summary>
    internal class TestJsonBytesDeserialize
    {
        public static void TestJsonDeserialize()
        {
            TestJsonObject testJson = new TestJsonObject();
            string jsonString = System.Text.Json.JsonSerializer.Serialize(testJson);
            var testBytes = Encoding.UTF8.GetBytes(jsonString);

            int loopcount = 10000000;
            //int loopcount = 2;

            Stopwatch sw = new Stopwatch();
            string step = "";

            //Newtonsoft.Json
            // warm up
            {
                var s = Encoding.UTF8.GetString(testBytes);
                var o = System.Text.Json.JsonSerializer.Deserialize(s, typeof(TestJsonObject));
            }
            {
                var span = testBytes.AsSpan(0, testBytes.Length);
                var o = System.Text.Json.JsonSerializer.Deserialize(span, typeof(TestJsonObject));
            }
            {
                var s = Encoding.UTF8.GetString(testBytes);
                var o = Newtonsoft.Json.JsonConvert.DeserializeObject <TestJsonObject>(s);
            }
            {
                using(MemoryStream stream = new MemoryStream(testBytes, 0, testBytes.Length))
                using (var streamReader = new StreamReader(stream))
                using (var jsonTextReader = new Newtonsoft.Json.JsonTextReader(streamReader))
                {
                    var serializer = Newtonsoft.Json.JsonSerializer.Create();
                    var typedBody = serializer.Deserialize<TestJsonObject>(jsonTextReader);
                }
            }

            {
                step = "System.Text.Json.JsonSerializer.Serialize jsonstring";
                sw.Restart();

                for (int i = 0; i < loopcount; i++)
                {
                    var s = Encoding.UTF8.GetString(testBytes);
                    var o = System.Text.Json.JsonSerializer.Deserialize(s, typeof(TestJsonObject));
                }

                sw.Stop();
                Console.WriteLine($"{step}: {sw.ElapsedMilliseconds} ms");
            }
            {
                step = "System.Text.Json.JsonSerializer.Serialize span";
                sw.Restart();

                for (int i = 0; i < loopcount; i++)
                {
                    var span = testBytes.AsSpan(0, testBytes.Length);
                    var o = System.Text.Json.JsonSerializer.Deserialize(span, typeof(TestJsonObject));
                }

                sw.Stop();
                Console.WriteLine($"{step}: {sw.ElapsedMilliseconds} ms");
            }
            {
                step = "System.Text.Json.JsonSerializer.Serialize span reuse span";
                var span = testBytes.AsSpan(0, testBytes.Length);
                sw.Restart();
                for (int i = 0; i < loopcount; i++)
                {
                    var o = System.Text.Json.JsonSerializer.Deserialize(span, typeof(TestJsonObject));
                }

                sw.Stop();
                Console.WriteLine($"{step}: {sw.ElapsedMilliseconds} ms");
            }
            {
                step = "Newtonsoft.Json.JsonSerializer jsonstring";
                sw.Restart();

                for (int i = 0; i < loopcount; i++)
                {
                    var s = Encoding.UTF8.GetString(testBytes);
                    var o = Newtonsoft.Json.JsonConvert.DeserializeObject <TestJsonObject>(s);
                }

                sw.Stop();
                Console.WriteLine($"{step}: {sw.ElapsedMilliseconds} ms");
            }
            {
                step = "Newtonsoft.Json.JsonSerializer stream";
                sw.Restart();

                for (int i = 0; i < loopcount; i++)
                {
                    using(MemoryStream stream = new MemoryStream(testBytes, 0, testBytes.Length))
                    using (var streamReader = new StreamReader(stream))
                    using (var jsonTextReader = new Newtonsoft.Json.JsonTextReader(streamReader))
                    {
                        var serializer = Newtonsoft.Json.JsonSerializer.Create();
                        var typedBody = serializer.Deserialize<TestJsonObject>(jsonTextReader);
                    }
                }

                sw.Stop();
                Console.WriteLine($"{step}: {sw.ElapsedMilliseconds} ms");
            }
            {
                step = "Newtonsoft.Json.JsonSerializer stream reuse serializer";
                var serializer = Newtonsoft.Json.JsonSerializer.Create();
                sw.Restart();

                for (int i = 0; i < loopcount; i++)
                {
                    using(MemoryStream stream = new MemoryStream(testBytes, 0, testBytes.Length))
                    using (var streamReader = new StreamReader(stream))
                    using (var jsonTextReader = new Newtonsoft.Json.JsonTextReader(streamReader))
                    {
                        var typedBody = serializer.Deserialize<TestJsonObject>(jsonTextReader);
                    }
                }

                sw.Stop();
                Console.WriteLine($"{step}: {sw.ElapsedMilliseconds} ms");
            }
            //{
            //    step = "Newtonsoft.Json.JsonSerializer stream reuse reader";
            //    using(MemoryStream stream = new MemoryStream(testBytes, 0, testBytes.Length))
            //    using (var streamReader = new StreamReader(stream))
            //    using (var jsonTextReader = new Newtonsoft.Json.JsonTextReader(streamReader))
            //    {
            //        sw.Restart();
            //        for (int i = 0; i < loopcount; i++)
            //        {
            //            stream.Position = 0;
            //            var serializer = Newtonsoft.Json.JsonSerializer.Create();
            //            var typedBody = serializer.Deserialize<TestJsonObject>(jsonTextReader);
            //        }
            //    }

            //    sw.Stop();
            //    Console.WriteLine($"{step}: {sw.ElapsedMilliseconds} ms");
            //}
            //{
            //    step = "Newtonsoft.Json.JsonSerializer stream reuse reader and serializer";
            //    var serializer = Newtonsoft.Json.JsonSerializer.Create();
            //    using(MemoryStream stream = new MemoryStream(testBytes, 0, testBytes.Length))
            //    using (var streamReader = new StreamReader(stream))
            //    using (var jsonTextReader = new Newtonsoft.Json.JsonTextReader(streamReader))
            //    {
            //        sw.Restart();
            //        for (int i = 0; i < loopcount; i++)
            //        {
            //            stream.Seek(0, SeekOrigin.Begin);
            //            var typedBody = serializer.Deserialize<TestJsonObject>(jsonTextReader);
            //        }
            //    }

            //    sw.Stop();
            //    Console.WriteLine($"{step}: {sw.ElapsedMilliseconds} ms");
            //}
            Console.WriteLine("enter to continue");
            Console.ReadLine();
        }

        /// <summary>
        /// 用于测试的json对象类
        /// </summary>
        private class TestJsonObject
        {
            public int I1 { get; set; } = 101;
            public string S1 { get; set; } = "abc";
            public DateTime D1 { get; set; } = DateTime.Now;
        }
    }
}