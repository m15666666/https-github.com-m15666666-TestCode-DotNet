using System;
using System.Collections.Generic;
using System.Linq;

namespace Moons.Common20
{
    /// <summary>
    /// linq 扩展类
    /// </summary>
    public static class LinqExtensions
    {
        /// <summary>
        /// 获得最大值对应的元素对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <typeparam name="TProp">返回比较的类型</typeparam>
        /// <param name="source">列表</param>
        /// <param name="propSelector">返回比较值的函数</param>
        /// <returns>最大值对应的元素对象</returns>
        public static T MaxItem<T, TProp>(this IEnumerable<T> source, Func<T, TProp> propSelector) =>
            source.OrderByDescending(propSelector).FirstOrDefault();

        /// <summary>
        /// 获得最小值对应的元素对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <typeparam name="TProp">返回比较的类型</typeparam>
        /// <param name="source">列表</param>
        /// <param name="propSelector">返回比较值的函数</param>
        /// <returns>最小值对应的元素对象</returns>
        public static T MinItem<T, TProp>(this IEnumerable<T> source, Func<T, TProp> propSelector) =>
            source.OrderBy(propSelector).FirstOrDefault();
    }
}