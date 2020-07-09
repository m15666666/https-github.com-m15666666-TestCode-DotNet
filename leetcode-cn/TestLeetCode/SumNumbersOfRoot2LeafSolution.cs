using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个二叉树，它的每个结点都存放一个 0-9 的数字，每条从根到叶子节点的路径都代表一个数字。

例如，从根到叶子节点路径 1->2->3 代表数字 123。

计算从根到叶子节点生成的所有数字之和。

说明: 叶子节点是指没有子节点的节点。

示例 1:

输入: [1,2,3]
    1
   / \
  2   3
输出: 25
解释:
从根到叶子节点路径 1->2 代表数字 12.
从根到叶子节点路径 1->3 代表数字 13.
因此，数字总和 = 12 + 13 = 25.
示例 2:

输入: [4,9,0,5,1]
    4
   / \
  9   0
 / \
5   1
输出: 1026
解释:
从根到叶子节点路径 4->9->5 代表数字 495.
从根到叶子节点路径 4->9->1 代表数字 491.
从根到叶子节点路径 4->0 代表数字 40.
因此，数字总和 = 495 + 491 + 40 = 1026.
 
 
*/
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
/*
public class Solution {
    int dfs(TreeNode r, int s) {
        if (r == null) return 0;
        s = s * 10 + r.val;
        if (r.left == null && r.right == null) return s;
        return dfs(r.left,s) + dfs(r.right,s);
    }
    public int SumNumbers(TreeNode root) {
        return dfs(root, 0);
    }
} 
 
*/