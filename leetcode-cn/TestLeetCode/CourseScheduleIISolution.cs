using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
现在你总共有 n 门课需要选，记为 0 到 n-1。

在选修某些课程之前需要一些先修课程。 例如，想要学习课程 0 ，你需要先完成课程 1 ，我们用一个匹配来表示他们: [0,1]

给定课程总量以及它们的先决条件，返回你为了学完所有课程所安排的学习顺序。

可能会有多个正确的顺序，你只要返回一种就可以了。如果不可能完成所有课程，返回一个空数组。

示例 1:

输入: 2, [[1,0]] 
输出: [0,1]
解释: 总共有 2 门课程。要学习课程 1，你需要先完成课程 0。因此，正确的课程顺序为 [0,1] 。
示例 2:

输入: 4, [[1,0],[2,0],[3,1],[3,2]]
输出: [0,1,2,3] or [0,2,1,3]
解释: 总共有 4 门课程。要学习课程 3，你应该先完成课程 1 和课程 2。并且课程 1 和课程 2 都应该排在课程 0 之后。
     因此，一个正确的课程顺序是 [0,1,2,3] 。另一个正确的排序是 [0,2,1,3] 。
说明:

输入的先决条件是由边缘列表表示的图形，而不是邻接矩阵。详情请参见图的表示法。
你可以假定输入的先决条件中没有重复的边。
提示:

这个问题相当于查找一个循环是否存在于有向图中。如果存在循环，则不存在拓扑排序，因此不可能选取所有课程进行学习。
通过 DFS 进行拓扑排序 - 一个关于Coursera的精彩视频教程（21分钟），介绍拓扑排序的基本概念。
拓扑排序也可以通过 BFS 完成。

