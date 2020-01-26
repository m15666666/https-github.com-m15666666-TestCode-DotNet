using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定二叉树的根节点 root，找出存在于不同节点 A 和 B 之间的最大值 V，其中 V = |A.val - B.val|，且 A 是 B 的祖先。

（如果 A 的任何子节点之一为 B，或者 A 的任何子节点是 B 的祖先，那么我们认为 A 是 B 的祖先）

 

示例：



输入：[8,3,10,1,6,null,14,null,null,4,7,13]
输出：7
解释： 
我们有大量的节点与其祖先的差值，其中一些如下：
|8 - 3| = 5
|3 - 7| = 4
|8 - 1| = 7
|10 - 13| = 3
在所有可能的差值中，最大值 7 由 |8 - 1| = 7 得出。
 

提示：

树中的节点数在 2 到 5000 之间。
每个节点的值介于 0 到 100000 之间。
*/
/// <summary>
/// https://leetcode-cn.com/problems/maximum-difference-between-node-and-ancestor/
/// 1026. 节点与其祖先之间的最大差值
/// 
/// </summary>
class MaximumDifferenceBetweenNodeAndAncestorSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int MaxAncestorDiff(TreeNode root)
    {
        Dfs(root);
        return _ret;
    }

    private void Dfs(TreeNode node)
    {
        if (node == null) return;

        int preMax = _max;
        int preMin = _min;

        var v = node.val;
        if (_max < v) _max = v;
        if (v < _min) _min = v;
        
        if (node.left == null && node.right == null)
        {
            var diff = _max - _min;
            if (_ret < diff) _ret = diff;
        }
        if (node.left != null) Dfs(node.left);
        if (node.right != null) Dfs(node.right);

        _max = preMax;
        _min = preMin;
    }

    private int _ret = 0;
    private int _max = int.MinValue;
    private int _min = int.MaxValue;
}
/*
通用树形DFS
斩空
126 阅读
class Solution {
    private int ans = 0;
    private int tmpMax = Integer.MIN_VALUE;
    private int tmpMin = Integer.MAX_VALUE;

    public int maxAncestorDiff(TreeNode root) {
        dfs(root);
        return ans;
    }

    private void dfs(TreeNode node) {
        if (node == null) {
            return;
        }
        int preMax = tmpMax, preMin = tmpMin;
        tmpMax = Math.max(tmpMax, node.val);
        tmpMin = Math.min(tmpMin, node.val);
        if (node.left == null && node.right == null) {
            ans = Math.max(ans, tmpMax - tmpMin);
        }
        if (node.left != null) {
            dfs(node.left);
        }
        if (node.right != null) {
            dfs(node.right);
        }
        tmpMax = preMax;
        tmpMin = preMin;
    }
}

public class Solution {
    int ans;
    private void Dfs(TreeNode node, int max, int min) {
        if (node == null) { return; }
        ans = Math.Max(Math.Max(Math.Abs(max - node.val), Math.Abs(min - node.val)), ans);
        Dfs(node.left, Math.Max(max, node.val), Math.Min(min, node.val));
        Dfs(node.right, Math.Max(max, node.val), Math.Min(min, node.val));
    }
    public int MaxAncestorDiff(TreeNode root) {
        ans = 0;
        Dfs(root, root.val, root.val);
        return ans;
    }
} 
*/
