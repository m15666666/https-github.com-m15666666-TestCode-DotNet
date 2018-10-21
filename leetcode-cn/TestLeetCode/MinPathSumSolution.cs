using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// https://leetcode-cn.com/problems/minimum-path-sum/
/// 64.最小路径和
/// 给定一个包含非负整数的 m x n 网格，请找出一条从左上角到右下角的路径，使得路径上的数字总和为最小。
/// 说明：每次只能向下或者向右移动一步。
/// </summary>
class MinPathSumSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public int MinPathSum(int[,] grid)
    {
        if (grid == null) return 0;

        var m = grid.GetLength(0);
        var n = grid.GetLength(1);
        if (m < 1 || n < 1) return 0;

        int[] vertical = new int[m];

        // 最右面一列
        for ( int i = 0; i < vertical.Length; i++ )
        {
            if( i == 0)
            {
                vertical[i] = grid[m - 1, n - 1];
                continue;
            }
            vertical[i] = grid[m - 1 - i, n - 1] + vertical[i-1];
        }

        for (int c = 1; c < n; c++)
        {
            for (int i = 0; i < vertical.Length; i++)
            {
                if( i == 0)
                {
                    vertical[i] = grid[m - 1, n - 1 - c] + vertical[i];
                    continue;
                }

                vertical[i] = grid[m - 1 - i, n - 1 - c] + Math.Min( vertical[i], vertical[i-1]);
            }
        }

        return vertical[vertical.Length - 1];
    }

}