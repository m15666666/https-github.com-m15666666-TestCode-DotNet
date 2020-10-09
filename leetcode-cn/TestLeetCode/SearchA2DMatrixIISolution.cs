/*
编写一个高效的算法来搜索 m x n 矩阵 matrix 中的一个目标值 target。该矩阵具有以下特性：

每行的元素从左到右升序排列。
每列的元素从上到下升序排列。
示例:

现有矩阵 matrix 如下：

[
  [1,   4,  7, 11, 15],
  [2,   5,  8, 12, 19],
  [3,   6,  9, 16, 22],
  [10, 13, 14, 17, 24],
  [18, 21, 23, 26, 30]
]
给定 target = 5，返回 true。

给定 target = 20，返回 false。

*/

/// <summary>
/// https://leetcode-cn.com/problems/search-a-2d-matrix-ii/
/// 240. 搜索二维矩阵 II
/// 编写一个高效的算法来搜索 m x n 矩阵 matrix 中的一个目标值 target。该矩阵具有以下特性：
/// 每行的元素从左到右升序排列。
/// 每列的元素从上到下升序排列。
/// 示例:
/// 现有矩阵 matrix 如下：
/// [
/// [1,   4,  7, 11, 15],
/// [2,   5,  8, 12, 19],
/// [3,   6,  9, 16, 22],
/// [10, 13, 14, 17, 24],
/// [18, 21, 23, 26, 30]
/// ]
/// 给定 target = 5，返回 true。
/// 给定 target = 20，返回 false。
/// </summary>
internal class SearchA2DMatrixIISolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public bool SearchMatrix(int[,] matrix, int target)
    {
        if (matrix == null || matrix.Length == 0) return false;
        int m = matrix.GetLength(0);
        int n = matrix.GetLength(1);

        return Search(0, 0, n - 1, m - 1);

        bool Search(int left, int up, int right, int down)
        {
            if (left > right || up > down) return false;
            else if (target < matrix[up, left] || target > matrix[down, right]) return false;
            
            if(up == down)
            {
                while (left <= right)
                {
                    int mid = left + (right - left) / 2;
                    var v = matrix[up, mid];
                    if (v == target) return true;
                    if (v < target) left = mid + 1;
                    else right = mid - 1;
                }
                return false;
            }

            int midColumn = left + (right - left) / 2;
            int u = up, d = down;

            while (u <= d)
            {
                int row = u + (d - u) / 2;
                var v = matrix[row, midColumn];
                if (v == target) return true;
                if (v < target) u = row + 1;
                else d = row - 1;
            }

            return Search(left, d + 1, midColumn - 1, down) || Search(midColumn + 1, up, right, d);
        }
    }

    //public bool SearchMatrix(int[,] matrix, int target)
    //{
    //    if (matrix == null || matrix.Length == 0) return false;

    //    int m = matrix.GetLength(0);
    //    int n = matrix.GetLength(1);

    //    int i = m - 1;
    //    int j = 0;
    //    while ( -1 < i && j < n )
    //    {
    //        var v = matrix[i, j];
    //        if (target == v) return true;
    //        else if (target > v) j++;
    //        else i--;
    //    }
    //    return false;
    //}
}

