using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个根为 root 的二叉树，每个结点的深度是它到根的最短距离。

如果一个结点在整个树的任意结点之间具有最大的深度，则该结点是最深的。

一个结点的子树是该结点加上它的所有后代的集合。

返回能满足“以该结点为根的子树中包含所有最深的结点”这一条件的具有最大深度的结点。

 

示例：

输入：[3,5,1,6,2,0,8,null,null,7,4]
输出：[2,7,4]
解释：

我们返回值为 2 的结点，在图中用黄色标记。
在图中用蓝色标记的是树的最深的结点。
输入 "[3, 5, 1, 6, 2, 0, 8, null, null, 7, 4]" 是对给定的树的序列化表述。
输出 "[2, 7, 4]" 是对根结点的值为 2 的子树的序列化表述。
输入和输出都具有 TreeNode 类型。
 

提示：

树中结点的数量介于 1 和 500 之间。
每个结点的值都是独一无二的。
*/
/// <summary>
/// https://leetcode-cn.com/problems/smallest-subtree-with-all-the-deepest-nodes/
/// 865. 具有所有最深结点的最小子树
/// 
/// </summary>
class SmallestSubtreeWithAllTheDeepestNodesSolution
{
    public void Test()
    {
        TreeNode root = new TreeNode(3);
        var n5 = root.left = new TreeNode(5);
        var n1 = root.right = new TreeNode(1);
        n5.left = new TreeNode(6);
        var n2 = n5.right = new TreeNode(2);
        n1.left = new TreeNode(0);
        n1.right = new TreeNode(8);
        n2.left = new TreeNode(7);
        n2.right = new TreeNode(4);
        var ret = SubtreeWithAllDeepest(root);
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public TreeNode SubtreeWithAllDeepest(TreeNode root)
    {
        if (root == null) return null;
        
        var left = root.left;
        var right = root.right;
        if (left == null && right == null) return root;

        _maxDepth = 0;
        _depths = new Dictionary<TreeNode, int>();
        if (left != null) Dfs(left, 0);
        if (right != null) Dfs(right, 0);

        return Deepest(root);
    }

    private Dictionary<TreeNode, int> _depths;
    private int _maxDepth;
    private void Dfs(TreeNode node, int parentDepth)
    {
        if (node != null)
        {
            int d = parentDepth + 1;
            
            var left = node.left;
            var right = node.right;
            if(left != null || right != null)
            {
                if (left != null) Dfs(left, d);
                if (right != null) Dfs(right, d);
                return;
            }
            if (_maxDepth < d) _maxDepth = d;
            _depths.Add(node, d);
        }
    }

    private TreeNode Deepest(TreeNode node)
    {
        var left = node.left;
        var right = node.right;
        if (left == null && right == null) return _depths[node] == _maxDepth ? node : null;

        if (left != null) left = Deepest(left);
        if (right != null) right = Deepest(right);
        if (left != null && right != null) return node;
        if (left != null) return left;
        if (right != null) return right;
        return null;
    }
}
/*
方法一： 两次深度优先搜索
思路

最直白的做法，先做一次深度优先搜索标记所有节点的深度来找到最深的节点，再做一次深度优先搜索用回溯法找最小子树。定义第二次深度优先搜索方法为 answer(node)，每次递归有以下四种情况需要处理：

如果 node 没有左右子树，返回 node。

如果 node 左右子树的后代中都有最深节点，返回 node。

如果只有左子树或右子树中有且拥有所有的最深节点，返回这棵子树的根节点（即 node 的左/右孩子）。

否则，当前子树中不存在答案。

算法

先做一次深度优先搜索标记所有节点的深度，再做一次深度优先搜索找到最终答案。

JavaPython
class Solution {
    Map<TreeNode, int> depth;
    int max_depth;
    public TreeNode subtreeWithAllDeepest(TreeNode root) {
        depth = new HashMap();
        depth.put(null, -1);
        dfs(root, null);
        max_depth = -1;
        for (int d: depth.values())
            max_depth = Math.max(max_depth, d);

        return answer(root);
    }

    public void dfs(TreeNode node, TreeNode parent) {
        if (node != null) {
            depth.put(node, depth.get(parent) + 1);
            dfs(node.left, node);
            dfs(node.right, node);
        }
    }

    public TreeNode answer(TreeNode node) {
        if (node == null || depth.get(node) == max_depth)
            return node;
        TreeNode L = answer(node.left),
                 R = answer(node.right);
        if (L != null && R != null) return node;
        if (L != null) return L;
        if (R != null) return R;
        return null;
    }
}
复杂度分析

时间复杂度： O(N)O(N)，其中 NN 为树的大小。

空间复杂度： O(N)O(N)。

方法二： 一次深度优先搜索
思路

可以把 方法一 中两次深度优先搜索合并成一次，定义方法 dfs(node)，与方法一中不同的是 dfs(node) 返回两个值，子树中的答案和 node 节点到最深节点的距离。

算法

dfs(node) 返回的结果有两个部分：

Result.node：包含所有最深节点的最小子树的根节点。
Result.dist：node 到最深节点的距离。
分别计算 dfs(node) 的两个返回结果：

对于 Result.node：

如果只有一个 childResult 具有最深节点，返回 childResult.node。

如果两个孩子都有最深节点，返回 node。

Result.dist 为 childResult.dist 加 1。

JavaPython
class Solution {
    public TreeNode subtreeWithAllDeepest(TreeNode root) {
        return dfs(root).node;
    }

    // Return the result of the subtree at this node.
    public Result dfs(TreeNode node) {
        if (node == null) return new Result(null, 0);
        Result L = dfs(node.left),
               R = dfs(node.right);
        if (L.dist > R.dist) return new Result(L.node, L.dist + 1);
        if (L.dist < R.dist) return new Result(R.node, R.dist + 1);
        return new Result(node, L.dist + 1);
    }
}

class Result {
    TreeNode node;
    int dist;
    Result(TreeNode n, int d) {
        node = n;
        dist = d;
    }
}
复杂度分析

时间复杂度： O(N)O(N)，其中 NN 为树的大小。

空间复杂度： O(N)O(N)。

public class Solution {
    public TreeNode SubtreeWithAllDeepest(TreeNode root) {
        if (root == null)
            return null;
        
        if (root.left == null && root.right == null)
            return root;
        
        List<TreeNode> layer = new List<TreeNode>();
        layer.Add(root);
        Dictionary<TreeNode, TreeNode> child2Parent = new Dictionary<TreeNode, TreeNode>();
        List<TreeNode> lastLayer = null;
        
        while (layer.Any())
        {
            List<TreeNode> children = new List<TreeNode>();
            
            foreach (var node in layer)
            {
                if (node.left != null)
                {
                    children.Add(node.left);
                    child2Parent[node.left] = node;
                }
                
                if (node.right != null)
                {
                    children.Add(node.right);
                    child2Parent[node.right] = node;
                }
            }
            
            if (!children.Any())
                lastLayer = layer;
            
            layer = children;
        }
        
        if (lastLayer.Count == 1)
            return lastLayer[0];
        
        HashSet<TreeNode> parents = new HashSet<TreeNode>();
        foreach (var node in lastLayer)
            parents.Add(child2Parent[node]);
        
        while (parents.Count > 1)
        {
            HashSet<TreeNode> ancestors = new HashSet<TreeNode>();
            
            foreach (var parent in parents)
                ancestors.Add(child2Parent[parent]);
            
            parents = ancestors;
        }
        
        return parents.ToArray()[0];
    }
}
*/
