namespace AnalysisAlgorithm
{
    /// <summary>
    /// ��������
    /// </summary>
    public enum WaveType
    {
        /// <summary>
        /// ʱ�䲨��
        /// </summary>
        TimeWave,

        /// <summary>
        /// Ƶ��
        /// </summary>
        Spectrum,
    } ;

    /// <summary>
    /// ��������
    /// </summary>
    public enum VibQtyType
    {
        /// <summary>
        /// ����
        /// </summary>
        Force,

        /// <summary>
        /// ��λ��
        /// </summary>
        Displacement,

        /// <summary>
        /// ���ٶ�
        /// </summary>
        Velocity,

        /// <summary>
        /// �񶯼��ٶ�
        /// </summary>
        Acceleration,

        /// <summary>
        /// ͨ�õķ�ʽ������׼ֵΪ1��ϵ��Ϊ10
        /// </summary>
        Generic,
    } ;
}