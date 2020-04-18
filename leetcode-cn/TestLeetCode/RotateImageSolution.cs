﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// https://leetcode-cn.com/problems/rotate-image/
/// 48. 旋转图像
/// 
/// </summary>
class RotateImageSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public void Rotate(int[][] matrix) 
    {
        int n = matrix.Length;

        int tmp;
        for (int i = 0; i < n; i++) 
        {
          for (int j = i + 1; j < n; j++) 
          {
            tmp = matrix[j][i];
            matrix[j][i] = matrix[i][j];
            matrix[i][j] = tmp;
          }
        }

        int half = n / 2; 
        for (int i = 0; i < n; i++) 
        {
          for (int j = 0; j < half; j++) 
          {
            tmp = matrix[i][j];
            matrix[i][j] = matrix[i][n - j - 1];
            matrix[i][n - j - 1] = tmp;
          }
        }
    }

}
/*
旋转图像
力扣 (LeetCode)
发布于 1 年前
41.6k
方法 1 ：转置加翻转
最直接的想法是先转置矩阵，然后翻转每一行。这个简单的方法已经能达到最优的时间复杂度O(N^2)O(N 
2
 )。

class Solution {
  public void rotate(int[][] matrix) {
    int n = matrix.length;

    // transpose matrix
    for (int i = 0; i < n; i++) {
      for (int j = i; j < n; j++) {
        int tmp = matrix[j][i];
        matrix[j][i] = matrix[i][j];
        matrix[i][j] = tmp;
      }
    }
    // reverse each row
    for (int i = 0; i < n; i++) {
      for (int j = 0; j < n / 2; j++) {
        int tmp = matrix[i][j];
        matrix[i][j] = matrix[i][n - j - 1];
        matrix[i][n - j - 1] = tmp;
      }
    }
  }
}
时间复杂度：O(N^2)O(N 
2
 ).
空间复杂度：O(1)O(1) 由于旋转操作是 就地 完成的。



方法 2 ：旋转四个矩形
直观想法

方法 1 使用了两次矩阵操作，但是有只使用一次操作的方法完成旋转。

为了实现这一点，我们来研究每个元素在旋转的过程中如何移动。

48_angles.png

这提供给我们了一个思路，将给定的矩阵分成四个矩形并且将原问题划归为旋转这些矩形的问题。

48_rectangles.png

现在的解法很直接 - 可以在第一个矩形中移动元素并且在 长度为 4 个元素的临时列表中移动它们。



代码

class Solution {
  public void rotate(int[][] matrix) {
    int n = matrix.length;
    for (int i = 0; i < n / 2 + n % 2; i++) {
      for (int j = 0; j < n / 2; j++) {
        int[] tmp = new int[4];
        int row = i;
        int col = j;
        for (int k = 0; k < 4; k++) {
          tmp[k] = matrix[row][col];
          int x = row;
          row = col;
          col = n - 1 - x;
        }
        for (int k = 0; k < 4; k++) {
          matrix[row][col] = tmp[(k + 3) % 4];
          int x = row;
          row = col;
          col = n - 1 - x;
        }
      }
    }
  }
}
复杂度分析

时间复杂度：O(N^2)O(N 
2
 ) 是两重循环的复杂度。
空间复杂度：O(1)O(1) 由于我们在一次循环中的操作是 就地 完成的，并且我们只用了长度为 4 的临时列表做辅助。


方法 3：在单次循环中旋转 4 个矩形
该想法和方法 2 相同，但是所有的操作可以在单次循环内完成并且这是更精简的方法。

class Solution {
  public void rotate(int[][] matrix) {
    int n = matrix.length;
    for (int i = 0; i < (n + 1) / 2; i ++) {
      for (int j = 0; j < n / 2; j++) {
        int temp = matrix[n - 1 - j][i];
        matrix[n - 1 - j][i] = matrix[n - 1 - i][n - j - 1];
        matrix[n - 1 - i][n - j - 1] = matrix[j][n - 1 -i];
        matrix[j][n - 1 - i] = matrix[i][j];
        matrix[i][j] = temp;
      }
    }
  }
}
时间复杂度：O(N^2)O(N 
2
 ) 是两重循环的复杂度。
空间复杂度：O(1)O(1) 由于旋转操作是 就地 完成的。
下一篇：实现矩阵旋转的两种方法：自外向内 & 两次旋转

*/