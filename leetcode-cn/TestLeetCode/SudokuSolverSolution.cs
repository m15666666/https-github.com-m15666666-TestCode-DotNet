using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
编写一个程序，通过已填充的空格来解决数独问题。

一个数独的解法需遵循如下规则：

数字 1-9 在每一行只能出现一次。
数字 1-9 在每一列只能出现一次。
数字 1-9 在每一个以粗实线分隔的 3x3 宫内只能出现一次。
空白格用 '.' 表示。

一个数独。

答案被标成红色。

Note:

给定的数独序列只包含数字 1-9 和字符 '.' 。
你可以假设给定的数独只有唯一解。
给定数独永远是 9x9 形式的。
*/
/// <summary>
/// https://leetcode-cn.com/problems/sudoku-solver/
/// 37. 解数独
/// 
/// 
/// </summary>
class SudokuSolverSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public void SolveSudoku(char[][] board)
    {
        const int n = 3;
        const int N = n * n;

        bool[,] rows = new bool[N, N + 1];
        bool[,] columns = new bool[N, N + 1];
        bool[,] boxes = new bool[N, N + 1];

        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
            {
                char c = board[i][j];
                if (c != '.') TryPlaceNumber(c - '0', i, j);
            }
        }

        Backtrack(0, 0);
        return;

        bool TryPlaceNumber(int d, int row, int col)
        {
            int idx = (row / n) * n + col / n;
            var couldPlaceNumber = !rows[row, d] && !columns[col, d] && !boxes[idx, d];
            if (!couldPlaceNumber) return false;

            rows[row, d] = true;
            columns[col, d] = true;
            boxes[idx, d] = true;
            board[row][col] = (char)(d + '0');
            return true;
        }

        void RemoveNumber(int d, int row, int col)
        {
            int idx = (row / n) * n + col / n;
            rows[row, d] = false;
            columns[col, d] = false;
            boxes[idx, d] = false;
            board[row][col] = '.';
        }

        bool PlaceNextNumbers(int row, int col)
        {
            if ((col == N - 1) && (row == N - 1)) return true;
            return col == N - 1 ? Backtrack(row + 1, 0) : Backtrack(row, col + 1);
        }

        bool Backtrack(int row, int col)
        {
            if (board[row][col] == '.')
            {
                for (int d = 1; d < 10; d++)
                {
                    if (TryPlaceNumber(d, row, col))
                    {
                        if (PlaceNextNumbers(row, col)) return true;

                        RemoveNumber(d, row, col);
                    }
                }
                return false;
            }

            return PlaceNextNumbers(row, col);
        }
    }
}
/*

解数独
力扣 (LeetCode)
发布于 1 年前
29.1k
方法 0：蛮力法
首先的想法是通过蛮力法来生成所有可能用1 到 9填充空白格的解，
并且检查合法从而保留解。这意味着共有 9^{81}9 
81
  个操作需要进行。
其中 99 是可行的数字个数，8181 是需要填充的格子数目。
因此我们必须考虑进一步优化。

方法 1：回溯法
使用的概念

了解两个编程概念会对接下来的分析有帮助。

第一个叫做 约束编程。

基本的意思是在放置每个数字时都设置约束。在数独上放置一个数字后立即
排除当前 行， 列 和 子方块 对该数字的使用。这会传播 约束条件 并有利于减少需要考虑组合的个数。

37_const3.png

第二个叫做 回溯。

让我们想象一下已经成功放置了几个数字
在数独上。
但是该组合不是最优的并且不能继续放置数字了。该怎么办？ 回溯。
意思是回退，来改变之前放置的数字并且继续尝试。如果还是不行，再次 回溯。

37_backtrack2.png

如何枚举子方块

一种枚举子方块的提示是：

使用 方块索引= (行 / 3) * 3 + 列 / 3
其中 / 表示整数除法。

36_boxes_2.png

算法

现在准备好写回溯函数了
backtrack(row = 0, col = 0)。

从最左上角的方格开始 row = 0, col = 0。直到到达一个空方格。

从1 到 9 迭代循环数组，尝试放置数字 d 进入 (row, col) 的格子。

如果数字 d 还没有出现在当前行，列和子方块中：

将 d 放入 (row, col) 格子中。
记录下 d 已经出现在当前行，列和子方块中。
如果这是最后一个格子row == 8, col == 8 ：
意味着已经找出了数独的解。
否则
放置接下来的数字。
如果数独的解还没找到：
将最后的数从 (row, col) 移除。
代码

class Solution {
  // box size
  int n = 3;
  // row size
  int N = n * n;

  int [][] rows = new int[N][N + 1];
  int [][] columns = new int[N][N + 1];
  int [][] boxes = new int[N][N + 1];

  char[][] board;

  boolean sudokuSolved = false;

  public boolean couldPlace(int d, int row, int col) {
int idx = (row / n) * n + col / n;
    return rows[row][d] + columns[col][d] + boxes[idx][d] == 0;
  }

  public void placeNumber(int d, int row, int col)
{
    int idx = (row / n) * n + col / n;

    rows[row][d]++;
    columns[col][d]++;
    boxes[idx][d]++;
    board[row][col] = (char)(d + '0');
}

public void removeNumber(int d, int row, int col)
{
    int idx = (row / n) * n + col / n;
    rows[row][d]--;
    columns[col][d]--;
    boxes[idx][d]--;
    board[row][col] = '.';
}

public void placeNextNumbers(int row, int col)
{
    // if we're in the last cell
    // that means we have the solution
    if ((col == N - 1) && (row == N - 1))
    {
        sudokuSolved = true;
    }
    // if not yet
    else
    {
        // if we're in the end of the row
        // go to the next row
        if (col == N - 1) backtrack(row + 1, 0);
        // go to the next column
        else backtrack(row, col + 1);
    }
}

public void backtrack(int row, int col)
{
    // if the cell is empty
    if (board[row][col] == '.')
    {
        // iterate over all numbers from 1 to 9
        for (int d = 1; d < 10; d++)
        {
            if (couldPlace(d, row, col))
            {
                placeNumber(d, row, col);
                placeNextNumbers(row, col);
                // if sudoku is solved, there is no need to backtrack
                // since the single unique solution is promised
                if (!sudokuSolved) removeNumber(d, row, col);
            }
        }
    }
    else placeNextNumbers(row, col);
}

public void solveSudoku(char[][] board)
{
    this.board = board;

    // init rows, columns and boxes
    for (int i = 0; i < N; i++)
    {
        for (int j = 0; j < N; j++)
        {
            char num = board[i][j];
            if (num != '.')
            {
                int d = Character.getNumericValue(num);
                placeNumber(d, i, j);
            }
        }
    }
    backtrack(0, 0);
}
}
复杂性分析

这里的时间复杂性是常数由于数独的大小是固定的，因此没有 N 变量来衡量。
但是我们可以计算需要操作的次数：(9!)^9(9!) 
9
  。
我们考虑一行，即不多于 99 个格子需要填。
第一个格子的数字不会多于 99 种情况，
两个格子不会多于 9 \times 89×8 种情况，
三个格子不会多于 9 \times 8 \times 79×8×7 种情况等等。
总之一行可能的情况不会多于 9!9! 种可能，
所有行不会多于(9!)^9(9!) 
9
  种情况。比较一下：

9^{81}9 
81
  = 196627050475552913618075908526912116283103450944214766927315415537966391196809
为蛮力法，
而(9!)^{9}(9!) 
9
  = 109110688415571316480344899355894085582848000000000
为回溯法，
即数字的操作次数减少了 10^{27}10 
27
  倍！
空间复杂性：数独大小固定，空间用来存储数独，行，列和子方块的结构，每个有 81 个元素。

public class Solution
{
    public void SolveSudoku(char[][] board)
    {
        bool[,] dp = new bool[9, 10], row = new bool[9, 10], line = new bool[9, 10];
        for (int i = 0; i < 9; i++)
            for (int j = 0; j < 9; j++)
                if (board[i][j] != '.')
                {
                    int x = board[i][j] - 48;
                    dp[i / 3 * 3 + j / 3, x] = row[i, x] = line[j, x] = true;
                }
        H(board, dp, row, line, 0, 0);
    }
    static bool H(char[][] board, bool[,] dp, bool[,] row, bool[,] line, int i, int j)
    {
        if (i == 9) return true;
        if (board[i][j] != '.')
        {
            if (H(board, dp, row, line, j == 8 ? i + 1 : i, j == 8 ? 0 : j + 1)) return true;
        }
        else for (int k = 1; k < 10; k++)
                if (!line[j, k] && !row[i, k] && !dp[i / 3 * 3 + j / 3, k])
                {
                    int x = i / 3 * 3 + j / 3;
                    board[i][j] = (char)(k + 48);
                    dp[x, k] = row[i, k] = line[j, k] = true;
                    if (H(board, dp, row, line, j == 8 ? i + 1 : i, j == 8 ? 0 : j + 1)) return true;
                    board[i][j] = '.';
                    dp[x, k] = row[i, k] = line[j, k] = false;
                }
        return false;
    }
}

public class Solution {
    public void SolveSudoku(char[][] board) {
        // 三个布尔数组 表明 行, 列, 还有 3*3 的方格的数字是否被使用过
        bool[,] rowUsed = new bool[9,10];
        bool[,] colUsed = new bool[9,10];
        bool[,,] boxUsed = new bool[3,3,10];
        // 初始化
        for(int row = 0; row < board.Length; row++){
            for(int col = 0; col < board[0].Length; col++) {
                int num = board[row][col] - '0';
                if(1 <= num && num <= 9){
                    rowUsed[row,num] = true;
                    colUsed[col,num] = true;
                    boxUsed[row/3,col/3,num] = true;
                }
            }
        }
        // 递归尝试填充数组 
        recusiveSolveSudoku(board, rowUsed, colUsed, boxUsed, 0, 0);
    }
    
    private bool recusiveSolveSudoku(char[][]board, bool[,]rowUsed, bool[,]colUsed, bool[,,]boxUsed, int row, int col){
        // 边界校验, 如果已经填充完成, 返回true, 表示一切结束
        if(col == board[0].Length){
            col = 0;
            row++;
            if(row == board.Length){
                return true;
            }
        }
        // 是空则尝试填充, 否则跳过继续尝试填充下一个位置
        if(board[row][col] == '.') {
            // 尝试填充1~9
            for(int num = 1; num <= 9; num++){
                bool canUsed = !(rowUsed[row,num] || colUsed[col,num] || boxUsed[row/3,col/3,num]);
                if(canUsed){
                    rowUsed[row,num] = true;
                    colUsed[col,num] = true;
                    boxUsed[row/3,col/3,num] = true;
                    
                    board[row][col] = (char)('0' + num);
                    if(recusiveSolveSudoku(board, rowUsed, colUsed, boxUsed, row, col + 1)){
                        return true;
                    }
                    board[row][col] = '.';
                    
                    rowUsed[row,num] = false;
                    colUsed[col,num] = false;
                    boxUsed[row/3,col/3,num] = false;
                }
            }
        } else {
            return recusiveSolveSudoku(board, rowUsed, colUsed, boxUsed, row, col + 1);
        }
        return false;
    }
}
*/
