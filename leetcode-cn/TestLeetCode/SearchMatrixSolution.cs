using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// https://leetcode-cn.com/problems/search-a-2d-matrix/
/// 74.搜索二维矩阵
/// 编写一个高效的算法来判断 m x n 矩阵中，是否存在一个目标值。该矩阵具有如下特性：
/// 每行中的整数从左到右按升序排列。
/// 每行的第一个整数大于前一行的最后一个整数。
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


    public bool SearchMatrix(int[,] matrix, int target)
    {
        if ( matrix == null || matrix.Length == 0) return false;

        int m = matrix.GetLength(0);
        int n = matrix.GetLength(1);

        if (target == matrix[0, 0] || matrix[m - 1, n - 1] == target) return true;
        if (target < matrix[0, 0] || matrix[m - 1, n - 1] < target) return false;

        //int rowIndex = m - 1;
        //for( ; -1 < rowIndex; rowIndex-- )
        //{
        //    var v = matrix[rowIndex, 0];
        //    if (v == target) return true;
        //    if (target < v) continue;
        //    break;
        //}

        int rowIndex = m - 1;
        if( target < matrix[m - 1, 0])
        {
            int beginIndex = 0, endIndex = m - 1;
            while( beginIndex <= endIndex)
            {
                rowIndex = ( beginIndex + endIndex ) / 2;
                if ( endIndex - beginIndex < 2 ) break;
                var v = matrix[rowIndex, 0];
                if (v == target) return true;
                if (target < v) endIndex = rowIndex;
                else beginIndex = rowIndex;
            }
        }

        int startIndex = 0, stopIndex = n - 1;

        while( startIndex <= stopIndex)
        {
            var midIndex = (startIndex + stopIndex) / 2;
            var v = matrix[rowIndex, midIndex];
            if (v == target) return true;
            if (target < v) stopIndex = midIndex - 1;
            else startIndex = midIndex + 1;
        }
        return false;
    }
}