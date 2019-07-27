namespace Moons.Common20.Collections
{
    /// <summary>
    /// 表示二元组
    /// </summary>
    /// <typeparam name="T1">元组第一个元素的类型</typeparam>
    /// <typeparam name="T2">元组第二个元素的类型</typeparam>
    public class Tuple<T1, T2>
    {
        /// <summary>
        /// 获取当前元组对象的第一个元素
        /// </summary>
        public T1 Item1 { get; private set; }

        /// <summary>
        /// 获取当前元组对象的第二个元素
        /// </summary>
        public T2 Item2 { get; private set; }

        /// <summary>
        /// 初始化一个二元组
        /// </summary>
        /// <param name="item1">元组第一个元素的值</param>
        /// <param name="item2">元组第二个元素的值</param>
        public Tuple(T1 item1, T2 item2)
        {
            Item1 = item1;
            Item2 = item2;
        }
    }
}