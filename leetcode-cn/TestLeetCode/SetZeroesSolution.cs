using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// https://leetcode-cn.com/problems/set-matrix-zeroes/
/// 73.矩阵置零
/// 给定一个 m x n 的矩阵，如果一个元素为 0，则将其所在行和列的所有元素都设为 0。请使用原地算法。
/// https://www.cnblogs.com/ariel-dreamland/p/9154179.html
/// </summary>
class SetZeroesSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public void SetZeroes(int[,] matrix)
    {
        if (matrix == null) return;

        int m = matrix.GetLength(0);
        int n = matrix.GetLength(1);

        if (m < 1 || n < 1) return;

        bool zeroFirstRow = false;
        for ( int i = 0; i < n; i++ )
            if(matrix[0,i] == 0)
            {
                zeroFirstRow = true;
                break;
            }

        bool zeroFirstColumn = false;
        for (int i = 0; i < m; i++)
            if (matrix[i, 0] == 0)
            {
                zeroFirstColumn = true;
                break;
            }

        for( var i = 1; i < m; i++)
            for(var j = 1; j < n; j++) 
                if( matrix[i, j] == 0 )
                {
                    matrix[0, j] = 0;
                    matrix[i, 0] = 0;
                }
        for (var i = 1; i < m; i++)
            for (var j = 1; j < n; j++)
                if (matrix[0, j] == 0 || matrix[i, 0] == 0)
                {
                    matrix[i, j] = 0;
                }

        if (zeroFirstRow)
            for (int i = 0; i < n; i++)
                matrix[0, i] = 0;

        if (zeroFirstColumn)
            for (int i = 0; i < m; i++)
                matrix[i, 0] = 0;
    }

}