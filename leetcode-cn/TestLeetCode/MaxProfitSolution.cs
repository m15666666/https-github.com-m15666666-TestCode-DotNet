using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// 假设你正在爬楼梯。需要 n 阶你才能到达楼顶。每次你可以爬 1 或 2 个台阶。你有多少种不同的方法可以爬到楼顶呢？
/// </summary>
class MaxProfitSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public int MaxProfit(int[] prices)
    {
        if (prices == null || prices.Length < 1) return 0;

        int max = 0;
        int min = prices[0];
        for (int i = 0; i < prices.Length; i++)
        {
            if (prices[i] < min)
                min = prices[i];
            else
            {
                if (max < prices[i] - min)
                    max = prices[i] - min;
            }
        }
        return max;
    }

    private int PartialMinIndex(int[] prices, int startIndex)
    {
        if (prices.Length <= startIndex) return -1;

        var min = prices[startIndex];
        var minIndex = startIndex;
        for (int i = startIndex + 1; i < prices.Length; i++)
        {
            var v = prices[i];
            if (min < v)
            {
                return minIndex;
            }
            if (v <= min)
            {
                minIndex = i;
                min = v;
            }
        }
        return minIndex;
    }

    private int PartialMaxIndex(int[] prices, int startIndex)
    {
        if (prices.Length <= startIndex) return -1;

        var max = prices[startIndex];
        var maxIndex = startIndex;
        for (int i = startIndex + 1; i < prices.Length; i++)
        {
            var v = prices[i];
            if ( v <= max )
            {
                return maxIndex;
            }
            if ( max < v )
            {
                maxIndex = i;
                max = v;
            }
        }
        return maxIndex;
    }

}