﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
给定一个二维的甲板， 请计算其中有多少艘战舰。 战舰用 'X'表示，空位用 '.'表示。 你需要遵守以下规则：

给你一个有效的甲板，仅由战舰或者空位组成。
战舰只能水平或者垂直放置。换句话说,战舰只能由 1xN (1 行, N 列)组成，或者 Nx1 (N 行, 1 列)组成，其中N可以是任意大小。
两艘战舰之间至少有一个水平或垂直的空位分隔 - 即没有相邻的战舰。
示例 :

X..X
...X
...X
在上面的甲板中有2艘战舰。

无效样例 :

...X
XXXX
...X
你不会收到这样的无效甲板 - 因为战舰之间至少会有一个空位将它们分开。

进阶:

你可以用一次扫描算法，只使用O(1)额外空间，并且不修改甲板的值来解决这个问题吗？ 
*/
/// <summary>
/// https://leetcode-cn.com/problems/battleships-in-a-board/
/// 419. 甲板上的战舰
/// </summary>
class BattleshipsInABoardSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int CountBattleships(char[][] board)
    {
        if (board == null || board.Length == 0 || board[0].Length == 0) return 0;
        int m = board.Length;
        int n = board[0].Length;
        int ret = 0;
        for( int i = 0; i<m; i++ )
            for( int j = 0; j < n; j++)
            {
                const char X = 'X';
                var c = board[i][j];
                if (c == '.' || (0 < i && board[i - 1][j] == X) || (0 < j && board[i][j - 1] == X)) continue;

                ++ret;
            }
        return ret;
    }
}
/*
public class Solution {
    public int CountBattleships(char[][] board) {
        
        int row = board.Length;
        if (row == 0)
            return 0;
        
        int col = board[0].Length;
        
        int count = 0;
        for (int i = 0; i < row; i ++) {
            for (int j = 0; j < col; j ++) {
                if (board[i][j] == 'X') {
                    if (i > 0 && board[i-1][j] == 'X') {
                        continue;
                    } else if (j > 0 && board[i][j-1] == 'X') {
                        continue;
                    } else {
                        count ++;
                    }
                }
            }
        }
        
        
        return count;
    }
}

*/
