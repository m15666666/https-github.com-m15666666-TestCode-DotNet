/// <summary>
/// https://leetcode-cn.com/problems/surrounded-regions/
/// 130.被围绕的区域
/// 给定一个二维的矩阵，包含 'X' 和 'O'（字母 O）。
/// 找到所有被 'X' 围绕的区域，并将这些区域里所有的 'O' 用 'X' 填充。
/// 示例:
/// X X X X
/// X O O X
/// X X O X
/// X O X X
/// </summary>
internal class SolveSurroundedRegionsSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public void Solve(char[][] board)
    {
        if (board == null || board.Length == 0) return;

        const char O = 'O';
        const char X = 'X';

        int rows = board.Length;
        int cols = board[0].Length;

        UnionFind uf = new UnionFind(rows * cols + 1);
        int dummyNode = rows * cols;

        for (int i = 0; i < rows; i++)
            for (int j = 0; j < cols; j++)
            {
                if (board[i][j] == X) continue;

                if (i == 0 || i == rows - 1 || j == 0 || j == cols - 1)
                {
                    uf.Union(Node(i, j), dummyNode);
                    //continue;
                }
                if (0 < i && board[i - 1][j] == O) uf.Union(Node(i, j), Node(i - 1, j));
                //if (i < rows - 1 && board[i + 1][j] == O) uf.Union(Node(i, j), Node(i + 1, j));
                if (0 < j && board[i][j - 1] == O) uf.Union(Node(i, j), Node(i, j - 1));
                //if (j < cols - 1 && board[i][j + 1] == O) uf.Union(Node(i, j), Node(i, j + 1));
            }

        for (int i = 0; i < rows; i++)
            for (int j = 0; j < cols; j++)
            {
                if (board[i][j] == X) continue;
                if (!uf.IsConnected(Node(i, j), dummyNode)) board[i][j] = X;
            }

        int Node(int i, int j)
        {
            return i * cols + j;
        }
    }

    public class UnionFind
    {
        private int[] parents;

        public UnionFind(int totalNodes)
        {
            parents = new int[totalNodes];
            for (int i = 0; i < totalNodes; i++)
            {
                parents[i] = i;
            }
        }

        public void Union(int node1, int node2)
        {
            int root1 = Find(node1);
            int root2 = Find(node2);
            if (root1 != root2)
            {
                parents[root2] = root1;
            }
        }

        public int Find(int node)
        {
            while (parents[node] != node)
            {
                parents[node] = parents[parents[node]];
                node = parents[node];
            }

            return node;
        }

        public bool IsConnected(int node1, int node2)
        {
            return Find(node1) == Find(node2);
        }
    }

    //public void Solve(char[,] board)
    //{
    //    if (board == null || board.Length < 9) return;

    //    const char X = 'X';
    //    const char O = 'O';
    //    const char smallO = 'o';

    //    var m = board.GetLength(0);
    //    var n = board.GetLength(1);

    //    int left = 0;
    //    int right = n - 1;
    //    int top = 0;
    //    int bottom = m - 1;

    //    Queue<Position2D> queue = new Queue<Position2D>();

    //    while( left < right - 1 && top < bottom - 1 )
    //    {
    //        {
    //            int rowIndex = top + 1;
    //            for (int columnIndex = left + 1; columnIndex < right; columnIndex++)
    //            {
    //                var v = board[rowIndex, columnIndex];
    //                if (v == O)
    //                {
    //                    if (board[rowIndex - 1, columnIndex] == O || (board[rowIndex, columnIndex - 1] == O) || (columnIndex == right - 1 && board[rowIndex, right] == O))
    //                    {
    //                        // do nothing
    //                        continue;
    //                    }
    //                    board[rowIndex, columnIndex] = smallO;
    //                    queue.Enqueue(new Position2D { X = rowIndex, Y = columnIndex });
    //                }
    //            }
    //            top++;
    //            if (!(left < right - 1 && top < bottom - 1)) break;
    //        }

    //        {
    //            int rowIndex = bottom - 1;
    //            for (int columnIndex = left + 1; columnIndex < right; columnIndex++)
    //            {
    //                var v = board[rowIndex, columnIndex];
    //                if (v == O)
    //                {
    //                    if (board[rowIndex + 1, columnIndex] == O || (board[rowIndex, columnIndex - 1] == O) || (columnIndex == right - 1 && board[rowIndex, right] == O))
    //                    {
    //                        // do nothing
    //                        continue;
    //                    }
    //                    board[rowIndex, columnIndex] = smallO;
    //                    queue.Enqueue(new Position2D { X = rowIndex, Y = columnIndex });
    //                }
    //            }
    //            bottom--;
    //            if (!(left < right - 1 && top < bottom - 1)) break;
    //        }

    //        {
    //            int columnIndex = left + 1;
    //            for (int rowIndex = top + 1; rowIndex < bottom; rowIndex++)
    //            {
    //                var v = board[rowIndex, columnIndex];
    //                if (v == O)
    //                {
    //                    if (board[rowIndex, columnIndex - 1] == O || (board[rowIndex - 1,columnIndex] == O) || (rowIndex == bottom - 1 && board[bottom,columnIndex] == O))
    //                    {
    //                        // do nothing
    //                        continue;
    //                    }
    //                    board[rowIndex, columnIndex] = smallO;
    //                    queue.Enqueue(new Position2D { X = rowIndex, Y = columnIndex });
    //                }
    //            }
    //            left++;
    //            if (!(left < right - 1 && top < bottom - 1)) break;
    //        }

    //        {
    //            int columnIndex = right - 1;
    //            for (int rowIndex = top + 1; rowIndex < bottom; rowIndex++)
    //            {
    //                var v = board[rowIndex, columnIndex];
    //                if (v == O)
    //                {
    //                    if (board[rowIndex, columnIndex + 1] == O || (board[rowIndex - 1, columnIndex] == O) || (rowIndex == bottom - 1 && board[bottom, columnIndex] == O))
    //                    {
    //                        // do nothing
    //                        continue;
    //                    }
    //                    board[rowIndex, columnIndex] = smallO;
    //                    queue.Enqueue(new Position2D { X = rowIndex, Y = columnIndex });
    //                }
    //            }
    //            right--;
    //        }
    //    } // while( left < right - 1 && top < bottom - 1 )
    //    if (queue.Count == 0) return;

    //    int maxCount = queue.Count;
    //    int iterateCount = 0;
    //    while( iterateCount < maxCount )
    //    {
    //        var a = queue.Dequeue();
    //        var i = a.X;
    //        var j = a.Y;
    //        var leftV = board[i, j - 1];
    //        var rightV = board[i, j + 1];
    //        var topV = board[i - 1, j];
    //        var bottomV = board[i + 1, j];
    //        if (leftV == O ||  rightV == O || topV == O || bottomV  == O )
    //        {
    //            board[i, j] = O;

    //            iterateCount = 0;
    //            maxCount--;
    //            continue;
    //        }

    //        if (leftV == X && rightV == X && topV == X && bottomV == X)
    //        {
    //            board[i, j] = X;

    //            iterateCount = 0;
    //            maxCount--;
    //            continue;
    //        }

    //        //if (leftV == smallO || rightV == smallO || topV == smallO || bottomV == smallO )
    //        //{
    //            queue.Enqueue(a);

    //            iterateCount++;

    //            if (iterateCount == maxCount) break;
    //        //}
    //    } // while( iterateCount < maxCount )

    //    foreach ( var a in queue)
    //    {
    //        board[a.X, a.Y] = X;
    //    }
    //}

    //public class Position2D
    //{
    //    public int X;
    //    public int Y;
    //}
}
/*

bfs+递归dfs+非递归dfs+并查集
胡明
发布于 2019-05-26
32.5k
思路：
这道题我们拿到基本就可以确定是图的 dfs、bfs 遍历的题目了。题目中解释说被包围的区间不会存在于边界上，所以我们会想到边界上的 OO 要特殊处理，只要把边界上的 OO 特殊处理了，那么剩下的 OO 替换成 XX 就可以了。问题转化为，如何寻找和边界联通的 OO，我们需要考虑如下情况。


X X X X
X O O X
X X O X
X O O X
这时候的 OO 是不做替换的。因为和边界是连通的。为了记录这种状态，我们把这种情况下的 OO 换成 # 作为占位符，待搜索结束之后，遇到 OO 替换为 XX（和边界不连通的 OO）；遇到 #，替换回 $O(和边界连通的 OO)。

如何寻找和边界联通的OO? 从边界出发，对图进行 dfs 和 bfs 即可。这里简单总结下 dfs 和 dfs。

bfs 递归。可以想想二叉树中如何递归的进行层序遍历。
bfs 非递归。一般用队列存储。
dfs 递归。最常用，如二叉树的先序遍历。
dfs 非递归。一般用 stack。
那么基于上面这种想法，我们有四种方式实现。

dfs递归:

class Solution {
    public void solve(char[][] board) {
        if (board == null || board.length == 0) return;
        int m = board.length;
        int n = board[0].length;
        for (int i = 0; i < m; i++) {
            for (int j = 0; j < n; j++) {
                // 从边缘o开始搜索
                boolean isEdge = i == 0 || j == 0 || i == m - 1 || j == n - 1;
                if (isEdge && board[i][j] == 'O') {
                    dfs(board, i, j);
                }
            }
        }

        for (int i = 0; i < m; i++) {
            for (int j = 0; j < n; j++) {
                if (board[i][j] == 'O') {
                    board[i][j] = 'X';
                }
                if (board[i][j] == '#') {
                    board[i][j] = 'O';
                }
            }
        }
    }

    public void dfs(char[][] board, int i, int j) {
        if (i < 0 || j < 0 || i >= board.length  || j >= board[0].length || board[i][j] == 'X' || board[i][j] == '#') {
            // board[i][j] == '#' 说明已经搜索过了. 
            return;
        }
        board[i][j] = '#';
        dfs(board, i - 1, j); // 上
        dfs(board, i + 1, j); // 下
        dfs(board, i, j - 1); // 左
        dfs(board, i, j + 1); // 右
    }
}
dsf 非递归:
非递归的方式，我们需要记录每一次遍历过的位置，我们用 stack 来记录，因为它先进后出的特点。而位置我们定义一个内部类 Pos 来标记横坐标和纵坐标。注意的是，在写非递归的时候，我们每次查看 stack 顶，但是并不出 stack，直到这个位置上下左右都搜索不到的时候出 Stack。


class Solution {
    public class Pos{
        int i;
        int j;
        Pos(int i, int j) {
            this.i = i;
            this.j = j;
        }
    }
    public void solve(char[][] board) {
        if (board == null || board.length == 0) return;
        int m = board.length;
        int n = board[0].length;
        for (int i = 0; i < m; i++) {
            for (int j = 0; j < n; j++) {
                // 从边缘第一个是o的开始搜索
                boolean isEdge = i == 0 || j == 0 || i == m - 1 || j == n - 1;
                if (isEdge && board[i][j] == 'O') {
                    dfs(board, i, j);
                }
            }
        }

        for (int i = 0; i < m; i++) {
            for (int j = 0; j < n; j++) {
                if (board[i][j] == 'O') {
                    board[i][j] = 'X';
                }
                if (board[i][j] == '#') {
                    board[i][j] = 'O';
                }
            }
        }
    }

    public void dfs(char[][] board, int i, int j) {
        Stack<Pos> stack = new Stack<>();
        stack.push(new Pos(i, j));
        board[i][j] = '#';
        while (!stack.isEmpty()) {
            // 取出当前stack 顶, 不弹出.
            Pos current = stack.peek();
            // 上
            if (current.i - 1 >= 0 
                && board[current.i - 1][current.j] == 'O') {
                stack.push(new Pos(current.i - 1, current.j));
                board[current.i - 1][current.j] = '#';
              continue;
            }
            // 下
            if (current.i + 1 <= board.length - 1 
                && board[current.i + 1][current.j] == 'O') {
                stack.push(new Pos(current.i + 1, current.j));
                board[current.i + 1][current.j] = '#';      
                continue;
            }
            // 左
            if (current.j - 1 >= 0 
                && board[current.i][current.j - 1] == 'O') {
                stack.push(new Pos(current.i, current.j - 1));
                board[current.i][current.j - 1] = '#';
                continue;
            }
            // 右
            if (current.j + 1 <= board[0].length - 1 
                && board[current.i][current.j + 1] == 'O') {
                stack.push(new Pos(current.i, current.j + 1));
                board[current.i][current.j + 1] = '#';
                continue;
            }
            // 如果上下左右都搜索不到,本次搜索结束，弹出stack
            stack.pop();
        }
    }
}

bfs 非递归:
dfs 非递归的时候我们用 stack 来记录状态，而 bfs 非递归，我们则用队列来记录状态。和 dfs 不同的是，dfs 中搜索上下左右，只要搜索到一个满足条件，我们就顺着该方向继续搜索，所以你可以看到 dfs 代码中，只要满足条件，就入 Stack，然后 continue 本次搜索，进行下一次搜索，直到搜索到没有满足条件的时候出 stack。而 dfs 中，我们要把上下左右满足条件的都入队，所以搜索的时候就不能 continue。大家可以对比下两者的代码，体会 bfs 和 dfs 的差异。


class Solution {
    public class Pos{
        int i;
        int j;
        Pos(int i, int j) {
            this.i = i;
            this.j = j;
        }
    }
    public void solve(char[][] board) {
        if (board == null || board.length == 0) return;
        int m = board.length;
        int n = board[0].length;
        for (int i = 0; i < m; i++) {
            for (int j = 0; j < n; j++) {
                // 从边缘第一个是o的开始搜索
                boolean isEdge = i == 0 || j == 0 || i == m - 1 || j == n - 1;
                if (isEdge && board[i][j] == 'O') {
                    bfs(board, i, j);
                }
            }
        }

        for (int i = 0; i < m; i++) {
            for (int j = 0; j < n; j++) {
                if (board[i][j] == 'O') {
                    board[i][j] = 'X';
                }
                if (board[i][j] == '#') {
                    board[i][j] = 'O';
                }
            }
        }
    }

    public void bfs(char[][] board, int i, int j) {
        Queue<Pos> queue = new LinkedList<>();
        queue.add(new Pos(i, j));
        board[i][j] = '#';
        while (!queue.isEmpty()) {
            Pos current = queue.poll();
            // 上
            if (current.i - 1 >= 0 
                && board[current.i - 1][current.j] == 'O') {
                queue.add(new Pos(current.i - 1, current.j));
                board[current.i - 1][current.j] = '#';
              	// 没有continue.
            }
            // 下
            if (current.i + 1 <= board.length - 1 
                && board[current.i + 1][current.j] == 'O') {
                queue.add(new Pos(current.i + 1, current.j));
                board[current.i + 1][current.j] = '#';      
            }
            // 左
            if (current.j - 1 >= 0 
                && board[current.i][current.j - 1] == 'O') {
                queue.add(new Pos(current.i, current.j - 1));
                board[current.i][current.j - 1] = '#';
            }
            // 右
            if (current.j + 1 <= board[0].length - 1 
                && board[current.i][current.j + 1] == 'O') {
                queue.add(new Pos(current.i, current.j + 1));
                board[current.i][current.j + 1] = '#';
            }
        }
    }
}
bfs 递归:
bfs 一般我们不会去涉及，而且比较绕，之前我们唯一 A 过的用 bfs 递归的方式是层序遍历二叉树的时候可以用递归的方式。

并查集:
并查集这种数据结构好像大家不太常用，实际上很有用，我在实际的 production code 中用过并查集。并查集常用来解决连通性的问题，即将一个图中连通的部分划分出来。当我们判断图中两个点之间是否存在路径时，就可以根据判断他们是否在一个连通区域。 而这道题我们其实求解的就是和边界的 OO 在一个连通区域的的问题。

并查集的思想就是，同一个连通区域内的所有点的根节点是同一个。将每个点映射成一个数字。先假设每个点的根节点就是他们自己，然后我们以此输入连通的点对，然后将其中一个点的根节点赋成另一个节点的根节点，这样这两个点所在连通区域又相互连通了。
并查集的主要操作有：

find(int m)：这是并查集的基本操作，查找 mm 的根节点。

isConnected(int m,int n)：判断 m，nm，n 两个点是否在一个连通区域。

union(int m,int n):合并 m，nm，n 两个点所在的连通区域。


class UnionFind {
    int[] parents;

    public UnionFind(int totalNodes) {
        parents = new int[totalNodes];
        for (int i = 0; i < totalNodes; i++) {
            parents[i] = i;
        }
    }
		// 合并连通区域是通过find来操作的, 即看这两个节点是不是在一个连通区域内.
    void union(int node1, int node2) {
        int root1 = find(node1);
        int root2 = find(node2);
        if (root1 != root2) {
            parents[root2] = root1;
        }
    }

    int find(int node) {
        while (parents[node] != node) {
            // 当前节点的父节点 指向父节点的父节点.
            // 保证一个连通区域最终的parents只有一个.
            parents[node] = parents[parents[node]];
            node = parents[node];
        }

        return node;
    }

    boolean isConnected(int node1, int node2) {
        return find(node1) == find(node2);
    }
}
我们的思路是把所有边界上的 OO 看做一个连通区域。遇到 OO 就执行并查集合并操作，这样所有的 OO 就会被分成两类

和边界上的 OO 在一个连通区域内的。这些 OO 我们保留。
不和边界上的 OO 在一个连通区域内的。这些 OO 就是被包围的，替换。
由于并查集我们一般用一维数组来记录，方便查找 parants，所以我们将二维坐标用 node 函数转化为一维坐标。


public void solve(char[][] board) {
        if (board == null || board.length == 0)
            return;

        int rows = board.length;
        int cols = board[0].length;

        // 用一个虚拟节点, 边界上的O 的父节点都是这个虚拟节点
        UnionFind uf = new UnionFind(rows * cols + 1);
        int dummyNode = rows * cols;

        for (int i = 0; i < rows; i++) {
            for (int j = 0; j < cols; j++) {
                if (board[i][j] == 'O') {
                    // 遇到O进行并查集操作合并
                    if (i == 0 || i == rows - 1 || j == 0 || j == cols - 1) {
                        // 边界上的O,把它和dummyNode 合并成一个连通区域.
                        uf.union(node(i, j), dummyNode);
                    } else {
                        // 和上下左右合并成一个连通区域.
                        if (i > 0 && board[i - 1][j] == 'O')
                            uf.union(node(i, j), node(i - 1, j));
                        if (i < rows - 1 && board[i + 1][j] == 'O')
                            uf.union(node(i, j), node(i + 1, j));
                        if (j > 0 && board[i][j - 1] == 'O')
                            uf.union(node(i, j), node(i, j - 1));
                        if (j < cols - 1 && board[i][j + 1] == 'O')
                            uf.union(node(i, j), node(i, j + 1));
                    }
                }
            }
        }

        for (int i = 0; i < rows; i++) {
            for (int j = 0; j < cols; j++) {
                if (uf.isConnected(node(i, j), dummyNode)) {
                    // 和dummyNode 在一个连通区域的,那么就是O；
                    board[i][j] = 'O';
                } else {
                    board[i][j] = 'X';
                }
            }
        }
    }

    int node(int i, int j) {
        return i * cols + j;
    }
}
下一篇：纯C，dfs回溯遍历，【130.被围绕的区域】【思路清晰，代码易读】

public class Solution {
    public void Solve(char[][] board) {
        if(board==null||board.Length==0)return ;
        for(int i=0;i<board.Length;i++)
        {
            dfs(board,i,0);
            dfs(board,i,board[0].Length-1);
        }
        for(int i=0;i<board[0].Length;i++)
        {
            dfs(board,0,i);
            dfs(board,board.Length-1,i);
        }
        for(int i=0;i<board.Length;i++)
        {
            for(int j=0;j<board[0].Length;j++)
            {
                if(board[i][j]=='O')board[i][j]='X';
                if(board[i][j]=='1')board[i][j]='O';
            }
        }
        return ;
    }
    void dfs(char[][] board ,int i,int j)
    {
        if(i<0||j<0||i>=board.Length||j>=board[0].Length||board[i][j]!='O')
        {
            return;
        }
        board[i][j]='1';
        dfs(board,i-1,j);
        dfs(board,i+1,j);
        dfs(board,i,j-1);
        dfs(board,i,j+1);
        return ;
    }
} 
 
 
*/