﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个包含了一些 0 和 1的非空二维数组 grid , 一个 岛屿 是由四个方向 (水平或垂直) 的 1 (代表土地) 构成的组合。你可以假设二维矩阵的四个边缘都被水包围着。

找到给定的二维数组中最大的岛屿面积。(如果没有岛屿，则返回面积为0。)

示例 1:

[[0,0,1,0,0,0,0,1,0,0,0,0,0],
 [0,0,0,0,0,0,0,1,1,1,0,0,0],
 [0,1,1,0,1,0,0,0,0,0,0,0,0],
 [0,1,0,0,1,1,0,0,1,0,1,0,0],
 [0,1,0,0,1,1,0,0,1,1,1,0,0],
 [0,0,0,0,0,0,0,0,0,0,1,0,0],
 [0,0,0,0,0,0,0,1,1,1,0,0,0],
 [0,0,0,0,0,0,0,1,1,0,0,0,0]]
对于上面这个给定矩阵应返回 6。注意答案不应该是11，因为岛屿只能包含水平或垂直的四个方向的‘1’。

示例 2:

[[0,0,0,0,0,0,0,0]]
对于上面这个给定的矩阵, 返回 0。

注意: 给定的矩阵grid 的长度和宽度都不超过 50。 
*/
/// <summary>
/// https://leetcode-cn.com/problems/max-area-of-island/
/// 695. 岛屿的最大面积
/// https://blog.csdn.net/w8253497062015/article/details/79967958
/// </summary>
class MaxAreaOfIslandSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int MaxAreaOfIsland(int[][] grid)
    {
        int ret = 0;
        int n = grid.Length;
        int m = grid[0].Length;
        for (int i = 0; i < n; i++)
            for (int j = 0; j < m; j++)
            {
                if (grid[i][j] == 1)
                {
                    var max = Dfs(grid, n, m, i, j);
                    if (ret < max) ret = max;
                }
            }
        return ret;
    }

    private static int[,] Directions = new int[,] { { 0, 1 }, { 0, -1 }, { 1, 0 }, { -1, 0 } };
    private static int Dfs(int[][] grid, int n, int m, int x0, int y0)
    {
        int sum = 1;

        grid[x0][y0] = 0; // 当前元素设置为0，避免再次搜索到
        for(int i=0; i<4; i++){
            int x = x0 + Directions[i,0];
            int y = y0 + Directions[i,1];
            if(0 <= x && x < n && 0 <= y && y < m && grid[x][y] == 1 )
                sum += Dfs(grid, n, m, x, y);
        }
        return sum;
    }
}