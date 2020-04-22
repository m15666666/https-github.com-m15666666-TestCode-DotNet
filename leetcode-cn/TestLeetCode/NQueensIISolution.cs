using System;
using System.Collections.Generic;

/*
n 皇后问题研究的是如何将 n 个皇后放置在 n×n 的棋盘上，并且使皇后彼此之间不能相互攻击。



上图为 8 皇后问题的一种解法。

给定一个整数 n，返回 n 皇后不同的解决方案的数量。

示例:

输入: 4
输出: 2
解释: 4 皇后问题存在如下两个不同的解法。
[
 [".Q..",  // 解法 1
  "...Q",
  "Q...",
  "..Q."],

 ["..Q.",  // 解法 2
  "Q...",
  "...Q",
  ".Q.."]
]
*/

/// <summary>
/// https://leetcode-cn.com/problems/n-queens-ii/
/// 52. N皇后 II
///
/// </summary>
internal class NQueensIISolution
{
    public void Test()
    {
        var ret = TotalNQueens(10);
        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int TotalNQueens(int n)
    {
        // 棋盘所有的列都可放置，
        // 即，按位表示为 n 个 '1'
        // bin(cols) = 0b1111 (n = 4), bin(cols) = 0b111 (n = 3)
        // [1 = 可放置]
        int Mask = (1 << n) - 1;

        int count = 0;
        BackTrack(0, 0, 0, 0);
        return count;

        void BackTrack(int row, int hills, int next_row, int dales)
        {
            //row: 当前放置皇后的行号
            //hills: 主对角线占据情况 [1 = 被占据，0 = 未被占据]
            //next_row: 下一行被占据的情况 [1 = 被占据，0 = 未被占据]
            //dales: 次对角线占据情况 [1 = 被占据，0 = 未被占据]
            //count: 所有可行解的个数

            if (row == n)
            {
                count++;
                return;
            }

            // 当前行可用的列
            // ! 表示 0 和 1 的含义对于变量 hills, next_row and dales的含义是相反的
            // [1 = 未被占据，0 = 被占据]
            int free_columns = Mask & ~(hills | next_row | dales);

            // 找到可以放置下一个皇后的列
            while (free_columns != 0)
            {
                // free_columns 的第一个为 '1' 的位
                // 在该列我们放置当前皇后
                int curr_column = -free_columns & free_columns;

                // 放置皇后
                // 并且排除对应的列
                free_columns ^= curr_column;

                BackTrack(row + 1,
                        (hills | curr_column) << 1,
                        next_row | curr_column,
                        (dales | curr_column) >> 1
                        );
            }
        }
    }

    //public int TotalNQueens(int n) 
    //{
    //    var ret = 0;
    //    var usedColumn = new bool[n];
    //    var antiDiagonals = new bool[2 * n - 1]; // -(n-1) to (n-1) elements
    //    var diagonals = new bool[2 * n - 1]; // 0 to (2n - 2) elements

    //    BackTrack(0);

    //    return ret;

    //    bool IsNotUnderAttack(int row, int col)
    //    {
    //        return !usedColumn[col] && !antiDiagonals[row - col + n - 1] && !diagonals[row + col];
    //    }

    //    void PlaceQueen(int row, int col)
    //    {
    //        usedColumn[col] = true;
    //        antiDiagonals[row - col + n - 1] = true;  // translate -(n-1) to 0
    //        diagonals[row + col] = true;   //
    //    }

    //    void RemoveQueen(int row, int col)
    //    {
    //        usedColumn[col] = false;
    //        antiDiagonals[row - col + n - 1] = false;  // translate -(n-1) to 0
    //        diagonals[row + col] = false;
    //    }

    //    void BackTrack(int row)
    //    {
    //        for (int col = 0; col < n; col++)
    //        {
    //            if (IsNotUnderAttack(row, col))
    //            {
    //                PlaceQueen(row, col);

    //                if (row + 1 == n) ret++;
    //                else BackTrack(row + 1);

    //                RemoveQueen(row, col);
    //            }
    //        }
    //    }
    //}
}
/*
http://www.ic-net.or.jp/home/takaken/e/queen/
N Queens Problem (number of Solutions)
Version 3.1 (July/2003), Version 3.2 (2011)

1. Basic Source code and Total Solutions

#include <stdio.h>

int  SIZE, MASK, COUNT;

void Backtrack(int y, int left, int down, int right)
{
    int  bitmap, bit;

    if (y == SIZE) {
        COUNT++;
    } else {
        bitmap = MASK & ~(left | down | right);
        while (bitmap) {
            bit = -bitmap & bitmap;
            bitmap ^= bit;
            Backtrack(y+1, (left | bit)<<1, down | bit, (right | bit)>>1);
        }
    }
}
int main(void)
{
    SIZE = 10;    // <- N  
    COUNT = 0;    // result 

    MASK = (1 << SIZE) - 1;
    Backtrack(0, 0, 0, 0);

    printf("N=%d -> %d\n", SIZE, COUNT);
    return 0;
}

Board Image and Bit Field:
- - - - - Q - -    00000100  0: Start
- - - Q - - - -    00010000  1:  |
- - - - - - Q -    00000010  2:  |
Q - - - - - - -    10000000  3:  | Back Tracking
- - - - - - - Q    00000001  4:  |
- Q - - - - - -    01000000  5:  |
- - - - Q - - -    00001000  6:  |/
- - Q - - - - -    00100000  7: Last


2. Unique Solutions

A unique solution is a minimum value in 8 ways of rotation and reverse.
- - - - Q   0
- - Q - -   2
Q - - - -   4   --->  0 2 4 1 3  (Unique judgment value)
- - - Q -   1
- Q - - -   3

First queen is not here(X). [N>=2]
X X X - - -    X X X - -
- - - - - -    - - - - -
- - - - - -    - - - - -
- - - - - -    - - - - -
- - - - - -    - - - - -
- - - - - -

If first queen is in the corner, a queen is not here(X).
X X X - X Q
- Q - - X -
- - - - X -
- - - - X -
- - - - - -
- - - - - -

First queen is inside, a queen is not here(X).
X X X X x Q X X
X - - - x x x X
C - - x - x - x
- - x - - x - -
- x - - - x - -
x - - - - x - A
X - - - - x - X
X X B - - x X X

If a queen is not in A and B and C, all Solutions is Unique.
Judgment value is investigated when that is not right.
 90-degree rotation. (A)
180-degree rotation. (B)
270-degree rotation. (C)


3. Total Solutions from Unique Solutions

If first queen is in the corner.
    Total Solutions = Unique Solutions X 8.

If first queen is inside.
    If 90-degree rotation is same pattern as the original.
        Total Solutions = Unique Solutions X 2.
    Else if 180-degree rotation is same pattern as the original.
        Total Solutions = Unique Solutions X 4.
    Else
        Total Solutions = Unique Solutions X 8.


4. Completed Source code

--> Download nqueens.c   (ver3.1 2003)
--> Download nqueens2.c  (ver3.2 2011)
--> More high-speed program is here.  (by deepgreen)

Version 3.2 (2-core)
<------  N-Queens Solutions  -----> <---- time ---->
 N:           Total          Unique days hh:mm:ss.--
 5:              10               2             0.00
 6:               4               1             0.00
 7:              40               6             0.00
 8:              92              12             0.00
 9:             352              46             0.00
10:             724              92             0.00
11:            2680             341             0.00
12:           14200            1787             0.00
13:           73712            9233             0.02
14:          365596           45752             0.05
15:         2279184          285053             0.22
16:        14772512         1846955             1.47
17:        95815104        11977939             9.42
18:       666090624        83263591          1:11.21
19:      4968057848       621012754          8:32.54
20:     39029188884      4878666808       1:10:55.48
21:    314666222712     39333324973       9:24:40.50

AMD Athlon(tm) Dual Core Processor 5050e 2.60 GHz
Microsoft Visual C++ 2008 Express Edition with SP1
Windows SDK for Windows Server 2008 and .NET

INDEX

https://leetcode-cn.com/problems/n-queens-ii/solution/nhuang-hou-ii-by-leetcode/

N皇后 II
力扣 (LeetCode)
发布于 1 年前
10.4k
直观想法
这个问题是一个经典的问题，感受解法的优雅性很重要。

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
  public boolean is_not_under_attack(int row, int col, int n,
                                     int [] rows,
                                     int [] hills,
                                     int [] dales) {
    int res = rows[col] + hills[row - col + 2 * n] + dales[row + col];
    return (res == 0) ? true : false;
  }

  public int backtrack(int row, int count, int n,
                       int [] rows,
                       int [] hills,
                       int [] dales) {
    for (int col = 0; col < n; col++) {
      if (is_not_under_attack(row, col, n, rows, hills, dales)) {
        // place_queen
        rows[col] = 1;
        hills[row - col + 2 * n] = 1;  // "hill" diagonals
        dales[row + col] = 1;   //"dale" diagonals    

        // if n queens are already placed
        if (row + 1 == n) count++;
        // if not proceed to place the rest
        else count = backtrack(row + 1, count, n,
                rows, hills, dales);

        // remove queen
        rows[col] = 0;
        hills[row - col + 2 * n] = 0;
        dales[row + col] = 0;
      }
    }
    return count;
  }

  public int totalNQueens(int n) {
    int rows[] = new int[n];
    // "hill" diagonals
    int hills[] = new int[4 * n - 1];
    // "dale" diagonals
    int dales[] = new int[2 * n - 1];

    return backtrack(0, 0, n, rows, hills, dales);
  }
}
复杂度分析

时间复杂度：\mathcal{O}(N!)O(N!). 放置第 1 个皇后有 N 种可能的方法，放置两个皇后的方法不超过 N (N - 2) ，放置 3 个皇后的方法不超过 N(N - 2)(N - 4) ，以此类推。总体上，时间复杂度为 \mathcal{O}(N!)O(N!) .
空间复杂度：\mathcal{O}(N)O(N) . 需要保存对角线和行的信息。



方法 2：使用 bitmap 回溯
如果你是在面试中 - 使用方法 1。

下面的算法有着相同的时间复杂度 \mathcal{O}(N!)O(N!)。但是由于使用了位运算，可以运行得更快。

感谢这个算法的提出者 takaken.

为了便于理解该算法，下面的代码进行了逐步解释。

class Solution {
  public int backtrack(int row, int hills, int next_row, int dales, int count, int n) {
     //row: 当前放置皇后的行号
     //hills: 主对角线占据情况 [1 = 被占据，0 = 未被占据]
     //next_row: 下一行被占据的情况 [1 = 被占据，0 = 未被占据]
     //dales: 次对角线占据情况 [1 = 被占据，0 = 未被占据]
     //count: 所有可行解的个数

    // 棋盘所有的列都可放置，
    // 即，按位表示为 n 个 '1'
    // bin(cols) = 0b1111 (n = 4), bin(cols) = 0b111 (n = 3)
    // [1 = 可放置]
    int columns = (1 << n) - 1;

    if (row == n)   // 如果已经放置了 n 个皇后
      count++;  // 累加可行解
    else {
      // 当前行可用的列
      // ! 表示 0 和 1 的含义对于变量 hills, next_row and dales的含义是相反的
      // [1 = 未被占据，0 = 被占据]
      int free_columns = columns & ~(hills | next_row | dales);

      // 找到可以放置下一个皇后的列
      while (free_columns != 0) {
        // free_columns 的第一个为 '1' 的位
        // 在该列我们放置当前皇后
        int curr_column = - free_columns & free_columns;

        // 放置皇后
        // 并且排除对应的列
        free_columns ^= curr_column;

        count = backtrack(row + 1,
                (hills | curr_column) << 1,
                next_row | curr_column,
                (dales | curr_column) >> 1,
                count, n);
      }
    }

    return count;
  }
  public int totalNQueens(int n) {
    return backtrack(0, 0, 0, 0, 0, n);
  }
}
复杂度分析

时间复杂度：\mathcal{O}(N!)O(N!).
空间复杂度：\mathcal{O}(N)O(N).
下一篇：DFS + 位运算剪枝

public class Solution {
    public int TotalNQueens(int n) {
        int x = 0;
        H(n, 0, new bool[n], new bool[2 * n - 1], new bool[2 * n - 1],ref x);
        return x;
    }
    static void H(int n, int i, bool[] line, bool[] A, bool[] B,ref int x)
    {
        if (i == n) x++;
        else if (i < n)
            for (int j = 0; j < n; j++)
                if (!line[j] && !A[i + j] && !B[i - j + n - 1]){
                    line[j] = A[i + j] = B[i - j + n - 1] = true;
                    H(n, i + 1, line, A, B, ref x);
                    line[j] = A[i + j] = B[i - j + n - 1] = false;
                }
    }
}
     
*/
