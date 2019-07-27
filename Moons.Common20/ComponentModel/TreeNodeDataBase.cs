using System.Collections.Generic;

namespace Moons.Common20.ComponentModel
{
    /// <summary>
    /// 树状数据结构，节点类的基类
    /// </summary>
    /// <typeparam name="T">实际树节点的类型</typeparam>
    public class TreeNodeDataBase<T> : EntityWithEventBase where T : TreeNodeDataBase<T>
    {
        private readonly IList<T> _children = new List<T>();

        /// <summary>
        /// 节点文字
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// 节点ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int? SortNo_NR { get; set; }

        /// <summary>
        /// 父节点，根节点没有父节点，则为null
        /// </summary>
        public T Parent { get; set; }

        /// <summary>
        /// 子节点集合
        /// </summary>
        public IList<T> Children
        {
            get { return _children; }
        }
    }

    /// <summary>
    /// 树节点
    /// </summary>
    public class TreeNodeDataBase : TreeNodeDataBase<TreeNodeDataBase>
    {
    }
}