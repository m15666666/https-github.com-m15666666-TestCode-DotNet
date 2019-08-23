using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
有 N 个网络节点，标记为 1 到 N。

给定一个列表 times，表示信号经过有向边的传递时间。 times[i] = (u, v, w)，其中 u 是源节点，v 是目标节点， w 是一个信号从源节点传递到目标节点的时间。

现在，我们向当前的节点 K 发送了一个信号。需要多久才能使所有节点都收到信号？如果不能使所有节点收到信号，返回 -1。

注意:

N 的范围在 [1, 100] 之间。
K 的范围在 [1, N] 之间。
times 的长度在 [1, 6000] 之间。
所有的边 times[i] = (u, v, w) 都有 1 <= u, v <= N 且 0 <= w <= 100。
*/
/// <summary>
/// https://leetcode-cn.com/problems/network-delay-time/
/// 743. 网络延迟时间
/// https://blog.csdn.net/xuxuxuqian1/article/details/80709191
/// </summary>
class NetworkDelayTimeSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int NetworkDelayTime(int[][] times, int N, int K)
    {
        int[] dist = new int[N +1];
        Array.Fill(dist, int.MaxValue);
        dist[0] = 0;
        dist[K] = 0;
        for (; ; )
        {
            bool changed = false;
            foreach (var e in times)
            {
                int u = e[0], v = e[1], w = e[2];
                if (dist[u] != int.MaxValue && dist[v] > dist[u] + w)
                {
                    dist[v] = dist[u] + w;
                    changed = true;
                }
            }
            if (!changed) break;
        }

        int ret = dist.Max();
        return ret == int.MaxValue ? -1 : ret;
    }
}
/*
public class Solution {
    public int NetworkDelayTime(int[][] times, int N, int K) {
        Dictionary<int, List<int[]>> graph = new Dictionary<int,List<int[]>>();
        
        foreach(int[] path in times){
            if(!graph.ContainsKey(path[0]))
                graph.Add(path[0],new List<int[]>());
            
            graph[path[0]].Add(new int[]{path[1],path[2]});
        }
        
        Dictionary<int,int> res = new Dictionary<int,int>();
        
        SortedSet<int[]> heap = new SortedSet<int[]>(Comparer<int[]>.Create(
            (a, b) => a[0] == b[0] ? a[1]-b[1]: a[0] - b[0]
        ));
        
        heap.Add(new int[]{0,K});
        
        while(heap.Count != 0){
            int[] path = heap.Min;
            int cost = path[0];
            int dest = path[1];
            heap.Remove(path);
            
            if(res.ContainsKey(dest))
                continue;
            
            res.Add(dest,cost);
            
            if(graph.ContainsKey(dest)){
                foreach(int[] edge in graph[dest]){
                    int nextNode = edge[0];
                    int dis = edge[1];
                    if(!res.ContainsKey(nextNode)){
                        heap.Add(new int[]{cost+dis,nextNode});
                    }
                }
            }
        }
        
        if(res.Count != N)
            return -1;
        
        int max = 0;
        
        foreach(KeyValuePair<int, int> length in res){
            max = Math.Max(max,length.Value);
        }
        
        return max;
    }
}
public class Solution {
    public int NetworkDelayTime(int[][] times, int N, int K) {
		--K;
        int[,] dist = new int[N, N];
		
		var inf = (int)1e9;
		
		for(var u = 0; u < N; ++u)
		{
			for(var v = 0; v < N; ++v)
			{
				dist[u, v] = inf;
			}
			
			dist[u, u] = 0;
		}

		
		foreach(var e in times)
		{
			int u = e[0] - 1, v = e[1] - 1, weight = e[2];
			dist[u, v] = weight;
		}
				
		for(var k = 0; k < N; ++k)
			for(var i = 0; i < N; ++i)
				for(var j = 0; j < N; ++j)
					if(dist[i, j] > dist[i, k] + dist[k, j])
						dist[i, j] = dist[i, k] + dist[k, j];
				
		var max = 0;
		for(var u = 0; u < N; ++u)
			max = Math.Max(max, dist[K, u]);
		
		return max == inf ? -1 : max;
    }
}
*/
