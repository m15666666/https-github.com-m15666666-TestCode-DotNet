using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// https://leetcode-cn.com/problems/search-a-2d-matrix-ii/
/// 240. 搜索二维矩阵 II
/// 编写一个高效的算法来搜索 m x n 矩阵 matrix 中的一个目标值 target。该矩阵具有以下特性：
/// 每行的元素从左到右升序排列。
/// 每列的元素从上到下升序排列。
/// 示例:
/// 现有矩阵 matrix 如下：
/// [
/// [1,   4,  7, 11, 15],
/// [2,   5,  8, 12, 19],
/// [3,   6,  9, 16, 22],
/// [10, 13, 14, 17, 24],
/// [18, 21, 23, 26, 30]
/// ]
/// 给定 target = 5，返回 true。
/// 给定 target = 20，返回 false。
/// </summary>
class SearchA2DMatrixIISolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public bool SearchMatrix(int[,] matrix, int target)
    {
        if (matrix == null || matrix.Length == 0) return false;

        int m = matrix.GetLength(0);
        int n = matrix.GetLength(1);

        int i = m - 1;
        int j = 0;
        while ( -1 < i && j < n )
        {
            var v = matrix[i, j];
            if (target == v) return true;
            else if (target > v) j++;
            else i--;
        }
        return false;
    }
}
/*
//别人的算法
public class Solution {
    public bool SearchMatrix(int[,] matrix, int target) {
        
        int n = matrix.GetLength(0), m = matrix.GetLength(1);
        if (n == 0 || m == 0)
        {
            return false;
        }
        int x = 0, y = m -1;

        while (x < n && y >= 0)
        {
            if (matrix[x, y] < target)
            {
                x++;
            }
            else if (matrix[x, y] > target)
            {
                y--;
            }
            else
                return true;
        }
        return false;
    }
}
     
*/
