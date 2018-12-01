using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// https://leetcode-cn.com/problems/binary-tree-preorder-traversal/
/// 144. 二叉树的前序遍历
/// 给定一个二叉树，返回它的 前序 遍历。
/// </summary>
class BinaryTreePreorderTraversalSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public IList<int> PreorderTraversal(TreeNode root)
    {
        List<int> ret = new List<int>();
        PreorderTraversal(root, ret);
        return ret;
    }

    private void PreorderTraversal( TreeNode root, IList<int> ret )
    {
        if (root == null) return;
        ret.Add(root.val);

        PreorderTraversal(root.left, ret);
        PreorderTraversal(root.right, ret);
    }
}