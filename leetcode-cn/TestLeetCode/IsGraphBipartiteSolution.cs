using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个无向图graph，当这个图为二分图时返回true。

如果我们能将一个图的节点集合分割成两个独立的子集A和B，并使图中的每一条边的两个节点一个来自A集合，一个来自B集合，我们就将这个图称为二分图。

graph将会以邻接表方式给出，graph[i]表示图中与节点i相连的所有节点。每个节点都是一个在0到graph.Length-1之间的整数。这图中没有自环和平行边： graph[i] 中不存在i，并且graph[i]中没有重复的值。


示例 1:
输入: [[1,3], [0,2], [1,3], [0,2]]
输出: true
解释: 
无向图如下:
0----1
|    |
|    |
3----2
我们可以将节点分成两组: {0, 2} 和 {1, 3}。

示例 2:
输入: [[1,2,3], [0,2], [0,1,3], [0,2]]
输出: false
解释: 
无向图如下:
0----1
| \  |
|  \ |
3----2
我们不能将节点分割成两个独立的子集。
注意:

graph 的长度范围为 [1, 100]。
graph[i] 中的元素的范围为 [0, graph.Length - 1]。
graph[i] 不会包含 i 或者有重复的值。
图是无向的: 如果j 在 graph[i]里边, 那么 i 也会在 graph[j]里边。
*/
/// <summary>
/// https://leetcode-cn.com/problems/is-graph-bipartite/
/// 785. 判断二分图
/// http://www.mamicode.com/info-detail-2488979.html
/// </summary>
class IsGraphBipartiteSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public bool IsBipartite(int[][] graph)
    {
        const byte Black = 0;
        const byte Green = 1;
        const byte Red = 2;
        //default 0: not visited;
        //lable 1: green
        //lable 2: red    

        var visited = new byte[graph.Length];
        Array.Fill<byte>(visited, Black);

        Queue<int> queue = new Queue<int>();
        for (int i = 0; i < graph.Length; i++)
        {
            // has been visited
            if (0 < visited[i]) continue;

            queue.Enqueue(i);
            // mark as green
            visited[i] = Green;
            while ( 0 < queue.Count)
            {
                int index = queue.Dequeue();
                var lable = visited[index];
                // if currentLable is green, fill neighborLable to red
                byte neighborLable = lable == Green ? Red : Green;
                foreach (int neighbor in graph[index])
                {
                    var l = visited[neighbor];
                    //such node has not been visited
                    if (l == Black)
                    {
                        visited[neighbor] = neighborLable;
                        queue.Enqueue(neighbor);
                        continue;
                    }
                    
                    // node visited, and visited[neighbor] != neighborLable, conflict happens
                    if (l != neighborLable) return false;
                }
            }
        }
        return true;
    }
}
/*
public class Solution {
    public bool IsBipartite(int[][] graph) {
        int n = graph.Length;
        
        int[] mark = new int[n];
        
        int start = GetStart(graph, mark);
        while(start >= 0) {
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(start);
        
            int flag = 1;
            while(queue.Count > 0) {
                int size = queue.Count;
                for (int i = 0; i < size; i ++) {
                    int tmp = queue.Dequeue();
                    if (mark[tmp] == 0) {
                        mark[tmp] = flag;
                        for (int j = 0; j < graph[tmp].Length; j ++) {
                            queue.Enqueue(graph[tmp][j]);
                        }
                    } else {
                        if (mark[tmp] != flag) {
                            return false;
                        }
                    }
                }
            
                if (flag == 1) {
                    flag = 2;
                } else {
                    flag = 1;
                }
            }
            
            start = GetStart(graph, mark);
        }
        return true;
    }
    
    int GetStart(int[][] graph, int[] mark) {
        int start = -1;
        int n = graph.Length;
        for (int i = 0; i < n; i ++) {
            if (graph[i].Length > 0 && mark[i] == 0) {
                start = i;
                break;
            }
        }
        return start;
    }
} 
*/
