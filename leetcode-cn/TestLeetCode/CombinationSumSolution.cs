using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// https://leetcode-cn.com/problems/combination-sum/
/// 组合总和
/// 给定一个无重复元素的数组 candidates 和一个目标数 target ，
/// 找出 candidates 中所有可以使数字和为 target 的组合。
/// https://blog.csdn.net/w8253497062015/article/details/80007834
/// </summary>
class CombinationSumSolution
{
    public static void Test()
    {
        int[] nums = new int[] {3, 2, 6, 7};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        CombinationSum(nums, 7);
        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public static IList<IList<int>> CombinationSum(int[] candidates, int target)
    {
        List<IList<int>> ret = new List<IList<int>>();

        if (candidates == null || candidates.Length == 0) return ret;

        Array.Sort( candidates );

        HashSet<string> existing = new HashSet<string>();
        List<int> list = new List<int>();
        BackTrack( candidates, target, 0, list, ret, existing );

        return ret;
    }

    private static void BackTrack(int[] candidates, int target, int startIndex, List<int> list, List<IList<int>> ret, HashSet<string> existing)
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

            BackTrack( candidates, target - v, i, list, ret, existing );

            list.RemoveAt(0);
        }
    }
}