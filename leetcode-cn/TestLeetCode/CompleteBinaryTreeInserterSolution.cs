using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
完全二叉树是每一层（除最后一层外）都是完全填充（即，结点数达到最大）的，并且所有的结点都尽可能地集中在左侧。

设计一个用完全二叉树初始化的数据结构 CBTInserter，它支持以下几种操作：

CBTInserter(TreeNode root) 使用头结点为 root 的给定树初始化该数据结构；
CBTInserter.insert(int v) 将 TreeNode 插入到存在值为 node.val = v  的树中以使其保持完全二叉树的状态，并返回插入的 TreeNode 的父结点的值；
CBTInserter.get_root() 将返回树的头结点。
 

示例 1：

输入：inputs = ["CBTInserter","insert","get_root"], inputs = [[[1]],[2],[]]
输出：[null,1,[1,2]]
示例 2：

输入：inputs = ["CBTInserter","insert","insert","get_root"], inputs = [[[1,2,3,4,5,6]],[7],[8],[]]
输出：[null,3,4,[1,2,3,4,5,6,7,8]]
 

提示：

最初给定的树是完全二叉树，且包含 1 到 1000 个结点。
每个测试用例最多调用 CBTInserter.insert  操作 10000 次。
给定结点或插入结点的每个值都在 0 到 5000 之间。
*/
/// <summary>
/// https://leetcode-cn.com/problems/complete-binary-tree-inserter/
/// 919. 完全二叉树插入器
/// 
/// </summary>
class CompleteBinaryTreeInserterSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public CompleteBinaryTreeInserterSolution(TreeNode root)
    {
        _root = root;
        Queue<TreeNode> queue = new Queue<TreeNode>();
        queue.Enqueue(root);

        // BFS to populate deque
        while (0 < queue.Count)
        {
            TreeNode node = queue.Dequeue();
            if (node.left == null || node.right == null) _deque.Add(node);
            if (node.left != null) queue.Enqueue(node.left);
            if (node.right != null) queue.Enqueue(node.right);
        }
    }

    private TreeNode _root;
    private List<TreeNode> _deque = new List<TreeNode>();
    public int Insert(int v)
    {
        TreeNode node = _deque.FirstOrDefault();
        if (node == null) return -1;

        var n = new TreeNode(v);
        _deque.Add(n);
        if (node.left == null)
            node.left = n;
        else
        {
            node.right = n;
            _deque.RemoveAt(0);
        }

        return node.val;
    }

    public TreeNode Get_root()
    {
        return _root;
    }
}
/*
方法 1：双端队列
想法

将所有节点编号，按照从上到下从左到右的顺序。

在每个插入步骤中，我们希望插入到一个编号最小的节点（这样有 0 或者 1 个孩子）。

通过维护一个 deque （双端队列），保存这些节点的编号，我们可以解决这个问题。插入一个节点之后，将成为最高编号的节点，并且没有孩子，所以插入到队列的后端。为了找到最小数字的节点，我们从队列前端弹出元素。

算法

首先，通过广度优先搜索将 deque 中插入含有 0 个或者 1 个孩子的节点编号。

然后插入节点，父亲是 deque 的第一个元素，我们将新节点加入我们的 deque。

JavaPython
class CBTInserter {
    TreeNode root;
    Deque<TreeNode> deque;
    public CBTInserter(TreeNode root) {
        this.root = root;
        deque = new LinkedList();
        Queue<TreeNode> queue = new LinkedList();
        queue.offer(root);

        // BFS to populate deque
        while (!queue.isEmpty()) {
            TreeNode node = queue.poll();
            if (node.left == null || node.right == null)
                deque.offerLast(node);
            if (node.left != null)
                queue.offer(node.left);
            if (node.right != null)
                queue.offer(node.right);
        }
    }

    public int insert(int v) {
        TreeNode node = deque.peekFirst();
        deque.offerLast(new TreeNode(v));
        if (node.left == null)
            node.left = deque.peekLast();
        else {
            node.right = deque.peekLast();
            deque.pollFirst();
        }

        return node.val;
    }

    public TreeNode get_root() {
        return root;
    }
}
复杂度分析

时间复杂度：预处理 O(N)O(N)，其中 NN 是树上节点编号。每个插入步骤是 O(1)O(1)。
空间复杂度：O(N_\text{cur})O(N 
cur
​	
 )，其中当前插入操作树的大小为 N_{\text{cur}}N 
cur
​	
public class CBTInserter {

    TreeNode root = null;
        Queue<TreeNode> queue = new Queue<TreeNode>();

        public CBTInserter(TreeNode root)
        {
            this.root = root;
            queue.Enqueue(root);
        }

        public int Insert(int v)
        {                    
            TreeNode current;            
            while(queue.Count>0)
            {
                for (int i = 0; i < queue.Count; i++)
                {
                    current = queue.Peek();
                    if(current.left!=null)
                    {
                        queue.Enqueue(current.left);
                    }
                    else
                    {
                        current.left = new TreeNode(v);
                        queue.Enqueue(current.left);
                        return current.val;
                    }
                    if(current.right!=null)
                    {
                        queue.Enqueue(current.right);
                        queue.Dequeue();
                    }
                    else
                    {
                        current.right = new TreeNode(v);
                        queue.Enqueue(current.right);
                        return current.val;
                    }
                }
            }
            return 0;
        }

        public TreeNode Get_root()
        {
            return root;
        }
}
 
*/
