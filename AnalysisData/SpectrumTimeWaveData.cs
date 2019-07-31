// /*----------------------------------------------------------------
// // Copyright (C) 2009 上海鸣志自动控制设备有限公司
// // 版权所有。 
// //
// // 文件功能描述：包含频谱、波形数据的数据类型
// //
// // 
// // 创建人员：董建林
// // 创建日期：2011-07-18
// //----------------------------------------------------------------*/

using System;

namespace AnalysisData
{
    /// <summary>
    /// 频谱、时间波形数据
    /// </summary>
    [Serializable]
    public class SpectrumTimeWaveData : WaveDataBase
    {
        /// <summary>
        /// 时间波形
        /// </summary>
        public double[] TimeWave;

        /// <summary>
        /// 频谱
        /// </summary>
        public double[] Spectrum;

        /// <summary>
        /// 频谱数据长度
        /// </summary>
        public int SpectrumDataLength;
    }
}