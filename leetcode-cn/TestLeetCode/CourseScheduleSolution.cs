using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// https://leetcode-cn.com/problems/course-schedule/
/// 207. 课程表
/// 现在你总共有 n 门课需要选，记为 0 到 n-1。
/// 在选修某些课程之前需要一些先修课程。 例如，想要学习课程 0 ，你需要先完成课程 1 ，我们用一个匹配来表示他们: [0,1]
/// 给定课程总量以及它们的先决条件，判断是否可能完成所有课程的学习？
/// 示例 1:
/// 输入: 2, [[1,0]] 
/// 输出: true
/// 解释: 总共有 2 门课程。学习课程 1 之前，你需要完成课程 0。所以这是可能的。
/// 示例 2:
/// 输入: 2, [[1,0],[0,1]]
/// 输出: false
/// 解释: 总共有 2 门课程。学习课程 1 之前，你需要先完成​课程 0；并且学习课程 0 之前，你还应先完成课程 1。这是不可能的。
/// </summary>
class CourseScheduleSolution
{
    public static void Test()
    {
        var ret = CanFinish(3, new int[,] { { 1, 0 }, { 2, 1 } });
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public static bool CanFinish(int numCourses, int[,] prerequisites)
    {
        var matrix = prerequisites;

        if (numCourses < 2) return true;
        if (matrix == null || matrix.Length == 0) return true;

        int m = matrix.GetLength(0);
        int n = matrix.GetLength(1);
        if (m == 1) return true;
        if (n != 2) return false;

        Queue<int> queue = new Queue<int>();
        Dictionary<int, List<int>> course2Dependency = new Dictionary<int, List<int>>();
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
//别人的解法
public class Solution {
    public bool CanFinish(int numCourses, int[,] prerequisites) {
        List<Node> nodeList = new List<Node>();
        for(int i = 0; i < numCourses; ++i)
            nodeList.Add(new Node(i));
        for(int i = 0; i < prerequisites.GetLength(0); ++i)
        {
            if(FindList(nodeList[prerequisites[i,0]], prerequisites[i,1], nodeList))
                AddListNode(nodeList[prerequisites[i,1]], prerequisites[i,0]);
            else
                return false;
        }
        return true;
    }
    
    bool FindList(Node root, int val, List<Node> nodeList)
    {
        if(root.val == val)
            return true;
        while(root.next != null)
        {
            if(root.next.val == val)
                return false;
            root = root.next;
            if(!FindList(nodeList[root.val], val, nodeList))
                return false;
        }
        return true;
    }
    
    void AddListNode(Node root, int val)
    {
        while(root.next != null)
            root = root.next;
        root.next = new Node(val);
    }
    
    class Node
    {
        public Node(int v)
        {
            val = v;
        }
        public Node next = null;
        public int val;
    }
}

public class Solution {
    public bool CanFinish(int numCourses, int[,] prerequisites) {
        List<Node> nodeList = new List<Node>();
        for(int i = 0; i < numCourses; ++i)
            nodeList.Add(new Node(i));
        for(int i = 0; i < prerequisites.GetLength(0); ++i)
        {
            if(FindList(nodeList[prerequisites[i,0]], prerequisites[i,1], nodeList))
                AddListNode(nodeList[prerequisites[i,1]], prerequisites[i,0]);
            else
                return false;
        }
        return true;
    }
    
    bool FindList(Node root, int val, List<Node> nodeList)
    {
        if(root.val == val)
            return true;
        while(root.next != null)
        {
            if(root.next.val == val)
                return false;
            root = root.next;
            if(!FindList(nodeList[root.val], val, nodeList))
                return false;
        }
        return true;
    }
    
    void AddListNode(Node root, int val)
    {
        while(root.next != null)
            root = root.next;
        root.next = new Node(val);
    }
    
    class Node
    {
        public Node(int v)
        {
            val = v;
        }
        public Node next = null;
        public int val;
    }
}
*/
