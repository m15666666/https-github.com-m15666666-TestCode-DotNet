using System;
using System.Collections.Generic;

/*
n 皇后问题研究的是如何将 n 个皇后放置在 n×n 的棋盘上，并且使皇后彼此之间不能相互攻击。

上图为 8 皇后问题的一种解法。

给定一个整数 n，返回所有不同的 n 皇后问题的解决方案。

每一种解法包含一个明确的 n 皇后问题的棋子放置方案，该方案中 'Q' 和 '.' 分别代表了皇后和空位。

示例:

输入: 4
输出: [
 [".Q..",  // 解法 1
  "...Q",
  "Q...",
  "..Q."],

 ["..Q.",  // 解法 2
  "Q...",
  "...Q",
  ".Q.."]
]
解释: 4 皇后问题存在两个不同的解法。
*/

/// <summary>
/// https://leetcode-cn.com/problems/n-queens/
/// 51. N皇后
///
/// </summary>
internal class NQueensSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public IList<IList<string>> SolveNQueens(int n)
    {
        var ret = new List<IList<string>>();
        var usedColumn = new bool[n];
        var antiDiagonals = new bool[2 * n - 1]; // -(n-1) to (n-1) elements
        var diagonals = new bool[2 * n - 1]; // 0 to (2n - 2) elements
        var queenColumnOfRow = new int[n];
        char[] template = new char[n];
        Array.Fill(template, '.');

        BackTrack(0);

        return ret;

        bool IsNotUnderAttack(int row, int col)
        {
            return !usedColumn[col] && !antiDiagonals[row - col + n - 1] && !diagonals[row + col];
        }

        void PlaceQueen(int row, int col)
        {
            queenColumnOfRow[row] = col;
            usedColumn[col] = true;
            antiDiagonals[row - col + n - 1] = true;  // translate -(n-1) to 0
            diagonals[row + col] = true;   //
        }

        void RemoveQueen(int row, int col)
        {
            queenColumnOfRow[row] = -1;
            usedColumn[col] = false;
            antiDiagonals[row - col + n - 1] = false;  // translate -(n-1) to 0
            diagonals[row + col] = false;
        }

        void AddSolution()
        {
            var solution = new List<string>();
            for (int i = 0; i < n; ++i)
            {
                int col = queenColumnOfRow[i];
                template[col] = 'Q';
                solution.Add(new string(template));
                template[col] = '.';
            }
            ret.Add(solution);
        }

        void BackTrack(int row)
        {
            for (int col = 0; col < n; col++)
            {
                if (IsNotUnderAttack(row, col))
                {
                    PlaceQueen(row, col);

                    if (row + 1 == n) AddSolution();
                    else BackTrack(row + 1);

                    RemoveQueen(row, col);
                }
            }
        }
    }
}

