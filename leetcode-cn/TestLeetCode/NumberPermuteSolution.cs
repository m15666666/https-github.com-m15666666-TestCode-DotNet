using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// https://leetcode-cn.com/problems/permutations/
/// 全排列
/// 给定一个没有重复数字的序列，返回其所有可能的全排列。
/// </summary>
class NumberPermuteSolution
{
    public static void Test()
    {
        int[] nums = new int[] {3, 2, 6, 7};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public IList<IList<int>> Permute(int[] nums)
    {
        List<IList<int>> ret = new List<IList<int>>();

        if (nums == null || nums.Length == 0) return ret;

        //Array.Sort( candidates );

        HashSet<int> numset = new HashSet<int>();
        List<int> list = new List<int>();
        BackTrade( nums, numset, list, ret );

        return ret;
    }

    private void BackTrade(int[] nums, HashSet<int> numset, List<int> list, List<IList<int>> ret )
    {
        if ( numset.Count == nums.Length )
        {
            //var key = string.Join(",", list);
            //if (!existing.Contains(key))
            {
                var l = list.ToArray();
                //existing.Add(key);
                ret.Add(l);
            }
            return;
        }

        for (int i = 0; i < nums.Length; i++)
        {
            var v = nums[i];

            if ( numset.Contains(v) ) continue;

            list.Insert(0, v);
            numset.Add(v);

            BackTrade( nums, numset, list, ret );

            list.RemoveAt(0);
            numset.Remove(v);
        }
    }
}