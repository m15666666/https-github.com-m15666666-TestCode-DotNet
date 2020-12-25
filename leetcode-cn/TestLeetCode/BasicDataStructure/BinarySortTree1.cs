using System;

namespace TestLeetCode.BasicDataStructure
{
    /// <summary>
    /// 参考：C# 实现二叉排序树
    /// https://blog.csdn.net/FHelloWorld/article/details/109540910
    /// 
    /// 其他参考：http://csharpexamples.com/c-binary-search-tree-implementation/
    /// 
    /// 教程：https://www.csharpstar.com/csharp-program-to-implement-binary-search-tree/
    /// </summary>
    public class BinarySortTree1
    {
        private Node Root;

        public void Add(int value) => Add(new Node { Value = value });

        public void Add(Node node)
        {
            if (Root == null) Root = node;
            else Root.AddNode(node);
        }

        public void InfixOrder()
        {
            if (Root != null) Root.InfixOrder();
            else Console.WriteLine("Empty!");
        }

        //查找要删除的节点
        public Node Search(int value)
        {
            if (Root == null) return null;
            else return Root.Search(value);
        }

        //查找待删除结点的父节点
        public Node SearchParent(int value)
        {
            if (Root == null) return null;
            else return Root.SearchParent(value);
        }

        /// <summary>
        /// 1. 返回以node为根节点的二叉排序树的最小节点的值
        /// 2. 删除node为根节点的二叉排序树的最小节点
        /// </summary>
        /// <param name="node">传入节点，当作二叉排序树的根节点</param>
        /// <returns>返回以node为根节点的二叉排序树的最小节点的值</returns>
        public int DeleteRightTreeMin(Node node)
        {
            Node t = node;
            //循环查找左节点
            while (t.Left != null) t = t.Left;
            //此时 t指向最小节点
            DeleteNode(t.Value);
            return t.Value;
        }

        /// <summary>
        /// 1. 返回以node为根节点的二叉排序树的最大节点的值
        /// 2. 删除node为根节点的二叉排序树的最大节点
        /// </summary>
        /// <param name="node">传入节点，当作二叉排序树的根节点</param>
        /// <returns>返回以node为根节点的二叉排序树的最大节点的值</returns>
        public int DeleteLeftTreeMax(Node node)
        {
            Node t = node;
            while (t.Right != null) t = t.Right;
            DeleteNode(t.Value);
            return t.Value;
        }

        //删除节点
        public void DeleteNode(int value)
        {
            if (Root == null) return;

            //1.查找targetNode
            Node targetNode = Search(value);
            //如果没有找到要删除的节点
            if (targetNode == null) return;

            //如果当前二叉排序树只有一个节点
            if (Root.Left == null && Root.Right == null)
            {
                Root = null;
                return;
            }

            //查找targetNode的父节点
            Node parent = SearchParent(value);

            //情况一
            if (targetNode.Left == null && targetNode.Right == null)
            {
                //判断targetNode 是parent的Left还是Right
                if (parent.Left != null && parent.Left.Value == value) parent.Left = null;
                else if (parent.Right != null && parent.Right.Value == value) parent.Right = null;
                return;
            }
            if (targetNode.Left != null && targetNode.Right != null)//情况三
            {
                // 从右子树找到值最小的节点并处理
                //int minVal = DeleteRightTreeMin(targetNode.Right);
                //targetNode.Value = minVal;

                //从左子树找到值最大的节点并处理
                int maxVal = DeleteLeftTreeMax(targetNode.Left);
                targetNode.Value = maxVal;
                return;
            }

            //余下的就是只有一颗子树的节点
            var branch = targetNode.Left ?? targetNode.Right;
            if (parent != null)
            {
                if (parent.Left.Value == value) parent.Left = branch;
                else parent.Right = branch;
            }
            else Root = branch;
        }
        
        public Node LowerBound( int lowerBound ) => Root?.LowerBound(lowerBound);

        public class Node
        {
            public int Value { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }

            /// <summary>
            /// 通过value查找对应节点
            /// </summary>
            /// <param name="value">希望查找的节点的value</param>
            /// <returns>如果查找到对应节点则返回，否则返回null</returns>
            public Node Search(int value)
            {
                if (value == Value) return this;
                return value < Value ? (Left?.Search(value)) : (Right?.Search(value));
            }

            /// <summary>
            /// 通过value查找对应节点的父节点
            /// </summary>
            /// <param name="value">带查找节点的value</param>
            /// <returns>如果查找到对应节点的父节点则返回，否则返回null</returns>
            public Node SearchParent(int value)
            {
                if ((Left != null && Left.Value == value) || (Right != null && Right.Value == value)) return this;
                if (value < Value && Left != null) return Left.SearchParent(value);
                if (value > Value && Right != null) return Right.SearchParent(value);
                return null;
            }

            //添加节点
            public void AddNode(Node node)
            {
                if (node == null) return;

                //判断节点的值，与当前子树根节点的值的关系
                if (node.Value < Value)
                {
                    //如果当前节点左子节点为null
                    if (Left == null) Left = node;
                    else Left.AddNode(node);//递归向左子树添加
                }
                else  //node 的值大于等于当前节点的值
                {
                    if (Right == null) Right = node;
                    else Right.AddNode(node);//递归向右处理
                }
            }

            public void InfixOrder()
            {
                if (Left != null) Left.InfixOrder();
                if (Right != null) Right.InfixOrder();
            }

            public Node LowerBound(int lowerBound)
            {
                if (lowerBound == Value) return this;

                if(lowerBound < Value)
                {
                    var ret = Left?.LowerBound(lowerBound);
                    return ret ?? this;
                }
                return (Right?.LowerBound(lowerBound));
            }

            public override string ToString() => $"Node: [value = {Value}]";
        }
    }
}