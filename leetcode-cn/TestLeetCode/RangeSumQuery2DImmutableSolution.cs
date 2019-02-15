using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
给定一个二维矩阵，计算其子矩形范围内元素的总和，该子矩阵的左上角为 (row1, col1) ，右下角为 (row2, col2)。

Range Sum Query 2D
上图子矩阵左上角 (row1, col1) = (2, 1) ，右下角(row2, col2) = (4, 3)，该子矩形内元素的总和为 8。

示例:

给定 matrix = [
  [3, 0, 1, 4, 2],
  [5, 6, 3, 2, 1],
  [1, 2, 0, 1, 5],
  [4, 1, 0, 1, 7],
  [1, 0, 3, 0, 5]
]

sumRegion(2, 1, 4, 3) -> 8
sumRegion(1, 1, 2, 2) -> 11
sumRegion(1, 2, 2, 4) -> 12
说明:

你可以假设矩阵不可变。
会多次调用 sumRegion 方法。
你可以假设 row1 ≤ row2 且 col1 ≤ col2。 
*/
/// <summary>
/// https://leetcode-cn.com/problems/range-sum-query-2d-immutable/
/// 304. 二维区域和检索 - 矩阵不可变
/// </summary>
/// https://www.bbsmax.com/A/obzb996dED/
/// https://blog.csdn.net/wudi_X/article/details/81905574
/// https://www.bbsmax.com/A/RnJWrLlEzq/
class RangeSumQuery2DImmutableSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    /**
     * Your NumMatrix object will be instantiated and called as such:
     * NumMatrix obj = new NumMatrix(matrix);
     * int param_1 = obj.SumRegion(row1,col1,row2,col2);
     */
    
    public RangeSumQuery2DImmutableSolution(int[,] matrix)
    {
        if (matrix == null) matrix = new int[0, 0];

        m = matrix.GetLength(0);
        n = matrix.GetLength(1);
        sum = new int[m, n];

        if (m == 0 || n == 0) return;

        //边界
        sum[0,0] = matrix[0,0];
        for (int i = 1; i < m; i++) sum[i,0] = matrix[i,0] + sum[i - 1,0];
        for (int j = 1; j < n; j++) sum[0,j] = matrix[0,j] + sum[0,j - 1];

        //计算sum
        for (int i = 1; i < m; i++)
            for (int j = 1; j < n; j++)
                sum[i,j] = matrix[i,j] + sum[i - 1,j] + sum[i,j - 1] - sum[i - 1,j - 1];
    }

    private int[,] sum;
    private int m;
    private int n;
    public int SumRegion(int row1, int col1, int row2, int col2)
    {
        if (m == 0 || n == 0) return 0;

        if (row1 == 0 && col1 == 0)
            return sum[row2,col2];
        else if (row1 == 0 && col1 != 0)
            return sum[row2,col2] - sum[row2,col1 - 1];
        else if (row1 != 0 && col1 == 0)
            return sum[row2,col2] - sum[row1 - 1,col2];

        return sum[row2,col2] - sum[row1 - 1,col2] - sum[row2,col1 - 1] + sum[row1 - 1,col1 - 1];
    }
}
/*
//别人的算法
public class NumMatrix {
	private long[,] dp = null;
		
	public NumMatrix(int[,] matrix) {
		int row = matrix.GetLength(0);
		int col = matrix.GetLength(1);
			
		if (row==0 || col==0) return;
            
		dp = new long[row+1, col+1];
		for (int i=1; i<row+1; ++i)
		{
			for (int j=1; j<col+1; ++j)
			{
				dp[i,j] = dp[i-1,j] + dp[i,j-1] - dp[i-1,j-1] + matrix[i-1,j-1];
			}
		}
	}
	    
	public int SumRegion(int row1, int col1, int row2, int col2) {
		return (int)(dp[row2+1,col2+1] - dp[row2+1,col1] - dp[row1,col2+1] + dp[row1,col1]);
	}
}
public class NumMatrix
{
    private int[,] _ma;
    public NumMatrix(int[,] matrix)
    {
        _ma = new int[matrix.GetLength(0), matrix.GetLength(1)];
        for (var i = 0; i < matrix.GetLength(0); i++)
        {
            for (var j = 0; j < matrix.GetLength(1); j++)
            {
                if (i == 0)
                {
                    if (j == 0)
                        _ma[i, j] = matrix[i, j];
                    else
                        _ma[i, j] = _ma[i, j - 1] + matrix[i, j];
                }
                else if (j == 0)
                {
                    _ma[i, j] = _ma[i - 1, j] + matrix[i, j];
                }
                else
                {
                    _ma[i, j] = _ma[i - 1, j] + _ma[i, j - 1] - _ma[i - 1, j - 1] + matrix[i, j];
                }
            }
        }
    }

    public int SumRegion(int row1, int col1, int row2, int col2)
    {
        if (row1 == 0 && col1 == 0)
        {
            return _ma[row2, col2];
        }
        else if (row1 == 0)
        {
            return _ma[row2, col2] - _ma[row2, col1-1];
        }
        else if (col1 == 0)
        {
            return _ma[row2, col2] - _ma[row1-1, col2];
        }
        else
        {
            var cut = _ma[row2, col2] - _ma[row1-1, col1-1];
            return cut - SumRegion(0, col1, row1-1, col2) - SumRegion(row1, 0, row2, col1-1);
        }
    }
}
*/
