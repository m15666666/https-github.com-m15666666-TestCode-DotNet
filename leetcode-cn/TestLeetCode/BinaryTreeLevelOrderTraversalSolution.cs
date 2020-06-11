using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给你一个二叉树，请你返回其按 层序遍历 得到的节点值。 （即逐层地，从左到右访问所有节点）。

 

示例：
二叉树：[3,9,20,null,null,15,7],

    3
   / \
  9  20
    /  \
   15   7
返回其层次遍历结果：

[
  [3],
  [9,20],
  [15,7]
]


*/
/// <summary>
/// https://leetcode-cn.com/problems/binary-tree-level-order-traversal/
/// 102. 二叉树的层序遍历
/// 
/// 
/// 
/// 
/// </summary>
class BinaryTreeLevelOrderTraversalSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public IList<IList<int>> LevelOrder(TreeNode root) {
        List<IList<int>> ret = new List<IList<int>>();

        if (root == null) return ret;
        Queue<TreeNode> queue = new Queue<TreeNode>();
        queue.Enqueue(root);

        while(0 < queue.Count)
        {
            int size = queue.Count;
            List<int> set = new List<int>();
            ret.Add(set);
            for(int i = 0; i < size; i++)
            {
                var item = queue.Dequeue();
                set.Add(item.val);
                if (item.left != null) queue.Enqueue(item.left);
                if (item.right != null) queue.Enqueue(item.right);
            }
        }

        return ret;
    }
}
/*

二叉树的层序遍历
力扣官方题解
发布于 2020-05-11
16.7k
📺视频题解

📖文字题解
方法一：宽度优先搜索
思路和算法

我们可以用宽度优先搜索解决这个问题。

我们可以想到最朴素的方法是用一个二元组 (node, level) 来表示状态，它表示某个节点和它所在的层数，每个新进队列的节点的 level 值都是父亲节点的 level 值加一。最后根据每个点的 level 对点进行分类，分类的时候我们可以利用哈希表，维护一个以 level 为键，对应节点值组成的数组为值，宽度优先搜索结束以后按键 level 从小到大取出所有值，组成答案返回即可。

考虑如何优化空间开销：如何不用哈希映射，并且只用一个变量 node 表示状态，实现这个功能呢？

我们可以用一种巧妙的方法修改 BFS：

首先根元素入队
当队列不为空的时候
求当前队列的长度 s_is 
i
​	
 
依次从队列中取 s_is 
i
​	
  个元素进行拓展，然后进入下一次迭代
它和 BFS 的区别在于 BFS 每次只取一个元素拓展，而这里每次取 s_is 
i
​	
  个元素。在上述过程中的第 ii 次迭代就得到了二叉树的第 ii 层的 s_is 
i
​	
  个元素。

为什么这么做是对的呢？我们观察这个算法，可以归纳出这样的循环不变式：第 ii 次迭代前，队列中的所有元素就是第 ii 层的所有元素，并且按照从左向右的顺序排列。证明它的三条性质（你也可以把它理解成数学归纳法）：

初始化：i = 1i=1 的时候，队列里面只有 root，是唯一的层数为 11 的元素，因为只有一个元素，所以也显然满足「从左向右排列」；
保持：如果 i = ki=k 时性质成立，即第 kk 轮中出队 s_ks 
k
​	
  的元素是第 kk 层的所有元素，并且顺序从左到右。因为对树进行 BFS 的时候由低 kk 层的点拓展出的点一定也只能是 k + 1k+1 层的点，并且 k + 1k+1 层的点只能由第 kk 层的点拓展到，所以由这 s_ks 
k
​	
  个点能拓展到下一层所有的 s_{k+1}s 
k+1
​	
  个点。又因为队列的先进先出（FIFO）特性，既然第 kk 层的点的出队顺序是从左向右，那么第 k + 1k+1 层也一定是从左向右。至此，我们已经可以通过数学归纳法证明循环不变式的正确性。
终止：因为该循环不变式是正确的，所以按照这个方法迭代之后每次迭代得到的也就是当前层的层次遍历结果。至此，我们证明了算法是正确的。
代码


class Solution {
public:
    vector<vector<int>> levelOrder(TreeNode* root) {
        vector <vector <int>> ret;
        if (!root) return ret;

        queue <TreeNode*> q;
        q.push(root);
        while (!q.empty()) {
            int currentLevelSize = q.size();
            ret.push_back(vector <int> ());
            for (int i = 1; i <= currentLevelSize; ++i) {
                auto node = q.front(); q.pop();
                ret.back().push_back(node->val);
                if (node->left) q.push(node->left);
                if (node->right) q.push(node->right);
            }
        }
        
        return ret;
    }
};
复杂度分析

记树上所有节点的个数为 nn。

时间复杂度：每个点进队出队各一次，故渐进时间复杂度为 O(n)O(n)。
空间复杂度：队列中元素的个数不超过 nn 个，故渐进空间复杂度为 O(n)O(n)。

public class Solution 
{
    
    public IList<IList<int>> LevelOrder(TreeNode root)
    {
        IList<IList<int>> res = new List<IList<int>>();
        
        DFS(root,0);
        
        void DFS(TreeNode node,int level)
        {
            if(node==null) return;
            if(res.Count==level)
            {
                List<int> lst = new List<int>();
                res.Add(lst);
            }
            
            res[level].Add(node.val);
            DFS(node.left,level+1);
            DFS(node.right,level+1);
        }
        
        return res;
    }
}

public class Solution {
    public IList<IList<int>> LevelOrder(TreeNode root) {
    List<IList<int>> res = new List<IList<int>>();
    Queue<TreeNode> que = new Queue<TreeNode>();

    if (root!=null)
    {
        que.Enqueue(root);
    } 
    while (que.Count!=0)
    {
        int n = que.Count;
        List<int> level = new List<int>();
        
        for (int i = 0; i < n; i++)
        {
            TreeNode temp = que.Dequeue();
            level.Add(temp.val);
            if (temp.left!=null)
            {
                que.Enqueue(temp.left);
            } 
            if (temp.right!=null)
            {
                que.Enqueue(temp.right);
            }
        }
        res.Add(level);
    }
    return res;
    }
} 
 
 
 
*/
