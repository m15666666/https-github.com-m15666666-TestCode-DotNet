using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
在一个 N × N 的方形网格中，每个单元格有两种状态：空（0）或者阻塞（1）。

一条从左上角到右下角、长度为 k 的畅通路径，由满足下述条件的单元格 C_1, C_2, ..., C_k 组成：

相邻单元格 C_i 和 C_{i+1} 在八个方向之一上连通（此时，C_i 和 C_{i+1} 不同且共享边或角）
C_1 位于 (0, 0)（即，值为 grid[0,0]）
C_k 位于 (N-1, N-1)（即，值为 grid[N-1,N-1]）
如果 C_i 位于 (r, c)，则 grid[r,c] 为空（即，grid[r,c] == 0）
返回这条从左上角到右下角的最短畅通路径的长度。如果不存在这样的路径，返回 -1 。

 

示例 1：

输入：[[0,1],[1,0]]

输出：2

示例 2：

输入：[[0,0,0],[1,1,0],[1,1,0]]

输出：4

 

提示：

1 <= grid.length == grid[0].length <= 100
grid[i,j] 为 0 或 1
通过次数3,212提交次数11,247
*/
/// <summary>
/// https://leetcode-cn.com/problems/shortest-path-in-binary-matrix/
/// 1091. 二进制矩阵中的最短路径
/// 
/// </summary>
class ShortestPathInBinaryMatrixSolution
{
    public void Test()
    {
        var ret = ShortestPathBinaryMatrix(new int[][] { new int[]{ 0,1}, new int[] { 1,0} });
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int ShortestPathBinaryMatrix(int[][] grid)
    {
        int m = grid.Length;
        int n = grid[0].Length;

        if (grid[0][0] == 1 || grid[m - 1][n - 1] == 1) return -1;

        int ret = 0;
        Queue<Node> queue = new Queue<Node>( m * n );

        var visited = new System.Collections.BitArray(m * n);
        visited.Set(0, true);

        queue.Enqueue(new Node(0, 0));

        int nextX, nextY, x, y;
        while (0 < queue.Count)
        {
            int size = queue.Count;
            
            for (int i = 0; i < size; i++)
            {
                Node currentNode = queue.Dequeue();
                x = currentNode.x;
                y = currentNode.y;

                if (x == (n - 1) && y == (m - 1)) return (ret + 1);
                
                foreach (var offset in OffsetNodes)
                {
                    nextX = x + offset.x;
                    nextY = y + offset.y;

                    if (nextX < 0 || nextX >= n || nextY < 0 || nextY >= m) continue;

                    int key = nextX * n + nextY;
                    if (visited[key]) continue;

                    if (grid[nextX][nextY] == 1) continue;

                    visited.Set(key, true);
                    queue.Enqueue(new Node((sbyte)nextX,(sbyte)nextY));
                }
            }
            ret++;
        }

        return -1;
    }

    private static readonly Node[] OffsetNodes = new Node[] {new Node(1, 0), new Node(-1, 0), new Node(1, - 1), new Node(1, 1),
                                          new Node(0, 1), new Node(0, -1), new Node(-1, -1), new Node(- 1, 1)};
    public struct Node
    {
        public Node(sbyte x0, sbyte y0 )
        {
            x = x0;
            y = y0;
        }
        public sbyte x;
        public sbyte y;
    }
}
/*
标准的BFS解法，多练习就会掌握。
hank
发布于 11 天前
208 阅读
解题思路
（1）BFS的问题一般都会选用队列方式实现；
（2）代码模板如下：

void BFS()
{
    定义队列;
    定义备忘录，用于记录已经访问的位置；

    判断边界条件，是否能直接返回结果的。

    将起始位置加入到队列中，同时更新备忘录。

    while (队列不为空) {
        获取当前队列中的元素个数。
        for (元素个数) {
            取出一个位置节点。
            判断是否到达终点位置。
            获取它对应的下一个所有的节点。
            条件判断，过滤掉不符合条件的位置。
            新位置重新加入队列。
        }
    }

}
代码
struct Node {
    int x;
    int y;
};
class Solution {
public:
    int shortestPathBinaryMatrix(vector<vector<int>>& grid) {
        int ans = 0;
        queue<Node> myQ; // BFS一般通过队列方式解决
        int M = grid.size();
        int N = grid[0].size();

        // 先判断边界条件，很明显，这两种情况下都是不能到达终点的。
        if (grid[0][0] == 1 || grid[M - 1][N - 1] == 1) {
            return -1;
        }
        
        // 备忘录，记录已经走过的节点
        vector<vector<int>> mem(M, vector<int>(N, 0));
        
        myQ.push({0, 0});
        mem[0][0] = 1;

        // 以下是标准BFS的写法
        while (!myQ.empty()) {
            int size = myQ.size();

            for (int i = 0; i < size; i++) {
                Node currentNode = myQ.front();
                int x = currentNode.x;
                int y = currentNode.y;

                // 判断是否满足退出的条件
                if (x == (N - 1) && y == (M - 1)) {
                    return (ans + 1);
                }

                // 下一个节点所有可能情况
                vector<Node> nextNodes = {{x + 1, y}, {x - 1, y}, {x + 1, y - 1}, {x + 1, y + 1},
                                          {x, y + 1}, {x, y - 1}, {x - 1, y - 1}, {x - 1, y + 1}};

                for (auto& n : nextNodes) {
                    // 过滤条件1： 边界检查
                    if (n.x < 0 || n.x >= N || n.y < 0 || n.y >= M) {
                        continue;
                    }
                    // 过滤条件2：备忘录检查
                    if (mem[n.y][n.x] == 1) {
                        continue;
                    }
                    // 过滤条件3：题目中的要求
                    if (grid[n.y][n.x] == 1) {
                        continue;
                    }

                    // 通过过滤筛选，加入队列！
                    mem[n.y][n.x] = 1;
                    myQ.push(n);
                }               

                myQ.pop();
            }
            ans++;
        }

        return -1;        
    }
};
下一篇：1091. 二进制矩阵中的最短路径

public class Solution {
    public int ShortestPathBinaryMatrix(int[][] grid) {
        if(grid == null || grid.Length == 0 || grid[0].Length == 0)
            return 0;
        
        int rows = grid.Length, cols = grid[0].Length;
        if(grid[0][0] == 1 || grid[rows - 1][cols - 1] == 1)
            return -1;  //Invalid problem
        
        //8-direction 2d array for guidence
        int[,] dirs = {{1, 0}, {0, -1}, {0, 1}, {0, -1}, {1, 1}, {-1, -1}, {1, -1}, {-1, 1}};
        
        bool[,] isVisited = new bool[rows, cols];   //Might be passed if we could mask on the input
        isVisited[0, 0] = true;
        Queue<int[]> queue = new Queue<int[]>();
        queue.Enqueue(new int[]{0, 0});
        
        int stepCount = 0;
        
        while(queue.Any())
        {
            int qCount = queue.Count;
            for(int i = 0; i < qCount; i++)
            {
                int[] node = queue.Dequeue();
                
                if(node[0] == rows - 1 && node[1] == cols - 1)  //Reach bottom-right
                {
                    return stepCount + 1;
                }
                else
                {
                    for(int d = 0; d < dirs.GetLength(0); d++)
                    {
                        int nextX = node[0] + dirs[d, 0];
                        int nextY = node[1] + dirs[d, 1];
                        
                        if(nextX >= 0 && nextX < rows && nextY >= 0 && nextY < cols
                          && grid[nextX][nextY] == 0 
                          && !isVisited[nextX, nextY])
                        {
                            queue.Enqueue(new int[]{nextX, nextY});
                            isVisited[nextX, nextY] = true;
                        }
                    }
                }
            }
            
            stepCount++;
        }
        //All invalid input
        return -1;
    }
} 
*/
