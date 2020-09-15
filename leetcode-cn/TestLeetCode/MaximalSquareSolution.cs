using System;

/*
在一个由 0 和 1 组成的二维矩阵内，找到只包含 1 的最大正方形，并返回其面积。

示例:

输入:

1 0 1 0 0
1 0 1 1 1
1 1 1 1 1
1 0 0 1 0

输出: 4

*/

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
internal class MaximalSquareSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int MaximalSquare(char[][] matrix)
    {
        if (matrix == null) return 0;

        int m = matrix.Length;
        if (m == 0) return 0;
        int n = matrix[0].Length;
        if (n == 0) return 0;

        int maxSide = 0;
        const char One = '1';
        var maxSides = new int[n];
        for (int j = 0; j < n; j++)
            if(matrix[0][j] == One)
            {
                maxSide = 1;
                maxSides[j] = 1;
            }
            else maxSides[j] = 0;

        for (int i = 1; i < m; i++)
        {
            int left;
            if (matrix[i][0] == One)
            {
                if (maxSide < 1) maxSide = 1;
                left = 1;
            }
            else left = 0;

            int leftTop = maxSides[0];
            maxSides[0] = left;
            for (int j = 1; j < n; j++)
            {
                int nextLeftTop = maxSides[j];
                int dp;
                if (matrix[i][j] == One)
                {
                    dp = Math.Min(Math.Min(maxSides[j], left), leftTop) + 1;
                    if (maxSide < dp) maxSide = dp;
                }
                else dp = 0;
                maxSides[j] = dp;
                left = dp;
                leftTop = nextLeftTop;
            }
        }
        return maxSide * maxSide;
    }

    //public int MaximalSquare(char[,] matrix)
    //{
    //    const char One = '1';
    //    int m = matrix.GetLength(0);
    //    int n = matrix.GetLength(1);
    //    int[,] areas = new int[m, n];
    //    int maxLength = 0;
    //    for( int i = 0; i < m; i++)
    //        if(matrix[i,0] == One)
    //        {
    //            areas[i, 0] = 1;
    //            maxLength = 1;
    //        }else areas[i, 0] = 0;

    //    for (int i = 0; i < n; i++)
    //        if (matrix[0, i] == One)
    //        {
    //            areas[0, i] = 1;
    //            maxLength = 1;
    //        }
    //        else areas[0, i] = 0;

    //    for( int i = 1; i < m; i++)
    //        for(int j = 1; j < n; j++)
    //        {
    //            if (matrix[i, j] == One)
    //            {
    //                var newLength = Math.Min(areas[i-1,j-1], Math.Min(areas[i - 1, j], areas[i, j - 1])) + 1;
    //                areas[i, j] = newLength;
    //                if (maxLength < newLength) maxLength = newLength;
    //            }
    //            else areas[i, j] = 0;
    //        }
    //    return maxLength * maxLength;
    //}
}

