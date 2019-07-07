using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定两个单词 word1 和 word2，找到使得 word1 和 word2 相同所需的最小步数，
每步可以删除任意一个字符串中的一个字符。

示例 1:

输入: "sea", "eat"
输出: 2
解释: 第一步将"sea"变为"ea"，第二步将"eat"变为"ea"
说明:

给定单词的长度不超过500。
给定单词中的字符只含有小写字母。 
*/
/// <summary>
/// https://leetcode-cn.com/problems/delete-operation-for-two-strings/
/// 583. 两个字符串的删除操作
/// https://blog.csdn.net/u010420283/article/details/85109616
/// https://blog.csdn.net/w8253497062015/article/details/80307394
/// </summary>
class DeleteOperationForTwoStringsSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int MinDistance(string word1, string word2)
    {
        if (word1 == null && word2 == null ) return 0;
        if (word1 == null || word1.Length == 0) return word2.Length;
        if (word2 == null || word2.Length == 0) return word1.Length;

        int m = word1.Length;
        int n = word2.Length;
        int[,] dp = new int[n + 1, m + 1];
        for (int i = 0; i <= n; i++)
            dp[i, 0] = 0;
        for (int i = 0; i <= m; i++)
            dp[0, i] = 0;
        for( int i = 1; i <= n; i++)
        {
            for( int j = 1; j <= m; j++)
            {
                if (word2[i - 1] == word1[j - 1])
                    dp[i, j] = dp[i - 1, j - 1] + 1;
                else
                    dp[i, j] = Math.Max(dp[i - 1, j], dp[i, j - 1]);
            }
        }
        var maxCommonSubstring = dp[n, m];
        return (m - maxCommonSubstring) + (n - maxCommonSubstring);
    }
}