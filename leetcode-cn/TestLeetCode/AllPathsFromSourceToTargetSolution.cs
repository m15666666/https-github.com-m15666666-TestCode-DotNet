using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给一个有 n 个结点的有向无环图，找到所有从 0 到 n-1 的路径并输出（不要求按顺序）

二维数组的第 i 个数组中的单元都表示有向图中 i 号结点所能到达的下一些结点（译者注：有向图是有方向的，即规定了a→b你就不能从b→a）空就是没有下一个结点了。

示例:
输入: [[1,2], [3], [3], []] 
输出: [[0,1,3],[0,2,3]] 
解释: 图是这样的:
0--->1
|    |
v    v
2--->3
这有两条路: 0 -> 1 -> 3 和 0 -> 2 -> 3.
提示:

结点的数量会在范围 [2, 15] 内。
你可以把路径以任意顺序输出，但在路径内的结点的顺序必须保证。
*/
/// <summary>
/// https://leetcode-cn.com/problems/all-paths-from-source-to-target/
/// 797. 所有可能的路径
/// https://blog.csdn.net/qq_38612955/article/details/83119739
/// </summary>
class AllPathsFromSourceToTargetSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public IList<IList<int>> AllPathsSourceTarget(int[][] graph)
    {
        List<IList<int>> ret = new List<IList<int>>();
        Stack<int> stack = new Stack<int>();
        stack.Push(0);
        Dfs(graph, 0, ret, stack);

        return ret;
    }

    private static void Dfs(int[][] graph, int startIndex, List<IList<int>> ret, Stack<int> stack)
    {
        if(startIndex == graph.Length - 1)
        {
            ret.Add(stack.Reverse().ToArray());
            return;
        }

        foreach( var i in graph[startIndex])
        {
            stack.Push(i);
            Dfs(graph, i, ret, stack);
            stack.Pop();
        }
    }
}
/*
public class Solution {
    public IList<IList<int>> AllPathsSourceTarget(int[][] graph) {
        IList<IList<int>> r= new 
            List<IList<int>>();
        for(int i=0;i<graph[0].Length;
            ++i)
        {
            IList<int> l=
                new List<int>();
            l.Add(0);
            l.Add(graph[0][i]);
            r.Add(l);
        }
        for(int i=1;i<graph.Length;++i)
        {
            for(int j=0;j<r.Count;++j)
            {
                if(r[j][r[j].Count-1]
                  !=i)
                    continue;
            for(int k=1;
                k<graph[i].Length;++k)
            {
                IList<int> l=new 
                    List<int>(r[j]);
                l.Add(graph[i][k]);
                r.Add(l);
            }
            if(graph[i].Length>0)
                r[j].Add(graph[i][0]);
            }
        }
        for(int i=0;i<r.Count;)
        {
            if(r[i][r[i].Count-1]
              !=graph.Length-1)
            {    
                r.RemoveAt(i);
            }
            else
                ++i;
        }
        return r;
    }
}
public class Solution {
    public IList<IList<int>> AllPathsSourceTarget(int[][] graph) {
        
        int n = graph.Length;
        
        IList<IList<int>> res = new List<IList<int>>();
        
        IList<int> tempList = new List<int>();
        tempList.Add(0);
        
        Queue<IList<int>> queue = new Queue<IList<int>>();
        
        queue.Enqueue(tempList);
        
        while(queue.Count() > 0) {
            int size = queue.Count();
            for (int i = 0; i < size; i ++) {
                IList<int> list = queue.Dequeue();
                int lastNum = list[list.Count() - 1];
                if (lastNum == n - 1) {
                    res.Add(list);
                } else {
                    for (int j = 0; j < graph[lastNum].Length; j ++) {
                        List<int> newList = new List<int>(list);
                        newList.Add(graph[lastNum][j]);
                        queue.Enqueue(newList);
                    }
                }
            }
        }
        
        return res;
    }
}
*/
