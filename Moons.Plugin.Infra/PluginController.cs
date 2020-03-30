using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Moons.Plugin.Contract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Threading;

namespace Moons.Plugin.Infra
{
    /// <summary>
    /// 插件控制器 demo
    /// https://cloud.tencent.com/developer/article/1520894
    /// </summary>
    public class PluginController : IPlugIn, IDisposable
    {
        private List<Assembly> _defaultAssemblies;
        private AssemblyLoadContext _context;
        private string _pluginName;
        private string _pluginDirectory;
        private volatile IPlugIn _instance;
        private volatile bool _changed;
        private object _reloadLock;
        private FileSystemWatcher _watcher;

        IPlugInContainer IPlugIn.PlugInContainer { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public PluginController(string pluginName, string pluginDirectory)
        {
            _defaultAssemblies = AssemblyLoadContext.Default.Assemblies
                .Where(assembly => !assembly.IsDynamic)
                .ToList();
            _pluginName = pluginName;
            _pluginDirectory = pluginDirectory;
            _reloadLock = new object();
            ListenFileChanges();
        }

        private void ListenFileChanges()
        {
            Action<string> onFileChanged = path =>
            {
                if (Path.GetExtension(path).ToLower() == ".cs")
                    _changed = true;
            };
            _watcher = new FileSystemWatcher();
            _watcher.Path = _pluginDirectory;
            _watcher.IncludeSubdirectories = true;
            _watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName;
            _watcher.Changed += (sender, e) => onFileChanged(e.FullPath);
            _watcher.Created += (sender, e) => onFileChanged(e.FullPath);
            _watcher.Deleted += (sender, e) => onFileChanged(e.FullPath);
            _watcher.Renamed += (sender, e) => { onFileChanged(e.FullPath); onFileChanged(e.OldFullPath); };
            _watcher.EnableRaisingEvents = true;
        }

        private void UnloadPlugin()
        {
            //_instance?.Dispose();
            _instance = null;
            _context?.Unload();
            _context = null;
        }

        private Assembly CompilePlugin()
        {
            var binDirectory = Path.Combine(_pluginDirectory, "bin");
            var dllPath = Path.Combine(binDirectory, $"{_pluginName}.dll");
            if (!Directory.Exists(binDirectory)) Directory.CreateDirectory(binDirectory);
            if (File.Exists(dllPath))
            {
                File.Delete($"{dllPath}.old");
                File.Move(dllPath, $"{dllPath}.old");
            }

            var sourceFiles = Directory.EnumerateFiles(
                _pluginDirectory, "*.cs", SearchOption.AllDirectories);
            var compilationOptions = new CSharpCompilationOptions(
                OutputKind.DynamicallyLinkedLibrary,
                optimizationLevel: OptimizationLevel.Debug);
            var references = _defaultAssemblies
                .Select(assembly => assembly.Location)
                .Where(path => !string.IsNullOrEmpty(path) && File.Exists(path))
                .Select(path => MetadataReference.CreateFromFile(path))
                .ToList();
            var syntaxTrees = sourceFiles
                .Select(p => CSharpSyntaxTree.ParseText(File.ReadAllText(p)))
                .ToList();
            var compilation = CSharpCompilation.Create(_pluginName)
                .WithOptions(compilationOptions)
                .AddReferences(references)
                .AddSyntaxTrees(syntaxTrees);

            var emitResult = compilation.Emit(dllPath);
            if (!emitResult.Success)
            {
                throw new InvalidOperationException(string.Join("\r\n",
                    emitResult.Diagnostics.Where(d => d.WarningLevel == 0)));
            }
            //return _context.LoadFromAssemblyPath(Path.GetFullPath(dllPath));
            using (var stream = File.OpenRead(dllPath))
            {
                var assembly = _context.LoadFromStream(stream);
                return assembly;
            }
        }

        private IPlugIn GetInstance()
        {
            var instance = _instance;
            if (instance != null && !_changed) return instance;

            lock (_reloadLock)
            {
                instance = _instance;
                if (instance != null && !_changed) return instance;

                UnloadPlugin();
                _context = new AssemblyLoadContext(
                    name: $"Plugin-{_pluginName}", isCollectible: true);

                var assembly = CompilePlugin();
                var pluginType = assembly.GetTypes()
                    .First(t => typeof(IPlugIn).IsAssignableFrom(t));
                instance = (IPlugIn)Activator.CreateInstance(pluginType);

                _instance = instance;
                _changed = false;
            }

            return instance;
        }

        public string GetMessage()
        {
            return GetInstance().GetValueByKey("abc") as string;//.GetMessage();
        }

        public void Dispose()
        {
            UnloadPlugin();
            _watcher?.Dispose();
            _watcher = null;
        }

        void IPlugInContainer.SetValueByKey(string key, object value)
        {
        }

        object IPlugInContainer.GetValueByKey(string key)
        {
            return key;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            using (var controller = new PluginController("MyPlugin", "../guest"))
            {
                bool keepRunning = true;
                Console.CancelKeyPress += (sender, e) => {
                    e.Cancel = true;
                    keepRunning = false;
                };
                while (keepRunning)
                {
                    try
                    {
                        Console.WriteLine(controller.GetMessage());
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"{ex.GetType()}: {ex.Message}");
                    }
                    Thread.Sleep(1000);
                }
            }
        }
    }
}
