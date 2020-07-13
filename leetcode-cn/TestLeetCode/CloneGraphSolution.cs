using System.Collections.Generic;

/*
给你无向 连通 图中一个节点的引用，请你返回该图的 深拷贝（克隆）。

图中的每个节点都包含它的值 val（int） 和其邻居的列表（list[Node]）。

class Node {
    public int val;
    public List<Node> neighbors;
}
 

测试用例格式：

简单起见，每个节点的值都和它的索引相同。例如，第一个节点值为 1（val = 1），第二个节点值为 2（val = 2），以此类推。该图在测试用例中使用邻接列表表示。

邻接列表 是用于表示有限图的无序列表的集合。每个列表都描述了图中节点的邻居集。

给定节点将始终是图中的第一个节点（值为 1）。你必须将 给定节点的拷贝 作为对克隆图的引用返回。

 

示例 1：

输入：adjList = [[2,4],[1,3],[2,4],[1,3]]
输出：[[2,4],[1,3],[2,4],[1,3]]
解释：
图中有 4 个节点。
节点 1 的值是 1，它有两个邻居：节点 2 和 4 。
节点 2 的值是 2，它有两个邻居：节点 1 和 3 。
节点 3 的值是 3，它有两个邻居：节点 2 和 4 。
节点 4 的值是 4，它有两个邻居：节点 1 和 3 。
示例 2：

输入：adjList = [[]]
输出：[[]]
解释：输入包含一个空列表。该图仅仅只有一个值为 1 的节点，它没有任何邻居。
示例 3：

输入：adjList = []
输出：[]
解释：这个图是空的，它不含任何节点。
示例 4：

输入：adjList = [[2],[1]]
输出：[[2],[1]]
 

提示：

节点数不超过 100 。
每个节点值 Node.val 都是唯一的，1 <= Node.val <= 100。
无向图是一个简单图，这意味着图中没有重复的边，也没有自环。
由于图是无向的，如果节点 p 是节点 q 的邻居，那么节点 q 也必须是节点 p 的邻居。
图是连通图，你可以从给定节点访问到所有节点。

*/

