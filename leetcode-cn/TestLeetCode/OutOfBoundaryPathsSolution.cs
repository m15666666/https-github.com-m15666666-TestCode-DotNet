using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个 m × n 的网格和一个球。球的起始坐标为 (i,j) ，你可以将球移到相邻的单元格内，
或者往上、下、左、右四个方向上移动使球穿过网格边界。但是，你最多可以移动 N 次。找出可以将球移出边界的路径数量。答案可能非常大，返回 结果 mod 109 + 7 的值。

示例 1：

输入: m = 2, n = 2, N = 2, i = 0, j = 0
输出: 6
解释:

示例 2：

输入: m = 1, n = 3, N = 3, i = 0, j = 1
输出: 12
解释:

说明:

球一旦出界，就不能再被移动回网格内。
网格的长度和高度在 [1,50] 的范围内。
N 在 [0,50] 的范围内。 
*/
/// <summary>
/// https://leetcode-cn.com/problems/out-of-boundary-paths/
/// 576. 出界的路径数
/// https://blog.csdn.net/sinat_15723179/article/details/80960312
/// 
/// </summary>
class OutOfBoundaryPathsSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int FindPaths(int m, int n, int N, int i, int j)
    {
        long[,] k_1Dp = new long[m, n];
        long[,] kDp = new long[m, n];
        for (int x = 0; x < m; x++)
            for (int y = 0; y < n; y++)
                k_1Dp[x, y] = 0;

        const long mod = 1000000007;
        for (int k = 1; k <= N; ++k)
        {
            var dp = k_1Dp;
            for (int x = 0; x < m; ++x)
            {
                for (int y = 0; y < n; ++y)
                {
                    long v1 = (x == 0) ? 1 : dp[x - 1,y];
                    long v2 = (y == 0) ? 1 : dp[x,y - 1];
                    long v3 = (x == m - 1) ? 1 : dp[x + 1,y];
                    long v4 = (y == n - 1) ? 1 : dp[x,y + 1];

                    kDp[x,y] = (v1 + v2 + v3 + v4) % mod;
                }
            }
            k_1Dp = kDp;
            kDp = dp;
        }
        return (int)k_1Dp[i, j];
    }
}