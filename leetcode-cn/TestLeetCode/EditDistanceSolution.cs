using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定两个单词 word1 和 word2，计算出将 word1 转换成 word2 所使用的最少操作数 。

你可以对一个单词进行如下三种操作：

插入一个字符
删除一个字符
替换一个字符
示例 1:

输入: word1 = "horse", word2 = "ros"
输出: 3
解释: 
horse -> rorse (将 'h' 替换为 'r')
rorse -> rose (删除 'r')
rose -> ros (删除 'e')
示例 2:

输入: word1 = "intention", word2 = "execution"
输出: 5
解释: 
intention -> inention (删除 't')
inention -> enention (将 'i' 替换为 'e')
enention -> exention (将 'n' 替换为 'x')
exention -> exection (将 'n' 替换为 'c')
exection -> execution (插入 'u') 
*/
/// <summary>
/// https://leetcode-cn.com/problems/edit-distance/
/// 72. 编辑距离
/// 
/// </summary>
class EditDistanceSolution
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
        int n1 = word1.Length;
        int n2 = word2.Length;

        if (n1 * n2 == 0) return n1 + n2;

        int[,] d = new int[n1 + 1, n2 + 1];

        for (int i = 0; i < n1 + 1; i++) d[i,0] = i;
        for (int j = 0; j < n2 + 1; j++) d[0,j] = j;

        for (int i = 1; i < n1 + 1; i++)
        {
            var c1 = word1[i - 1];
            for (int j = 1; j < n2 + 1; j++)
            {
                int prevRow = d[i - 1,j] + 1;
                int prevColumn = d[i,j - 1] + 1;
                int prevRowColumn = d[i - 1,j - 1];
                if (c1 != word2[j - 1]) prevRowColumn += 1;

                d[i,j] = Math.Min(prevRow, Math.Min(prevColumn, prevRowColumn));
            }
        }
        return d[n1,n2];
    }
}
/*
编辑距离
力扣 (LeetCode)
发布于 9 个月前
29.4k
想法
编辑距离算法被数据科学家广泛应用，是用作机器翻译和语音识别评价标准的基本算法。

最简单的方法是检查所有可能的编辑序列，从中找出最短的一条。但这个序列总数可能达到指数级，但完全不需要这么多，因为我们只要找到距离最短的那条而不是所有可能的序列。

方法 1：动态规划
我们的目的是让问题简单化，比如说两个单词 horse 和 ros 计算他们之间的编辑距离 D，容易发现，如果把单词变短会让这个问题变得简单，很自然的想到用 D[n][m] 表示输入单词长度为 n 和 m 的编辑距离。

具体来说，D[i][j] 表示 word1 的前 i 个字母和 word2 的前 j 个字母之间的编辑距离。

72-1.png

当我们获得 D[i-1][j]，D[i][j-1] 和 D[i-1][j-1] 的值之后就可以计算出 D[i][j]。

每次只可以往单个或者两个字符串中插入一个字符

那么递推公式很显然了

如果两个子串的最后一个字母相同，word1[i] = word2[i] 的情况下：

D[i][j] = 1 + \min(D[i - 1][j], D[i][j - 1], D[i - 1][j - 1] - 1)
D[i][j]=1+min(D[i−1][j],D[i][j−1],D[i−1][j−1]−1)

否则，word1[i] != word2[i] 我们将考虑替换最后一个字符使得他们相同：

D[i][j] = 1 + \min(D[i - 1][j], D[i][j - 1], D[i - 1][j - 1])
D[i][j]=1+min(D[i−1][j],D[i][j−1],D[i−1][j−1])

所以每一步结果都将基于上一步的计算结果，示意如下：

72-2.png

同时，对于边界情况，一个空串和一个非空串的编辑距离为 D[i][0] = i 和 D[0][j] = j。

综上我们得到了算法的全部流程。



class Solution {
  public int minDistance(String word1, String word2) {
    int n = word1.length();
    int m = word2.length();

    // if one of the strings is empty
    if (n * m == 0)
      return n + m;

    // array to store the convertion history
    int [][] d = new int[n + 1][m + 1];

    // init boundaries
    for (int i = 0; i < n + 1; i++) {
      d[i][0] = i;
    }
    for (int j = 0; j < m + 1; j++) {
      d[0][j] = j;
    }

    // DP compute 
    for (int i = 1; i < n + 1; i++) {
      for (int j = 1; j < m + 1; j++) {
        int left = d[i - 1][j] + 1;
        int down = d[i][j - 1] + 1;
        int left_down = d[i - 1][j - 1];
        if (word1.charAt(i - 1) != word2.charAt(j - 1))
          left_down += 1;
        d[i][j] = Math.min(left, Math.min(down, left_down));

      }
    }
    return d[n][m];
  }
}
复杂度分析

时间复杂度 ：O(m n)O(mn)，两层循环显而易见。
空间复杂度 ：O(m n)O(mn)，循环的每一步都要记录结果。
下一篇：自底向上 和自顶向下
 
public class Solution
{
    int[,] dic;
    public int MinDistance(string word1, string word2)
    {
        if (word1 == "") return word2.Length;
        if (word2 == "") return word1.Length;
        char[] nw1 = word1.ToCharArray();
        char[] nw2 = word2.ToCharArray();
        dic = new int[nw1.Length + 1, nw2.Length + 1];
        for (int i = 0; i < nw1.Length + 1; i++)
            dic[i, 0] = i;
        for (int j = 0; j < nw2.Length + 1; j++)
            dic[0, j] = j;
        return Compare(nw1, nw2, nw1.Length-1 , nw2.Length-1 );
    }
    public int Compare(char[] nw1, char[] nw2, int i, int j)
    {
        if (dic[i+1 , j + 1] != 0)
            return dic[i + 1, j + 1];
        else if (i == 0 && j == 0)
            if (nw1[0] == nw2[0])
                return 0;
            else return 1;

        if (nw1[i] == nw2[j])
        {
            dic[i + 1, j + 1] = Compare(nw1, nw2, i - 1, j - 1);
            return dic[i + 1, j + 1];
        }
        else
        {
            int add = Compare(nw1, nw2, i, j - 1) + 1;
            int ins = Compare(nw1, nw2, i-1, j - 1) + 1;
            int del = Compare(nw1, nw2, i-1, j ) + 1;

            int min = Math.Min(add, ins);
            min = Math.Min(min, del);
            dic[i + 1, j + 1] = min;
            return min;
        }
    }
}

public class Solution {
    public int MinDistance(string word1, string word2)
    {
        word1 = word1 ?? string.Empty;
        word2 = word2 ?? string.Empty;

        if (string.IsNullOrEmpty(word1)) return word2.Length;
        if (string.IsNullOrEmpty(word2)) return word1.Length;

        var d = new int[word2.Length];

        for (int i = 0; i < word2.Length; i++)
        {
            d[i] = i+1;
        }

        for (int i = 0; i < word1.Length; i++)
        {
            var dj = i+1;
            var x = i;
            for (int j = 0; j < word2.Length; j++)
            {
                var m = Math.Min(dj, d[j]);
                dj = word1[i] == word2[j]
                    ? Math.Min(m, x - 1) + 1
                    : Math.Min(m, x) + 1;
                x = d[j];
                d[j] = dj;
            }
        }

        return d[word2.Length-1];
    }
}
*/
