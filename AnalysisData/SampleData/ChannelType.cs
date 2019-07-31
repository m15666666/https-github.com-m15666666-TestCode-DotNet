namespace AnalysisData.SampleData
{
    /// <summary>
    /// 通道类型
    /// </summary>
    public static class ChannelType
    {
        /// <summary>
        /// 动态通道
        /// </summary>
        public const int ChannelType_Dynamic = 1;

        /// <summary>
        /// 静态通道
        /// </summary>
        public const int ChannelType_Static = 2;

        /// <summary>
        /// 转速通道
        /// </summary>
        public const int ChannelType_Rev = 3;

        /// <summary>
        /// 开关量通道
        /// </summary>
        public const int ChannelType_Switch = 5;

        /// <summary>
        /// 积分通道
        /// </summary>
        public const int ChannelType_Int = 11;

        /// <summary>
        /// 动态通道名称
        /// </summary>
        public const string DynamicChannelName = "动态通道";

        /// <summary>
        /// 静态通道名称
        /// </summary>
        public const string StaticChannelName = "静态通道";

        /// <summary>
        /// 转速通道名称
        /// </summary>
        public const string RevChannelName = "转速通道";

        /// <summary>
        /// 开关量通道
        /// </summary>
        public const string SwitchChannelName = "开关量通道";

        /// <summary>
        /// 积分通道
        /// </summary>
        public const string IntChannelName = "积分通道";

        /// <summary>
        /// 动态通道前缀
        /// </summary>
        private const string ChannelTypePrefix_Dynamic = "1_";

        /// <summary>
        /// 静态通道前缀
        /// </summary>
        private const string ChannelTypePrefix_Static = "2_";

        /// <summary>
        /// 转速通道前缀
        /// </summary>
        private const string ChannelTypePrefix_Rev = "3_";

        /// <summary>
        /// 开关量通道前缀
        /// </summary>
        private const string ChannelTypePrefix_Switch = "5_";

        /// <summary>
        /// 积分通道前缀
        /// </summary>
        private const string ChannelTypePrefix_Int = "11_";

        /// <summary>
        /// 通道CD的字符串格式
        /// </summary>
        private const string ChannelCDFormat = "{0}_{1}";

        /// <summary>
        /// 获得通道CD
        /// </summary>
        /// <param name="channelType">通道类型</param>
        /// <param name="channelNumber">通道号</param>
        /// <returns>通道CD</returns>
        public static string GetChannelCD( int channelType, int channelNumber )
        {
            switch( channelType )
            {
                case ChannelType_Dynamic:
                    return GetChannelCD_Dynamic( channelNumber );

                case ChannelType_Static:
                    return GetChannelCD_Static( channelNumber );

                case ChannelType_Switch:
                    return GetChannelCD_Switch( channelNumber );

                case ChannelType_Int:
                    return GetChannelCD_Int( channelNumber );

                default:
                    return GetChannelCD_Rev( channelNumber );
            }
        }

        /// <summary>
        /// 获得动态通道的通道CD
        /// </summary>
        /// <param name="channelNumber">通道号</param>
        /// <returns>动态通道的通道CD</returns>
        public static string GetChannelCD_Dynamic( int channelNumber )
        {
            return ChannelTypePrefix_Dynamic + channelNumber;
        }

        /// <summary>
        /// 获得静态通道的通道CD
        /// </summary>
        /// <param name="channelNumber">通道号</param>
        /// <returns>静态通道的通道CD</returns>
        public static string GetChannelCD_Static( int channelNumber )
        {
            return ChannelTypePrefix_Static + channelNumber;
        }

        /// <summary>
        /// 获得转速通道的通道CD
        /// </summary>
        /// <param name="channelNumber">通道号</param>
        /// <returns>转速通道的通道CD</returns>
        public static string GetChannelCD_Rev( int channelNumber )
        {
            return ChannelTypePrefix_Rev + channelNumber;
        }

        /// <summary>
        /// 获得开关量通道的通道CD
        /// </summary>
        /// <param name="channelNumber">通道号</param>
        /// <returns>开关量通道的通道CD</returns>
        public static string GetChannelCD_Switch( int channelNumber )
        {
            return ChannelTypePrefix_Switch + channelNumber;
        }

        /// <summary>
        /// 获得积分通道的通道CD
        /// </summary>
        /// <param name="channelNumber">通道号</param>
        /// <returns>积分通道的通道CD</returns>
        public static string GetChannelCD_Int( int channelNumber )
        {
            return ChannelTypePrefix_Int + channelNumber;
        }

        /// <summary>
        /// 是否为动态通道CD
        /// </summary>
        /// <param name="channelCD">通道CD</param>
        /// <returns>是否为动态通道CD</returns>
        public static bool IsChannelCD_Dynamic( string channelCD )
        {
            return channelCD.StartsWith( ChannelTypePrefix_Dynamic );
        }

        /// <summary>
        /// 是否为静态通道CD
        /// </summary>
        /// <param name="channelCD">通道CD</param>
        /// <returns>是否为静态通道CD</returns>
        public static bool IsChannelCD_Static( string channelCD )
        {
            return channelCD.StartsWith( ChannelTypePrefix_Static );
        }

        /// <summary>
        /// 是否为转速通道CD
        /// </summary>
        /// <param name="channelCD">通道CD</param>
        /// <returns>是否为转速通道CD</returns>
        public static bool IsChannelCD_Rev( string channelCD )
        {
            return channelCD.StartsWith( ChannelTypePrefix_Rev );
        }

        /// <summary>
        /// 是否为开关量通道CD
        /// </summary>
        /// <param name="channelCD">通道CD</param>
        /// <returns>是否为开关量通道CD</returns>
        public static bool IsChannelCD_Switch( string channelCD )
        {
            return channelCD.StartsWith( ChannelTypePrefix_Switch );
        }

        /// <summary>
        /// 是否为积分通道CD
        /// </summary>
        /// <param name="channelCD">通道CD</param>
        /// <returns>是否为积分通道CD</returns>
        public static bool IsChannelCD_Int( string channelCD )
        {
            return channelCD.StartsWith( ChannelTypePrefix_Int );
        }
    }
}