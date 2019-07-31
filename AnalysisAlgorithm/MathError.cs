using System;

namespace AnalysisAlgorithm
{
    /// <summary>
    /// ���������Ϣ�ࡣ��һ������������ʱ���㷨����ⷵ��һ��������Ϣ��
    /// </summary>
    public static class MathError
    {
        /// <summary>
        /// �������б�������ͬ����
        /// </summary>
        public const string MathErrorEqualSamples = "�������б�������ͬ����";

        /// <summary>
        /// �����ĸ����������0
        /// </summary>
        public const string MathErrorSamplesGTZero = "�����ĸ����������0";

        /// <summary>
        /// �����ĸ���������ڻ����0
        /// </summary>
        public const string MathErrorSamplesGEZero = "�����ĸ���������ڻ����0";

        /// <summary>
        /// �����ĸ����������2
        /// </summary>
        public const string MathErrorSamplesGETwo = " �����ĸ����������2";

        /// <summary>
        /// �����ĸ����������3
        /// </summary>
        public const string MathErrorSamplesGEThree = "�����ĸ����������3";

        /// <summary>
        /// ��������ĳ���û������ָ��������
        /// </summary>
        public const string MathErrorArraySize = "��������ĳ���û������ָ��������";

        /// <summary>
        /// ��������ĳ��ȱ�����һ��2���ݴ�
        /// </summary>
        public const string MathErrorPowerOfTwo = "��������ĳ��ȱ�����һ��2���ݴ�";

        /// <summary>
        /// �������������������������� �� (0, ������)
        /// </summary>
        public const string MathErrorCycles = "�������������������� 0< ������ < ������";

        /// <summary>
        /// �ӳٺͿ�ȱ�������������(�ӳ� + ���) С�� (������)
        /// </summary>
        public const string MathErrorDelayWidth = "�ӳٺͿ�ȱ������������� (�ӳ� + ���) < ������";

        /// <summary>
        /// dt��dx�������0
        /// </summary>
        public const string MathErrorDtGTZero = "dt��dx�������0";

        /// <summary>
        /// �����±��������������index �� [0,������) 
        /// </summary>
        public const string MathErrorIndexLTSamples = "�����±�������� 0 <= ��� < ������";

        /// <summary>
        /// �����±�ͳ��ȱ�������������(index + len) �� [0,������) 
        /// </summary>
        public const string MathErrorIndexLength = "��źͳ��ȱ�������0 <= (��� + ����) < ������";

        /// <summary>
        /// ����ֵ������ڻ��������ֵ
        /// </summary>
        public const string MathErrorUpperGELower = "����ֵ������ڻ��������ֵ";

        /// <summary>
        /// ����ֵ�����������ֵ
        /// </summary>
        public const string MathErrorUpperGTLower = "��ֹƵ������ֵ�����������ֵ";

        /// <summary>
        /// ����Ƶ��fc��������������fc �� [0,fs/2]
        /// </summary>
        public const string MathErrorNyquist = "����Ƶ��fc��������0 <= fc <= (fs/2)";

        /// <summary>
        /// �����������0
        /// </summary>
        public const string MathErrorOrderGTZero = "�����������0";

        /// <summary>
        /// ʮ������ֵ��������������ʮ������ ��(0, ������]
        /// </summary>
        public const string MathErrorDecimationFactor = "ʮ������ֵ��������0 < ʮ������ <= ������";

        /// <summary>
        /// ��Ч�Ĵ���ָ��
        /// </summary>
        public const string MathErrorBandSpec = "��Ч�Ĵ���ָ��";

        /// <summary>
        /// �Ʋ����ȱ������0
        /// </summary>
        public const string MathErrorRippleGTZero = "�Ʋ����ȱ������0";

        /// <summary>
        /// ˥���������0
        /// </summary>
        public const string MathErrorAttenGTZero = "˥���������0";

        /// <summary>
        /// ˥����������Ʋ���ֵ
        /// </summary>
        public const string MathErrorAttenGTRipple = "˥����������Ʋ���ֵ";

        /// <summary>
        /// �˲�����Ʋ�������ָ���Ĳ�������
        /// </summary>
        public const string MathErrorEqRippleDesign = "�˲�����Ʋ�������ָ���Ĳ�������";

        /// <summary>
        /// �˲���ϵ����������������
        /// </summary>
        public const string MathErrorEvenSize = "�˲���ϵ����������������";

        /// <summary>
        /// �˲���ϵ������������ż��
        /// </summary>
        public const string MathErrorOddSize = "�˲���ϵ������������ż��";

        /// <summary>
        /// �ڶ������������ȫΪ������û��0
        /// </summary>
        public const string MathErrorMixedSign = "�ڶ������������ȫΪ������û��0";

        /// <summary>
        /// ���鳤�ȱ�����ڽ���
        /// </summary>
        public const string MathErrorSizeGTOrder = "���鳤�ȱ�����ڽ���";

        /// <summary>
        /// ������������һ������
        /// </summary>
        public const string MathErrorSquareMatrix = "������������һ������";

        /// <summary>
        /// �������������ģ��������޽�
        /// </summary>
        public const string MathErrorSingularMatrix = "�������������ģ��������޽�";

        /// <summary>
        /// ��θ�������������ķ�Χ
        /// </summary>
        public const string MathErrorLevels = "��θ�������������ķ�Χ";

