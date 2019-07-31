using System;
using Moons.Common20;
// 计算中使用的数值类型，用以弥补不能直接使用泛型的问题。
using _ValueT = System.Single;

namespace AnalysisAlgorithm
{
    /// <summary>
    /// 相关算法类
    /// </summary>
    public static partial class Correlate
    {
        /// <summary>
        /// 直接计算两个数组的相关函数(已做端点效应修正)
        /// </summary>
        /// <param name="xArray">输入第一个数组</param>
        /// <param name="yArray">输入第二个数组</param>
        /// <param name="dt">输入采样时间间隔</param>
        /// <param name="rxy">返回相关结果数组</param>
        /// <param name="tau">返回时间延迟数组</param>
        /// <remarks>
        /// 计算公式如下: 
        ///	如果: 
        ///		n = x[]的元素个数      m = y[]的元素个数 
        /// 那么: 
        ///		              m-1 
        ///			Rxy[i] =  Σ x[k+n-1-i] * y[k] 
        ///		              k=0 
        /// </remarks>  
        public static void DirectCorrelate( _ValueT[] xArray, _ValueT[] yArray, _ValueT dt,
                                            out _ValueT[] rxy, out _ValueT[] tau )
        {
            int xLength = xArray.Length;
            int yLength = yArray.Length;

            int rxyLength = xLength + yLength - 1;
            _ValueT[] rxyArray = new _ValueT[rxyLength];
            _ValueT[] tauArray = new _ValueT[rxyLength];

            for( int outterIndex = 0; outterIndex < rxyLength; outterIndex++ )
            {
                _ValueT rxySum = 0;

                // 累加的次数
                int count = 0;

                for( int innerIndex = 0; innerIndex < yLength; innerIndex++ )
                {
                    int xIndex = innerIndex + xLength - 1 - outterIndex;
                    if( 0 <= xIndex && xIndex < xLength )
                    {
                        count++;
                        rxySum += xArray[xIndex] * yArray[innerIndex];
                    }
                }

                tauArray[outterIndex] = ( outterIndex - xLength + 1 ) * dt;

                //进行端点效应修正
                //rxyArray[outterIndex] = rxySum / count;

                // 不做端点修正
                rxyArray[outterIndex] = rxySum;
            }

            // 根据盛国瑛的意见，相关返回N，不返回N+M-1，N为两个数组长度的小值
            //rxy = new _ValueT[Math.Min( xLength, yLength )];
            //tau = new _ValueT[rxy.Length];
            //for( int index = 0; index < rxy.Length; index++ )
            //{
            //    rxy[index] = rxyArray[index];
            //    tau[index] = tauArray[index];
            //}

            // 必须要返回完整的N+M-1，且x轴显示tau，否则无法通过相关方法确定锤击实验中两个传感器的时间差
            rxy = rxyArray;
            tau = tauArray;
        }

