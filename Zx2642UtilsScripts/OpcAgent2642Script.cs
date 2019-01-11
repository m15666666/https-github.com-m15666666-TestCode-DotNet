using System;
using System.Collections.Generic;
using System.Text;

namespace Zx2642UtilsScripts
{
    /// <summary>
    /// 生成Moons.OPC.DA.Agent2642项目OpcConfigurationData.xml文件中的Opc变量名、中间变量名、2642变量名的对应关系
    /// </summary>
    public class OpcAgent2642Script : ScriptBase
    {
        //private string[,] opc_mid_2642varNames = new string[3, 3] {
        //    { "","","" },
        //    { "","","" },
        //    { "","","" },
        //};

        /// <summary>
        /// opc名、中间变量名、2642变量名对应的矩阵
        /// 需要修改
        /// </summary>
        private List<string[]> opc_mid_2642varNames = new List<string[]> {
            //new string[]{ "OpcItemName", "ItemName", "VarName"},
            //new string[]{ "OpcItemName2", "ItemName2", "VarName2"},
            new string[]{ "-6-1同步机负荷端温度转换输出值-", "V3020201", "V3020201"},
new string[]{ "-6-1同步机非负荷端温度转换输出值-", "V3020202", "V3020202"},
new string[]{ "-6-1球磨机副轴负荷端温度转换输出值-", "V3020203", "V3020203"},
new string[]{ "-6-1球磨机副轴非负荷端温度转换输出值-", "V3020204", "V3020204"},
new string[]{ "-6-1球磨机主轴负荷端温度1转换输出值-", "V3020205", "V3020205"},
new string[]{ "-6-1球磨机主轴负荷端温度2转换输出值-", "V3020206", "V3020206"},
new string[]{ "-6-1球磨机主轴负荷端温度3转换输出值-", "V3020207", "V3020207"},
new string[]{ "-6-1球磨机主轴非负荷端温度1转换输出值-", "V3020208", "V3020208"},
new string[]{ "-6-1球磨机主轴非负荷端温度2转换输出值-", "V3020209", "V3020209"},
new string[]{ "-6-1球磨机主轴非负荷端温度3转换输出值-", "V3020210", "V3020210"},
new string[]{ "-6-1球磨机主轴负荷端油流转换输出值-", "V3022201", "V3022201"},

            //new string[]{ "", "", ""},
            //new string[]{ "", "", ""},
        };

        /// <summary>
        /// 生成xml片段
        /// </summary>
        public void GenerateXmlSegments()
        {
            WriteLine($"<!-- 中间名称的组合(DataA)对2642变量名(DataB)的映射 -->");
            WriteLine($"<ItemName2VarName>");
            foreach( var names in opc_mid_2642varNames)
            {
                var opc = names[0];
                var mid = names[1];
                var var2642 = names[2];
                WriteLine($"  <Item2Var>");
                WriteLine($"    <DataA>{mid}</DataA>");
                WriteLine($"    <DataB>{var2642}</DataB>");
                WriteLine($"  </Item2Var>");
            }
            WriteLine($"</ItemName2VarName>");
            WriteLine($"");
            WriteLine($"");
            WriteLine($"<!-- OPC服务器数据项名称(DataA)对中间名称(DataB)的映射 -->");
            WriteLine($"<OpcItemName2ItemName>");
            foreach (var names in opc_mid_2642varNames)
            {
                var opc = names[0];
                var mid = names[1];
                var var2642 = names[2];
                WriteLine($"  <Opc2Item>");
                WriteLine($"    <DataA>{opc}</DataA>");
                WriteLine($"    <DataB>{mid}</DataB>");
                WriteLine($"  </Opc2Item>");
            }
            WriteLine($"</OpcItemName2ItemName>");
            WriteLine($"");
        }
    }
}
