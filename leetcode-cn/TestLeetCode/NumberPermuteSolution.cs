using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个 没有重复 数字的序列，返回其所有可能的全排列。

示例:

输入: [1,2,3]
输出:
[
  [1,2,3],
  [1,3,2],
  [2,1,3],
  [2,3,1],
  [3,1,2],
  [3,2,1]
]
*/
/// <summary>
/// https://leetcode-cn.com/problems/permutations/
/// 46. 全排列
/// 
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

        int len = nums.Length;
        BackTrack(0);

        return ret;

        void BackTrack( int start )
        {
            if(start == len - 1)
            {
                ret.Add((int[])nums.Clone());
                return;
            }
            for( int i = start; i < len; i++ )
            {
                (nums[start], nums[i]) = (nums[i], nums[start]);
                BackTrack(start + 1);
                (nums[start], nums[i]) = (nums[i], nums[start]);
            }
        }
    }
    //public IList<IList<int>> Permute(int[] nums)
    //{
    //    List<IList<int>> ret = new List<IList<int>>();

    //    if (nums == null || nums.Length == 0) return ret;

    //    //Array.Sort( candidates );

    //    HashSet<int> numset = new HashSet<int>();
    //    List<int> list = new List<int>();
    //    BackTrack( nums, numset, list, ret );

    //    return ret;
    //}

    //private void BackTrack(int[] nums, HashSet<int> numset, List<int> list, List<IList<int>> ret )
    //{
    //    if ( numset.Count == nums.Length )
    //    {
    //        //var key = string.Join(",", list);
    //        //if (!existing.Contains(key))
    //        {
    //            var l = list.ToArray();
    //            //existing.Add(key);
    //            ret.Add(l);
    //        }
    //        return;
    //    }

    //    for (int i = 0; i < nums.Length; i++)
    //    {
    //        var v = nums[i];

    //        if ( numset.Contains(v) ) continue;

    //        list.Insert(0, v);
    //        numset.Add(v);

    //        BackTrack( nums, numset, list, ret );

    //        list.RemoveAt(0);
    //        numset.Remove(v);
    //    }
    //}
}
/*

https://leetcode-cn.com/problems/permutations/solution/quan-pai-lie-by-leetcode/
全排列
力扣 (LeetCode)
发布于 1 年前
77.9k
方法1：回溯法
回溯法
是一种通过探索所有可能的候选解来找出所有的解的算法。如果候选解被确认 不是 一个解的话（或者至少不是 最后一个 解），回溯算法会通过在上一步进行一些变化抛弃该解，即 回溯 并且再次尝试。

这里有一个回溯函数，使用第一个整数的索引作为参数 backtrack(first)。

如果第一个整数有索引 n，意味着当前排列已完成。
遍历索引 first 到索引 n - 1 的所有整数。Iterate over the integers from index first to index n - 1.
在排列中放置第 i 个整数，
即 swap(nums[first], nums[i]).
继续生成从第 i 个整数开始的所有排列: backtrack(first + 1).
现在回溯，即通过 swap(nums[first], nums[i]) 还原.


class Solution {
  public void backtrack(int n,
                        ArrayList<Integer> nums,
                        List<List<Integer>> output,
                        int first) {
    // if all integers are used up
    if (first == n)
      output.add(new ArrayList<Integer>(nums));
    for (int i = first; i < n; i++) {
      // place i-th integer first 
      // in the current permutation
      Collections.swap(nums, first, i);
      // use next integers to complete the permutations
      backtrack(n, nums, output, first + 1);
      // backtrack
      Collections.swap(nums, first, i);
    }
  }

  public List<List<Integer>> permute(int[] nums) {
    // init output list
    List<List<Integer>> output = new LinkedList();

    // convert nums into list since the output is a list of lists
    ArrayList<Integer> nums_lst = new ArrayList<Integer>();
    for (int num : nums)
      nums_lst.add(num);

    int n = nums.length;
    backtrack(n, nums_lst, output, 0);
    return output;
  }
}
复杂性分析

时间复杂度：\mathcal{O}(\sum_{k = 1}^{N}{P(N, k)})O(∑ 
k=1
N
​	
 P(N,k))， P(N, k) = \frac{N!}{(N - k)!} = N (N - 1) ... (N - k + 1)P(N,k)= 
(N−k)!
N!
​	
 =N(N−1)...(N−k+1)，该式被称作 n 的 k-排列，或者_部分排列_.
为了简单起见，使 first + 1 = kfirst+1=k.
这个公式很容易理解：对于每个 kk (每个firstfirst)
有 N(N - 1) ... (N - k + 1)N(N−1)...(N−k+1) 次操作，
且 kk 的范围从 11 到 NN (firstfirst 从 00 到 N - 1N−1).

我们来做一个结果的粗略估计：
N! \le \sum_{k = 1}^{N}{\frac{N!}{(N - k)!}} = \sum_{k = 1}^{N}{P(N, k)} \le N \times N!N!≤∑ 
k=1
N
​	
  
(N−k)!
N!
​	
 =∑ 
k=1
N
​	
 P(N,k)≤N×N!,
即算法比 \mathcal{O}(N \times N!)O(N×N!)更优 且
比 \mathcal{O}(N!)O(N!) 稍慢.

空间复杂度：\mathcal{O}(N!)O(N!) 由于必须要保存
N! 个解。
下一篇：回溯算法

public class Solution {
    public IList<IList<int>> Permute(int[] nums) {
        IList<IList<int>> res = new List<IList<int>>();
        BackTrackPermute(0,nums.Length,nums,res);
        return res;
    }

    void BackTrackPermute(int idx,int len,int[] nums,IList<IList<int>> res)
    {
        if (idx == len)
        {
            res.Add(new List<int>(nums));
            return;
        }

        for (int i = idx; i < len; i++)
        {
            Swap(nums, i, idx);
            BackTrackPermute(idx+1,len,nums,res);
            Swap(nums,idx, i);
        }
    }
     void Swap(int[] nums, int i, int j)
    {
        int temp = nums[i];
        nums[i] = nums[j];
        nums[j] = temp;
    }
}

public class Solution {
    public IList<IList<int>> Permute(int[] nums) {
        List<IList<int>> ans = new List<IList<int>>();
        ans.Add(new List<int>(nums));
        if(nums.Length <= 1) return ans;
        int len = nums.Length;
        int all = 1;
        while(len > 0) {
            all *= len;
            len--;    
        }
        int count = 1;
        while(count < all){
            int i = nums.Length-2;
            while(i >= 0 && nums[i] >= nums[i+1]) i--;
            if(i >= 0){
                int j = nums.Length-1;
                while(j > i && nums[j] <= nums[i]) j--;
                Swap(i, j, nums);
            }
            Reserve(i+1, nums);
            count++;
            ans.Add(new List<int>(nums));
        }
        return ans;
    }

    public void Reserve(int start, int[] nums){
        int i = start;
        int j = nums.Length-1;
        while(i < j){
            Swap(i, j, nums);
            i++;
            j--;
        }
    }

    public void Swap(int i, int j, int[] nums){
        int temp = nums[i];
        nums[i] = nums[j];
        nums[j] = temp;
    }
}

public class Solution {
    public IList<IList<int>> Permute(int[] nums) {
        var current = new int[nums.Length];
        var result = new List<IList<int>>();
        var used = new bool[nums.Length];
        DFS(0,nums,used,current,result);
        return result;
    }

    private void DFS(int count,int[] nums, bool[] used, int[] current, List<IList<int>> result)
    {
        if(nums.Length == count)
        {
            result.Add(current.ToList());
            return;
        }

        for(var i = 0; i < nums.Length; i++)
        {
            if(used[i]) continue;
            used[i] = true;
            current[i] = nums[count];
            DFS(count+1,nums,used,current,result);
            //current.RemoveAt(current.Count-1);
            used[i] = false;
        }
    }
}

public class Solution {
public IList<IList<int>> Permute(int[] nums)
{
	var res = new List<IList<int>>();
	if (nums.Length == 0) return res;

	res.Add(nums);
	for (int i = 0; i < nums.Length; i++)
	{
		var count = res.Count();
		for (int j = i + 1; j < nums.Length; j++)
		{			
			for (int k = 0; k < count; k++)
			{
				var copy = res[k].ToArray();
				var temp = copy[i];
				copy[i] = copy[j];
				copy[j] = temp;
				res.Add(copy);
			}
		}
	}
	return res;
}
}
 
     
*/