using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// https://leetcode-cn.com/problems/unique-binary-search-trees/
/// 96.不同的二叉搜索树
/// 给定一个整数 n，求以 1 ... n 为节点组成的二叉搜索树有多少种？
/// https://blog.csdn.net/Scarlett_Guan/article/details/80601382
/// </summary>
class NumTreesSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int NumTrees(int n)
    {
        Dictionary<int, int> map = new Dictionary<int, int>();
        return NumTrees(n, map);
    }

    private int NumTrees(int n, Dictionary<int,int> map )
    {
        if ( n <= 1 ) return 1;
        if ( map.ContainsKey(n) ) return map[n];

        int ret = 0;
        for (int i = 1; i <= n; i++)
        {
            ret += NumTrees(i - 1, map ) * NumTrees( n - i, map );
        }
        map[n] = ret;
        return ret;
    }
}