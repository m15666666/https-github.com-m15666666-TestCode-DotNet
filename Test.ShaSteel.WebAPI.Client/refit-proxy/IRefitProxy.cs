using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Refit;
using Test.ShaSteel.WebAPI.Core;

namespace Test.ShaSteel.WebAPI.Client.refit_proxy
{
    /// <summary>
    /// https://github.com/reactiveui/refit
    /// </summary>
    public interface IRefitProxy
    {
        [Post("/VibMetaData")]
        Task<VibMetaDataOutputDto> VibMetaDataAsync([Body] VibMetaDataInput value);

        [Post("/VibWaveData?WaveTag={p.WaveTag}&Length={p.Length}&CurrIndex={p.CurrIndex}&BlockSize={p.BlockSize}")]
        Task<VibMetaDataOutputDto> VibWaveDataAsync(VibWaveDataInput p, [Body] byte[] input);

        [Post("/VibMetaData")]
        Task<ProcessDatasOutputDto> ProcessDatasAsync([Body] ProcessDatasInput value);

        [Post("/AddOtherAlarm")]
        Task<string> AddOtherAlarmAsync([Body] VibAlarmInput value);
    }
}
