using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个无重复元素的数组 candidates 和一个目标数 target ，
找出 candidates 中所有可以使数字和为 target 的组合。

candidates 中的数字可以无限制重复被选取。

说明：

所有数字（包括 target）都是正整数。
解集不能包含重复的组合。 
示例 1:

输入: candidates = [2,3,6,7], target = 7,
所求解集为:
[
  [7],
  [2,2,3]
]
示例 2:

输入: candidates = [2,3,5], target = 8,
所求解集为:
[
  [2,2,2,2],
  [2,3,3],
  [3,5]
]
*/
/// <summary>
/// https://leetcode-cn.com/problems/combination-sum/
/// 39. 组合总和
/// 
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

        if (candidates == null || candidates.Length == 0 || target < 1) return ret;

        Array.Sort(candidates); // 需要排序，以便后续可以提前结束整个循环

        Stack<int> list = new Stack<int>();
        BackTrack(target, 0);

        return ret;

        void BackTrack(int target, int startIndex)
        {
            //if (target < 0) return; // 提前判断，减少一次函数调用

            if (target == 0)
            {
                var l = list.ToArray();
                ret.Add(l);
                return;
            }

            for (int i = startIndex; i < candidates.Length; i++)
            {
                var v = candidates[i];
                if (target < v) break; // 因为是排序数组，所以可以提前结束整个循环
                if (startIndex < i && v == candidates[i - 1]) continue;

                list.Push(v);

                BackTrack(target - v, i);

                list.Pop();
            }
        }
    }

    //public static IList<IList<int>> CombinationSum(int[] candidates, int target)
    //{
    //    List<IList<int>> ret = new List<IList<int>>();

    //    if (candidates == null || candidates.Length == 0) return ret;

    //    Array.Sort( candidates );

    //    HashSet<string> existing = new HashSet<string>();
    //    List<int> list = new List<int>();
    //    BackTrack( candidates, target, 0, list, ret, existing );

    //    return ret;
    //}

    //private static void BackTrack(int[] candidates, int target, int startIndex, List<int> list, List<IList<int>> ret, HashSet<string> existing)
    //{
    //    if (target < 0) return;

    //    if ( target == 0 )
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

    //    for (int i = startIndex; i < candidates.Length; i++)
    //    {
    //        var v = candidates[i];

    //        list.Insert(0, v);

    //        BackTrack( candidates, target - v, i, list, ret, existing );

    //        list.RemoveAt(0);
    //    }
    //}
}
/*
回溯算法 + 剪枝
liweiwei1419
发布于 9 个月前
62.3k
思路：根据示例 1：输入: candidates = [2,3,6,7]，target = 7。

候选数组里有 2 ，如果找到了 7 - 2 = 5 的所有组合，再在之前加上 2 ，就是 7 的所有组合；
同理考虑 3，如果找到了 7 - 3 = 4 的所有组合，再在之前加上 3 ，就是 7 的所有组合，依次这样找下去；
上面的思路就可以画成下面的树形图。
其实这里思路已经介绍完了，大家可以自己尝试在纸上画一下这棵树。然后编码实现，如果遇到问题，再看下面的文字。

39-1.png

说明：

蓝色结点表示：尝试找到组合之和为该数的所有组合，怎么找呢？逐个减掉候选数组中的元素即可；
以 target = 7 为根结点，每一个分支做减法；
减到 00 或者负数的时候，到了叶子结点；
减到 00 的时候结算，这里 “结算” 的意思是添加到结果集；
从根结点到叶子结点（必须为 0）的路径，就是题目要我们找的一个组合。
把文字的部分去掉。

39-2.png

如果这样编码的话，会发现提交不能通过，这是因为递归树画的有问题，下面看一下是什么原因。

39-3.png

画出图以后，我看了一下，我这张图画出的结果有 44 个 00，对应的路径是 [[2, 2, 3], [2, 3, 2], [3, 2, 2], [7]]，而示例中的解集只有 [[7], [2, 2, 3]]，很显然，重复的原因是在较深层的结点值考虑了之前考虑过的元素，因此我们需要设置“下一轮搜索的起点”即可（这里可能没有说清楚，已经尽力了）。

去重复
在搜索的时候，需要设置搜索起点的下标 begin ，由于一个数可以使用多次，下一层的结点从这个搜索起点开始搜索；
在搜索起点 begin 之前的数因为以前的分支搜索过了，所以一定会产生重复。
剪枝提速
如果一个数位搜索起点都不能搜索到结果，那么比它还大的数肯定搜索不到结果，基于这个想法，我们可以对输入数组进行排序，以减少搜索的分支；

排序是为了提高搜索速度，非必要；

搜索问题一般复杂度较高，能剪枝就尽量需要剪枝。把候选数组排个序，遇到一个较大的数，如果以这个数为起点都搜索不到结果，后面的数就更搜索不到结果了。

参考代码：
这里感谢 @rmokerone 提供的 C++ 代码实现。

import java.util.ArrayDeque;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.Deque;
import java.util.List;

public class Solution {

    public List<List<Integer>> combinationSum(int[] candidates, int target) {
        List<List<Integer>> res = new ArrayList<>();
        int len = candidates.length;

        // 排序是为了提前终止搜索
        Arrays.sort(candidates);

        dfs(candidates, len, target, 0, new ArrayDeque<>(), res);
        return res;
    }

     * @param candidates 数组输入
     * @param len        输入数组的长度，冗余变量
     * @param residue    剩余数值
     * @param begin      本轮搜索的起点下标
     * @param path       从根结点到任意结点的路径
     * @param res        结果集变量
     
private void dfs(int[] candidates,
                 int len,
                 int residue,
                 int begin,
                 Deque<Integer> path,
                 List<List<Integer>> res)
{
    if (residue == 0)
    {
        // 由于 path 全局只使用一份，到叶子结点的时候需要做一个拷贝
        res.add(new ArrayList<>(path));
        return;
    }

    for (int i = begin; i < len; i++)
    {

        // 在数组有序的前提下，剪枝
        if (residue - candidates[i] < 0)
        {
            break;
        }

        path.addLast(candidates[i]);
        dfs(candidates, len, residue - candidates[i], i, path, res);
        path.removeLast();

    }
}
}
附注：这道题我用的是减法，有兴趣的朋友还可以使用加法，加到 target 的时候结算，超过 target 的时候剪枝。

做完这题的朋友，不妨做一下 LeetCode 第 40 题：组合问题 II。

下一篇：非常详细的递归、回溯套路

public class Solution {
        public IList<IList<int>> CombinationSum(int[] candidates, int target)
        {
            var result = new List<IList<int>>();
            Array.Sort(candidates);
            Core(candidates, 0, target, new Stack<int>(), result);
            return result;
        }

        private void Core(int[] candidates, int start, int target, Stack<int> stack, List<IList<int>> result)
        {
            if (target < 0) return;
            else if (target == 0) result.Add(stack.ToArray());
            else
            {
                for (int i = start; i < candidates.Length; i++)
                {
                    if (candidates[i] > target) break;
                    stack.Push(candidates[i]);
                    Core(candidates, i, target - candidates[i], stack, result);
                    stack.Pop();
                }
            }

        }
}

public class Solution {
    public List<IList<int>> ans = new List<IList<int>>();
    public List<int> temp= new List<int>();
    public int len;
    int[] a;
    public IList<IList<int>> CombinationSum(int[] candidates, int target) {
        len = candidates.Length;
        if(len == 0) return ans;
        Array.Sort(candidates);
        a = candidates;
        if(candidates[0] > target) return ans; 
        dfs(0,target,0);
        
        return ans;
    }

    public void dfs(int start,int target, int sum) {
        if(sum > target) return;
        if(target == sum) {
            Console.WriteLine(temp.Count);
            List<int> b = new List<int>();
            foreach(var t in temp) {
                b.Add(t);
            }
            ans.Add(b);
            return ;
        } 

        for(int i = start; i < len; i ++) {
            temp.Add(a[i]);
            dfs(i,target,sum+a[i]);
            temp.RemoveAt(temp.Count-1);
        }
    }
}
*/
