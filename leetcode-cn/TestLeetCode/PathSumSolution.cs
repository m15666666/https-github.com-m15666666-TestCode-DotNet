using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// https://leetcode-cn.com/problems/path-sum-ii/
/// 113.路径总和II
/// 给定一个二叉树和一个目标和，找到所有从根节点到叶子节点路径总和等于给定目标和的路径。
/// 说明: 叶子节点是指没有子节点的节点。
/// </summary>
class PathSumSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public IList<IList<int>> PathSum(TreeNode root, int sum )
    {
        List<IList<int>> ret = new List<IList<int>>();
        if (root == null) return ret;

        List<int> list = new List<int>();
        BackTrack(root, list, ret, 0, sum);
        return ret;
    }

    private void BackTrack( TreeNode root, List<int> list, List<IList<int>> ret, int sum, int target )
    {
        if ( root == null ) return;
        var v = root.val;
        sum += v;
        list.Insert(0, v);

        if (root.left == null && root.right == null)
        {
            if (sum == target)
            {
                //var array = list.ToList();
                //array.Reverse();
                int[] array = new int[list.Count];
                int index = list.Count - 1;
                foreach (var value in list) array[index--] = value;

                ret.Add(array);
            }
        }
        else
        {
            BackTrack(root.left, list, ret, sum, target);
            BackTrack(root.right, list, ret, sum, target);
        }

        list.RemoveAt(0);
    }
}