/*
搜索二维矩阵 II
力扣 (LeetCode)
发布于 2019-06-30
44.0k
方法一：暴力法
对于每一行我们可以像搜索未排序的一维数组——通过检查每个元素来判断是否有目标值。

算法：
这个算法并没有做到聪明的事情。我们循环数组，依次检查每个元素。如果，我们找到了，我们返回 true。否则，对于搜索到末尾都没有返回的循环，我们返回 false。此算法在所有情况下都是正确的答案，因为我们耗尽了整个搜索空间。


class Solution {
    public boolean searchMatrix(int[][] matrix, int target) {
        for (int i = 0; i < matrix.length; i++) {
            for (int j = 0; j < matrix[0].length; j++) {
                if (matrix[i][j] == target) {
                    return true;
                }
            }
        }

        return false;
    }
}
复杂度分析

时间复杂度：O(mn)O(mn)。因为我们在 n\times mn×m 矩阵上操作，总的时间复杂度为矩阵的大小
空间复杂度：O(1)O(1)，暴力法分配的额外空间不超过少量指针，因此内存占用是恒定的。
方法二：二分法搜索
矩阵已经排过序，就需要使用二分法搜索以加快我们的算法。

算法：
首先，我们确保矩阵不为空。那么，如果我们迭代矩阵对角线，从当前元素对列和行搜索，我们可以保持从当前 (row,col)(row,col) 对开始的行和列为已排序。 因此，我们总是可以二分搜索这些行和列切片。我们以如下逻辑的方式进行 : 在对角线上迭代，二分搜索行和列，直到对角线的迭代元素用完为止（意味着我们可以返回 false ）或者找到目标（意味着我们可以返回 true ）。binary search 函数的工作原理和普通的二分搜索一样,但需要同时搜索二维数组的行和列。


class Solution {
    private boolean binarySearch(int[][] matrix, int target, int start, boolean vertical) {
        int lo = start;
        int hi = vertical ? matrix[0].length-1 : matrix.length-1;

        while (hi >= lo) {
            int mid = (lo + hi)/2;
            if (vertical) { // searching a column
                if (matrix[start][mid] < target) {
                    lo = mid + 1;
                } else if (matrix[start][mid] > target) {
                    hi = mid - 1;
                } else {
                    return true;
                }
            } else { // searching a row
                if (matrix[mid][start] < target) {
                    lo = mid + 1;
                } else if (matrix[mid][start] > target) {
                    hi = mid - 1;
                } else {
                    return true;
                }
            }
        }

        return false;
    }

    public boolean searchMatrix(int[][] matrix, int target) {
        // an empty matrix obviously does not contain `target`
        if (matrix == null || matrix.length == 0) {
            return false;
        }

        // iterate over matrix diagonals
        int shorterDim = Math.min(matrix.length, matrix[0].length);
        for (int i = 0; i < shorterDim; i++) {
            boolean verticalFound = binarySearch(matrix, target, i, true);
            boolean horizontalFound = binarySearch(matrix, target, i, false);
            if (verticalFound || horizontalFound) {
                return true;
            }
        }
        
        return false; 
    }
}
复杂度分析

时间复杂度：O(lg(n!))O(lg(n!))。
这个算法产生的时间复杂度并不是特别明显的是 O(lg(n!))O(lg(n!)) ，所以让我们一步一步地分析它。在主循环中执行的工作量逐渐最大，它运行 min(m,n)min(m,n) 次迭代，其中 mm 表示行数，nn 表示列数。在每次迭代中，我们对长度为 m-im−i 和 n-in−i 的数组片执行两次二分搜索。因此，循环的每一次迭代都以 O(lg(m-i)+lg(n-i))O(lg(m−i)+lg(n−i)) 时间运行，其中 ii 表示当前迭代。我们可以将其简化为 O(2 \cdot lg(n-i))= O(lg(n-i))O(2⋅lg(n−i))=O(lg(n−i)) 在最坏的情况是 n\approx mn≈m 。当 n \ll mn≪m 时会发生什么（不损失一般性）；nn 将在渐近分析中占主导地位。通过汇总所有迭代的运行时间，我们得到以下表达式：
\quad {O}(lg(n) + lg(n-1) + lg(n-2) + \ldots + lg(1))
O(lg(n)+lg(n−1)+lg(n−2)+…+lg(1))

然后，我们可以利用对数乘法规则（lg(a)+lg(b)=lg(ab)lg(a)+lg(b)=lg(ab)）将复杂度改写为：

{O}(lg(n) + lg(n-1) + lg(n-2) + \ldots + lg(1))O(lg(n)+lg(n−1)+lg(n−2)+…+lg(1))
={O}(lg(n \cdot (n-1) \cdot (n-2) \cdot \ldots \cdot 1))=O(lg(n⋅(n−1)⋅(n−2)⋅…⋅1))
= {O}(lg(1 \cdot \ldots \cdot (n-2) \cdot (n-1) \cdot n))=O(lg(1⋅…⋅(n−2)⋅(n−1)⋅n))
= {O}(lg(n!))=O(lg(n!))

空间复杂度：O(1)O(1)，因为我们的二分搜索实现并没有真正地切掉矩阵中的行和列的副本，我们可以避免分配大于常量内存。
方法三：搜索空间的缩减
我们可以将已排序的二维矩阵划分为四个子矩阵，其中两个可能包含目标，其中两个肯定不包含。

算法：
由于该算法是递归操作的，因此可以通过它的基本情况和递归情况的正确性来判断它的正确性。

基本情况 ：
对于已排序的二维数组，有两种方法可以确定一个任意元素目标是否可以用常数时间判断。第一，如果数组的区域为零，则它不包含元素，因此不能包含目标。其次，如果目标小于数组的最小值或大于数组的最大值，那么矩阵肯定不包含目标值。

递归情况：
如果目标值包含在数组内，因此我们沿着索引行的矩阵中间列 ，matrix[row-1][mid] < target < matrix[row][mid]matrix[row−1][mid]<target<matrix[row][mid] ，（很明显，如果我们找到 target ，我们立即返回 true）。现有的矩阵可以围绕这个索引分为四个子矩阵；左上和右下子矩阵不能包含目标（通过基本情况部分来判断），所以我们可以从搜索空间中删除它们 。另外，左下角和右上角的子矩阵是二维矩阵，因此我们可以递归地将此算法应用于它们。


class Solution {
    private int[][] matrix;
    private int target;

    private boolean searchRec(int left, int up, int right, int down) {
        // this submatrix has no height or no width.
        if (left > right || up > down) {
            return false;
        // `target` is already larger than the largest element or smaller
        // than the smallest element in this submatrix.
        } else if (target < matrix[up][left] || target > matrix[down][right]) {
            return false;
        }

        int mid = left + (right-left)/2;

        // Locate `row` such that matrix[row-1][mid] < target < matrix[row][mid]
        int row = up;
        while (row <= down && matrix[row][mid] <= target) {
            if (matrix[row][mid] == target) {
                return true;
            }
            row++;
        }

        return searchRec(left, row, mid-1, down) || searchRec(mid+1, up, right, row-1);
    }

    public boolean searchMatrix(int[][] mat, int targ) {
        // cache input values in object to avoid passing them unnecessarily
        // to `searchRec`
        matrix = mat;
        target = targ;

        // an empty matrix obviously does not contain `target`
        if (matrix == null || matrix.length == 0) {
            return false;
        }

        return searchRec(0, 0, matrix[0].length-1, matrix.length-1);
    }
}
复杂度分析

时间复杂度：O(nlgn)O(nlgn)。
首先，为了便于分析，假设 n \approx mn≈m 如方法 2 的分析中所述。另外，指定 x=n^2=matrixx=n 
2
 =matrix；这将使主方法更容易应用。现在，让我们将分治方法的运行时建模为循环关系：
T(x) = 2 \cdot T(\frac{x}{4}) + \sqrt{x}
T(x)=2⋅T( 
4
x
​	
 )+ 
x
​	
 

第一个式子(2\cdot t(\frac x 4)2⋅t( 
4
x
​	
 ))源于我们在大约为原矩阵的四分之一大小的两个子矩阵上递归，而 \sqrt x 
x
​	
  源于沿着 O(n)O(n)长度列查找所花费的时间。假设变量（A=2;B=4;C=0.5A=2;B=4;C=0.5）之后，我们注意到 \log_b{a}=clog 
b
​	
 a=c。因此，这种情况属于方法二，并出现以下情况：

T(x)T(x)
= {O}(x^c \cdot lgx)=O(x 
c
 ⋅lgx)
= {O}(x^{0.5} \cdot lgx)=O(x 
0.5
 ⋅lgx)
= {O}((n^2)^{0.5} \cdot lg(n^2))=O((n 
2
 ) 
0.5
 ⋅lg(n 
2
 ))
= {O}(n \cdot lg(n^2))=O(n⋅lg(n 
2
 ))
= {O}(2n \cdot lgn)=O(2n⋅lgn)
= {O}(n \cdot lgn)=O(n⋅lgn)

扩展：如果我们用二分搜索分割点，而不是用线性扫描，那么复杂度会怎样呢？

空间复杂度：O(lgn)O(lgn)，虽然这种方法从根本上不需要大于常量的附加内存，但是它使用递归意味着它将使用与其递归树高度成比例的内存。因为这种方法丢弃了每一级递归一半的矩阵（并进行了两次递归调用），所以树的高度由 lgnlgn 限定。
方法四：
因为矩阵的行和列是排序的（分别从左到右和从上到下），所以在查看任何特定值时，我们可以修剪O(m)O(m)或O(n)O(n)元素。

算法：
首先，我们初始化一个指向矩阵左下角的 (row，col)(row，col) 指针。然后，直到找到目标并返回 true（或者指针指向矩阵维度之外的 (row，col)(row，col) 为止，我们执行以下操作：如果当前指向的值大于目标值，则可以 “向上” 移动一行。 否则，如果当前指向的值小于目标值，则可以移动一列。不难理解为什么这样做永远不会删减正确的答案；因为行是从左到右排序的，所以我们知道当前值右侧的每个值都较大。 因此，如果当前值已经大于目标值，我们知道它右边的每个值会比较大。也可以对列进行非常类似的论证，因此这种搜索方式将始终在矩阵中找到目标（如果存在）。

在下面的动画中查看算法的一些示例运行：




class Solution {
    public boolean searchMatrix(int[][] matrix, int target) {
        // start our "pointer" in the bottom-left
        int row = matrix.length-1;
        int col = 0;

        while (row >= 0 && col < matrix[0].length) {
            if (matrix[row][col] > target) {
                row--;
            } else if (matrix[row][col] < target) {
                col++;
            } else { // found it
                return true;
            }
        }

        return false;
    }
}
复杂度分析

时间复杂度：O(n+m)O(n+m)。
时间复杂度分析的关键是注意到在每次迭代（我们不返回 true）时，行或列都会精确地递减/递增一次。由于行只能减少 mm 次，而列只能增加 nn 次，因此在导致 while 循环终止之前，循环不能运行超过 n+mn+m 次。因为所有其他的工作都是常数，所以总的时间复杂度在矩阵维数之和中是线性的。
空间复杂度：O(1)O(1)，因为这种方法只处理几个指针，所以它的内存占用是恒定的。

public class Solution {
    public bool SearchMatrix(int[,] matrix, int target) {
        for (int i = 0, j = matrix.GetLength(1) - 1; j >= 0 && i < matrix.GetLength(0);)
            {
                if (matrix[i, j] == target)
                {
                    return true;
                }
                else if (matrix[i, j] > target)
                {
                    j--;
                }
                else
                {
                    i++;
                }
            }
            return false;
    }
}


public class Solution {
    public bool SearchMatrix(int[,] matrix, int target) {
        int n = matrix.GetLength(0), m = matrix.GetLength(1);
        if (n == 0 || m == 0)
        {
            return false;
        }
        int x = 0, y = m -1;

        while (x < n && y >= 0)
        {
            if (matrix[x, y] < target)
            {
                x++;
            }
            else if (matrix[x, y] > target)
            {
                y--;
            }
            else
                return true;
        }
        return false;
    }
}

*/