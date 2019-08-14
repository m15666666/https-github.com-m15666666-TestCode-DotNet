using System;
using System.Text;

namespace SampleServer.Helper
{
    /// <summary>
    /// 采集服务器的探针数据类
    /// </summary>
    public class SampleServerProbeData
    {
        #region 变量和属性

        /// <summary>
        /// 正常采集的报警队列中的元素个数
        /// </summary>
        private int _normalAlmQueueCount;

        /// <summary>
        /// 正常采集的队列中的元素个数
        /// </summary>
        private int _normalDataQueueCount;

        /// <summary>
        /// 程序启动时间
        /// </summary>
        public DateTime ProgramStartTime { get; set; }

        /// <summary>
        /// 最后一次更新正常采集的队列的时间
        /// </summary>
        public DateTime UpdateNormalSampleQueueTime { get; private set; }

        /// <summary>
        /// 最后一次上传正常采集数据的时间
        /// </summary>
        public DateTime UploadNormalSampleDataTime { get; set; }

        /// <summary>
        /// 最后一次上传正常采集报警的时间
        /// </summary>
        public DateTime UploadNormalSampleAlmTime { get; set; }

        /// <summary>
        /// 上传正常采集数据过程中的信息
        /// </summary>
        public string UploadNormalDataMessage { get; set; }

        /// <summary>
        /// 上传正常采集报警过程中的信息
        /// </summary>
        public string UploadNormalAlmMessage { get; set; }

        /// <summary>
        /// 最后一次上传正常采集数据的时间跨度
        /// </summary>
        public TimeSpan LastUploadNormalDataTimeSpan { get; set; }

        /// <summary>
        /// 最后一次上传正常采集报警的时间跨度
        /// </summary>
        public TimeSpan LastUploadNormalAlmTimeSpan { get; set; }

        #endregion

        /// <summary>
        /// 更新正常采集队列中的元素个数
        /// </summary>
        /// <param name="count">正常采集队列中的元素个数</param>
        public void SetNormalDataQueueCount( int count )
        {
            _normalDataQueueCount = count;

            UpdateNormalSampleQueueTime = DateTime.Now;
        }

        /// <summary>
        /// 更新正常采集报警队列中的元素个数
        /// </summary>
        /// <param name="count">正常采集报警队列中的元素个数</param>
        public void SetNormalAlmQueueCount( int count )
        {
            _normalAlmQueueCount = count;

            UpdateNormalSampleQueueTime = DateTime.Now;
        }

        /// <summary>
        /// 重置各个属性
        /// </summary>
        public void Reset()
        {
            _normalAlmQueueCount = _normalDataQueueCount = 0;

            UploadNormalSampleDataTime = UploadNormalSampleAlmTime = UpdateNormalSampleQueueTime = DateTime.MinValue;
        }


        public override string ToString()
        {
            var buffer = new StringBuilder();

            buffer.AppendFormat( "ProgramStartTime:{0}", ProgramStartTime );
            buffer.AppendLine();

            buffer.AppendFormat( "NormalDataQueueCount:{0}({1})", _normalDataQueueCount, UpdateNormalSampleQueueTime );
            buffer.AppendLine();

            buffer.AppendFormat( "NormalAlmQueueCount:{0}({1})", _normalAlmQueueCount, UpdateNormalSampleQueueTime );
            buffer.AppendLine();

            buffer.AppendFormat( "UploadNormalSampleDataTime:{0}", UploadNormalSampleDataTime );
            buffer.AppendLine();

            buffer.AppendFormat( "UploadNormalSampleAlmTime:{0}", UploadNormalSampleAlmTime );
            buffer.AppendLine();

            if( !string.IsNullOrEmpty( UploadNormalDataMessage ) )
            {
                buffer.AppendFormat( "UploadNormalDataMessage:{0}", UploadNormalDataMessage );
                buffer.AppendLine();
            }

            if( !string.IsNullOrEmpty( UploadNormalAlmMessage ) )
            {
                buffer.AppendFormat( "UploadNormalAlmMessage:{0}", UploadNormalAlmMessage );
                buffer.AppendLine();
            }

            buffer.AppendFormat( "LastUploadNormalDataTotalSeconds:{0}", LastUploadNormalDataTimeSpan.TotalSeconds );
            buffer.AppendLine();

            buffer.AppendFormat( "LastUploadNormalAlmTotalSeconds:{0}", LastUploadNormalAlmTimeSpan.TotalSeconds );
            buffer.AppendLine();

            return buffer.ToString();
        }
    }
}