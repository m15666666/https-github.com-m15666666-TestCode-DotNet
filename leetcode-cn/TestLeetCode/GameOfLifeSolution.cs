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
/// 11(3)：表示当前是生，更新后是生。
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

    private readonly static int[,] move = new int[8,2] { { -1, -1 }, { 0, -1 }, { 1, -1 }, { -1, 0 }, { 1, 0 }, { -1, 1 }, { 0, 1 }, { 1, 1 } };
    public void GameOfLife(int[][] board)
    {
        int row = board.Length;
        if (row < 1) return;
        int col = board[0].Length;
        if (col < 1) return;

        const int Live = 1;
        const int Live_Live = 0b11; // 3
        const int Live_Dead = 0b01; // 1
        const int Dead_Live = 0b10; // 2
        for (int i = 0; i < row; i++)
            for (int j = 0; j < col; j++)
            {
                int liveNeighbors = 0;
                for (int k = 0; k < 8; k++)
                {
                    int x = i + move[k,0];
                    int y = j + move[k,1];
                    if (x < 0 || y < 0 || x >= row || y >= col) continue;
                    if ((board[x][y] & 1) == 1) ++liveNeighbors;
                }
                if (board[i][j] == Live) board[i][j] = (liveNeighbors < 2 || 3 < liveNeighbors) ? Live_Dead : Live_Live;
                else if (liveNeighbors == 3) board[i][j] = Dead_Live;
            }

        for (int i = 0; i < row; i++)
            for (int j = 0; j < col; j++)
                board[i][j] >>= 1;
    }
}
/*
生命游戏
力扣官方题解
发布于 2020-03-31
29.1k
📺 视频题解

📖 文字题解
分析
在讲具体解法之前，请先根据下面的图片理解题目中描述的细胞遵循的生存定律，这有助于我们后面的讲解。

fig

fig

方法一：复制原数组进行模拟
思路

这个问题看起来很简单，但有一个陷阱，如果你直接根据规则更新原始数组，那么就做不到题目中说的 同步 更新。假设你直接将更新后的细胞状态填入原始数组，那么当前轮次其他细胞状态的更新就会引用到当前轮已更新细胞的状态，但实际上每一轮更新需要依赖上一轮细胞的状态，是不能用这一轮的细胞状态来更新的。

fig

如上图所示，已更新细胞的状态会影响到周围其他还未更新细胞状态的计算。一个最简单的解决方法就是复制一份原始数组，复制的那一份永远不修改，只作为更新规则的引用。这样原始数组的细胞值就不会被污染了。

fig

算法

复制一份原始数组；

根据复制数组中邻居细胞的状态来更新 board 中的细胞状态。


class Solution {
    public void gameOfLife(int[][] board) {
        int[] neighbors = {0, 1, -1};

        int rows = board.length;
        int cols = board[0].length;

        // 创建复制数组 copyBoard
        int[][] copyBoard = new int[rows][cols];

        // 从原数组复制一份到 copyBoard 中
        for (int row = 0; row < rows; row++) {
            for (int col = 0; col < cols; col++) {
                copyBoard[row][col] = board[row][col];
            }
        }

        // 遍历面板每一个格子里的细胞
        for (int row = 0; row < rows; row++) {
            for (int col = 0; col < cols; col++) {

                // 对于每一个细胞统计其八个相邻位置里的活细胞数量
                int liveNeighbors = 0;

                for (int i = 0; i < 3; i++) {
                    for (int j = 0; j < 3; j++) {

                        if (!(neighbors[i] == 0 && neighbors[j] == 0)) {
                            int r = (row + neighbors[i]);
                            int c = (col + neighbors[j]);

                            // 查看相邻的细胞是否是活细胞
                            if ((r < rows && r >= 0) && (c < cols && c >= 0) && (copyBoard[r][c] == 1)) {
                                liveNeighbors += 1;
                            }
                        }
                    }
                }

                // 规则 1 或规则 3      
                if ((copyBoard[row][col] == 1) && (liveNeighbors < 2 || liveNeighbors > 3)) {
                    board[row][col] = 0;
                }
                // 规则 4
                if (copyBoard[row][col] == 0 && liveNeighbors == 3) {
                    board[row][col] = 1;
                }
            }
        }
    }
}
复杂度分析

时间复杂度：O(mn)O(mn)，其中 mm 和 nn 分别为 board 的行数和列数。

空间复杂度：O(mn)O(mn)，为复制数组占用的空间。

方法二：使用额外的状态
思路

方法一中 O(mn)O(mn) 的空间复杂度在数组很大的时候内存消耗是非常昂贵的。题目中每个细胞只有两种状态 live(1) 或 dead(0)，但我们可以拓展一些复合状态使其包含之前的状态。举个例子，如果细胞之前的状态是 0，但是在更新之后变成了 1，我们就可以给它定义一个复合状态 2。这样我们看到 2，既能知道目前这个细胞是活的，还能知道它之前是死的。

fig

算法

遍历 board 中的细胞。

根据数组的细胞状态计算新一轮的细胞状态，这里会用到能同时代表过去状态和现在状态的复合状态。

具体的计算规则如下所示：

规则 1：如果活细胞周围八个位置的活细胞数少于两个，则该位置活细胞死亡。这时候，将细胞值改为 -1，代表这个细胞过去是活的现在死了；

规则 2：如果活细胞周围八个位置有两个或三个活细胞，则该位置活细胞仍然存活。这时候不改变细胞的值，仍为 1；

规则 3：如果活细胞周围八个位置有超过三个活细胞，则该位置活细胞死亡。这时候，将细胞的值改为 -1，代表这个细胞过去是活的现在死了。可以看到，因为规则 1 和规则 3 下细胞的起始终止状态是一致的，因此它们的复合状态也一致；

规则 4：如果死细胞周围正好有三个活细胞，则该位置死细胞复活。这时候，将细胞的值改为 2，代表这个细胞过去是死的现在活了。

根据新的规则更新数组；

现在复合状态隐含了过去细胞的状态，所以我们可以在不复制数组的情况下完成原地更新；

对于最终的输出，需要将 board 转成 0，1 的形式。因此这时候需要再遍历一次数组，将复合状态为 2 的细胞的值改为 1，复合状态为 -1 的细胞的值改为 0。


class Solution {
    public void gameOfLife(int[][] board) {
        int[] neighbors = {0, 1, -1};

        int rows = board.length;
        int cols = board[0].length;

        // 遍历面板每一个格子里的细胞
        for (int row = 0; row < rows; row++) {
            for (int col = 0; col < cols; col++) {

                // 对于每一个细胞统计其八个相邻位置里的活细胞数量
                int liveNeighbors = 0;

                for (int i = 0; i < 3; i++) {
                    for (int j = 0; j < 3; j++) {

                        if (!(neighbors[i] == 0 && neighbors[j] == 0)) {
                            // 相邻位置的坐标
                            int r = (row + neighbors[i]);
                            int c = (col + neighbors[j]);

                            // 查看相邻的细胞是否是活细胞
                            if ((r < rows && r >= 0) && (c < cols && c >= 0) && (Math.abs(board[r][c]) == 1)) {
                                liveNeighbors += 1;
                            }
                        }
                    }
                }

                // 规则 1 或规则 3 
                if ((board[row][col] == 1) && (liveNeighbors < 2 || liveNeighbors > 3)) {
                    // -1 代表这个细胞过去是活的现在死了
                    board[row][col] = -1;
                }
                // 规则 4
                if (board[row][col] == 0 && liveNeighbors == 3) {
                    // 2 代表这个细胞过去是死的现在活了
                    board[row][col] = 2;
                }
            }
        }

        // 遍历 board 得到一次更新后的状态
        for (int row = 0; row < rows; row++) {
            for (int col = 0; col < cols; col++) {
                if (board[row][col] > 0) {
                    board[row][col] = 1;
                } else {
                    board[row][col] = 0;
                }
            }
        }
    }
}
复杂度分析

时间复杂度：O(mn)O(mn)，其中 mm，nn 分别为 board 的行数和列数。

空间复杂度：O(1)O(1)，除原数组外只需要常数的空间存放若干变量。

public class Solution {
    private int getValue(int[][] board, int row, int column)
            {
                if (row >= 0 && row < board.Length && column >= 0 && column < board[0].Length)
                {
                    return board[row][column];
                }

                return 0;
            }
            
    public int Simulate(int[][] board, int currentRowIndex, int currentColumnIndex)
            {
                int val = getValue(board, currentRowIndex - 1, currentColumnIndex - 1) % 2
                          + getValue(board, currentRowIndex - 1, currentColumnIndex) % 2
                          + getValue(board, currentRowIndex - 1, currentColumnIndex + 1) % 2
                          + getValue(board, currentRowIndex, currentColumnIndex - 1) % 2
                          + getValue(board, currentRowIndex, currentColumnIndex + 1) % 2
                          + getValue(board, currentRowIndex + 1, currentColumnIndex - 1) % 2
                          + getValue(board, currentRowIndex + 1, currentColumnIndex) % 2
                          + getValue(board, currentRowIndex + 1, currentColumnIndex + 1) % 2;

                if (board[currentRowIndex][currentColumnIndex] == 1)
                {
                    //活细胞
                    if (val < 2)
                        return 0;
                    else if (val > 3)
                        return 0;
                    else return 1;
                }
                else
                {
                    if (val == 3)
                        return 1;
                    return 0;
                }
                
            }

            public void GameOfLife(int[][] board)
            {
               int[][] WillBoard = new int[board.Length][];
                for (int rowIndex = 0; rowIndex < board.Length; rowIndex++)
                {
                    WillBoard[rowIndex] = new int[board[0].Length];
                    for (int columnIndex = 0; columnIndex < board[0].Length; columnIndex++)
                    {
                        // Console.WriteLine($"{rowIndex},{columnIndex}:{board[rowIndex][columnIndex]}");
                        WillBoard[rowIndex][columnIndex] = Simulate(board, rowIndex, columnIndex);
                    }
                }
                
                for (int rowIndex = 0; rowIndex < board.Length; rowIndex++)
                {
                    for (int columnIndex = 0; columnIndex < board[0].Length; columnIndex++)
                    {
                        board[rowIndex][columnIndex] = WillBoard[rowIndex][columnIndex];
                    }
                }
            }
}

public class Solution {
    public void GameOfLife(int[][] board) {
        for(int r=0;r<board.Length;r++){
        for(int c=0;c<board[0].Length;c++){
            int liveCount=0;
            if(r-1>=0) liveCount+=(board[r-1][c])%2;
            if(r+1<board.Length) liveCount+=(board[r+1][c])%2;
            if(c-1>=0) liveCount+=(board[r][c-1])%2;
            if(c+1<board[0].Length) liveCount+=(board[r][c+1])%2;
            if(r-1>=0&&c-1>=0) liveCount+=(board[r-1][c-1])%2;
            if(r+1<board.Length&&c+1<board[0].Length) liveCount+=(board[r+1][c+1])%2;
            if(r-1>=0&&c+1<board[0].Length) liveCount+=(board[r-1][c+1])%2;
            if(r+1<board.Length&&c-1>=0) liveCount+=(board[r+1][c-1])%2;
            if(board[r][c]==1){
                if(liveCount<2 || liveCount>3){
                    board[r][c]=3; // 3== 1 -> 0;
                }
            } else{
                if(liveCount==3){
                    board[r][c]=2; // 2== 0 -> 1
                }
            }
        }
    }
    for(int r=0;r<board.Length;r++){
        for(int c=0;c<board[0].Length;c++){
            if(board[r][c]==3) board[r][c]=0;
            if(board[r][c]==2) board[r][c]=1;
        }
    }
    }
}

     
*/
