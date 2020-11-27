using System.Collections.Generic;

/*
给定一个整数矩阵，找出最长递增路径的长度。

对于每个单元格，你可以往上，下，左，右四个方向移动。 你不能在对角线方向上移动或移动到边界外（即不允许环绕）。

示例 1:

输入: nums =
[
  [9,9,4],
  [6,6,8],
  [2,1,1]
]
输出: 4
解释: 最长递增路径为 [1, 2, 6, 9]。
示例 2:

输入: nums =
[
  [3,4,5],
  [3,2,6],
  [2,2,1]
]
输出: 4
解释: 最长递增路径是 [3, 4, 5, 6]。注意不允许在对角线方向上移动。

*/

/// <summary>
/// https://leetcode-cn.com/problems/longest-increasing-path-in-a-matrix/
/// 329. 矩阵中的最长递增路径
///
///
///
/// </summary>
internal class LongestIncreasingPathInAMatrixSolution
{
    public void Test()
    {
        int[][] nums = new int[3][] { new[] { 9, 9, 4 }, new[] { 6, 6, 8 }, new[] { 2, 1, 1 } };
        var ret = this.LongestIncreasingPath(nums);
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int LongestIncreasingPath(int[][] matrix)
    {
        int[,] dirs = { { -1, 0 }, { 1, 0 }, { 0, -1 }, { 0, 1 } };
        int rows, columns;

        if (matrix == null || matrix.Length == 0 || matrix[0].Length == 0) return 0;
        rows = matrix.Length;
        columns = matrix[0].Length;
        var outdegrees = new int[rows, columns];
        for (int i = 0; i < rows; ++i)
            for (int j = 0; j < columns; ++j)
                for (int d = 0; d < 4; d++)
                {
                    int newRow = i + dirs[d, 0];
                    int newColumn = j + dirs[d, 1];
                    if (-1 < newRow && newRow < rows && -1 < newColumn && newColumn < columns && matrix[i][j] < matrix[newRow][newColumn]) ++outdegrees[i, j];
                }

        Queue<int[]> queue = new Queue<int[]>();
        for (int i = 0; i < rows; ++i)
            for (int j = 0; j < columns; ++j)
                if (outdegrees[i, j] == 0) queue.Enqueue(new int[] { i, j });

        int ret = 0;
        while (0 < queue.Count)
        {
            ++ret;
            int size = queue.Count;
            for (int i = 0; i < size; ++i)
            {
                int[] cell = queue.Dequeue();
                int row = cell[0], column = cell[1];
                for (int d = 0; d < 4; d++)
                {
                    int newRow = row + dirs[d, 0];
                    int newColumn = column + dirs[d, 1];
                    if (-1 < newRow && newRow < rows && -1 < newColumn && newColumn < columns && matrix[newRow][newColumn] < matrix[row][column])
                    {
                        --outdegrees[newRow, newColumn];
                        if (outdegrees[newRow, newColumn] == 0) queue.Enqueue(new int[] { newRow, newColumn });
                    }
                }
            }
        }
        return ret;
    }
}

/*
矩阵中的最长递增路径
力扣官方题解
发布于 2020-07-25
31.1k
方法一：记忆化深度优先搜索
将矩阵看成一个有向图，每个单元格对应图中的一个节点，如果相邻的两个单元格的值不相等，则在相邻的两个单元格之间存在一条从较小值指向较大值的有向边。问题转化成在有向图中寻找最长路径。

深度优先搜索是非常直观的方法。从一个单元格开始进行深度优先搜索，即可找到从该单元格开始的最长递增路径。对每个单元格分别进行深度优先搜索之后，即可得到矩阵中的最长递增路径的长度。

但是如果使用朴素深度优先搜索，时间复杂度是指数级，会超出时间限制，因此必须加以优化。

朴素深度优先搜索的时间复杂度过高的原因是进行了大量的重复计算，同一个单元格会被访问多次，每次访问都要重新计算。由于同一个单元格对应的最长递增路径的长度是固定不变的，因此可以使用记忆化的方法进行优化。用矩阵 \text{memo}memo 作为缓存矩阵，已经计算过的单元格的结果存储到缓存矩阵中。

使用记忆化深度优先搜索，当访问到一个单元格 (i,j)(i,j) 时，如果 \text{memo}[i][j] \neq 0memo[i][j] 

​	
 =0，说明该单元格的结果已经计算过，则直接从缓存中读取结果，如果 \text{memo}[i][j]=0memo[i][j]=0，说明该单元格的结果尚未被计算过，则进行搜索，并将计算得到的结果存入缓存中。

遍历完矩阵中的所有单元格之后，即可得到矩阵中的最长递增路径的长度。


class Solution {
    public int[][] dirs = {{-1, 0}, {1, 0}, {0, -1}, {0, 1}};
    public int rows, columns;

    public int longestIncreasingPath(int[][] matrix) {
        if (matrix == null || matrix.length == 0 || matrix[0].length == 0) {
            return 0;
        }
        rows = matrix.length;
        columns = matrix[0].length;
        int[][] memo = new int[rows][columns];
        int ans = 0;
        for (int i = 0; i < rows; ++i) {
            for (int j = 0; j < columns; ++j) {
                ans = Math.max(ans, dfs(matrix, i, j, memo));
            }
        }
        return ans;
    }

    public int dfs(int[][] matrix, int row, int column, int[][] memo) {
        if (memo[row][column] != 0) {
            return memo[row][column];
        }
        ++memo[row][column];
        for (int[] dir : dirs) {
            int newRow = row + dir[0], newColumn = column + dir[1];
            if (newRow >= 0 && newRow < rows && newColumn >= 0 && newColumn < columns && matrix[newRow][newColumn] > matrix[row][column]) {
                memo[row][column] = Math.max(memo[row][column], dfs(matrix, newRow, newColumn, memo) + 1);
            }
        }
        return memo[row][column];
    }
}
复杂度分析

时间复杂度：O(mn)O(mn)，其中 mm 和 nn 分别是矩阵的行数和列数。深度优先搜索的时间复杂度是 O(V+E)O(V+E)，其中 VV 是节点数，EE 是边数。在矩阵中，O(V)=O(mn)O(V)=O(mn)，O(E)\approx O(4mn) = O(mn)O(E)≈O(4mn)=O(mn)。

空间复杂度：O(mn)O(mn)，其中 mm 和 nn 分别是矩阵的行数和列数。空间复杂度主要取决于缓存和递归调用深度，缓存的空间复杂度是 O(mn)O(mn)，递归调用深度不会超过 mnmn。

方法二：拓扑排序
从方法一可以看到，每个单元格对应的最长递增路径的结果只和相邻单元格的结果有关，那么是否可以使用动态规划求解？

根据方法一的分析，动态规划的状态定义和状态转移方程都很容易得到。方法一中使用的缓存矩阵 \text{memo}memo 即为状态值，状态转移方程如下：

\begin{aligned} & \text{memo}[i][j] = \max\{\text{memo}[x][y]\} + 1 \\ & 其中~(x, y)~与~(i, j)~在矩阵中相邻，并且~\mathrm{matrix}[x][y] > \mathrm{matrix}[i][j] \end{aligned}
​	
  
memo[i][j]=max{memo[x][y]}+1
其中 (x,y) 与 (i,j) 在矩阵中相邻，并且 matrix[x][y]>matrix[i][j]
​	
 

动态规划除了状态定义和状态转移方程，还需要考虑边界情况。这里的边界情况是什么呢？

如果一个单元格的值比它的所有相邻单元格的值都要大，那么这个单元格对应的最长递增路径是 11，这就是边界条件。这个边界条件并不直观，而是需要根据矩阵中的每个单元格的值找到作为边界条件的单元格。

仍然使用方法一的思想，将矩阵看成一个有向图，计算每个单元格对应的出度，即有多少条边从该单元格出发。对于作为边界条件的单元格，该单元格的值比所有的相邻单元格的值都要大，因此作为边界条件的单元格的出度都是 00。

基于出度的概念，可以使用拓扑排序求解。从所有出度为 00 的单元格开始广度优先搜索，每一轮搜索都会遍历当前层的所有单元格，更新其余单元格的出度，并将出度变为 00 的单元格加入下一层搜索。当搜索结束时，搜索的总层数即为矩阵中的最长递增路径的长度。


class Solution {
    public int[][] dirs = {{-1, 0}, {1, 0}, {0, -1}, {0, 1}};
    public int rows, columns;

    public int longestIncreasingPath(int[][] matrix) {
        if (matrix == null || matrix.length == 0 || matrix[0].length == 0) {
            return 0;
        }
        rows = matrix.length;
        columns = matrix[0].length;
        int[][] outdegrees = new int[rows][columns];
        for (int i = 0; i < rows; ++i) {
            for (int j = 0; j < columns; ++j) {
                for (int[] dir : dirs) {
                    int newRow = i + dir[0], newColumn = j + dir[1];
                    if (newRow >= 0 && newRow < rows && newColumn >= 0 && newColumn < columns && matrix[newRow][newColumn] > matrix[i][j]) {
                        ++outdegrees[i][j];
                    }
                }
            }
        }
        Queue<int[]> queue = new LinkedList<int[]>();
        for (int i = 0; i < rows; ++i) {
            for (int j = 0; j < columns; ++j) {
                if (outdegrees[i][j] == 0) {
                    queue.offer(new int[]{i, j});
                }
            }
        }
        int ans = 0;
        while (!queue.isEmpty()) {
            ++ans;
            int size = queue.size();
            for (int i = 0; i < size; ++i) {
                int[] cell = queue.poll();
                int row = cell[0], column = cell[1];
                for (int[] dir : dirs) {
                    int newRow = row + dir[0], newColumn = column + dir[1];
                    if (newRow >= 0 && newRow < rows && newColumn >= 0 && newColumn < columns && matrix[newRow][newColumn] < matrix[row][column]) {
                        --outdegrees[newRow][newColumn];
                        if (outdegrees[newRow][newColumn] == 0) {
                            queue.offer(new int[]{newRow, newColumn});
                        }
                    }
                }
            }
        }
        return ans;
    }
}
复杂度分析

时间复杂度：O(mn)O(mn)，其中 mm 和 nn 分别是矩阵的行数和列数。拓扑排序的时间复杂度是 O(V+E)O(V+E)，其中 VV 是节点数，EE 是边数。在矩阵中，O(V)=O(mn)O(V)=O(mn)，O(E)\approx O(4mn) = O(mn)O(E)≈O(4mn)=O(mn)。

空间复杂度：O(mn)O(mn)，其中 mm 和 nn 分别是矩阵的行数和列数。空间复杂度主要取决于队列，队列中的元素个数不会超过 mnmn。

思考题
为了让大家更好地理解这道题，小编出了四道思考题，欢迎感兴趣的同学在评论区互动哦。

「方法一」中使用了记忆化存储和深度优先搜索，这里的深度优先搜索可以替换成广度优先搜索吗？

「方法二」中基于拓扑排序对排序后的有向无环图做了层次遍历，如果没有拓扑排序直接进行广度优先搜索会发生什么？

「方法二」中如果不使用拓扑排序，而是直接按照矩阵中元素的值从大到小进行排序，并依此顺序进行状态转移，那么可以得到正确的答案吗？如果是从小到大进行排序呢？

「变式」给定一个整数矩阵，找出符合以下条件的路径的数量：这个路径是严格递增的，且它的长度至少是 33。矩阵的边长最大为 10^310 
3
 ，答案对 10^9 + 710 
9
 +7 取模。其他条件和题目相同。思考：是否可以借鉴这道题的方法？
 
 
 public class Solution {
    public int LongestIncreasingPath(int[][] matrix) {
        int rows = matrix.Length;
        if(rows == 0)
            return 0;
        
        var cols = matrix[0].Length;
        
        var dp = new int[rows,cols];
        var result = 0;
        for(int i = 0;i<rows;i++){
            for(int j = 0;j<cols;j++){
                if(dp[i,j] == 0){
                    Helper(i,j,rows,cols,dp,matrix[i][j]-1,matrix);
                }
                
                result = Math.Max(dp[i,j],result);
            }
        }
        
        return result;
    }
    
    private int Helper(int i,int j,int rows,int cols,
                        int[,] dp,int p,int[][] matrix){
        if(i < 0 || j < 0 || i >=rows || j >= cols || matrix[i][j] <= p)
            return 0;
        
        if(dp[i,j] == 0)
        {
            var r = 1;
            r = Math.Max(Helper(i+1,j,rows,cols,dp,matrix[i][j],matrix)+1,r);
            r = Math.Max(Helper(i-1,j,rows,cols,dp,matrix[i][j],matrix)+1,r);
            r = Math.Max(Helper(i,j+1,rows,cols,dp,matrix[i][j],matrix)+1,r);
            r = Math.Max(Helper(i,j-1,rows,cols,dp,matrix[i][j],matrix)+1,r);
            
            dp[i,j] = r;
        }
        
        return dp[i,j];
    }
    
}

public class Solution {
    public int LongestIncreasingPath(int[][] matrix) {
        if (matrix == null || matrix.Length == 0)
            return 0;

        int ans = 0;
        bool[,] visited = new bool[matrix.Length, matrix[0].Length];
        int[,] path = new int[matrix.Length, matrix[0].Length];
        for (int i = 0; i < matrix.Length; ++i)
        {
            for (int j = 0; j < matrix[0].Length; ++j)
                ans = Math.Max(ans, DFS(matrix, int.MinValue, i, j, visited, path));
        }

        return ans;
    }

    private int DFS(int[][] matrix, int val, int row, int col, bool[,] visited, int[,] path)
    {
        if (row < 0 || row >= matrix.Length || col < 0 || col >= matrix[0].Length || visited[row, col] || matrix[row][col] <= val)
            return 0;

        if (path[row, col] != 0)
            return path[row, col];

        visited[row, col] = true;
        int a0 = DFS(matrix, matrix[row][col], row - 1, col, visited, path);
        int a1 = DFS(matrix, matrix[row][col], row + 1, col, visited, path);
        int a2 = DFS(matrix, matrix[row][col], row, col - 1, visited, path);
        int a3 = DFS(matrix, matrix[row][col], row, col + 1, visited, path);
        int max = 1 + Math.Max(a0, Math.Max(a1, Math.Max(a2, a3)));
        path[row, col] = max;
        visited[row, col] = false;

        return max;
    }
}

public class Solution {
        Dictionary<int, int> dict = new Dictionary<int, int>();
        public int LongestIncreasingPath(int[][] matrix)
        {
            int m = matrix.Length;
            if (m == 0)
                return 0;
            int n = matrix[0].Length;
            for (int i = 0; i < m; i++) 
            {
                for (int j = 0; j < n; j++) 
                {
                    Dfs(matrix, i, j);
                }
            }

            int ans = 0;
            foreach (var v in dict.Values)
                ans = Math.Max(ans, v);
            return ans;
        }

        int[] rd = new int[] { -1, 0, 1, 0 };
        int[] cd = new int[] { 0, -1, 0, 1 };
        public int Dfs(int[][] matrix, int i, int j)
        {
            int m = matrix.Length, n = matrix[0].Length;
            int code = i * n + j;
            if (dict.ContainsKey(code))
                return dict[code];

            int path = 1;
            for (int k = 0; k < 4; k++) 
            {
                int newI = i + rd[k];
                int newJ = j + cd[k];
                if (newI < 0 || newI >= m || newJ < 0 || newJ >= n)
                    continue;
                if (matrix[newI][newJ] <= matrix[i][j])
                    continue;
                path = Math.Max(path, 1 + Dfs(matrix, newI, newJ));
            }
            dict[code] = path;
            return path;
        }
}

public class Solution {

    private int[][] path;
    private int[][] map;
    private int maxRow;
    private int maxCol;
    private int ans;
    private int[] dirArr;

    struct Grid {
        public int row;
        public int col;
        public int value;

        public Grid(int row,int col,int value) {
            this.row = row;
            this.col = col;
            this.value = value;
        }
    }

    public int LongestIncreasingPath(int[][] matrix) {
        map = matrix;
        maxRow = matrix.Length;
        if (matrix.Length == 0) {
            return 0;
        }
        maxCol = matrix[0].Length;
        path = new int[maxRow][];
        PriorityQueue<Grid> queue = new PriorityQueue<Grid>((a,b) => {
            return a.value - b.value;
        });
        for (int i=0;i<maxRow;i++) {
            path[i] = new int[maxCol];
            for (int j=0;j<maxCol;j++) {
                path[i][j] = 1;
                Grid grid = new Grid(i,j,matrix[i][j]);
                queue.Push(grid);
            }
        }
        ans = 1;
        dirArr = new int[]{0,-1,0,1,1,0,-1,0};
        while (queue.Count > 0) {
            Grid grid = queue.Pop();
            if (path[grid.row][grid.col] == 1) {
                DFS(grid.row,grid.col,1);
            }
        }
        return ans;
    }

    private void DFS(int row,int col,int pathLen) {
        path[row][col] = pathLen;
        ans = Math.Max(pathLen,ans);
        for (int i=0;i<8;i+=2) {
            int nextRow = row + dirArr[i];
            int nextCol = col + dirArr[i+1];
            if (nextRow < 0 || nextRow >= maxRow || nextCol < 0 || nextCol >= maxCol) {
                continue;
            }
            if (map[nextRow][nextCol] <= map[row][col] || path[nextRow][nextCol] >= pathLen+1) {
                continue;
            }
            DFS(nextRow,nextCol,pathLen+1);
        }
    }

    public class PriorityQueue<T>
    {
        private T[] _heap;
        private int _capacity;
        private int _count;
        private IComparer<T> _comparer;
        private T _defaultValue;

        internal sealed class MyComparer<W> : IComparer<W>
        {
            private Comparison<W> _comparision;

            public MyComparer(Comparison<W> comparision)
            {
                _comparision = comparision;
            }

            public int Compare(W x, W y)
            {
                return _comparision(x, y);
            }
        }

        public PriorityQueue()
            : this(16)
        {

        }

        public PriorityQueue(int capacity)
            : this(capacity, null)
        {

        }

        public PriorityQueue(IComparer<T> comparer)
            : this(16, comparer)
        {

        }

        public PriorityQueue(Comparison<T> comparision)
            : this(16, new MyComparer<T>(comparision))
        {

        }

        public PriorityQueue(int capacity, IComparer<T> comparer)
        {
            _capacity = capacity;
            _heap = new T[_capacity];
            _count = 0;
            _defaultValue = default(T);
            _comparer = comparer == null ? Comparer<T>.Default : comparer;
        }

        public PriorityQueue(T[] arr, IComparer<T> comparer = null)
        {
            _capacity = 16;
            _comparer = comparer == null ? Comparer<T>.Default : comparer;
            if (arr.Length <= 1)
            {
                _heap = new T[_capacity];
                _count = arr.Length;
                if (_count == 1)
                {
                    _heap[0] = arr[0];
                }
                return;
            }
            _count = arr.Length;
            while (_capacity < _count)
            {
                _capacity <<= 1;
            }
            _heap = new T[_capacity];
            Array.Copy(arr, _heap, arr.Length);
            for (int k = (_count - 2) >> 1; k >= 0; k--)
            {
                SiftDown(k);
            }
            _defaultValue = default(T);
        }

        public PriorityQueue(T[] arr, Comparison<T> comparision)
            : this(arr, new MyComparer<T>(comparision))
        {

        }

        public void Push(T item)
        {
            if (_count >= _capacity)
            {
                _capacity <<= 1;
                Array.Resize(ref _heap, _capacity);
            }
            _heap[_count] = item;
            SiftUp(_count++);
        }

        public T Pop()
        {
            if (_count == 0)
                throw new System.Exception("PriorityQueue is empty!");
            T ret = _heap[0];
            _heap[0] = _heap[--_count];
            _heap[_count] = _defaultValue;
            SiftDown(0);
            return ret;
        }

        public T Top()
        {
            if (_count > 0)
                return _heap[0];
            throw new System.Exception("PriorityQueue is empty!");
        }

        private void SiftUp(int index)
        {
            T tmp;
            int tmpIndex;
            while (index > 0)
            {
                tmpIndex = (index - 1) >> 1;
                if (_comparer.Compare(_heap[index], _heap[tmpIndex]) < 0)
                {
                    tmp = _heap[index];
                    _heap[index] = _heap[tmpIndex];
                    _heap[tmpIndex] = tmp;
                    index = tmpIndex;
                }
                else
                {
                    break;
                }
            }
        }

        private void SiftDown(int index)
        {
            T tmp = _heap[index];
            for (int tmpIndex = (index << 1) + 1; tmpIndex < _count; index = tmpIndex, tmpIndex = (tmpIndex << 1) + 1)
            {
                if (tmpIndex + 1 < _count && _comparer.Compare(_heap[tmpIndex], _heap[tmpIndex + 1]) > 0)
                {
                    tmpIndex++;
                }
                if (_comparer.Compare(tmp, _heap[tmpIndex]) <= 0)
                {
                    break;
                }
                _heap[index] = _heap[tmpIndex];
            }
            _heap[index] = tmp;
        }

        public bool IsEmpty()
        {
            return _count == 0;
        }

        public int Count
        {
            get { return _count; }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < _count; i++)
            {
                sb.Append(_heap[i] + ",");
            }
            return sb.ToString();
        }
    }
}


*/