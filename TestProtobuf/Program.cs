using System;

namespace TestProtobuf
{
    class Program
    {
        static void Main(string[] args)
        {
            new TestGoogleProtobuf().Test();
            new TestProtobufNet().Test();
            Console.WriteLine("Hello World!");
        }
    }
}
