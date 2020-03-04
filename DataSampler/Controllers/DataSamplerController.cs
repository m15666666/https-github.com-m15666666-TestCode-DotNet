using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnalysisData.SampleData;
using DataSampler.Core.Dto;
using DataSampler.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moons.Common20;

namespace Test.ShaSteel.WebAPI.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DataSamplerController : ControllerBase
    {
        // GET: api/DataSampler
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

        #region 辅助执行函数

        /// <summary>
        /// 执行函数
        /// </summary>
        /// <param name="cmd">函数</param>
        /// <returns>true：成功，false：不成功</returns>
        private static SampleStationCmdOutputDto<T> ExecSampleStationCmd<T>(Action20 cmd)
        {
            var ret = new SampleStationCmdOutputDto<T>();
            (ret.Succeed, ret.InnerException) = DoHandler(cmd);
            return ret;
        }

        /// <summary>
        /// 执行函数
        /// </summary>
        /// <param name="handler">函数</param>
        /// <returns>true：成功，false：不成功</returns>
        private static (bool, Exception) DoHandler(Action20 handler)
        {
            try
            {
                handler();

                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex);
            }
        }

        /// <summary>
        /// 从字节数组转化为SampleStationProxy对象
        /// </summary>
        /// <param name="cmdInput">SampleStationCmdInputDto</param>
        /// <returns>SampleStationProxy对象</returns>
        private static SampleStationProxy StationConfig2Proxy(SampleStationCmdInputDto cmdInput)
        {
            return StationConfig2Proxy(cmdInput.SampleStationData);
        }

        /// <summary>
        ///     从SampleStationData转化为SampleStationProxy对象
        /// </summary>
        /// <param name="stationConfig">SampleStationData</param>
        /// <returns>SampleStationProxy对象</returns>
        private static SampleStationProxy StationConfig2Proxy(SampleStationData stationConfig)
        {
            return new SampleStationProxy { SampleStationData = stationConfig };
        }

        #endregion

        // POST: api/DataSampler/GetSampleStationStatus
        /// <summary>
        /// 采集工作站获取状态
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public SampleStationCmdOutputDto<SampleStationStatusData> GetSampleStationStatus([FromBody] SampleStationCmdInputDto value)
        {
            SampleStationStatusData stationStatusData = null;
            void Cmd()
            {
                SampleStationProxy sampleStation = StationConfig2Proxy(value);
                stationStatusData = sampleStation.GetSampleStationStatus();
            }

            var ret = ExecSampleStationCmd<SampleStationStatusData>(Cmd);
            ret.Data = stationStatusData;

            return ret;

            //return new SampleStationCmdOutputDto<SampleStationStatusData> {
            //    Data = new SampleStationStatusData
            //    {
            //        StationState = SampleStationState.NormalSample
            //    }
            //};
        }

        // POST: api/DataSampler/StartNormalSample
        /// <summary>
        /// 采集工作站开始正常采集
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public SampleStationCmdOutputDto<bool> StartNormalSample([FromBody] SampleStationCmdInputDto value)
        {
            void Cmd()
            {
                SampleStationProxy sampleStation = StationConfig2Proxy(value);
                sampleStation.StartNormalSample();
            }

            return ExecSampleStationCmd<bool>(Cmd);
        }

        // POST: api/DataSampler/StartNormalSample
        /// <summary>
        /// 采集工作站停止采集
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public SampleStationCmdOutputDto<bool> StopSample([FromBody] SampleStationCmdInputDto value)
        {
            void Cmd()
            {
                SampleStationProxy sampleStation = StationConfig2Proxy(value);
                sampleStation.StopSample();
            }

            return ExecSampleStationCmd<bool>(Cmd);
        }

        // POST: api/DataSampler/StartNormalSample
        /// <summary>
        /// 采集工作站下载采集配置参数
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public SampleStationCmdOutputDto<SampleStationParameterErrorDataCollection> DownloadSampleConfi([FromBody] SampleStationCmdInputDto value)
        {
            SampleStationParameterErrorDataCollection parameterErrorDatas = null;
            void Cmd()
            {
                SampleStationProxy sampleStation = StationConfig2Proxy(value);
                sampleStation.DownloadSampleConfig(ref parameterErrorDatas);
            }

            var ret = ExecSampleStationCmd<SampleStationParameterErrorDataCollection>(Cmd);
            ret.Data = parameterErrorDatas;

            return ret;
        }
    }
}
