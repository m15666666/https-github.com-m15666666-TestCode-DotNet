using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// https://leetcode-cn.com/problems/binary-tree-inorder-traversal/
/// 94.二叉树的中序遍历
/// 给定一个二叉树，返回它的中序 遍历。
/// </summary>
class InorderTraversalSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    # region 迭代方式
    public IList<int> InorderTraversal(TreeNode root)
    {
        List<int> ret = new List<int>();
        if (root == null) return ret;

        Stack<TreeNode> stack = new Stack<TreeNode>(100);

        while( true )
        {
            if (root != null)
            {
                stack.Push(root);
                root = root.left;
                continue;
            }

            if (stack.Count == 0) break;

            root = stack.Pop();
            ret.Add(root.val);

            root = root.right;
        }
        
        return ret;
    }
    #endregion

    #region 递归方式
    //public IList<int> InorderTraversal(TreeNode root)
    //{
    //    List<int> ret = new List<int>();
    //    if (root == null) return ret;
    //    InorderTraversal(root, ret);
    //    return ret;
    //}

    //private void InorderTraversal( TreeNode root, List<int> ret )
    //{
    //    if (root.left != null) InorderTraversal(root.left, ret);
    //    ret.Add(root.val);
    //    if (root.right != null) InorderTraversal(root.right, ret);
    //}
    #endregion
}