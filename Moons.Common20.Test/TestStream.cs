using Moons.Common20.Serialization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Moons.Common20.Test
{
    public class TestStream
    {
        /// <summary>
        /// 测试不同实现的性能
        /// </summary>
        private static void TestBinaryRead_Performance()
        {
            Stopwatch sw = new Stopwatch();
            const int loopcount = 1000000;
            const int itemCount = 1024;
            byte[] loopbuffer = new byte[itemCount * 2];


            using (MemoryStream stream = new MemoryStream(loopbuffer))
            using (var r = new BinaryRead(stream))// 性能差一些9574ms，差距不大
            {
                sw.Restart();
                for (int i = 0; i < loopcount; i++)
                {
                    r.BaseStream.Position = 0;
                    r.ReadInt16Array(itemCount);
                }
                sw.Stop();
            }
            Console.WriteLine($"{sw.ElapsedMilliseconds}");

            using (var r = new ByteArrayReader(loopbuffer)) // 性能好一些4334ms
            {
                sw.Restart();
                for (int i = 0; i < loopcount; i++)
                {
                    r.BaseStream.Position = 0;
                    r.ReadInt16Array(itemCount);
                }
                sw.Stop();
            }
            Console.WriteLine($"{sw.ElapsedMilliseconds}");

            using (MemoryStream stream = new MemoryStream(loopbuffer))
            using (var r = new BinaryRead(stream))// 性能差一些8993ms，差距不大
            {
                sw.Restart();
                for (int i = 0; i < loopcount; i++)
                {
                    r.BaseStream.Position = 0;
                    r.ReadInt16Array(itemCount);
                }
                sw.Stop();
            }
            Console.WriteLine($"{sw.ElapsedMilliseconds}");
        }
        public static void TestBinaryRead()
        {
            TestBinaryRead_Performance();
            TestBinaryRead_Correct();
        }
        /// <summary>
        /// 测试不同实现的准确性
        /// </summary>
        private static void TestBinaryRead_Correct()
        {
            byte[] buffer = new byte[8] {1,2,3,4,5,6,7,8};
            {
                var read = new ByteArrayReader(buffer);
                var ret = read.ReadInt16Array(4);
            }
            {
                int startIndex = 1;
                int count = 3;
                var read = new ByteArrayReader(buffer,startIndex,buffer.Length - startIndex);
                var ret = read.ReadInt16Array(count);
            }
            {
                using (MemoryStream stream = new MemoryStream(buffer))
                {
                    var read = new BinaryRead(stream);
                    var ret = read.ReadInt16Array(4);
                }
            }
            {
                int startIndex = 1;
                int count = 3;
                using (MemoryStream stream = new MemoryStream(buffer, startIndex, buffer.Length - startIndex))
                {
                    var read = new BinaryRead(stream);
                    var ret = read.ReadInt16Array(count);
                }
            }

        }
    }
}
