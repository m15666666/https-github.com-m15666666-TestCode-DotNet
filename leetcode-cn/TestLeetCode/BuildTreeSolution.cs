using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// https://leetcode-cn.com/problems/construct-binary-tree-from-preorder-and-inorder-traversal/
/// 105.从前序与中序遍历序列构造二叉树
/// 根据一棵树的前序遍历与中序遍历构造二叉树。
/// https://www.jianshu.com/p/4ef1f50d45b5
/// http://www.bubuko.com/infodetail-2638838.html
/// </summary>
class BuildTreeSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public TreeNode BuildTree(int[] preorder, int[] inorder)
    {
        if (preorder == null || preorder.Length == 0 || inorder == null || inorder.Length != preorder.Length) return null;
        return BuildTree(preorder, 0, preorder.Length - 1, inorder, 0, inorder.Length - 1);
    }

    private TreeNode BuildTree(int[] preorder, int pStart, int pStop, int[] inorder, int iStart, int iStop)
    {
        if ( pStop < pStart || iStop < iStart ) return null;

        var v = preorder[pStart];
        int index;
        for (index = iStart; index <= iStop; index++)
            if (v == inorder[index]) break;

        TreeNode ret = new TreeNode( v );
        var leftTreeNodeCount = index - iStart;
        var leftTreeStop = pStart + leftTreeNodeCount;
        ret.left = BuildTree( preorder, pStart + 1, leftTreeStop, inorder, iStart, index - 1);
        ret.right = BuildTree( preorder, leftTreeStop + 1, pStop, inorder, index + 1, iStop );
        return ret;
    }
}