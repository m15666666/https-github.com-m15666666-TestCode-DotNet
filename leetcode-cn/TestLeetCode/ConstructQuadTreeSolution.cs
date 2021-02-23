/*
给你一个 n * n 矩阵 grid ，矩阵由若干 0 和 1 组成。请你用四叉树表示该矩阵 grid 。

你需要返回能表示矩阵的 四叉树 的根结点。

注意，当 isLeaf 为 False 时，你可以把 True 或者 False 赋值给节点，两种值都会被判题机制 接受 。

四叉树数据结构中，每个内部节点只有四个子节点。此外，每个节点都有两个属性：

val：储存叶子结点所代表的区域的值。1 对应 True，0 对应 False；
isLeaf: 当这个节点是一个叶子结点时为 True，如果它有 4 个子节点则为 False 。
class Node {
    public boolean val;
    public boolean isLeaf;
    public Node topLeft;
    public Node topRight;
    public Node bottomLeft;
    public Node bottomRight;
}
我们可以按以下步骤为二维区域构建四叉树：

如果当前网格的值相同（即，全为 0 或者全为 1），将 isLeaf 设为 True ，将 val 设为网格相应的值，并将四个子节点都设为 Null 然后停止。
如果当前网格的值不同，将 isLeaf 设为 False， 将 val 设为任意值，然后如下图所示，将当前网格划分为四个子网格。
使用适当的子网格递归每个子节点。

如果你想了解更多关于四叉树的内容，可以参考 wiki 。

四叉树格式：

输出为使用层序遍历后四叉树的序列化形式，其中 null 表示路径终止符，其下面不存在节点。

它与二叉树的序列化非常相似。唯一的区别是节点以列表形式表示 [isLeaf, val] 。

如果 isLeaf 或者 val 的值为 True ，则表示它在列表 [isLeaf, val] 中的值为 1 ；如果 isLeaf 或者 val 的值为 False ，则表示值为 0 。

 

示例 1：

输入：grid = [[0,1],[1,0]]
输出：[[0,1],[1,0],[1,1],[1,1],[1,0]]
解释：此示例的解释如下：
请注意，在下面四叉树的图示中，0 表示 false，1 表示 True 。

示例 2：

输入：grid = [[1,1,1,1,0,0,0,0],[1,1,1,1,0,0,0,0],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,0,0,0,0],[1,1,1,1,0,0,0,0],[1,1,1,1,0,0,0,0],[1,1,1,1,0,0,0,0]]
输出：[[0,1],[1,1],[0,1],[1,1],[1,0],null,null,null,null,[1,0],[1,0],[1,1],[1,1]]
解释：网格中的所有值都不相同。我们将网格划分为四个子网格。
topLeft，bottomLeft 和 bottomRight 均具有相同的值。
topRight 具有不同的值，因此我们将其再分为 4 个子网格，这样每个子网格都具有相同的值。
解释如下图所示：

示例 3：

输入：grid = [[1,1],[1,1]]
输出：[[1,1]]
示例 4：

输入：grid = [[0]]
输出：[[1,0]]
示例 5：

输入：grid = [[1,1,0,0],[1,1,0,0],[0,0,1,1],[0,0,1,1]]
输出：[[0,1],[1,1],[1,0],[1,0],[1,1]]
 

提示：

n == grid.length == grid[i].length
n == 2^x 其中 0 <= x <= 6

*/

