using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
根据百度百科，生命游戏，简称为生命，是英国数学家约翰·何顿·康威在1970年发明的细胞自动机。

给定一个包含 m × n 个格子的面板，每一个格子都可以看成是一个细胞。每个细胞具有一个初始状态 
live（1）即为活细胞， 或 dead（0）即为死细胞。每个细胞与其八个相邻位置（水平，垂直，对角线）的细胞都遵循以下四条生存定律：

如果活细胞周围八个位置的活细胞数少于两个，则该位置活细胞死亡；
如果活细胞周围八个位置有两个或三个活细胞，则该位置活细胞仍然存活；
如果活细胞周围八个位置有超过三个活细胞，则该位置活细胞死亡；
如果死细胞周围正好有三个活细胞，则该位置死细胞复活；
根据当前状态，写一个函数来计算面板上细胞的下一个（一次更新后的）状态。
下一个状态是通过将上述规则同时应用于当前状态下的每个细胞所形成的，其中细胞的出生和死亡是同时发生的。

示例:

输入: 
[
  [0,1,0],
  [0,0,1],
  [1,1,1],
  [0,0,0]
]
输出: 
[
  [0,0,0],
  [1,0,1],
  [0,1,1],
  [0,1,0]
]
进阶:

你可以使用原地算法解决本题吗？请注意，面板上所有格子需要同时被更新：你不能先更新某些格子，然后使用它们的更新后的值再更新其他格子。
本题中，我们使用二维数组来表示面板。原则上，面板是无限的，但当活细胞侵占了面板边界时会造成问题。你将如何解决这些问题？
*/