/*

最大正方形
力扣官方题解
发布于 2020-05-06
45.3k
📺 视频题解

📖 文字题解
方法一：暴力法
由于正方形的面积等于边长的平方，因此要找到最大正方形的面积，首先需要找到最大正方形的边长，然后计算最大边长的平方即可。

暴力法是最简单直观的做法，具体做法如下：

遍历矩阵中的每个元素，每次遇到 11，则将该元素作为正方形的左上角；

确定正方形的左上角后，根据左上角所在的行和列计算可能的最大正方形的边长（正方形的范围不能超出矩阵的行数和列数），在该边长范围内寻找只包含 11 的最大正方形；

每次在下方新增一行以及在右方新增一列，判断新增的行和列是否满足所有元素都是 11。


class Solution {
    public int maximalSquare(char[][] matrix) {
        int maxSide = 0;
        if (matrix == null || matrix.length == 0 || matrix[0].length == 0) {
            return maxSide;
        }
        int rows = matrix.length, columns = matrix[0].length;
        for (int i = 0; i < rows; i++) {
            for (int j = 0; j < columns; j++) {
                if (matrix[i][j] == '1') {
                    // 遇到一个 1 作为正方形的左上角
                    maxSide = Math.max(maxSide, 1);
                    // 计算可能的最大正方形边长
                    int currentMaxSide = Math.min(rows - i, columns - j);
                    for (int k = 1; k < currentMaxSide; k++) {
                        // 判断新增的一行一列是否均为 1
                        boolean flag = true;
                        if (matrix[i + k][j + k] == '0') {
                            break;
                        }
                        for (int m = 0; m < k; m++) {
                            if (matrix[i + k][j + m] == '0' || matrix[i + m][j + k] == '0') {
                                flag = false;
                                break;
                            }
                        }
                        if (flag) {
                            maxSide = Math.max(maxSide, k + 1);
                        } else {
                            break;
                        }
                    }
                }
            }
        }
        int maxSquare = maxSide * maxSide;
        return maxSquare;
    }
}
复杂度分析

时间复杂度：O(mn \min(m,n)^2)O(mnmin(m,n) 
2
 )，其中 mm 和 nn 是矩阵的行数和列数。

需要遍历整个矩阵寻找每个 11，遍历矩阵的时间复杂度是 O(mn)O(mn)。
对于每个可能的正方形，其边长不超过 mm 和 nn 中的最小值，需要遍历该正方形中的每个元素判断是不是只包含 11，遍历正方形时间复杂度是 O(\min(m,n)^2)O(min(m,n) 
2
 )。
总时间复杂度是 O(mn \min(m,n)^2)O(mnmin(m,n) 
2
 )。
空间复杂度：O(1)O(1)。额外使用的空间复杂度为常数。

方法二：动态规划
方法一虽然直观，但是时间复杂度太高，有没有办法降低时间复杂度呢？

可以使用动态规划降低时间复杂度。我们用 dp(i, j)dp(i,j) 表示以 (i, j)(i,j) 为右下角，且只包含 11 的正方形的边长最大值。如果我们能计算出所有 dp(i, j)dp(i,j) 的值，那么其中的最大值即为矩阵中只包含 11 的正方形的边长最大值，其平方即为最大正方形的面积。

那么如何计算 dpdp 中的每个元素值呢？对于每个位置 (i, j)(i,j)，检查在矩阵中该位置的值：

如果该位置的值是 00，则 dp(i, j) = 0dp(i,j)=0，因为当前位置不可能在由 11 组成的正方形中；

如果该位置的值是 11，则 dp(i, j)dp(i,j) 的值由其上方、左方和左上方的三个相邻位置的 dpdp 值决定。具体而言，当前位置的元素值等于三个相邻位置的元素中的最小值加 11，状态转移方程如下：

dp(i, j)=min(dp(i−1, j), dp(i−1, j−1), dp(i, j−1))+1
dp(i,j)=min(dp(i−1,j),dp(i−1,j−1),dp(i,j−1))+1

如果读者对这个状态转移方程感到不解，可以参考 1277. 统计全为 1 的正方形子矩阵的官方题解，其中给出了详细的证明。

此外，还需要考虑边界条件。如果 ii 和 jj 中至少有一个为 00，则以位置 (i, j)(i,j) 为右下角的最大正方形的边长只能是 11，因此 dp(i, j) = 1dp(i,j)=1。

以下用一个例子具体说明。原始矩阵如下。


0 1 1 1 0
1 1 1 1 0
0 1 1 1 1
0 1 1 1 1
0 0 1 1 1
对应的 dpdp 值如下。


0 1 1 1 0
1 1 2 2 0
0 1 2 3 1
0 1 2 3 2
0 0 1 2 3
下图也给出了计算 dpdp 值的过程。

fig1


class Solution {
    public int maximalSquare(char[][] matrix) {
        int maxSide = 0;
        if (matrix == null || matrix.length == 0 || matrix[0].length == 0) {
            return maxSide;
        }
        int rows = matrix.length, columns = matrix[0].length;
        int[][] dp = new int[rows][columns];
        for (int i = 0; i < rows; i++) {
            for (int j = 0; j < columns; j++) {
                if (matrix[i][j] == '1') {
                    if (i == 0 || j == 0) {
                        dp[i][j] = 1;
                    } else {
                        dp[i][j] = Math.min(Math.min(dp[i - 1][j], dp[i][j - 1]), dp[i - 1][j - 1]) + 1;
                    }
                    maxSide = Math.max(maxSide, dp[i][j]);
                }
            }
        }
        int maxSquare = maxSide * maxSide;
        return maxSquare;
    }
}
复杂度分析

时间复杂度：O(mn)O(mn)，其中 mm 和 nn 是矩阵的行数和列数。需要遍历原始矩阵中的每个元素计算 dp 的值。

空间复杂度：O(mn)O(mn)，其中 mm 和 nn 是矩阵的行数和列数。创建了一个和原始矩阵大小相同的矩阵 dp。由于状态转移方程中的 dp(i, j)dp(i,j) 由其上方、左方和左上方的三个相邻位置的 dpdp 值决定，因此可以使用两个一维数组进行状态转移，空间复杂度优化至 O(n)O(n)。

public class Solution {
    public int MaximalSquare(char[][] matrix) {
        if (matrix.Length == 0) return 0;
        int[,] dp = new int[matrix.Length, matrix[0].Length];
        int max = 0;
        for (int row = 0; row < matrix.Length; row++)
        {
            for (int col = 0; col < matrix[0].Length; col++)
            {
                if (row == 0 || col == 0) dp[row, col] = matrix[row][col] == '1' ? 1 : 0;
                else
                {
                    if (matrix[row][col] == '0') dp[row, col] = 0;
                    else
                    {
                        dp[row, col] = Math.Min(dp[row - 1, col], dp[row, col - 1]);
                        dp[row, col] = Math.Min(dp[row - 1, col - 1], dp[row, col]) + 1;
                    }
                }
                if (dp[row, col] > max) max = dp[row, col];
            }
        }
        return max * max;
    }
}

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