/// <summary>
/// https://leetcode-cn.com/problems/construct-quad-tree/
/// 427. 建立四叉树
///
///
///
/// </summary>
internal class ConstructQuadTreeSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    // Definition for a QuadTree node.
    public class Node
    {
        public bool val;
        public bool isLeaf;
        public Node topLeft;
        public Node topRight;
        public Node bottomLeft;
        public Node bottomRight;

        public Node()
        {
            val = false;
            isLeaf = false;
            topLeft = null;
            topRight = null;
            bottomLeft = null;
            bottomRight = null;
        }

        public Node(bool _val, bool _isLeaf)
        {
            val = _val;
            isLeaf = _isLeaf;
            topLeft = null;
            topRight = null;
            bottomLeft = null;
            bottomRight = null;
        }

        public Node(bool _val, bool _isLeaf, Node _topLeft, Node _topRight, Node _bottomLeft, Node _bottomRight)
        {
            val = _val;
            isLeaf = _isLeaf;
            topLeft = _topLeft;
            topRight = _topRight;
            bottomLeft = _bottomLeft;
            bottomRight = _bottomRight;
        }
    }

    public Node Construct(int[][] grid)
    {
        const int True = 1;
        return Construct(grid, 0, grid.Length, 0, grid[0].Length);

        static bool Check(int[][] grid, int row1, int row2, int col1, int col2)
        {
            int unique = grid[row1][col1];
            for (int i = row1; i < row2; ++i)
                for (int j = col1; j < col2; ++j)
                    if (grid[i][j] != unique) return false;
            return true;
        }

        static Node Construct(int[][] grid, int row1, int row2, int col1, int col2)
        {
            if (Check(grid, row1, row2, col1, col2)) return new Node(grid[row1][col1] == True, true);

            int step = (row2 - row1) / 2;
            return new Node(grid[row1][col1] == True, false)
            {
                topLeft = Construct(grid, row1, row1 + step, col1, col1 + step),
                topRight = Construct(grid, row1, row1 + step, col1 + step, col2),
                bottomLeft = Construct(grid, row1 + step, row2, col1, col1 + step),
                bottomRight = Construct(grid, row1 + step, row2, col1 + step, col2)
            };
        }
    }
}

