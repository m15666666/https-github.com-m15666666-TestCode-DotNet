using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个字符串s，找到其中最长的回文子序列。可以假设s的最大长度为1000。

示例 1:
输入:

"bbbab"
输出:

4
一个可能的最长回文子序列为 "bbbb"。

示例 2:
输入:

"cbbd"
输出:

2
一个可能的最长回文子序列为 "bb"。
*/
/// <summary>
/// https://leetcode-cn.com/problems/longest-palindromic-subsequence/
/// 516. 最长回文子序列
/// https://www.jianshu.com/p/95498a13d1f2
/// </summary>
class LongestPalindromicSubsequenceSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int LongestPalindromeSubseq(string s)
    {
        if (string.IsNullOrEmpty(s)) return 0;

        int len = s.Length;
        if (len == 1) return 1;

        int[,] dp = new int[len,len];
        dp[0, 0] = 1;
        for (int i = 1; i < len; i++)
        {
            dp[i,i] = 1;

            var c = s[i];
            dp[i - 1, i] = (c == s[i - 1] ? 2 : 1);

            for (int j = i - 2; -1 < j; j--)
            {
                if (c == s[j]) dp[j, i] = dp[j + 1, i - 1] + 2;
                else dp[j, i] = Math.Max(dp[j, i - 1], dp[j + 1, i]);
            }
        }
        return dp[0,len - 1];
    }
}