using Moons.Common20;
using Moons.EquipmentDiagnosis.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moons.EquipmentDiagnosis.Core
{
    /// <summary>
    /// 配置工具类
    /// </summary>
    public static class Config
    {
        /// <summary>
        /// 记录诊断日志
        /// </summary>
        public static ILogNet Logger { get; set; } = TraceUtils.Logger;


        /// <summary>
        /// 诊断上下文
        /// </summary>
        public static IEquipmentDiagnosisContext Context { get; set; } 

    }
}
