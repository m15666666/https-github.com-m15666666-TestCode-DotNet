using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个整数 n, 返回从 1 到 n 的字典顺序。

例如，

给定 n =13，返回 [1,10,11,12,13,2,3,4,5,6,7,8,9] 。

请尽可能的优化算法的时间复杂度和空间复杂度。 输入的数据 n 小于等于 5,000,000。 
*/
/// <summary>
/// https://leetcode-cn.com/problems/lexicographical-numbers/
/// 386. 字典序排数
/// https://blog.csdn.net/qq_36946274/article/details/81394392
/// </summary>
class LexicographicalNumbersSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public IList<int> LexicalOrder(int n)
    {
        List<int> ret = new List<int>();
        for (int i = 1; i < 10; i++)
        {
            if (i<= n)
            {
                ret.Add(i);
                BackTrade(ret, n, i * 10);
            }
            else break;
        }
        return ret;
    }

    private static void BackTrade( List<int> ret, int n, int baseNum )
    {
        for( int i = 0; i < 10; i++)
        {
            int temp = baseNum + i;
            if (temp <= n)
            {
                ret.Add(temp);
                BackTrade(ret, n, temp * 10);
            }
            else break;
        }
    }
}
/*
public class Solution {
    public IList<int> LexicalOrder(int n) {
        List<int> list = new List<int>();
        
        void dfs(int num)
        {
            list.Add(num);
            for(int t = num * 10, max = t + 9; t <= max && t <= n; t++)
                dfs(t);
        }
        
        for(int i = 1; i <= 9 && i <= n; i++)
            dfs(i);
        
        return list;
    }
}
*/
