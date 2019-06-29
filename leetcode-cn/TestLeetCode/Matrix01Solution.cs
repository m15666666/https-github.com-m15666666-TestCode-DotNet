using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个由 0 和 1 组成的矩阵，找出每个元素到最近的 0 的距离。

两个相邻元素间的距离为 1 。

示例 1: 
输入:

0 0 0
0 1 0
0 0 0
输出:

0 0 0
0 1 0
0 0 0
示例 2: 
输入:

0 0 0
0 1 0
1 1 1
输出:

0 0 0
0 1 0
1 2 1
注意:

给定矩阵的元素个数不超过 10000。
给定矩阵中至少有一个元素是 0。
矩阵中的元素只在四个方向上相邻: 上、下、左、右。 
*/
/// <summary>
/// https://leetcode-cn.com/problems/01-matrix/
/// 542. 01 矩阵
/// https://www.jianshu.com/p/aecba8d8aca0
/// </summary>
class Matrix01Solution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int[][] UpdateMatrix(int[][] matrix)
    {
        int row = matrix.Length;
        int col = matrix[0].Length;
        Queue<int> q = new Queue<int>(128);
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                if (matrix[i][j] == 0)
                {
                    EnqueuePair(q,i,j);
                }
                else
                {
                    matrix[i][j] = int.MaxValue;
                }
            }
        }

        while ( 0 < q.Count )
        {
            int x = q.Dequeue();
            int y = q.Dequeue();
            int cur = matrix[x][y] + 1;
            if (x > 0 && matrix[x - 1][y] > cur)
            {
                matrix[x - 1][y] = cur;
                EnqueuePair(q, x - 1, y);
            }
            if (x < row - 1 && matrix[x + 1][y] > cur)
            {
                matrix[x + 1][y] = cur;
                EnqueuePair(q, x + 1, y);
            }
            if (y > 0 && matrix[x][y - 1] > cur)
            {
                matrix[x][y - 1] = cur;
                EnqueuePair(q, x, y - 1);
            }
            if (y < col - 1 && matrix[x][y + 1] > cur)
            {
                matrix[x][y + 1] = cur;
                EnqueuePair(q, x, y + 1);
            }
        }
        return matrix;

        void EnqueuePair(Queue<int> queue, int x, int y)
        {
            queue.Enqueue(x); queue.Enqueue(y);
        }
    }
}
/*
public class Solution {
    public int[][] UpdateMatrix(int[][] matrix) {
        int m = matrix.Count();
        int n = matrix[0].Count();
        
        var queue = new Queue<int[]>();
        for (int i = 0; i < m; i++) {
            for (int j = 0; j < n; j++) {
                if (matrix[i][j] == 0) {
                    queue.Enqueue(new int[] {i, j});
                }
                else {
                    matrix[i][j] = int.MaxValue;
                }
            }
        }
        
        var dirs = new int[][]{new int[]{-1, 0}, new int[]{1, 0}, new int[]{0, -1}, new int[]{0, 1}};
        
        while (queue.Count() != 0) {
            int[] cell = queue.Dequeue();
            foreach (int[] d in dirs) {
                int r = cell[0] + d[0];
                int c = cell[1] + d[1];
                if (r < 0 || r >= m || c < 0 || c >= n || 
                    matrix[r][c] <= matrix[cell[0]][cell[1]] + 1) continue;
                queue.Enqueue(new int[] {r, c});
                matrix[r][c] = matrix[cell[0]][cell[1]] + 1;
            }
        }
        
        return matrix;
    }
}
*/
