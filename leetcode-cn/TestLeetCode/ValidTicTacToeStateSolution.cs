using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
用字符串数组作为井字游戏的游戏板 board。当且仅当在井字游戏过程中，玩家有可能将字符放置成游戏板所显示的状态时，才返回 true。

该游戏板是一个 3 x 3 数组，由字符 " "，"X" 和 "O" 组成。字符 " " 代表一个空位。

以下是井字游戏的规则：

玩家轮流将字符放入空位（" "）中。
第一个玩家总是放字符 “X”，且第二个玩家总是放字符 “O”。
“X” 和 “O” 只允许放置在空位中，不允许对已放有字符的位置进行填充。
当有 3 个相同（且非空）的字符填充任何行、列或对角线时，游戏结束。
当所有位置非空时，也算为游戏结束。
如果游戏结束，玩家不允许再放置字符。
示例 1:
输入: board = ["O  ", "   ", "   "]
输出: false
解释: 第一个玩家总是放置“X”。

示例 2:
输入: board = ["XOX", " X ", "   "]
输出: false
解释: 玩家应该是轮流放置的。

示例 3:
输入: board = ["XXX", "   ", "OOO"]
输出: false

示例 4:
输入: board = ["XOX", "O O", "XOX"]
输出: true
说明:

游戏板 board 是长度为 3 的字符串数组，其中每个字符串 board[i] 的长度为 3。
 board[i][j] 是集合 {" ", "X", "O"} 中的一个字符。
*/
/// <summary>
/// https://leetcode-cn.com/problems/valid-tic-tac-toe-state/
/// 794. 有效的井字游戏
/// https://blog.csdn.net/lcvcl/article/details/88722068
/// </summary>
class ValidTicTacToeStateSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public bool ValidTicTacToe(string[] board)
    {
        const char X = 'X';
        const char O = 'O';
        int xCount = 0;
        int oCount = 0;
        foreach (var s in board)
            foreach (var c in s)
                switch(c)
                {
                    case O:
                        oCount++;
                        break;

                    case X:
                        xCount++;
                        break;
                }

        if (xCount - oCount == 1)
        {
            var oSuccess = IsWinner(board, O);
            return !oSuccess;
        }
        if (xCount == oCount)
        {
            var xSuccess = IsWinner(board, X);
            return !xSuccess;
        }
        return false;
    }

    private static bool IsWinner(string[] board, char c)
    {
        const int Length = 3;
        for (int i = 0; i < Length; i++) {
            if (IsWinner(c, board[0][i],board[1][i],board[2][i])) return true;
            if (IsWinner(c,board[i][0],board[i][1],board[i][2])) return true;
        }

        if (IsWinner(c,board[1][1], board[0][0],board[2][2])) return true;
        if (IsWinner(c, board[1][1],board[0][2],board[2][0])) return true;

        return false;
    }
    private static bool IsWinner(char c, params char[] chars )
    {
        return (chars[0] == c && chars[1] == c && chars[2] == c);
    }
}
/*
public class Solution {
public bool ValidTicTacToe(string[] board)
        {
            if (board == null || board.Length == 0)
            {
                return false;
            }

            int[] row = new int[3];
            int[] column = new int[3];
            int diagonal = 0;
            int antiDiagnal = 0;
            int turns = 0;

            for (int i = 0; i < board.Length; i++)
            {
                for (int j = 0; j < board[0].Length; j++)
                {
                    if (board[i][j] == 'X')
                    {
                        turns++;
                        row[i]++;
                        column[j]++;
                        if (i == j)
                        {
                            diagonal++;
                        }

                        if (j + i == 2)
                        {
                            antiDiagnal++;
                        }
                    } else if (board[i][j] == 'O')
                    {
                        turns--;
                        row[i]--;
                        column[j]--;
                        if (i == j)
                        {
                            diagonal--;
                        }

                        if (j + i == 2)
                        {
                            antiDiagnal--;
                        }
                    }
                }
            }

            bool xWins = false;
            bool oWins = false;


            for (int index = 0; index < 3; index++)
            {
                if (row[index] == 3)
                {
                    xWins = true;
                }
                else if (row[index] == -3)
                {
                    oWins = true;
                }

                if (column[index] == 3)
                {
                    xWins = true;
                }
                else if(column[index] == -3)
                {
                    oWins = true;
                }
            }

            xWins = xWins || diagonal == 3 || antiDiagnal == 3;
            oWins = oWins || diagonal == -3 || antiDiagnal == -3;
            if ((xWins && turns == 0) || (oWins && turns == 1))
            {
                return false;
            }

            return (turns == 0 || turns == 1) && (!oWins || !xWins);
        }
}

*/