/*
栈/递归实现：递归链+终止条件
Bollie
发布于 2021-01-28
60
解题思路
题目虽然长，但是最关键的信息就是构造法则给予递归构造条件：

如果当前网格的值相同（即全为 0 或者全为 1），将 isLeaf 设为 True ，将 val 设为网格相应的值，并将四个子节点都设为 Null 然后停止。（递归终止条件，创建叶子节点）
如果当前网格的值不同，将 isLeaf 设为 False， 将 val 设为任意值，然后如下图所示，将当前网格划分为四个子网格。（递归链）
使用适当的子网格递归每个子节点。（递归前的四个子树处理）
因此很容易得到如下递归框架：

当前区域是否全部相同（递归触发条件）：
是：此区域就是叶子节点，创建叶子节点就返回。（递归终止条件）
否：当前区域（根节点）不是叶子节点，因此isLeaft = false，然后分别处理四个子树
topLeft：划分 topLeft 区域，继续递归查看 topLeft 区域是否全部相同
topRight：划分 topRight 区域，继续递归查看 topRight 区域是否全部相同
bottomLeft：划分 bottomLeft 区域，继续递归查看 bottomLeft 区域是否全部相同
bottomRight：划分 bottomRight 区域，继续递归查看 bottomRight 区域是否全部相同
要检测当前区域是否全部相同需要实现如下函数：


bool check(vector<vector<int>>& grid, int row1, int row2, int col1, int col2) {
    int unique = grid[row1][col1];
    for (int i = row1; i < row2; ++i) {
        for (int j = col1; j < col2; ++j) {
            if (grid[i][j] != unique) return false;
        }
    }
    return true;
}
代码
代码一中是递归实现，代码二中是栈实现：


class Solution {
public: 
    bool check(vector<vector<int>>& grid, int row1, int row2, int col1, int col2) {
        int unique = grid[row1][col1];
        for (int i = row1; i < row2; ++i) {
            for (int j = col1; j < col2; ++j) {
                if (grid[i][j] != unique) return false;
            }
        }
        return true;
    }

    Node *helper(vector<vector<int>>& grid, int row1, int row2, int col1, int col2) {
        if (check(grid, row1, row2, col1, col2)) 
            return new Node(grid[row1][col1], true);  // 叶子节点结束
        int step = (row2 - row1) / 2;  // 划分区间
        Node *ret = new Node(grid[row1][col1], false);
        ret->topLeft = helper(grid, row1, row1 + step, col1, col1 + step);
        ret->topRight = helper(grid, row1, row1 + step, col1 + step, col2);
        ret->bottomLeft = helper(grid, row1 + step, row2, col1, col1 + step);
        ret->bottomRight = helper(grid, row1 + step, row2, col1 + step, col2);
        return ret;
    }

    Node* construct(vector<vector<int>>& grid) {
        return helper(grid, 0, grid.size(), 0, grid[0].size());
    }
};
复杂度分析：

class Solution {
public: 
    bool check(vector<vector<int>>& grid, int row1, int row2, int col1, int col2) {
        int unique = grid[row1][col1];
        for (int i = row1; i < row2; ++i) {
            for (int j = col1; j < col2; ++j) {
                if (grid[i][j] != unique) return false;
            }
        }
        return true;
    }

    Node* construct(vector<vector<int>>& grid) {
        using NodeInfo = tuple<Node *&, int , int, int, int>;  // 父节点必须为指针引用（二级指针）
        Node *root = nullptr;
        stack<NodeInfo> stk; stk.push({root, 0, grid.size(), 0, grid[0].size()});
        while (!stk.empty()) {
            auto [parent, row1, row2, col1, col2] = stk.top(); stk.pop();
            Node *node = new Node(grid[row1][col1], false);
            if (check(grid, row1, row2, col1, col2)) {  // 叶子节点
                node->isLeaf = true;
            } else {
                int step = (row2 - row1) / 2;
                stk.push({node->bottomRight, row1 + step, row2, col1 + step, col2});
                stk.push({node->bottomLeft, row1 + step, row2, col1, col1 + step});
                stk.push({node->topRight, row1, row1 + step, col1 + step, col2});
                stk.push({node->topLeft, row1, row1 + step, col1, col1 + step});
            }
            parent = node;  // 父节点指向自己
        }
        return root;
    }
};
复杂度分析：

时间复杂度：O(mn)O(mn)，最坏情况下 0 1 交错，即叶子节点是区域面积为 1。
空间复杂度：O(mn)O(mn)，递归深度就是树高度为 \frac{mn}{4} 
4
mn
​	
 。
 
public class Solution {
    public Node Construct(int[][] grid) {
        Node one = new Node(true, true);
        Node zero = new Node(false, true);
        int n = grid.Length;
        Node[][] nodes = new Node[n][];
        Node[][] nodes2 = nodes;
        for (int i = 0; i < n; ++i)
        {
            nodes[i] = new Node[n];
            for (int j = 0; j < n; ++j)
                nodes[i][j] = (grid[i][j] == 0) ? zero : one;
        }
        for (int sizen = 1; (1 << sizen) <= n; ++sizen)
        {
            int size = n >> sizen;
            for (int i = 0; i < size; ++i)
            {
                for (int j = 0; j < size; ++j)
                {
                    int ci = i << 1;
                    int cj = j << 1;
                    Node tl = nodes[ci][cj];
                    Node tr = nodes[ci][cj+1];
                    Node dl = nodes[ci+1][cj];
                    Node dr = nodes[ci+1][cj+1];
                    if (tl.isLeaf && tr.isLeaf && dl.isLeaf && dr.isLeaf && tl.val == tr.val && tl.val == dl.val && tl.val == dr.val)
                    {
                        nodes2[i][j] = new Node(tl.val, true);
                    }
                    else
                    {
                        nodes2[i][j] = new Node(true, false, tl, tr, dl, dr);
                    }
                }
            }
        }
        return nodes[0][0];
    }
} 


public class Solution {
    public Node Construct(int[][] grid) {
        return ConstructImple(grid, 0, grid[0].Length - 1, 0, grid.Length - 1);
    }

    public Node ConstructImple(int[][] grid, int left, int right, int top, int bottom) {
        if(left > right || top > bottom) return null;
        if(IsLeaf(grid, left, right, top, bottom)) {
            return new Node(grid[top][left] == 1 ? true : false, true);
        }
        int delta = (right - left) / 2;
        Node node = new Node(true, false);
        node.topLeft = ConstructImple(grid, left, left + delta, top, top + delta);
        node.topRight = ConstructImple(grid, left + delta + 1, right, top, top + delta);
        node.bottomLeft = ConstructImple(grid, left, left + delta, top + delta + 1, bottom);
        node.bottomRight = ConstructImple(grid, left + delta + 1, right, top + delta + 1, bottom);
        return node;
    }

    public bool IsLeaf(int[][] grid, int left, int right, int top, int bottom) {
        int ret = grid[top][left];
        for(int i = top; i <= bottom; i ++) {
            for(int j = left; j <= right; j ++) {
                if(grid[i][j] != ret) {
                    return false;
                }
            }
        }
        return true;
    }
}
*/