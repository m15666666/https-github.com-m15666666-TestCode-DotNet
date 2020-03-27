using Moons.Common20.Reflection;
using Natasha;
using Natasha.Operator;
using System;
using System.Text;

namespace Moons.Natasha.ObjectMapper
{
    /// <summary>
    /// 对象复制实用工具类
    /// 
    /// https://github.com/dotnetcore/Natasha
    /// https://natasha.dotnetcore.xyz/
    /// https://natasha.dotnetcore.xyz/zh/method/fast-method.html
    /// </summary>
    /// <typeparam name="TSource">源对象类型</typeparam>
    /// <typeparam name="TDestination">目标对象类型</typeparam>
    public static class ObjectMapperUtils<TSource, TDestination> where TDestination : new()
    {
        public delegate void MapDelegate(TSource arg1, TDestination arg2);
        private static readonly Action<TSource, TDestination> _instance = GenerateConvertFunction();
        private static Action<TSource, TDestination> GenerateConvertFunction()
        {
            var p1 = "arg1";
            var p2 = "arg2";
            StringBuilder body = new StringBuilder();
            var sourceProperties = ReflectionUtils.GetPublicCanReadWriteProperties(typeof(TSource));
            foreach (var pD in ReflectionUtils.GetPublicCanReadWriteProperties(typeof(TDestination)))
            {
                foreach (var pS in sourceProperties)
                {
                    if (!pS.Name.Equals(pD.Name, StringComparison.OrdinalIgnoreCase)) continue;
                    if (pS.PropertyType != pD.PropertyType) break;
                    body.AppendLine($"{p2}.{pD.Name} = {p1}.{pS.Name};");
                    break;
                }
            }

            //var script = FastMethodOperator.Default()
            //   .Param<TSource>(p1)
            //   .Param<TDestination>(p2)
            //   .MethodBody(body.ToString())
            //   //.Return<string>()
            //   .Return()
            //   .Builder()
            //   .MethodScript;
            //var a = DelegateOperator<MapDelegate>.Delegate(body.ToString());


            var a = NDomain.Default().Action<TSource, TDestination>(body.ToString());
            return (Action<TSource, TDestination>)a;
        }

        public static void MapByExpressTree(TSource source, TDestination destination) 
        {
            _instance(source, destination);
        }

        public static TDestination MapByExpressTree(TSource source)
        {
            TDestination destination = new TDestination();
            _instance(source, destination);
            return destination;
        }
    }
}
