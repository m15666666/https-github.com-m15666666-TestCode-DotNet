using Moons.Common20;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.TcpClient
{
    class Program
    {
        static void Main(string[] args)
        {
            using (System.Net.Sockets.TcpClient client = new System.Net.Sockets.TcpClient())
            {
                try
                {
                    client.Connect("127.0.0.1", 1283);

                    using (var stream = client.GetStream())
                    {
                        byte[] buffer = null;
                        string fileName = "SendContents.txt";
                        if (File.Exists(fileName))
                        {
                            var content = File.ReadAllText(fileName);
                            content = content.Trim();
                            content = content.Replace("-", string.Empty);
                            buffer = StringUtils.HexString2Bytes(content);
                        }

                        for( int i = 0; i < 10; i++)
                            stream.Write(buffer, 0, buffer.Length);
                    }
                }
                catch( Exception ex)
                {
                    var s = ex.ToString();
                }

                Console.ReadLine();
            }
        }
    }
}
