namespace AnalysisData.Constants
{
    /// <summary>
    /// 压缩数据类型编号
    /// </summary>
    public class CompressID
    {
        #region 各个阶段常量

        /// <summary>
        /// 第一(初始)阶段
        /// </summary>
        protected const byte Step1 = 1;

        /// <summary>
        /// 第二(初始)阶段
        /// </summary>
        protected const byte Step2 = 2;

        /// <summary>
        /// 第三(初始)阶段
        /// </summary>
        protected const byte Step3 = 3;

        /// <summary>
        /// 第四(初始)阶段
        /// </summary>
        protected const byte Step4 = 4;

        /// <summary>
        /// 第五(初始)阶段
        /// </summary>
        protected const byte Step5 = 5;

        #endregion

        /// <summary>
        /// 报警事件关联的数据
        /// </summary>
        public const byte AlmEventData = 100;

        /// <summary>
        /// 每天保留一个
        /// </summary>
        public const byte Day = Step4;

        /// <summary>
        /// 30分钟保留一个
        /// </summary>
        public const byte HalfAnHour = Step2;

        /// <summary>
        /// 6小时保留一个
        /// </summary>
        public const byte SixHour = Step3;

        /// <summary>
        /// 未压缩的
        /// </summary>
        public const byte UnCompressed = Step1;
    }

    /// <summary>
    /// 压缩数据类型编号，第一种变化
    /// </summary>
    public class CompressID_1 : CompressID
    {
        /// <summary>
        /// 2小时保留一个
        /// </summary>
        public const byte TwoHour = Step2;

        /// <summary>
        /// 6小时保留一个
        /// </summary>
        public new const byte SixHour = Step3;
    }

    /// <summary>
    /// 压缩(方式)类型编号
    /// </summary>
    public static class CompressTypeID
    {
        /// <summary>
        /// 用于CompressID类型，只用于唐钢，现已不用
        /// </summary>
        public const int Type0 = 0;

        /// <summary>
        /// 用于CompressID_1类型
        /// </summary>
        public const int Type1 = 1;
    }
}