/*

N皇后
力扣 (LeetCode)
发布于 1 年前
34.7k
直观想法
第一个想法是使用蛮力法，意味着生成在棋盘上放置 N 个皇后的所有可能的情况，并且检查是否保证没有皇后可以互相攻击。这意味着 \mathcal{O}(N^N)O(N 
N
 ) 的时间复杂度，因此我们必须考虑优化。

下面是两个有用的编程概念。

第一个叫做 约束编程.

它的基本含义是在放置每个皇后以后增加限制。当在棋盘上放置了一个皇后后，立即排除当前行，列和对应的两个对角线。该过程传递了 约束 从而有助于减少需要考虑情况数。

51_pic.png

第二个叫做 回溯法.

我们来想象一下，当在棋盘上放置了几个皇后且不会相互攻击。但是选择的方案不是最优的，因为无法放置下一个皇后。此时我们该怎么做？回溯。意思是回退一步，来改变最后放置皇后的位置并且接着往下放置。如果还是不行，再 回溯。

51_backtracking_.png




方法1：回溯
在建立算法之前，我们来考虑两个有用的细节。

一行只可能有一个皇后且一列也只可能有一个皇后。

这意味着没有必要再棋盘上考虑所有的方格。只需要按列循环即可。

对于所有的主对角线有 行号 + 列号 = 常数，对于所有的次对角线有 行号 - 列号 = 常数.

这可以让我们标记已经在攻击范围下的对角线并且检查一个方格 (行号, 列号) 是否处在攻击位置。

51_diagonals.png

现在已经可以写回溯函数 backtrack(row = 0).

从第一个 row = 0 开始.

循环列并且试图在每个 column 中放置皇后.

如果方格 (row, column) 不在攻击范围内

在 (row, column) 方格上放置皇后。
排除对应行，列和两个对角线的位置。
If 所有的行被考虑过，row == N
意味着我们找到了一个解
Else
继续考虑接下来的皇后放置 backtrack(row + 1).
回溯：将在 (row, column) 方格的皇后移除.
下面是上述算法的一个直接的实现。



class Solution {
  int rows[];
  // "hill" diagonals
  int hills[];
  // "dale" diagonals
  int dales[];
  int n;
  // output
  List<List<String>> output = new ArrayList();
  // queens positions
  int queens[];

  public boolean isNotUnderAttack(int row, int col) {
    int res = rows[col] + hills[row - col + 2 * n] + dales[row + col];
    return (res == 0) ? true : false;
  }

  public void placeQueen(int row, int col) {
    queens[row] = col;
    rows[col] = 1;
    hills[row - col + 2 * n] = 1;  // "hill" diagonals
    dales[row + col] = 1;   //"dale" diagonals
  }

  public void removeQueen(int row, int col) {
    queens[row] = 0;
    rows[col] = 0;
    hills[row - col + 2 * n] = 0;
    dales[row + col] = 0;
  }

  public void addSolution() {
    List<String> solution = new ArrayList<String>();
    for (int i = 0; i < n; ++i) {
      int col = queens[i];
      StringBuilder sb = new StringBuilder();
      for(int j = 0; j < col; ++j) sb.append(".");
      sb.append("Q");
      for(int j = 0; j < n - col - 1; ++j) sb.append(".");
      solution.add(sb.toString());
    }
    output.add(solution);
  }

  public void backtrack(int row) {
    for (int col = 0; col < n; col++) {
      if (isNotUnderAttack(row, col)) {
        placeQueen(row, col);
        // if n queens are already placed
        if (row + 1 == n) addSolution();
          // if not proceed to place the rest
        else backtrack(row + 1);
        // backtrack
        removeQueen(row, col);
      }
    }
  }

  public List<List<String>> solveNQueens(int n) {
    this.n = n;
    rows = new int[n];
    hills = new int[4 * n - 1];
    dales = new int[2 * n - 1];
    queens = new int[n];

    backtrack(0);
    return output;
  }
}
复杂度分析

时间复杂度：\mathcal{O}(N!)O(N!). 放置第 1 个皇后有 N 种可能的方法，放置两个皇后的方法不超过 N (N - 2) ，放置 3 个皇后的方法不超过 N(N - 2)(N - 4) ，以此类推。总体上，时间复杂度为 \mathcal{O}(N!)O(N!) .
空间复杂度：\mathcal{O}(N)O(N) . 需要保存对角线和行的信息。
下一篇：回溯算法详解

public class Solution {
    public IList<IList<string>> SolveNQueens(int n) {
            List<IList<string>> result = new List<IList<string>>();
            int[] rows = new int[n];//用于记录行方向会被皇后攻击的索引
            int[] mains = new int[2 * n - 1];//用于记录主对角线方向会被皇后攻击的索引
            int[] seconds = new int[2 * n - 1];//用于记录次对角线会被皇后攻击的索引
            int[] queens = new int[n];//用于记录放置皇后的索引
            void BackTrack(int row)
            {
                if (row >= n)
                    return;
                // 分别尝试在 row 行中的每一列中放置皇后
                for (int col = 0; col < n; col++)
                {
                    if(IsNotAttack(row, col))
                    {
                        PlaceQueen(row, col);
                        if (row == n - 1)// 当前行是最后一行，则找到了一个解决方案
                            AddSolution();
                        BackTrack(row + 1);// 在下一行中放置皇后
                        RemoveQueen(row, col);// 撤销，回溯，即将当前位置的皇后去掉
                    }
                }
            }
            bool IsNotAttack(int row, int col)
            {
                return rows[col] + mains[row - col + n - 1] + seconds[row + col] == 0;
            }
            void AddSolution()
            {
                List<string> solution = new List<string>();
                for(int i = 0; i < n; i++)
                {
                    StringBuilder sb = new StringBuilder();
                    int col = queens[i];
                    for (int j = 0; j < col; j++)
                        sb.Append('.');
                    sb.Append('Q');
                    for (int j = 0; j < n - col - 1; j++)
                        sb.Append('.');
                    solution.Add(sb.ToString());
                }
                result.Add(solution);
            }
            void RemoveQueen(int row, int col)
            {
                queens[row] = 0;
                rows[col] = 0;
                mains[row - col + n - 1] = 0;
                seconds[row + col] = 0;
            }
            void PlaceQueen(int row, int col)
            {
                queens[row] = col;
                rows[col] = 1;//表示当前位置列已经有皇后
                mains[row - col + n - 1] = 1;//表示主对角线的这个位置已经不能被放置
                seconds[col + row] = 1;//次对角线位置已经不能被放置
            }
            BackTrack(0);
            return result;
    }
}

public class Solution {
    public IList<IList<string>> SolveNQueens(int n) {
        var res = new List<IList<string>>();
        H(n, 0, new bool[n], new bool[2 * n - 1], new bool[2 * n - 1], new Dictionary<int, int>(), res);
        return res;
    }
    static void H(int n, int i, bool[] line, bool[] A, bool[] B, Dictionary<int, int> dic, List<IList<string>> res)
    {
        if (i == n){
            string[] str = new string[n];
            for (int j = 0; j < n; j++){
                string temp = "";
                for (int k = 0; k < n; k++)
                    temp += dic[j] == k ? "Q" : ".";
                str[j] = temp;
            }
            res.Add(str);
        }
        for (int j = 0; j < n && i < n; j++)
            if (!line[j] && !A[i + j] && !B[i - j + n - 1]){
                line[j] = A[i + j] = B[i - j + n - 1] = true;
                dic[i] = j;
                H(n, i + 1, line, A, B, dic, res);
                line[j] = A[i + j] = B[i - j + n - 1] = false;
                dic.Remove(i);
            }
    }
}
*/
