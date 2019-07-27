namespace Moons.Common20
{
    /// <summary>
    /// 用于转换的数据类型
    /// </summary>
    public enum DataType4Convert
    {
        /// <summary>
        /// 字符
        /// </summary>
        String = 1,

        /// <summary>
        /// 日期
        /// </summary>
        DateTime = 2,

        /// <summary>
        /// 整型
        /// </summary>
        Int32 = 3,

        /// <summary>
        /// 长整型
        /// </summary>
        Int64 = 4,

        /// <summary>
        /// 双精度小数
        /// </summary>
        Double = 5,

        /// <summary>
        /// 浮点数
        /// </summary>
        Float = 6,

        /// <summary>
        /// 只限字符和数字
        /// </summary>
        CharAndNum = 7,

        /// <summary>
        /// 只限邮件地址
        /// </summary>
        Email = 8,

        /// <summary>
        /// 只限字符和数字和中文
        /// </summary>
        CharAndNumAndChinese = 9
    }
}