using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个字符串 S 和一个字符串 T，计算在 S 的子序列中 T 出现的个数。

一个字符串的一个子序列是指，通过删除一些（也可以不删除）字符且不干扰剩余字符相对位置所组成的新字符串。（例如，"ACE" 是 "ABCDE" 的一个子序列，而 "AEC" 不是）

题目数据保证答案符合 32 位带符号整数范围。

 

示例 1：

输入：S = "rabbbit", T = "rabbit"
输出：3
解释：

如下图所示, 有 3 种可以从 S 中得到 "rabbit" 的方案。
(上箭头符号 ^ 表示选取的字母)

rabbbit
^^^^ ^^
rabbbit
^^ ^^^^
rabbbit
^^^ ^^^
示例 2：

输入：S = "babgbag", T = "bag"
输出：5
解释：

如下图所示, 有 5 种可以从 S 中得到 "bag" 的方案。 
(上箭头符号 ^ 表示选取的字母)

babgbag
^^ ^
babgbag
^^    ^
babgbag
^    ^^
babgbag
  ^  ^^
babgbag
    ^^^

*/
/// <summary>
/// https://leetcode-cn.com/problems/distinct-subsequences/
/// 115. 不同的子序列
/// 
/// 
/// 
/// </summary>
class DistinctSubsequencesSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int NumDistinct(string s, string t) 
    {
        int[] dp = new int[s.Length + 1];
        dp[0] = 0;
        
        char c = t[0];
        // t的第一个字符
        for (int j = 1; j <= s.Length; j++) dp[j] = c == s[j - 1] ? 1 + dp[j - 1] : dp[j - 1];

        for (int i = 2; i <= t.Length; i++) 
        {
            c = t[i - 1];
            int prevRowJ_1 = 0;
            for (int j = 1; j <= s.Length; j++) {
                int prevRowJ = dp[j];
                dp[j] = (c == s[j - 1]) ? prevRowJ_1 + dp[j - 1] : dp[j - 1];
                prevRowJ_1 = prevRowJ;
            }
        }
        return dp[s.Length];
    }

}
/*

动态规划
powcai
发布于 2019-07-01
13.7k
思路:
动态规划

dp[i][j] 代表 T 前 i 字符串可以由 S j 字符串组成最多个数.

所以动态方程:

当 S[j] == T[i] , dp[i][j] = dp[i-1][j-1] + dp[i][j-1];

当 S[j] != T[i] , dp[i][j] = dp[i][j-1]

举个例子,如示例的

1561970400084.png

对于第一行, T 为空,因为空集是所有字符串子集, 所以我们第一行都是 1

对于第一列, S 为空,这样组成 T 个数当然为 0` 了

至于下面如何进行,大家可以通过动态方程,自行模拟一下!

代码:

class Solution {
    public int numDistinct(String s, String t) {
        int[][] dp = new int[t.length() + 1][s.length() + 1];
        for (int j = 0; j < s.length() + 1; j++) dp[0][j] = 1;
        for (int i = 1; i < t.length() + 1; i++) {
            for (int j = 1; j < s.length() + 1; j++) {
                if (t.charAt(i - 1) == s.charAt(j - 1)) dp[i][j] = dp[i - 1][j - 1] + dp[i][j - 1];
                else dp[i][j] = dp[i][j - 1];
            }
        }
        return dp[t.length()][s.length()];
    }
}
下一篇：详细通俗的思路分析，多解法

public class Solution {
    public int NumDistinct(string s, string t)
    {
        int[,] dp = new int[s.Length + 1, t.Length + 1];
        for (int i = 0; i <= s.Length; i++)
        {
            dp[i, 0] = 1;
        }
        for (int i = 0; i < s.Length; i++)
        {
            for (int j = 0; j < t.Length; j++)
            {
                if (s[i] == t[j])
                {
                    dp[i + 1, j + 1] = dp[i, j + 1] + dp[i, j];
                }
                else
                {
                    dp[i + 1, j + 1] = dp[i, j + 1];
                }
            }
        }
        return dp[s.Length, t.Length];
    }
}

public class Solution {
    public int NumDistinct(string s, string t) {
                    if(string.IsNullOrEmpty(s) || string.IsNullOrEmpty(t)) return 0;
            int[,] dp = new int[s.Length + 1, t.Length + 1];
            int cnt = 0;
            for(int i = 0; i < s.Length; i++)
            {
                if (s[i] == t[0]) cnt++;
                dp[i + 1, 1] = cnt;
            }
            for(int i=1;i<=s.Length;i++)
            {
                for(int j=2;j<=t.Length;j++)
                {
                    if (s[i-1] == t[j-1]) dp[i,j] = dp[i - 1, j - 1] + dp[i - 1, j];
                    else dp[i,j] = dp[i - 1, j];
                }
            }
            return dp[s.Length,t.Length];
    }
}

public class Solution {
    public int NumDistinct(string s, string t) {
        int[,] dp = new int[s.Length + 1, t.Length + 1];
        //初始化，如果S是空串，但是T不是空串，那么出现次数为0
        for (int j = 0; j <= t.Length; j++) {
            dp[0,j] = 0;
        }
        //初始化，如果S不是空串，但是T是空串，出现次数为1
        for (int i = 0; i <= s.Length; i++) {
            dp[i,0] = 1;
        }
        for (int i = 1; i <= s.Length; ++i){
            for (int j = 1; j <= t.Length; ++j){
                if (s[i - 1] == t[j - 1]){
                    dp[i,j] = dp[i - 1,j - 1] + dp[i - 1,j];
                }else {
                    dp[i,j] = dp[i - 1,j];
                }
            }
        }
        return dp[s.Length,t.Length];
    }
}

 
 
 
 
*/
