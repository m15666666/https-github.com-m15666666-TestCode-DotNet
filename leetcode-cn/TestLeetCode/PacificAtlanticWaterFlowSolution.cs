using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个 m x n 的非负整数矩阵来表示一片大陆上各个单元格的高度。“太平洋”处于大陆的左边界和上边界，而“大西洋”处于大陆的右边界和下边界。

规定水流只能按照上、下、左、右四个方向流动，且只能从高到低或者在同等高度上流动。

请找出那些水流既可以流动到“太平洋”，又能流动到“大西洋”的陆地单元的坐标。

 

提示：

输出坐标的顺序不重要
m 和 n 都小于150
 

示例：

 

给定下面的 5x5 矩阵:

  太平洋 ~   ~   ~   ~   ~ 
       ~  1   2   2   3  (5) *
       ~  3   2   3  (4) (4) *
       ~  2   4  (5)  3   1  *
       ~ (6) (7)  1   4   5  *
       ~ (5)  1   1   2   4  *
          *   *   *   *   * 大西洋

返回:

[[0, 4], [1, 3], [1, 4], [2, 2], [3, 0], [3, 1], [4, 0]] (上图中带括号的单元). 
*/
/// <summary>
/// https://leetcode-cn.com/problems/pacific-atlantic-water-flow/
/// 417. 太平洋大西洋水流问题
/// </summary>
class PacificAtlanticWaterFlowSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public IList<IList<int>> PacificAtlantic(int[][] matrix)
    {
        if (matrix == null || matrix.Length == 0 || matrix[0].Length == 0) return new List<IList<int>>();

        int[,] dires = { { 1, 0 }, { -1, 0 }, { 0, 1 }, { 0, -1 } };

        List<IList<int>> ret = new List<IList<int>>();
        int m = matrix.Length;
        int n = matrix[0].Length;
        bool[,] canReachP = new bool[m,n];
        bool[,] canReachA = new bool[m,n];
        for (int i = 0; i < n; i++)
        {
            Dfs(0, i, canReachP);
            Dfs(m - 1, i, canReachA);
        }
        for (int i = 0; i < m; i++)
        {
            Dfs(i, 0, canReachP);
            Dfs(i, n - 1, canReachA);
        }
        for (int i = 0; i < m; i++)
            for (int j = 0; j < n; j++)
                if (canReachA[i,j] && canReachP[i,j]) ret.Add(new[]{ i,j});
        return ret;

        void Dfs(int x,int y, bool[,] canReach)
        {
            canReach[x,y] = true;
            for (int i = 0; i < 4; i++)
            {
                int newX = x + dires[i,0];
                int newY = y + dires[i,1];
                if (IsIn(newX, newY) && matrix[x][y] <= matrix[newX][newY] && !canReach[newX,newY]) Dfs(newX, newY, canReach);
            }
        }

        bool IsIn(int x, int y) => -1 < x && x < m && -1 < y && y < n;
    }
    //public IList<IList<int>> PacificAtlantic(int[][] matrix)
    //{
    //    if (matrix == null || matrix.Length == 0 || matrix[0].Length == 0) return new List<IList<int>>();

    //    List<IList<int>> ret = new List<IList<int>>();
    //    int m = matrix.Length;
    //    int n = matrix[0].Length;
    //    byte[,] flags = new byte[m, n];
    //    for( int i = 0; i < m; i++)
    //        for( int j = 0; j < n; j++)
    //            if (PacificAtlantic(matrix, m, n, i, j, flags) == TwoOcean) ret.Add(new[] { i, j });

    //    return ret;
    //}

    //private const byte Atlantic = 0x02;
    //private const byte Pacific = 0x01;
    ///// <summary>
    ///// 可以连通两个海洋
    ///// </summary>
    //private const byte TwoOcean = Atlantic | Pacific;

    ///// <summary>
    ///// 计算完毕的格子
    ///// </summary>
    //private const byte Calculated = 0x80;

    ///// <summary>
    ///// 计算中的格子
    ///// </summary>
    //private const byte Calculating = 0x40;

    //private static byte PacificAtlantic(int[][] matrix, int m, int n, int i, int j, byte[,] flags)
    //{
    //    byte flag = flags[i, j];
    //    if ((flag & Calculated) == Calculated) return (byte)(flag & TwoOcean);
    //    if ((flag & Calculating) == Calculating) return 0;
    //    flags[i, j] = Calculating;

    //    var v = matrix[i][j];
    //    byte ret = 0;

    //    if (i == 0) ret |= Pacific;
    //    else if( matrix[i-1][j] <= v ) ret |= PacificAtlantic(matrix, m, n, i - 1, j, flags);

    //    if(ret == TwoOcean)
    //    {
    //        flags[i, j] = (byte)(Calculated & ret);
    //        return ret;
    //    }

    //    if (j == 0) ret |= Pacific;
    //    else if(matrix[i][j-1] <= v) ret |= PacificAtlantic(matrix, m, n, i, j - 1, flags);

    //    if (ret == TwoOcean)
    //    {
    //        flags[i, j] = (byte)(Calculated & ret);
    //        return ret;
    //    }

    //    if (i == m - 1) ret |= Atlantic;
    //    else if(matrix[i+1][j] <= v) ret |= PacificAtlantic(matrix, m, n, i + 1, j, flags);

    //    if (ret == TwoOcean)
    //    {
    //        flags[i, j] = (byte)(Calculated & ret);
    //        return ret;
    //    }

    //    if (j == n - 1) ret |= Atlantic;
    //    else if (matrix[i][j+1] <= v) ret |= PacificAtlantic(matrix, m, n, i, j + 1, flags);

    //    flags[i, j] = (byte)(Calculated & ret);
    //    return ret;
    //}
}
/*
public class Solution {
    
    private static int[] dx = new int [] { -1, 0, 1, 0};
    private static int[] dy = new int [] { 0, -1, 0, 1};
    
    public IList<int[]> PacificAtlantic(int[][] matrix) {
        var res = new List<int[]>();
        if(matrix == null || matrix.Length == 0 || matrix[0].Length == 0)
            return res;
        
        var Map = new int [matrix.Length, matrix[0].Length];
        Queue<int> Points = new Queue<int>();
        for(int i = 0, M = Map.GetLength(0) - 1; i <= M; i++)
        {
            int N = Map.GetLength(1) - 1;
            int step = (i == 0 || i == M || N <= 0) ? 1 : N;
            for(int j = 0; j <= N; j += step)
            {
                int water = 0;
                if(i == 0 || j == 0)
                    water |= 1;
                if(i == M || j == N)
                    water |= 2;
                
                Points.Enqueue(i);
                Points.Enqueue(j);
                while(Points.Count > 0)
                {
                    int x = Points.Dequeue();
                    int y = Points.Dequeue();
                    if(Map[x ,y] != 3 && water != Map[x, y])
                    {
                        Map[x, y] |= water;
                        if(Map[x, y] == 3)
                            res.Add(new int [] {x , y});
                        for(int k = 0; k < 4; k++)
                        {
                            int nx = x + dx[k];
                            int ny = y + dy[k];
                            if(nx > -1 && nx <= M && ny > -1 && ny <= N && matrix[nx][ny] >= matrix[x][y])
                            {
                                Points.Enqueue(nx);
                                Points.Enqueue(ny);
                            }
                        }
                    } 
                }
            }
            
        }
        
        return res;
    }
}
public class Solution {
    public IList<int[]> PacificAtlantic(int[,] matrix)
    {
        int m = matrix.GetLength(0);
        int n = matrix.GetLength(1);
        List<int[]> results = new List<int[]>();
        bool[,] pacific = new bool[m, n];
        bool[,] atlantic = new bool[m, n];
        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                pacific[i, j] = false;
                atlantic[i, j] = false;
            }
        }

        for (int i = 0; i < m; i++)
        {
            PacificAtlanticDFS(matrix, i, 0, int.MinValue, ref pacific);
            PacificAtlanticDFS(matrix, i, n - 1, int.MinValue, ref atlantic);
        }

        for (int i = 0; i < n; i++)
        {
            PacificAtlanticDFS(matrix, 0, i, int.MinValue, ref pacific);
            PacificAtlanticDFS(matrix, m - 1, i, int.MinValue, ref atlantic);
        }

        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (pacific[i, j] && atlantic[i, j])
                {
                    results.Add(new int[] { i, j });
                }
            }
        }

        return results;
    }

    private void PacificAtlanticDFS(int[,] matrix, int i, int j, int pre, ref bool[,] visited)
    {
        if (i < 0 || j < 0 || i >= matrix.GetLength(0) || j >= matrix.GetLength(1)) return;
        if (visited[i, j] || matrix[i, j] < pre) return;

        visited[i, j] = true;
        PacificAtlanticDFS(matrix, i, j - 1, matrix[i, j], ref visited);
        PacificAtlanticDFS(matrix, i, j + 1, matrix[i, j], ref visited);
        PacificAtlanticDFS(matrix, i - 1, j, matrix[i, j], ref visited);
        PacificAtlanticDFS(matrix, i + 1, j, matrix[i, j], ref visited);
    }
}
*/
