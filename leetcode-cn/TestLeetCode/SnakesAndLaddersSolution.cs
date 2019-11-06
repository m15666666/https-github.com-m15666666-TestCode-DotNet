using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
在一块 N x N 的棋盘 board 上，从棋盘的左下角开始，每一行交替方向，按从 1 到 N*N 的数字给方格编号。例如，对于一块 6 x 6 大小的棋盘，可以编号如下：


玩家从棋盘上的方格 1 （总是在最后一行、第一列）开始出发。

每一次从方格 x 起始的移动都由以下部分组成：

你选择一个目标方块 S，它的编号是 x+1，x+2，x+3，x+4，x+5，或者 x+6，只要这个数字 <= N*N。
如果 S 有一个蛇或梯子，你就移动到那个蛇或梯子的目的地。否则，你会移动到 S。 
在 r 行 c 列上的方格里有 “蛇” 或 “梯子”；如果 board[r][c] != -1，那个蛇或梯子的目的地将会是 board[r][c]。

注意，你每次移动最多只能爬过蛇或梯子一次：就算目的地是另一条蛇或梯子的起点，你也不会继续移动。

返回达到方格 N*N 所需的最少移动次数，如果不可能，则返回 -1。

 

示例：

输入：[
[-1,-1,-1,-1,-1,-1],
[-1,-1,-1,-1,-1,-1],
[-1,-1,-1,-1,-1,-1],
[-1,35,-1,-1,13,-1],
[-1,-1,-1,-1,-1,-1],
[-1,15,-1,-1,-1,-1]]
输出：4
解释：
首先，从方格 1 [第 5 行，第 0 列] 开始。
你决定移动到方格 2，并必须爬过梯子移动到到方格 15。
然后你决定移动到方格 17 [第 3 行，第 5 列]，必须爬过蛇到方格 13。
然后你决定移动到方格 14，且必须通过梯子移动到方格 35。
然后你决定移动到方格 36, 游戏结束。
可以证明你需要至少 4 次移动才能到达第 N*N 个方格，所以答案是 4。
 

提示：

2 <= board.length = board[0].length <= 20
board[i][j] 介于 1 和 N*N 之间或者等于 -1。
编号为 1 的方格上没有蛇或梯子。
编号为 N*N 的方格上没有蛇或梯子。
*/
/// <summary>
/// https://leetcode-cn.com/problems/snakes-and-ladders/
/// 909. 蛇梯棋
/// 
/// </summary>
class SnakesAndLaddersSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int SnakesAndLadders(int[][] board)
    {
        int rowCount = board.Length;
        int sum = rowCount * rowCount;

        Dictionary<int, int> number2Distances = new Dictionary<int, int>();
        number2Distances.Add(1, 0);

        Queue<int> queue = new Queue<int>();
        queue.Enqueue(1);

        while (0 < queue.Count)
        {
            int number = queue.Dequeue();
            if (number == sum) return number2Distances[number];

            int upper = Math.Min(number + 6, sum);
            for (int n2 = number + 1; n2 <= upper; ++n2)
            {
                int r, c;
                (r,c ) = GetRowColumn(n2, rowCount);
                int n2target = board[r][c] == -1 ? n2 : board[r][c];
                if (!number2Distances.ContainsKey(n2target))
                {
                    number2Distances.Add(n2target, number2Distances[number] + 1);
                    queue.Enqueue(n2target);
                }
            }
        }
        return -1;
    }

    private static (int,int) GetRowColumn(int number, int rowCount)
    {
        int quot = (number - 1) / rowCount;
        int rem = (number - 1) % rowCount;
        int row = rowCount - 1 - quot;
        int col = row % 2 != rowCount % 2 ? rem : rowCount - 1 - rem;
        return (row, col);
    }
}
/*
方法 1：广度优先搜索
想法

由于我们需要找到一条最短路径，广度优先搜索是一种理想的方法。难点在于如何对每个方格枚举所有可能的移动。

算法

假设我们在方格 s 上，我们想知道一次移动之后所有可能的终点 s2。

这需要知道方格 s2 的坐标 get(s2)，这有一个小技巧：我们知道行号每 N 个方格改变一次，所以只依赖于 quot = (s2-1) / N；同样列号依赖于 rem = (s2-1) % N。

由此，我们可以实现一个根据方格 s 的信息进行广度优先搜索。

JavaPython
class Solution {
    public int snakesAndLadders(int[][] board) {
        int N = board.length;

        Map<Integer, Integer> dist = new HashMap();
        dist.put(1, 0);

        Queue<Integer> queue = new LinkedList();
        queue.add(1);

        while (!queue.isEmpty()) {
            int s = queue.remove();
            if (s == N*N) return dist.get(s);

            for (int s2 = s+1; s2 <= Math.min(s+6, N*N); ++s2) {
                int rc = get(s2, N);
                int r = rc / N, c = rc % N;
                int s2Final = board[r][c] == -1 ? s2 : board[r][c];
                if (!dist.containsKey(s2Final)) {
                    dist.put(s2Final, dist.get(s) + 1);
                    queue.add(s2Final);
                }
            }
        }
        return -1;
    }

    public int get(int s, int N) {
        // Given a square num s, return board coordinates (r, c) as r*N + c
        int quot = (s-1) / N;
        int rem = (s-1) % N;
        int row = N - 1 - quot;
        int col = row % 2 != N % 2 ? rem : N - 1 - rem;
        return row * N + col;
    }
}
复杂度分析

时间复杂度：O(N^2)O(N 
2
 )，其中 NN 是 board 的长度。
空间复杂度：O(N^2)O(N 
2
 )。
 
*/
