﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给出两个图像 A 和 B ，A 和 B 为大小相同的二维正方形矩阵。（并且为二进制矩阵，只包含0和1）。

我们转换其中一个图像，向左，右，上，或下滑动任何数量的单位，并把它放在另一个图像的上面。之后，该转换的重叠是指两个图像都具有 1 的位置的数目。

（请注意，转换不包括向任何方向旋转。）

最大可能的重叠是什么？

示例 1:

输入：A = [[1,1,0],
          [0,1,0],
          [0,1,0]]
     B = [[0,0,0],
          [0,1,1],
          [0,0,1]]
输出：3
解释: 将 A 向右移动一个单位，然后向下移动一个单位。
注意: 

1 <= A.Length = A[0].Length = B.Length = B[0].Length <= 30
0 <= A[i][j], B[i][j] <= 1
*/
/// <summary>
/// https://leetcode-cn.com/problems/image-overlap/
/// 835. 图像重叠
/// 
/// </summary>
class ImageOverlapSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int LargestOverlap(int[][] A, int[][] B)
    {
        int ret = 0;
        for (int i = 0; i < A.Length; i++)
        {
            for (int j = 0; j < A.Length; j++)
            {
                int x = Check(A, B, i, j);
                int y = Check(B, A, i, j);
                ret = Math.Max(ret, Math.Max(x, y));
            }
        }
        return ret;
    }
    private static int Check(int[][] a, int[][] b, int r, int c)
    {
        int x = 0;
        for (int i1 = r, i2 = 0; i1 < a.Length && i2 < b.Length; i1++, i2++)
        {
            for (int j1 = c, j2 = 0; j1 < a.Length && i2 < b.Length; j1++, j2++)
            {
                x += (a[i1][j1] & b[i2][j2]);
            }
        }
        return x;
    }
}