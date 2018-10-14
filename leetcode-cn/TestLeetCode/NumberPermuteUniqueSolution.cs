using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// https://leetcode-cn.com/problems/permutations-ii/
/// 全排列II
/// 给定一个可包含重复数字的序列，返回所有不重复的全排列。
/// </summary>
class NumberPermuteUniqueSolution
{
    public static void Test()
    {
        int[] nums = new int[] { 3, 2, 6, 7 };
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public IList<IList<int>> PermuteUnique(int[] nums)
    {
        List<IList<int>> ret = new List<IList<int>>();

        if (nums == null || nums.Length == 0) return ret;

        //Array.Sort( candidates );

        HashSet<int> indexset = new HashSet<int>();
        HashSet<string> existing = new HashSet<string>();
        List<int> list = new List<int>();
        BackTrade(nums, indexset, list, ret, existing);

        return ret;
    }

    private void BackTrade(int[] nums, HashSet<int> indexset, List<int> list, List<IList<int>> ret, HashSet<string> existing)
    {
        if (indexset.Count == nums.Length)
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

        for (int i = 0; i < nums.Length; i++)
        {
            if (indexset.Contains(i)) continue;

            var v = nums[i];

            list.Insert(0, v);
            indexset.Add(i);

            BackTrade(nums, indexset, list, ret, existing);

            list.RemoveAt(0);
            indexset.Remove(i);
        }
    }
}