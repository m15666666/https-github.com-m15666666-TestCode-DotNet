using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// https://leetcode-cn.com/problems/sum-root-to-leaf-numbers/
/// 129.求根到叶子节点数字之和
/// 给定一个二叉树，它的每个结点都存放一个 0-9 的数字，每条从根到叶子节点的路径都代表一个数字。
/// 例如，从根到叶子节点路径 1->2->3 代表数字 123。
/// 计算从根到叶子节点生成的所有数字之和。
/// 说明: 叶子节点是指没有子节点的节点。
/// </summary>
class SumNumbersOfRoot2LeafSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public int SumNumbers(TreeNode root)
    {
        int sum = 0;
        SumNumbers(root, 0, ref sum);
        return sum;
    }

    private void SumNumbers(TreeNode root, int parentSum, ref int sum )
    {
        if (root == null) return;
        parentSum = parentSum * 10 + root.val;

        if( root.left == null && root.right == null)
        {
            sum += parentSum;
            return;
        }

        SumNumbers(root.left, parentSum, ref sum);
        SumNumbers(root.right, parentSum, ref sum);
    }
}