using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个包含 m x n 个元素的矩阵（m 行, n 列），请按照顺时针螺旋顺序，返回矩阵中的所有元素。

示例 1:

输入:
[
 [ 1, 2, 3 ],
 [ 4, 5, 6 ],
 [ 7, 8, 9 ]
]
输出: [1,2,3,6,9,8,7,4,5]
示例 2:

输入:
[
  [1, 2, 3, 4],
  [5, 6, 7, 8],
  [9,10,11,12]
]
输出: [1,2,3,4,8,12,11,10,9,5,6,7]
*/
/// <summary>
/// https://leetcode-cn.com/problems/spiral-matrix/
/// 54. 螺旋矩阵
/// 
/// </summary>
class SpiralOrderSolution
{
    public static void Test()
    {
        //int[,] a = new int[2, 1] { { 3},{ 2} };

        //SpiralOrder(a);
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public IList<int> SpiralOrder(int[][] matrix)
    {
        var ret = new List<int>();
        if (matrix.Length == 0) return ret;

        int rowLeft = 0, rowRight = matrix.Length - 1;
        int columnLeft = 0, columnRight = matrix[0].Length - 1;
        while (rowLeft <= rowRight && columnLeft <= columnRight)
        {
            for (int c = columnLeft; c <= columnRight; c++) ret.Add(matrix[rowLeft][c]);
            for (int r = rowLeft + 1; r <= rowRight; r++) ret.Add(matrix[r][columnRight]);
            if (rowLeft < rowRight && columnLeft < columnRight)
            {
                for (int c = columnRight - 1; c > columnLeft; c--) ret.Add(matrix[rowRight][c]);
                for (int r = rowRight; r > rowLeft; r--) ret.Add(matrix[r][columnLeft]);
            }
            rowLeft++;
            rowRight--;
            columnLeft++;
            columnRight--;
        }
        return ret;
    }

    //public static IList<int> SpiralOrder(int[,] matrix)
    //{
    //    if (matrix == null || matrix.Length == 0) return new List<int>();

    //    Direction direction = Direction.Right;
    //    int upIndexBorder = 0;
    //    int downIndexBorder = matrix.GetUpperBound(0);
    //    int leftIndexBorder = 0;
    //    int rightIndexBorder = matrix.GetUpperBound(1);
    //    int rowIndex = 0, columnIndex = 0;
    //    int count = 0;

    //    List<int> ret = new List<int>();
    //    while( count < matrix.Length )
    //    {
    //        ret.Add(matrix[rowIndex, columnIndex]);
    //        count++;

    //        switch ( direction)
    //        {
    //            case Direction.Right:
    //                if( rightIndexBorder == columnIndex )
    //                {
    //                    //columnIndex = rightIndexBorder;
    //                    direction = Direction.Down;
    //                    rowIndex++;
    //                    upIndexBorder++;
    //                }
    //                else columnIndex++;
    //                break;

    //            case Direction.Down:
    //                if( downIndexBorder == rowIndex )
    //                {
    //                    //rowIndex = downIndexBorder;
    //                    direction = Direction.Left;
    //                    columnIndex--;
    //                    rightIndexBorder--;
    //                }
    //                else rowIndex++;
    //                break;

    //            case Direction.Left:
    //                if(columnIndex == leftIndexBorder)
    //                {
    //                    //columnIndex = leftIndexBorder;
    //                    direction = Direction.Up;
    //                    rowIndex--;
    //                    downIndexBorder--;
    //                }
    //                else columnIndex--;
    //                break;

    //            case Direction.Up:
    //                if(rowIndex == upIndexBorder)
    //                {
    //                    //rowIndex = upIndexBorder;
    //                    direction = Direction.Right;
    //                    columnIndex++;
    //                    leftIndexBorder++;
    //                }
    //                else rowIndex--;
    //                break;
    //        }
    //    }

    //    return ret;
    //}

    //private enum Direction
    //{
    //    Right = 0,
    //    Down = 1,
    //    Left = 2,
    //    Up = 3
    //}
}
/*


螺旋矩阵
力扣 (LeetCode)
发布于 1 年前
35.6k
方法 1：模拟
直觉

绘制螺旋轨迹路径，我们发现当路径超出界限或者进入之前访问过的单元格时，会顺时针旋转方向。

算法

假设数组有 \text{R}R 行 \text{C}C 列，\text{seen[r][c]}seen[r][c] 表示第 r 行第 c 列的单元格之前已经被访问过了。当前所在位置为 \text{(r, c)}(r, c)，前进方向是 \text{di}di。我们希望访问所有 \text{R}R x \text{C}C 个单元格。

当我们遍历整个矩阵，下一步候选移动位置是 \text{(cr, cc)}(cr, cc)。如果这个候选位置在矩阵范围内并且没有被访问过，那么它将会变成下一步移动的位置；否则，我们将前进方向顺时针旋转之后再计算下一步的移动位置。

class Solution {
    public List<Integer> spiralOrder(int[][] matrix) {
        List ans = new ArrayList();
        if (matrix.length == 0) return ans;
        int R = matrix.length, C = matrix[0].length;
        boolean[][] seen = new boolean[R][C];
        int[] dr = {0, 1, 0, -1};
        int[] dc = {1, 0, -1, 0};
        int r = 0, c = 0, di = 0;
        for (int i = 0; i < R * C; i++) {
            ans.add(matrix[r][c]);
            seen[r][c] = true;
            int cr = r + dr[di];
            int cc = c + dc[di];
            if (0 <= cr && cr < R && 0 <= cc && cc < C && !seen[cr][cc]){
                r = cr;
                c = cc;
            } else {
                di = (di + 1) % 4;
                r += dr[di];
                c += dc[di];
            }
        }
        return ans;
    }
}
复杂度分析

时间复杂度： O(N)O(N)，其中 N 是输入矩阵所有元素的个数。因为我们将矩阵中的每个元素都添加进答案里。
空间复杂度： O(N)O(N)，需要两个矩阵 seen 和 ans 存储所需信息。



方法 2：按层模拟
直觉

答案是最外层所有元素按照顺时针顺序输出，其次是次外层，以此类推。

算法

我们定义矩阵的第 k 层是到最近边界距离为 k 的所有顶点。例如，下图矩阵最外层元素都是第 1 层，次外层元素都是第 2 层，然后是第 3 层的。

[[1, 1, 1, 1, 1, 1, 1],
 [1, 2, 2, 2, 2, 2, 1],
 [1, 2, 3, 3, 3, 2, 1],
 [1, 2, 2, 2, 2, 2, 1],
 [1, 1, 1, 1, 1, 1, 1]]
对于每层，我们从左上方开始以顺时针的顺序遍历所有元素，假设当前层左上角坐标是 \text{(r1, c1)}(r1, c1)，右下角坐标是 \text{(r2, c2)}(r2, c2)。

首先，遍历上方的所有元素 \text{(r1, c)}(r1, c)，按照 \text{c = c1,...,c2}c = c1,...,c2 的顺序。然后遍历右侧的所有元素 \text{(r, c2)}(r, c2)，按照 \text{r = r1+1,...,r2}r = r1+1,...,r2 的顺序。如果这一层有四条边（也就是 \text{r1 < r2}r1 < r2 并且 \text{c1 < c2}c1 < c2 ），我们以下图所示的方式遍历下方的元素和左侧的元素。

54_spiralmatrix.png

class Solution {
    public List < Integer > spiralOrder(int[][] matrix) {
        List ans = new ArrayList();
        if (matrix.length == 0)
            return ans;
        int r1 = 0, r2 = matrix.length - 1;
        int c1 = 0, c2 = matrix[0].length - 1;
        while (r1 <= r2 && c1 <= c2) {
            for (int c = c1; c <= c2; c++) ans.add(matrix[r1][c]);
            for (int r = r1 + 1; r <= r2; r++) ans.add(matrix[r][c2]);
            if (r1 < r2 && c1 < c2) {
                for (int c = c2 - 1; c > c1; c--) ans.add(matrix[r2][c]);
                for (int r = r2; r > r1; r--) ans.add(matrix[r][c1]);
            }
            r1++;
            r2--;
            c1++;
            c2--;
        }
        return ans;
    }
}
复杂度分析

时间复杂度： O(N)O(N)，其中 N 是输入矩阵所有元素的个数。因为我们将矩阵中的每个元素都添加进答案里。
空间复杂度： O(N)O(N)，需要矩阵 ans 存储信息。
下一篇：C++ 详细题解
 
     
*/