using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定二叉搜索树（BST）的根节点和要插入树中的值，将值插入二叉搜索树。 返回插入后二叉搜索树的根节点。 保证原始二叉搜索树中不存在新值。

注意，可能存在多种有效的插入方式，只要树在插入后仍保持为二叉搜索树即可。 你可以返回任意有效的结果。

例如, 

给定二叉搜索树:

        4
       / \
      2   7
     / \
    1   3

和 插入的值: 5
你可以返回这个二叉搜索树:

         4
       /   \
      2     7
     / \   /
    1   3 5
或者这个树也是有效的:

         5
       /   \
      2     7
     / \   
    1   3
         \
          4
*/
/// <summary>
/// https://leetcode-cn.com/problems/insert-into-a-binary-search-tree/
/// 701. 二叉搜索树中的插入操作
/// https://www.cnblogs.com/xiagnming/p/9706692.html
/// </summary>
class InsertIntoABinarySearchTreeSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public TreeNode InsertIntoBST(TreeNode root, int val)
    {
        if (root == null) { return new TreeNode(val); }
        TreeNode cur = root;
        TreeNode pre = null;
        while (cur != null)
        {
            pre = cur;
            if (cur.val > val)
            {
                cur = cur.left;
                if (cur == null)
                {
                    pre.left = new TreeNode(val);
                }
            }
            else
            {
                cur = cur.right;
                if (cur == null)
                {
                    pre.right = new TreeNode(val);
                }
            }
        }
        return root;
    }
}
/*
public class Solution {
    public TreeNode InsertIntoBST(TreeNode root, int val) {
        TreeNode f=root;
        TreeNode t=root;
        while(t!=null)
        {
            f=t;
            if(t.val<val)
                t=t.right;
            else
                t=t.left;
        }
        TreeNode n=new TreeNode(val);
        if(f.val>val)
            f.left=n;
        else f.right=n;
        return root;
    }
}
public class Solution
{
    public TreeNode InsertIntoBST(TreeNode root, int val)
    {
        if (root is null)
            return new TreeNode(val);
        
        if (root.val < val)
            root.right = InsertIntoBST(root.right, val);
        else
            root.left = InsertIntoBST(root.left, val);
        
        return root;
    }
}
*/
