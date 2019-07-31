using Moons.Common20;

namespace AnalysisData
{
    /// <summary>
    ///     表示设备状态的常量
    /// </summary>
    public static class MoonsOpcFlags
    {
        /// <summary>
        ///     前缀
        /// </summary>
        public static string Moons_Opc_Prefix { get; set; }
            = "Moons_Opc_".ToUpper();

        /// <summary>
        ///     设备已经停止运行，不要采集数据
        /// </summary>
        public static string Moons_Opc_MachSampleStop { get; set; }
            = Moons_Opc_Prefix + "MachSampleStop".ToUpper();

        /// <summary>
        ///     设备已经开始运行，采集数据
        /// </summary>
        public static string Moons_Opc_MachSampleStart { get; set; }
            = Moons_Opc_Prefix + "MachSampleStart".ToUpper();

        /// <summary>
        ///     将字符串转换为统一的大小写格式
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string GetMoonsOpcFlags( string text )
        {
            return StringUtils.ToUpper( text );
        }

        /// <summary>
        ///     是否为表示设备状态的常量
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool IsMoonsOpcFlags( string text )
        {
            return string.IsNullOrEmpty( text ) ? false : text.StartsWith( Moons_Opc_Prefix );
        }
    }
}