using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个含有 M x N 个元素的矩阵（M 行，N 列），请以对角线遍历的顺序返回这个矩阵中的所有元素，对角线遍历如下图所示。

 

示例:

输入:
[
 [ 1, 2, 3 ],
 [ 4, 5, 6 ],
 [ 7, 8, 9 ]
]

输出:  [1,2,4,7,5,3,6,8,9]

解释:

 

说明:

给定矩阵中的元素总数不会超过 100000 。 
*/
/// <summary>
/// https://leetcode-cn.com/problems/diagonal-traverse/
/// 498. 对角线遍历
/// </summary>
class DiagonalTraverseSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int[] FindDiagonalOrder(int[][] matrix)
    {
        if (matrix == null || matrix.Length == 0 || matrix[0].Length == 0) return new int[0];

        int m = matrix.Length;
        int n = matrix[0].Length;
        int count = m * n;
        int index = 0;
        int[] ret = new int[count];

        if( m == 1 || n == 1)
        {
            for (int i = 0; i < m; i++)
                for (int j = 0; j < n; j++)
                    ret[index++] = matrix[i][j];
            return ret;
        }

        int bottom = m - 1;
        int right = n - 1;
        

        bool isUpDirection = true;
        int row = 0;
        int column = 0;

        do
        {
            ret[index++] = matrix[row][column];
            if (isUpDirection)
            {
                if( row == 0 || column == right )
                {
                    isUpDirection = !isUpDirection;
                    if (column < right) column++;
                    else if (row < bottom) row++;
                    else break;
                }
                else
                {
                    row--;
                    column++;
                }
            }
            else
            {
                if (row == bottom || column == 0)
                {
                    isUpDirection = !isUpDirection;
                    if (row < bottom) row++;
                    else if (column < right) column++;
                    else break;
                }
                else
                {
                    row++;
                    column--;
                }
            }
        } while (true);
        return ret;
    }
}