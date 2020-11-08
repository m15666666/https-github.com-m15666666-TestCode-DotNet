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
二维区域和检索 - 矩阵不可变
力扣 (LeetCode)
发布于 2019-06-20
10.3k
方法一：暴力法[超出时间限制]
算法：
每次调用 sumregion 时，我们都使用两重循环来求和 (row1，col1)\rightarrow(row2，col2)(row1，col1)→(row2，col2) 中的所有元素。


private int[][] data;

public NumMatrix(int[][] matrix) {
    data = matrix;
}

public int sumRegion(int row1, int col1, int row2, int col2) {
    int sum = 0;
    for (int r = row1; r <= row2; r++) {
        for (int c = col1; c <= col2; c++) {
            sum += data[r][c];
        }
    }
    return sum;
}
复杂度分析

时间复杂度：每次查询的时间 O(mn)O(mn)。假设 mm 和 nn 分别代表行数和列数，每个 sumregion 查询最多需要访问 m \times nm×n 元素。
空间复杂度：O(1)O(1)，数据是对矩阵的引用，不是它的副本。
方法二：缓存 [超过内存限制]
因为 sumregion 可以多次调用，所以我们肯定需要做一些预处理。

算法：
我们可以通过预先计算所有可能的矩形区域和并将其存储在哈希表中，从而在额外的空间中换取速度。每个 sumregion 查询现在只需要恒定的时间复杂度。
复杂度分析

时间复杂度：每次查询的时间 O(1)O(1)。O(m^2n^2)O(m 
2
 n 
2
 ) 的时间预计算。每个 sumregion 查询需要 O(1)O(1) 时间，因为哈希表查找的时间复杂度是恒定的。预计算将花费 O(m^2n^2)O(m 
2
 n 
2
 ) 时间，因为总共需要缓存 M^2 \times n^2M 
2
 ×n 
2
  的可能值。
空间复杂度：O(m^2n^2)O(m 
2
 n 
2
 )。由于矩形区域的左上方和右下方的可能性各不相同，因此所需的额外空间为 O(m^2n^2)O(m 
2
 n 
2
 )。
方法三：缓存行
还记得我们使用累积和数组的一维版本吗？我们可以直接用它来解这个二维版本吗？

算法：
尝试将二维矩阵视为一维数组的 mm 行。为了找到区域和，我们只需要在区域中逐行累积和。


private int[][] dp;

public NumMatrix(int[][] matrix) {
    if (matrix.length == 0 || matrix[0].length == 0) return;
    dp = new int[matrix.length][matrix[0].length + 1];
    for (int r = 0; r < matrix.length; r++) {
        for (int c = 0; c < matrix[0].length; c++) {
            dp[r][c + 1] = dp[r][c] + matrix[r][c];
        }
    }
}

public int sumRegion(int row1, int col1, int row2, int col2) {
    int sum = 0;
    for (int row = row1; row <= row2; row++) {
        sum += dp[row][col2 + 1] - dp[row][col1];
    }
    return sum;
}
复杂度分析

时间复杂度：每次查询的时间 O(m)O(m)。O(mn)O(mn) 时间预计算。构造函数中的预计算需要 O(mn)O(mn) 时间。sumregion 查询需要 O(m)O(m) 时间。
空间复杂度：O(mn)O(mn)。该算法使用 O(mn)O(mn) 空间存储所有行的累积和。
方法四：智能缓存
算法：
我们在一维版本中使用了累积和数组。我们注意到累积和是根据索引 0 处的原点计算的。将这个类比扩展到二维情况，我们可以预先计算出一个与原点相关的累积区域和，即 (0,0)(0,0)。

image.png

Sum(OD)是相对于原点(0,0)的累计区域和。
如何使用预先计算的累积区域和得出 Sum(ABCD)Sum(ABCD) 呢？

image.png

Sum(OB)是矩形顶部的累积区域和。

image.png

Sum(OC)是矩形左侧的累积区域和。

image.png

Sum(OA) 是矩形左上角的累积区域和。
区域 Sum(OA)Sum(OA) 由 Sum(OB)Sum(OB) 和 Sum(OC)Sum(OC)两次覆盖。我们可以使用包含排除原则计算 Sum(ABCD)Sum(ABCD) 如下：

sum(abcd)=sum(od)-sum(ob)-sum(oc)+sum(oa)
sum(abcd)=sum(od)−sum(ob)−sum(oc)+sum(oa)


private int[][] dp;

public NumMatrix(int[][] matrix) {
    if (matrix.length == 0 || matrix[0].length == 0) return;
    dp = new int[matrix.length + 1][matrix[0].length + 1];
    for (int r = 0; r < matrix.length; r++) {
        for (int c = 0; c < matrix[0].length; c++) {
            dp[r + 1][c + 1] = dp[r + 1][c] + dp[r][c + 1] + matrix[r][c] - dp[r][c];
        }
    }
}

public int sumRegion(int row1, int col1, int row2, int col2) {
    return dp[row2 + 1][col2 + 1] - dp[row1][col2 + 1] - dp[row2 + 1][col1] + dp[row1][col1];
}
复杂度分析

时间复杂度：每次查询时间 O(1)O(1)，O(mn)O(mn) 的时间预计算。构造函数中的预计算需要 O(mn)O(mn) 时间。每个 sumregion 查询需要 O(1)O(1) 时间 。
空间复杂度：O(mn)O(mn)，该算法使用 O(mn)O(mn) 空间存储累积区域和。

*/
