using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
对于一个具有树特征的无向图，我们可选择任何一个节点作为根。图因此可以成为树，在所有可能的树中，具有最小高度的树被称为最小高度树。给出这样的一个图，写出一个函数找到所有的最小高度树并返回他们的根节点。

格式

该图包含 n 个节点，标记为 0 到 n - 1。给定数字 n 和一个无向边 edges 列表（每一个边都是一对标签）。

你可以假设没有重复的边会出现在 edges 中。由于所有的边都是无向边， [0, 1]和 [1, 0] 是相同的，因此不会同时出现在 edges 里。

示例 1:

输入: n = 4, edges = [[1, 0], [1, 2], [1, 3]]

        0
        |
        1
       / \
      2   3 

输出: [1]
示例 2:

输入: n = 6, edges = [[0, 3], [1, 3], [2, 3], [4, 3], [5, 4]]

     0  1  2
      \ | /
        3
        |
        4
        |
        5 

输出: [3, 4]
说明:

 根据树的定义，树是一个无向图，其中任何两个顶点只通过一条路径连接。 换句话说，一个任何没有简单环路的连通图都是一棵树。
树的高度是指根节点和叶子节点之间最长向下路径上边的数量。
*/
/// <summary>
/// https://leetcode-cn.com/problems/minimum-height-trees/
/// 310. 最小高度树
/// http://www.cnblogs.com/grandyang/p/5000291.html
/// http://www.cnblogs.com/TonyYPZhang/p/5123058.html
/// https://blog.csdn.net/weixin_37373020/article/details/81109439
/// </summary>
class MinimumHeightTreesSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public IList<int> FindMinHeightTrees(int n, int[,] edges)
    {
        if (edges == null) return new List<int>();

        int m = edges.GetLength(0);
        Queue<int> nodes = new Queue<int>(m);
        Dictionary<int, HashSet<int>> map = new Dictionary<int, HashSet<int>>(m);

        for (int i = 0; i < n; i++) {
            nodes.Enqueue(i);
            map[i] = new HashSet<int>();
        }
        
        for ( int i = 0; i < m; i++ )
        {
            var start = edges[i, 0];
            var stop = edges[i, 1];
            map[start].Add(stop);
            map[stop].Add(start);
        }

        Queue<int> toRemove = new Queue<int>();
        while( 2 < nodes.Count)
        {
            int currentCount = nodes.Count;
            int index = 0;
            while ( index++ < currentCount )
            {
                var i = nodes.Dequeue();
                if (map[i].Count == 1) {
                    toRemove.Enqueue(i);
                    continue;
                }
                nodes.Enqueue(i);
            }
            //foreach (var i in map.Keys ) if (map[i].Count == 1) toRemove.Enqueue(i);
            while( 0 < toRemove.Count)
            {
                var i = toRemove.Dequeue();
                map[map[i].First()].Remove(i);
                //map.Remove(i);
            }
        }
        return nodes.ToArray();
    }
}
/*
public class Solution {
    public IList<int> FindMinHeightTrees(int n, int[,] edges) {
            IList<int> ret = new List<int>();
            if (n == 1)
            {
                ret.Add(0);
                return ret;
            }
            int row = edges.GetLength(0);
            IDictionary<int, GraphEntity> graphDic = new Dictionary<int, GraphEntity>();
            for (int i = 0; i < row; i++)
            {
                int x = edges[i, 0], y = edges[i, 1];
                GraphEntity gX, gY;
                if (graphDic.ContainsKey(x))
                {
                    gX = graphDic[x];
                }
                else
                {
                    gX = new GraphEntity(x);
                    graphDic.Add(x, gX);
                }
                if (graphDic.ContainsKey(y))
                {
                    gY = graphDic[y];
                }
                else
                {
                    gY = new GraphEntity(y);
                    graphDic.Add(y, gY);
                }
                gX.Neibor.Add(gY);
                gY.Neibor.Add(gX);
            }
            while (graphDic.Count > 2)
            {
                IList<int> deleteK = new List<int>();
                foreach (var k in graphDic.Keys)
                {
                    if (graphDic[k].Neibor.Count == 1)
                    {
                        deleteK.Add(k);
                    }
                }
                foreach (var k in deleteK)
                {
                    graphDic[graphDic[k].Neibor[0].V].Neibor.Remove(graphDic[k]);
                    graphDic.Remove(k);
                }
            }
            foreach (var k in graphDic.Keys)
            {
                ret.Add(k);
            }
            return ret;
    }
    
        /// <summary>
    /// 图形
    /// </summary>
    public class GraphEntity
    {

        public GraphEntity(int v)
        {
            V = v;
            Neibor = new List<GraphEntity>();
        }

        /// <summary>
        /// 顶点编号
        /// </summary>
        public int V
        {
            get;
            set;
        }

        /// <summary>
        /// 临接图形集合
        /// </summary>
        public IList<GraphEntity> Neibor
        {
            get;
            set;
        }
    }
} 
*/
