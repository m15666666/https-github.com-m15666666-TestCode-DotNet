using System.Collections.Generic;

/*
找出所有相加之和为 n 的 k 个数的组合。组合中只允许含有 1 - 9 的正整数，并且每种组合中不存在重复的数字。

说明：

所有数字都是正整数。
解集不能包含重复的组合。 
示例 1:

输入: k = 3, n = 7
输出: [[1,2,4]]
示例 2:

输入: k = 3, n = 9
输出: [[1,2,6], [1,3,5], [2,3,4]]

*/

/// <summary>
/// https://leetcode-cn.com/problems/combination-sum-iii/
/// 216. 组合总和 III
/// 找出所有相加之和为 n 的 k 个数的组合。组合中只允许含有 1 - 9 的正整数，并且每种组合中不存在重复的数字。
/// 说明：
/// 所有数字都是正整数。
/// 解集不能包含重复的组合。
/// 示例 1:
/// 输入: k = 3, n = 7
/// 输出: [[1,2,4]]
/// 示例 2:
/// 输入: k = 3, n = 9
/// 输出: [[1,2,6], [1,3,5], [2,3,4]]
/// </summary>
internal class CombinationSumIIISolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public IList<IList<int>> CombinationSum3(int k, int n)
    {
        List<IList<int>> ret = new List<IList<int>>(); 
        var list = new Stack<int>(k);
        BackTrack(n, 1, k);

        return ret;

        void BackTrack(int target, int start, int maxCount)
        {
            if (target < 0) return;
            if (target == 0)
            {
                if (0 == maxCount)
                {
                    var l = list.ToArray();
                    ret.Add(l);
                }
                return;
            }

            if (10 - start < maxCount) return;
            for (int v = start; v < 10; v++)
            {
                if (target < v) break;
                list.Push(v);

                BackTrack(target - v, v + 1, maxCount - 1);

                list.Pop();
            }
        }
    }

    //private static readonly int[] candidates = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
    //public IList<IList<int>> CombinationSum3(int k, int n)
    //{
    //    List<IList<int>> ret = new List<IList<int>>();

    //    List<int> list = new List<int>();
    //    BackTrack(candidates, n, 0, list, ret, k);

    //    return ret;
    //}

    //private static void BackTrack(int[] candidates, int target, int startIndex, List<int> list, List<IList<int>> ret, int maxCount )
    //{
    //    if ( target < 0 ) return;
    //    //if ( maxCount < list.Count ) return;
    //    if ( candidates.Length - startIndex + list.Count < maxCount ) return;

    //    if (list.Count == maxCount)
    //    {
    //        if (target == 0)
    //        {
    //            var l = list.ToArray();
    //            ret.Add(l);
    //        }
    //        return;
    //    }
    //    else if ( target == 0 ) return;

    //    for (int i = startIndex; i < candidates.Length; i++)
    //    {
    //        var v = candidates[i];

    //        list.Insert(0, v);

    //        BackTrack(candidates, target - v, i + 1, list, ret, maxCount);

    //        list.RemoveAt(0);
    //    }
    //}
}

/*

*/