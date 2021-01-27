using System.Collections.Generic;

/*
给出方程式 A / B = k, 其中 A 和 B 均为代表字符串的变量， k 是一个浮点型数字。根据已知方程式求解问题，并返回计算结果。如果结果不存在，则返回 -1.0。

示例 :
给定 a / b = 2.0, b / c = 3.0
问题: a / c = ?, b / a = ?, a / e = ?, a / a = ?, x / x = ?
返回 [6.0, 0.5, -1.0, 1.0, -1.0 ]

输入为: vector<pair<string, string>> equations, vector<double>& values, vector<pair<string, string>> queries(方程式，方程式结果，问题方程式)， 其中 equations.size() == values.size()，即方程式的长度与方程式结果长度相等（程式与结果一一对应），并且结果值均为正数。以上为方程式的描述。 返回vector<double>类型。

基于上述例子，输入如下：

equations(方程式) = [ ["a", "b"], ["b", "c"] ],
values(方程式结果) = [2.0, 3.0],
queries(问题方程式) = [ ["a", "c"], ["b", "a"], ["a", "e"], ["a", "a"], ["x", "x"] ].
输入总是有效的。你可以假设除法运算中不会出现除数为0的情况，且不存在任何矛盾的结果。
*/

/// <summary>
/// https://leetcode-cn.com/problems/evaluate-division/
/// 399. 除法求值
/// https://blog.csdn.net/qq_23523409/article/details/84799489
/// </summary>
internal class EvaluateDivisionSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    #region 带权并查集

    public double[] CalcEquation(IList<IList<string>> equations, double[] values, IList<IList<string>> queries)
    {
        int equationsSize = equations.Count;
        int unionSize = 2 * equationsSize;
        UnionFind unionFind = new UnionFind(unionSize);
        var var2Id = new Dictionary<string, int>(unionSize);
        for (int i = 0, id = 0; i < equationsSize; i++)
        {
            var equation = equations[i];
            var var1 = equation[0];
            var var2 = equation[1];

            if (!var2Id.ContainsKey(var1)) var2Id[var1] = id++;
            if (!var2Id.ContainsKey(var2)) var2Id[var2] = id++;

            unionFind.Union(var2Id[var1], var2Id[var2], values[i]);
        }

        int queriesSize = queries.Count;
        double[] ret = new double[queriesSize];
        for (int i = 0; i < queriesSize; i++)
        {
            var query = queries[i];
            var var1 = query[0];
            var var2 = query[1];
            ret[i] = !var2Id.ContainsKey(var1) || !var2Id.ContainsKey(var2) ? -1 : unionFind.IsConnected(var2Id[var1], var2Id[var2]);
        }
        return ret;
    }

    private class UnionFind
    {
        private readonly int[] _parent;
        private readonly double[] _weight;

        public UnionFind(int n)
        {
            _parent = new int[n];
            _weight = new double[n];
            for (int i = 0; i < n; i++)
            {
                _parent[i] = i;
                _weight[i] = 1;
            }
        }

        public void Union(int x, int y, double value)
        {
            int rootX = Find(x);
            int rootY = Find(y);
            if (rootX == rootY) return;

            _parent[rootX] = rootY;
            _weight[rootX] = _weight[y] * value / _weight[x];
        }

        public int Find(int x)
        {
            if (x != _parent[x])
            {
                int origin = _parent[x];
                _parent[x] = Find(_parent[x]);
                _weight[x] *= _weight[origin];
            }
            return _parent[x];
        }

        public double IsConnected(int x, int y)
        {
            int rootX = Find(x);
            int rootY = Find(y);
            return rootX == rootY ? _weight[x] / _weight[y] : -1;
        }
    }

    #endregion 带权并查集

    //public double[] CalcEquation(string[][] equations, double[] values, string[][] queries)
    //{
    //    if (equations == null || equations.Length == 0 || queries == null || queries.Length == 0) return new double[0];

    //    for( int i = 0; i < equations.Length; i++)
    //    {
    //        var equation = equations[i];
    //        var m = equation[0];
    //        var d = equation[1];

    //        if( !_map.ContainsKey(m)) _map[m] = new Dictionary<string, double>();
    //        _map[m][d] = values[i];
    //    }

    //    double[] ret = new double[queries.Length];
    //    for( int i = 0; i < ret.Length; i++)
    //    {
    //        var query = queries[i];
    //        var m = query[0];
    //        var d = query[1];
    //        if(m == d)
    //        {
    //            ret[i] = 1;
    //            continue;
    //        }
    //        ret[i] = Solve(m, d);
    //        _visited.Clear();
    //    }
    //    return ret;
    //}

    //private double Solve( string m, string d)
    //{
    //    if (_map.ContainsKey(m) && _map[m].ContainsKey(d)) return _map[m][d];
    //    if (_map.ContainsKey(d) && _map[d].ContainsKey(m)) return 1 / _map[d][m];

    //    var paths = Find(m, d);
    //    if(paths.Count == 0) paths = Find(d, m);
    //    if (paths.Count == 0) return -1;

    //    double ret = 1;
    //    foreach (var path in paths)
    //        ret *= _map[path.Item1][path.Item2];

    //    return ret;
    //}

    //private List<Tuple<string,string>> Find(string m, string d)
    //{
    //    _visited.Add(m);
    //    if (!_map.ContainsKey(m)) return new List<Tuple<string, string>>();

    //    var nexts = _map[m];
    //    if (nexts.ContainsKey(d)) return new List<Tuple<string, string>> { new Tuple<string, string>(m,d) };
    //    foreach( var key in nexts.Keys)
    //    {
    //        if (_visited.Contains(key)) continue;
    //        var paths = Find(key, d);
    //        if (paths.Count == 0) continue;

    //        paths.Insert(0, new Tuple<string, string>(m, key));
    //        return paths;
    //    }

    //    return new List<Tuple<string, string>>();
    //}

    //private Dictionary<string, Dictionary<string, double>> _map = new Dictionary<string, Dictionary<string, double>>();
    //private HashSet<string> _visited = new HashSet<string>();
}
/*
🎦 399. 除法求值
力扣 (LeetCode)

发布于 2021-01-06
23.0k
📺 视频讲解

📖 文字解析
这道题是在「力扣」第 990 题（等式方程的可满足性）的基础上，在变量和变量之间有了倍数关系。由于 变量之间的倍数关系具有传递性，处理有传递性关系的问题，可以使用「并查集」，我们需要在并查集的「合并」与「查询」操作中 维护这些变量之间的倍数关系。

说明：请大家注意看一下题目中的「注意」和「数据范围」，例如：每个 Ai 或 Bi 是一个表示单个变量的字符串。所以用例 equation = ["ab", "cd"] ，这里的 ab 视为一个变量，不表示 a * b。如果面试中遇到这样的问题，一定要和面试官确认清楚题目的条件。还有 1 <= equations.length <= 20 和 values[i] > 0.0 可以避免一些特殊情况的讨论。

方法：并查集
分析示例 1：

a / b = 2.0 说明 a = 2ba=2b， a 和 b 在同一个集合中；

b / c = 3.0 说明 b = 3cb=3c ，b 和 c 在同一个集合中。

求 \cfrac{a}{c} 
c
a
​	
  ，可以把 a = 2ba=2b，b = 3cb=3c 依次代入，得到 \cfrac{a}{c} = \cfrac{2b} {c} = \cfrac{2 \cdot 3c} {c} = 6.0 
c
a
​	
 = 
c
2b
​	
 = 
c
2⋅3c
​	
 =6.0；

求 \cfrac{b}{a} 
a
b
​	
  ，很显然根据 a = 2ba=2b，知道 \cfrac{b}{a} = 0.5 
a
b
​	
 =0.5，也可以把 bb 和 aa 都转换成为 cc 的倍数，\cfrac{b}{a} = \cfrac{b} {2b} = \cfrac{3c} {6c} = \cfrac{1}{2} = 0.5 
a
b
​	
 = 
2b
b
​	
 = 
6c
3c
​	
 = 
2
1
​	
 =0.5；

我们计算了两个结果，不难知道：可以将题目给出的 equation 中的两个变量所在的集合进行「合并」，同在一个集合中的两个变量就可以通过某种方式计算出它们的比值。具体来说，可以把 不同的变量的比值转换成为相同的变量的比值，这样在做除法的时候就可以消去相同的变量，然后再计算转换成相同变量以后的系数的比值，就是题目要求的结果。统一了比较的标准，可以以 O(1)O(1) 的时间复杂度完成计算。

如果两个变量不在同一个集合中， 返回 -1.0−1.0。并且根据题目的意思，如果两个变量中 至少有一个 变量没有出现在所有 equations 出现的字符集合中，也返回 -1.0−1.0。

构建有向图
通过例 1 的分析，我们就知道了，题目给出的 equations 和 values 可以表示成一个图，equations 中出现的变量就是图的顶点，「分子」于「分母」的比值可以表示成一个有向关系（因为「分子」和「分母」是有序的，不可以对换），并且这个图是一个带权图，values 就是对应的有向边的权值。例 1 中给出的 equations 和 values 表示的「图形表示」、「数学表示」和「代码表示」如下表所示。其中 parent[a] = b 表示：结点 a 的（直接）父亲结点是 b，与之对应的有向边的权重，记为 weight[a] = 2.0，即 weight[a] 表示结点 a 到它的 直接父亲结点 的有向边的权重。



「统一变量」与「路径压缩」的关系
刚刚在分析例 1 的过程中，提到了：可以把一个一个 query 中的不同变量转换成 同一个变量，这样在计算 query 的时候就可以以 O(1)O(1) 的时间复杂度计算出结果，在「并查集」的一个优化技巧中，「路径压缩」就恰好符合了这样的应用场景。

为了避免并查集所表示的树形结构高度过高，影响查询性能。「路径压缩」就是针对树的高度的优化。「路径压缩」的效果是：在查询一个结点 a 的根结点同时，把结点 a 到根结点的沿途所有结点的父亲结点都指向根结点。如下图所示，除了根结点以外，所有的结点的父亲结点都指向了根结点。特别地，也可以认为根结点的父亲结点就是根结点自己。如下国所示：路径压缩前后，并查集所表示的两棵树形结构等价，路径压缩以后的树的高度为 22，查询性能最好。

image.png

由于有「路径压缩」的优化，两个同在一个连通分量中的不同的变量，它们分别到根结点（父亲结点）的权值的比值，就是题目的要求的结果。

如何在「查询」操作的「路径压缩」优化中维护权值变化
如下图所示，我们在结点 a 执行一次「查询」操作。路径压缩会先一层一层向上先找到根结点 d，然后依次把 c、b 、a 的父亲结点指向根结点 d。

c 的父亲结点已经是根结点了，它的权值不用更改；
b 的父亲结点要修改成根结点，它的权值就是从当前结点到根结点经过的所有有向边的权值的乘积，因此是 3.03.0 乘以 4.04.0 也就是 12.012.0；
a 的父亲结点要修改成根结点，它的权值就是依然是从当前结点到根结点经过的所有有向边的权值的乘积，但是我们 没有必要把这三条有向边的权值乘起来，这是因为 b 到 c，c 到 d 这两条有向边的权值的乘积，我们在把 b 指向 d 的时候已经计算出来了。因此，a 到根结点的权值就等于 b 到根结点 d 的新的权值乘以 a 到 b 的原来的有向边的权值。
image.png

如何在「合并」操作中维护权值的变化
「合并」操作基于这样一个 很重要的前提：我们将要合并的两棵树的高度最多为 22，换句话说两棵树都必需是「路径压缩」以后的效果，两棵树的叶子结点到根结点最多只需要经过一条有向边。

例如已知 \cfrac{a}{b} = 3.0 
b
a
​	
 =3.0，\cfrac{d}{c} = 4.0 
c
d
​	
 =4.0 ，又已知 \cfrac{a}{d} = 6.0 
d
a
​	
 =6.0 ，现在合并结点 a 和 d 所在的集合，其实就是把 a 的根结点 b 指向 d 的根结 c，那么如何计算 b 指向 c 的这条有向边的权重呢？

根据 a 经过 b 可以到达 c，a 经过 d 也可以到达 c，因此 两条路径上的有向边的权值的乘积是一定相等的。设 b 到 c 的权值为 xx，那么 3.0 \cdot x = 6.0 \cdot 4.03.0⋅x=6.0⋅4.0 ，得 x = 8.0x=8.0。

image.png

一个容易忽略的细节
接下来还有一个小的细节问题：在合并以后，产生了一棵高度为 33 的树，那么我们在执行查询的时候，例如下图展示的绿色结点和黄色结点，绿色结点并不直接指向根结点，在计算这两个变量的比值的时候，计算边的权值的比值得到的结果是不对的。

image.png

但其实不用担心这个问题，并查集的「查询」操作会执行「路径压缩」，所以真正在计算两个变量的权值的时候，绿色结点已经指向了根结点，和黄色结点的根结点相同。因此可以用它们指向根结点的有向边的权值的比值作为两个变量的比值。

image.png

我们通过这个细节向大家强调：一边查询一边修改结点指向是并查集的特色。

参考代码：


import java.util.HashMap;
import java.util.List;
import java.util.Map;

public class Solution {

    public double[] calcEquation(List<List<String>> equations, double[] values, List<List<String>> queries) {
        int equationsSize = equations.size();

        UnionFind unionFind = new UnionFind(2 * equationsSize);
        // 第 1 步：预处理，将变量的值与 id 进行映射，使得并查集的底层使用数组实现，方便编码
        Map<String, Integer> hashMap = new HashMap<>(2 * equationsSize);
        int id = 0;
        for (int i = 0; i < equationsSize; i++) {
            List<String> equation = equations.get(i);
            String var1 = equation.get(0);
            String var2 = equation.get(1);

            if (!hashMap.containsKey(var1)) {
                hashMap.put(var1, id);
                id++;
            }
            if (!hashMap.containsKey(var2)) {
                hashMap.put(var2, id);
                id++;
            }
            unionFind.union(hashMap.get(var1), hashMap.get(var2), values[i]);
        }

        // 第 2 步：做查询
        int queriesSize = queries.size();
        double[] res = new double[queriesSize];
        for (int i = 0; i < queriesSize; i++) {
            String var1 = queries.get(i).get(0);
            String var2 = queries.get(i).get(1);

            Integer id1 = hashMap.get(var1);
            Integer id2 = hashMap.get(var2);

            if (id1 == null || id2 == null) {
                res[i] = -1.0d;
            } else {
                res[i] = unionFind.isConnected(id1, id2);
            }
        }
        return res;
    }

    private class UnionFind {

        private int[] parent;

        // 指向的父结点的权值
        private double[] weight;


        public UnionFind(int n) {
            this.parent = new int[n];
            this.weight = new double[n];
            for (int i = 0; i < n; i++) {
                parent[i] = i;
                weight[i] = 1.0d;
            }
        }

        public void union(int x, int y, double value) {
            int rootX = find(x);
            int rootY = find(y);
            if (rootX == rootY) {
                return;
            }

            parent[rootX] = rootY;
          	// 关系式的推导请见「参考代码」下方的示意图
            weight[rootX] = weight[y] * value / weight[x];
        }


         //路径压缩
         // @param x
         // @return 根结点的 id
        public int find(int x) {
            if (x != parent[x]) {
                int origin = parent[x];
                parent[x] = find(parent[x]);
                weight[x] *= weight[origin];
            }
            return parent[x];
        }

        public double isConnected(int x, int y) {
            int rootX = find(x);
            int rootY = find(y);
            if (rootX == rootY) {
                return weight[x] / weight[y];
            } else {
                return -1.0d;
            }
        }
    }
}
说明：代码 weight[rootX] = weight[y] * value / weight[x]; 的推导过程，主要需要明白各个变量的含义，由两条路径有向边的权值乘积相等得到相等关系，然后做等价变换即可。

image.png

复杂度分析：

时间复杂度：O((N + Q)\log A)O((N+Q)logA)，

构建并查集 O(N \log A)O(NlogA) ，这里 NN 为输入方程 equations 的长度，每一次执行合并操作的时间复杂度是 O(\log A)O(logA)，这里 AA 是 equations 里不同字符的个数；
查询并查集 O(Q \log A)O(QlogA)，这里 QQ 为查询数组 queries 的长度，每一次查询时执行「路径压缩」的时间复杂度是 O(\log A)O(logA)。
空间复杂度：O(A)O(A)：创建字符与 id 的对应关系 hashMap 长度为 AA，并查集底层使用的两个数组 parent 和 weight 存储每个变量的连通分量信息，parent 和 weight 的长度均为 AA。

练习
「力扣」第 547 题：省份数量（中等）；
「力扣」第 684 题：冗余连接（中等）；
「力扣」第 1319 题：连通网络的操作次数（中等）；
「力扣」第 1631 题：最小体力消耗路径（中等）；
「力扣」第 959 题：由斜杠划分区域（中等）；
「力扣」第 1202 题：交换字符串中的元素（中等）；
「力扣」第 947 题：移除最多的同行或同列石头（中等）；
「力扣」第 721 题：账户合并（中等）；
「力扣」第 803 题：打砖块（困难）；
「力扣」第 1579 题：保证图可完全遍历（困难）;
「力扣」第 778 题：水位上升的泳池中游泳（困难）。

除法求值
力扣官方题解
发布于 2021-01-05
19.3k
方法一：广度优先搜索
我们可以将整个问题建模成一张图：给定图中的一些点（变量），以及某些边的权值（两个变量的比值），试对任意两点（两个变量）求出其路径长（两个变量的比值）。

因此，我们首先需要遍历 \textit{equations}equations 数组，找出其中所有不同的字符串，并通过哈希表将每个不同的字符串映射成整数。

在构建完图之后，对于任何一个查询，就可以从起点出发，通过广度优先搜索的方式，不断更新起点与当前点之间的路径长度，直到搜索到终点为止。


class Solution {
    public double[] calcEquation(List<List<String>> equations, double[] values, List<List<String>> queries) {
        int nvars = 0;
        Map<String, Integer> variables = new HashMap<String, Integer>();

        int n = equations.size();
        for (int i = 0; i < n; i++) {
            if (!variables.containsKey(equations.get(i).get(0))) {
                variables.put(equations.get(i).get(0), nvars++);
            }
            if (!variables.containsKey(equations.get(i).get(1))) {
                variables.put(equations.get(i).get(1), nvars++);
            }
        }

        // 对于每个点，存储其直接连接到的所有点及对应的权值
        List<Pair>[] edges = new List[nvars];
        for (int i = 0; i < nvars; i++) {
            edges[i] = new ArrayList<Pair>();
        }
        for (int i = 0; i < n; i++) {
            int va = variables.get(equations.get(i).get(0)), vb = variables.get(equations.get(i).get(1));
            edges[va].add(new Pair(vb, values[i]));
            edges[vb].add(new Pair(va, 1.0 / values[i]));
        }

        int queriesCount = queries.size();
        double[] ret = new double[queriesCount];
        for (int i = 0; i < queriesCount; i++) {
            List<String> query = queries.get(i);
            double result = -1.0;
            if (variables.containsKey(query.get(0)) && variables.containsKey(query.get(1))) {
                int ia = variables.get(query.get(0)), ib = variables.get(query.get(1));
                if (ia == ib) {
                    result = 1.0;
                } else {
                    Queue<Integer> points = new LinkedList<Integer>();
                    points.offer(ia);
                    double[] ratios = new double[nvars];
                    Arrays.fill(ratios, -1.0);
                    ratios[ia] = 1.0;

                    while (!points.isEmpty() && ratios[ib] < 0) {
                        int x = points.poll();
                        for (Pair pair : edges[x]) {
                            int y = pair.index;
                            double val = pair.value;
                            if (ratios[y] < 0) {
                                ratios[y] = ratios[x] * val;
                                points.offer(y);
                            }
                        }
                    }
                    result = ratios[ib];
                }
            }
            ret[i] = result;
        }
        return ret;
    }
}

class Pair {
    int index;
    double value;

    Pair(int index, double value) {
        this.index = index;
        this.value = value;
    }
}
复杂度分析

时间复杂度：O(ML+Q\cdot(L+M))O(ML+Q⋅(L+M))，其中 MM 为边的数量，QQ 为询问的数量，LL 为字符串的平均长度。构建图时，需要处理 MM 条边，每条边都涉及到 O(L)O(L) 的字符串比较；处理查询时，每次查询首先要进行一次 O(L)O(L) 的比较，然后至多遍历 O(M)O(M) 条边。

空间复杂度：O(NL+M)O(NL+M)，其中 NN 为点的数量，MM 为边的数量，LL 为字符串的平均长度。为了将每个字符串映射到整数，需要开辟空间为 O(NL)O(NL) 的哈希表；随后，需要花费 O(M)O(M) 的空间存储每条边的权重；处理查询时，还需要 O(N)O(N) 的空间维护访问队列。最终，总的复杂度为 O(NL+M+N) = O(NL+M)O(NL+M+N)=O(NL+M)。

方法二：\text{Floyd}Floyd 算法
对于查询数量很多的情形，如果为每次查询都独立搜索一次，则效率会变低。为此，我们不妨对图先做一定的预处理，随后就可以在较短的时间内回答每个查询。

在本题中，我们可以使用 \text{Floyd}Floyd 算法，预先计算出任意两点之间的距离。


class Solution {
    public double[] calcEquation(List<List<String>> equations, double[] values, List<List<String>> queries) {
        int nvars = 0;
        Map<String, Integer> variables = new HashMap<String, Integer>();

        int n = equations.size();
        for (int i = 0; i < n; i++) {
            if (!variables.containsKey(equations.get(i).get(0))) {
                variables.put(equations.get(i).get(0), nvars++);
            }
            if (!variables.containsKey(equations.get(i).get(1))) {
                variables.put(equations.get(i).get(1), nvars++);
            }
        }
        double[][] graph = new double[nvars][nvars];
        for (int i = 0; i < nvars; i++) {
            Arrays.fill(graph[i], -1.0);
        }
        for (int i = 0; i < n; i++) {
            int va = variables.get(equations.get(i).get(0)), vb = variables.get(equations.get(i).get(1));
            graph[va][vb] = values[i];
            graph[vb][va] = 1.0 / values[i];
        }

        for (int k = 0; k < nvars; k++) {
            for (int i = 0; i < nvars; i++) {
                for (int j = 0; j < nvars; j++) {
                    if (graph[i][k] > 0 && graph[k][j] > 0) {
                        graph[i][j] = graph[i][k] * graph[k][j];
                    }
                }
            }
        }

        int queriesCount = queries.size();
        double[] ret = new double[queriesCount];
        for (int i = 0; i < queriesCount; i++) {
            List<String> query = queries.get(i);
            double result = -1.0;
            if (variables.containsKey(query.get(0)) && variables.containsKey(query.get(1))) {
                int ia = variables.get(query.get(0)), ib = variables.get(query.get(1));
                if (graph[ia][ib] > 0) {
                    result = graph[ia][ib];
                }
            }
            ret[i] = result;
        }
        return ret;
    }
}
复杂度分析

时间复杂度：O(ML+N^3+QL)O(ML+N 
3
 +QL)。构建图需要 O(ML)O(ML) 的时间；\text{Floyd}Floyd 算法需要 O(N^3)O(N 
3
 ) 的时间；处理查询时，单次查询只需要 O(L)O(L) 的字符串比较以及常数时间的额外操作。

空间复杂度：O(NL+N^2)O(NL+N 
2
 )。

方法三：带权并查集
我们还可以考虑以并查集的方式存储节点之间的关系。设节点 xx 的值（即对应变量的取值）为 v[x]v[x]。对于任意两点 x, yx,y，假设它们在并查集中具有共同的父亲 ff，且 v[x]/v[f] = a, v[y]/v[f]=bv[x]/v[f]=a,v[y]/v[f]=b，则 v[x]/v[y]=a/bv[x]/v[y]=a/b。

在观察到这一点后，就不难利用并查集的思想解决此题。对于每个节点 xx 而言，除了维护其父亲 f[x]f[x] 之外，还要维护其权值 ww，其中「权值」定义为节点 xx 的取值与父亲 f[x]f[x] 的取值之间的比值。换言之，我们有

w[x] = \frac{v[x]}{v[f[x]]}
w[x]= 
v[f[x]]
v[x]
​	
 

下面，我们对并查集的两种操作的实现细节做出讨论。

当查询节点 xx 父亲时，如果 f[x] \ne xf[x] 

​	
 =x，我们需要先找到 f[x]f[x] 的父亲 \textit{father}father，并将 f[x]f[x] 更新为 \textit{father}father。此时，我们有

\begin{aligned} w[x] &\leftarrow \frac{v[x]}{v[\textit{father}]} \\ &= \frac{v[x]}{v[f[x]]} \cdot \frac{v[f[x]]}{v[\textit{father}]} \\ &= w[i] \cdot w[f[x]] \end{aligned}
w[x]
​	
  
← 
v[father]
v[x]
​	
 
= 
v[f[x]]
v[x]
​	
 ⋅ 
v[father]
v[f[x]]
​	
 
=w[i]⋅w[f[x]]
​	
 

也就是说，我们要将 w[x]w[x] 更新为 w[x] \cdot w[f[x]]w[x]⋅w[f[x]]。

当合并两个节点 x,yx,y 时，我们首先找到两者的父亲 f_x, f_yf 
x
​	
 ,f 
y
​	
 ，并将 f[f_x]f[f 
x
​	
 ] 更新为 f_yf 
y
​	
 。此时，我们有

\begin{aligned} w[f_x] &\leftarrow \frac{v[f_x]}{v[f_y]} \\ &= \frac{v[x]/w[x]}{v[y]/w[y]} \\ &= \frac{v[x]}{v[y]} \cdot \frac{w[y]}{w[x]} \end{aligned}
w[f 
x
​	
 ]
​	
  
← 
v[f 
y
​	
 ]
v[f 
x
​	
 ]
​	
 
= 
v[y]/w[y]
v[x]/w[x]
​	
 
= 
v[y]
v[x]
​	
 ⋅ 
w[x]
w[y]
​	
 
​	
 

也就是说，当在已有的图中添加一条方程式 \frac{v[x]}{v[y]}=k 
v[y]
v[x]
​	
 =k 时，需要将 w[f_x]w[f 
x
​	
 ] 更新为 k\cdot \frac{w[y]}{w[x]}k⋅ 
w[x]
w[y]
​	
 。


class Solution {
    public double[] calcEquation(List<List<String>> equations, double[] values, List<List<String>> queries) {
        int nvars = 0;
        Map<String, Integer> variables = new HashMap<String, Integer>();

        int n = equations.size();
        for (int i = 0; i < n; i++) {
            if (!variables.containsKey(equations.get(i).get(0))) {
                variables.put(equations.get(i).get(0), nvars++);
            }
            if (!variables.containsKey(equations.get(i).get(1))) {
                variables.put(equations.get(i).get(1), nvars++);
            }
        }
        int[] f = new int[nvars];
        double[] w = new double[nvars];
        Arrays.fill(w, 1.0);
        for (int i = 0; i < nvars; i++) {
            f[i] = i;
        }

        for (int i = 0; i < n; i++) {
            int va = variables.get(equations.get(i).get(0)), vb = variables.get(equations.get(i).get(1));
            merge(f, w, va, vb, values[i]);
        }
        int queriesCount = queries.size();
        double[] ret = new double[queriesCount];
        for (int i = 0; i < queriesCount; i++) {
            List<String> query = queries.get(i);
            double result = -1.0;
            if (variables.containsKey(query.get(0)) && variables.containsKey(query.get(1))) {
                int ia = variables.get(query.get(0)), ib = variables.get(query.get(1));
                int fa = findf(f, w, ia), fb = findf(f, w, ib);
                if (fa == fb) {
                    result = w[ia] / w[ib];
                }
            }
            ret[i] = result;
        }
        return ret;
    }

    public void merge(int[] f, double[] w, int x, int y, double val) {
        int fx = findf(f, w, x);
        int fy = findf(f, w, y);
        f[fx] = fy;
        w[fx] = val * w[y] / w[x];
    }

    public int findf(int[] f, double[] w, int x) {
        if (f[x] != x) {
            int father = findf(f, w, f[x]);
            w[x] = w[x] * w[f[x]];
            f[x] = father;
        }
        return f[x];
    }
}
复杂度分析

时间复杂度：O(ML+N+M\log N+Q\cdot(L+\log N))O(ML+N+MlogN+Q⋅(L+logN))。构建图需要 O(ML)O(ML) 的时间；初始化并查集需要 O(N)O(N) 的初始化时间；构建并查集的单次操作复杂度为 O(\log N)O(logN)，共需 O(M\log N)O(MlogN) 的时间；每个查询需要 O(L)O(L) 的字符串比较以及 O(\log N)O(logN) 的查询。

空间复杂度：O(NL)O(NL)。哈希表需要 O(NL)O(NL) 的空间，并查集需要 O(N)O(N) 的空间。



public class Solution {
    public double[] CalcEquation(IList<IList<string>> equations, double[] values, IList<IList<string>> queries) {
      var dic = new Dictionary<string, Dictionary<string, double>>();
      for (int i = 0; i < values.Length; i++)
      {
        var pair = equations[i];
        if(dic.TryGetValue(pair[0], out var ele1))
        {
          ele1.Add(pair[1], values[i]);
        }
        else
        {
          dic.Add(pair[0], new Dictionary<string, double>{{pair[1], values[i]}});
        }
        if(dic.TryGetValue(pair[1], out var ele2))
        {
          ele2.Add(pair[0], 1/values[i]);
        }
        else
        {
          dic.Add(pair[1], new Dictionary<string, double>{{pair[0], 1/values[i]}});
        }
      }
      var res = new List<double>();
      for (int i = 0; i < queries.Count; i++)
      {
        var ele1 = queries[i][0];
        var ele2 = queries[i][1];
        res.Add( dfs(ele1, ele2, new List<string>(), dic));
      }
      return res.ToArray();
            
    }
    
    public double dfs(string ele1, string ele2, List<string> visited, Dictionary<string, Dictionary<string, double>> dic)
    {
      if(!dic.TryGetValue(ele1, out var var1) || !dic.TryGetValue(ele2, out var var2)) return -1;
      if(dic[ele1].TryGetValue(ele2, out var target)) return target;
      if(ele1.Equals(ele2)) return 1;
      visited.Add(ele1);
      foreach(KeyValuePair<string, double> division in dic[ele1])
      {
        if (visited.Contains(division.Key)) continue;
        var result = division.Value * dfs(division.Key, ele2, visited, dic);
         if(result > 0) return result;
        // return result;
      }
      return -1;
    }
}
 
*/