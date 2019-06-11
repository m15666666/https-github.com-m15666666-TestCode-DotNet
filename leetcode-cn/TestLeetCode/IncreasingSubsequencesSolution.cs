using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个整型数组, 你的任务是找到所有该数组的递增子序列，递增子序列的长度至少是2。

示例:

输入: [4, 6, 7, 7]
输出: [[4, 6], [4, 7], [4, 6, 7], [4, 6, 7, 7], [6, 7], [6, 7, 7], [7,7], [4,7,7]]
说明:

给定数组的长度不会超过15。
数组中的整数范围是 [-100,100]。
给定数组中可能包含重复数字，相等的数字应该被视为递增的一种情况。 
*/
/// <summary>
/// https://leetcode-cn.com/problems/increasing-subsequences/
/// 491. 递增子序列
/// https://blog.csdn.net/gl486546/article/details/79784081
/// </summary>
class IncreasingSubsequencesSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public IList<IList<int>> FindSubsequences(int[] nums)
    {
        _nodes.Clear();

        List<IList<int>> ret = new List<IList<int>>();
        List<int> list = new List<int>();
        FindSubsequences(0, nums, list, ret);
        return ret;
    }

    private void FindSubsequences(int startIndex, int[] nums, List<int> list, List<IList<int>> ret)
    {
        for (int i = startIndex; i < nums.Length; ++i)
        {
            if (list.Count == 0 || list[list.Count - 1] <= nums[i])
            {
                list.Add(nums[i]);

                // 在结果列表里面尚不存在，加入结果列表
                if (1 < list.Count && !Contain(list)) ret.Add(list.ToArray());

                // 处理子序列分支
                FindSubsequences(i + 1, nums, list, ret);

                list.RemoveAt(list.Count - 1);
            }
        }
    }

    private bool Contain(List<int> list)
    {
        bool ret = true;
        var start = list[0];
        Node startNode = null;
        if (!_nodes.ContainsKey(start))
        {
            startNode = new Node { Value = start };
            _nodes.Add(start, startNode);
            ret = false;
        }
        else startNode = _nodes[start];

        for( int i = 1; i < list.Count; i++)
        {
            var v = list[i];
            var child = startNode.Children.FirstOrDefault(n => n.Value == v);
            if(child == null)
            {
                child = new Node { Value = v };
                startNode.Children.Add(child);
                ret = false;
            }
            startNode = child;
        }
        return ret;
    }

    private Dictionary<int,Node> _nodes = new Dictionary<int, Node>();

    class Node
    {
        public int Value { get; set; }
        public List<Node> Children { get; set; } = new List<Node>();
    }
}