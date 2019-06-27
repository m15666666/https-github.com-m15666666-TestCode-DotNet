using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定字符串列表，你需要从它们中找出最长的特殊序列。最长特殊序列定义如下：该序列为某字符串独有的最长子序列（即不能是其他字符串的子序列）。

子序列可以通过删去字符串中的某些字符实现，但不能改变剩余字符的相对顺序。空序列为所有字符串的子序列，任何字符串为其自身的子序列。

输入将是一个字符串列表，输出是最长特殊序列的长度。如果最长特殊序列不存在，返回 -1 。

 

示例：

输入: "aba", "cdc", "eae"
输出: 3
 

提示：

所有给定的字符串长度不会超过 10 。
给定字符串列表的长度将在 [2, 50 ] 之间。
*/
/// <summary>
/// https://leetcode-cn.com/problems/longest-uncommon-subsequence-ii/
/// 522. 最长特殊序列 II
/// https://zhuanlan.zhihu.com/p/56515275
/// </summary>
class LongestUncommonSubsequenceIISolution
{
    public void Test()
    {
        var ret = FindLUSlength(new string[] { "aabbcc", "aabbcc", "c", "e", "aabbcd" });
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int FindLUSlength(string[] strs)
    {
        if (strs == null || strs.Length == 0) return -1;
        strs = strs.Where( s => !string.IsNullOrEmpty(s) ).OrderBy(s =>s.Length).ToArray();

        int n = strs.Length;
        if (n == 1) return strs[0].Length;
        if (strs[n - 2].Length < strs[n - 1].Length) return strs[n - 1].Length;

        int ret = -1;
        HashSet<string> set = new HashSet<string>();
        for (int i = 0; i < n; i++)
        {
            var sI = strs[i];
            if (set.Contains(sI)) continue;
            set.Add(sI);

            int j = i + 1;
            for (; j < n; j++)
                if (IsSubString(sI, strs[j])) break;

            if (j == n)
            {
                var r = sI.Length;
                if (ret < r) ret = r;
            }
        }

        return ret;

        bool IsSubString(string s1, string s2)
        {
            int i = 0;
            foreach (var c in s2)
            {
                if (c == s1[i])
                {
                    i++;
                    if (i == s1.Length) break;
                }
            }
            return i == s1.Length;
        }
    }
}