        /// <summary>
        /// 快速相关(已做端点效应修正)
        /// </summary>
        /// <param name="xArray">输入第一个数组x[n]</param>
        /// <param name="yArray">输入第二个数组y[m]</param>
        /// <param name="dt">输入采样时间间隔</param>
        /// <param name="rxy">返回相关结果数组</param>
        /// <param name="tau">返回时间延迟数组</param>
        /// <remarks>
        ///计算步骤如下：
        ///1.选择L满足：L>=n+m-1;L=2^r；
        ///2.将x[n]和y[m]补零,形成长为L=2^r的序列；
        ///		x0[i]=x[i], i∈[0,n-1];
        ///		x0[i]=0,    i∈[n,L-1];
        ///		y0[i]=0,    i∈[0,n-2];
        ///		y0[i]=y[i], i∈[n-1,n+m-2];
        ///		y0[i]=0,    i∈[n+m-1,L-1];
        ///3.用FFT分别计算x0[i]和y0[i]的X0[i]和Y0[i]；
        ///4.计算X0[i]的共轭和Y0[i]的乘积, Z[i]=conj(X0[i])*Y0[i];
        ///5.用FFT计算Z[i]的逆变换，取前n+m-1即是线性相关。
        /// </remarks>
        public static void FastCorrelate( _ValueT[] xArray, _ValueT[] yArray, _ValueT dt,
                                          out _ValueT[] rxy, out _ValueT[] tau )
        {
            int xLength = xArray.Length;
            int yLength = yArray.Length;

            int rxyLength = xLength + yLength - 1;
            _ValueT[] rxyArray = new _ValueT[rxyLength];
            _ValueT[] tauArray = new _ValueT[rxyLength];

            //步骤1-选择L
            int fftLength = 1;
            for( int index = 1; index <= MathConst.FFTPowerMax; index++ )
            {
                fftLength *= 2;
                if( fftLength >= rxyLength )
                {
                    break;
                }
            }

            //步骤2-将x[n]和y[m]补零,形成长为L=2^r的序列
            _ValueT[] xr0 = new _ValueT[fftLength];
            _ValueT[] yr0 = new _ValueT[fftLength];

            // 复制数据
            xArray.CopyTo( xr0, 0 );
            yArray.CopyTo( yr0, xLength - 1 );

            //步骤3-用FFT分别计算x0[i]和y0[i]的X0[i]和Y0[i]
            _ValueT[] xi0, yi0;
            DSPBasic.ReFFT( xr0, out xi0 );
            DSPBasic.ReFFT( yr0, out yi0 );

            //步骤4-计算X0[i]的共轭和Y0[i]的乘积, Z[i]=conj(X0[i])*Y0[i];
            _ValueT[] zr = new _ValueT[fftLength];
            _ValueT[] zi = new _ValueT[fftLength];

            for( int index = 0; index < fftLength; index++ )
            {
                zr[index] = xr0[index] * yr0[index] + xi0[index] * yi0[index];
                zi[index] = xr0[index] * yi0[index] - xi0[index] * yr0[index];
            }

            //步骤4-用FFT计算Z[i]的逆变换，取前n+m-1即是线性相关
            DSPBasic.CxInvFFT( zr, zi );

            //进行端点效应修正
            for( int index = 0; index < rxyLength; index++ )
            {
                tauArray[index] = ( index - xLength + 1 ) * dt;
                //rxyArray[index] = index < xLength ? zr[index] / ( index + 1 ) : zr[index] / ( 2 * xLength - index );

                // 不做端点修正
                rxyArray[index] = zr[index];
            }

            // 根据盛国瑛的意见，相关返回N，不返回N+M-1，N为两个数组长度的小值
            //rxy = new _ValueT[Math.Min( xLength, yLength )];
            //tau = new _ValueT[rxy.Length];
            //for( int index = 0; index < rxy.Length; index++ )
            //{
            //    rxy[index] = rxyArray[index];
            //    tau[index] = tauArray[index];
            //}

            // 必须要返回完整的N+M-1，且x轴显示tau，否则无法通过相关方法确定锤击实验中两个传感器的时间差
            rxy = rxyArray;
            tau = tauArray;
        }

