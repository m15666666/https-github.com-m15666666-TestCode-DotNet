using System;

/*
给你一个 m x n 的矩阵，其中的值均为非负整数，代表二维高度图每个单元的高度，请计算图中形状最多能接多少体积的雨水。

示例：

给出如下 3x6 的高度图:
[
  [1,4,3,1,3,2],
  [3,2,1,3,2,4],
  [2,3,3,2,3,1]
]

返回 4 。

如上图所示，这是下雨前的高度图[[1,4,3,1,3,2],[3,2,1,3,2,4],[2,3,3,2,3,1]] 的状态。

下雨后，雨水将会被存储在这些方块中。总的接雨水量是4。

提示：

1 <= m, n <= 110
0 <= heightMap[i][j] <= 20000

*/

/// <summary>
/// https://leetcode-cn.com/problems/trapping-rain-water-ii/
/// 407. 接雨水 II
///
/// </summary>
internal class TrappingRainWaterIISolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int TrapRainWater(int[][] heights)
    {
        if (heights == null || heights.Length == 0) return 0;

        int n = heights.Length;
        int m = heights[0].Length;
        if (n < 3 || m < 3) return 0;
        bool[,] visited = new bool[n, m];
        // 优先队列中存放三元组 [x,y,h] 坐标和高度
        MaxPQ2<int[]> minPQ = new MaxPQ2<int[]>((o1, o2) => o1[2].CompareTo(o2[2]), n * m, false);

        // 先把最外一圈放进去
        for (int i = 0; i < n; i++)
        {
            int r = i, c = 0;
            minPQ.Insert(new int[] { r, c, heights[r][c] });
            visited[r, c] = true;
            c = m - 1;
            minPQ.Insert(new int[] { r, c, heights[r][c] });
            visited[r, c] = true;
        }
        for (int j = 1; j < m - 1; j++)
        {
            int r = 0, c = j;
            minPQ.Insert(new int[] { r, c, heights[r][c] });
            visited[r, c] = true;
            r = n - 1;
            minPQ.Insert(new int[] { r, c, heights[r][c] });
            visited[r, c] = true;
        }

        int ret = 0;
        // 方向数组，把dx和dy压缩成一维来做
        int[] dirs = { -1, 0, 1, 0, -1 };
        while (0 < minPQ.Count)
        {
            int[] minHeight = minPQ.DelMax();
            for (int k = 0; k < 4; k++)
            {
                int nextX = minHeight[0] + dirs[k];
                int nextY = minHeight[1] + dirs[k + 1];
                if (-1 < nextX && nextX < n && -1 < nextY && nextY < m && !visited[nextX, nextY])
                {
                    var h = heights[nextX][nextY];
                    if (h < minHeight[2])
                    {
                        ret += minHeight[2] - h;
                        h = minHeight[2];
                    }

                    minPQ.Insert(new int[] { nextX, nextY, h });
                    visited[nextX, nextY] = true;
                }
            }
        }
        return ret;
    }

    public abstract class MaxPQBase<Key>
    {
        // 存储元素的数组
        private Key[] pq;

        // 当前 Priority Queue 中的元素个数
        private int N = 0;

        /// <summary>
        /// 是最大堆还是最小堆，默认最大堆
        /// </summary>
        private readonly bool _maxOrMin = true;

        public MaxPQBase(int cap, bool maxOrMin = true)
        {
            // 索引 0 不用，所以多分配一个空间
            pq = new Key[cap + 1];

            _maxOrMin = maxOrMin;
        }

        // 上浮第 k 个元素，以维护最大堆性质
        private void Swim(int k)
        {
            // 如果浮到堆顶，就不能再上浮了
            while (k > 1 && Less(Parent(k), k))
            {
                // 如果第 k 个元素比上层大
                // 将 k 换上去
                Exchange(Parent(k), k);
                k = Parent(k);
            }
        }

        ///下沉第 k 个元素，以维护最大堆性质
        private void Sink(int k)
        {
            // 如果沉到堆底，就沉不下去了
            while (Left(k) <= N)
            {
                // 先假设左边节点较大
                int older = Left(k);

                // 如果右边节点存在，比一下大小
                if (Right(k) <= N && Less(older, Right(k))) older = Right(k);

                // 结点 k 比俩孩子都大，就不必下沉了
                if (Less(older, k)) break;

                // 否则，不符合最大堆的结构，下沉 k 结点
                Exchange(k, older);
                k = older;
            }
        }

        // 交换数组的两个元素
        private void Exchange(int i, int j)
        {
            Key temp = pq[i];
            pq[i] = pq[j];
            pq[j] = temp;
        }

        // 比较两个数组元素的大小，子类实现
        protected abstract int Compare(Key i, Key j);

        // pq[i] 是否比 pq[j] 小？
        private bool Less(int i, int j) => (_maxOrMin ? Compare(pq[i], pq[j]) : Compare(pq[j], pq[i])) < 0;

        private int Parent(int i) => i / 2;

        private int Left(int i) => i * 2;

        private int Right(int i) => i * 2 + 1;

        public void Insert(Key e)
        {
            N++;
            // 先把新元素加到最后
            pq[N] = e;
            // 然后让它上浮到正确的位置
            Swim(N);
        }

        public Key DelMax()
        {
            // 最大堆的堆顶就是最大元素
            Key max = pq[1];

            // 把这个最大元素换到最后，删除之
            Exchange(1, N);
            pq[N] = default(Key);
            N--;

            // 让 pq[1] 下沉到正确位置
            Sink(1);

            return max;
        }

        // 返回当前队列中最大元素
        public Key Max => pq[1];

        public int Count => N;
    }

    /// <summary>
    /// 使用Comparison比较的优先级队列
    /// </summary>
    /// <typeparam name="Key"></typeparam>
    public class MaxPQ2<Key> : MaxPQBase<Key>
    {
        // 自定义的比较大小的函数
        private readonly Comparison<Key> _comparison;

        public MaxPQ2(Comparison<Key> comparison, int cap, bool maxOrMin = true) : base(cap, maxOrMin)
        {
            _comparison = comparison;
        }

        protected override int Compare(Key i, Key j) => _comparison(i, j);
    }
}

