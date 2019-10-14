using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// https://leetcode-cn.com/problems/all-nodes-distance-k-in-binary-tree/
/// 863. 二叉树中所有距离为 K 的结点
/// 
/// </summary>
class AllNodesDistanceKInBinaryTreeSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    
    public int ClimbStairs(int n)
    {
        Dictionary<int, int> n2PathCount = new Dictionary<int, int>();
        return ClimbStairs(n, n2PathCount);
    }

    private int ClimbStairs(int n, Dictionary<int, int> n2PathCount)
    {
        if (n <= 1) return 1;
        if (n == 2) return 2;

        if (n2PathCount.ContainsKey(n)) return n2PathCount[n];

        var ret = ClimbStairs(n - 1, n2PathCount) + ClimbStairs(n - 2, n2PathCount);

        if (!n2PathCount.ContainsKey(n)) n2PathCount[n] = ret;

        return ret;
    }
}