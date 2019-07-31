using System;

namespace AnalysisAlgorithm
{
    /// <summary>
    /// 计算特征频率的实用工具类
    /// </summary>
    public static class FeatureFrequences
    {
        #region 获得边频

        #region 获得轴承的边频

        /// <summary>
        /// 获得轴承的边频（以轴承所在轴的阶次表示，例如：1.1表示工频的1.1倍）
        /// </summary>
        /// <param name="bpfi">内圈通过频率</param>
        /// <param name="bpfo">外圈通过频率</param>
        /// <param name="bsf">滚动体频率</param>
        /// <param name="ftfi">保持架频率</param>
        /// <param name="bpfiSideFreqOrder">内圈通过频率的边频</param>
        /// <param name="bpfoSideFreqOrder">外圈通过频率的边频</param>
        /// <param name="bsfSideFreqOrder">滚动体频率的边频</param>
        public static void GetBearingSideFreqOrder( Double bpfi, Double bpfo, Double bsf, Double ftfi,
                                                    out Double bpfiSideFreqOrder, out Double bpfoSideFreqOrder,
                                                    out Double bsfSideFreqOrder )
        {
            bpfiSideFreqOrder = bpfoSideFreqOrder = 1;
            bsfSideFreqOrder = ftfi;
        }

        #endregion

        /// <summary>
        /// 获得齿轮的啮合频率和边频（以齿轮所在轴的阶次表示，例如：1.1表示工频的1.1倍）
        /// </summary>
        /// <param name="teethOfThisSide">本侧齿轮齿数</param>
        /// <param name="teethOfTheOtherSide">另一侧齿轮齿数</param>
        /// <param name="meshingFreqOrder">啮合频率</param>
        /// <param name="sideFreqOrderOfThisSide">本侧的边频</param>
        /// <param name="sideFreqOrderOfTheOtherSide">另一侧的边频</param>
        public static void GetGearSideFreqOrder( Double teethOfThisSide, Double teethOfTheOtherSide,
                                                 out Double meshingFreqOrder, out Double sideFreqOrderOfThisSide,
                                                 out Double sideFreqOrderOfTheOtherSide )
        {
            meshingFreqOrder = teethOfThisSide;
            sideFreqOrderOfThisSide = 1;
            sideFreqOrderOfTheOtherSide = teethOfThisSide / teethOfTheOtherSide;
        }

        #endregion
    }
}