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
        public VibMetaDataOutput VibMetaData([FromBody] VibMetaDataInput value)
        {
            var content = Newtonsoft.Json.JsonConvert.SerializeObject(value);
            TraceUtils.Info($"VibMetaData value:{content}");

            return new VibMetaDataOutput
            {
                WaveTag = "9e3e009a - f138 - dcd6 - 6323 - c768dc533b2f",
                CurrLength = 0,
            };

            //            return @"{
            //	'WaveTag': '9e3e009a - f138 - dcd6 - 6323 - c768dc533b2f',

            //    'CurrLength': 0

            //    }
            //";
        }

        // POST: api/VDiagnosis/VibMetaData
        [HttpPost]
        public VibMetaDataOutput VibMetaData2([FromBody] string value)
        {
            return new VibMetaDataOutput
            {
                WaveTag = "9e3e009a - f138 - dcd6 - 6323 - c768dc533b2f",
                CurrLength = 0
            };
        }

        // POST: api/VDiagnosis/VibWaveData
        [HttpPost]
        public VibMetaDataOutput VibWaveData([FromQuery] /*VibWaveDataInput VibMetaDataOutput*/string wavetag, 
            [FromQuery] string length, [FromQuery] string currIndex, [FromQuery] string blockSize, 
            [FromBody] byte[] value)
        {
            TraceUtils.Info($"VibWaveData ({wavetag},{length},{currIndex},{blockSize},{string.Join('-', value)})");

            return new VibMetaDataOutput
            {
                WaveTag = string.Join('-',value),
                CurrLength = value.Length
            };
        }
        [HttpPost]
        public object ProcessDatas([FromBody] ProcessDatasInput value)
        {
            if(value.Code == "-1") return -1;

            var content = Newtonsoft.Json.JsonConvert.SerializeObject(value);
            TraceUtils.Info($"ProcessDatas value:{content}");

            return new VibMetaDataOutput
            {
                WaveTag = "9e3e009a - f138 - dcd6 - 6323 - c768dc533b2f",
                CurrLength = 0
            };
        }
    }
}