        /// <summary>
        /// 互谱：使用快速相关的中间结果作为互谱
        /// </summary>
        /// <param name="xArray">输入第一个数组x[n]</param>
        /// <param name="yArray">输入第二个数组y[m]</param>
        /// <param name="ampSpectrum">返回幅值谱数组</param>
        /// <param name="phaseSpectrum">返回相位谱数组</param>
        /// <param name="fftLength">做fft变换的时间波形长度</param>
        /// <remarks>
        ///计算步骤如下：
        ///1.选择L满足：L>=n+m-1;L=2^r；
        ///2.将x[n]和y[m]补零,形成长为L=2^r的序列；
        ///		x0[i]=x[i], i∈[0,n-1];
        ///		x0[i]=0,    i∈[n,L-1];
        ///		y0[i]=0,    i∈[0,n-2];
        ///		y0[i]=y[i], i∈[n-1,n+m-2];
        ///		y0[i]=0,    i∈[n+m-1,L-1];
        ///3.用FFT分别计算x0[i]和y0[i]的X0[i]和Y0[i]；
        ///4.计算X0[i]的共轭和Y0[i]的乘积, Z[i]=conj(X0[i])*Y0[i];
        ///5.用FFT计算Z[i]的逆变换，取前n+m-1即是线性相关。
        /// </remarks>
        public static void CrossSpectrum( _ValueT[] xArray, _ValueT[] yArray, out _ValueT[] ampSpectrum,
                                          out _ValueT[] phaseSpectrum, out int fftLength )
        {
            int xLength = xArray.Length;
            int yLength = yArray.Length;

            int rxyLength = xLength + yLength - 1;

            //步骤1-选择L
            int fftWaveLength = 1;
            for( int index = 1; index <= MathConst.FFTPowerMax; index++ )
            {
                fftWaveLength *= 2;
                if( fftWaveLength >= rxyLength )
                {
                    break;
                }
            }
            fftLength = fftWaveLength;

            //步骤2-将x[n]和y[m]补零,形成长为L=2^r的序列
            _ValueT[] xr0 = new _ValueT[fftWaveLength];
            _ValueT[] yr0 = new _ValueT[fftWaveLength];

            // 复制数据
            xArray.CopyTo( xr0, 0 );
            yArray.CopyTo( yr0, xLength - 1 );

            //步骤3-用FFT分别计算x0[i]和y0[i]的X0[i]和Y0[i]
            _ValueT[] xi0, yi0;
            DSPBasic.ReFFT( xr0, out xi0 );
            DSPBasic.ReFFT( yr0, out yi0 );

            //步骤4-计算X0[i]的共轭和Y0[i]的乘积, Z[i]=conj(X0[i])*Y0[i];
            _ValueT[] zr = new _ValueT[fftWaveLength];
            _ValueT[] zi = new _ValueT[fftWaveLength];

            for( int index = 0; index < fftWaveLength; index++ )
            {
                zr[index] = xr0[index] * yr0[index] + xi0[index] * yi0[index];
                zi[index] = xr0[index] * yi0[index] - xi0[index] * yr0[index];
            }

            ampSpectrum = new _ValueT[DSPBasic.GetSpectrumLengthByTimeWaveLength( fftLength )];
            phaseSpectrum = new _ValueT[ampSpectrum.Length];

            // 不进行端点修正
            for( int index = 0; index < ampSpectrum.Length; index++ )
            {
                var ampSquare = zr[index] * zr[index] + zi[index] * zi[index];
                if( ampSquare != 0 )
                {
                    ampSpectrum[index] = (_ValueT)Math.Sqrt( ampSquare );
                }
                phaseSpectrum[index] = (_ValueT)MathBasic.ReIm2Phase180( zr[index], zi[index] );
            }
        }

        /// <summary>
        /// 直接计算两个数组的相干函数
        /// </summary>
        /// <param name="xArray">输入第一个数组</param>
        /// <param name="yArray">输入第二个数组</param>
        /// <returns>返回相干结果数组</returns>
        public static _ValueT[] DirectCoherence( _ValueT[] xArray, _ValueT[] yArray )
        {
            #region 输入参数合理性检查

            MathError.CheckLengthGTZero( xArray );
            MathError.CheckLengthGTZero( yArray );

            #endregion

            int minLength = Math.Min( xArray.Length, yArray.Length );

            _ValueT[] r, tau, wave4FFT = new _ValueT[minLength];

            DirectCorrelate( xArray, yArray, 1, out r, out tau );
            ArrayUtils.Copy( r, wave4FFT, 0, wave4FFT.Length );
            var xyAmpSpectrum = SpectrumBasic.AmpSpectrum( wave4FFT );

            DirectCorrelate( xArray, xArray, 1, out r, out tau );
            ArrayUtils.Copy( r, wave4FFT, 0, wave4FFT.Length );
            var xxAmpSpectrum = SpectrumBasic.AmpSpectrum( wave4FFT );

            DirectCorrelate( yArray, yArray, 1, out r, out tau );
            ArrayUtils.Copy( r, wave4FFT, 0, wave4FFT.Length );
            var yyAmpSpectrum = SpectrumBasic.AmpSpectrum( wave4FFT );

            return CrossSpectrum2Coherence( xyAmpSpectrum, xxAmpSpectrum, yyAmpSpectrum );
        }

