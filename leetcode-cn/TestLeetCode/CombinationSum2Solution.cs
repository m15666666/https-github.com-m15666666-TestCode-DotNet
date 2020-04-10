using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个数组 candidates 和一个目标数 target ，找出 candidates 中所有可以使数字和为 target 的组合。

candidates 中的每个数字在每个组合中只能使用一次。

说明：

所有数字（包括目标数）都是正整数。
解集不能包含重复的组合。 
示例 1:

输入: candidates = [10,1,2,7,6,1,5], target = 8,
所求解集为:
[
  [1, 7],
  [1, 2, 5],
  [2, 6],
  [1, 1, 6]
]
示例 2:

输入: candidates = [2,5,2,1,2], target = 5,
所求解集为:
[
  [1,2,2],
  [5]
]
*/
/// <summary>
/// https://leetcode-cn.com/problems/combination-sum-ii/
/// 40. 组合总和 II
/// 
/// </summary>
class CombinationSum2Solution
{
    public static void Test()
    {
        int[] nums = new int[] {3, 2, 6, 7};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        CombinationSum2(nums, 7);
        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public static IList<IList<int>> CombinationSum2(int[] candidates, int target)
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

                // 每个数字在每个组合中只能使用一次，所以索引要加一
                BackTrack(target - v, i + 1); 

                list.Pop();
            }
        }
    }

    //public static IList<IList<int>> CombinationSum2(int[] candidates, int target)
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

    //        BackTrack( candidates, target - v, i + 1, list, ret, existing );

    //        list.RemoveAt(0);
    //    }
    //}
}
/*

回溯算法 + 剪枝
liweiwei1419
发布于 9 个月前
21.7k
这道题与上一问的区别在于：

第 39 题：candidates 中的数字可以无限制重复被选取。
第 40 题：candidates 中的每个数字在每个组合中只能使用一次。
编码的不同在于下一层递归的起始索引不一样。

第 39 题：还从候选数组的当前索引值开始。
第 40 题：从候选数组的当前索引值的下一位开始。
相同之处：解集不能包含重复的组合。

为了使得解集不包含重复的组合。我们想一想，如何去掉一个数组中重复的元素，除了使用哈希表以外，我们还可以先对数组升序排序，重复的元素一定不是排好序以后的第 1 个元素和相同元素的第 1 个元素。根据这个思想，我们先对数组升序排序是有必要的。候选数组有序，对于在递归树中发现重复分支，进而“剪枝”也是有效的。

思路分析：

这道题其实比上一问更简单，思路是：

以 target 为根结点，依次减去数组中的数字，直到小于 00 或者等于 00，把等于 00 的结果记录到结果集中。

当然你也可以以 00 为根结点，依次加上数组中的数字，直到大于 target 或者等于 target，把等于 target 的结果记录到结果集中。

“解集不能包含重复的组合”，就提示我们得对数组先排个序（“升序”或者“降序”均可，下面示例中均使用“升序”）。
“candidates 中的每个数字在每个组合中只能使用一次”，那就按照顺序依次减去数组中的元素，递归求解即可：遇到 00 就结算且回溯，遇到负数也回溯。
candidates 中的数字可以重复，可以借助「力扣」第 47 题：“全排列 II” 的思想，在搜索的过程中，找到可能发生重复结果的分支，把它剪去。
（温馨提示：下面的幻灯片中，有几页上有较多的文字，可能需要您停留一下，可以点击右下角的后退 “|◀” 或者前进 “▶|” 按钮控制幻灯片的播放。）



参考代码 1：以 target 为根结点，依次减去数组中的数字，直到小于 00 或者等于 00，把等于 00 的结果记录到结果集中。

感谢用户 @rmokerone 提供的 C++ 版本的参考代码。

import java.util.ArrayDeque;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.Deque;
import java.util.List;

public class Solution {

     * @param candidates 候选数组
     * @param len
     * @param begin      从候选数组的 begin 位置开始搜索
     * @param residue    表示剩余，这个值一开始等于 target，基于题目中说明的"所有数字（包括目标数）都是正整数"这个条件
     * @param path       从根结点到叶子结点的路径
     * @param res
private void dfs(int[] candidates, int len, int begin, int residue, Deque<Integer> path, List<List<Integer>> res)
{
    if (residue == 0)
    {
        res.add(new ArrayList<>(path));
        return;
    }
    for (int i = begin; i < len; i++)
    {
        // 大剪枝
        if (residue - candidates[i] < 0)
        {
            break;
        }

        // 小剪枝
        if (i > begin && candidates[i] == candidates[i - 1])
        {
            continue;
        }

        path.addLast(candidates[i]);

        // 因为元素不可以重复使用，这里递归传递下去的是 i + 1 而不是 i
        dfs(candidates, len, i + 1, residue - candidates[i], path, res);

        path.removeLast();
    }
}

public List<List<Integer>> combinationSum2(int[] candidates, int target)
{
    int len = candidates.length;
    List<List<Integer>> res = new ArrayList<>();
    if (len == 0)
    {
        return res;
    }

    // 先将数组排序，这一步很关键
    Arrays.sort(candidates);

    Deque<Integer> path = new ArrayDeque<>(len);
    dfs(candidates, len, 0, target, path, res);
    return res;
}
}
这里按照用户 @Aspire 提供的思路，给出从 00 开始，一个使用加法，搜索加到目标数的写法，“前提是排序（升序降序均可）”，然后“剪枝”的操作和上面一样。

40-3.png

参考代码 2：以 00 为根结点，依次加上数组中的数字，直到大于 target 或者等于 target，把等于 target 的结果记录到结果集中。

#include <iostream>
#include <vector>
#include <map>

using namespace std;

class Solution
{
    public:

    vector<int> input;
    int target;
    vector<vector<int>> result;
    vector<int> vc;

    void dfs(int index, int sum)
    {
        // index >= input.size() ，写成 index == input.size() 即可
        // 因为每次都 + 1，在 index == input.size() 剪枝就可以了
        if (sum >= target || index == input.size())
        {
            if (sum == target)
            {
                result.push_back(vc);
            }
            return;
        }
        for (int i = index; i < input.size(); i++)
        {
            if (input[i] > target)
            {
                continue;
            }

            // 【我添加的代码在这里】：
            // 1、i > index 表明剪枝的分支一定不是当前层的第 1 个分支
            // 2、input[i - 1] == input[i] 表明当前选出来的数等于当前层前一个分支选出来的数
            // 因为前一个分支的候选集合一定大于后一个分支的候选集合
            // 故后面出现的分支中一定包含了前面分支出现的结果，因此剪枝
            // “剪枝”的前提是排序，升序或者降序均可
            if (i > index && input[i - 1] == input[i])
            {
                continue;
            }

            vc.push_back(input[i]);
            sum += input[i];
            dfs(i + 1, sum);
            vc.pop_back();
            sum -= input[i];
        }
    }

    vector<vector<int>> combinationSum2(vector<int> &candidates, int target)
    {
        // “剪枝”的前提是排序，升序或者降序均可
        sort(candidates.begin(), candidates.end());
        this->input = candidates;
        this->target = target;
        dfs(0, 0);
        return result;
    }
};


int main()
{
    cout << "LeetCode 第 40 题：组合问题 II" << endl;
    Solution solution = Solution();

    vector<int> candidates;
    candidates.push_back(10);
    candidates.push_back(1);
    candidates.push_back(2);
    candidates.push_back(7);
    candidates.push_back(6);
    candidates.push_back(1);
    candidates.push_back(5);

    int target = 8;
    vector<vector<int>> res = solution.combinationSum2(candidates, target);
    for (int i = 0; i < res.size(); ++i)
    {
        for (int j = 0; j < res[i].size(); ++j)
        {
            cout << res[i][j] << ",";
        }
        cout << "" << endl;
    }
    return 0;
}
下一篇：C++ DFS

*/