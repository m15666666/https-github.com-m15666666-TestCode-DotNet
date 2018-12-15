using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// https://leetcode-cn.com/problems/number-of-islands/
/// 200. 岛屿的个数
/// 给定一个由 '1'（陆地）和 '0'（水）组成的的二维网格，计算岛屿的数量。
/// 一个岛被水包围，并且它是通过水平方向或垂直方向上相邻的陆地连接而成的。
/// 你可以假设网格的四个边均被水包围。
/// https://blog.csdn.net/lv1224/article/details/82470100
/// </summary>
class NumberOfIslandsSolution
{
    public static void Test()
    {
        char[,] grid = new char[,] { 
            { '1','0','1', '1', '1' },
            { '1','0','1', '0', '1' },
            { '1','1','1', '0', '1' }
        };

        var ret = NumIslands(grid);
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    
    private const char Island = '1';
    private const char KnownIsland = '0';
    public static int NumIslands(char[,] grid)
    {
        var matrix = grid;
        if (matrix == null || matrix.Length == 0) return 0;
        
        int ret = 0;
        int m = matrix.GetLength(0);
        int n = matrix.GetLength(1);

        Queue<Coordinate2D> queue = new Queue<Coordinate2D>();
        HashSet<long> visited = new HashSet<long>();

        for ( int row = m - 1;  -1 < row; row--)
            for( int column = 0; column < n; column++)
            {
                long key = row * n + column;
                if (visited.Contains(key)) continue;
                visited.Add(key);

                var v = matrix[row, column];
                if (v == '0') continue;

                ret++;
                queue.Enqueue(new Coordinate2D { X = row, Y = column });

                while(0 < queue.Count)
                {
                    var co = queue.Dequeue();
                    var r = co.X;
                    var c = co.Y;
                    matrix[r, c] = KnownIsland;
                    long rn = r * n;
                    long key2 = rn + (c - 1);
                    if (-1 < c - 1 && !visited.Contains(key2))
                    {
                        visited.Add(key2);
                                
                        if (matrix[r, c - 1] == Island)
                            queue.Enqueue(new Coordinate2D { X = r, Y = c - 1 });
                    }

                    key2 = rn + (c + 1);
                    if (c + 1 < n && !visited.Contains(key2))
                    {
                        visited.Add(key2);

                        if (matrix[r, c + 1] == Island)
                            queue.Enqueue(new Coordinate2D { X = r, Y = c + 1 });
                    }

                    key2 = rn - n + c;
                    if (-1 < r - 1 && !visited.Contains(key2))
                    {
                        visited.Add(key2);

                        if (matrix[r - 1, c] == Island)
                            queue.Enqueue(new Coordinate2D { X = r - 1, Y = c });
                    }

                    key2 = rn + n + c;
                    if (r + 1 < m && !visited.Contains(key2))
                    {
                        visited.Add(key2);

                        if (matrix[r + 1, c] == Island)
                            queue.Enqueue(new Coordinate2D { X = r + 1, Y = c });
                    }
                }
            }
        return ret;
    }
}

public struct Coordinate2D
{
    public int X;
    public int Y;
}

/*
// 别人的解法
    public int NumIslands(char[,] grid)
    {
        int num = 0;

        int raw = grid.GetLength(0);
        int bound = grid.GetLength(1);

        bool[,] vistedGrid = new bool[raw, bound];

        for (int i = 0; i < raw; i++)
        {
            for (int j = 0; j < bound; j++)
            {
                if(grid[i,j]=='1' && vistedGrid[i, j] == false)
                {
                    VistiedPoint(grid, vistedGrid, i, j);
                    num += 1;
                }
            }
        }

        return num;
    }
    
    private void VistiedPoint(char[,] grid, bool[,] vistedGrid, int x, int y)
    {
        if (x < 0 || x >= grid.GetLength(0)) return;
        if (y < 0 || y >= grid.GetLength(1)) return;
        if (grid[x, y] == '0' || vistedGrid[x, y]) return;

        vistedGrid[x, y] = true;
        VistiedPoint(grid, vistedGrid, x - 1, y);
        VistiedPoint(grid, vistedGrid, x + 1, y);
        VistiedPoint(grid, vistedGrid, x, y - 1);
        VistiedPoint(grid, vistedGrid, x, y + 1);
    }
*/
