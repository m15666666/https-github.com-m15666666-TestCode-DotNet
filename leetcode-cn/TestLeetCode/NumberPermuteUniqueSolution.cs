using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;


/*
给定一个可包含重复数字的序列，返回所有不重复的全排列。

示例:

输入: [1,1,2]
输出:
[
  [1,1,2],
  [1,2,1],
  [2,1,1]
]
*/
/// <summary>
/// https://leetcode-cn.com/problems/permutations-ii/
/// 47. 全排列 II
/// 
/// </summary>
class NumberPermuteUniqueSolution
{
    public void Test()
    {
        int[] nums = new int[] { 0,1,0,0,9 };
        var ret = Permute(nums);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public IList<IList<int>> Permute(int[] nums)
    {
        List<IList<int>> ret = new List<IList<int>>();
        if (nums == null || nums.Length == 0) return ret;

        Array.Sort(nums);
        int len = nums.Length;
        bool[] used = new bool[len];
        Stack<int> stack = new Stack<int>();
        BackTrack(0);

        return ret;

        void BackTrack(int depth)
        {
            if (depth == len)
            {
                ret.Add(new List<int>(stack));
                return;
            }
            
            for (int i = 0; i < len; i++)
            {
                if (used[i]) continue;
                if (0 < i && nums[i] == nums[i - 1] && !used[i-1]) continue;

                stack.Push(nums[i]);
                used[i] = true;

                BackTrack(depth + 1);

                stack.Pop();
                used[i] = false;
            }
        }
    }

    //public IList<IList<int>> Permute(int[] nums)
    //{
    //    List<IList<int>> ret = new List<IList<int>>();
    //    if (nums == null || nums.Length == 0) return ret;

    //    Array.Sort(nums);
    //    int len = nums.Length;
    //    BackTrack(0);

    //    return ret;

    //    void BackTrack( int start )
    //    {
    //        if(start == len - 1)
    //        {
    //            Debug.Print("add:" + string.Join("-", nums) + $",{start}");
    //            ret.Add(new List<int>(nums));
    //            return;
    //        }
    //        BackTrack(start + 1);
    //        for( int i = start + 1; i < len; i++ )
    //        {
    //            if (nums[i] == nums[i - 1]) continue;
    //            if (nums[start] == nums[i]) continue;

    //            Debug.Print("1: " + string.Join("-", nums) + $",{start},{i}");
    //            (nums[start], nums[i]) = (nums[i], nums[start]);
    //            Debug.Print("2: " + string.Join("-", nums) + $",{start},{i}");
    //            BackTrack(start + 1);
    //            Debug.Print("3: " + string.Join("-", nums) + $",{start},{i}");
    //            (nums[start], nums[i]) = (nums[i], nums[start]);
    //            Debug.Print("4: " + string.Join("-", nums) + $",{start},{i}");
    //        }
    //    }
    //}

    //public IList<IList<int>> PermuteUnique(int[] nums)
    //{
    //    List<IList<int>> ret = new List<IList<int>>();

    //    if (nums == null || nums.Length == 0) return ret;

    //    //Array.Sort( candidates );

    //    HashSet<int> indexset = new HashSet<int>();
    //    HashSet<string> existing = new HashSet<string>();
    //    List<int> list = new List<int>();
    //    BackTrack(nums, indexset, list, ret, existing);

    //    return ret;
    //}

    //private void BackTrack(int[] nums, HashSet<int> indexset, List<int> list, List<IList<int>> ret, HashSet<string> existing)
    //{
    //    if (indexset.Count == nums.Length)
    //    {
    //        var key = string.Join(",", list);
    //        if (!existing.Contains(key))
    //        {
    //            var l = list.ToArray();
    //            existing.Add(key);
    //            ret.Add(l);
    //        }
    //        return;
    //    }

    //    for (int i = 0; i < nums.Length; i++)
    //    {
    //        if (indexset.Contains(i)) continue;

    //        var v = nums[i];

    //        list.Insert(0, v);
    //        indexset.Add(i);

    //        BackTrack(nums, indexset, list, ret, existing);

    //        list.RemoveAt(0);
    //        indexset.Remove(i);
    //    }
    //}
}
/*

回溯搜索 + 剪枝
liweiwei1419
发布于 10 个月前
34.3k

这一题是在「力扣」第 46 题： “全排列” 的基础上增加了“序列中的元素可重复”这一条件，但要求返回的结果又不能有重复元素。

思路：在一定会产生重复结果集的地方剪枝。

一个比较容易想到的办法是在结果集中去重。但是问题又来了，这些结果集的元素是一个又一个列表，对列表去重不像用哈希表对基本元素去重那样容易。

如果要比较两个列表是否一样，一个很显然的办法是分别排序，然后逐个比对。既然要排序，我们可以在搜索之前就对候选数组排序，一旦发现这一支搜索下去可能搜索到重复的元素就停止搜索，这样结果集中不会包含重复元素。

LeetCode 第 47 题：“全排列 II”题解配图.png

产生重复结点的地方，正是图中标注了“剪刀”，且被绿色框框住的地方。

大家也可以把第 2 个 1 加上 ' ，即 [1, 1', 2] 去想象这个搜索的过程。只要遇到起点一样，就有可能产生重复。这里还有一个很细节的地方：

1、在图中 ② 处，搜索的数也和上一次一样，但是上一次的 1 还在使用中；
2、在图中 ① 处，搜索的数也和上一次一样，但是上一次的 1 刚刚被撤销，正是因为刚被撤销，下面的搜索中还会使用到，因此会产生重复，剪掉的就应该是这样的分支。

代码实现方面，在第 46 题的基础上，要加上这样一段代码：

if (i > 0 && nums[i] == nums[i - 1] && !used[i - 1]) {
    continue;
}
这段代码就能检测到标注为 ① 的两个结点，跳过它们。注意：这里 used[i - 1] 不加 !，测评也能通过。有兴趣的朋友可以想一想这是为什么。建议大家做这样几个对比实验：

1、干脆就不写 !used[i - 1] 结果是什么样？
2、写 used[i - 1] 结果是什么，代码又是怎样执行的。这里给的结论是：!used[i - 1] 这样的剪枝更彻底。附录会分析原因。

参考代码 1：

import java.util.ArrayDeque;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.Deque;
import java.util.List;

public class Solution {

    public List<List<Integer>> permuteUnique(int[] nums) {
        int len = nums.length;
        List<List<Integer>> res = new ArrayList<>();
        if (len == 0) {
            return res;
        }

        // 排序（升序或者降序都可以），排序是剪枝的前提
        Arrays.sort(nums);

        boolean[] used = new boolean[len];
        // 使用 Deque 是 Java 官方 Stack 类的建议
        Deque<Integer> path = new ArrayDeque<>(len);
        dfs(nums, len, 0, used, path, res);
        return res;
    }

    private void dfs(int[] nums, int len, int depth, boolean[] used, Deque<Integer> path, List<List<Integer>> res) {
        if (depth == len) {
            res.add(new ArrayList<>(path));
            return;
        }

        for (int i = 0; i < len; ++i) {
            if (used[i]) {
                continue;
            }

            // 剪枝条件：i > 0 是为了保证 nums[i - 1] 有意义
            // 写 !used[i - 1] 是因为 nums[i - 1] 在深度优先遍历的过程中刚刚被撤销选择
            if (i > 0 && nums[i] == nums[i - 1] && !used[i - 1]) {
                continue;
            }

            path.addLast(nums[i]);
            used[i] = true;

            dfs(nums, len, depth + 1, used, path, res);
            // 回溯部分的代码，和 dfs 之前的代码是对称的
            used[i] = false;
            path.removeLast();
        }
    }

    public static void main(String[] args) {
        Solution solution = new Solution();
        int[] nums = {1, 1, 2};
        List<List<Integer>> res = solution.permuteUnique(nums);
        System.out.println(res);
    }
}
复杂度分析：（理由同第 46 题，重复元素越多，剪枝越多。但是计算复杂度的时候需要考虑最差情况。）

时间复杂度：O(N \times N!)O(N×N!)，这里 NN 为数组的长度。
空间复杂度：O(N \times N!)O(N×N!)。



补充说明
写 used[i - 1] 代码正确，但是不推荐的原因。

思路是根据深度优先遍历的执行流程，看一看那些状态变量（布尔数组 used）的值。

1、如果剪枝写的是：

if (i > 0 && nums[i] == nums[i - 1] && !used[i - 1]) {
    continue;
}
那么，对于数组 [1, 1’, 1’’, 2]，回溯的过程如下：

image.png

得到的全排列是：[[1, 1', 1'', 2], [1, 1', 2, 1''], [1, 2, 1', 1''], [2, 1, 1', 1'']]。特点是：1、1'、1'' 出现的顺序只能是 1、1'、1''。

2、如果剪枝写的是：

if (i > 0 && nums[i] == nums[i - 1] && used[i - 1]) {
    continue;
}
那么，对于数组 [1, 1’, 1’’, 2]，回溯的过程如下（因为过程稍显繁琐，所以没有画在一张图里）：

（1）先选第 1 个数字，有 4 种取法。

image.png

（2）对第 1 步的第 1 个分支，可以继续搜索，但是发现，没有搜索到合适的叶子结点。

image.png

（3）对第 1 步的第 2 个分支，可以继续搜索，但是同样发现，没有搜索到合适的叶子结点。

image.png

（4）对第 1 步的第 3 个分支，继续搜索发现搜索到合适的叶子结点。

image.png

（5）对第 1 步的第 4 个分支，继续搜索发现搜索到合适的叶子结点。

image.png

因此，used[i - 1] 前面加不加感叹号的区别仅在于保留的是相同元素的顺序索引，还是倒序索引。很明显，顺序索引（即使用 !used[i - 1] 作为剪枝判定条件得到）的递归树剪枝更彻底，思路也相对较自然。

下一篇：【HOT 100】47.全排列II Python3 回溯 考虑重复 --> 46.全排列

 
*/