using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


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
// 别人的算法
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
