using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// https://leetcode-cn.com/problems/unique-paths-ii/
/// 63.不同路径II
/// 一个机器人位于一个 m x n 网格的左上角 （起始点在下图中标记为“Start” ）。
/// 机器人每次只能向下或者向右移动一步。机器人试图达到网格的右下角（在下图中标记为“Finish”）。
/// 现在考虑网格中有障碍物。那么从左上角到右下角将会有多少条不同的路径？
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


    public int UniquePathsWithObstacles(int[,] obstacleGrid)
    {
        const int Obstacle = 1;

        if ( obstacleGrid == null ) return 0;

        var m = obstacleGrid.GetLength(0);
        var n = obstacleGrid.GetLength(1);
        if ( m < 1 || n < 1 ) return 0;
        if( m == 1)
        {
            for (int i = 0; i < n; i++)
                if (obstacleGrid[0, i] == Obstacle) return 0;
            return 1;
        }
        else if ( n == 1)
        {
            for (int i = 0; i < m; i++)
                if (obstacleGrid[i, 0] == Obstacle) return 0;
            return 1;
        }
        if (obstacleGrid[m-1, n-1] == Obstacle) return 0;

        int[] vertical = new int[m - 1];

        // 最下面一行，和最右面一列的路径都是1.
        vertical[0] = obstacleGrid[m - 2, n - 1] == Obstacle ? 0 : 1;
        for (int i = 1; i < vertical.Length; i++)
        {
            vertical[i] = obstacleGrid[m - 2 - i, n - 1] == Obstacle ? 0 : vertical[i - 1];
        }

        int firstRowValue = 1;
        for (int c = 0; c < n - 1; c++)
        {
            firstRowValue = obstacleGrid[m - 1, n - 2 - c] == Obstacle ? 0 : firstRowValue;

            int horizontalValue = firstRowValue;
            for (int i = 0; i < vertical.Length; i++)
            {
                if (obstacleGrid[m - 2 - i, n - 2 - c] == Obstacle) horizontalValue = 0;
                else horizontalValue += vertical[i];

                vertical[i] = horizontalValue;
            }
        }

        return vertical[vertical.Length - 1];
    }
}