*/
/// <summary>
/// https://leetcode-cn.com/problems/course-schedule-ii/
/// 210.课程表 II
/// 现在你总共有 n 门课需要选，记为 0 到 n-1。
/// 在选修某些课程之前需要一些先修课程。 例如，想要学习课程 0 ，你需要先完成课程 1 ，我们用一个匹配来表示他们: [0,1]
/// 给定课程总量以及它们的先决条件，返回你为了学完所有课程所安排的学习顺序。
/// 可能会有多个正确的顺序，你只要返回一种就可以了。如果不可能完成所有课程，返回一个空数组。
/// 示例 1:
/// 输入: 2, [[1,0]] 
/// 输出: [0,1]
/// 解释: 总共有 2 门课程。要学习课程 1，你需要先完成课程 0。因此，正确的课程顺序为 [0,1] 。
/// 示例 2:
/// 输入: 4, [[1,0],[2,0],[3,1],[3,2]]
/// 输出: [0,1,2,3]
/// or[0, 2, 1, 3]
/// 解释: 总共有 4 门课程。要学习课程 3，你应该先完成课程 1 和课程 2。并且课程 1 和课程 2 都应该排在课程 0 之后。
/// 因此，一个正确的课程顺序是[0, 1, 2, 3] 。另一个正确的排序是[0, 2, 1, 3] 。
/// </summary>
class CourseScheduleIISolution
{
    public static void Test()
    {
        var ret = FindOrder( 2, new int[,]{ { 0,1} });
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public static int[] FindOrder(int numCourses, int[,] prerequisites)
    {
        List<int> ret = new List<int>();
        HashSet<int> isInResult = new HashSet<int>();
        Dictionary<int, List<int>> course2Dependency = new Dictionary<int, List<int>>();

        var canFinish = CanFinish(numCourses, prerequisites, course2Dependency);
        if (!canFinish) return ret.ToArray();

        for( int c = 0; c < numCourses; c++)
        {
            AddToResult(c, ret, isInResult, course2Dependency);
        }

        return ret.ToArray();
    }

    private static void AddToResult( int courseIndex, List<int> ret, HashSet<int> isInResult, Dictionary<int, List<int>> course2Dependency )
    {
        if ( isInResult.Contains(courseIndex) ) return;
        if ( !course2Dependency.ContainsKey(courseIndex) )
        {
            ret.Add(courseIndex);
            isInResult.Add(courseIndex);
            return;
        }

        foreach( var c in course2Dependency[courseIndex])
        {
            AddToResult(c, ret, isInResult, course2Dependency);
        }

        ret.Add(courseIndex);
        isInResult.Add(courseIndex);
    }

    private static bool CanFinish(int numCourses, int[,] prerequisites, Dictionary<int, List<int>> course2Dependency)
    {
        var matrix = prerequisites;

        if (numCourses < 2) return true;
        if (matrix == null || matrix.Length == 0) return true;

        int m = matrix.GetLength(0);
        int n = matrix.GetLength(1);
        //if (m == 1) return true;
        if (n != 2) return false;

        Queue<int> queue = new Queue<int>();
        
        for (int r = 0; r < m; r++)
        {
            int course = matrix[r, 0];
            int dependencyCourse = matrix[r, 1];

            queue.Enqueue(dependencyCourse);
            while (0 < queue.Count)
            {
                int c = queue.Dequeue();
                if (course2Dependency.ContainsKey(c))
                {
                    foreach (var d in course2Dependency[c])
                    {
                        if (d == course) return false;
                        queue.Enqueue(d);
                    }
                }
            }

            if (course2Dependency.ContainsKey(course))
                course2Dependency[course].Add(dependencyCourse);
            else
                course2Dependency.Add(course, new List<int> { dependencyCourse });
        }

        return true;
    }
}

/*

课程表 II
力扣官方题解
发布于 2020-05-16
26.8k
📺 视频题解

📖 文字题解
前言
本题是一道经典的「拓扑排序」问题。

给定一个包含 nn 个节点的有向图 GG，我们给出它的节点编号的一种排列，如果满足：

对于图 GG 中的任意一条有向边 (u, v)(u,v)，uu 在排列中都出现在 vv 的前面。

那么称该排列是图 GG 的「拓扑排序」。根据上述的定义，我们可以得出两个结论：

如果图 GG 中存在环（即图 GG 不是「有向无环图」），那么图 GG 不存在拓扑排序。这是因为假设图中存在环 x_1, x_2, \cdots, x_n, x_1x 
1
​	
 ,x 
2
​	
 ,⋯,x 
n
​	
 ,x 
1
​	
 ，那么 x_1x 
1
​	
  在排列中必须出现在 x_nx 
n
​	
  的前面，但 x_nx 
n
​	
  同时也必须出现在 x_1x 
1
​	
  的前面，因此不存在一个满足要求的排列，也就不存在拓扑排序；

如果图 GG 是有向无环图，那么它的拓扑排序可能不止一种。举一个最极端的例子，如果图 GG 值包含 nn 个节点却没有任何边，那么任意一种编号的排列都可以作为拓扑排序。

有了上述的简单分析，我们就可以将本题建模成一个求拓扑排序的问题了：

我们将每一门课看成一个节点；

如果想要学习课程 AA 之前必须完成课程 BB，那么我们从 BB 到 AA 连接一条有向边。这样以来，在拓扑排序中，BB 一定出现在 AA 的前面。

求出该图的拓扑排序，就可以得到一种符合要求的课程学习顺序。下面介绍两种求解拓扑排序的方法。

方法一：深度优先搜索
思路

我们可以将深度优先搜索的流程与拓扑排序的求解联系起来，用一个栈来存储所有已经搜索完成的节点。

对于一个节点 uu，如果它的所有相邻节点都已经搜索完成，那么在搜索回溯到 uu 的时候，uu 本身也会变成一个已经搜索完成的节点。这里的「相邻节点」指的是从 uu 出发通过一条有向边可以到达的所有节点。

假设我们当前搜索到了节点 uu，如果它的所有相邻节点都已经搜索完成，那么这些节点都已经在栈中了，此时我们就可以把 uu 入栈。可以发现，如果我们从栈顶往栈底的顺序看，由于 uu 处于栈顶的位置，那么 uu 出现在所有 uu 的相邻节点的前面。因此对于 uu 这个节点而言，它是满足拓扑排序的要求的。

这样以来，我们对图进行一遍深度优先搜索。当每个节点进行回溯的时候，我们把该节点放入栈中。最终从栈顶到栈底的序列就是一种拓扑排序。

算法

对于图中的任意一个节点，它在搜索的过程中有三种状态，即：

「未搜索」：我们还没有搜索到这个节点；

「搜索中」：我们搜索过这个节点，但还没有回溯到该节点，即该节点还没有入栈，还有相邻的节点没有搜索完成）；

「已完成」：我们搜索过并且回溯过这个节点，即该节点已经入栈，并且所有该节点的相邻节点都出现在栈的更底部的位置，满足拓扑排序的要求。

通过上述的三种状态，我们就可以给出使用深度优先搜索得到拓扑排序的算法流程，在每一轮的搜索搜索开始时，我们任取一个「未搜索」的节点开始进行深度优先搜索。

我们将当前搜索的节点 uu 标记为「搜索中」，遍历该节点的每一个相邻节点 vv：

如果 vv 为「未搜索」，那么我们开始搜索 vv，待搜索完成回溯到 uu；

如果 vv 为「搜索中」，那么我们就找到了图中的一个环，因此是不存在拓扑排序的；

如果 vv 为「已完成」，那么说明 vv 已经在栈中了，而 uu 还不在栈中，因此 uu 无论何时入栈都不会影响到 (u, v)(u,v) 之前的拓扑关系，以及不用进行任何操作。

当 uu 的所有相邻节点都为「已完成」时，我们将 uu 放入栈中，并将其标记为「已完成」。

在整个深度优先搜索的过程结束后，如果我们没有找到图中的环，那么栈中存储这所有的 nn 个节点，从栈顶到栈底的顺序即为一种拓扑排序。

下面的幻灯片给出了深度优先搜索的可视化流程。图中的「白色」「黄色」「绿色」节点分别表示「未搜索」「搜索中」「已完成」的状态。




class Solution {
    // 存储有向图
    List<List<Integer>> edges;
    // 标记每个节点的状态：0=未搜索，1=搜索中，2=已完成
    int[] visited;
    // 用数组来模拟栈，下标 n-1 为栈底，0 为栈顶
    int[] result;
    // 判断有向图中是否有环
    boolean valid = true;
    // 栈下标
    int index;

    public int[] findOrder(int numCourses, int[][] prerequisites) {
        edges = new ArrayList<List<Integer>>();
        for (int i = 0; i < numCourses; ++i) {
            edges.add(new ArrayList<Integer>());
        }
        visited = new int[numCourses];
        result = new int[numCourses];
        index = numCourses - 1;
        for (int[] info : prerequisites) {
            edges.get(info[1]).add(info[0]);
        }
        // 每次挑选一个「未搜索」的节点，开始进行深度优先搜索
        for (int i = 0; i < numCourses && valid; ++i) {
            if (visited[i] == 0) {
                dfs(i);
            }
        }
        if (!valid) {
            return new int[0];
        }
        // 如果没有环，那么就有拓扑排序
        return result;
    }

    public void dfs(int u) {
        // 将节点标记为「搜索中」
        visited[u] = 1;
        // 搜索其相邻节点
        // 只要发现有环，立刻停止搜索
        for (int v: edges.get(u)) {
            // 如果「未搜索」那么搜索相邻节点
            if (visited[v] == 0) {
                dfs(v);
                if (!valid) {
                    return;
                }
            }
            // 如果「搜索中」说明找到了环
            else if (visited[v] == 1) {
                valid = false;
                return;
            }
        }
        // 将节点标记为「已完成」
        visited[u] = 2;
        // 将节点入栈
        result[index--] = u;
    }
}
复杂度分析

时间复杂度: O(n+m)O(n+m)，其中 nn 为课程数，mm 为先修课程的要求数。这其实就是对图进行深度优先搜索的时间复杂度。

空间复杂度: O(n+m)O(n+m)。题目中是以列表形式给出的先修课程关系，为了对图进行深度优先搜索，我们需要存储成邻接表的形式，空间复杂度为 O(n+m)O(n+m)。在深度优先搜索的过程中，我们需要最多 O(n)O(n) 的栈空间（递归）进行深度优先搜索，并且还需要若干个 O(n)O(n) 的空间存储节点状态、最终答案等。

方法二：广度优先搜索
思路

方法一的深度优先搜索是一种「逆向思维」：最先被放入栈中的节点是在拓扑排序中最后面的节点。我们也可以使用正向思维，顺序地生成拓扑排序，这种方法也更加直观。

我们考虑拓扑排序中最前面的节点，该节点一定不会有任何入边，也就是它没有任何的先修课程要求。当我们将一个节点加入答案中后，我们就可以移除它的所有出边，代表着它的相邻节点少了一门先修课程的要求。如果某个相邻节点变成了「没有任何入边的节点」，那么就代表着这门课可以开始学习了。按照这样的流程，我们不断地将没有入边的节点加入答案，直到答案中包含所有的节点（得到了一种拓扑排序）或者不存在没有入边的节点（图中包含环）。

上面的想法类似于广度优先搜索，因此我们可以将广度优先搜索的流程与拓扑排序的求解联系起来。

算法

我们使用一个队列来进行广度优先搜索。开始时，所有入度为 00 的节点都被放入队列中，它们就是可以作为拓扑排序最前面的节点，并且它们之间的相对顺序是无关紧要的。

在广度优先搜索的每一步中，我们取出队首的节点 uu：

我们将 uu 放入答案中；

我们移除 uu 的所有出边，也就是将 uu 的所有相邻节点的入度减少 11。如果某个相邻节点 vv 的入度变为 00，那么我们就将 vv 放入队列中。

在广度优先搜索的过程结束后。如果答案中包含了这 nn 个节点，那么我们就找到了一种拓扑排序，否则说明图中存在环，也就不存在拓扑排序了。

下面的幻灯片给出了广度优先搜索的可视化流程。




class Solution {
    // 存储有向图
    List<List<Integer>> edges;
    // 存储每个节点的入度
    int[] indeg;
    // 存储答案
    int[] result;
    // 答案下标
    int index;

    public int[] findOrder(int numCourses, int[][] prerequisites) {
        edges = new ArrayList<List<Integer>>();
        for (int i = 0; i < numCourses; ++i) {
            edges.add(new ArrayList<Integer>());
        }
        indeg = new int[numCourses];
        result = new int[numCourses];
        index = 0;
        for (int[] info : prerequisites) {
            edges.get(info[1]).add(info[0]);
            ++indeg[info[0]];
        }

        Queue<Integer> queue = new LinkedList<Integer>();
        // 将所有入度为 0 的节点放入队列中
        for (int i = 0; i < numCourses; ++i) {
            if (indeg[i] == 0) {
                queue.offer(i);
            }
        }

        while (!queue.isEmpty()) {
            // 从队首取出一个节点
            int u = queue.poll();
            // 放入答案中
            result[index++] = u;
            for (int v: edges.get(u)) {
                --indeg[v];
                // 如果相邻节点 v 的入度为 0，就可以选 v 对应的课程了
                if (indeg[v] == 0) {
                    queue.offer(v);
                }
            }
        }

        if (index != numCourses) {
            return new int[0];
        }
        return result;
    }
}
复杂度分析

时间复杂度: O(n+m)O(n+m)，其中 nn 为课程数，mm 为先修课程的要求数。这其实就是对图进行广度优先搜索的时间复杂度。

空间复杂度: O(n+m)O(n+m)。题目中是以列表形式给出的先修课程关系，为了对图进行广度优先搜索，我们需要存储成邻接表的形式，空间复杂度为 O(n+m)O(n+m)。在广度优先搜索的过程中，我们需要最多 O(n)O(n) 的队列空间（迭代）进行广度优先搜索，并且还需要若干个 O(n)O(n) 的空间存储节点入度、最终答案等。


public class Solution {
    public int[] FindOrder(int numCourses, int[,] prerequisites) {        
        if(numCourses == 0 || prerequisites == null)
            return new int[0];
        
        var result = new List<int>();
        
        var indegree = new int[numCourses];
        var vertexs = new List<int>[numCourses];
        for(var i=0; i<numCourses; i++)
        {
            vertexs[i] = new List<int>();
        }
        var edgeCount = prerequisites.GetLength(0);
        for(var i=0; i<edgeCount; i++)
        {
            vertexs[prerequisites[i, 1]].Add(prerequisites[i, 0]);
            indegree[prerequisites[i, 0]]++;
        }        
           
        var queue = new Queue<int>();
        EnqueueTopNodesIndex(indegree, queue);
        while(queue.Count > 0)
        {
            var head = queue.Dequeue();
            result.Add(head);
            foreach(var link in vertexs[head])
            {
                indegree[link]--;
                if(indegree[link] == 0)
                {
                    queue.Enqueue(link);
                    indegree[link]--;
                }
            }
        }
        if(result.Count != numCourses)
            return new int[0];
        return result.ToArray();
    }
    
    private void EnqueueTopNodesIndex(int[] indegree, Queue<int> queue)
    {
        for(var i=0; i<indegree.Length; i++)
        {
            if(indegree[i] == 0)
            {
                queue.Enqueue(i);
                indegree[i]--;
            }
        }        
    }
}

public class Solution {
    public int[] FindOrder(int numCourses, int[,] prerequisites) {
        int[] result = new int[numCourses];//结果集
            int k = 0;

            if (prerequisites.Length == 0)
            {
                for (int i = 0; i < numCourses; i++)
                {
                    result[i] = i;
                }
                return result;
            }

            Stack<int> stack = new Stack<int>();//用来存储入度为0的课程
            int[] pathin = new int[numCourses];//用来存储课程的入度数量
           
            Dictionary<int, List<int>> map = new Dictionary<int, List<int>>();//用来存储课程和该课程的后继课程
            for(int i=0;i<prerequisites.GetLength(0);i++)
            {//遍历数组得出每个结点的入度数和后继课程集合
                if (map.ContainsKey(prerequisites[i,1]))
                {
                    List<int > temp = map[prerequisites[i,1]];
                    temp.Add(prerequisites[i,0]);
                  
                }
                else
                {
                    List<int> temp = new List<int>();
                    temp.Add(prerequisites[i, 0]);
                    map.Add(prerequisites[i, 1], temp);
                }
                pathin[prerequisites[i, 0]]++;
            }


            for (int i = 0; i < numCourses; i++)
            {//进行拓扑排序
                if (pathin[i] == 0)
                {//找到入度为0的课程 入栈
                    pathin[i] = -1;
                    stack.Push(i);
                }


                while (stack.Count!=0 && k < numCourses)
                {
                    int index = stack.Pop();//入度为0的课程出栈 其后续课程的pathin对应减一
                    result[k++] = index;//将出栈的入度为0的课程号添加入结果集中
                    List<int> temp = null;
                    if (map.ContainsKey(index))
                    {
                        temp = map[index];
                    }

                    for (int j = 0; temp != null && j < temp.Count ; j++)
                    {//对应的后续课程的入度数-1
                        pathin[map[index][j]]--;
                        if (pathin[map[index][j]] == 0)
                        {//判断后续课程的入度数减完是否0,0则出栈
                            stack.Push (map[index][j]);
                            pathin[map[index][j]] = -1;
                        }
                    }
                }
            }

            if (k != numCourses)
            {
                int[] t = { };
                return t;
            }
            return result;
    }
}
*/