        /// <summary>
        /// FFT���ȳ���������ķ�Χ2^30
        /// </summary>
        public const string MathErrorFFTSize = "FFT���ȳ���������ķ�Χ2^30";

        /// <summary>
        /// ���ص�ˮƽ����������ķ�Χ
        /// </summary>
        public const string MathErrorFactor = "���ص�ˮƽ����������ķ�Χ";

        /// <summary>
        /// ̫�ٵĹ۲���
        /// </summary>
        public const string MathErrorObservations = "̫�ٵĹ۲���";

        /// <summary>
        /// ���ݴ���
        /// </summary>
        public const string MathErrorData = "���ݴ���";

        /// <summary>
        /// ���ݲ�ƽ��
        /// </summary>
        public const string MathErrorBalance = "���ݲ�ƽ��";

        /// <summary>
        /// ���̶�ЧӦģ�ͱ���Ҫʱ��Ҫ�����ЧӦģ��
        /// </summary>
        public const string MathErrorModel = "���̶�ЧӦģ�ͱ���Ҫʱ��Ҫ�����ЧӦģ��";

        /// <summary>
        /// x-ֵ���쳣��
        /// </summary>
        public const string MathErrorDistinct = "x-ֵ���쳣��";

        /// <summary>
        /// ��ֵ������Ҫ���ֵ����һ������
        /// </summary>
        public const string MathErrorPole = "��ֵ������Ҫ���ֵ����һ������";

        /// <summary>
        /// X����ĵ�һ�б���ȫ��Ϊ1
        /// </summary>
        public const string MathErrorColumn = "X����ĵ�һ�б���ȫ��Ϊ1";

        /// <summary>
        /// ��Ч�����ɶ���
        /// </summary>
        public const string MathErrorFreedom = "��Ч�����ɶ���";

        /// <summary>
        /// ���ʱ�������������p ��(0, 1)
        /// </summary>
        public const string MathErrorProbability = "���ʱ������� 0 < p < 1";

        /// <summary>
        /// ��Ч���������������
        /// </summary>
        public const string MathErrorCategory = "��Ч���������������";

        /// <summary>
        /// ��ȷ���Ա���һ������
        /// </summary>
        public const string MathErrorContingencyTable = "��ȷ���Ա���һ������";

        /// <summary>
        /// beta�����Ĳ�����������������p ��(0, 1)
        /// </summary>
        public const string MathErrorBetaFunction = "beta�����Ĳ�����������0 < p < 1";

        /// <summary>
        /// ��Ч��ά�������������
        /// </summary>
        public const string MathErrorDimension = "��Ч��ά�������������";

        /// <summary>
        /// ��0��
        /// </summary>
        public const string MathErrorDivideByZero = "��0��";

        /// <summary>
        /// ��Ч��ѡ��
        /// </summary>
        public const string MathErrorInvalidSelection = "��Ч��ѡ��";

        /// <summary>
        /// ��������������
        /// </summary>
        public const string MathErrorMaxIterations = "��������������";

        /// <summary>
        /// ��Ч�Ķ���ʽ
        /// </summary>
        public const string MathErrorPolynomial = "��Ч�Ķ���ʽ";

        /// <summary>
        /// ������Ԫ�ز���ȫ��0
        /// </summary>
        public const string MathErrorZeroVector = "������Ԫ�ز���ȫ��0";

        /// <summary>
        /// IIR�˲���������ȷ��ʼ��
        /// </summary>
        public const string MathErrorIIRFilterUninitialized = "IIR�˲���������ȷ��ʼ��";

        /// <summary>
        /// ����С�ڶ���
        /// </summary>
        public const string MathErrorBaseGETop = "����С�ڶ���";

        /// <summary>
        /// ����λ�ű������������� (λ��) С�� (������)
        /// </summary>
        public const string MathErrorShiftRange = "����λ�ű������� λ��< ������";

        /// <summary>
        /// ����������ڻ����0
        /// </summary>
        public const string MathErrorOrderGEZero = "����������ڻ����0";

        /// <summary>
        /// �ڲ�����
        /// </summary>
        public const string MathErrorInternal = "�ڲ�����";

        /// <summary>
        /// ��������
        /// </summary>
        public const string MathErrorRankDeficient = "��������";


        /// <summary>
        /// ��������Ƿ�2���ݴ�
        /// </summary>
        /// <param name="number">����������</param>
        /// <return>����true��ʾ��2���ݴ�</return>
        public static void CheckPowerOfTwo(int number)
        {
            if (!MathBasic.IsPowerOfTwo(number))
            {
                throw new AlgorithmException(MathErrorPowerOfTwo);
            }
        }

        /// <summary>
        /// ������鳤�ȱ������
        /// </summary>
        /// <param name="x">����1</param>
        /// <param name="y">����2</param>
        public static void CheckLengthEqual(Array x, Array y)
        {
            if (x.Length != y.Length)
            {
                throw new AlgorithmException(MathErrorEqualSamples);
            }
        }

        /// <summary>
        /// ������鳤�ȱ������0
        /// </summary>
        /// <param name="x">����</param>
        public static void CheckLengthGTZero(Array x)
        {
            if (x.Length == 0)
            {
                throw new AlgorithmException(MathErrorSamplesGTZero);
            }
        }
    }
}
