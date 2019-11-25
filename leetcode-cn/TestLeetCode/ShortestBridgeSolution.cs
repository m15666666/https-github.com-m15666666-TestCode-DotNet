using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
在给定的二维二进制数组 A 中，存在两座岛。（岛是由四面相连的 1 形成的一个最大组。）

现在，我们可以将 0 变为 1，以使两座岛连接起来，变成一座岛。

返回必须翻转的 0 的最小数目。（可以保证答案至少是 1。）

示例 1：

输入：[[0,1],[1,0]]
输出：1
示例 2：

输入：[[0,1,0],[0,0,0],[0,0,1]]
输出：2
示例 3：

输入：[[1,1,1,1,1],[1,0,0,0,1],[1,0,1,0,1],[1,0,0,0,1],[1,1,1,1,1]]
输出：1

提示：

1 <= A.Length = A[0].Length <= 100
A[i][j] == 0 或 A[i][j] == 1
*/
/// <summary>
/// https://leetcode-cn.com/problems/shortest-bridge/
/// 934. 最短的桥
/// 
/// </summary>
class ShortestBridgeSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int ShortestBridge(int[][] A)
    {
        int R = A.Length, C = A[0].Length;
        int[,] colors = GetColors(A);

        Queue<Node> queue = new Queue<Node>();
        var target = new HashSet<int>();

        for (int r = 0; r < R; ++r)
            for (int c = 0; c < C; ++c)
            {
                if (colors[r,c] == 1)
                {
                    queue.Enqueue(new Node(r, c, 0));
                }
                else if (colors[r,c] == 2)
                {
                    target.Add(r * C + c);
                }
            }

        while (0 < queue.Count)
        {
            Node node = queue.Dequeue();
            if (target.Contains(node.r * C + node.c)) return node.depth - 1;

            foreach (int nei in GetNeighbors(A, node.r, node.c))
            {
                int nr = nei / C, nc = nei % C;
                if (colors[nr,nc] != 1)
                {
                    queue.Enqueue(new Node(nr, nc, node.depth + 1));
                    colors[nr,nc] = 1;
                }
            }
        }
        return R + C;
    }

    private static int[,] GetColors(int[][] A)
    {
        int R = A.Length;
        int C = A[0].Length;
        int[,] colors = new int[R,C];
        int t = 0;

        for (int r0 = 0; r0 < R; ++r0)
            for (int c0 = 0; c0 < C; ++c0)
                if (colors[r0,c0] == 0 && A[r0][c0] == 1)
                {
                    // Start dfs
                    Stack<int> stack = new Stack<int>();
                    stack.Push(r0 * C + c0);
                    colors[r0,c0] = ++t;

                    while (0 < stack.Count)
                    {
                        int node = stack.Pop();
                        int r = node / C, c = node % C;
                        foreach (int nei in GetNeighbors(A, r, c))
                        {
                            int nr = nei / C, nc = nei % C;
                            if (A[nr][nc] == 1 && colors[nr,nc] == 0)
                            {
                                colors[nr,nc] = t;
                                stack.Push(nr * C + nc);
                            }
                        }
                    }
                }

        return colors;
    }

    private static List<int> GetNeighbors(int[][] A, int r, int c)
    {
        int R = A.Length, C = A[0].Length;
        List<int> ans = new List<int>();
        if (0 <= r - 1) ans.Add((r - 1) * R + c);
        if (0 <= c - 1) ans.Add(r * R + (c - 1));
        if (r + 1 < R) ans.Add((r + 1) * R + c);
        if (c + 1 < C) ans.Add(r * R + (c + 1));
        return ans;
    }

    public class Node
    {
        public int r;
        public int c;
        public int depth;
        public Node(int r, int c, int d)
        {
            this.r = r;
            this.c = c;
            depth = d;
        }
    }
}
/*
方法一：搜索
分析

我们使用的方法非常直接：首先找到这两座岛，随后选择一座，将它不断向外延伸一圈，直到到达了另一座岛。

在寻找这两座岛时，我们使用深度优先搜索。在向外延伸时，我们使用广度优先搜索。

算法

我们通过对数组 A 中的 1 进行深度优先搜索，可以得到两座岛的位置集合，分别为 source 和 target。随后我们从 source 中的所有位置开始进行广度优先搜索，当它们到达了 target 中的任意一个位置时，搜索的层数就是答案。

JavaPython
class Solution {
    public int shortestBridge(int[][] A) {
        int R = A.length, C = A[0].length;
        int[][] colors = getComponents(A);

        Queue<Node> queue = new LinkedList();
        Set<Integer> target = new HashSet();

        for (int r = 0; r < R; ++r)
            for (int c = 0; c < C; ++c) {
                if (colors[r][c] == 1) {
                    queue.add(new Node(r, c, 0));
                } else if (colors[r][c] == 2) {
                    target.add(r * C + c);
                }
            }

        while (!queue.isEmpty()) {
            Node node = queue.poll();
            if (target.contains(node.r * C + node.c))
                return node.depth - 1;
            for (int nei: neighbors(A, node.r, node.c)) {
                int nr = nei / C, nc = nei % C;
                if (colors[nr][nc] != 1) {
                    queue.add(new Node(nr, nc, node.depth + 1));
                    colors[nr][nc] = 1;
                }
            }
        }

        throw null;
    }

    public int[][] getComponents(int[][] A) {
        int R = A.length, C = A[0].length;
        int[][] colors = new int[R][C];
        int t = 0;

        for (int r0 = 0; r0 < R; ++r0)
            for (int c0 = 0; c0 < C; ++c0)
                if (colors[r0][c0] == 0 && A[r0][c0] == 1) {
                    // Start dfs
                    Stack<Integer> stack = new Stack();
                    stack.push(r0 * C + c0);
                    colors[r0][c0] = ++t;

                    while (!stack.isEmpty()) {
                        int node = stack.pop();
                        int r = node / C, c = node % C;
                        for (int nei: neighbors(A, r, c)) {
                            int nr = nei / C, nc = nei % C;
                            if (A[nr][nc] == 1 && colors[nr][nc] == 0) {
                                colors[nr][nc] = t;
                                stack.push(nr * C + nc);
                            }
                        }
                    }
                }

        return colors;
    }

    public List<Integer> neighbors(int[][] A, int r, int c) {
        int R = A.length, C = A[0].length;
        List<Integer> ans = new ArrayList();
        if (0 <= r-1) ans.add((r-1) * R + c);
        if (0 <= c-1) ans.add(r * R + (c-1));
        if (r+1 < R) ans.add((r+1) * R + c);
        if (c+1 < C) ans.add(r * R + (c+1));
        return ans;
    }
}

class Node {
    int r, c, depth;
    Node(int r, int c, int d) {
        this.r = r;
        this.c = c;
        depth = d;
    }
}
复杂度分析

时间复杂度：O(MN)O(MN)，其中 MM 和 NN 分别是数组 A 的行数和列数。

空间复杂度：O(MN)O(MN)。

 
*/
