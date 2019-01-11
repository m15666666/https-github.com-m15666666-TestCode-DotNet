using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// https://leetcode-cn.com/problems/lowest-common-ancestor-of-a-binary-tree/
/// 236. 二叉树的最近公共祖先
/// 给定一个二叉树, 找到该树中两个指定节点的最近公共祖先。
/// 百度百科中最近公共祖先的定义为：“对于有根树 T 的两个结点 p、q，
/// 最近公共祖先表示为一个结点 x，满足 x 是 p、q 的祖先且 x 的深度尽可能大
/// （一个节点也可以是它自己的祖先）。”
/// https://blog.csdn.net/xuchonghao/article/details/80688166
/// </summary>
class LowestCommonAncestorOfaBinaryTreeSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public TreeNode LowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q)
    {
        if (root == null || root.val == p.val || root.val == q.val)
            return root;
        TreeNode leftN = LowestCommonAncestor(root.left, p, q);
        TreeNode rightN = LowestCommonAncestor(root.right, p, q);
        if (leftN != null && rightN != null)
            return root;
        return leftN == null ? rightN : leftN;
    }
}
/*
//别人的算法
public class Solution {
    public TreeNode LowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q) {
        if(root == null || root == p || root == q)
            return root;
        TreeNode left = LowestCommonAncestor(root.left, p, q);
        TreeNode right = LowestCommonAncestor(root.right, p, q);

        if(left == null && right == null) return null;
        else if(left != null && right != null) return root;
        else return left == null ? right : left;
    }
}
     
     
*/
