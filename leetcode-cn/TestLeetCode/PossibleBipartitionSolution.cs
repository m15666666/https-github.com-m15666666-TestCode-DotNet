using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一组 N 人（编号为 1, 2, ..., N）， 我们想把每个人分进任意大小的两组。

每个人都可能不喜欢其他人，那么他们不应该属于同一组。

形式上，如果 dislikes[i] = [a, b]，表示不允许将编号为 a 和 b 的人归入同一组。

当可以用这种方法将每个人分进两组时，返回 true；否则返回 false。

 

示例 1：

输入：N = 4, dislikes = [[1,2],[1,3],[2,4]]
输出：true
解释：group1 [1,4], group2 [2,3]
示例 2：

输入：N = 3, dislikes = [[1,2],[1,3],[2,3]]
输出：false
示例 3：

输入：N = 5, dislikes = [[1,2],[2,3],[3,4],[4,5],[1,5]]
输出：false
 

提示：

1 <= N <= 2000
0 <= dislikes.length <= 10000
1 <= dislikes[i][j] <= N
dislikes[i][0] < dislikes[i][1]
对于 dislikes[i] == dislikes[j] 不存在 i != j 
*/
/// <summary>
/// https://leetcode-cn.com/problems/possible-bipartition/
/// 886. 可能的二分法
/// </summary>
class PossibleBipartitionSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public bool PossibleBipartition(int N, int[][] dislikes)
    {
        graph = new List<int>[N + 1];
        for (int i = 1; i <= N; ++i)
            graph[i] = new List<int>();

        foreach (int[] edge in dislikes)
        {
            graph[edge[0]].Add(edge[1]);
            graph[edge[1]].Add(edge[0]);
        }

        for (int node = 1; node <= N; ++node)
            if (!dislikeStatus.ContainsKey(node) && !Dfs(node, false))
                return false;
        return true;
    }

    private List<int>[] graph;
    private Dictionary<int, bool> dislikeStatus = new Dictionary<int, bool>();

    private bool Dfs(int node, bool dislike)
    {
        if (dislikeStatus.ContainsKey(node)) return dislikeStatus[node] == dislike;

        dislikeStatus.Add(node, dislike);

        foreach (int neighbor in graph[node])
            if (!Dfs(neighbor, !dislike))
                return false;
        return true;
    }
}