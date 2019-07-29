using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定两个整数 n 和 k，你需要实现一个数组，这个数组包含从 1 到 n 的 n 个不同整数，同时满足以下条件：

① 如果这个数组是 [a1, a2, a3, ... , an] ，那么数组 [|a1 - a2|, |a2 - a3|, |a3 - a4|, ... , |an-1 - an|] 中应该有且仅有 k 个不同整数；.

② 如果存在多种答案，你只需实现并返回其中任意一种.

示例 1:

输入: n = 3, k = 1
输出: [1, 2, 3]
解释: [1, 2, 3] 包含 3 个范围在 1-3 的不同整数， 并且 [1, 1] 中有且仅有 1 个不同整数 : 1
 
示例 2:

输入: n = 3, k = 2
输出: [1, 3, 2]
解释: [1, 3, 2] 包含 3 个范围在 1-3 的不同整数， 并且 [2, 1] 中有且仅有 2 个不同整数: 1 和 2

提示:

 n 和 k 满足条件 1 <= k < n <= 104. 
*/
/// <summary>
/// https://leetcode-cn.com/problems/beautiful-arrangement-ii/
/// 667. 优美的排列 II
/// https://blog.csdn.net/fys465253658/article/details/82144957
/// </summary>
class BeautifulArrangementIISolution
{
    public void Test()
    {
        var ret = ConstructArray(7, 1);
        ret = ConstructArray(7, 2);
        ret = ConstructArray(7, 3);
        ret = ConstructArray(7, 4);
        ret = ConstructArray(7, 5);
        ret = ConstructArray(7, 6);
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int[] ConstructArray(int n, int k)
    {
        int[] ret = new int[n];
        Array.Fill(ret, 0);
        int index = 0;
        for (int i = 1; i < n - k; i++)
            ret[index++] = i;

        for (int i = 0; i <= k; i++)
        {
            int offset = i / 2;
            ret[index++] = (i % 2 == 0) ? (n - k + offset) : (n - offset);
        }

        return ret;
    }
}