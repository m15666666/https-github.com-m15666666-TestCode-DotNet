using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
编写一个高效的算法来判断 m x n 矩阵中，是否存在一个目标值。该矩阵具有如下特性：

每行中的整数从左到右按升序排列。
每行的第一个整数大于前一行的最后一个整数。
示例 1:

输入:
matrix = [
  [1,   3,  5,  7],
  [10, 11, 16, 20],
  [23, 30, 34, 50]
]
target = 3
输出: true
示例 2:

输入:
matrix = [
  [1,   3,  5,  7],
  [10, 11, 16, 20],
  [23, 30, 34, 50]
]
target = 13
输出: false

*/
/// <summary>
/// https://leetcode-cn.com/problems/search-a-2d-matrix/
/// 74.搜索二维矩阵
/// 
/// </summary>
class SearchMatrixClimbStairs
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public bool SearchMatrix(int[][] matrix, int target) 
    {
        if (matrix == null) return false;

        int m = matrix.Length;
        if (m == 0) return false;

        int n = matrix[0].Length;
        if (n == 0 || target < matrix[0][0] || matrix[m - 1][n - 1] < target) return false;

        int left = 0, right = m * n - 1;
        while (left <= right) 
        {
          int mid = (left + right) / 2;
          int v = matrix[mid / n][mid % n];
          if (target == v) return true;
          if (target < v) right = mid - 1;
          else left = mid + 1;
        }
        return false;
    }

    //public bool SearchMatrix(int[,] matrix, int target)
    //{
    //    if ( matrix == null || matrix.Length == 0) return false;

    //    int m = matrix.GetLength(0);
    //    int n = matrix.GetLength(1);

    //    if (target == matrix[0, 0] || matrix[m - 1, n - 1] == target) return true;
    //    if (target < matrix[0, 0] || matrix[m - 1, n - 1] < target) return false;

    //    int rowIndex = m - 1;
    //    if( target < matrix[m - 1, 0])
    //    {
    //        int beginIndex = 0, endIndex = m - 1;
    //        while( beginIndex <= endIndex)
    //        {
    //            rowIndex = ( beginIndex + endIndex ) / 2;
    //            if ( endIndex - beginIndex < 2 ) break;
    //            var v = matrix[rowIndex, 0];
    //            if (v == target) return true;
    //            if (target < v) endIndex = rowIndex;
    //            else beginIndex = rowIndex;
    //        }
    //    }

    //    int startIndex = 0, stopIndex = n - 1;

    //    while( startIndex <= stopIndex)
    //    {
    //        var midIndex = (startIndex + stopIndex) / 2;
    //        var v = matrix[rowIndex, midIndex];
    //        if (v == target) return true;
    //        if (target < v) stopIndex = midIndex - 1;
    //        else startIndex = midIndex + 1;
    //    }
    //    return false;
    //}
}
/*
public class Solution {
    public bool SearchMatrix(int[][] matrix, int target) {
        int xlen = matrix.Length;
        if (xlen == 0) return false;
        int ylen = matrix[0].Length;

        // binary search
        int left = 0, right = xlen * ylen - 1;
        //int mid, val;
        while (left <= right)
        {
            int mid = left + (right - left) / 2;
            int val = matrix[mid / ylen][mid % ylen];
            if (target == val) 
                return true;
            if (target < val) 
                right = mid - 1;
            else 
                left = mid + 1;
        }
        return false;        
    }
} 

*/