using Moons.Plugin.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Moons.Plugin.Infra
{
    /// <summary>
    /// 插件加载器
    /// </summary>
    public class PlugInLoader : IDisposable
    {
        private readonly AssemblyLoader _assemblyLoader = new AssemblyLoader();


        /// <summary>
        /// 加载插件
        /// </summary>
        /// <param name="loadAssemblyDto">LoadAssemblyDto</param>
        /// <param name="releaseBefore">是否释放之前加载的插件</param>
        /// <returns>插件对象</returns>
        public IPlugIn LoadPlugIn(LoadAssemblyDto loadAssemblyDto, bool releaseBefore)
        {
            var assembly = _assemblyLoader.LoadAssembly(loadAssemblyDto, releaseBefore);
            var pluginType = assembly.GetTypes()
                    .First(t => typeof(IPlugIn).IsAssignableFrom(t));
            return (IPlugIn)Activator.CreateInstance(pluginType);
        }

        public void Dispose()
        {
            _assemblyLoader?.Dispose();
        }
    }
}
