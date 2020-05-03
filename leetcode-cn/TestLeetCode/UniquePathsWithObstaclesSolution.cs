using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
一个机器人位于一个 m x n 网格的左上角 （起始点在下图中标记为“Start” ）。

机器人每次只能向下或者向右移动一步。机器人试图达到网格的右下角（在下图中标记为“Finish”）。

现在考虑网格中有障碍物。那么从左上角到右下角将会有多少条不同的路径？



网格中的障碍物和空位置分别用 1 和 0 来表示。

说明：m 和 n 的值均不超过 100。

示例 1:

输入:
[
  [0,0,0],
  [0,1,0],
  [0,0,0]
]
输出: 2
解释:
3x3 网格的正中间有一个障碍物。
从左上角到右下角一共有 2 条不同的路径：
1. 向右 -> 向右 -> 向下 -> 向下
2. 向下 -> 向下 -> 向右 -> 向右
*/
/// <summary>
/// https://leetcode-cn.com/problems/unique-paths-ii/
/// 63.不同路径II
/// 
/// </summary>
class UniquePathsWithObstaclesSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int UniquePathsWithObstacles(int[][] obstacleGrid)
    {
        const int Obstacle = 1;

        if ( obstacleGrid == null ) return 0;
        var m = obstacleGrid.Length;
        if ( m == 0 ) return 0;
        var n = obstacleGrid[0].Length;
        if ( n == 0 ) return 0;
        if ( obstacleGrid[0][0] == Obstacle ) return 0;
        if (obstacleGrid[m-1][n-1] == Obstacle) return 0;

        if( m == 1)
        {
            for (int i = 0; i < n; i++)
                if (obstacleGrid[0][i] == Obstacle) return 0;
            return 1;
        }
        if ( n == 1)
        {
            for (int i = 0; i < m; i++)
                if (obstacleGrid[i][0] == Obstacle) return 0;
            return 1;
        }

        obstacleGrid[m - 2][n - 1] = obstacleGrid[m - 2][n - 1] == Obstacle ? 0 : 1;
        for( int r = m - 3; -1 < r; r--)
            obstacleGrid[r][n - 1] = obstacleGrid[r][n - 1] == Obstacle ? 0 : obstacleGrid[r + 1][n - 1];

        obstacleGrid[m - 1][n - 2] = obstacleGrid[m - 1][n - 2] == Obstacle ? 0 : 1;
        for( int c = n - 3; -1 < c; c--)
            obstacleGrid[m - 1][c] = obstacleGrid[m - 1][c] == Obstacle ? 0 : obstacleGrid[m-1][c+1];

        for (int c = n - 2; -1 < c; c--)
            for (int r = m - 2; -1 < r; r--)
                obstacleGrid[r][c] = obstacleGrid[r][c] == Obstacle ? 0 : obstacleGrid[r+1][c]+obstacleGrid[r][c+1];

        return obstacleGrid[0][0];
    }


    //public int UniquePathsWithObstacles(int[,] obstacleGrid)
    //{
    //    const int Obstacle = 1;

    //    if ( obstacleGrid == null ) return 0;

    //    var m = obstacleGrid.GetLength(0);
    //    var n = obstacleGrid.GetLength(1);
    //    if ( m < 1 || n < 1 ) return 0;
    //    if( m == 1)
    //    {
    //        for (int i = 0; i < n; i++)
    //            if (obstacleGrid[0, i] == Obstacle) return 0;
    //        return 1;
    //    }
    //    else if ( n == 1)
    //    {
    //        for (int i = 0; i < m; i++)
    //            if (obstacleGrid[i, 0] == Obstacle) return 0;
    //        return 1;
    //    }
    //    if (obstacleGrid[m-1, n-1] == Obstacle) return 0;

    //    int[] vertical = new int[m - 1];

    //    // 最下面一行，和最右面一列的路径都是1.
    //    vertical[0] = obstacleGrid[m - 2, n - 1] == Obstacle ? 0 : 1;
    //    for (int i = 1; i < vertical.Length; i++)
    //    {
    //        vertical[i] = obstacleGrid[m - 2 - i, n - 1] == Obstacle ? 0 : vertical[i - 1];
    //    }

    //    int firstRowValue = 1;
    //    for (int c = 0; c < n - 1; c++)
    //    {
    //        firstRowValue = obstacleGrid[m - 1, n - 2 - c] == Obstacle ? 0 : firstRowValue;

    //        int horizontalValue = firstRowValue;
    //        for (int i = 0; i < vertical.Length; i++)
    //        {
    //            if (obstacleGrid[m - 2 - i, n - 2 - c] == Obstacle) horizontalValue = 0;
    //            else horizontalValue += vertical[i];

    //            vertical[i] = horizontalValue;
    //        }
    //    }

    //    return vertical[vertical.Length - 1];
    //}
}
/*

不同路径 II
力扣 (LeetCode)
发布于 1 年前
30.4k
方法 1：动态规划
直觉

机器人只可以向下和向右移动，因此第一行的格子只能从左边的格子移动到，第一列的格子只能从上方的格子移动到。



对于剩下的格子，可以从左边或者上方的格子移动到。



如果格子上有障碍，那么我们不考虑包含这个格子的任何路径。我们从左至右、从上至下的遍历整个数组，那么在到达某个顶点之前我们就已经获得了到达前驱节点的方案数，这就变成了一个动态规划问题。我们只需要一个 obstacleGrid 数组作为 DP 数组。

注意： 根据题目描述，包含障碍物的格点有权值 1，我们依此来判断是否包含在路径中，然后我们可以用这个空间来存储到达这个格点的方案数。

算法

如果第一个格点 obstacleGrid[0,0] 是 1，说明有障碍物，那么机器人不能做任何移动，我们返回结果 0。
否则，如果 obstacleGrid[0,0] 是 0，我们初始化这个值为 1 然后继续算法。
遍历第一行，如果有一个格点初始值为 1 ，说明当前节点有障碍物，没有路径可以通过，设值为 0 ；否则设这个值是前一个节点的值 obstacleGrid[i,j] = obstacleGrid[i,j-1]。
遍历第一列，如果有一个格点初始值为 1 ，说明当前节点有障碍物，没有路径可以通过，设值为 0 ；否则设这个值是前一个节点的值 obstacleGrid[i,j] = obstacleGrid[i-1,j]。
现在，从 obstacleGrid[1,1] 开始遍历整个数组，如果某个格点初始不包含任何障碍物，就把值赋为上方和左侧两个格点方案数之和 obstacleGrid[i,j] = obstacleGrid[i-1,j] + obstacleGrid[i,j-1]。
如果这个点有障碍物，设值为 0 ，这可以保证不会对后面的路径产生贡献。


class Solution {
    public int uniquePathsWithObstacles(int[][] obstacleGrid) {

        int R = obstacleGrid.length;
        int C = obstacleGrid[0].length;

        // If the starting cell has an obstacle, then simply return as there would be
        // no paths to the destination.
        if (obstacleGrid[0][0] == 1) {
            return 0;
        }

        // Number of ways of reaching the starting cell = 1.
        obstacleGrid[0][0] = 1;

        // Filling the values for the first column
        for (int i = 1; i < R; i++) {
            obstacleGrid[i][0] = (obstacleGrid[i][0] == 0 && obstacleGrid[i - 1][0] == 1) ? 1 : 0;
        }

        // Filling the values for the first row
        for (int i = 1; i < C; i++) {
            obstacleGrid[0][i] = (obstacleGrid[0][i] == 0 && obstacleGrid[0][i - 1] == 1) ? 1 : 0;
        }

        // Starting from cell(1,1) fill up the values
        // No. of ways of reaching cell[i][j] = cell[i - 1][j] + cell[i][j - 1]
        // i.e. From above and left.
        for (int i = 1; i < R; i++) {
            for (int j = 1; j < C; j++) {
                if (obstacleGrid[i][j] == 0) {
                    obstacleGrid[i][j] = obstacleGrid[i - 1][j] + obstacleGrid[i][j - 1];
                } else {
                    obstacleGrid[i][j] = 0;
                }
            }
        }

        // Return value stored in rightmost bottommost cell. That is the destination.
        return obstacleGrid[R - 1][C - 1];
    }
}
复杂度分析

时间复杂度 ： O(M \times N)O(M×N) 。长方形网格的大小是 M \times NM×N，而访问每个格点恰好一次。
空间复杂度 ： O(1)O(1)。我们利用 obstacleGrid 作为 DP 数组，因此不需要额外的空间。
下一篇：动态规划 优化为一维 双百 还蛮好理解

public class Solution {
    public int UniquePathsWithObstacles(int[][] arr) {
        int m = arr.Length, n = arr[0].Length;
        int[,] dp = new int[m + 1, n + 1];
        dp[0, 1] = 1;
        for (int i = 0; i < m; i++)
            for (int j = 0; j < n; j++)
                dp[i + 1, j + 1] = arr[i][j] == 1 ? 0 : dp[i, j + 1] + dp[i + 1, j];
        return dp[m, n];
    }
}
public class Solution {
    public int UniquePathsWithObstacles(int[][] obstacleGrid) {
        
        int m = obstacleGrid.Length;
        int n = obstacleGrid[0].Length;

        // 右下角被遮挡
        if(obstacleGrid[m-1][n-1] == 1) return 0; 

        int[,] arr = new int[m,n];

        bool block = false;
        for(int i=0;i<m;++i) 
        {
            if(obstacleGrid[i][0] == 1) block = true;
            arr[i,0] = block ? 0 : 1; 
        }
        block = false;
        for(int i=0;i<n;++i)
        {
            if(obstacleGrid[0][i] == 1) block = true;
            arr[0,i] = block ? 0 : 1;
        }

        for(int i = 1 ; i < m; ++i)
        {
            for(int j = 1; j < n; ++j)
            {
                int up = obstacleGrid[i-1][j] == 1 ? 0 : arr[i-1,j];
                int left = obstacleGrid[i][j-1] == 1 ? 0 : arr[i,j-1];

                arr[i,j] = up + left;
            }
        }

        return arr[m-1,n-1];
    }
}
*/