/*
优先队列的思路解决接雨水II，逐行解释
Jerry
发布于 2020-03-21
9.2k
解题思路
前言：可能不是最好的思路，如果有好的思路麻烦大佬们艾特我学习下！谢谢了！

接雨水I中，我们维护了左右两个最高的墙，那么在这里，就是维护周围一个圈，用堆来维护周围这一圈中的最小元素。为什么是维护最小的元素不是最大的元素呢，因为木桶原理呀。这个最小的元素从堆里弹出来，和它四个方向的元素去比较大小，看能不能往里灌水，怎么灌水呢，如果用方向就比较复杂了，我们可以用visited数组来表示哪些遍历过，哪些没遍历过。如果当前弹出来的高度比它周围的大，他就能往矮的里面灌水了，灌水后要把下一个柱子放进去的时候，放的高度要取两者较大的，也就是灌水后的高度，不是它原来矮的时候的高度了，如果不能灌水，继续走。


Given the following 3x6 height map:
[
  [1,4,3,1,3,2],
  [3,2,1,3,2,4],
  [2,3,3,2,3,1]
]
就拿这个例子来说，我们先把第一圈都放进去，然后开始从堆中弹出，第一圈，最小值是1（遍历时候标记为访问过），1从堆里弹出来，比如弹出来1(坐标[0,3])，它下方的3没有被访问过，尝试灌水，发现不能灌水，3入堆，然后继续弹。比如说，我此时弹出来一个3（坐标[1,0]），它能向右边2(坐标[1,1])灌水，那这边就可以统计了，然后我们要插入2(坐标[1,1])这个位置，但是插入的时候，要记得你得是插入的高度得是灌水后的高度，而不是原来的高度了

当然我这边只是举个例子哈

代码

class Solution {
    public int trapRainWater(int[][] heights) {
        if (heights == null || heights.length == 0) return 0;
        int n = heights.length;
        int m = heights[0].length;
        // 用一个vis数组来标记这个位置有没有被访问过
        boolean[][] vis = new boolean[n][m];
        // 优先队列中存放三元组 [x,y,h] 坐标和高度
        PriorityQueue<int[]> pq = new PriorityQueue<>((o1, o2) -> o1[2] - o2[2]);

        // 先把最外一圈放进去
        for (int i = 0; i < n; i++) {
            for (int j = 0; j < m; j++) {
                if (i == 0 || i == n - 1 || j == 0 || j == m - 1) {
                    pq.offer(new int[]{i, j, heights[i][j]});
                    vis[i][j] = true;
                }
            }
        }
        int res = 0;
        // 方向数组，把dx和dy压缩成一维来做
        int[] dirs = {-1, 0, 1, 0, -1};
        while (!pq.isEmpty()) {
            int[] poll = pq.poll();
            // 看一下周围四个方向，没访问过的话能不能往里灌水
            for (int k = 0; k < 4; k++) {
                int nx = poll[0] + dirs[k];
                int ny = poll[1] + dirs[k + 1];
                // 如果位置合法且没访问过
                if (nx >= 0 && nx < n && ny >= 0 && ny < m && !vis[nx][ny]) {
                    // 如果外围这一圈中最小的比当前这个还高，那就说明能往里面灌水啊
                    if (poll[2] > heights[nx][ny]) {
                        res += poll[2] - heights[nx][ny];
                    }
                    // 如果灌水高度得是你灌水后的高度了，如果没灌水也要取高的
                    pq.offer(new int[]{nx, ny, Math.max(heights[nx][ny], poll[2])});
                    vis[nx][ny] = true;
                }
            }
        }
        return res;
    }
}

public class Solution {
    struct Cell{
        public Cell(int height, int row, int col)
        {
            Height = height;
            Row = row;
            Col = col;
        }
        public int Height {get; private set;}
        public int Row {get; private set;}
        public int Col {get; private set;}
    }
    class MinHeap{
        private List<Cell> list = new List<Cell>(32);
        private int count =0;
        public void Insert(Cell val)
        {
            if(list.Count > count)
            {
                list[count] = val;
            }else
            {
                list.Add(val);
            }
            SiftUp(count);
            count++;
        }
        private void SiftUp(int i)
        {
            while(i>0)
            {
                int parent = i-1>>1;
                if(list[parent].Height <= list[i].Height)
                {
                    break;
                }
                (list[parent], list[i]) = (list[i], list[parent]);
                i = parent;
            }
        }

        public int Count => count;

        public Cell Pull()
        {
            Cell val = list[0];
            count --;
            if(count != 0)
            {
                list[0] = list[count];
                Heapify(0, count);
            }
            return val;
        }

        private void Heapify(int i, int n)
        {
            int left = i*2+1, right = i*2+2;
            int min = i;
            if(left < n && list[left].Height < list[min].Height)
            {
                min = left;
            }
            if(right < n && list[right].Height < list[min].Height)
            {
                min = right;
            }
            if(min != i)
            {
                (list[i], list[min]) = (list[min], list[i]);
                Heapify(min, n);
            }
        }
    }
    public int TrapRainWater(int[][] heightMap) {
        if(heightMap == null||heightMap.Length <=2) return 0;
        int n = heightMap.Length;
        int m = heightMap[0].Length;
        if(m<=2) return 0;

        int ans = 0;
        bool[,] visited = new bool[n,m];
        MinHeap heap = new MinHeap();
        for(int col = 0; col < m; col++)
        {
            visited[0,col] = true;
            heap.Insert(new Cell(heightMap[0][col], 0, col));
        }
        for(int row = 1; row < n; row++)
        {
            visited[row,0] = true;
            heap.Insert(new Cell(heightMap[row][0], row, 0));
        }
        for(int col = 1; col < m; col++)
        {
            visited[n-1,col] = true;
            heap.Insert(new Cell(heightMap[n-1][col], n-1, col));
        }
        for(int row =1; row < n-1; row ++)
        {
            visited[row,m-1] = true;
            heap.Insert(new Cell(heightMap[row][m-1], row, m-1));
        }

        while(heap.Count > 0)
        {
            Cell cell = heap.Pull();
            foreach(var next in GetNeighbours(cell.Row, cell.Col))
            {
                if(next.row >=0 && next.row < n && next.col >=0 && next.col <m && !visited[next.row, next.col])
                {
                    visited[next.row, next.col] = true;
                    if(heightMap[next.row][next.col] < cell.Height)
                    {
                        ans += cell.Height - heightMap[next.row][next.col];
                        heap.Insert(new Cell(cell.Height, next.row, next.col));
                    }else{
                        heap.Insert(new Cell(heightMap[next.row][next.col], next.row, next.col));
                    }
                }
            }
        }
        return ans;
    }

    private IEnumerable<(int row, int col)> GetNeighbours(int x, int y)
    {
        yield return (x, y-1);
        yield return (x, y+1);
        yield return (x-1, y);
        yield return (x+1, y);
    }
}


C++ 并查集做法
竹芒
发布于 2020-11-27
652
大家似乎都是优先队列+由外向内搜索做的，提供一个不同的思路：从低到高枚举水面的高度，用并查集合并相邻的“水池”，并维护总体积。


struct Node{
    const string description = "EXAMPLE UNION SET";
    Node* fa;
    int filled, height, siz;
    bool side, vis;
    Node(): filled(), siz(1), side(), vis(){
    }
    int gap(Node* b){
        return (height-b->height)*b->siz + b->filled;
    }
    void merge(Node* b){
        if(b==this)return;
        filled += gap(b);
        siz += b->siz;
        b->fa = this;
    }
    Node* get(){
        return fa==this?fa:fa=fa->get();
    }
};
class Solution {
public:
    int trapRainWater(vector<vector<int>>& s) {
        int n=s.size(), m=s[0].size(), res=0;
        vector<pair<int, int>> V;
        vector<vector<Node>> N(n, vector<Node>(m));
        for(int i=0; i<n; i++){
            for(int j=0; j<m; j++){
                V.emplace_back(i, j);
                N[i][j].fa = &N[i][j];
            }
        }
        sort(V.begin(), V.end(), [&](pair<int, int> a, pair<int, int> b){
            return s[a.first][a.second] < s[b.first][b.second];
        });
        vector<int> dir = {-1, 0, 1, 0, -1};
        for(auto[x, y]: V){
            int h = s[x][y];
            bool side = false; // side表示是否当前块是否在边缘。
            for(int i=0; i<4; i++){
                int dx = x+dir[i], dy = y+dir[i+1];
                if(dx<0 || dx>=n || dy<0 || dy>=m || N[dx][dy].get()->side){
                    side = true;
                    break;
                }
            }
            N[x][y].height = s[x][y];
            N[x][y].side = side;
            N[x][y].vis = true;
            for(int i=0; i<4; i++){
                int dx = x+dir[i], dy = y+dir[i+1];
                if(dx<0 || dx>=n || dy<0 || dy>=m)continue;
                Node* tmp = N[dx][dy].get();
                if(tmp->vis && !tmp->side){
                    if(side)res += N[x][y].gap(tmp);
                    N[x][y].merge(tmp);
                }
            }
        }
        return res;
    }
};

407. 接雨水(最小堆)——C++解法
赫连昊栋

发布于 2020-03-12
3.7k
解题思路
我们还是来分享解题的技巧

方向数组的添加。因为二维数组，可以看作是一个矩阵，我们初始化好外围的元素，相当于一个“井”，外面呢一圈初始化为true，表示已经遍历过。
接下来遍历“井中的元素”的时候，使用方向数组，x和y轴上均以绝对值1来上下左右递增。具体元素怎么排是无所谓的，因为后面会if判断当前元素是否是“未遍历的”，是否是“井中的元素”。
最小堆的建立。因为不能让水溢出来，“木桶原理”，所以要找“最短的呢个板”。找到后，和“井中的元素”比较大小，如果最短板比井中元素高，那就“灌水”灌水的高度是二者的差值。如果最短板比井中元素低，那就不用灌水，因为井中元素其实也是“木板”，这时候再灌水就会溢出来。
最小堆的更新。我们在初始化好外围元素后，遍历井中元素的过程中，要对最小堆进行更新。
假如当前的最短木板是8，它未遍历的井中元素在下方，下方这个木板的高度是13，比8大，那么8就会被淘汰，不能灌水，相当于对于剩下的未遍历的元素来说，外面的“围墙”更高了。
假如当前的最短木板是12，它未遍历的井中元素在左方，左方这个木板的高度是10，比12小，那么因为“围墙”比井中元素大，所以灌水，灌到12这么大刚好。那么现在对于还“未遍历的井中元素**来说，旁边的10变成了12，还是“围墙”变高了。
所以说，不管如何，minHeap最小堆中元素的更新，都取的是当前最短木板和其井内未遍历元素高度，二者之间的最大值。
Cell结构体中小于号<的重载，让堆变成了最小堆，也就是元素越小，优先级越高，那么最小的元素会放在队列的top位。
Compare函数可以不要。
代码（半年后）

class Solution {
public:
    int trapRainWater(vector<vector<int>>& g) {
        typedef pair<int, pair<int, int>> PIII;
        int n = g.size(), m = g[0].size();
        
        vector<vector<bool>> st(n, vector<bool>(m, false));
        priority_queue<PIII, vector<PIII>, greater<PIII>> heap;
        
        for (int i = 0; i < n; i ++ )
        {
            st[i][0] = st[i][m - 1] = true;
            heap.push({g[i][0], {i, 0}});
            heap.push({g[i][m - 1], {i, m - 1}});
        }

        for (int j = 0; j < m; j ++ )
        {
            st[0][j] = st[n - 1][j] = true;
            heap.push({g[0][j], {0, j}});
            heap.push({g[n - 1][j], {n - 1, j}});
        }
        
        int res = 0;
        int dx[] = {-1, 0, 1, 0}, dy[] = {0, 1, 0, -1};
        while (heap.size())
        {
            auto t = heap.top();
            heap.pop();

            for (int k = 0; k < 4; k ++ )
            {
                int x = t.second.first, y = t.second.second, h = t.first;
                int a = x + dx[k], b = y + dy[k];
                if (a >= 0 && a < n && b >= 0 && b < m)
                {
                    if (st[a][b] == false)
                    {
                        st[a][b] = true;
                        if (h > g[a][b]) res += h - g[a][b];
                        heap.push({max(g[a][b], h), {a, b}});
                    }
                }
            }
        }

        return res;
    }
};
代码

class Solution 
{
public:
    struct Cell
    {
        int x, y, h;

        Cell() {};
        Cell(int xx, int yy, int hh) : x(xx), y(yy), h(hh) {};

        bool operator< (const Cell& c) const
        {
            return h > c.h;
        }
    };

    int compare(Cell x, Cell y)
    {
        if (x.h > y.h) return 1;
        else if (x.h == y.h) return 0;
        else return -1;
    }

    int trapRainWater(vector<vector<int>>& heightMap) 
    {
        if (heightMap.empty() || heightMap[0].empty()) return 0;

        priority_queue<Cell> minHeap;
        int rows = heightMap.size();
        int cols = heightMap[0].size();

        vector<vector<bool> > visited(rows, vector<bool>(cols));  // 一开始不用给visited数组赋值
        for (int i = 0; i < cols; i++)
        {
            minHeap.push({0, i, heightMap[0][i]});
            minHeap.push({rows - 1, i, heightMap[rows - 1][i]});
            visited[0][i] = true;
            visited[rows - 1][i] = true;
        }

        for (int j = 1; j < rows - 1; j++)
        {
            minHeap.push({j, 0, heightMap[j][0]});
            minHeap.push({j, cols - 1, heightMap[j][cols - 1]});
            visited[j][0] = true;
            visited[j][cols - 1] = true;
        }

        int dx[4] = {-1, 0, 1, 0};
        int dy[4] = {0, 1, 0, -1};

        int ans = 0;
        while(!minHeap.empty())
        {
            Cell curCell = minHeap.top();
            minHeap.pop();

            for (int i = 0; i < 4; i++)
            {
                int sx = curCell.x + dx[i];
                int sy = curCell.y + dy[i];

                if (sx >= 0 && sx < rows &&
                    sy >= 0 && sy < cols &&
                    visited[sx][sy] == false)
                    {
                        visited[sx][sy] = true;
                        minHeap.push({sx, sy, max(heightMap[sx][sy], curCell.h)}); // 之所以取得最大值是因为已经灌水到更大的值了
                                                                                   // 想想10和12之间，取的是12
                        if (heightMap[sx][sy] < curCell.h)
                        {
                            ans = ans + curCell.h - heightMap[sx][sy];
                        }
                        else ans = ans + 0; // 假如未遍历的柱子高度比minHeap中的最小元素高，就不加水了，因为如果加水一定会溢出来
                    } 
            }
        }
        return ans;
    }
};
下一篇：[Cpp] 优先级队列



*/