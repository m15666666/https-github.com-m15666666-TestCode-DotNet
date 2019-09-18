using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定二叉树根结点 root ，此外树的每个结点的值要么是 0，要么是 1。

返回移除了所有不包含 1 的子树的原二叉树。

( 节点 X 的子树为 X 本身，以及所有 X 的后代。)

示例1:
输入: [1,null,0,0,1]
输出: [1,null,0,null,1]
 
解释: 
只有红色节点满足条件“所有不包含 1 的子树”。
右图为返回的答案。

示例2:
输入: [1,0,1,0,0,0,1]
输出: [1,null,1,null,1]

示例3:
输入: [1,1,0,1,1,0,1,0]
输出: [1,1,0,1,1,null,1]

说明:

给定的二叉树最多有 100 个节点。
每个节点的值只会为 0 或 1 。
*/
/// <summary>
/// https://leetcode-cn.com/problems/binary-tree-pruning/
/// 814. 二叉树剪枝
/// https://www.cnblogs.com/xiagnming/p/9541554.html
/// </summary>
class BinaryTreePruningSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public TreeNode PruneTree(TreeNode root)
    {
        if (root == null) { return null; }
        root.left = PruneTree(root.left);
        root.right = PruneTree(root.right);
        if (root.left == null && root.right == null && root.val == 0) { return null; }
        return root;
    }
}
/*
public class Solution {
    public TreeNode PruneTree(TreeNode root) 
    {
        Travelsal(root);
        Travelsal2(root);
        return root;
    }
    
    public int Travelsal(TreeNode node)
    {
        if (node == null)
        {
            return 0;
        }
        int i1 = Travelsal(node.left);
        int i2 = Travelsal(node.right);
        node.val += i1 + i2;
        if (i1 == 0)
        {
            node.left = null;
        }
        if (i2 == 0)
        {
            node.right = null;
        }
        return node.val;
    }
    
    public void Travelsal2(TreeNode node)
    {
        if (node == null)
        {
            return;
        }
        int childValue = 0;
        if (node.left != null)
        {
            childValue += node.left.val;
        }
        if (node.right != null)
        {
            childValue += node.right.val;
        }
        if(node.val == childValue)
        {
            node.val = 0;
        }
        else
        {
            node.val = 1;
        }
        Travelsal2(node.left);
        Travelsal2(node.right);
    }
}

*/
