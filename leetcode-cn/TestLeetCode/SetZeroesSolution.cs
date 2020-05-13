using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个 m x n 的矩阵，如果一个元素为 0，则将其所在行和列的所有元素都设为 0。请使用原地算法。

示例 1:

输入: 
[
  [1,1,1],
  [1,0,1],
  [1,1,1]
]
输出: 
[
  [1,0,1],
  [0,0,0],
  [1,0,1]
]
示例 2:

输入: 
[
  [0,1,2,0],
  [3,4,5,2],
  [1,3,1,5]
]
输出: 
[
  [0,0,0,0],
  [0,4,5,0],
  [0,3,1,0]
]
进阶:

一个直接的解决方案是使用  O(mn) 的额外空间，但这并不是一个好的解决方案。
一个简单的改进方案是使用 O(m + n) 的额外空间，但这仍然不是最好的解决方案。
你能想出一个常数空间的解决方案吗？

*/
/// <summary>
/// https://leetcode-cn.com/problems/set-matrix-zeroes/
/// 73.矩阵置零
/// 
/// https://www.cnblogs.com/ariel-dreamland/p/9154179.html
/// </summary>
class SetZeroesSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public void SetZeroes(int[][] matrix)
    {
        if (matrix == null) return;

        int m = matrix.Length;
        int n = matrix[0].Length;

        if (m < 1 || n < 1) return;

        bool zeroFirstRow = matrix[0][0] == 0 ? true : false;
        if(!zeroFirstRow)
        for ( int i = 0; i < n; i++ )
            if(matrix[0][i] == 0)
            {
                zeroFirstRow = true;
                break;
            }

        bool zeroFirstColumn = matrix[0][0] == 0 ? true : false;
        if(!zeroFirstColumn)
        for (int i = 0; i < m; i++)
            if (matrix[i][0] == 0)
            {
                zeroFirstColumn = true;
                break;
            }

        for( var i = 1; i < m; i++)
            for(var j = 1; j < n; j++) 
                if( matrix[i][j] == 0 )
                {
                    matrix[0][j] = 0;
                    matrix[i][0] = 0;
                }
        for (var i = 1; i < m; i++)
            for (var j = 1; j < n; j++)
                if (matrix[0][j] == 0 || matrix[i][0] == 0)
                {
                    matrix[i][j] = 0;
                }

        if (zeroFirstRow)
            for (int i = 0; i < n; i++)
                matrix[0][i] = 0;

        if (zeroFirstColumn)
            for (int i = 0; i < m; i++)
                matrix[i][0] = 0;
    }
    //public void SetZeroes(int[,] matrix)
    //{
    //    if (matrix == null) return;

    //    int m = matrix.GetLength(0);
    //    int n = matrix.GetLength(1);

    //    if (m < 1 || n < 1) return;

    //    bool zeroFirstRow = false;
    //    for ( int i = 0; i < n; i++ )
    //        if(matrix[0,i] == 0)
    //        {
    //            zeroFirstRow = true;
    //            break;
    //        }

    //    bool zeroFirstColumn = false;
    //    for (int i = 0; i < m; i++)
    //        if (matrix[i, 0] == 0)
    //        {
    //            zeroFirstColumn = true;
    //            break;
    //        }

    //    for( var i = 1; i < m; i++)
    //        for(var j = 1; j < n; j++) 
    //            if( matrix[i, j] == 0 )
    //            {
    //                matrix[0, j] = 0;
    //                matrix[i, 0] = 0;
    //            }
    //    for (var i = 1; i < m; i++)
    //        for (var j = 1; j < n; j++)
    //            if (matrix[0, j] == 0 || matrix[i, 0] == 0)
    //            {
    //                matrix[i, j] = 0;
    //            }

    //    if (zeroFirstRow)
    //        for (int i = 0; i < n; i++)
    //            matrix[0, i] = 0;

    //    if (zeroFirstColumn)
    //        for (int i = 0; i < m; i++)
    //            matrix[i, 0] = 0;
    //}
}
/*

矩阵置零
力扣 (LeetCode)
发布于 1 年前
23.2k
这个问题看上去相当简单，但是需要我们原地更新矩阵，也就是空间复杂度要求是 O(1)O(1)。

我们将用三种不同方法解决这个问题，第一个方法需要额外的存储空间而后两个不用。

方法 1：额外存储空间方法
想法

如果矩阵中任意一个格子有零我们就记录下它的行号和列号，这些行和列的所有格子在下一轮中全部赋为零。

算法

我们扫描一遍原始矩阵，找到所有为零的元素。
如果我们找到 [i, j] 的元素值为零，我们需要记录下行号 i 和列号 j。
用两个 sets ，一个记录行信息一个记录列信息。
if cell[i][j] == 0 {
    row_set.add(i)
    column_set.add(j)
}
最后，我们迭代原始矩阵，对于每个格子检查行 r 和列 c 是否被标记过，如果是就将矩阵格子的值设为 0。
if r in row_set or c in column_set {
    cell[r][c] = 0
}
class Solution {
  public void setZeroes(int[][] matrix) {
    int R = matrix.length;
    int C = matrix[0].length;
    Set<Integer> rows = new HashSet<Integer>();
    Set<Integer> cols = new HashSet<Integer>();

    // Essentially, we mark the rows and columns that are to be made zero
    for (int i = 0; i < R; i++) {
      for (int j = 0; j < C; j++) {
        if (matrix[i][j] == 0) {
          rows.add(i);
          cols.add(j);
        }
      }
    }

    // Iterate over the array once again and using the rows and cols sets, update the elements.
    for (int i = 0; i < R; i++) {
      for (int j = 0; j < C; j++) {
        if (rows.contains(i) || cols.contains(j)) {
          matrix[i][j] = 0;
        }
      }
    }
  }
}
复杂度分析

时间复杂度：O(M \times N)O(M×N)，其中 MM 和 NN 分别对应行数和列数。
空间复杂度：O(M+N)O(M+N)。
方法 2：O(1)空间的暴力
想法

在上面的方法中我们利用额外空间去记录需要置零的行号和列号，通过修改原始矩阵可以避免额外空间的消耗。

算法

遍历原始矩阵，如果发现如果某个元素 cell[i][j] 为 0，我们将第 i 行和第 j 列的所有非零元素设成很大的负虚拟值（比如说 -1000000）。注意，正确的虚拟值取值依赖于问题的约束，任何允许值范围外的数字都可以作为虚拟之。
最后，我们便利整个矩阵将所有等于虚拟值（常量在代码中初始化为 MODIFIED）的元素设为 0。
class Solution {
  public void setZeroes(int[][] matrix) {
    int MODIFIED = -1000000;
    int R = matrix.length;
    int C = matrix[0].length;

    for (int r = 0; r < R; r++) {
      for (int c = 0; c < C; c++) {
        if (matrix[r][c] == 0) {
          // We modify the corresponding rows and column elements in place.
          // Note, we only change the non zeroes to MODIFIED
          for (int k = 0; k < C; k++) {
            if (matrix[r][k] != 0) {
              matrix[r][k] = MODIFIED;
            }
          }
          for (int k = 0; k < R; k++) {
            if (matrix[k][c] != 0) {
              matrix[k][c] = MODIFIED;
            }
          }
        }
      }
    }

    for (int r = 0; r < R; r++) {
      for (int c = 0; c < C; c++) {
        // Make a second pass and change all MODIFIED elements to 0 """
        if (matrix[r][c] == MODIFIED) {
          matrix[r][c] = 0;
        }
      }
    }
  }
}
复杂度分析

时间复杂度：O((M \times N) \times (M + N))O((M×N)×(M+N))，其中 MM 和 NN 分别对应行数和列数。尽管这个方法避免了使用额外空间，但是效率很低，因为最坏情况下每个元素都为零我们需要访问所有的行和列，因此所有 (M \times N)(M×N) 个格子都需要访问 (M + N)(M+N) 个格子并置零。
空间复杂度：O(1)O(1)
想法

第二种方法不高效的地方在于我们会重复对同一行或者一列赋零。我们可以推迟对行和列赋零的操作。

我们可以用每行和每列的第一个元素作为标记，这个标记用来表示这一行或者这一列是否需要赋零。这意味着对于每个节点不需要访问 M+NM+N 个格子而是只需要对标记点的两个格子赋值。

if cell[i][j] == 0 {
    cell[i][0] = 0
    cell[0][j] = 0
}
这些标签用于之后对矩阵的更新，如果某行的第一个元素为零就将整行置零，如果某列的第一个元素为零就将整列置零。

算法

遍历整个矩阵，如果 cell[i][j] == 0 就将第 i 行和第 j 列的第一个元素标记。
第一行和第一列的标记是相同的，都是 cell[0][0]，所以需要一个额外的变量告知第一列是否被标记，同时用 cell[0][0] 继续表示第一行的标记。
然后，从第二行第二列的元素开始遍历，如果第 r 行或者第 c 列被标记了，那么就将 cell[r][c] 设为 0。这里第一行和第一列的作用就相当于方法一中的 row_set 和 column_set 。
然后我们检查是否 cell[0][0] == 0 ，如果是则赋值第一行的元素为零。
然后检查第一列是否被标记，如果是则赋值第一列的元素为零。


73-1.png

通过上述操作得到的标记，并将标记的对于行列元素赋值为零。

73-2.png

class Solution {
  public void setZeroes(int[][] matrix) {
    Boolean isCol = false;
    int R = matrix.length;
    int C = matrix[0].length;

    for (int i = 0; i < R; i++) {

      // Since first cell for both first row and first column is the same i.e. matrix[0][0]
      // We can use an additional variable for either the first row/column.
      // For this solution we are using an additional variable for the first column
      // and using matrix[0][0] for the first row.
      if (matrix[i][0] == 0) {
        isCol = true;
      }

      for (int j = 1; j < C; j++) {
        // If an element is zero, we set the first element of the corresponding row and column to 0
        if (matrix[i][j] == 0) {
          matrix[0][j] = 0;
          matrix[i][0] = 0;
        }
      }
    }

    // Iterate over the array once again and using the first row and first column, update the elements.
    for (int i = 1; i < R; i++) {
      for (int j = 1; j < C; j++) {
        if (matrix[i][0] == 0 || matrix[0][j] == 0) {
          matrix[i][j] = 0;
        }
      }
    }

    // See if the first row needs to be set to zero as well
    if (matrix[0][0] == 0) {
      for (int j = 0; j < C; j++) {
        matrix[0][j] = 0;
      }
    }

    // See if the first column needs to be set to zero as well
    if (isCol) {
      for (int i = 0; i < R; i++) {
        matrix[i][0] = 0;
      }
    }
  }
}
复杂度分析

时间复杂度：O(M \times N)O(M×N)
空间复杂度：O(1)O(1)
下一篇：记录所有零点的坐标，然后清空

 
     
     
*/