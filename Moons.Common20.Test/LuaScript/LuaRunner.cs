using NLua;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moons.Common20.Test.LuaScript
{
    /// <summary>
    /// 运行lua脚本，用于监测程序运行状态
    /// 
    /// https://github.com/NLua/NLua
    /// </summary>
    public class LuaRunner
    {
        public static LuaRunner Instance { get; set; } = new LuaRunner();

        public List<Plugin.Contract.IPlugIn> InitLuas { get; set; } = new List<Plugin.Contract.IPlugIn>();

        public string RunLua(LuaInputDto value)
        {
            using (Lua lua = new Lua())
            {
                lua.State.Encoding = System.Text.Encoding.UTF8;
                lua.LoadCLRPackage();

                foreach ( var init in InitLuas)
                {
                    init.SetValueByKey("initlua", lua);
                }

                


                //lua.DoString("res = 'Файл'");
                //lua["x"] = 5;
                //global::DataSampler.Config.Logger
                //global::DataSampler.DataSamplerController.Instance.State
                //lua["DataSamplerController"] = global::DataSampler.DataSamplerController.Instance;
                //value.Text = @"
                //import ('DataSampler.Core','DataSampler')
                //res = {}
                //res[1] = DataSamplerController.Instance.State
                //res[2] = Config.Probe
                //";
                lua.DoString(value.Text);
                string ret;
                var res = lua["res"];
                if (res is NLua.LuaTable table)
                {
                    StringBuilder builder = new StringBuilder();
                    var enurator = table.GetEnumerator();
                    while (enurator.MoveNext())
                    {
                        builder.AppendLine($"{enurator.Key}:{enurator.Value}");
                    }
                    ret = builder.ToString();
                }
                else ret = res?.ToString();
                Config.WriteLine(ret);
                return ret;
            }
        }

        public static string a = @"
                import ('DataSampler.Core','DataSampler')
                res = {}
                res[1] = DataSamplerController.Instance.State
                res[2] = Config.Probe
                ";
    }
}
