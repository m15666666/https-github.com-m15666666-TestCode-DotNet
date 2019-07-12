using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个用字符数组表示的 CPU 需要执行的任务列表。其中包含使用大写的 A - Z 字母表示的26 种不同种类的任务。
任务可以以任意顺序执行，并且每个任务都可以在 1 个单位时间内执行完。CPU 在任何一个单位时间内都可以执行一个任务，或者在待命状态。

然而，两个相同种类的任务之间必须有长度为 n 的冷却时间，因此至少有连续 n 个单位时间内 CPU 在执行不同的任务，或者在待命状态。

你需要计算完成所有任务所需要的最短时间。

示例 1：

输入: tasks = ["A","A","A","B","B","B"], n = 2
输出: 8
执行顺序: A -> B -> (待命) -> A -> B -> (待命) -> A -> B.
注：

任务的总个数为 [1, 10000]。
n 的取值范围为 [0, 100]。 
*/
/// <summary>
/// https://leetcode-cn.com/problems/task-scheduler/
/// 621. 任务调度器
/// https://blog.csdn.net/koala_tree/article/details/78498586
/// </summary>
class TaskSchedulerSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int LeastInterval(char[] tasks, int n)
    {
        if (tasks == null || tasks.Length == 0) return 0;

        const int len = 26;
        int[] c = new int[len];
        Array.Fill(c, 0);

        foreach (char t in tasks)
        {
            c[t - 'A']++;
        }
        Array.Sort(c);

        // 最多任务数
        int maxTask = c[len - 1];
        int i = len - 1;
        while (i >= 0 && c[i] == maxTask) i--;

        // 相同最多任务的任务个数
        var maxTaskCount = len - 1 - i;

        return Math.Max( tasks.Length, (maxTask - 1) * (n + 1) + maxTaskCount );
    }
}
/*
public class Solution {
    public int LeastInterval(char[] tasks, int n) 
    {
        int[] map = new int[26];
        for(int i = 0; i < tasks.Length; i++)
        {
            map[tasks[i]-'A']++;
        }
        Array.Sort(map);
        int max = map[25]-1;
        int idleCount = max*n;
        
        for(int i = 24; i >=0 && map[i] > 0; i--)
        {
            idleCount -= Math.Min(max,map[i]);
        }
        return idleCount < 0 ? tasks.Length : (tasks.Length + idleCount);
    }
}
public class Solution {
        public int LeastInterval(char[] tasks, int n)
        {
            if (n <= 0)
                return tasks.Length;
            Dictionary<char, int> dic = new Dictionary<char, int>();
            foreach (char c in tasks)
            {
                if (dic.ContainsKey(c))
                    dic[c]++;
                else
                    dic.Add(c, 1);
            }
            int Max = dic.Values.Max();
            int MaxCount = dic.Where(a => a.Value == Max).Count();
            int count = (Max - 1) * (n + 1) + MaxCount;
            if(count<tasks.Length)
                return tasks.Length;
            return count;
        }
}

     
*/