        /// <summary>
        /// 使用快速相关的中间结果作为互谱，计算两个数组的相干
        /// </summary>
        /// <param name="xArray">输入第一个数组</param>
        /// <param name="yArray">输入第二个数组</param>
        /// <param name="coherence">相干结果数组</param>
        /// <param name="fftLength">做fft变换的时间波形长度</param>
        /// <returns>返回相干结果数组</returns>
        public static void FastCoherence( _ValueT[] xArray, _ValueT[] yArray, out _ValueT[] coherence,
                                          out int fftLength )
        {
            #region 输入参数合理性检查

            MathError.CheckLengthGTZero( xArray );
            MathError.CheckLengthGTZero( yArray );
            MathError.CheckLengthEqual( xArray, yArray );

            #endregion

            // 使用快速相关的中间结果作为互谱
            _ValueT[] xyAmpSpectrum, xyPhaseSpectrum;
            int xyFftLength;
            CrossSpectrum( xArray, yArray, out xyAmpSpectrum, out xyPhaseSpectrum, out xyFftLength );

            _ValueT[] xxAmpSpectrum, xxPhaseSpectrum;
            int xxFftLength;
            CrossSpectrum( xArray, xArray, out xxAmpSpectrum, out xxPhaseSpectrum, out xxFftLength );

            _ValueT[] yyAmpSpectrum, yyPhaseSpectrum;
            int yyFftLength;
            CrossSpectrum( yArray, yArray, out yyAmpSpectrum, out yyPhaseSpectrum, out yyFftLength );

            fftLength = xyFftLength;

            coherence = CrossSpectrum2Coherence( xyAmpSpectrum, xxAmpSpectrum, yyAmpSpectrum );
        }

        /// <summary>
        /// 使用互谱结果计算相干
        /// </summary>
        /// <param name="xyAmpSpectrum">Sxy幅值</param>
        /// <param name="xxAmpSpectrum">Sxx幅值</param>
        /// <param name="yyAmpSpectrum">Syy幅值</param>
        /// <returns>返回相干结果数组</returns>
        private static _ValueT[] CrossSpectrum2Coherence( _ValueT[] xyAmpSpectrum, _ValueT[] xxAmpSpectrum,
                                                          _ValueT[] yyAmpSpectrum )
        {
            var ret = new _ValueT[xyAmpSpectrum.Length];
            for( int index = 0; index < ret.Length; index++ )
            {
                var GxxGyy = xxAmpSpectrum[index] * yyAmpSpectrum[index];
                var GxyGxy = xyAmpSpectrum[index] * xyAmpSpectrum[index];
                if( GxyGxy != 0 && GxxGyy != 0 )
                {
                    ret[index] = (_ValueT)Math.Sqrt( GxyGxy / GxxGyy );
                }
            }
            return ret;
        }

        /// <summary>
        /// 直接计算两个数组的频响函数
        /// </summary>
        /// <param name="xArray">输入第一个数组</param>
        /// <param name="yArray">输入第二个数组</param>
        /// <param name="ampSpectrum">频响的幅频特性</param>
        /// <param name="phaseSpectrum">频响的相频特性</param>
        public static void DirectFrequenceResponse( _ValueT[] xArray, _ValueT[] yArray, out _ValueT[] ampSpectrum,
                                                    out _ValueT[] phaseSpectrum )
        {
            #region 输入参数合理性检查

            MathError.CheckLengthGTZero( xArray );
            MathError.CheckLengthGTZero( yArray );

            #endregion

            int minLength = Math.Min( xArray.Length, yArray.Length );
            int lineCount = DSPBasic.GetSpectrumLengthByTimeWaveLength( minLength );

            // 使用直接计算的相关再计算互谱
            _ValueT[] r, tau, wave4FFT = new _ValueT[minLength];
            DirectCorrelate( xArray, yArray, 1, out r, out tau );
            ArrayUtils.Copy( r, wave4FFT, 0, wave4FFT.Length );

            _ValueT[] xyAmpSpectrum, xyPhaseSpectrum;
            DSPBasic.BiAmpPhaseSpectrum( wave4FFT, out xyAmpSpectrum, out xyPhaseSpectrum );

            DirectCorrelate( xArray, xArray, 1, out r, out tau );
            ArrayUtils.Copy( r, wave4FFT, 0, wave4FFT.Length );

            _ValueT[] xxAmpSpectrum, xxPhaseSpectrum;
            DSPBasic.BiAmpPhaseSpectrum( wave4FFT, out xxAmpSpectrum, out xxPhaseSpectrum );

            ampSpectrum = new _ValueT[lineCount];
            phaseSpectrum = new _ValueT[ampSpectrum.Length];

            CrossSpectrum2FrequenceResponse( xyAmpSpectrum, xyPhaseSpectrum, xxAmpSpectrum, xxPhaseSpectrum, ampSpectrum,
                                             phaseSpectrum );
        }

