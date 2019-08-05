using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
在本问题中, 树指的是一个连通且无环的无向图。

输入一个图，该图由一个有着N个节点 (节点值不重复1, 2, ..., N) 的树及一条附加的边构成。附加的边的两个顶点包含在1到N中间，这条附加的边不属于树中已存在的边。

结果图是一个以边组成的二维数组。每一个边的元素是一对[u, v] ，满足 u < v，表示连接顶点u 和v的无向图的边。

返回一条可以删去的边，使得结果图是一个有着N个节点的树。如果有多个答案，则返回二维数组中最后出现的边。答案边 [u, v] 应满足相同的格式 u < v。

示例 1：

输入: [[1,2], [1,3], [2,3]]
输出: [2,3]
解释: 给定的无向图为:
  1
 / \
2 - 3
示例 2：

输入: [[1,2], [2,3], [3,4], [1,4], [1,5]]
输出: [1,4]
解释: 给定的无向图为:
5 - 1 - 2
    |   |
    4 - 3
注意:

输入的二维数组大小在 3 到 1000。
二维数组中的整数在1到N之间，其中N是输入数组的大小。
更新(2017-09-26):
我们已经重新检查了问题描述及测试用例，明确图是无向 图。对于有向图详见冗余连接II。对于造成任何不便，我们深感歉意。 
*/
/// <summary>
/// https://leetcode-cn.com/problems/redundant-connection/
/// 684. 冗余连接
/// https://blog.csdn.net/gl486546/article/details/79780126
/// https://www.cnblogs.com/kexinxin/p/10400341.html
/// </summary>
class RedundantConnectionSolution
{
    public void Test()
    {
        var ret = FindRedundantConnection(new int[][] { new int[] { 1, 2 }, new int[] { 1, 3 }, new int[] { 2, 3 } });
        ret = FindRedundantConnection(new int[][] { new int[] { 1, 2 }, new int[] { 2, 3 }, new int[] { 3, 4 }, new int[] { 1, 4 }, new int[] { 1, 5 } });
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int[] FindRedundantConnection(int[][] edges)
    {
        int n = edges.Length;
        int[] parents = new int[n+1];
        for (int i = 1; i <= n; ++i)
        {
            parents[i] = i;
        }
        foreach (var e in edges)
        {
            int x = e[0];
            int y = e[1];

            int l1 = Find(x, parents);
            int l2 = Find(y, parents);

            if (l1 == l2) return e;

            parents[l1] = l2;
        }
        return new int[]{ -1,-1};
    }
    private static int Find(int x, int[] parents)
    {
        return x == parents[x] ? x : parents[x] = Find(parents[x], parents);
    }
}
/*
public class Solution {
    public int[] FindRedundantConnection(int[][] edges) {
        int n = edges.Length;
        UnionFind uf = new UnionFind(n);
        
        foreach (var e in edges)
        {
            // Console.WriteLine("Check connect {e[0]} and {e[1]}");
            if (uf.Find(e[0]) == uf.Find(e[1]))
            {
                return e;
            }
            else
            {
                // Console.WriteLine("Union {e[0]} and {e[1]}");
                uf.Union(e[0], e[1]);
            }
        }
        
        return null;
    }
    
    public class UnionFind
    {
        int[] a = null;
        int[] rank = null;
        public UnionFind(int n)
        {
            a = new int[n + 1];
            rank = new int[n + 1];
            for (int i = 0; i <= n; i++)
            {
                a[i] = i;
                rank[i] = 1;
            }
        }
        
        public int Find(int s)
        {
            while (s != a[s])
            {
                s = a[s];
            }
            return s;
        }
        
        public void Union(int s, int t)
        {
            int sID = Find(s);
            int tID = Find(t);
            if (sID == tID) return;          
            if (rank[sID] <= rank[tID])
            {
                a[sID] = tID;
                rank[tID] += rank[sID];
            }
            else
            {
                a[tID] = sID;
                rank[sID] += rank[tID];
            }
        }
    }
} 
ublic class Solution {
    public int[] FindRedundantConnection(int[][] edges) {
            var tempEdges = edges.ToList();
            var index = new List<int>();
            var res = new int[2];
            for(int i=0;i<2000;i++){
                index.Add(i);
            }
            tempEdges.ForEach(t=>{
                int node1 = t[0];
                int node2 = t[1];
                while(node1!=index[node1])
                {
                    node1=index[node1];
                }
                while(node2!=index[node2])
                {
                    node2=index[node2];
                }
                if(node1==node2)
                {
                    res=t;
                }
                else
                    index[node1]=node2;
            });
          
            return res;
    }
}
*/
