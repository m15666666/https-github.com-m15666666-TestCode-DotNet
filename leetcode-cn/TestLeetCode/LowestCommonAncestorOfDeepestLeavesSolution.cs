using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给你一个有根节点的二叉树，找到它最深的叶节点的最近公共祖先。

回想一下：

叶节点 是二叉树中没有子节点的节点
树的根节点的 深度 为 0，如果某一节点的深度为 d，那它的子节点的深度就是 d+1
如果我们假定 A 是一组节点 S 的 最近公共祖先，S 中的每个节点都在以 A 为根节点的子树中，且 A 的深度达到此条件下可能的最大值。
 

示例 1：

输入：root = [1,2,3]
输出：[1,2,3]
示例 2：

输入：root = [1,2,3,4]
输出：[4]
示例 3：

输入：root = [1,2,3,4,5]
输出：[2,4,5]
 

提示：

给你的树中将有 1 到 1000 个节点。
树中每个节点的值都在 1 到 1000 之间。
*/
/// <summary>
/// https://leetcode-cn.com/problems/lowest-common-ancestor-of-deepest-leaves/
/// 1123. 最深叶节点的最近公共祖先
/// 
/// </summary>
class LowestCommonAncestorOfDeepestLeavesSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public TreeNode LcaDeepestLeaves(TreeNode root)
    {
        if (root == null) return null;

        maxDepth = -1;
        Dfs(root, maxDepth);
        return node;
    }

    private TreeNode node;
    private int maxDepth;
    private int Dfs(TreeNode root, int depth)
    {
        if (root == null) return depth;

        depth++;
        int left = Dfs(root.left, depth);
        int right = Dfs(root.right, depth);
        depth = Math.Max(left, right);

        if (left == right && maxDepth <= depth )
        {
            node = root;
            maxDepth = depth;
        }
        return depth;
    }
}
/*
简单的后序遍历
hundanLi
发布于 3 个月前
289 阅读
class Solution {
    private TreeNode node;
    private int maxDepth;
    public TreeNode lcaDeepestLeaves(TreeNode root) {
        if (root == null) {
            return null;
        }
        dfs(root, 0);
        return node;
    }

    private int dfs(TreeNode root, int depth) {
        if (root == null) {
            return depth;
        }
        depth++;
        int left = dfs(root.left, depth), right = dfs(root.right, depth);
        depth = Math.max(left, right);
        if (left == right && depth >= maxDepth) {
            node = root;
            maxDepth = depth;
        }
        return depth;
    }
}

public class Solution {
    public TreeNode LcaDeepestLeaves(TreeNode root) {
        return GetLCAHelper(root, GetMaxDepth(root));
    }
    
    //Get the depth of the tree
    private int GetMaxDepth(TreeNode node)
    {
        if(node == null)
            return 0;
    
        return 1 + Math.Max(GetMaxDepth(node.left), GetMaxDepth(node.right));
    }
    
    private TreeNode GetLCAHelper(TreeNode root, int maxDepth)
    {
        if(root == null)
            return null;
        if(maxDepth == 1)
            return root;
        
        TreeNode left = GetLCAHelper(root.left, maxDepth - 1);
        TreeNode right = GetLCAHelper(root.right, maxDepth - 1);
        
        if(left != null && right != null)
            return root;
        
        return left ?? right;
    }
} 
*/
