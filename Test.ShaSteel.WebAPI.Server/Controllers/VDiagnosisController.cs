using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Test.ShaSteel.WebAPI.Core;

namespace Test.ShaSteel.WebAPI.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VDiagnosisController : ControllerBase
    {
        // GET: api/VDiagnosis
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/VDiagnosis/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/VDiagnosis/VibMetaData
        [HttpPost]
        public VibMetaDataOutput VibMetaData([FromBody] VibMetaDataInput value)
        {
            return new VibMetaDataOutput { 
                WaveTag = "9e3e009a - f138 - dcd6 - 6323 - c768dc533b2f",
                CurrLength = "0",
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
                CurrLength = "0"
            };

            //            return @"{
            //	'WaveTag': '9e3e009a - f138 - dcd6 - 6323 - c768dc533b2f',

            //    'CurrLength': 0

            //    }
            //";
        }

        // POST: api/VDiagnosis/VibWaveData
        [HttpPost]
        public VibMetaDataOutput VibWaveData([FromBody] short[] value)
        {
            return new VibMetaDataOutput
            {
                WaveTag = "9e3e009a - f138 - dcd6 - 6323 - c768dc533b2f",
                CurrLength = "0"
            };

            //            return @"{
            //	'WaveTag': '9e3e009a - f138 - dcd6 - 6323 - c768dc533b2f',

            //    'CurrLength': 0

            //    }
            //";
        }
        [HttpPost]
        public VibMetaDataOutput VibWaveData2([FromBody] string value)
        {
            return new VibMetaDataOutput
            {
                WaveTag = "9e3e009a - f138 - dcd6 - 6323 - c768dc533b2f",
                CurrLength = "0"
            };

            //            return @"{
            //	'WaveTag': '9e3e009a - f138 - dcd6 - 6323 - c768dc533b2f',

            //    'CurrLength': 0

            //    }
            //";
        }
    }
}
