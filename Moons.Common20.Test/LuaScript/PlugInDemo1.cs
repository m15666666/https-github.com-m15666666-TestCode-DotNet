using Moons.Plugin.Contract;
using NLua;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moons.Common20.Test.LuaScript
{
    public class PlugInDemo1 : Plugin.Contract.IPlugIn
    {
        public PlugInDemo1() { }

        public IPlugInContainer PlugInContainer { get; set; }

        public object GetValueByKey(string key)
        {
            Config.WriteLine($"PlugInDemo1.GetValueByKey,{key}");
            Config.WriteLine(Environment.StackTrace);
            return null;
        }

        public void SetValueByKey(string key, object value)
        {
            switch (key)
            {
                case "initlua":
                    if(value is Lua lua)
                    {
                        lua["init"] = this;
                        lua["lua"] = lua;
                        lua["DetectedDemo1"] = DetectedDemo1.Instance;
                    }
                    break;
            }

            Config.WriteLine($"PlugInDemo1.SetValueByKey,{key}:{value}");
            Config.WriteLine(Environment.StackTrace);
        }
    }
}