        /// <summary>
        /// 使用快速相关的中间结果作为互谱，计算两个数组的频响函数
        /// </summary>
        /// <param name="xArray">输入第一个数组</param>
        /// <param name="yArray">输入第二个数组</param>
        /// <param name="ampSpectrum">频响的幅频特性</param>
        /// <param name="phaseSpectrum">频响的相频特性</param>
        /// <param name="fftLength">做fft变换的时间波形长度</param>
        public static void FastFrequenceResponse( _ValueT[] xArray, _ValueT[] yArray, out _ValueT[] ampSpectrum,
                                                  out _ValueT[] phaseSpectrum, out int fftLength )
        {
            #region 输入参数合理性检查

            MathError.CheckLengthGTZero( xArray );
            MathError.CheckLengthGTZero( yArray );
            MathError.CheckLengthEqual( xArray, yArray );

            #endregion

            // 使用快速相关的中间结果作为互谱
            _ValueT[] xyAmpSpectrum, xyPhaseSpectrum;
            int xyFftLength;
            CrossSpectrum( xArray, yArray, out xyAmpSpectrum, out xyPhaseSpectrum, out xyFftLength );

            _ValueT[] xxAmpSpectrum, xxPhaseSpectrum;
            int xxFftLength;
            CrossSpectrum( xArray, xArray, out xxAmpSpectrum, out xxPhaseSpectrum, out xxFftLength );

            fftLength = xyFftLength;

            ampSpectrum = new _ValueT[xyAmpSpectrum.Length];
            phaseSpectrum = new _ValueT[ampSpectrum.Length];

            CrossSpectrum2FrequenceResponse( xyAmpSpectrum, xyPhaseSpectrum, xxAmpSpectrum, xxPhaseSpectrum, ampSpectrum,
                                             phaseSpectrum );
        }

        /// <summary>
        /// 使用互谱结果计算频响
        /// </summary>
        /// <param name="xyAmpSpectrum">Sxy幅值</param>
        /// <param name="xyPhaseSpectrum">Sxy相位</param>
        /// <param name="xxAmpSpectrum">Sxx幅值</param>
        /// <param name="xxPhaseSpectrum">Sxx相位</param>
        /// <param name="ampSpectrum">频响的幅频特性</param>
        /// <param name="phaseSpectrum">频响的相频特性</param>
        private static void CrossSpectrum2FrequenceResponse( _ValueT[] xyAmpSpectrum, _ValueT[] xyPhaseSpectrum,
                                                             _ValueT[] xxAmpSpectrum, _ValueT[] xxPhaseSpectrum,
                                                             _ValueT[] ampSpectrum,
                                                             _ValueT[] phaseSpectrum )
        {
            for( int index = 0; index < ampSpectrum.Length; index++ )
            {
                phaseSpectrum[index] =
                    (_ValueT)MathBasic.DegreeToAngle180( xyPhaseSpectrum[index] - xxPhaseSpectrum[index] );
                if( xxAmpSpectrum[index] != 0 )
                {
                    ampSpectrum[index] = xyAmpSpectrum[index] / xxAmpSpectrum[index];
                }
            }
        }
    }
}