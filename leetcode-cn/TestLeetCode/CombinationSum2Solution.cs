﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// https://leetcode-cn.com/problems/combination-sum-ii/
/// 组合总和II
/// 给定一个数组 candidates 和一个目标数 target ，
/// 找出 candidates 中所有可以使数字和为 target 的组合。
/// </summary>
class CombinationSum2Solution
{
    public static void Test()
    {
        int[] nums = new int[] {3, 2, 6, 7};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        CombinationSum2(nums, 7);
        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public static IList<IList<int>> CombinationSum2(int[] candidates, int target)
    {
        List<IList<int>> ret = new List<IList<int>>();

        if (candidates == null || candidates.Length == 0) return ret;

        Array.Sort( candidates );

        HashSet<string> existing = new HashSet<string>();
        List<int> list = new List<int>();
        BackTrade( candidates, target, 0, list, ret, existing );

        return ret;
    }

    private static void BackTrade(int[] candidates, int target, int startIndex, List<int> list, List<IList<int>> ret, HashSet<string> existing)
    {
        if (target < 0) return;

        if ( target == 0 )
        {
            var key = string.Join(",", list);
            if (!existing.Contains(key))
            {
                var l = list.ToArray();
                existing.Add(key);
                ret.Add(l);
            }
            return;
        }

        for (int i = startIndex; i < candidates.Length; i++)
        {
            var v = candidates[i];

            list.Insert(0, v);

            BackTrade( candidates, target - v, i + 1, list, ret, existing );

            list.RemoveAt(0);
        }
    }
}