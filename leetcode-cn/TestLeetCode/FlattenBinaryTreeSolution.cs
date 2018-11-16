using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// https://leetcode-cn.com/problems/flatten-binary-tree-to-linked-list/
/// 114.二叉树展开为链表
/// 给定一个二叉树，原地将它展开为链表。
/// </summary>
class FlattenBinaryTreeSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public void Flatten(TreeNode root)
    {
        List<TreeNode> list = new List<TreeNode>();

        var current = new TreeNode(0);
        Flatten(root, list);
        foreach (var node in list)
        {
            current.left = null;
            current.right = node;
            current = node;
        }
    }

    private void Flatten( TreeNode root, List<TreeNode> list )
    {
        if (root == null) return;
        list.Add(root);

        Flatten(root.left, list);
        Flatten(root.right, list);
    }
}