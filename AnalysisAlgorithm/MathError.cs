using System;

namespace AnalysisAlgorithm
{
    /// <summary>
    /// 计算错误信息类。当一个计算错误产生时，算法组件库返回一个错误信息。
    /// </summary>
    public static class MathError
    {
        /// <summary>
        /// 输入序列必须是相同长度
        /// </summary>
        public const string MathErrorEqualSamples = "输入序列必须是相同长度";

        /// <summary>
        /// 样本的个数必须大于0
        /// </summary>
        public const string MathErrorSamplesGTZero = "样本的个数必须大于0";

        /// <summary>
        /// 样本的个数必须大于或等于0
        /// </summary>
        public const string MathErrorSamplesGEZero = "样本的个数必须大于或等于0";

        /// <summary>
        /// 样本的个数必须大于2
        /// </summary>
        public const string MathErrorSamplesGETwo = " 样本的个数必须大于2";

        /// <summary>
        /// 样本的个数必须大于3
        /// </summary>
        public const string MathErrorSamplesGEThree = "样本的个数必须大于3";

        /// <summary>
        /// 输入数组的长度没有满足指定的条件
        /// </summary>
        public const string MathErrorArraySize = "输入数组的长度没有满足指定的条件";

        /// <summary>
        /// 输入数组的长度必须是一个2的幂次
        /// </summary>
        public const string MathErrorPowerOfTwo = "输入数组的长度必须是一个2的幂次";

        /// <summary>
        /// 周期数必须满足条件：周期数 ∈ (0, 样本数)
        /// </summary>
        public const string MathErrorCycles = "周期数必须满足条件： 0< 周期数 < 样本数";

        /// <summary>
        /// 延迟和宽度必须满足条件：(延迟 + 宽度) 小于 (样本数)
        /// </summary>
        public const string MathErrorDelayWidth = "延迟和宽度必须满足条件： (延迟 + 宽度) < 样本数";

        /// <summary>
        /// dt或dx必须大于0
        /// </summary>
        public const string MathErrorDtGTZero = "dt或dx必须大于0";

        /// <summary>
        /// 数组下标必须满足条件：index ∈ [0,样本数) 
        /// </summary>
        public const string MathErrorIndexLTSamples = "数组下标必须满足 0 <= 序号 < 样本数";

        /// <summary>
        /// 数组下标和长度必须满足条件：(index + len) ∈ [0,样本数) 
        /// </summary>
        public const string MathErrorIndexLength = "序号和长度必须满足0 <= (序号 + 长度) < 样本数";

        /// <summary>
        /// 上限值必须大于或等于下限值
        /// </summary>
        public const string MathErrorUpperGELower = "上限值必须大于或等于下限值";

        /// <summary>
        /// 上限值必须大于下限值
        /// </summary>
        public const string MathErrorUpperGTLower = "截止频率上限值必须大于下限值";

        /// <summary>
        /// 截至频率fc必须满足条件：fc ∈ [0,fs/2]
        /// </summary>
        public const string MathErrorNyquist = "截至频率fc必须满足0 <= fc <= (fs/2)";

        /// <summary>
        /// 阶数必须大于0
        /// </summary>
        public const string MathErrorOrderGTZero = "阶数必须大于0";

        /// <summary>
        /// 十倍因素值必须满足条件：十倍因素 ∈(0, 样本数]
        /// </summary>
        public const string MathErrorDecimationFactor = "十倍因素值必须满足0 < 十倍因素 <= 样本数";

        /// <summary>
        /// 无效的带宽指标
        /// </summary>
        public const string MathErrorBandSpec = "无效的带宽指标";

        /// <summary>
        /// 纹波幅度必须大于0
        /// </summary>
        public const string MathErrorRippleGTZero = "纹波幅度必须大于0";

        /// <summary>
        /// 衰减必须大于0
        /// </summary>
        public const string MathErrorAttenGTZero = "衰减必须大于0";

        /// <summary>
        /// 衰减必须大于纹波幅值
        /// </summary>
        public const string MathErrorAttenGTRipple = "衰减必须大于纹波幅值";

        /// <summary>
        /// 滤波器设计不能满足指定的参数性能
        /// </summary>
        public const string MathErrorEqRippleDesign = "滤波器设计不能满足指定的参数性能";

        /// <summary>
        /// 滤波器系数个数必须是奇数
        /// </summary>
        public const string MathErrorEvenSize = "滤波器系数个数必须是奇数";

        /// <summary>
        /// 滤波器系数个数必须是偶数
        /// </summary>
        public const string MathErrorOddSize = "滤波器系数个数必须是偶数";

        /// <summary>
        /// 第二个数组必须是全为正或负且没有0
        /// </summary>
        public const string MathErrorMixedSign = "第二个数组必须是全为正或负且没有0";

        /// <summary>
        /// 数组长度必须大于阶数
        /// </summary>
        public const string MathErrorSizeGTOrder = "数组长度必须大于阶数";

        /// <summary>
        /// 输入矩阵必须是一个方阵
        /// </summary>
        public const string MathErrorSquareMatrix = "输入矩阵必须是一个方阵";

