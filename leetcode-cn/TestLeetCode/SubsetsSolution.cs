using System;
using System.Collections.Generic;

/*
给定一组不含重复元素的整数数组 nums，返回该数组所有可能的子集（幂集）。

说明：解集不能包含重复的子集。

示例:

输入: nums = [1,2,3]
输出:
[
  [3],
  [1],
  [2],
  [1,2,3],
  [1,3],
  [2,3],
  [1,2],
  []
]

*/

/// <summary>
/// https://leetcode-cn.com/problems/subsets/
/// 78.子集
///
///
/// </summary>
internal class SubsetsSolution
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
        if (nums == null || nums.Length == 0) return new List<IList<int>>();

        List<IList<int>> ret = new List<IList<int>>((int)Math.Pow(2, nums.Length))
        {
            new int[0]
        };

        foreach (int num in nums)
        {
            var newSubsets = new List<IList<int>>();
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

    //public IList<IList<int>> Subsets(int[] nums)
    //{
    //    List<IList<int>> ret = new List<IList<int>>();
    //    ret.Add(new List<int>());

    //    if (nums == null || nums.Length < 1) return ret;
    //    if(nums.Length == 1)
    //    {
    //        ret.Add(new List<int> { nums[0] });
    //        return ret;
    //    }

    //    HashSet<int> set = new HashSet<int>(nums);

    //    int halfLength = nums.Length / 2;
    //    for (int i = 1; i <= halfLength; i++)
    //        Combine(nums, i, ret, set);

    //    ret.Add( new List<int>(nums) );
    //    return ret;
    //}

    //private void Combine(int[] nums, int k, List<IList<int>> ret, HashSet<int> set)
    //{
    //    int n = nums.Length;
    //    if (n == 0 || k == 0 || n < k) return;

    //    List<int> list = new List<int>(k);
    //    BackTrack(nums, k, 0, list, ret, set);
    //}

    //private void BackTrack(int[] nums, int k, int startIndex,
    //    List<int> list, List<IList<int>> ret, HashSet<int> set)
    //{
    //    int n = nums.Length;
    //    if ((n - startIndex + list.Count) < k) return;

    //    if (list.Count == k)
    //    {
    //        ret.Add( list.ToList() );

    //        if( k < n - k ) ret.Add( set.ToList() );
    //        return;
    //    }

    //    for (int i = startIndex; i < n; i++)
    //    {
    //        if ((n - startIndex + list.Count) < k) return;

    //        var v = nums[i];

    //        list.Insert(0, v);
    //        set.Remove(v);

    //        BackTrack(nums, k, i + 1,
    //            list, ret, set);

    //        list.RemoveAt(0);
    //        set.Add(v);
    //    }
    //}
}

