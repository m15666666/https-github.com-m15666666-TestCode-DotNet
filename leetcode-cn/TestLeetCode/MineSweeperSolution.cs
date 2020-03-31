using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
让我们一起来玩扫雷游戏！

给定一个代表游戏板的二维字符矩阵。 'M' 代表一个未挖出的地雷，'E' 代表一个未挖出的空方块，
'B' 代表没有相邻（上，下，左，右，和所有4个对角线）地雷的已挖出的空白方块，数字（'1' 到 '8'）表示有多少地雷与这块已挖出的方块相邻，'X' 则表示一个已挖出的地雷。

现在给出在所有未挖出的方块中（'M'或者'E'）的下一个点击位置（行和列索引），根据以下规则，返回相应位置被点击后对应的面板：

如果一个地雷（'M'）被挖出，游戏就结束了- 把它改为 'X'。
如果一个没有相邻地雷的空方块（'E'）被挖出，修改它为（'B'），并且所有和其相邻的方块都应该被递归地揭露。
如果一个至少与一个地雷相邻的空方块（'E'）被挖出，修改它为数字（'1'到'8'），表示相邻地雷的数量。
如果在此次点击中，若无更多方块可被揭露，则返回面板。
 

示例 1：

输入: 

[['E', 'E', 'E', 'E', 'E'],
 ['E', 'E', 'M', 'E', 'E'],
 ['E', 'E', 'E', 'E', 'E'],
 ['E', 'E', 'E', 'E', 'E']]

Click : [3,0]

输出: 

[['B', '1', 'E', '1', 'B'],
 ['B', '1', 'M', '1', 'B'],
 ['B', '1', '1', '1', 'B'],
 ['B', 'B', 'B', 'B', 'B']]

解释:

示例 2：

输入: 

[['B', '1', 'E', '1', 'B'],
 ['B', '1', 'M', '1', 'B'],
 ['B', '1', '1', '1', 'B'],
 ['B', 'B', 'B', 'B', 'B']]

Click : [1,2]

输出: 

[['B', '1', 'E', '1', 'B'],
 ['B', '1', 'X', '1', 'B'],
 ['B', '1', '1', '1', 'B'],
 ['B', 'B', 'B', 'B', 'B']]

解释:

 

注意：

输入矩阵的宽和高的范围为 [1,50]。
点击的位置只能是未被挖出的方块 ('M' 或者 'E')，这也意味着面板至少包含一个可点击的方块。
输入面板不会是游戏结束的状态（即有地雷已被挖出）。
简单起见，未提及的规则在这个问题中可被忽略。例如，当游戏结束时你不需要挖出所有地雷，考虑所有你可能赢得游戏或标记方块的情况。
*/
/// <summary>
/// https://leetcode-cn.com/problems/minesweeper/
/// 529. 扫雷游戏
/// </summary>
class MineSweeperSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public char[][] UpdateBoard(char[][] board, int[] click)
    {
        int i = click[0];
        int j = click[1];
        int order = 0;
        BackTrack(board, i, j, ref order);
        return board;
    }

    private static readonly int[] _iNext = new int[] { -1, -1, -1, 0, 0, 1, 1, 1 };
    private static readonly int[] _jNext = new int[] { -1, 0, 1, -1, 1, -1, 0, 1 };
    private static void BackTrack( char[][] board, int i, int j, ref int order )
    {
        ++order;
        int row = board.Length;
        int column = board[0].Length;

        //越界返回
        if (i < 0 || i >= row || j < 0 || j >= column) return;

        var c = board[i][j];

        //挖到雷返回
        if (1 == order && c == 'M')
        {//一开始就挖到雷，主过程直接返回
            board[i][j] = 'X';//修改为X
            return;
        }

        //如果不是雷
        if ('E' == c )
        {
            int cnt = 0;
            for (int k = 0; k < 8; ++k)
            {//数周围的八个方向的地雷数
                int iNext = i + _iNext[k];
                int jNext = j + _jNext[k];

                if (iNext >= 0 && iNext < row && jNext >= 0 && jNext < column && board[iNext][jNext] == 'M')
                    ++cnt;
            }
            if (0 == cnt)
            {
                board[i][j] = 'B';
                for (int k = 0; k < 8; ++k)
                {//递归搜索周围的八个方向
                    int iNext = i + _iNext[k];
                    int jNext = j + _jNext[k];
                    BackTrack(board, iNext, jNext, ref order);//如果i,j被改为B,则递归搜索其他位置
                }
            }
            else
            {
                board[i][j] = (char)('0' + cnt);//i,j被改为数字(说明到了边界)，就不再递归的搜索
                return;
            }
        }
    }
}
/*
public class Solution {
    private static int[,] _d= new int[8,2]{{-1,-1},{-1,0},{-1,1},{0,-1},{0,1},{1,-1},{1,0},{1,1}}; 
    private int _length;
    private int _width;
    
    public char[,] UpdateBoard(char[,] board, int[] click) {
        _length=board.GetLength(0);
        _width=board.GetLength(1);
        if(board[click[0],click[1]]=='M')
        {
            board[click[0],click[1]]='X';
            return board;
        }
        else if(board[click[0],click[1]]!='E')
            return board;
        int count = 0;
        for(int i=0;i<8;i++)
        {
            int x=click[0]+_d[i,0];
            int y=click[1]+_d[i,1]; 
            if(InBoard(x,y)&&(board[x,y]=='M'))
                count++;
        }
        if(count>0)
        {
            board[click[0],click[1]]=char.Parse(count.ToString());
            return board;
        }
        else
        {
            board[click[0],click[1]]='B';
            for(int i=0;i<8;i++)
            {
                int x=click[0]+_d[i,0];
                int y=click[1]+_d[i,1]; 
                if(InBoard(x,y))
                {
                    int[] newClick = new int[2]{x,y};
                    board=UpdateBoard(board,newClick);
                }
            }
        }
        return board;
    }
    
    private bool InBoard(int x, int y)
    {
        return (x>=0&&x<_length&&y>=0&&y<_width);
    }
} 
*/
