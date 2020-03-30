using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;

namespace Moons.Plugin.Infra
{
    /// <summary>
    /// 程序集加载器
    /// </summary>
    public class AssemblyLoader : IDisposable
    {
        private readonly List<Assembly> _defaultAssemblies;
        private AssemblyLoadContext _context;
        private readonly object _reloadLock = new object();

        public AssemblyLoader()
        {
            _defaultAssemblies = AssemblyLoadContext.Default.Assemblies
                .Where(assembly => !assembly.IsDynamic)
                .ToList();
        }

        public Assembly LoadAssembly(LoadAssemblyDto loadAssemblyDto, bool releaseBefore)
        {
            if (releaseBefore) UnloadContext();

            switch (loadAssemblyDto.LoadAssemblyMethodType)
            {
                case LoadAssemblyMethodType.DLLBytes:
                    return LoadAssembly(loadAssemblyDto.DLLBytes);

                case LoadAssemblyMethodType.DLLBytesBase64:
                    return LoadAssembly(Convert.FromBase64String(loadAssemblyDto.DLLBytesBase64));

                case LoadAssemblyMethodType.DLLFilePath:
                    using (var stream = File.OpenRead(loadAssemblyDto.DLLFilePath))
                    {
                        return LoadAssembly(stream);
                    }

                case LoadAssemblyMethodType.SourceCodes:
                    return LoadAssembly(CompileToBytes(loadAssemblyDto.SourceCodes,null));
            }
            throw new ArgumentException($"{nameof(loadAssemblyDto.LoadAssemblyMethodType)}:{loadAssemblyDto.LoadAssemblyMethodType}");
        }

        /// <summary>
        /// 将源代码编译为程序集
        /// </summary>
        /// <param name="sourceCodes">源代码</param>
        /// <returns>程序集字节数组</returns>
        public byte[] CompileToBytes(IEnumerable<string> sourceCodes, string assemblyName )
        {
            assemblyName ??= GetAssemblyName();
            var compilationOptions = new CSharpCompilationOptions(
                OutputKind.DynamicallyLinkedLibrary,
                optimizationLevel: OptimizationLevel.Debug);
            var references = _defaultAssemblies
                .Select(assembly => assembly.Location)
                .Where(path => !string.IsNullOrEmpty(path) && File.Exists(path))
                .Select(path => MetadataReference.CreateFromFile(path))
                .ToList();
            var syntaxTrees = sourceCodes
                .Select(p => CSharpSyntaxTree.ParseText(p))
                .ToList();
            var compilation = CSharpCompilation.Create(assemblyName)
                .WithOptions(compilationOptions)
                .AddReferences(references)
                .AddSyntaxTrees(syntaxTrees);

            using (var stream = new MemoryStream())
            {
                var emitResult = compilation.Emit(stream);
                if (!emitResult.Success)
                {
                    throw new InvalidOperationException(string.Join("\r\n",
                        emitResult.Diagnostics.Where(d => d.WarningLevel == 0)));
                }
                return stream.ToArray();
            }
        }

        private string GetAssemblyName()
        {
            return Path.GetRandomFileName();
        }

        private Assembly LoadAssembly(byte[] bytes)
        {
            using (var stream = new MemoryStream(bytes))
            {
                return LoadAssembly(stream);
            }
        }
        private Assembly LoadAssembly(Stream stream)
        {
            if (_context == null)
            {
                lock (_reloadLock)
                {
                    if (_context == null) _context = new AssemblyLoadContext(name: null, isCollectible: true);
                }
            }
            var assembly = _context.LoadFromStream(stream);
            return assembly;
        }

        private void UnloadContext()
        {
            lock (_reloadLock)
            {
                _context?.Unload();
                _context = null;
            }
        }

        public void Dispose()
        {
            UnloadContext();
        }
    }
}
