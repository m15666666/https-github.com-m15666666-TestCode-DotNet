using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*

输出: true
示例 2:

输入:
[
  ["8","3",".",".","7",".",".",".","."],
  ["6",".",".","1","9","5",".",".","."],
  [".","9","8",".",".",".",".","6","."],
  ["8",".",".",".","6",".",".",".","3"],
  ["4",".",".","8",".","3",".",".","1"],
  ["7",".",".",".","2",".",".",".","6"],
  [".","6",".",".",".",".","2","8","."],
  [".",".",".","4","1","9",".",".","5"],
  [".",".",".",".","8",".",".","7","9"]
]
输出: false
解释: 除了第一行的第一个数字从 5 改为 8 以外，空格内其他数字均与 示例1 相同。
     但由于位于左上角的 3x3 宫内有两个 8 存在, 因此这个数独是无效的。
说明:

一个有效的数独（部分已被填充）不一定是可解的。
只需要根据以上规则，验证已经填入的数字是否有效即可。
给定数独序列只包含数字 1-9 和字符 '.' 。
给定数独永远是 9x9 形式的。
*/
/// <summary>
/// https://leetcode-cn.com/problems/valid-sudoku/
/// 36. 有效的数独
/// 
/// </summary>
class ValidSudokuSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public bool IsValidSudoku(char[][] board)
    {
        bool[] has = new bool[27 * 10];
        Array.Fill(has, false);

        int num;
        int box;
        int rowIndex;
        int columnIndex;
        int boxIndex;
        for (int row = 0; row < 9; row++)
        {
            for (int column = 0; column < 9; column++)
            {
                char c = board[row][column];
                if (c == '.') continue;

                num = c - '0';
                box = (row / 3) * 3 + column / 3;

                rowIndex = row * 9 + num;
                if (has[rowIndex]) return false;
                else has[rowIndex] = true;

                columnIndex = 81 + column * 9 + num;
                if (has[columnIndex]) return false;
                else has[columnIndex] = true;

                boxIndex = 162 + box * 9 + num;
                if (has[boxIndex]) return false;
                else has[boxIndex] = true;
            }
        }

        return true;
    }
}
/*

有效的数独
力扣 (LeetCode)
发布于 1 年前
40.8k
思路
一个简单的解决方案是遍历该 9 x 9 数独 三 次，以确保：

行中没有重复的数字。
列中没有重复的数字。
3 x 3 子数独内没有重复的数字。
实际上，所有这一切都可以在一次迭代中完成。

方法：一次迭代
首先，让我们来讨论下面两个问题：

如何枚举子数独？
可以使用 box_index = (row / 3) * 3 + columns / 3，其中 / 是整数除法。

image.png

如何确保行 / 列 / 子数独中没有重复项？
可以利用 value -> count 哈希映射来跟踪所有已经遇到的值。

现在，我们完成了这个算法的所有准备工作：

遍历数独。
检查看到每个单元格值是否已经在当前的行 / 列 / 子数独中出现过：
如果出现重复，返回 false。
如果没有，则保留此值以进行进一步跟踪。
返回 true。


class Solution {
  public boolean isValidSudoku(char[][] board) {
    // init data
    HashMap<Integer, Integer> [] rows = new HashMap[9];
    HashMap<Integer, Integer> [] columns = new HashMap[9];
    HashMap<Integer, Integer> [] boxes = new HashMap[9];
    for (int i = 0; i < 9; i++) {
      rows[i] = new HashMap<Integer, Integer>();
      columns[i] = new HashMap<Integer, Integer>();
      boxes[i] = new HashMap<Integer, Integer>();
    }

    // validate a board
    for (int i = 0; i < 9; i++) {
      for (int j = 0; j < 9; j++) {
        char num = board[i][j];
        if (num != '.') {
          int n = (int)num;
          int box_index = (i / 3 ) * 3 + j / 3;

          // keep the current cell value
          rows[i].put(n, rows[i].getOrDefault(n, 0) + 1);
          columns[j].put(n, columns[j].getOrDefault(n, 0) + 1);
          boxes[box_index].put(n, boxes[box_index].getOrDefault(n, 0) + 1);

          // check if this value has been already seen before
          if (rows[i].get(n) > 1 || columns[j].get(n) > 1 || boxes[box_index].get(n) > 1)
            return false;
        }
      }
    }

    return true;
  }
}
复杂度分析

时间复杂度：O(1)O(1)，因为我们只对 81 个单元格进行了一次迭代。
空间复杂度：O(1)O(1)。 

public class Solution {
    public bool IsValidSudoku(char[][] board)
    {
        var hash = new HashSet<char>();
        for (int i = 0; i < 9; i++)
        {
            if (!Check(board, hash, 0, i, 8, i)) return false;
            if (!Check(board, hash, i, 0, i, 8)) return false;
            var x1 = (i % 3) * 3;
            var y1 = i / 3 * 3;
            if (!Check(board, hash, x1, y1, x1 + 2, y1 + 2)) return false;
        }
        return true;
    }

    bool Check(char[][] board, HashSet<char> hash, int x1, int y1, int x2, int y2)
    {
        hash.Clear();
        for (int i = x1; i <= x2; i++)
        {
            for (int j = y1; j <= y2; j++)
            {
                if (board[j][i] != '.')
                {
                    if (!hash.Add(board[j][i]))
                    {
                        return false;
                    }
                }
            }
        }
        return true;
    }
}
*/
