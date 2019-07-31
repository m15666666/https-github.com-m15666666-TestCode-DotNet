using System;

namespace AnalysisAlgorithm.Helper
{
    /// <summary>
    /// 创建正弦波的参数类
    /// </summary>
    public class CreateSinParameter
    {
        /// <summary>
        /// 数据长度
        /// </summary>
        public int DataLength { get; set; }

        /// <summary>
        /// 初始相位，单位是度
        /// </summary>
        public Double InitPhaseInDegree { get; set; }

        /// <summary>
        /// 信号频率
        /// </summary>
        public Double F0 { get; set; }

        /// <summary>
        /// 采样频率
        /// </summary>
        public Double Fs { get; set; }

        /// <summary>
        /// 幅值
        /// </summary>
        public Double Amplitude { get; set; }

        /// <summary>
        /// 创建正弦波参数
        /// </summary>
        public CreateSinParameter()
        {
            InitPhaseInDegree = 0;

            Amplitude = 1;

            DataLength = 1024;

            F0 = 1;

            Fs = 1024;
        }
    }
}
