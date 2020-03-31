using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// https://leetcode-cn.com/problems/subsets/
/// 78.子集
/// 给定一组不含重复元素的整数数组 nums，返回该数组所有可能的子集（幂集）。
/// 说明：解集不能包含重复的子集。
/// </summary>
class SubsetsSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public IList<IList<int>> Subsets(int[] nums)
    {
        List<IList<int>> ret = new List<IList<int>>();
        ret.Add(new List<int>());

        if (nums == null || nums.Length < 1) return ret;
        if(nums.Length == 1)
        {
            ret.Add(new List<int> { nums[0] });
            return ret;
        }

        HashSet<int> set = new HashSet<int>(nums);

        int halfLength = nums.Length / 2;
        for (int i = 1; i <= halfLength; i++)
            Combine(nums, i, ret, set);

        ret.Add( new List<int>(nums) );
        return ret;
    }

    private void Combine(int[] nums, int k, List<IList<int>> ret, HashSet<int> set)
    {
        int n = nums.Length;
        if (n == 0 || k == 0 || n < k) return;

        List<int> list = new List<int>(k);
        BackTrack(nums, k, 0, list, ret, set);
    }

    private void BackTrack(int[] nums, int k, int startIndex,
        List<int> list, List<IList<int>> ret, HashSet<int> set)
    {
        int n = nums.Length;
        if ((n - startIndex + list.Count) < k) return;

        if (list.Count == k)
        {
            ret.Add( list.ToList() );
            
            if( k < n - k ) ret.Add( set.ToList() );
            return;
        }

        for (int i = startIndex; i < n; i++)
        {
            if ((n - startIndex + list.Count) < k) return;

            var v = nums[i];

            list.Insert(0, v);
            set.Remove(v);

            BackTrack(nums, k, i + 1,
                list, ret, set);

            list.RemoveAt(0);
            set.Add(v);
        }
    }
}