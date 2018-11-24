using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// https://leetcode-cn.com/problems/clone-graph/
/// 133.克隆图
/// 克隆一张无向图，图中的每个节点包含一个 label （标签）和一个 neighbors （邻接点）列表 。
/// https://www.cnblogs.com/immiao0319/p/8392052.html
/// https://blog.csdn.net/Bendaai/article/details/80985328
/// </summary>
class CloneGraphSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public UndirectedGraphNode CloneGraph(UndirectedGraphNode node)
    {
        if (node == null) return null;

        Dictionary<UndirectedGraphNode, UndirectedGraphNode> map = Bfs( node );
        foreach( var head in map.Keys )
        {
            var neighbors = map[head].neighbors;
            foreach (var neighbor in head.neighbors)
                neighbors.Add(map[neighbor]);
        }

        return map[node];
    }

    private static Dictionary<UndirectedGraphNode, UndirectedGraphNode> Bfs( UndirectedGraphNode node )
    {
        Dictionary<UndirectedGraphNode, UndirectedGraphNode> ret = new Dictionary<UndirectedGraphNode, UndirectedGraphNode>();
        Queue<UndirectedGraphNode> queue = new Queue<UndirectedGraphNode>();

        queue.Enqueue(node);
        ret[node] = new UndirectedGraphNode(node.label);
        while( 0 < queue.Count)
        {
            var head = queue.Dequeue();
            foreach( var neighbor in head.neighbors )
            {
                if (ret.ContainsKey(neighbor)) continue;
                queue.Enqueue(neighbor);
                ret[neighbor] = new UndirectedGraphNode(neighbor.label);
            }
        }

        return ret;
    }

}

/**
 * Definition for undirected graph.
 *  
 */
public class UndirectedGraphNode {
    public int label;
    public IList<UndirectedGraphNode> neighbors;
    public UndirectedGraphNode(int x) { label = x; neighbors = new List<UndirectedGraphNode>(); }
}