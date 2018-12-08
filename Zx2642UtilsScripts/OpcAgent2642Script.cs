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
            new string[]{ "OpcItemName", "ItemName", "VarName"},
            new string[]{ "OpcItemName2", "ItemName2", "VarName2"},
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
