using System;
using System.Drawing;

namespace AnalysisData.Constants
{
    /// <summary>
    /// 报警类型编号
    /// </summary>
    public static class AlmTypeID
    {
        /// <summary>
        /// 超限
        /// </summary>
        public const int High = 0;

        /// <summary>
        /// 低限
        /// </summary>
        public const int Low = 1;

        /// <summary>
        /// 窗内
        /// </summary>
        public const int InWindow = 2;

        /// <summary>
        /// 窗外
        /// </summary>
        public const int OutWindow = 3;

        /// <summary>
        /// 根据ID获得报警类型名称
        /// </summary>
        /// <param name="almTypeId">报警类型编号</param>
        /// <returns>报警类型名称</returns>
        public static string GetNameByID( int almTypeId )
        {
            switch( almTypeId )
            {
                case Low:
                    return AlmTypeName.Low;

                case InWindow:
                    return AlmTypeName.InWindow;

                case OutWindow:
                    return AlmTypeName.OutWindow;

                default:
                case High:
                    return AlmTypeName.High;
            }
        }
    }

    /// <summary>
    /// 报警类型名称
    /// </summary>
    public static class AlmTypeName
    {
        /// <summary>
        /// 超限
        /// </summary>
        public const string High = "超上限报警";

        /// <summary>
        /// 低限
        /// </summary>
        public const string Low = "超下限报警";

        /// <summary>
        /// 窗内
        /// </summary>
        public const string InWindow = "窗内报警";

        /// <summary>
        /// 窗外
        /// </summary>
        public const string OutWindow = "窗外报警";
    }

    /// <summary>
    /// 报警级别编号
    /// </summary>
    public static class AlmLevelID
    {
        #region 变量和属性

        /// <summary>
        /// 等级0，正常
        /// </summary>
        public const int Level_Normal = 0;

        /// <summary>
        /// 预警
        /// </summary>
        public const int Level_1 = 1;

        /// <summary>
        /// 警告
        /// </summary>
        public const int Level_2 = 2;

        /// <summary>
        /// 报警
        /// </summary>
        public const int Level_3 = 3;

        /// <summary>
        /// 危险
        /// </summary>
        public const int Level_4 = 4;

        /// <summary>
        /// 报警级别ID的数组，升序，不包括正常
        /// </summary>
        private static readonly int[] AlmLevelIDsAsc;

        /// <summary>
        /// 报警级别ID的数组，降序，不包括正常
        /// </summary>
        private static readonly int[] AlmLevelIDsDesc;

        /// <summary>
        /// 报警级别ID的数组，升序，不包括正常
        /// </summary>
        public static int[] AlmLevelIDs_Asc
        {
            get { return AlmLevelIDsAsc; }
        }

        /// <summary>
        /// 报警级别ID的数组，降序，不包括正常
        /// </summary>
        public static int[] AlmLevelIDs_Desc
        {
            get { return AlmLevelIDsDesc; }
        }

        #endregion

        #region 自定义函数

        static AlmLevelID()
        {
            AlmLevelIDsAsc = GetAlmLevelIDs();

            AlmLevelIDsDesc = GetAlmLevelIDs();
            Array.Reverse( AlmLevelIDsDesc );
        }

        /// <summary>
        /// 获得报警级别ID的数组，升序，不包括正常
        /// </summary>
        /// <returns>报警级别ID的数组，升序，不包括正常</returns>
        private static int[] GetAlmLevelIDs()
        {
            return new[] { Level_1, Level_2, Level_3, Level_4 };
        }

        #endregion

        /// <summary>
        /// 根据ID获得报警级别名称
        /// </summary>
        /// <param name="almLevelId">报警级别编号</param>
        /// <returns>报警级别名称</returns>
        public static string GetNameByID( int almLevelId )
        {
            int index = Array.FindIndex( AlmLevelIDsAsc, id => id == almLevelId );
            return -1 < index ? AlmLevelName.AlmLevelNames[index] : AlmLevelName.Level_Normal;
        }

        /// <summary>
        /// 根据ID获得报警级别名称
        /// </summary>
        /// <param name="almLevelID">报警级别编号</param>
        /// <returns>报警级别名称</returns>
        public static Color GetColorByID( int almLevelID )
        {
            int index = Array.FindIndex( AlmLevelIDsAsc, id => id == almLevelID );
            return -1 < index ? AlmLevelColor.AlmLevelColors[index] : AlmLevelColor.AlmColor_Level0;
        }
    }

    /// <summary>
    /// 报警级别名称
    /// </summary>
    public static class AlmLevelName
    {
        #region 变量和属性

        /// <summary>
        /// 等级0，正常
        /// </summary>
        public const string Level_Normal = "正常";

        /// <summary>
        /// 等级1
        /// </summary>
        public const string Level_1 = "警告";

        /// <summary>
        /// 等级2
        /// </summary>
        public const string Level_2 = "危险";

        /// <summary>
        /// 报警等级的名称
        /// </summary>
        private static readonly string[] _almLevelNames = new[] { Level_1, Level_1, Level_2, Level_2 };

        /// <summary>
        /// 报警等级的名称
        /// </summary>
        public static string[] AlmLevelNames
        {
            get { return _almLevelNames; }
        }

        #endregion
    }

    /// <summary>
    /// 报警级别颜色
    /// </summary>
    public static class AlmLevelColor
    {
        #region 变量和属性

        /// <summary>
        /// 正常的颜色
        /// </summary>
        public static Color AlmColor_Level0 = Color.FromArgb( 0xFF, 0xC0, 0xFF, 0xC0 );

        /// <summary>
        /// 预警的颜色
        /// </summary>
        public static readonly Color AlmColor_Level1 = Color.FromArgb(0xFF, 0xFF, 0x9D, 0x1D);

        /// <summary>
        /// 警告的颜色
        /// </summary>
        public static readonly Color AlmColor_Level2 = Color.FromArgb( 0xFF, 0xC0, 0, 0 );

        /// <summary>
        /// 报警等级的颜色
        /// </summary>
        private static readonly Color[] _almLevelColors = new[]
                                                              {
                                                                  AlmColor_Level1, AlmColor_Level1, AlmColor_Level2, AlmColor_Level2
                                                              };

        /// <summary>
        /// 报警等级的颜色
        /// </summary>
        public static Color[] AlmLevelColors
        {
            get { return _almLevelColors; }
        }

        #endregion
    }
}
