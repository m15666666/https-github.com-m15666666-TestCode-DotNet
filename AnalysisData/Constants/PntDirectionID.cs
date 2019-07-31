using Moons.Common20.StringResources;

namespace AnalysisData.Constants
{
    /// <summary>
    /// 测点（传感器）方向ID
    /// </summary>
    public class PntDirectionID
    {
        #region 方向常量

        /// <summary>
        /// 测点方向-无
        /// </summary>
        public const byte None = 99;

        /// <summary>
        /// 测点方向-水平
        /// </summary>
        public const byte Horizontal = 0;

        /// <summary>
        /// 测点方向-垂直
        /// </summary>
        public const byte Vertical = 1;

        /// <summary>
        /// 测点方向-轴向
        /// </summary>
        public const byte Axial = 2;

        /// <summary>
        /// 测点方向-45度
        /// </summary>
        public const byte Direction45 = 3;

        /// <summary>
        /// 测点方向-135度
        /// </summary>
        public const byte Direction135 = 4;

        /// <summary>
        /// 测点方向-射线
        /// </summary>
        public const byte Radial = 5;

        /// <summary>
        /// 测点方向-切线
        /// </summary>
        public const byte Tangential = 6;

        #endregion

        #region 方向常量名称

        /// <summary>
        /// 测点方向-无
        /// </summary>
        public static string Name_None
        {
            get
            {
                return
                    StringResourceValues.GetStringResource( GetStringResourceKey( None ), "无" );
            }
        }

        /// <summary>
        /// 测点方向-水平
        /// </summary>
        public static string Name_Horizontal
        {
            get
            {
                return
                    StringResourceValues.GetStringResource( GetStringResourceKey( Horizontal ), "水平" );
            }
        }

        /// <summary>
        /// 测点方向-垂直
        /// </summary>
        public static string Name_Vertical
        {
            get
            {
                return
                    StringResourceValues.GetStringResource( GetStringResourceKey( Vertical ), "垂直" );
            }
        }

        /// <summary>
        /// 测点方向-轴向
        /// </summary>
        public static string Name_Axial
        {
            get
            {
                return
                    StringResourceValues.GetStringResource( GetStringResourceKey( Axial ), "轴向" );
            }
        }

        /// <summary>
        /// 测点方向-45度
        /// </summary>
        public static string Name_Direction45
        {
            get
            {
                return
                    StringResourceValues.GetStringResource( GetStringResourceKey( Direction45 ), "45度" );
            }
        }

        /// <summary>
        /// 测点方向-135度
        /// </summary>
        public static string Name_Direction135
        {
            get
            {
                return
                    StringResourceValues.GetStringResource( GetStringResourceKey( Direction135 ), "135度" );
            }
        }

        /// <summary>
        /// 测点方向-射线
        /// </summary>
        public static string Name_Radial
        {
            get
            {
                return
                    StringResourceValues.GetStringResource( GetStringResourceKey( Radial ), "射线" );
            }
        }

        /// <summary>
        /// 测点方向-切线
        /// </summary>
        public static string Name_Tangential
        {
            get
            {
                return
                    StringResourceValues.GetStringResource( GetStringResourceKey( Tangential ), "切线" );
            }
        }

        /// <summary>
        /// 获得StringResourceKey
        /// </summary>
        /// <param name="pntDirectionNR">测点方向</param>
        /// <returns>StringResourceKey</returns>
        private static string GetStringResourceKey( int pntDirectionNR )
        {
            return "DescriptionItem_PntDirection_" + pntDirectionNR;
        }

        #endregion

        #region 集合常量

        /// <summary>
        /// 用于502的方向ID集合
        /// </summary>
        public static int[] DirectionIDs_502
        {
            get { return new int[] {Radial, Horizontal, Vertical, Axial, Tangential, None}; }
        }

        /// <summary>
        /// 用于502的方向名称集合
        /// </summary>
        public static string[] DirectionNames_502
        {
            get
            {
                return new[]
                           {
                               Name_Radial, Name_Horizontal, Name_Vertical, Name_Axial, Name_Tangential,
                               Name_None
                           };
            }
        }

        #endregion
    }
}