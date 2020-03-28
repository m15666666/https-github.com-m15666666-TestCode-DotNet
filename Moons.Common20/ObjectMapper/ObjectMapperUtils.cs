using Moons.Common20.Reflection;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Moons.Common20.ObjectMapper
{
    /// <summary>
    /// 对象复制实用工具类
    /// 
    /// 使用表达式树：https://www.cnblogs.com/lsgsanxiao/p/8205096.html
    /// ILSpy设置，查看反编译的表达式树：https://blog.csdn.net/weixin_30699955/article/details/98230610
    /// </summary>
    public static class ObjectMapperUtils
    {
        public static TDestination MapByReflection<TSource, TDestination>(TSource source) where TDestination : new()
        {
            TDestination ret = new TDestination();
            MapByReflection(source, ret);
            return ret;
        }

        public static void MapByReflection<TSource, TDestination>(TSource source, TDestination destination)
        {
            var sourceProperties = ReflectionUtils.GetPublicCanReadWriteProperties(typeof(TSource));
            foreach (var pD in ReflectionUtils.GetPublicCanReadWriteProperties(typeof(TDestination)))
            {
                foreach( var pS in sourceProperties)
                {
                    if (!pS.Name.Equals(pD.Name, StringComparison.OrdinalIgnoreCase)) continue;
                    if (pS.PropertyType != pD.PropertyType) break;
                    var v = ReflectionUtils.GetPropertyValue(pS, source);
                    if (v != null) ReflectionUtils.SetPropertyValue(pD, destination, v);
                    break;
                }
            }
        }
        private static readonly ConcurrentDictionary<string, object> _expressTreeCache = new ConcurrentDictionary<string, object>();
        public static TDestination MapByExpressTree<TSource, TDestination>(TSource source)
        {
            string key = string.Format("trans_exp_{0}_{1}", typeof(TSource).FullName, typeof(TDestination).FullName);
            if (!_expressTreeCache.ContainsKey(key))
            {
                ParameterExpression parameterExpression = Expression.Parameter(typeof(TSource), "p");
                List<MemberBinding> memberBindingList = new List<MemberBinding>();
                var sourceProperties = ReflectionUtils.GetPublicCanReadWriteProperties(typeof(TSource));
                foreach (var pD in ReflectionUtils.GetPublicCanReadWriteProperties(typeof(TDestination)))
                {
                    foreach (var pS in sourceProperties)
                    {
                        if (!pS.Name.Equals(pD.Name, StringComparison.OrdinalIgnoreCase)) continue;
                        if (pS.PropertyType != pD.PropertyType) break;

                        MemberExpression property = Expression.Property(parameterExpression, pS);
                        MemberBinding memberBinding = Expression.Bind(pD, property);
                        memberBindingList.Add(memberBinding);
                        break;
                    }
                }

                MemberInitExpression memberInitExpression = Expression.MemberInit(Expression.New(typeof(TDestination)), memberBindingList.ToArray());
                Expression<Func<TSource, TDestination>> lambda = Expression.Lambda<Func<TSource, TDestination>>(memberInitExpression, new ParameterExpression[] { parameterExpression });
                Func<TSource, TDestination> func = lambda.Compile();

                _expressTreeCache[key] = func;
            }
            return ((Func<TSource, TDestination>)_expressTreeCache[key])(source);
        }
    }

    /// <summary>
    /// 对象复制实用工具类
    /// </summary>
    /// <typeparam name="TSource">源对象类型</typeparam>
    /// <typeparam name="TDestination">目标对象类型</typeparam>
    public static class ObjectMapperUtils<TSource, TDestination> where TDestination : new ()
    {
        //private static readonly Func<TSource, TDestination> _createAndCopy = GenerateConvertFunction();
        private static readonly Action<TSource, TDestination> _copy = GenerateCopyFunction();

        /// <summary>
        /// 生成对象初始化方式幅值的表达式树
        /// </summary>
        /// <returns></returns>
        private static Func<TSource, TDestination> GenerateConvertFunction()
        {
            ParameterExpression source = Expression.Parameter(typeof(TSource), "source");
            List<MemberBinding> memberBindings = new List<MemberBinding>(); // 对象初始化使用的绑定对象
            var sourceProperties = ReflectionUtils.GetPublicCanReadWriteProperties(typeof(TSource));
            foreach (var pD in ReflectionUtils.GetPublicCanReadWriteProperties(typeof(TDestination)))
            {
                foreach (var pS in sourceProperties)
                {
                    if (!pS.Name.Equals(pD.Name, StringComparison.OrdinalIgnoreCase)) continue;
                    if (pS.PropertyType != pD.PropertyType) break;

                    MemberExpression pSource = Expression.Property(source, pS);
                    MemberBinding memberBinding = Expression.Bind(pD, pSource);
                    memberBindings.Add(memberBinding);
                    break;
                }
            }

            MemberInitExpression memberInit = Expression.MemberInit(Expression.New(typeof(TDestination)), memberBindings.ToArray());
            Expression<Func<TSource, TDestination>> lambda = Expression.Lambda<Func<TSource, TDestination>>(memberInit, new ParameterExpression[] { source });
            return lambda.Compile();
        }

        /// <summary>
        /// 生成copy方式赋值的表达式树
        /// </summary>
        /// <returns></returns>
        private static Action<TSource, TDestination> GenerateCopyFunction()
        {
            ParameterExpression source = Expression.Parameter(typeof(TSource), "source");
            ParameterExpression destination = Expression.Parameter(typeof(TDestination), "destination");
            var assigns = new List<BinaryExpression>(); // 幅值使用的对象
            var sourceProperties = ReflectionUtils.GetPublicCanReadWriteProperties(typeof(TSource));
            foreach (var pD in ReflectionUtils.GetPublicCanReadWriteProperties(typeof(TDestination)))
            {
                foreach (var pS in sourceProperties)
                {
                    if (!pS.Name.Equals(pD.Name, StringComparison.OrdinalIgnoreCase)) continue;
                    if (pS.PropertyType != pD.PropertyType) break;

                    MemberExpression pSource = Expression.Property(source, pS);
                    MemberExpression pDest = Expression.Property(destination, pD);
                    assigns.Add(Expression.Assign(pDest, pSource));
                    break;
                }
            }
            var block = Expression.Block(assigns); // 把所有幅值语句合并为一个block
            Expression<Action<TSource, TDestination>> lambda = Expression.Lambda<Action<TSource, TDestination>>(block, new ParameterExpression[] { source, destination });
            return lambda.Compile();
        }

        public static void MapByExpressTree(TSource source, TDestination destination)
        {
            _copy(source, destination);
        }
        public static TDestination MapByExpressTree(TSource source)
        {
            var ret = new TDestination();
            _copy(source, ret);
            return ret;
            //return _createAndCopy(source);
        }
    }
}
/*
1、需求
在代码中经常会遇到需要把对象复制一遍，或者把属性名相同的值复制一遍。

比如：

复制代码
复制代码
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public int Age { get; set; } 
    }

    public class StudentSecond
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; } 
    }
复制代码
复制代码
Student s = new Student() { Age = 20, Id = 1, Name = "Emrys" };

我们需要给新的Student赋值

Student ss = new Student { Age = s.Age, Id = s.Id, Name = s.Name };

再或者给另一个类StudentSecond的属性赋值，两个类属性的名称和类型一致。

StudentSecond ss = new StudentSecond { Age = s.Age, Id = s.Id, Name = s.Name };

 

2、解决办法
当然最原始的办法就是把需要赋值的属性全部手动手写。这样的效率是最高的。但是这样代码的重复率太高，而且代码看起来也不美观，更重要的是浪费时间，如果一个类有几十个属性，那一个一个属性赋值岂不是浪费精力，像这样重复的劳动工作更应该是需要优化的。

2.1、反射
反射应该是很多人用过的方法，就是封装一个类，反射获取属性和设置属性的值。

复制代码
复制代码
 private static TDestination TransReflection<TSource, TDestination>(TSource tIn)
        {
            TDestination tOut = Activator.CreateInstance<TDestination>();
            var tInType = tIn.GetType();
            foreach (var itemOut in tOut.GetType().GetProperties())
            {
                var itemIn = tInType.GetProperty(itemOut.Name); ;
                if (itemIn != null)
                {
                    itemOut.SetValue(tOut, itemIn.GetValue(tIn));
                }
            }
            return tOut;
        }
复制代码
复制代码
 

调用：StudentSecond ss= TransReflection<Student, StudentSecond>(s);

调用一百万次耗时：2464毫秒

 

2.2、序列化
序列化的方式有很多种，有二进制、xml、json等等，今天我们就用Newtonsoft的json进行测试。

调用：StudentSecond ss= JsonConvert.DeserializeObject<StudentSecond>(JsonConvert.SerializeObject(s));

调用一百万次耗时：2984毫秒

从这可以看出序列化和反射效率差别不大。

 

3、表达式树
3.1、简介
关于表达式树不了解的可以百度。

也就是说复制对象也可以用表达式树的方式。

        Expression<Func<Student, StudentSecond>> ss = (x) => new StudentSecond { Age = x.Age, Id = x.Id, Name = x.Name };
        var f = ss.Compile();
        StudentSecond studentSecond = f(s);
这样的方式我们可以达到同样的效果。

有人说这样的写法和最原始的复制没有什么区别，代码反而变多了呢，这个只是第一步。

 

3.2、分析代码
我们用ILSpy反编译下这段表达式代码如下：

复制代码
复制代码
　　 ParameterExpression parameterExpression;
    Expression<Func<Student, StudentSecond>> ss = Expression.Lambda<Func<Student, StudentSecond>>(Expression.MemberInit(Expression.New(typeof(StudentSecond)), new MemberBinding[]
    {
        Expression.Bind(methodof(StudentSecond.set_Age(int)), Expression.Property(parameterExpression, methodof(Student.get_Age()))),
        Expression.Bind(methodof(StudentSecond.set_Id(int)), Expression.Property(parameterExpression, methodof(Student.get_Id()))),
        Expression.Bind(methodof(StudentSecond.set_Name(string)), Expression.Property(parameterExpression, methodof(Student.get_Name())))
    }), new ParameterExpression[]
    {
        parameterExpression
    });
    Func<Student, StudentSecond> f = ss.Compile();
    StudentSecond studentSecond = f(s);
复制代码
复制代码
那么也就是说我们只要用反射循环所有的属性然后Expression.Bind所有的属性。最后调用Compile()(s)就可以获取正确的StudentSecond。

看到这有的人又要问了，如果用反射的话那岂不是效率很低，和直接用反射或者用序列化没什么区别吗？

当然这个可以解决的，就是我们的表达式树可以缓存。只是第一次用的时候需要反射，以后再用就不需要反射了。

 

3.3、复制对象通用代码
为了通用性所以其中的Student和StudentSecond分别泛型替换。

复制代码
复制代码
        private static Dictionary<string, object> _Dic = new Dictionary<string, object>();

        private static TDestination TransExp<TSource, TDestination>(TSource tIn)
        {
            string key = string.Format("trans_exp_{0}_{1}", typeof(TSource).FullName, typeof(TDestination).FullName);
            if (!_Dic.ContainsKey(key))
            {
                ParameterExpression parameterExpression = Expression.Parameter(typeof(TSource), "p");
                List<MemberBinding> memberBindingList = new List<MemberBinding>();

                foreach (var item in typeof(TDestination).GetProperties())
                {  
　　
　　　　　　　　　　　　if (!item.CanWrite)
　　　　　　　　　　　　　　continue; 
                    MemberExpression property = Expression.Property(parameterExpression, typeof(TSource).GetProperty(item.Name));
                    MemberBinding memberBinding = Expression.Bind(item, property);
                    memberBindingList.Add(memberBinding);
                }

                MemberInitExpression memberInitExpression = Expression.MemberInit(Expression.New(typeof(TDestination)), memberBindingList.ToArray());
                Expression<Func<TSource, TDestination>> lambda = Expression.Lambda<Func<TSource, TDestination>>(memberInitExpression, new ParameterExpression[] { parameterExpression });
                Func<TSource, TDestination> func = lambda.Compile();

                _Dic[key] = func;
            }
            return ((Func<TSource, TDestination>)_Dic[key])(tIn);
        }
复制代码
复制代码
调用：StudentSecond ss= TransExp<Student, StudentSecond>(s);

调用一百万次耗时：564毫秒

 

3.4、利用泛型的特性再次优化代码
不用字典存储缓存，因为泛型就可以很容易解决这个问题。

 

复制代码
复制代码
 public static class TransExpV2<TSource, TDestination>
    {

        private static readonly Func<TSource, TDestination> cache = GetFunc();
        private static Func<TSource, TDestination> GetFunc()
        {
            ParameterExpression parameterExpression = Expression.Parameter(typeof(TSource), "p");
            List<MemberBinding> memberBindingList = new List<MemberBinding>();

            foreach (var item in typeof(TDestination).GetProperties())
            {
　　　　　　　　　if (!item.CanWrite)
　　　　　　　　　　    continue;

                MemberExpression property = Expression.Property(parameterExpression, typeof(TSource).GetProperty(item.Name));
                MemberBinding memberBinding = Expression.Bind(item, property);
                memberBindingList.Add(memberBinding);
            }

            MemberInitExpression memberInitExpression = Expression.MemberInit(Expression.New(typeof(TDestination)), memberBindingList.ToArray());
            Expression<Func<TSource, TDestination>> lambda = Expression.Lambda<Func<TSource, TDestination>>(memberInitExpression, new ParameterExpression[] { parameterExpression });

            return lambda.Compile();
        }

        public static TDestination Trans(TSource tIn)
        {
            return cache(tIn);
        }

    }
复制代码
复制代码
 

调用：StudentSecond ss= TransExpV2<Student, StudentSecond>.Trans(s);

调用一百万次耗时：107毫秒

耗时远远的小于使用automapper的338毫秒。 

 

4、总结
从以上的测试和分析可以很容易得出，用表达式树是可以达到效率与书写方式二者兼备的方法之一，总之比传统的序列化和反射更加优秀。

最后望对各位有所帮助，本文原创，欢迎拍砖和推荐。   
*/
