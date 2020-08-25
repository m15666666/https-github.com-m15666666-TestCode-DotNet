using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给你一个由 '1'（陆地）和 '0'（水）组成的的二维网格，请你计算网格中岛屿的数量。

岛屿总是被水包围，并且每座岛屿只能由水平方向或竖直方向上相邻的陆地连接形成。

此外，你可以假设该网格的四条边均被水包围。

 

示例 1:

输入:
[
['1','1','1','1','0'],
['1','1','0','1','0'],
['1','1','0','0','0'],
['0','0','0','0','0']
]
输出: 1
示例 2:

输入:
[
['1','1','0','0','0'],
['1','1','0','0','0'],
['0','0','1','0','0'],
['0','0','0','1','1']
]
输出: 3
解释: 每座岛屿只能由水平和/或竖直方向上相邻的陆地连接而成。

 
*/
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
岛屿数量
力扣 (LeetCode)
发布于 2019-07-08
108.0k
📺 视频题解

📖 文字题解
方法一：深度优先搜索
我们可以将二维网格看成一个无向图，竖直或水平相邻的 11 之间有边相连。

为了求出岛屿的数量，我们可以扫描整个二维网格。如果一个位置为 11，则以其为起始节点开始进行深度优先搜索。在深度优先搜索的过程中，每个搜索到的 11 都会被重新标记为 00。

最终岛屿的数量就是我们进行深度优先搜索的次数。

下面的动画展示了整个算法。




class Solution {
    void dfs(char[][] grid, int r, int c) {
        int nr = grid.length;
        int nc = grid[0].length;

        if (r < 0 || c < 0 || r >= nr || c >= nc || grid[r][c] == '0') {
            return;
        }

        grid[r][c] = '0';
        dfs(grid, r - 1, c);
        dfs(grid, r + 1, c);
        dfs(grid, r, c - 1);
        dfs(grid, r, c + 1);
    }

    public int numIslands(char[][] grid) {
        if (grid == null || grid.length == 0) {
            return 0;
        }

        int nr = grid.length;
        int nc = grid[0].length;
        int num_islands = 0;
        for (int r = 0; r < nr; ++r) {
            for (int c = 0; c < nc; ++c) {
                if (grid[r][c] == '1') {
                    ++num_islands;
                    dfs(grid, r, c);
                }
            }
        }

        return num_islands;
    }
}
复杂度分析

时间复杂度：O(MN)O(MN)，其中 MM 和 NN 分别为行数和列数。

空间复杂度：O(MN)O(MN)，在最坏情况下，整个网格均为陆地，深度优先搜索的深度达到 M NMN。

方法二：广度优先搜索
同样地，我们也可以使用广度优先搜索代替深度优先搜索。

为了求出岛屿的数量，我们可以扫描整个二维网格。如果一个位置为 11，则将其加入队列，开始进行广度优先搜索。在广度优先搜索的过程中，每个搜索到的 11 都会被重新标记为 00。直到队列为空，搜索结束。

最终岛屿的数量就是我们进行广度优先搜索的次数。


class Solution {
    public int numIslands(char[][] grid) {
        if (grid == null || grid.length == 0) {
            return 0;
        }

        int nr = grid.length;
        int nc = grid[0].length;
        int num_islands = 0;

        for (int r = 0; r < nr; ++r) {
            for (int c = 0; c < nc; ++c) {
                if (grid[r][c] == '1') {
                    ++num_islands;
                    grid[r][c] = '0';
                    Queue<Integer> neighbors = new LinkedList<>();
                    neighbors.add(r * nc + c);
                    while (!neighbors.isEmpty()) {
                        int id = neighbors.remove();
                        int row = id / nc;
                        int col = id % nc;
                        if (row - 1 >= 0 && grid[row-1][col] == '1') {
                            neighbors.add((row-1) * nc + col);
                            grid[row-1][col] = '0';
                        }
                        if (row + 1 < nr && grid[row+1][col] == '1') {
                            neighbors.add((row+1) * nc + col);
                            grid[row+1][col] = '0';
                        }
                        if (col - 1 >= 0 && grid[row][col-1] == '1') {
                            neighbors.add(row * nc + col-1);
                            grid[row][col-1] = '0';
                        }
                        if (col + 1 < nc && grid[row][col+1] == '1') {
                            neighbors.add(row * nc + col+1);
                            grid[row][col+1] = '0';
                        }
                    }
                }
            }
        }

        return num_islands;
    }
}
复杂度分析

