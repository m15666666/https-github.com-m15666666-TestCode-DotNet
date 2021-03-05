namespace AnalysisData.Constants
{
    /// <summary>
    /// 信号类型ID
    /// </summary>
    public static class SignalTypeID
    {
        #region 变量和属性

        /// <summary>
        /// 速度
        /// </summary>
        public const int Velocity = 101;

        /// <summary>
        /// 加速度
        /// </summary>
        public const int Acceleration = 102;

        /// <summary>
        /// 位移
        /// </summary>
        public const int Displacement = 103;

        /// <summary>
        /// 电流
        /// </summary>
        public const int WaveCurrent = 113;

        /// <summary>
        /// 电压
        /// </summary>
        public const int WaveVoltage = 118;

        /// <summary>
        /// 冲击能量
        /// </summary>
        public const int Shock = 119;

        /// <summary>
        /// 轴位移
        /// </summary>
        public const int ShaftDisplacement = 120;

        /// <summary>
        /// 轴位置
        /// </summary>
        public const int ShaftPosition = 121;

        /// <summary>
        /// 扭距, DataType_ID = 3, UnitType_ID = 103
        /// </summary>
        public const int Torque = 406;

        /// <summary>
        /// 温度
        /// </summary>
        public const int Tempt = 409;

        /// <summary>
        /// 转速
        /// </summary>
        public const int Rev = 410;

        /// <summary>
        /// 电流（数值）
        /// </summary>
        public const int Current = 413;

        /// <summary>
        /// 电压（数值）
        /// </summary>
        public const int Voltage = 418;

        /// <summary>
        /// 流量（数值）
        /// </summary>
        public const int LiquidFlow = 420;

        /// <summary>
        /// 亮度（数值）
        /// </summary>
        public const int Dimming = 431;

        /// <summary>
        /// 功率（数值）
        /// </summary>
        public const int Power = 441;

        /// <summary>
        /// 频率（数值）
        /// </summary>
        public const int Freq = 427;

        /// <summary>
        /// 光强（数值）
        /// </summary>
        public const int Luminous = 451;

        /// <summary>
        /// 压力（数值）
        /// </summary>
        public const int Pressure = 405;

        /// <summary>
        /// 无量纲（数值）
        /// </summary>
        public const int None = 0;

        #endregion

        #region 判断

        /// <summary>
        /// 是否为位移
        /// </summary>
        /// <returns>是否为时间位移</returns>
        public static bool IsDisplacemeng( int signalTypeId )
        {
            return signalTypeId == Displacement;
        }


        /// <summary>
        /// 是否为轴位移
        /// </summary>
        /// <returns>是否为轴位移</returns>
        public static bool IsShaftDisplacement( int signalTypeId )
        {
            return signalTypeId == ShaftDisplacement;
        }

        /// <summary>
        /// 是否为轴位置
        /// </summary>
        /// <returns>是否为轴位置</returns>
        public static bool IsShaftPosition( int signalTypeId )
        {
            return signalTypeId == ShaftPosition;
        }

        #endregion
    }
}