/// <summary>
/// https://leetcode-cn.com/problems/clone-graph/
/// 133.克隆图
/// 克隆一张无向图，图中的每个节点包含一个 label （标签）和一个 neighbors （邻接点）列表 。
/// https://www.cnblogs.com/immiao0319/p/8392052.html
/// https://blog.csdn.net/Bendaai/article/details/80985328
/// </summary>
internal class CloneGraphSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public class UndirectedGraphNode
    {
        public int label;
        public IList<UndirectedGraphNode> neighbors;

        public UndirectedGraphNode(int x)
        {
            label = x; neighbors = new List<UndirectedGraphNode>();
        }
    }

    public class Node
    {
        public int val;
        public IList<Node> neighbors;

        public Node(int x)
        {
            val = x; neighbors = new List<Node>();
        }
    }

    public Node CloneGraph(Node node)
    {
        if (node == null) return node;

        Dictionary<Node, Node> visited = new Dictionary<Node, Node>();

        var queue = new Queue<Node>();
        queue.Enqueue(node);
        var ret = new Node(node.val);
        visited.Add(node, ret);

        while (0 < queue.Count)
        {
            Node n = queue.Dequeue();
            var copyNeighbors = visited[n].neighbors;
            foreach (Node neighbor in n.neighbors)
            {
                Node copy;
                if (!visited.ContainsKey(neighbor))
                {
                    copy = new Node(neighbor.val);
                    visited.Add(neighbor, copy);
                    queue.Enqueue(neighbor);
                }
                else copy = visited[neighbor];
                copyNeighbors.Add(copy);
            }
        }
        return ret;
    }

    public UndirectedGraphNode CloneGraph(UndirectedGraphNode node)
    {
        if (node == null) return null;

        Dictionary<UndirectedGraphNode, UndirectedGraphNode> map = Bfs(node);
        foreach (var head in map.Keys)
        {
            var neighbors = map[head].neighbors;
            foreach (var neighbor in head.neighbors)
                neighbors.Add(map[neighbor]);
        }

        return map[node];
    }

    private static Dictionary<UndirectedGraphNode, UndirectedGraphNode> Bfs(UndirectedGraphNode node)
    {
        Dictionary<UndirectedGraphNode, UndirectedGraphNode> ret = new Dictionary<UndirectedGraphNode, UndirectedGraphNode>();
        Queue<UndirectedGraphNode> queue = new Queue<UndirectedGraphNode>();

        queue.Enqueue(node);
        ret[node] = new UndirectedGraphNode(node.label);
        while (0 < queue.Count)
        {
            var head = queue.Dequeue();
            foreach (var neighbor in head.neighbors)
            {
                if (ret.ContainsKey(neighbor)) continue;
                queue.Enqueue(neighbor);
                ret[neighbor] = new UndirectedGraphNode(neighbor.label);
            }
        }

        return ret;
    }
}
/*
 
克隆图
力扣 (LeetCode)
发布于 2020-02-17
10.6k
方法一：深度优先搜索 DFS
思想

注意：首先尝试将问题描述的更加清楚，使其便于理解。因为这个问题使我感到困惑，所以我决定编写该问题的题解，希望帮助读者弄清可能会遇到的疑问。

图中一个节点可以拥有任意数量的邻接点。为了避免在复制时陷入死循环，需要了解图的结构。根据问题描述，任何给定的无向边都可以表示为两个有向边。如果节点 A 和节点 B 之间存在无向边，则它的图表示具有从节点 A 到节点 B 的有向边和从节点 B 到节点 A 的有向边。无向图实际上是一组连接在一起的节点，其中所有的边都是双向的。



为了防止多次遍历同一个节点，避免陷入死循环，需要以某种方式跟踪已经复制的节点。

算法

从给定节点开始遍历图。

使用一个 HashMap 存储所有已被访问和复制的节点。HashMap 中的 key 是原始图中的节点，value 是克隆图中的对应节点。如果某个节点已经被访问过，则返回其克隆图中的对应节点。

给定边 A - B，表示 A 能连接到 B，且 B 能连接到 A。如果对访问过的节点不做标记，则会陷入死循环中。



如果当前访问的节点不在 HashMap 中，则创建它的克隆节点存储在 HashMap 中。注意：在进入递归之前，必须先创建克隆节点并保存在 HashMap 中。


clone_node = Node(node.val, [])
visited[node] = clone_node
如果不保证这种顺序，可能会在递归中再次遇到同一个节点，再次遍历该节点时，陷入死循环。



递归调用每个节点的邻接点。每个节点递归调用的次数等于邻接点的数量，每一次调用返回其对应邻接点的克隆节点，最终返回这些克隆邻接点的列表，将其放入对应克隆节点的邻接表中。这样就可以克隆给定的节点和其邻接点。

提示：如果在递归调用中传入节点自身会出现什么情况？为什么每次递归调用输入不同的节点，却执行相同的操作。实际上，只需要保证对一个节点的递归调用正确即可，其他的节点也会在递归过程中建立正确的连接关系。


class Solution {
    private HashMap <Node, Node> visited = new HashMap <> ();
    public Node cloneGraph(Node node) {
        if (node == null) {
            return node;
        }

        // If the node was already visited before.
        // Return the clone from the visited dictionary.
        if (visited.containsKey(node)) {
            return visited.get(node);
        }

        // Create a clone for the given node.
        // Note that we don't have cloned neighbors as of now, hence [].
        Node cloneNode = new Node(node.val, new ArrayList());
        // The key is original node and value being the clone node.
        visited.put(node, cloneNode);

        // Iterate through the neighbors to generate their clones
        // and prepare a list of cloned neighbors to be added to the cloned node.
        for (Node neighbor: node.neighbors) {
            cloneNode.neighbors.add(cloneGraph(neighbor));
        }
        return cloneNode;
    }
}
复杂度分析

时间复杂度：O(N)O(N)，每个节点只处理一次。

空间复杂度：O(N)O(N)，存储克隆节点和原节点的 HashMap 需要 O(N)O(N) 的空间，递归调用栈需要 O(H)O(H) 的空间，其中 HH 是图的深度。总体空间复杂度为 O(N)O(N)。

方法二：广度优先遍历 BFS
思路

考虑到调用栈的深度，使用 BFS 进行图的遍历比 DFS 更好。



方法一与方法二的区别仅在于 DFS 和 BFS。DFS 以深度优先，BFS 以广度优先。这两种方法都需要借助 HashMap 避免陷入死循环。

算法

使用 HashMap 存储所有访问过的节点和克隆节点。HashMap 的 key 存储原始图的节点，value 存储克隆图中的对应节点。visited 用于防止陷入死循环，和获得克隆图的节点。

将第一个节点添加到队列。克隆第一个节点添加到名为 visited 的 HashMap 中。

BFS 遍历

从队列首部取出一个节点。
遍历该节点的所有邻接点。
如果某个邻接点已被访问，则该邻接点一定在 visited 中，那么从 visited 获得该邻接点。
否则，创建一个新的节点存储在 visited 中。
将克隆的邻接点添加到克隆图对应节点的邻接表中。

class Solution {
    public Node cloneGraph(Node node) {
        if (node == null) {
            return node;
        }

        // Hash map to save the visited node and it's respective clone
        // as key and value respectively. This helps to avoid cycles.
        HashMap<Node, Node> visited = new HashMap();

        // Put the first node in the queue
        LinkedList<Node> queue = new LinkedList<Node> ();
        queue.add(node);
        // Clone the node and put it in the visited dictionary.
        visited.put(node, new Node(node.val, new ArrayList()));

        // Start BFS traversal
        while (!queue.isEmpty()) {
            // Pop a node say "n" from the from the front of the queue.
            Node n = queue.remove();
            // Iterate through all the neighbors of the node "n"
            for (Node neighbor: n.neighbors) {
                if (!visited.containsKey(neighbor)) {
                    // Clone the neighbor and put in the visited, if not present already
                    visited.put(neighbor, new Node(neighbor.val, new ArrayList()));
                    // Add the newly encountered node to the queue.
                    queue.add(neighbor);
                }
                // Add the clone of the neighbor to the neighbors of the clone node "n".
                visited.get(n).neighbors.add(visited.get(neighbor));
            }
        }

        // Return the clone of the node from visited.
        return visited.get(node);
    }
}
复杂度分析

时间复杂度：O(N)O(N)，每个节点只处理一次。

空间复杂度：O(N)O(N)。visited 使用 O(N)O(N) 的空间。BFS 中的队列使用 O(W)O(W) 的空间，其中 WW 是图的宽度。总体空间复杂度为 O(N)O(N)。

public class Solution {
    private Dictionary<Node, Node> Record = new Dictionary<Node, Node>();
    public Node CloneGraph(Node node) {
        if(node == null)
            return null;

        if(Record.ContainsKey(node))
            return Record[node];

        Node _node = new Node(node.val, new List<Node>());
        Record.Add(node, _node);

        foreach(Node nodeNei in node.neighbors)
            _node.neighbors.Add(CloneGraph(nodeNei));
        
        return Record[node];
    }
}

public class Solution {
    public Node CloneGraph(Node node) {
        if (node == null) return null;
            Dictionary<int, Node> dic = new Dictionary<int, Node>();
            dic.Add(node.val, new Node(node.val, new List<Node>()));
            Queue queue = new Queue();
            queue.Enqueue(node);

            while (queue.Count > 0)
            {
                var temp = (Node)queue.Dequeue();
                foreach (var n in temp.neighbors)
                {
                    if (dic.ContainsKey(n.val) == false)
                    {
                        var nn = new Node(n.val, new List<Node>());
                        dic.Add(n.val, nn);
                        queue.Enqueue(n);
                    }
                    dic[temp.val].neighbors.Add(dic[n.val]);
                }
            }

            return dic[node.val];
    }
}


 
 */

