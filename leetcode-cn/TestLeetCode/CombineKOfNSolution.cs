using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



/*
给定两个整数 n 和 k，返回 1 ... n 中所有可能的 k 个数的组合。

示例:

输入: n = 4, k = 2
输出:
[
  [2,4],
  [3,4],
  [2,3],
  [1,2],
  [1,3],
  [1,4],
]


*/
/// <summary>
/// https://leetcode-cn.com/problems/combinations/
/// 77.组合
/// 
/// 
/// </summary>
class CombineKOfNSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public IList<IList<int>> Combine(int n, int k)
    {
        List<IList<int>> ret = new List<IList<int>>();

        if (n == 0 || k == 0 || n < k) return ret;

        //HashSet<int> indexset = new HashSet<int>();
        List<int> list = new List<int>();
        BackTrack( n, k, 0, 
            //indexset, 
            list, ret );

        return ret;
    }

    private void BackTrack( int n, int k, int startIndex, 
        //HashSet<int> indexset, 
        List<int> list, List<IList<int>> ret )
    {
        if ((n - startIndex + list.Count) < k) return;

        if ( list.Count == k )
        {
            var l = list.ToArray();
            ret.Add(l);
            return;
        }

        for ( int i = startIndex; i < n; i++ )
        {
            //if (indexset.Contains(i)) continue;

            if ((n - startIndex + list.Count) < k) return;

            var v = i + 1;

            list.Insert(0, v);
            //indexset.Add(i);

            BackTrack( n, k, i + 1, 
                //indexset, 
                list, ret );

            list.RemoveAt(0);
            //indexset.Remove(i);
        }
    }
}
/*
组合
力扣 (LeetCode)
发布于 1 年前
24.4k
方法一 : 回溯法
算法

回溯法
是一种通过遍历所有可能成员来寻找全部可行解的算法。若候选 不是 可行解 (或者至少不是 最后一个 解)，回溯法会在前一步进行一些修改以舍弃该候选，换而言之， 回溯 并再次尝试。

这是一个回溯法函数，它将第一个添加到组合中的数和现有的组合作为参数。 backtrack(first, curr)

若组合完成- 添加到输出中。

遍历从 first t到 n的所有整数。

将整数 i 添加到现有组合 curr中。

继续向组合中添加更多整数 :
backtrack(i + 1, curr).

将 i 从 curr中移除，实现回溯。

实现



class Solution {
  List<List<Integer>> output = new LinkedList();
  int n;
  int k;

  public void backtrack(int first, LinkedList<Integer> curr) {
    // if the combination is done
    if (curr.size() == k)
      output.add(new LinkedList(curr));

    for (int i = first; i < n + 1; ++i) {
      // add i into the current combination
      curr.add(i);
      // use next integers to complete the combination
      backtrack(i + 1, curr);
      // backtrack
      curr.removeLast();
    }
  }

  public List<List<Integer>> combine(int n, int k) {
    this.n = n;
    this.k = k;
    backtrack(1, new LinkedList<Integer>());
    return output;
  }
}
复杂度分析

时间复杂度 : O(k C_N^k)O(kC 
N
k
​	
 )，其中 C_N^k = \frac{N!}{(N - k)! k!}C 
N
k
​	
 = 
(N−k)!k!
N!
​	
  是要构成的组合数。
append / pop (add / removeLast) 操作使用常数时间，唯一耗费时间的是将长度为 k 的组合添加到输出中。

空间复杂度 : O(C_N^k)O(C 
N
k
​	
 ) ，用于保存全部组合数以输出。




方法二: 字典序 (二进制排序) 组合
直觉

主要思路是以字典序的顺序获得全部组合。

image.png
算法

算法非常直截了当 :

将 nums 初始化为从 1 到 k的整数序列。 将 n + 1添加为末尾元素，起到“哨兵”的作用。
将指针设为列表的开头 j = 0.

While j < k :

将nums 中的前k个元素添加到输出中，换而言之，除了“哨兵”之外的全部元素。

找到nums中的第一个满足 nums[j] + 1 != nums[j + 1]的元素，并将其加一
nums[j]++ 以转到下一个组合。

实现

class Solution {
  public List<List<Integer>> combine(int n, int k) {
    // init first combination
    LinkedList<Integer> nums = new LinkedList<Integer>();
    for(int i = 1; i < k + 1; ++i)
      nums.add(i);
    nums.add(n + 1);

    List<List<Integer>> output = new ArrayList<List<Integer>>();
    int j = 0;
    while (j < k) {
      // add current combination
      output.add(new LinkedList(nums.subList(0, k)));
      // increase first nums[j] by one
      // if nums[j] + 1 != nums[j + 1]
      j = 0;
      while ((j < k) && (nums.get(j + 1) == nums.get(j) + 1))
        nums.set(j, j++ + 1);
      nums.set(j, nums.get(j) + 1);
    }
    return output;
  }
}
复杂度分析

时间复杂度 : O(k C_N^k)O(kC 
N
k
​	
 )，其中C_N^k = \frac{N!}{(N - k)! k!}C 
N
k
​	
 = 
(N−k)!k!
N!
​	
  是要构建的组合数。由于组合数是C_N^kC 
N
k
​	
 ，外层的 while 循环执行了C_N^kC 
N
k
​	
 次 。对给定的一个j，内层的 while 循环执行了C_{N - j}^{k - j}C 
N−j
k−j
​	
 次。外层循环超过 C_N^kC 
N
k
​	
 次访问，平均而言每次访问的执行次数少于1。因此，最耗费时间的部分是将每个长度为kk 的组合(共计C_N^kC 
N
k
​	
  个组合) 添加到输出中，
消耗 O(k C_N^k)O(kC 
N
k
​	
 ) 的时间。

你可能注意到，尽管方法二的时间复杂度与方法一相同，但方法二却要快上许多。这是由于方法一需要处理递归调用栈，且其带来的影响在Python中比在Java中更为显著。

空间复杂度 : O(C_N^k)O(C 
N
k
​	
 ) ，用于保存全部组合数以输出。

下一篇：回溯算法 + 剪枝

 
     
     
     
     
*/