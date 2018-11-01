using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// https://leetcode-cn.com/problems/subsets-ii/
/// 90.子集II
/// 给定一个可能包含重复元素的整数数组 nums，返回该数组所有可能的子集（幂集）。
/// 说明：解集不能包含重复的子集。
/// </summary>
class SubsetsWithDupSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public IList<IList<int>> SubsetsWithDup(int[] nums)
    {
        List<IList<int>> ret = new List<IList<int>>();
        ret.Add(new List<int>());

        if (nums == null || nums.Length < 1) return ret;
        if (nums.Length == 1)
        {
            ret.Add(new List<int> { nums[0] });
            return ret;
        }

        Array.Sort(nums);

        HashSet<int> indexSet = new HashSet<int>(nums.Length);
        for (int i = 0; i < nums.Length; i++) indexSet.Add(i);

        int halfLength = nums.Length / 2;
        for (int i = 1; i <= halfLength; i++)
            Combine(nums, i, ret, indexSet);

        ret.Add(new List<int>(nums));
        return ret;
    }

    private void Combine(int[] nums, int k, List<IList<int>> ret, HashSet<int> indexSet)
    {
        int n = nums.Length;
        if (n == 0 || k == 0 || n < k) return;

        List<int> list = new List<int>(k);
        HashSet<string> existing = new HashSet<string>();
        BackTrade(nums, k, 0, list, ret, indexSet, existing);
    }

    private void BackTrade(int[] nums, int k, int startIndex,
        List<int> list, List<IList<int>> ret, HashSet<int> indexSet, HashSet<string> existing)
    {
        int n = nums.Length;
        if ((n - startIndex + list.Count) < k) return;

        if (list.Count == k)
        {
            var key = string.Join( "-", list );
            if (!existing.Contains(key))
            {
                existing.Add(key);

                ret.Add(list.ToList());

                if ( k < n - k ) ret.Add( indexSet.Select( index => nums[index] ).ToList() );
            }
            return;
        }

        for (int i = startIndex; i < n; i++)
        {
            if ((n - startIndex + list.Count) < k) return;

            var v = nums[i];

            list.Insert(0, v);
            indexSet.Remove(i);

            BackTrade(nums, k, i + 1,
                list, ret, indexSet, existing );

            list.RemoveAt(0);
            indexSet.Add(i);
        }
    }
}