时间复杂度：O(MN)O(MN)，其中 MM 和 NN 分别为行数和列数。

空间复杂度：O(\min(M, N))O(min(M,N))，在最坏情况下，整个网格均为陆地，队列的大小可以达到 \min(M, N)min(M,N)。

方法三：并查集
同样地，我们也可以使用并查集代替搜索。

为了求出岛屿的数量，我们可以扫描整个二维网格。如果一个位置为 11，则将其与相邻四个方向上的 11 在并查集中进行合并。

最终岛屿的数量就是并查集中连通分量的数目。

下面的动画展示了整个算法。




class Solution {
    class UnionFind {
        int count;
        int[] parent;
        int[] rank;

        public UnionFind(char[][] grid) {
            count = 0;
            int m = grid.length;
            int n = grid[0].length;
            parent = new int[m * n];
            rank = new int[m * n];
            for (int i = 0; i < m; ++i) {
                for (int j = 0; j < n; ++j) {
                    if (grid[i][j] == '1') {
                        parent[i * n + j] = i * n + j;
                        ++count;
                    }
                    rank[i * n + j] = 0;
                }
            }
        }

        public int find(int i) {
            if (parent[i] != i) parent[i] = find(parent[i]);
            return parent[i];
        }

        public void union(int x, int y) {
            int rootx = find(x);
            int rooty = find(y);
            if (rootx != rooty) {
                if (rank[rootx] > rank[rooty]) {
                    parent[rooty] = rootx;
                } else if (rank[rootx] < rank[rooty]) {
                    parent[rootx] = rooty;
                } else {
                    parent[rooty] = rootx;
                    rank[rootx] += 1;
                }
                --count;
            }
        }

        public int getCount() {
            return count;
        }
    }

    public int numIslands(char[][] grid) {
        if (grid == null || grid.length == 0) {
            return 0;
        }

        int nr = grid.length;
        int nc = grid[0].length;
        int num_islands = 0;
        UnionFind uf = new UnionFind(grid);
        for (int r = 0; r < nr; ++r) {
            for (int c = 0; c < nc; ++c) {
                if (grid[r][c] == '1') {
                    grid[r][c] = '0';
                    if (r - 1 >= 0 && grid[r-1][c] == '1') {
                        uf.union(r * nc + c, (r-1) * nc + c);
                    }
                    if (r + 1 < nr && grid[r+1][c] == '1') {
                        uf.union(r * nc + c, (r+1) * nc + c);
                    }
                    if (c - 1 >= 0 && grid[r][c-1] == '1') {
                        uf.union(r * nc + c, r * nc + c - 1);
                    }
                    if (c + 1 < nc && grid[r][c+1] == '1') {
                        uf.union(r * nc + c, r * nc + c + 1);
                    }
                }
            }
        }

        return uf.getCount();
    }
}
复杂度分析

时间复杂度：O(MN * \alpha(MN))O(MN∗α(MN))，其中 MM 和 NN 分别为行数和列数。注意当使用路径压缩（见 find 函数）和按秩合并（见数组 rank）实现并查集时，单次操作的时间复杂度为 \alpha(MN)α(MN)，其中 \alpha(x)α(x) 为反阿克曼函数，当自变量 xx 的值在人类可观测的范围内（宇宙中粒子的数量）时，函数 \alpha(x)α(x) 的值不会超过 55，因此也可以看成是常数时间复杂度。

空间复杂度：O(MN)O(MN)，这是并查集需要使用的空间。

下一篇：DFS + BFS + 并查集（Python 代码、Java 代码）

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
