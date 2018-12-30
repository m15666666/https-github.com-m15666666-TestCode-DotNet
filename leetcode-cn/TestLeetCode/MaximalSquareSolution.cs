﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// https://leetcode-cn.com/problems/maximal-square/
/// 221. 最大正方形
/// 在一个由 0 和 1 组成的二维矩阵内，找到只包含 1 的最大正方形，并返回其面积。
/// 示例:
/// 输入: 
/// 1 0 1 0 0
/// 1 0 1 1 1
/// 1 1 1 1 1
/// 1 0 0 1 0
/// 输出: 4
/// https://blog.csdn.net/w8253497062015/article/details/80143432
/// </summary>
class MaximalSquareSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public int MaximalSquare(char[,] matrix)
    {
        const char One = '1';
        int m = matrix.GetLength(0);
        int n = matrix.GetLength(1);
        int[,] areas = new int[m, n];
        int maxLength = 0;
        for( int i = 0; i < m; i++)
            if(matrix[i,0] == One)
            {
                areas[i, 0] = 1;
                maxLength = 1;
            }else areas[i, 0] = 0;

        for (int i = 0; i < n; i++)
            if (matrix[0, i] == One)
            {
                areas[0, i] = 1;
                maxLength = 1;
            }
            else areas[0, i] = 0;

        for( int i = 1; i < m; i++)
            for(int j = 1; j < n; j++)
            {
                if (matrix[i, j] == One)
                {
                    var newLength = Math.Min(areas[i-1,j-1], Math.Min(areas[i - 1, j], areas[i, j - 1])) + 1;
                    areas[i, j] = newLength;
                    if (maxLength < newLength) maxLength = newLength;
                }
                else areas[i, j] = 0;
            }
        return maxLength * maxLength;
    }
}
/*
// 别人的算法
public class Solution {
    public int MaximalSquare(char[,] matrix)
        {
            int len = matrix.GetLength(0);
            int len2 = matrix.GetLength(1);

            int max = 0;

            for (int i = 0; i < len; ++i)
            {
                for (int j = 0; j < len2; ++j)
                {
                    max = Math.Max(max, GetArea(matrix,i, j));
                }
            }

            return max;
        }

        public int GetArea(char[,] list, int x, int y)
        {
            if (list[x, y] == 48)
                return 0;

            int len = 1;
            while (true)
            {
                if (x + len >= list.GetLength(0) || y + len >= list.GetLength(1))
                    return len * len;

                for (int i = x; i <= x+len; ++i)
                {
                    if (list[i, y + len] == 48)
                        return len * len;
                }

                for (int i = y; i <= y+len; ++i)
                {
                    if (list[x + len, i] == 48)
                        return len *len;
                }

                len++;
            }
        }
}
*/
