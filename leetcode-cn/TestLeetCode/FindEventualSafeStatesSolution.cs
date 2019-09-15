using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
在有向图中, 我们从某个节点和每个转向处开始, 沿着图的有向边走。 如果我们到达的节点是终点 (即它没有连出的有向边), 我们停止。

现在, 如果我们最后能走到终点，那么我们的起始节点是最终安全的。 更具体地说, 存在一个自然数 K,  无论选择从哪里开始行走, 我们走了不到 K 步后必能停止在一个终点。

哪些节点最终是安全的？ 结果返回一个有序的数组。

该有向图有 N 个节点，标签为 0, 1, ..., N-1, 其中 N 是 graph 的节点数.  图以以下的形式给出: graph[i] 是节点 j 的一个列表，满足 (i, j) 是图的一条有向边。

示例：
输入：graph = [[1,2],[2,3],[5],[0],[5],[],[]]
输出：[2,4,5,6]
这里是上图的示意图。



提示：

graph 节点数不超过 10000.
图的边数不会超过 32000.
每个 graph[i] 被排序为不同的整数列表， 在区间 [0, graph.length - 1] 中选取。
在真实的面试中遇到过这道题？
*/
/// <summary>
/// https://leetcode-cn.com/problems/find-eventual-safe-states/
/// 802. 找到最终的安全状态
/// https://blog.csdn.net/weixin_43732798/article/details/99769825
/// </summary>
class FindEventualSafeStatesSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public IList<int> EventualSafeNodes(int[][] graph)
    {
        var ret = new List<int>();//记录结果
        int n = graph.Length;//长度
        byte[] type = new byte[n];//访问类型
        Array.Fill<byte>(type, NotVisited);
        for (int i = 0; i < n; i++)
        {
            if (DFS(graph, i, type) == NotRing) ret.Add(i);
        }
        return ret;
    }

    private const byte Ring = 3;
    private const byte NotRing = 2;
    private const byte Visited = 1;
    private const byte NotVisited = 0;

    private static int DFS(int[][] graph, int index, byte[] type)
    {
        var t = type[index];
        if (t == Visited) return Ring;//如果访问过了，说明成环
        if (t != NotVisited) return t;   //如果不是0，返回自身

        type[index] = Visited;//标记访问了
        foreach (int i in graph[index])
            if (DFS(graph, i, type) == Ring)
            {
                type[i] = Ring;
                return Ring;
            }

        type[index] = NotRing;//不成环
        return NotRing;
    }
}
/*
public class Solution {
        bool dfs(int[][] graph, int cur, List<int> color)
        {
            if(color[cur] > 0)
            {
                return color[cur] == 2;
            }
            color[cur] = 1;
            foreach(var i in graph[cur])
            {
                if(color[i] == 2)
                {
                    continue;
                }
                if(color[i] == 1 || !dfs(graph, i, color))
                {
                    return false;
                }
            }
            color[cur] = 2;
            return true;
        }
    public IList<int> EventualSafeNodes(int[][] graph) {
        int n = graph.Length;
        var res = new List<int>();
        var color = new List<int>(n);
        for(int i = 0; i < n; ++i)
        {
            color.Add(0);   
        }
        for(int i = 0; i < n; ++i)
        {
            if(dfs(graph, i, color))
            {
                res.Add(i);
            }
        }
        return res;
    }
} 
*/
