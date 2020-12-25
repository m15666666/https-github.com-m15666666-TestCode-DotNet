/*
给定一个非空二维矩阵 matrix 和一个整数 k，找到这个矩阵内部不大于 k 的最大矩形和。

示例:

输入: matrix = [[1,0,1],[0,-2,3]], k = 2
输出: 2
解释: 矩形区域 [[0, 1], [-2, 3]] 的数值和是 2，且 2 是不超过 k 的最大数字（k = 2）。
说明：

矩阵内的矩形区域面积必须大于 0。
如果行数远大于列数，你将如何解答呢？

*/

using System;
using System.Collections.Generic;

/// <summary>
/// https://leetcode-cn.com/problems/max-sum-of-rectangle-no-larger-than-k/
/// 363. 矩形区域不超过 K 的最大数值和
///
/// </summary>
internal class MaxSumOfRectangleNoLargerThanKSolution
{
    public void Test()
    {
        int[][] nums = new int[][] { new []{27,5,-20,-9,1,26,1,12,7,-4,8,7,-1,5,8},new []{16,28,8,3,16,28,-10,-7,-5,-13,7,9,20,-9,26},new []{24,-14,20,23,25,-16,-15,8,8,-6,-14,-6,12,-19,-13},new []{28,13,-17,20,-3,-18,12,5,1,25,25,-14,22,17,12},new []{7,29,-12,5,-5,26,-5,10,-5,24,-9,-19,20,0,18},new []{-7,-11,-8,12,19,18,-15,17,7,-1,-11,-10,-1,25,17},new []{-3,-20,-20,-7,14,-12,22,1,-9,11,14,-16,-5,-12,14},new []{-20,-4,-17,3,3,-18,22,-13,-1,16,-11,29,17,-2,22},new []{23,-15,24,26,28,-13,10,18,-6,29,27,-19,-19,-8,0},new []{5,9,23,11,-4,-20,18,29,-6,-4,-11,21,-6,24,12},new []{13,16,0,-20,22,21,26,-3,15,14,26,17,19,20,-5},new []{15,1,22,-6,1,-9,0,21,12,27,5,8,8,18,-1},new []{15,29,13,6,-11,7,-6,27,22,18,22,-3,-9,20,14},new []{26,-6,12,-10,0,26,10,1,11,-10,-16,-18,29,8,-8},new []{-19,14,15,18,-10,24,-9,-7,-19,-14,23,23,17,-5,6} };
        var ret = this.MaxSumSubmatrix(nums, -100);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int MaxSumSubmatrix(int[][] matrix, int k)
    {
        //SortedDictionary
        if (matrix == null || matrix.Length == 0 || matrix[0].Length == 0) return 0;
        int rows = matrix.Length;
        int cols = matrix[0].Length;
        int max = int.MinValue;

        int[] rowSum = new int[rows];
        for (int l = 0; l < cols; l++)
        {
            Array.Fill(rowSum, 0);
            for (int r = l; r < cols; r++)
            {
                int max_sub_sum=0;
                int max_sum = int.MinValue;
                for (int i = 0; i < rows; i++)
                {
                    rowSum[i] += matrix[i][r];
                    max_sub_sum += rowSum[i];
                    if (max_sub_sum <= k && max < max_sub_sum) max = max_sub_sum;
                    if (max_sub_sum < 0) max_sub_sum = 0;
                    if (max_sum < max_sub_sum) max_sum = max_sub_sum;
                }
                if (max == k) return k;
                if (max_sum <= k) continue;

                //BinarySortTree1 tree = new BinarySortTree1();
                //tree.Add(new BinarySortTree1.Node { Value = 0 });
                RBTree1 tree = new RBTree1();
                tree.Add(0);
                int prefixRowSum = 0;
                for (int i = 0; i < rows;++i)
                {
                    prefixRowSum += rowSum[i];
                    var node = tree.LowerBound( prefixRowSum - k );
                    //tree.Add( new BinarySortTree1.Node { Value = prefixRowSum });
                    tree.Add( prefixRowSum );
                    if (node == null) continue;
                    int tryMax = prefixRowSum - node.Value;
                    if (max < tryMax)
                    {
                        if (tryMax == k) return k;
                        max = tryMax;
                    }
                }
            }
        }
        return max;
    }

    public class RBTree1
    {
        private Node Root; //头指针
        private Node[] path = new Node[32]; //记录访问路径上的结点
        private int p; //表示当前访问到的结点在_path上的索引

        public Node Head => Root;

        public bool Add(int value) //添加一个元素
        {
            //如果是空树，则新结点成为二叉排序树的根
            if (Root == null)
            {
                Root = new Node { Value = value, IsRed = false };
                return true;
            }
            p = 0;
            //parent为上一次访问的结点，current为当前访问结点
            Node parent = null, current = Root;
            while (current != null)
            {
                path[p++] = current; //将路径上的结点插入数组
                //如果插入值已存在，则插入失败
                if (current.Value == value) return false;
                parent = current;
                //当插入值小于当前结点，则继续访问左子树，否则访问右子树
                current = (value < parent.Value) ? parent.Left : parent.Right;
            }
            current = new Node { Value = value, IsRed = true }; //创建新结点
            //如果插入值小于双亲结点的值
            if (value < parent.Value) parent.Left = current; //成为左孩子
            //如果插入值大于双亲结点的值
            else parent.Right = current; //成为右孩子
            if (!parent.IsRed) return true;
            path[p] = current;
            //回溯并旋转
            while ((p -= 2) >= 0) //现在p指向插入点的祖父结点
            {
                Node grandParent = path[p];
                parent = path[p + 1];
                if (!parent.IsRed) break;
                Node uncle = grandParent.Left == parent ? grandParent.Right : grandParent.Left;
                current = path[p + 2];
                // 叔父存在并且为红色的情况
                if (IsRed(uncle))
                {
                    parent.IsRed = false;
                    uncle.IsRed = false;
                    // 如果祖父不是根结点，则将其染成红色
                    if (0 < p) grandParent.IsRed = true;
                }
                //叔父不存在或为黑的情况需要旋转
                else
                {
                    Node newRoot;
                    //如果当前结点及父结点同为左孩子或右孩子
                    if (grandParent.Left == parent) newRoot = (parent.Left == current) ? LL(grandParent) : LR(grandParent);
                    else newRoot = (parent.Right == current) ? RR(grandParent) : RL(grandParent);

                    grandParent.IsRed = true; //祖父染成红色
                    newRoot.IsRed = false; //新根染成黑色
                    //将新根同曾祖父连接
                    ReplaceChildOfNodeOrRoot((p > 0) ? path[p - 1] : null, grandParent, newRoot);
                    return true; //旋转后不需要继续回溯，添加成功，直接退出
                }
            }
            return true;
        }

        //旋转根旋转后换新根
        private void ReplaceChildOfNodeOrRoot(Node parent, Node child, Node newChild)
        {
            if (parent != null)
            {
                if (parent.Left == child) parent.Left = newChild;
                else parent.Right = newChild;
                return;
            }
            Root = newChild;
        }

        private static bool IsBlack(Node node) => (node != null) && !node.IsRed;

        private static bool IsNullOrBlack(Node node) => node != null ? !node.IsRed : true;

        private static bool IsRed(Node node) => (node != null) && node.IsRed;

        //删除指定值
        public bool Remove(int value)
        {
            p = -1;
            //parent表示双亲结点，node表示当前结点
            Node node = Root;
            //寻找指定值所在的结点
            while (node != null)
            {
                path[++p] = node;
                //如果找到，则调用RemoveNode方法删除结点
                if (value == node.Value)
                {
                    RemoveNode(node);//现在p指向被删除结点
                    return true; //返回true表示删除成功
                }
                //如果删除值小于当前结点，则向左子树继续寻找
                if (value < node.Value) node = node.Left;
                //如果删除值大于当前结点，则向右子树继续寻找
                else node = node.Right;
            }
            return false; //返回false表示删除失败
        }

        //删除指定结点
        private void RemoveNode(Node node)
        {
            Node tmp = null; //tmp最终指向实际被删除的结点
            //当被删除结点存在左右子树时
            if (node.Left != null && node.Right != null)
            {
                tmp = node.Left; //获取左子树
                path[++p] = tmp;
                while (tmp.Right != null) //获取node的中序遍历前驱结点，并存放于tmp中
                {   //找到左子树中的最右下结点
                    tmp = tmp.Right;
                    path[++p] = tmp;
                }
                //用中序遍历前驱结点的值代替被删除结点的值
                node.Value = tmp.Value;
            }
            else tmp = node;

            //当只有左子树或右子树或为叶子结点时
            //首先找到惟一的孩子结点
            Node newTmp = tmp.Left;
            //如果只有右孩子或没孩子
            if (newTmp == null) newTmp = tmp.Right;
            if (p > 0)
            {
                Node parent = path[p - 1];
                //如果被删结点是左孩子
                if (parent.Left == tmp) parent.Left = newTmp;
                //如果被删结点是右孩子
                else parent.Right = newTmp;
                if (!tmp.IsRed && IsRed(newTmp))
                {
                    newTmp.IsRed = false;
                    return;
                }
            }
            else  //当删除的是根结点时
            {
                Root = newTmp;
                if (Root != null) Root.IsRed = false;
                return;
            }
            path[p] = newTmp;

            //如果被删除的是红色结点，则它必定是叶子结点，删除成功，返回
            if (IsRed(tmp)) return;

            //删除完后进行旋转，现在p指向实际被删除的位置,这个位置可能为空,tmp指向被删除的旧点，
            while (0 < p)
            {   //剩下的是双黑的情况
                //首先找到兄弟结点
                Node current = path[p];
                Node parent = path[p - 1];
                bool currentIsLeft = (parent.Left == current);
                Node sibling = currentIsLeft ? parent.Right : parent.Left;
                //红兄的情况，需要LL旋转或RR旋转
                if (IsRed(sibling))
                {
                    Node newRoot;
                    if (currentIsLeft) newRoot = RR(parent);
                    else newRoot = LL(parent);

                    ReplaceChildOfNodeOrRoot(p > 1 ? path[p - 2] : null, parent, newRoot);
                    sibling.IsRed = false;
                    parent.IsRed = true;
                    //回溯点降低
                    path[p - 1] = newRoot;
                    path[p] = parent;
                    path[++p] = current;
                    continue;
                }
                //黑兄的情况
                //黑兄二黑侄
                if (IsNullOrBlack(sibling.Left) && IsNullOrBlack(sibling.Right))
                {  //红父的情况
                    if (parent.IsRed)
                    {
                        parent.IsRed = false;
                        sibling.IsRed = true;
                        if (current != null)
                        {
                            current.IsRed = false;
                        }
                        break; //删除成功
                    }
                    else //黑父的情况
                    {
                        parent.IsRed = IsRed(current);
                        if (current != null)
                        {
                            current.IsRed = false;
                        }
                        sibling.IsRed = true;
                        p--; //需要继续回溯
                    }
                }
                else //黑兄红侄
                {
                    Node newRoot;
                    if (currentIsLeft) //兄弟在右边
                    {
                        if (IsRed(sibling.Right)) //红侄在右边
                        {  //RR型旋转
                            newRoot = RR(parent);
                            sibling.Right.IsRed = false;
                        }
                        else
                        {  //RL型旋转
                            newRoot = RL(parent);
                        }
                    }
                    else //兄弟在左边
                    {
                        if (IsRed(sibling.Left))
                        {  //LL型旋转
                            newRoot = LL(parent);
                            sibling.Left.IsRed = false;
                        }
                        else
                        {  //LR型旋转
                            newRoot = LR(parent);
                        }
                    }
                    if (current != null)
                    {
                        current.IsRed = false;
                    }
                    newRoot.IsRed = parent.IsRed;
                    parent.IsRed = false;
                    ReplaceChildOfNodeOrRoot(p > 1 ? path[p - 2] : null, parent, newRoot);
                    break; //不需要回溯，删除成功
                }
            }
        }

        //root为旋转根，rootPrev为旋转根双亲结点
        private Node LL(Node root) //LL型旋转，返回旋转后的新子树根
        {
            Node left = root.Left;
            root.Left = left.Right;
            left.Right = root;
            return left;
        }

        private Node LR(Node root) //LR型旋转，返回旋转后的新子树根
        {
            Node left = root.Left;
            Node right = left.Right;
            root.Left = right.Right;
            right.Right = root;
            left.Right = right.Left;
            right.Left = left;
            return right;
        }

        private Node RR(Node root) //RR型旋转，返回旋转后的新子树根
        {
            Node right = root.Right;
            root.Right = right.Left;
            right.Left = root;
            return right;
        }

        private Node RL(Node root) //RL型旋转，返回旋转后的新子树根
        {
            Node right = root.Right;
            Node left = right.Left;
            root.Right = left.Left;
            left.Left = root;
            right.Left = left.Right;
            left.Right = right;
            return left;
        }

        public Node LowerBound( int lowerBound ) => Root?.LowerBound(lowerBound);

        public class Node
        {
            public int Value { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }
            public bool IsRed { get; set; }

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

    public class BinarySortTree1
    {
        private Node Root;

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

            public override string ToString() => $"Node: [value = {this.Value}]";
        }
    }

    //public int MaxSumSubmatrix(int[][] matrix, int k)
    //{
    //    if (matrix == null || matrix.Length == 0 || matrix[0].Length == 0) return 0;
    //    int rows = matrix.Length;
    //    int cols = matrix[0].Length;
    //    int max = int.MinValue;

    //    int[] rowSum = new int[rows];
    //    for (int l = 0; l < cols; l++)
    //    {
    //        Array.Fill(rowSum, 0);
    //        for (int r = l; r < cols; r++)
    //        {
    //            for (int i = 0; i < rows; i++) rowSum[i] += matrix[i][r];

    //            max = Math.Max(max, DpMax(rowSum, k));
    //            if (max == k) return k;
    //        }
    //    }
    //    return max;
    //    static int DpMax(int[] sums, int k)
    //    {
    //        int rollSum = sums[0], rollMax = rollSum;
    //        for (int i = 1; i < sums.Length; i++)
    //        {
    //            if (0 < rollSum ) rollSum += sums[i];
    //            else rollSum = sums[i];
    //            if (rollMax < rollSum) rollMax = rollSum;
    //        }
    //        if (rollMax <= k) return rollMax;

    //        int ret = int.MinValue;
    //        for (int left = 0; left < sums.Length; left++)
    //        {
    //            int sum = 0;
    //            for (int right = left; right < sums.Length; right++)
    //            {
    //                sum += sums[right];
    //                if (ret < sum && sum <= k) ret = sum;
    //                if (ret == k) return k;
    //            }
    //        }
    //        return ret;
    //    }
    //}

    //public int MaxSumSubmatrix(int[][] matrix, int k)
    //{
    //    if (matrix == null || matrix.Length == 0 || matrix[0].Length == 0) return 0;

    //    int M = matrix.Length;
    //    int N = matrix[0].Length;
    //    int ret = int.MinValue;
    //    int[,] prefixSum = new int[M, N];
    //    for (int i = 0; i < M; i++)
    //        for (int j = 0; j < N; j++)
    //        {
    //            prefixSum[i, j] = matrix[i][j];
    //            if (i > 0) prefixSum[i, j] += prefixSum[i - 1, j];
    //            if (j > 0) prefixSum[i, j] += prefixSum[i, j - 1];
    //            if (i > 0 && j > 0) prefixSum[i, j] -= prefixSum[i - 1, j - 1];

    //            for (int r = 0; r <= i; r++)
    //                for (int c = 0; c <= j; c++)
    //                {
    //                    int curRecSum = prefixSum[i, j];
    //                    if (r > 0) curRecSum -= prefixSum[r - 1, j];
    //                    if (c > 0) curRecSum -= prefixSum[i, c - 1];
    //                    if (r > 0 && c > 0) curRecSum += prefixSum[r - 1, c - 1];

    //                    if (curRecSum <= k && ret < curRecSum) ret = curRecSum;
    //                }
    //        }
    //    return ret;
    //}
}

/*

【中规中矩】动态规划最接近target值的子矩形面积
jyj407
发布于 2020-12-10
143
解题思路
此处撰写解题思路

代码

class Solution1 {
public:
    int maxSumSubmatrix(vector<vector<int>>& matrix, int k) {
        if (matrix.empty() || matrix[0].empty()) {
            return 0;
        }

        int M = matrix.size();
        int N = matrix[0].size();
        int res = INT_MIN;
        vector<vector<int>> prefixSum(M, vector<int>(N, 0));
        for (int i = 0; i < M; i++) {
            for (int j = 0; j < N; j++) {
                prefixSum[i][j] = matrix[i][j];
                if (i > 0) {
                    prefixSum[i][j] += prefixSum[i - 1][j];
                }
                if (j > 0) {
                    prefixSum[i][j] += prefixSum[i][j - 1];
                }
                if (i > 0 && j > 0) {
                    prefixSum[i][j] -= prefixSum[i - 1][j - 1];
                }

                // This can be in a seperate loop for better readability
                // calculate the sub-rectangle from [r, c] to [i, j]
                for (int r = 0; r <= i; r++) {
                    for (int c = 0; c <= j; c++) {
                        int curRecSum = prefixSum[i][j];
                        if (r > 0) {
                            curRecSum -= prefixSum[r - 1][j];
                        }
                        if (c > 0) {
                            curRecSum -= prefixSum[i][c - 1];
                        }
                        if (r > 0 && c > 0) {
                            curRecSum += prefixSum[r - 1][c - 1];
                        }

                        if (curRecSum <= k) {
                            res = max(res, curRecSum);
                        }
                    }
                }
            }
        }

        return res;
    }
};

ava，从暴力开始优化，配图配注释
lzhlyle

发布于 2020-03-13
7.4k
一、暴力 + 动态规划
枚举矩形的 左上角、右下角，从 (i1, j1) 到 (i2, j2)
从左上角、到右下角的矩形区域数值和：黄色 = 绿色 + 橙色 - 蓝色 + (i2, j2)


状态转移方程为 dp(i1,j1,i2,j2) = dp(i1,j1,i2 - 1,j2) + dp(i1,j1,i2,j2 - 1) - dp(i1,j1,i2 - 1,j2 - 1) + matrix[i2 - 1][j2 - 1];
四层遍历，时间复杂度 O(m^2n^2)O(m 
2
 n 
2
 )，空间复杂度 O(m^2n^2)O(m 
2
 n 
2
 )
超出内存限制
思路有戏，进一步压缩状态试试
image.png


public int maxSumSubmatrix(int[][] matrix, int k) {
    int rows = matrix.length, cols = matrix[0].length, max = Integer.MIN_VALUE;
    int[][][][] dp = new int[rows + 1][cols + 1][rows + 1][cols + 1]; // from (i1,j1) to (i2,j2)
    for (int i1 = 1; i1 <= rows; i1++) {
        for (int j1 = 1; j1 <= cols; j1++) {
            dp[i1][j1][i1][j1] = matrix[i1 - 1][j1 - 1];
            for (int i2 = i1; i2 <= rows; i2++) {
                for (int j2 = j1; j2 <= cols; j2++) {
                    dp[i1][j1][i2][j2] = dp[i1][j1][i2 - 1][j2] + dp[i1][j1][i2][j2 - 1] - dp[i1][j1][i2 - 1][j2 - 1] + matrix[i2 - 1][j2 - 1];
                    if (dp[i1][j1][i2][j2] <= k && dp[i1][j1][i2][j2] > max) max = dp[i1][j1][i2][j2];
                }
            }
        }
    }
    return max;
}
二、暴力 + 动态规划 + 状态压缩
从上述代码发现，每次更换左上角 (i, j) 之后，之前记录的值都没用过了
尝试每次更换左上角时就重复利用 dp，故只需记录右下角即可
依然四层遍历，时间复杂度 O(m^2n^2)O(m 
2
 n 
2
 )，空间复杂度 O(mn)O(mn)
image.png


public int maxSumSubmatrix(int[][] matrix, int k) {
    int rows = matrix.length, cols = matrix[0].length, max = Integer.MIN_VALUE;
    for (int i1 = 1; i1 <= rows; i1++) {
        for (int j1 = 1; j1 <= cols; j1++) {
            int[][] dp = new int[rows + 1][cols + 1]; // renew  // from (i1,j1) to (i2,j2)
            dp[i1][j1] = matrix[i1 - 1][j1 - 1];
            for (int i2 = i1; i2 <= rows; i2++) {
                for (int j2 = j1; j2 <= cols; j2++) {
                    dp[i2][j2] = dp[i2 - 1][j2] + dp[i2][j2 - 1] - dp[i2 - 1][j2 - 1] + matrix[i2 - 1][j2 - 1];
                    if (dp[i2][j2] <= k && dp[i2][j2] > max) max = dp[i2][j2];
                }
            }
        }
    }
    return max;
}
三、数组滚动
看过大神的思路 @powcai 固定左右边界，前缀和+二分
固定左右边界 ……这句一下就把思路打开了
虽然看不懂 python..但还是不能放弃呀
先固定左右边界，不断压入 行累计数组

public int maxSumSubmatrix(int[][] matrix, int k) {
    int rows = matrix.length, cols = matrix[0].length, max = Integer.MIN_VALUE;
    // O(cols ^ 2 * rows)
    for (int l = 0; l < cols; l++) { // 枚举左边界
        int[] rowSum = new int[rows]; // 左边界改变才算区域的重新开始
        for (int r = l; r < cols; r++) { // 枚举右边界
            for (int i = 0; i < rows; i++) { // 按每一行累计到 dp
                rowSum[i] += matrix[i][r];
            }

            // ？？？
        }
    }
    return max;
}
画图感受一下
左边界 从 0 开始
右边界从左边界开始（即同一列）
rowSum 数组，记录两个边界中间的 每一行 的 和
image.png

表演开始了
右边界 r 向右移动
rowSum 数组，记录两个边界中间的 每一行 的 和
累加新来的
image.png

这张过后你也豁然开朗了吗
右边界 r 继续向右移动
rowSum 数组，仍然记录两个边界中间的 每一行 的 和
继续累加新来的即可
image.png

rowSum 有何用
以 l、r 为左右界的，任意矩形的面积，即 rowSum 连续子数组 的 和
image.png

再让我们回到代码

public int maxSumSubmatrix(int[][] matrix, int k) {
    int rows = matrix.length, cols = matrix[0].length, max = Integer.MIN_VALUE;
    // O(cols ^ 2 * rows)
    for (int l = 0; l < cols; l++) { // 枚举左边界
        int[] rowSum = new int[rows]; // 左边界改变才算区域的重新开始
        for (int r = l; r < cols; r++) { // 枚举右边界
            for (int i = 0; i < rows; i++) { // 按每一行累计到 dp
                rowSum[i] += matrix[i][r];
            }

            // 求 rowSum 连续子数组 的 和
            // 和 尽量大，但不大于 k
            max = Math.max(max, dpmax(rowSum, k));
        }
    }
    return max;
}

// 在数组 arr 中，求不超过 k 的最大值
private int dpmax(int[] arr, int k) {
    // TODO
}
问题进入到最后一个环节，完善 dpmax()
暴力求最大值
枚举子数组起点、终点，累计中间元素
此时的运行时间已经起飞很多了
image.png


// 在数组 arr 中，求不超过 k 的最大值
private int dpmax(int[] arr, int k) {
    // O(rows ^ 2)
    int max = Integer.MIN_VALUE;
    for (int l = 0; l < arr.length; l++) {
        int sum = 0;
        for (int r = l; r < arr.length; r++) {
            sum += arr[r];
            if (sum > max && sum <= k) max = sum;
        }
    }
    return max;
}
可是我们就是要完美一下呢
并不是所有时候都值得遍历找 k
先来这题：53. 最大子序和，有一种解法是

public int maxSubArray(int[] nums) {
    int len = nums.length, max, dp;
    if (len == 0) return 0;
    // 要尽量大，就尽量不要负数
    dp = max = nums[0];
    for (int i = 1; i < len; i++) {
        if (dp > 0) dp += nums[i]; // 之前的和 > 0，那就累计进来
        else dp = nums[i]; // 之前的和 <= 0，那就重新开始
        if (dp > max) max = dp; // max = Math.max(max, dp);
    }
    return max;
}
先画图感受一下
开始遍历数组 [4, 3, -1, -7, -9, 6, 2, -7]
image.png

此时出现了 之前的和小于0 的情况
那下一个数开始，咱就不要之前的了，另起炉灶（还是连续两次另立炉灶）
image.png

最终得到 [6, 2] 这个区间的子数组和最大，最大值 8
这里复杂的是还要 不大于 k 怎么办？
继续深入细究 k
假设 k = Integer.MAX_VALUE ，那么上述数组不小于 k 的最大子数组和为 8
假设 k = 100 ，那么上述数组不小于 k 的最大子数组和 仍然 为 8
你也许注意到了，要是 k 很大，大过上述滚动玩法的最大值，那结果就是上述的 8
那如果 k == 8 呢？太棒了，就是 8 咯，最好的最大值
那如果 k < 8 呢，假设 k = 5
回顾我们 dp 一路滚过来的值 [4, 7, 6, -1, -9, 6, 8, 1]
难道不大于 k = 5 的子数组的最大值就是 4 吗？是的，这里看起来是
注意这是 dp 一路滚来的值，不是数组原值
原数组是 [4, 3, -1, -7, -9, 6, 2, -7]
如果我们再在原数组后增加 14 形成 [4, 3, -1, -7, -9, 6, 2, -7, 14]
则结果应该是 整个数组 的和 5，而不是 因为前面的 -9 而断开累计
怎么办？——暴力就好了（在下只能暴力了...还有别的法子吗...）
image.png


// 隔壁有完整代码
// 在数组 arr 中，求不超过 k 的最大值
private int dpmax(int[] arr, int k) {
    int rollSum = arr[0], rollMax = rollSum;
    // O(rows)
    for (int i = 1; i < arr.length; i++) {
        if (rollSum > 0) rollSum += arr[i];
        else rollSum = arr[i];
        if (rollSum > rollMax) rollMax = rollSum;
    }
    if (rollMax <= k) return rollMax;
    // O(rows ^ 2)
    int max = Integer.MIN_VALUE;
    for (int l = 0; l < arr.length; l++) {
        int sum = 0;
        for (int r = l; r < arr.length; r++) {
            sum += arr[r];
            if (sum > max && sum <= k) max = sum;
            if (max == k) return k; // 尽量提前
        }
    }
    return max;
}
得，愉快的大半天又没了，可是你能看到这儿，笔者还是很开心，值了：）

法三：二分


前提还是一样的，采用固定好左右两边(列)，再对行进行求和操作
但是有没有不再像法一那样的暴力O(n^2)方法了呢?可以优化到O(nlogn)

首先想起来前缀和求法sum[j]-sum[i]，可以理解成 大面积-小面积
结合起题目来就是 大-小<=k 而稍微变化一下就是 小>=大-k
我们主要找的就是符合的小面积，而且要想最逼近k，在大面积一定情况下，小面积越小越好
首先定好大：其实就是暴力中的j即sum[j]
而小，我们要存好暴力中前面所有的结果都存起来即sum[i]
而找小>=大-k 中这个小
想起了lower_bound(elem)这个二分：找有序排列中第一个>=elem的元素的位置
问题来了有序？ 那就用set自动排序存
刚好set有lower_bound函数，一切都是刚刚好
我们就通过不断存入小sum.at(i)，在通过lower_bound(大-k)来找到对应的小，即当前小面积集合中，符合大面积减去小面积小于等于k的更小集合中，小面积最小的那一个
再用 大面积-小面积 即可能答案
注意：当大面积为[0]时，因为没有小面积，所以要先预存一个0进入set中作为假的小面积
用个图来演示就是



class Solution {
public:
    int maxSumSubmatrix(vector<vector<int>>& matrix, int k) {
        int row=matrix.size();
        if (row==0)
            return 0;
        int column=matrix.at(0).size();
        int ans=INT_MIN;
        for (int left=0;left<column;++left)
        {
            vector<int> row_sum(row,0);
            for (int right=left;right<column;++right)
            {
                for (int i=0;i<row;++i)
                    row_sum.at(i)+=matrix.at(i).at(right);
                set<int> helper_set;
                helper_set.insert(0);
                int prefix_row_sum=0;
                for (int i=0;i<row;++i)
                {
                    prefix_row_sum+=row_sum.at(i);
                    auto p=helper_set.lower_bound(prefix_row_sum-k);
                    helper_set.insert(prefix_row_sum);
                    if (p==helper_set.end())
                        continue;
                    else
                    {
                        int temp=prefix_row_sum-(*p);
                        if (temp>ans)
                            ans=temp;
                    }
                }
                if (ans==k)
                    return k;
            }
        }        
        return ans;
    }
};
法四：二分法+剪枝


可否进一步优化连二分都不做了呢？想起了法二中通过优化53题最大子序和的结论优化成O(n)
再大胆一点，是不是在构建row_sum时候直接就可以嵌入53题的代码了呢
就更加提前的剪枝，不用构建row_sum一个O(n) 求最大子序和一个O(n) 然后二分O(nlog(n))
直接构建row_sum+最大子序和O(n)，倘若最大子序和max_sum都符合题意<=k，那就没必要再二分了，因为二分的最大结果也只是max_sum

class Solution {
public:
    int maxSumSubmatrix(vector<vector<int>>& matrix, int k) {
        int row=matrix.size();
        if (row==0)
            return 0;
        int column=matrix.at(0).size();
        int ans=INT_MIN;
        for (int left=0;left<column;++left)
        {
            vector<int> row_sum(row,0);
            for (int right=left;right<column;++right)
            {
                //直接在构建row_sum时嵌入求最大子序和
                int max_sub_sum=0;
                int max_sum=INT_MIN;
                for (int i=0;i<row;++i)
                {
                    row_sum.at(i)+=matrix.at(i).at(right);
                    max_sub_sum+=row_sum.at(i);
                    if (max_sub_sum<=k && ans<max_sub_sum)
                        ans=max_sub_sum;
                    if (max_sub_sum<0)
                        max_sub_sum=0;
                    max_sum=max(max_sum,max_sub_sum);
                }
                if (ans==k)
                    return k;
                if (max_sum<=k)
                    continue;
                set<int> helper_set;
                helper_set.insert(0);
                int prefix_row_sum=0;
                for (int i=0;i<row;++i)
                {
                    prefix_row_sum+=row_sum.at(i);
                    auto p=helper_set.lower_bound(prefix_row_sum-k);
                    helper_set.insert(prefix_row_sum);
                    if (p==helper_set.end())
                        continue;
                    else
                    {
                        int temp=prefix_row_sum-(*p);
                        if (temp>ans)
                            ans=temp;
                    }
                }
                if (ans==k)
                    return k;
            }
        }        
        return ans;
    }
};


*/