/// <summary>
/// https://leetcode-cn.com/problems/game-of-life/
/// 289. 生命游戏
/// https://www.cnblogs.com/kexinxin/p/10204994.html
/// 既然需要"就地解决"，我们不妨分析一下borad的特性：board上的元素有两种状态，生（1）和死（0）。
/// 这两种状态存在了一个int型里面。所以我们可以有效利用除最低位的其它位，去保存更新后的状态，这样就不需要有额外的空间了。
/// 具体而言，我们可以用最低位表示当前状态，次低位表示更新后状态：
/// 00(0)：表示当前是死，更新后是死；
/// 01(1)：表示当前是生，更新后是死；
/// 10(2)：表示当前是死，更新后是生；
/// 11(3)：表示当前是神，更新后是生。
/// https://leetcode-cn.com/submissions/detail/12535542/
/// </summary>
class GameOfLifeSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public void GameOfLife(int[][] board)
    {
        int row = board.Length;
        if (row < 1) return;
        int col = board[0].Length;
        if (col < 1) return;

        int[,] move = new int[8,2] { { -1, -1 }, { 0, -1 }, { 1, -1 }, { -1, 0 }, { 1, 0 }, { -1, 1 }, { 0, 1 }, { 1, 1 } };
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                int live = 0;
                for (int k = 0; k < 8; k++)
                {
                    int x = i + move[k,0];
                    int y = j + move[k,1];
                    if (x < 0 || y < 0 || x >= row || y >= col) continue;
                    if ((board[x][y] & 1) == 1) ++live;
                }
                if ((board[i][j] & 1) == 1)
                {
                    if (live < 2 || live > 3) board[i][j] = 1;
                    else board[i][j] = 3;
                }
                else
                {
                    if (live == 3) board[i][j] = 2;
                }
            }
        }
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                board[i][j] >>= 1;
            }
        }
    }
}
/*
public class Solution {
       public  void GameOfLife(int[][] board)
        {
            for (var i = 0; i < board.Length; i++)
            {
                for (var j = 0; j < board[i].Length; j++)
                {
                    int live;
                    _count(out live, board, i, j);
                    if (board[i][j] == 1)
                    {
                        if (live < 2 || live > 3)
                            board[i][j] = -1;
                    }
                    else
                    {
                        if (live == 3)
                            board[i][j] = 2;
                    }
                }
            }
            for (var i = 0; i < board.Length; i++)
            {
                for (var j = 0; j < board[i].Length; j++)
                {
                    if (board[i][j] == 2)
                        board[i][j] = 1;
                    else if (board[i][j] == -1)
                        board[i][j] = 0;
                }
            }
        }
        private  void _count(out int live, int[][] board, int i, int j)
        {
            live = 0;
            if (i - 1 >= 0 && j - 1 >= 0 && (board[i - 1][j - 1] == 1 || board[i - 1][j - 1] == -1))
                ++live;
            if (i - 1 >= 0 && (board[i - 1][j] == 1|| board[i - 1][j] == -1))
                ++live;
            if (i - 1 >= 0 && j + 1 < board[i - 1].Length&&(board[i - 1][j + 1] == 1|| board[i - 1][j + 1] == -1))
                ++live;
            if (j - 1 >= 0 && (board[i][j - 1] == 1 || board[i][j - 1] == -1))
                ++live;
            if (j + 1 < board[i].Length && (board[i][j + 1] == 1|| board[i][j + 1] == -1))
                ++live;
            if (i + 1 < board.Length && j - 1 >= 0 && (board[i + 1][j - 1] == 1|| board[i + 1][j - 1] == -1))
                ++live;
            if (i + 1 < board.Length && (board[i + 1][j] == 1|| board[i + 1][j] == -1))
                ++live;
            if (i + 1 < board.Length && j + 1 < board[i + 1].Length && (board[i + 1][j + 1] == 1|| board[i + 1][j + 1] == -1))
                ++live;
        }
}
public class Solution {
    public void GameOfLife(int[][] board) 
    {
        for(int i = 0; i < board.Length; i++)
        {
            for(int j = 0; j < board[i].Length; j++)
            {
                UpdateCellState(board,i,j);
            }
        }
        
         for(int i = 0; i < board.Length; i++)
        {
            for(int j = 0; j < board[i].Length; j++)
            {
                board[i][j] = board[i][j]>>1;
            }
        }
    }
    
    private void UpdateCellState(int[][] board, int r, int c)
    {
        int liveCount = 0;
        for(int i = Math.Max(r-1,0); i < Math.Min(r+2,board.Length); i++)
        {
            for(int j = Math.Max(c-1,0); j < Math.Min(c+2,board[r].Length); j++)
            {
                if((board[i][j]&1) == 1)
                    liveCount++;
            }
        }
        
        if(liveCount ==3 || liveCount-board[r][c] == 3)
            board[r][c] |= 2;
    }
}
public class Solution {
    public void GameOfLife(int[][] board) {
        
           
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board[i].Length; j++)
                {
                    if(board[i][j] == 0 && GameOfLifeback(board, i, j)==3)
                    {
                        board[i][j] = -1;
                    }
                   else if(board[i][j] == 1)
                    {
                        if(GameOfLifeback(board, i, j) == 0) { board[i][j] = 1; }
                        else
                        board[i][j] = GameOfLifeback(board, i, j);
                    }
                    
                }
            }
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board[i].Length; j++)
                {
                    if (board[i][j] ==1)
                    {
                        // board[i][j] = 0;
                        board[i][j] = 0;

                    }
                    else if (board[i][j] == 2 || board[i][j]==3)
                    {
                        board[i][j] = 3;
                    }
                    else if(board[i][j] == -1)
                    {
                        board[i][j] = 3;
                    }
                     else
                    {
                        board[i][j] = 0;
                  ////      // board[i][j] = 0;
                   }
                    board[i][j] %= 2;
                }
            }
    }
       public static int GameOfLifeback(int[][] board, int m, int n)
        {
            int sum = 0;
            if (m > 0)
            {
                if (board[m - 1][n] >= 1)
                {
                    sum++;
                }
            }
            if (board.GetLength(0) - 1 > m)
            {
                if (board[m + 1][n] >= 1)
                {
                    sum++;
                }
            }
            if (n > 0)
            {
                if (board[m][n - 1] >= 1)
                {
                    sum++;
                }
            }
            if (board[m].Length - 1 > n)
            {
                if (board[m][n + 1] >= 1)
                {
                    sum++;
                }
            }
            if (board[m].Length - 1 > n && board.GetLength(0) - 1 > m)
            {
                if (board[m + 1][n + 1] >= 1)
                {
                    sum++;
                }
            }
            if (n > 0 && board.GetLength(0) - 1 > m)
            {
                if (board[m + 1][n - 1] >= 1)
                {
                    sum++;
                }
            }
            if (board[m].Length - 1 > n && m > 0)
            {

                if (board[m - 1][n + 1] >= 1)
                {
                    sum++;
                }
            }
            if (n > 0 && m > 0)
            {
                if (board[m - 1][n - 1] >= 1)
                {
                    sum++;
                }
            }
            return sum;
        }
}
public class Solution {
    public void GameOfLife(int[][] board) {
        int right = board[0].Length - 1;
            int bottom = board.Length - 1;
            for (int i = 0; i < board.Length; i++)
            {
                for (int j = 0; j < board[i].Length; j++)
                {
                    int live = 0;
                    for (int io = -1; io <= 1; io++)
                    {
                        for (int jo = -1; jo <= 1; jo++)
                        {
                            if (io == 0 && jo == 0) continue;
                            int row = i + io, col = j + jo;
                            if (row >= 0 && row < board.Length && col >= 0 && col < board[row].Length && (board[row][col] & 1) == 1) live++;
                        }
                    }
                    if ((board[i][j] & 1) == 1)
                    {
                        if (2 <= live && live <= 3) board[i][j] |= 0b10;
                    }
                    else
                    {
                        if (live == 3) board[i][j] |= 0b10;
                    }
                }
            }
            for (int i = 0; i < board.Length; i++)
            {
                for (int j = 0; j < board[i].Length; j++)
                {
                    board[i][j] >>= 1;
                }
            
            }}
}
     
*/
