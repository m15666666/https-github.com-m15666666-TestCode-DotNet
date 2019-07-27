namespace Moons.Common20
{
    /// <summary>
    /// 数据对的类
    /// </summary>
    /// <typeparam name="TA">类型1</typeparam>
    /// <typeparam name="TB">类型2</typeparam>
    public class PairData<TA, TB>
    {
        /// <summary>
        /// 数据A
        /// </summary>
        public TA DataA { get; set; }

        /// <summary>
        /// 数据B
        /// </summary>
        public TB DataB { get; set; }
    }

    /// <summary>
    /// 只包含名和值的类对象
    /// </summary>
    /// <typeparam name="TKey">名类型</typeparam>
    /// <typeparam name="TValue">值类型</typeparam>
    public class NameValueData<TKey, TValue> : PairData<TKey, TValue>
    {
        /// <summary>
        /// 名
        /// </summary>
        public TKey Name
        {
            get { return DataA; }
            set { DataA = value; }
        }

        /// <summary>
        /// 名
        /// </summary>
        public TValue Value
        {
            get { return DataB; }
            set { DataB = value; }
        }
    }

    /// <summary>
    /// 字符串类型的只包含名和值的类对象
    /// </summary>
    public class NameValueStringData : NameValueData<string, string>
    {
    }

    /// <summary>
    /// 字符串类型的数据对的类
    /// </summary>
    public class PairStringData : PairData<string, string>
    {
    }
}