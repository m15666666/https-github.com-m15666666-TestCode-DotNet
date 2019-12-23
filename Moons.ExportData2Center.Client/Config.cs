using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Moons.ExportData2Center.Client
{
    public class Config
    {
        public static readonly Config Instance = new Config();

        public HttpClient HttpClient { get; set; } = new HttpClient();

        public Client Client { get; set; }

        public void Init( string url )
        {
            Client = new Client(url, HttpClient);
        }
    }
}
