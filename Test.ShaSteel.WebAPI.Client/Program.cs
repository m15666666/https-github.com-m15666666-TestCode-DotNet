using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.ShaSteel.WebAPI.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient();
            var client = new Client("http://localhost:60611/", httpClient);

            var ret = await client.VibMetaDataAsync(new Core.VibMetaDataInput { 
                Code = "abcd",
                FullPath = "a-b-c-d"
            });

            httpClient.Dispose();
        }
    }

}
