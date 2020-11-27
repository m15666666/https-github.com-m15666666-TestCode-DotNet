using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moons.Common20;
using Test.ShaSteel.WebAPI.Core;

namespace Test.ShaSteel.WebAPI.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class VDiagnosisController : ControllerBase
    {
        // GET: api/VDiagnosis
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }


        [HttpPost]
        public string A1()
        {
            return "value";
        }

        // POST: api/VDiagnosis/VibMetaData
        [HttpPost]
        public VibMetaDataOutputDto VibMetaData([FromBody] VibMetaDataInput value)
        {
            var content = Newtonsoft.Json.JsonConvert.SerializeObject(value);
            TraceUtils.Info($"VibMetaData value:{content}");

            return new VibMetaDataOutputDto {
                Status = 200,
                Data = new VibMetaDataOutput
                {
                    WaveTag = "9e3e009a - f138 - dcd6 - 6323 - c768dc533b2f",
                    CurrLength = 0,
                }
            };
        }


        // POST: api/VDiagnosis/VibWaveData
        [HttpPost]
        public VibMetaDataOutputDto VibWaveData([FromQuery] /*VibWaveDataInput VibMetaDataOutput*/string wavetag, 
            [FromQuery] string length, [FromQuery] string currIndex, [FromQuery] string blockSize, 
            [FromBody] byte[] value)
        {
            TraceUtils.Info($"VibWaveData ({wavetag},{length},{currIndex},{blockSize},{string.Join('-', value)})");

            return new VibMetaDataOutputDto
            {
                Status = 200,
                Data = new VibMetaDataOutput
                {
                    WaveTag = wavetag,
                    CurrLength = value.Length,
                }
            };
        }
        [HttpPost]
        public ProcessDatasOutputDto ProcessDatas([FromBody] ProcessDatasInput value)
        {
            var content = Newtonsoft.Json.JsonConvert.SerializeObject(value);
            TraceUtils.Info($"ProcessDatas value:{content}");

            return new ProcessDatasOutputDto
            {
                Status = 200,
                Data = new ProcessDatasOutput
                {
                    Code = 1,
                    Success = true
                }
            };
        }
        [HttpPost]
        public string AddOtherAlarm([FromBody] VibAlarmInput value)
        {
            var content = Newtonsoft.Json.JsonConvert.SerializeObject(value);
            TraceUtils.Info($"AddOtherAlarm value:{content}");

            return "1";
        }
    }
}
