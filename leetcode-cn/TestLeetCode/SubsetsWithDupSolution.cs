using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个可能包含重复元素的整数数组 nums，返回该数组所有可能的子集（幂集）。

说明：解集不能包含重复的子集。

示例:

输入: [1,2,2]
输出:
[
  [2],
  [1],
  [1,2,2],
  [2,2],
  [1,2],
  []
]
 
*/
/// <summary>
/// https://leetcode-cn.com/problems/subsets-ii/
/// 90.子集II
/// 
/// 
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
        List<IList<int>> ret = new List<IList<int>>
        {
            new int[0]
        };

        if (nums == null || nums.Length < 1) return ret;

        Array.Sort(nums);

        int len = nums.Length;
        int lastValue = 0;
        List<IList<int>> lastSubsets = null;
        for ( int i = 0; i <  len; i++ )
        {
            var num = nums[i];
            if( 0 < i && num == lastValue)
            {
                for( int j = lastSubsets.Count - 1; -1 < j; j--)
                {
                    var last = lastSubsets[j];
                    lastSubsets.RemoveAt(j);
                    
                    var set = new int[last.Count + 1];
                    set[0] = num;
                    ((int[])last).CopyTo(set, 1);
                    lastSubsets.Add(set);
                }
                ret.AddRange(lastSubsets);
                continue;
            }
            lastValue = num;

            var newSubsets = new List<IList<int>>();
            lastSubsets = newSubsets;
            foreach (var current in ret)
            {
                var set = new int[current.Count + 1];
                set[0] = num;
                ((int[])current).CopyTo(set, 1);
                newSubsets.Add(set);
            }
            ret.AddRange(newSubsets);
        }

        return ret;
    }
    //public IList<IList<int>> SubsetsWithDup(int[] nums)
    //{
    //    List<IList<int>> ret = new List<IList<int>>();
    //    ret.Add(new List<int>());

    //    if (nums == null || nums.Length < 1) return ret;
    //    if (nums.Length == 1)
    //    {
    //        ret.Add(new List<int> { nums[0] });
    //        return ret;
    //    }

    //    Array.Sort(nums);

    //    HashSet<int> indexSet = new HashSet<int>(nums.Length);
    //    for (int i = 0; i < nums.Length; i++) indexSet.Add(i);

    //    int halfLength = nums.Length / 2;
    //    for (int i = 1; i <= halfLength; i++)
    //        Combine(nums, i, ret, indexSet);

    //    ret.Add(new List<int>(nums));
    //    return ret;
    //}

    //private void Combine(int[] nums, int k, List<IList<int>> ret, HashSet<int> indexSet)
    //{
    //    int n = nums.Length;
    //    if (n == 0 || k == 0 || n < k) return;

    //    List<int> list = new List<int>(k);
    //    HashSet<string> existing = new HashSet<string>();
    //    BackTrack(nums, k, 0, list, ret, indexSet, existing);
    //}

    //private void BackTrack(int[] nums, int k, int startIndex,
    //    List<int> list, List<IList<int>> ret, HashSet<int> indexSet, HashSet<string> existing)
    //{
    //    int n = nums.Length;
    //    if ((n - startIndex + list.Count) < k) return;

    //    if (list.Count == k)
    //    {
    //        var key = string.Join( "-", list );
    //        if (!existing.Contains(key))
    //        {
    //            existing.Add(key);

    //            ret.Add(list.ToList());

    //            if ( k < n - k ) ret.Add( indexSet.Select( index => nums[index] ).ToList() );
    //        }
    //        return;
    //    }

    //    for (int i = startIndex; i < n; i++)
    //    {
    //        if ((n - startIndex + list.Count) < k) return;

    //        var v = nums[i];

    //        list.Insert(0, v);
    //        indexSet.Remove(i);

    //        BackTrack(nums, k, i + 1,
    //            list, ret, indexSet, existing );

    //        list.RemoveAt(0);
    //        indexSet.Add(i);
    //    }
    //}
}
/*
public class Solution {
    Dictionary<string, IList<int>> result;
    IList<int> data;
    int len;
    //HashSet<int> dupChecker;
    public IList<IList<int>> SubsetsWithDup(int[] nums) {
        result = new Dictionary<string, IList<int>>();
        result.Add("", new List<int>());
        if(nums == null || nums.Length == 0)
           result.Values.ToList();
        len = nums.Length;
        data = new List<int>();
        Array.Sort(nums);
        dfs(nums, 0, 0);
        return result.Values.ToList();
    }
    
    public void dfs(int[] nums, int step, int cur)
    {
        if(step >= len)
        {
            return;
        }
        for(int i = cur; i < len; i++)
        {
            //Console.WriteLine($"step:{step}, cur:{cur}");
            data.Add(nums[i]);
            string key = string.Join("", data);
            if(!result.ContainsKey(key))
            {
                result.Add(key, new List<int>(data.ToArray()));
                dfs(nums, step+1, i+1);
            }
            data.Remove(nums[i]);
        }
    }
} 
public class Solution {
    IList<IList<int>> result = new List<IList<int>>();
    int len = 0;
    public IList<IList<int>> SubsetsWithDup(int[] nums) {
        if(nums == null) return result;
        len = nums.Length;
        Array.Sort(nums);
        BackTrack(nums,0,new List<int>());
        return result;
    }

    void BackTrack(int[] nums,int start,List<int> list){
        result.Add(list.ToArray());
        
        for(int i = start;i < len;i++){
            if(i > start && nums[i] == nums[i - 1]) continue;
            list.Add(nums[i]);
            BackTrack(nums,i+1,list);
            list.RemoveAt(list.Count - 1);
        }
    }
}

 
*/