/*

子集
力扣 (LeetCode)
发布于 3 个月前
17.9k
解决方案
观察全排列/组合/子集问题，它们比较相似，且可以使用一些通用策略解决。

首先，它们的解空间非常大：

全排列：N!N!。

组合：N!N!。

子集：2^N2 
N
 ，每个元素都可能存在或不存在。

在它们的指数级解法中，要确保生成的结果 完整 且 无冗余，有三种常用的方法：

递归

回溯

基于二进制位掩码和对应位掩码之间的映射字典生成排列/组合/子集

相比前两种方法，第三种方法将每种情况都简化为二进制数，易于实现和验证。

此外，第三种方法具有最优的时间复杂度，可以生成按照字典顺序的输出结果。

方法一：递归
思路

开始假设输出子集为空，每一步都向子集添加新的整数，并生成新的子集。



class Solution {
  public List<List<Integer>> subsets(int[] nums) {
    List<List<Integer>> output = new ArrayList();
    output.add(new ArrayList<Integer>());

    for (int num : nums) {
      List<List<Integer>> newSubsets = new ArrayList();
      for (List<Integer> curr : output) {
        newSubsets.add(new ArrayList<Integer>(curr){{add(num);}});
      }
      for (List<Integer> curr : newSubsets) {
        output.add(curr);
      }
    }
    return output;
  }
}
复杂度分析

时间复杂度：\mathcal{O}(N \times 2^N)O(N×2 
N
 )，生成所有子集，并复制到输出结果中。

空间复杂度：\mathcal{O}(N \times 2^N)O(N×2 
N
 )，这是子集的数量。

对于给定的任意元素，它在子集中有两种情况，存在或者不存在（对应二进制中的 0 和 1）。因此，NN 个数字共有 2^N2 
N
  个子集。
方法二：回溯
算法

幂集是所有长度从 0 到 n 所有子集的组合。

根据定义，该问题可以看作是从序列中生成幂集。

遍历 子集长度，通过 回溯 生成所有给定长度的子集。



回溯法是一种探索所有潜在可能性找到解决方案的算法。如果当前方案不是正确的解决方案，或者不是最后一个正确的解决方案，则回溯法通过修改上一步的值继续寻找解决方案。



算法

定义一个回溯方法 backtrack(first, curr)，第一个参数为索引 first，第二个参数为当前子集 curr。

如果当前子集构造完成，将它添加到输出集合中。

否则，从 first 到 n 遍历索引 i。

将整数 nums[i] 添加到当前子集 curr。

继续向子集中添加整数：backtrack(i + 1, curr)。

从 curr 中删除 nums[i] 进行回溯。

class Solution {
  List<List<Integer>> output = new ArrayList();
  int n, k;

  public void backtrack(int first, ArrayList<Integer> curr, int[] nums) {
    // if the combination is done
    if (curr.size() == k)
      output.add(new ArrayList(curr));

    for (int i = first; i < n; ++i) {
      // add i into the current combination
      curr.add(nums[i]);
      // use next integers to complete the combination
      backtrack(i + 1, curr, nums);
      // backtrack
      curr.remove(curr.size() - 1);
    }
  }

  public List<List<Integer>> subsets(int[] nums) {
    n = nums.length;
    for (k = 0; k < n + 1; ++k) {
      backtrack(0, new ArrayList<Integer>(), nums);
    }
    return output;
  }
}
复杂度分析

时间复杂度：\mathcal{O}(N \times 2^N)O(N×2 
N
 )，生成所有子集，并复制到输出集合中。

空间复杂度：\mathcal{O}(N \times 2^N)O(N×2 
N
 )，存储所有子集，共 nn 个元素，每个元素都有可能存在或者不存在。

方法三：字典排序（二进制排序） 子集
思路

该方法思路来自于 Donald E. Knuth。

将每个子集映射到长度为 n 的位掩码中，其中第 i 位掩码 nums[i] 为 1，表示第 i 个元素在子集中；如果第 i 位掩码 nums[i] 为 0，表示第 i 个元素不在子集中。



例如，位掩码 0..00（全 0）表示空子集，位掩码 1..11（全 1）表示输入数组 nums。

因此要生成所有子集，只需要生成从 0..00 到 1..11 的所有 n 位掩码。

乍看起来生成二进制数很简单，但如何处理左边填充 0 是一个问题。因为必须生成固定长度的位掩码：例如 001，而不是 1。因此可以使用一些位操作技巧：

int nthBit = 1 << n;
for (int i = 0; i < (int)Math.pow(2, n); ++i) {
    // generate bitmask, from 0..00 to 1..11
    String bitmask = Integer.toBinaryString(i | nthBit).substring(1);
或者使用简单但低效的迭代进行控制：

for (int i = (int)Math.pow(2, n); i < (int)Math.pow(2, n + 1); ++i) {
  // generate bitmask, from 0..00 to 1..11
  String bitmask = Integer.toBinaryString(i).substring(1);
算法

生成所有长度为 n 的二进制位掩码。

将每个子集都映射到一个位掩码数：位掩码中第 i 位如果是 1 表示子集中存在 nums[i]，0 表示子集中不存在 nums[i]。

返回子集列表。

class Solution {
  public List<List<Integer>> subsets(int[] nums) {
    List<List<Integer>> output = new ArrayList();
    int n = nums.length;

    for (int i = (int)Math.pow(2, n); i < (int)Math.pow(2, n + 1); ++i) {
      // generate bitmask, from 0..00 to 1..11
      String bitmask = Integer.toBinaryString(i).substring(1);

      // append subset corresponding to that bitmask
      List<Integer> curr = new ArrayList();
      for (int j = 0; j < n; ++j) {
        if (bitmask.charAt(j) == '1') curr.add(nums[j]);
      }
      output.add(curr);
    }
    return output;
  }
}
复杂度分析

时间复杂度：\mathcal{O}(N \times 2^N)O(N×2 
N
 )，生成所有的子集，并复制到输出列表中。

空间复杂度：\mathcal{O}(N \times 2^N)O(N×2 
N
 )，存储所有子集，共 nn 个元素，每个元素都有可能存在或者不存在。

下一篇：回溯算法

public class Solution {
    IList<IList<int>> result;
    IList<int> data;
    int len;
    public IList<IList<int>> Subsets(int[] nums) {
        result = new List<IList<int>>();
        result.Add(new List<int>());
        if(nums == null || nums.Length == 0)
            return result;
        len = nums.Length;
        data = new List<int>();
        dfs(nums, 0, 0);
        return result;
    }
    
    public void dfs(int[] nums, int step, int cur)
    {
        if(step >= len)
        {
            return;
        }
        for(int i = cur; i < len; i++)
        {
            data.Add(nums[i]);
            result.Add(new List<int>(data.ToArray()));
            dfs(nums, step+1,i+1);
            data.Remove(nums[i]);
        }
    }
}

public class Solution {
    public IList<IList<int>> Subsets (int[] nums) {
        var res = new List<IList<int>> {
            new List<int> ()
        };

        foreach (var num in nums) {
            var sub = new List<IList<int>> ();
            foreach (var r in res) {
                var temp = new List<int> (r) {
                    num
                };
                sub.Add (temp);
            }

            foreach (var s in sub) {
                res.Add (s);
            }
        }

        return res;
    }
}

public class Solution {
    public IList<IList<int>> Subsets(int[] nums) {
      IList<IList<int>> list=new List<IList<int>>();
      list.Add(new List<int>());
      int len ;
      for(int i=0;i<nums.Length;i++)
      {
          len =list.Count;
          for(int j=0;j<len;j++)
          {
              list.Add(new List<int>(list[j]));
          }
          for(int k=len;k<list.Count;k++)
          {
              list[k].Add(nums[i]);
          }
      }
      return list;
    }
}

public class Solution {
    public IList<IList<int>> Subsets(int[] nums) {
       IList<IList<int>> result=new List<IList<int>>();
       int n=1;
       for(int i=0;i<nums.Length;i++)
       {
           n*=2;
       } 
       for(int j=0;j<n;j++)
       {
           IList<int> temp=new List<int>();
           for(int k=0;k<nums.Length;k++)
           {
               if(((1<<k)&j)!=0)
               temp.Add(nums[k]);
           }
           result.Add(temp);
       }
       return result;
    }
}

public class Solution {
    public IList<IList<int>> Subsets(int[] nums) {
        int num=1;
        for(int i =1;i<=nums.Length;i++)
        {
            num=num*2;
        }
        num-=1;
        IList<IList<int>> result=new List<IList<int>>();
        for(int j=0;j<=num;j++)
        {
            IList<int> once=new List<int>();
            for(int k=0;k<nums.Length;k++)
            {
                int b=1<<k;
                if((j&b)!=0)
                {once.Add(nums[k]);}
            } 
            result.Add(once);
        }
        return result;
    }
}
*/
