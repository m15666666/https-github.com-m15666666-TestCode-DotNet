using System;

namespace AnalysisAlgorithm
{
    /// <summary>
    ///     动平衡算法工具类
    /// </summary>
    public static class DynBalance
    {
        /// <summary>
        ///     计算影响系数
        /// </summary>
        /// <param name="vibrations">振动矢量，输入</param>
        /// <param name="trialWeights">试重矢量，输入</param>
        /// <param name="coeffecients">动平衡的影响系数，输出</param>
        /// <param name="isAttatcheds">试重块1是否保留在转子上，输入</param>
        /// <param name="isSinglePlaneBalance">输入，true:单面动平衡（默认），false：双面</param>
        /// <returns>校正平衡量，输出</returns>
        public static Complex[] CalcCoeffecients( Complex[] vibrations, Complex[] trialWeights, Complex[] coeffecients,
            bool[] isAttatcheds, bool isSinglePlaneBalance = true )
        {
            // 单面动平衡
            if( isSinglePlaneBalance )
            {
                if( ( vibrations.Length < 2 ) || ( trialWeights.Length < 1 ) || ( coeffecients.Length < 1 ) )
                {
                    return null;
                }

                // 求影响系数
                coeffecients[0] = ( vibrations[1] - vibrations[0] ) / trialWeights[0];

                return CalcCorrectWeightsByCoeffecient( new[] { isAttatcheds[0] ? vibrations[1] : vibrations[0] },
                    coeffecients );
            }

            // 双面动平衡
            if( ( vibrations.Length < 6 ) || ( trialWeights.Length < 2 ) || ( coeffecients.Length < 4 ) )
            {
                return null;
            }

            // 求影响系数(排列顺序依次为a11,a12,a21,a22)
            coeffecients[0] = ( vibrations[2] - vibrations[0] ) / trialWeights[0];
            coeffecients[2] = ( vibrations[3] - vibrations[1] ) / trialWeights[0];

            // 保留试重1后计算影响系数
            if( isAttatcheds[0] )
            {
                coeffecients[1] = ( vibrations[4] - vibrations[2] ) / trialWeights[1];
                coeffecients[3] = ( vibrations[5] - vibrations[3] ) / trialWeights[1];
            }
            else
            {
                coeffecients[1] = ( vibrations[4] - vibrations[0] ) / trialWeights[1];
                coeffecients[3] = ( vibrations[5] - vibrations[1] ) / trialWeights[1];
            }

            // 求剩余不平衡量
            Complex vib1, vib2;
            if( isAttatcheds[1] )
            {
                vib1 = vibrations[4];
                vib2 = vibrations[5];
            }
            else if( isAttatcheds[0] )
            {
                vib1 = vibrations[2];
                vib2 = vibrations[3];
            }
            else
            {
                vib1 = vibrations[0];
                vib2 = vibrations[1];
            }

            return CalcCorrectWeightsByCoeffecient( new[] { vib1, vib2 }, coeffecients, false );
        }

        /// <summary>
        ///     已知影响系数，计算校正平衡量
        /// </summary>
        /// <param name="vibrations">振动矢量，输入</param>
        /// <param name="coeffecients">动平衡的影响系数，输入</param>
        /// <param name="isSinglePlaneBalance">true:单面动平衡（默认），false：双面</param>
        /// <returns>校正平衡量，输出</returns>
        public static Complex[] CalcCorrectWeightsByCoeffecient( Complex[] vibrations, Complex[] coeffecients,
            bool isSinglePlaneBalance = true )
        {
            //单面动平衡
            if( isSinglePlaneBalance )
            {
                if( ( vibrations.Length < 1 ) || ( coeffecients.Length < 1 ) )
                {
                    return null;
                }
                // 求剩余不平衡量
                var correctWeight = -vibrations[0] / coeffecients[0];

                return new[] { correctWeight };
            }

            //双面动平衡
            if( ( vibrations.Length < 2 ) || ( coeffecients.Length < 4 ) )
            {
                return null;
            }

            // 求剩余不平衡量
            try
            {
                var tempValue = coeffecients[0] * coeffecients[3] - coeffecients[1] * coeffecients[2];

                var correctWeight1 = ( vibrations[1] * coeffecients[1] - vibrations[0] * coeffecients[3] ) / tempValue;
                var correctWeight2 = ( vibrations[0] * coeffecients[2] - vibrations[1] * coeffecients[0] ) / tempValue;

                return new[] { correctWeight1, correctWeight2 };
            }
            catch( Exception )
            {
                return new[] { new Complex( 0, 0 ), new Complex( 0, 0 ) };
            }
        }
    }
}