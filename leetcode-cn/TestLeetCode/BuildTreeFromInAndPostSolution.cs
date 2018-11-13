using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// https://leetcode-cn.com/problems/construct-binary-tree-from-inorder-and-postorder-traversal/
/// 106.从中序与后序遍历序列构造二叉树
/// 根据一棵树的中序遍历与后序遍历构造二叉树。
/// 注意:你可以假设树中没有重复的元素。
/// </summary>
class BuildTreeFromInAndPostSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public TreeNode BuildTree(int[] inorder, int[] postorder)
    {
        if (postorder == null || postorder.Length == 0 || inorder == null || inorder.Length != postorder.Length) return null;
        return BuildTree(postorder, 0, postorder.Length - 1, inorder, 0, inorder.Length - 1);
    }

    private TreeNode BuildTree(int[] postorder, int pStart, int pStop, int[] inorder, int iStart, int iStop)
    {
        if (pStop < pStart || iStop < iStart) return null;

        var v = postorder[pStop];
        int index;
        for (index = iStart; index <= iStop; index++)
            if (v == inorder[index]) break;

        TreeNode ret = new TreeNode(v);
        var rightTreeNodeCount = iStop - index;
        var rightTreeStart = pStop - rightTreeNodeCount;
        ret.left = BuildTree(postorder, pStart, rightTreeStart - 1, inorder, iStart, index - 1);
        ret.right = BuildTree(postorder, rightTreeStart, pStop - 1, inorder, index + 1, iStop);
        return ret;
    }
}