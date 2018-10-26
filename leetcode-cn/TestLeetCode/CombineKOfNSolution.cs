using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// https://leetcode-cn.com/problems/combinations/
/// 77.组合
/// 给定两个整数 n 和 k，返回 1 ... n 中所有可能的 k 个数的组合。
/// </summary>
class CombineKOfNSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public IList<IList<int>> Combine(int n, int k)
    {
        List<IList<int>> ret = new List<IList<int>>();

        if (n == 0 || k == 0 || n < k) return ret;

        //HashSet<int> indexset = new HashSet<int>();
        List<int> list = new List<int>();
        BackTrade( n, k, 0, 
            //indexset, 
            list, ret );

        return ret;
    }

    private void BackTrade( int n, int k, int startIndex, 
        //HashSet<int> indexset, 
        List<int> list, List<IList<int>> ret )
    {
        if ((n - startIndex + list.Count) < k) return;

        if ( list.Count == k )
        {
            var l = list.ToArray();
            ret.Add(l);
            return;
        }

        for ( int i = startIndex; i < n; i++ )
        {
            //if (indexset.Contains(i)) continue;

            if ((n - startIndex + list.Count) < k) return;

            var v = i + 1;

            list.Insert(0, v);
            //indexset.Add(i);

            BackTrade( n, k, i + 1, 
                //indexset, 
                list, ret );

            list.RemoveAt(0);
            //indexset.Remove(i);
        }
    }
}