        /// <summary>
        /// 输入矩阵是奇异的，方程组无解
        /// </summary>
        public const string MathErrorSingularMatrix = "输入矩阵是奇异的，方程组无解";

        /// <summary>
        /// 层次个数超出了允许的范围
        /// </summary>
        public const string MathErrorLevels = "层次个数超出了允许的范围";

        /// <summary>
        /// FFT长度超出了允许的范围2^30
        /// </summary>
        public const string MathErrorFFTSize = "FFT长度超出了允许的范围2^30";

        /// <summary>
        /// 因素的水平超出了允许的范围
        /// </summary>
        public const string MathErrorFactor = "因素的水平超出了允许的范围";

        /// <summary>
        /// 太少的观测量
        /// </summary>
        public const string MathErrorObservations = "太少的观测量";

        /// <summary>
        /// 数据错误
        /// </summary>
        public const string MathErrorData = "数据错误";

        /// <summary>
        /// 数据不平衡
        /// </summary>
        public const string MathErrorBalance = "数据不平衡";

        /// <summary>
        /// 当固定效应模型被需要时，要求随机效应模型
        /// </summary>
        public const string MathErrorModel = "当固定效应模型被需要时，要求随机效应模型";

        /// <summary>
        /// x-值是异常的
        /// </summary>
        public const string MathErrorDistinct = "x-值是异常的";

        /// <summary>
        /// 插值函数在要求的值处有一个极点
        /// </summary>
        public const string MathErrorPole = "插值函数在要求的值处有一个极点";

        /// <summary>
        /// X矩阵的第一列必须全部为1
        /// </summary>
        public const string MathErrorColumn = "X矩阵的第一列必须全部为1";

        /// <summary>
        /// 无效的自由度数
        /// </summary>
        public const string MathErrorFreedom = "无效的自由度数";

        /// <summary>
        /// 概率必须满足条件：p ∈(0, 1)
        /// </summary>
        public const string MathErrorProbability = "概率必须满足 0 < p < 1";

        /// <summary>
        /// 无效的种类或样本个数
        /// </summary>
        public const string MathErrorCategory = "无效的种类或样本个数";

        /// <summary>
        /// 不确定性表有一个负数
        /// </summary>
        public const string MathErrorContingencyTable = "不确定性表有一个负数";

        /// <summary>
        /// beta函数的参数必须满足条件：p ∈(0, 1)
        /// </summary>
        public const string MathErrorBetaFunction = "beta函数的参数必须满足0 < p < 1";

        /// <summary>
        /// 无效的维数或因变量个数
        /// </summary>
        public const string MathErrorDimension = "无效的维数或因变量个数";

        /// <summary>
        /// 被0除
        /// </summary>
        public const string MathErrorDivideByZero = "被0除";

        /// <summary>
        /// 无效的选择
        /// </summary>
        public const string MathErrorInvalidSelection = "无效的选择";

        /// <summary>
        /// 超过最大迭代次数
        /// </summary>
        public const string MathErrorMaxIterations = "超过最大迭代次数";

        /// <summary>
        /// 无效的多项式
        /// </summary>
        public const string MathErrorPolynomial = "无效的多项式";

        /// <summary>
        /// 向量的元素不能全是0
        /// </summary>
        public const string MathErrorZeroVector = "向量的元素不能全是0";

        /// <summary>
        /// IIR滤波器不能正确初始化
        /// </summary>
        public const string MathErrorIIRFilterUninitialized = "IIR滤波器不能正确初始化";

        /// <summary>
        /// 底数小于顶数
        /// </summary>
        public const string MathErrorBaseGETop = "底数小于顶数";

        /// <summary>
        /// 交换位号必须满足条件： (位号) 小于 (样本数)
        /// </summary>
        public const string MathErrorShiftRange = "交换位号必须满足 位号< 样本数";

        /// <summary>
        /// 阶数必须大于或等于0
        /// </summary>
        public const string MathErrorOrderGEZero = "阶数必须大于或等于0";

        /// <summary>
        /// 内部错误
        /// </summary>
        public const string MathErrorInternal = "内部错误";

        /// <summary>
        /// 矩阵不满秩
        /// </summary>
        public const string MathErrorRankDeficient = "矩阵不满秩";


        /// <summary>
        /// 检查整数是否2的幂次
        /// </summary>
        /// <param name="number">被检查的整数</param>
        /// <return>返回true表示是2的幂次</return>
        public static void CheckPowerOfTwo(int number)
        {
            if (!MathBasic.IsPowerOfTwo(number))
            {
                throw new AlgorithmException(MathErrorPowerOfTwo);
            }
        }

        /// <summary>
        /// 检查数组长度必须相等
        /// </summary>
        /// <param name="x">数组1</param>
        /// <param name="y">数组2</param>
        public static void CheckLengthEqual(Array x, Array y)
        {
            if (x.Length != y.Length)
            {
                throw new AlgorithmException(MathErrorEqualSamples);
            }
        }

        /// <summary>
        /// 检查数组长度必须大于0
        /// </summary>
        /// <param name="x">数组</param>
        public static void CheckLengthGTZero(Array x)
        {
            if (x.Length == 0)
            {
                throw new AlgorithmException(MathErrorSamplesGTZero);
            }
        }
    }
}
