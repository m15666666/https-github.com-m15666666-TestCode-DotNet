using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个二叉树和一个目标和，找到所有从根节点到叶子节点路径总和等于给定目标和的路径。

说明: 叶子节点是指没有子节点的节点。

示例:
给定如下二叉树，以及目标和 sum = 22，

              5
             / \
            4   8
           /   / \
          11  13  4
         /  \    / \
        7    2  5   1
返回:

[
   [5,4,11,2],
   [5,8,4,5]
]
 
 
*/
/// <summary>
/// https://leetcode-cn.com/problems/path-sum-ii/
/// 113.路径总和II
/// 
/// 给定一个二叉树和一个目标和，找到所有从根节点到叶子节点路径总和等于给定目标和的路径。
/// 说明: 叶子节点是指没有子节点的节点。
/// </summary>
class PathSumIISolution
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

        Stack<int> stack = new Stack<int>();
        BackTrack(root, 0, sum);
        return ret;

        void BackTrack( TreeNode root, int sum, int target )
        {
            if ( root == null ) return;
            var v = root.val;
            sum += v;

            stack.Push(v);

            if (root.left == null && root.right == null)
            {
                if (sum == target)
                {
                    int[] array = new int[stack.Count];
                    int index = array.Length - 1;
                    foreach (var value in stack) array[index--] = value;

                    ret.Add(array);
                }
            }
            else
            {
                BackTrack(root.left, sum, target);
                BackTrack(root.right, sum, target);
            }

            stack.Pop();
        }
    }
    //public IList<IList<int>> PathSum(TreeNode root, int sum )
    //{
    //    List<IList<int>> ret = new List<IList<int>>();
    //    if (root == null) return ret;

    //    List<int> list = new List<int>();
    //    BackTrack(root, list, ret, 0, sum);
    //    return ret;
    //}

    //private void BackTrack( TreeNode root, List<int> list, List<IList<int>> ret, int sum, int target )
    //{
    //    if ( root == null ) return;
    //    var v = root.val;
    //    sum += v;
    //    list.Insert(0, v);

    //    if (root.left == null && root.right == null)
    //    {
    //        if (sum == target)
    //        {
    //            //var array = list.ToList();
    //            //array.Reverse();
    //            int[] array = new int[list.Count];
    //            int index = list.Count - 1;
    //            foreach (var value in list) array[index--] = value;

    //            ret.Add(array);
    //        }
    //    }
    //    else
    //    {
    //        BackTrack(root.left, list, ret, sum, target);
    //        BackTrack(root.right, list, ret, sum, target);
    //    }

    //    list.RemoveAt(0);
    //}
}
/*
public class Solution {
    public IList<IList<int>> PathSum(TreeNode root, int sum)
    {
        List<IList<int>> result = new List<IList<int>>();
        if (root == null)
            return result;

        List<int> temp = new List<int>();
        temp.Add((int)root.val);
        Dfs(root, sum, (int)root.val, temp, result);
        return result;
    }

    private void Dfs(TreeNode node, int sum, int currSum, List<int> temp, List<IList<int>> res)
    {
        if (node.left == null && node.right == null && currSum == sum)
        {
            res.Add(new List<int>(temp));
        } else 
        {
            if (node.left != null)
            {
                temp.Add((int)node.left.val);
                Dfs(node.left, sum, currSum + (int)node.left.val, temp, res);
                temp.RemoveAt(temp.Count - 1);
            }

            if (node.right != null)
            {
                temp.Add((int)node.right.val);
                Dfs(node.right, sum, currSum + (int)node.right.val, temp, res);
                temp.RemoveAt(temp.Count - 1);
            }
        }
    }
}


 
 
*/