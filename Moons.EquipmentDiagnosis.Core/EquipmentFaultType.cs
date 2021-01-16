using System;
using System.Collections.Generic;
using System.Text;

namespace Moons.EquipmentDiagnosis.Core
{
    /// <summary>
    /// 设备故障类型枚举类
    /// </summary>
    public static class EquipmentFaultType
    {
        public static class Generic
        {

            /// <summary>
            /// 轴承座或联轴器不同心
            /// </summary>
            public static readonly string Misalign001 = "-Generic-Misalign001:轴承座或联轴器不同心";
            /// <summary>
            /// 摩擦故障
            /// </summary>
            public static readonly string Rub001 = "-Generic-Rub001:摩擦故障";
            /// <summary>
            /// 轴承存在摩擦或轴瓦间隙不良
            /// </summary>
            public static readonly string Rub002 = "-Generic-Rub002:轴承存在摩擦或轴瓦间隙不良";
            /// <summary>
            /// 联轴端轴承或轴上零部件存在动静摩擦故障，检查联轴端轴承等部位动静安装配合状态
            /// </summary>
            public static readonly string Rub003 = "-Generic-Rub003:联轴端轴承或轴上零部件存在动静摩擦故障，检查联轴端轴承等部位动静安装配合状态";
            /// <summary>
            /// 非联轴端轴承或轴上零部件存在动静摩擦，检查非联轴端轴承等部位动静安装配合状态
            /// </summary>
            public static readonly string Rub004 = "-Generic-Rub004:非联轴端轴承或轴上零部件存在动静摩擦，检查非联轴端轴承等部位动静安装配合状态";
            /// <summary>
            /// 轴承或轴上零部件存在松动摩擦，检查轴承等部位动静安装配合状态
            /// </summary>
            public static readonly string Loose001 = "-Generic-Loose001:轴承或轴上零部件存在松动摩擦，检查轴承等部位动静安装配合状态";
            /// <summary>
            /// 联轴端轴承或轴上其它零部件存在松动或间隙不良
            /// </summary>
            public static readonly string Loose002 = "-Generic-Loose002:联轴端轴承或轴上其它零部件存在松动或间隙不良";
            /// <summary>
            /// 非联轴端轴承或轴上其它零部件存在松动或间隙不良
            /// </summary>
            public static readonly string Loose003 = "-Generic-Loose003:非联轴端轴承或轴上其它零部件存在松动或间隙不良";
            /// <summary>
            /// 电气故障
            /// </summary>
            public static readonly string Electric001 = "-Generic-Electric001:电气故障";
            /// <summary>
            /// 断条故障
            /// </summary>
            public static readonly string Electric002 = "-Generic-Electric002:断条故障";
            /// <summary>
            /// 电机转系偏心或气隙不均或定子松动
            /// </summary>
            public static readonly string Electric003 = "-Generic-Electric003:电机转系偏心或气隙不均或定子松动";
            /// <summary>
            /// 定子短路故障
            /// </summary>
            public static readonly string Electric004 = "-Generic-Electric004:定子短路故障";

            /// <summary>
            /// 台板变形、不平等引起定子偏心
            /// </summary>
            public static readonly string Stator001 = "-Generic-Stator001:台板变形、不平等引起定子偏心";
            /// <summary>
            /// 转子存在不平衡。须通过测量台板、水泥基础振动排除支撑水平刚度不足故障
            /// </summary>
            public static readonly string Imbalance001 = "-Generic-Imbalance001:转子存在不平衡。须通过测量台板、水泥基础振动排除支撑水平刚度不足故障";
            /// <summary>
            /// 转子存在不平衡且支撑刚度不足
            /// </summary>
            public static readonly string Imbalance002 = "-Generic-Imbalance002:转子存在不平衡且支撑刚度不足";
            /// <summary>
            /// 轴承故障，建议加强监测或检查轴承。
            /// </summary>
            public static readonly string Bearing001 = "-Generic-Bearing001:轴承故障，建议加强监测或检查轴承";
            /// <summary>
            /// 基础垂直刚度不足。检查台板、水泥基础以及垫铁等紧固松动或台板不平
            /// </summary>
            public static readonly string Base001 = "-Generic-Base001:基础垂直刚度不足。检查台板、水泥基础以及垫铁等紧固松动或台板不平";
            /// <summary>
            /// 轴承配合间隙不良
            /// </summary>
            public static readonly string Bearing002 = "-Generic-Bearing002:轴承配合间隙不良";
            /// <summary>
            /// 轴承保持架存在碰磨故障
            /// </summary>
            public static readonly string Bearing003 = "-Generic-Bearing003:轴承保持架存在碰磨故障";
            /// <summary>
            /// 轴承滚动体存在损伤故障
            /// </summary>
            public static readonly string Bearing004 = "-Generic-Bearing004:轴承滚动体存在损伤故障";
            /// <summary>
            /// 轴承外圈存在损伤故障
            /// </summary>
            public static readonly string Bearing005 = "-Generic-Bearing005:轴承外圈存在损伤故障";
            /// <summary>
            /// 轴承内圈存在损伤故障
            /// </summary>
            public static readonly string Bearing006 = "-Generic-Bearing006:轴承内圈存在损伤故障";
            /// <summary>
            /// 其他未知故障，需便携式振动分析仪全面测量数据
            /// </summary>
            public static readonly string Unknown001 = "-Generic-Unknown001:其他未知故障，需便携式振动分析仪全面测量数据";
        }

        public static class Motor
        {
        }

        public static class Pump
        {
            
        }
    }
}
