using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.ShaSteel.WebAPI.Core;

namespace Test.ShaSteel.WebAPI.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient();
            var client = new Client("http://localhost:60611/", httpClient);

            while (true)
            {
                Console.WriteLine("Enter q to quit.");
                var line = Console.ReadLine();
                if ("q".Equals(line, StringComparison.InvariantCultureIgnoreCase)) break;

                try
                {
                    var ret = await client.VibMetaDataAsync(new VibMetaDataInput
                    {
                        Code = "abcd",
                        FullPath = "a-b-c-d"
                    });

                    Console.WriteLine(ret.ToString());
                    //WaveTag=9e3e009a-f138-dcd6-6323-c768dc533b2f&Length=131072&CurrIndex=0&BlockSize=131072
                    VibWaveDataInput waveDataInput = new VibWaveDataInput { WaveTag = "9e3e009a - f138 - dcd6 - 6323 - c768dc533b2f",
                        Length = 131072,
                        CurrIndex = 0 ,
                        BlockSize = 131072 };
                    ret = await client.VibWaveDataAsync(waveDataInput, new byte[] { 1, 0, 1, 0 });
                    Console.WriteLine(ret.ToString());
                }
                catch( Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }

            httpClient.Dispose();
        }
    }

}
