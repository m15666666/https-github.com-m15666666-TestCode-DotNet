using Moons.Common20.WebApi;
using Test.ShaSteel.WebAPI.Core;

namespace Test.ShaSteel.WebAPI.Client
{
    /// <summary>
    /// 重构的代理类
    /// </summary>
    public class Proxy2 : WebApiClientBase, IRondsProxy
    {
        public Proxy2(string baseUrl, System.Net.Http.HttpClient httpClient) : base(baseUrl, httpClient)
        {
        }

        /// <returns>Success</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public System.Threading.Tasks.Task<VibMetaDataOutputDto> VibMetaDataAsync(VibMetaDataInput input)
        {
            return PostAsync<VibMetaDataInput, VibMetaDataOutputDto>("/VibMetaData", input, System.Threading.CancellationToken.None);
        }

        public System.Threading.Tasks.Task<VibMetaDataOutputDto> VibWaveDataAsync(VibWaveDataInput p, byte[] input)
        {
            return PostAsync<byte[], VibMetaDataOutputDto>($"/VibWaveData?WaveTag={p.WaveTag}&Length={p.Length}&CurrIndex={p.CurrIndex}&BlockSize={p.BlockSize}", input, System.Threading.CancellationToken.None);
        }

        /// <returns>Success</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public System.Threading.Tasks.Task<ProcessDatasOutputDto> ProcessDatasAsync(ProcessDatasInput input)
        {
            return PostAsync<ProcessDatasInput, ProcessDatasOutputDto>($"/ProcessDatas", input, System.Threading.CancellationToken.None);
        }

        /// <returns>Success</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public System.Threading.Tasks.Task<string> AddOtherAlarmAsync(VibAlarmInput input)
        {
            return PostAsync<VibAlarmInput, string>($"/AddOtherAlarm", input, System.Threading.CancellationToken.None);
        }
    }
}