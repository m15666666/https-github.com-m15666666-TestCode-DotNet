using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Compilation;
using System.Web.Hosting;

//[assembly: PreApplicationStartMethod(typeof(WebMVCOwinAutofac.IOC.Initializer), "Initialize")]

namespace WebMVCOwinAutofac.IOC
{
    public static class Initializer
    {
        public static void Initialize()
        {
            var pluginFolder = new DirectoryInfo(HostingEnvironment.MapPath("~/plugins"));
            var pluginAssemblyFiles = pluginFolder.GetFiles("*.dll", SearchOption.AllDirectories);
            foreach (var pluginAssemblyFile in pluginAssemblyFiles)
            {
                var asm = Assembly.LoadFrom(pluginAssemblyFile.FullName);
                BuildManager.AddReferencedAssembly(asm);
            }
        }
    }
}