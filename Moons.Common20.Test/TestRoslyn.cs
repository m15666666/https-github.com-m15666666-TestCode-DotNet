using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using Moons.Common20.Test.LuaScript;
using Moons.Plugin.Contract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Moons.Common20.Test
{
    /// <summary>
    ///  测试roslyn编译引擎
    ///  
    /// https://github.com/dotnet/roslyn/wiki/Scripting-API-Samples
    /// https://www.cnblogs.com/TianFang/p/6939723.html
    ///  https://blog.csdn.net/WPwalter/article/details/80545207
    /// 动态代理：https://www.cnblogs.com/liuxiaoji/p/9875826.html
    /// 
    /// .NET Core 3.0 可卸载程序集原理简析:https://www.cnblogs.com/zkweb/p/11516062.html
    /// 使用 .NET Core 3.0 的 AssemblyLoadContext 实现插件热加载:https://cloud.tencent.com/developer/article/1520894
    /// 
    /// </summary>
    public static class TestRoslyn
    {
        /// <summary>
        /// 网上的例子
        /// </summary>
        public static void SyntaxTreeDemo1()
        {
            string script = 
                @"
                using System;
                namespace RoslynCompileSample
                {
                    public class Writer
                    {
                        public void Write(string message)
                        {
                            System.Console.WriteLine(message);
                        }
                    }
                }";
            SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(script); 
            string assemblyName = Path.GetRandomFileName();
            //  
            //MetadataReference[] references = new MetadataReference[]
            //{
            //    CreateMetadataReference(typeof(object)),
            //    CreateMetadataReference(typeof(Console)),
            //    CreateMetadataReference(typeof(Enumerable))
            //};

            //Dictionary<string, MetadataReference> map = new Dictionary<string, MetadataReference>();
            //CreateMetadataReference(map,
            //    typeof(Object),
            //    typeof(string),
            //    typeof(decimal),
            //    typeof(System.Console),
            //    typeof(Enumerable),
            //    typeof(System.Runtime.AmbiguousImplementationException)
            //    );
            //MetadataReference[] references = map.Values.ToArray();

            var references = AppDomain.CurrentDomain.GetAssemblies().Select(x => MetadataReference.CreateFromFile(x.Location));

            // 把.NET Core 运行时用到的那些引用都加入到引用了。
            // 加入引用是必要的，不然连 object 类型都是没有的
            //CSharpScript
            CSharpCompilation compilation = CSharpCompilation.Create(
                assemblyName,
                syntaxTrees: new[] { syntaxTree },
                //references: references,
                options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary)
                )
                .AddReferences(AppDomain.CurrentDomain.GetAssemblies().Select(x => MetadataReference.CreateFromFile(x.Location)))
                ;
            using (var ms = new MemoryStream())
            {
                EmitResult result = compilation.Emit(ms);
                ms.Seek(0, SeekOrigin.Begin);
                Assembly assembly = Assembly.Load(ms.ToArray());

                Type type = assembly.GetType("RoslynCompileSample.Writer");
                object obj = Activator.CreateInstance(type);
                type.InvokeMember("Write",
                BindingFlags.Default | BindingFlags.InvokeMethod,
                null,
                obj,
                new object[] { "Hello World" });
            }
        }

        /// <summary>
        /// 测试基本的PlugIn接口实现
        /// </summary>
        public static void SyntaxTreeDemo2()
        {
            string script =
                @"
using System;
using Moons.Plugin.Contract;
namespace RoslynCompileSample
{
    public class PlusIn001 : Moons.Plugin.Contract.IPlugIn
    {
        public IPlugInContainer PlugInContainer { get ; set; }

        public object GetValueByKey(string key)
        {
            Console.WriteLine($""key: { key}"");
            return null;
        }

        public void SetValueByKey(string key, object value)
        {
            Console.WriteLine($""{ key}:{value}"");
        }
    }
}";
            SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(script);
            string assemblyName = Path.GetRandomFileName();

            var references = AppDomain.CurrentDomain.GetAssemblies().Select(x => MetadataReference.CreateFromFile(x.Location));

            // 把.NET Core 运行时用到的那些引用都加入到引用了。
            // 加入引用是必要的，不然连 object 类型都是没有的
            //CSharpScript
            CSharpCompilation compilation = CSharpCompilation.Create(
                assemblyName,
                syntaxTrees: new[] { syntaxTree },
                //references: references,
                options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary)
                )
                .AddReferences(AppDomain.CurrentDomain.GetAssemblies().Select(x => MetadataReference.CreateFromFile(x.Location)))
                ;
            using (var ms = new MemoryStream())
            {
                EmitResult result = compilation.Emit(ms);
                ms.Seek(0, SeekOrigin.Begin);
                Assembly assembly = Assembly.Load(ms.ToArray());

                Type type = assembly.GetType("RoslynCompileSample.PlusIn001");
                object obj = Activator.CreateInstance(type);
                var plusin = obj as Moons.Plugin.Contract.IPlugIn;
                plusin.SetValueByKey("key1", "hello plusin");
                var r = plusin.GetValueByKey("key1");
            }
        }

        public static void SyntaxTreeDemo3()
        {
            string script = File.ReadAllText("luascript/PlugInDemo1.cs");

            SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(script);
            string assemblyName = Path.GetRandomFileName();

            var references = AppDomain.CurrentDomain.GetAssemblies().Select(x => MetadataReference.CreateFromFile(x.Location));

            // 把.NET Core 运行时用到的那些引用都加入到引用了。
            // 加入引用是必要的，不然连 object 类型都是没有的
            //CSharpScript
            CSharpCompilation compilation = CSharpCompilation.Create(
                assemblyName,
                syntaxTrees: new[] { syntaxTree },
                //references: references,
                options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary)
                )
                .AddReferences(AppDomain.CurrentDomain.GetAssemblies().Select(x => MetadataReference.CreateFromFile(x.Location)))
                ;
            using (var ms = new MemoryStream())
            {
                EmitResult result = compilation.Emit(ms);
                ms.Seek(0, SeekOrigin.Begin);
                Assembly assembly = Assembly.Load(ms.ToArray());
                Type type = assembly.GetExportedTypes()[0];

                //Type type = assembly.GetType("RoslynCompileSample.PlusIn001");
                object obj = Activator.CreateInstance(type);
                var plusin = obj as Moons.Plugin.Contract.IPlugIn;
                plusin.SetValueByKey("key1", "hello plusin");
                var r = plusin.GetValueByKey("key1");

                LuaRunner.Instance.InitLuas.Add(plusin);

                string luascript_1 = @"
                res = {}
                res[1] = init
                res[2] = lua
                res[3] = DetectedDemo1.Probe
                res[4] = DetectedDemo1.Probe:ToString()
                res[5] = DetectedDemo1
                res[6] = DetectedDemo1:ToString()
                
                ";
                string luascript_2 = @"
                import ('DataSampler.Core','DataSampler')
                res = {}
                res[1] = DataSamplerController.Instance.State
                res[2] = Config.Probe
                ";
                var dto = new LuaInputDto { Text = luascript_1 };
                var s = LuaRunner.Instance.RunLua(dto);
            }
        }

        #region 辅助方法

        private static void CreateMetadataReference(Dictionary<string, MetadataReference> map, params Type[] types )
        {
            foreach (var type in types)
            {
                var location = type.Assembly.Location;
                if (map.ContainsKey(location)) continue;
                map.Add(location,
                    MetadataReference.CreateFromFile(location)
                    );
                
            }
        }

        private static MetadataReference CreateMetadataReference(Type type)
        {
            var location = type.Assembly.Location;
            return MetadataReference.CreateFromFile(location);
        }

        #endregion
    }

    public class d1 : Moons.Plugin.Contract.IPlugIn
    {
        public IPlugInContainer PlugInContainer { get ; set; }

        public object GetValueByKey(string key)
        {
            Console.WriteLine($"key:{key}");
            return null;
        }

        public void SetValueByKey(string key, object value)
        {
            Console.WriteLine($"{key}:{value}");
        }
    }
}
