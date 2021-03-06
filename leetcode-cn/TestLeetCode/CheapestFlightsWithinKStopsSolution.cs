﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
有 n 个城市通过 m 个航班连接。每个航班都从城市 u 开始，以价格 w 抵达 v。

现在给定所有的城市和航班，以及出发城市 src 和目的地 dst，你的任务是找到从 src 到 dst 最多经过 k 站中转的最便宜的价格。 如果没有这样的路线，则输出 -1。

示例 1:
输入: 
n = 3, edges = [[0,1,100],[1,2,100],[0,2,500]]
src = 0, dst = 2, k = 1
输出: 200
解释: 
城市航班图如下


从城市 0 到城市 2 在 1 站中转以内的最便宜价格是 200，如图中红色所示。
示例 2:
输入: 
n = 3, edges = [[0,1,100],[1,2,100],[0,2,500]]
src = 0, dst = 2, k = 0
输出: 500
解释: 
城市航班图如下


从城市 0 到城市 2 在 0 站中转以内的最便宜价格是 500，如图中蓝色所示。
提示：

n 范围是 [1, 100]，城市标签从 0 到 n - 1.
航班数量范围是 [0, n * (n - 1) / 2].
每个航班的格式 (src, dst, price).
每个航班的价格范围是 [1, 10000].
k 范围是 [0, n - 1].
航班没有重复，且不存在环路
*/
/// <summary>
/// https://leetcode-cn.com/problems/cheapest-flights-within-k-stops/
/// 787. K 站中转内最便宜的航班
/// http://www.dongcoder.com/detail-1162028.html
/// </summary>
class CheapestFlightsWithinKStopsSolution
{
    public void Test()
    {
        var ret = FindCheapestPrice(3, new int[][] { new int[] { 0, 1, 100 }, new int[] { 1, 2, 100 }, new int[] { 0, 2, 500 } }, 0, 2, 1);
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int FindCheapestPrice(int n, int[][] flights, int src, int dst, int K)
    {
        int upper = K + 1;
        int[,] dp = new int[n, upper + 1];
        //dp[i,k]表示经过k个中转站到达i的最少花费
        for(int i = 0; i < n; i++)
        {
            for(int j = 0; j <= upper; j++)
            {
                dp[i,j] = int.MaxValue;
            }
        }

        //Queue<int[]> queue = new Queue<int[]>(flights);
        dp[src, 0] = 0;
        for (int k = 1; k <= upper; k++)
        {
            foreach( var flight in flights)
            {
                var begin = flight[0];
                var end = flight[1];
                var price = flight[2];
                var previous = dp[begin, k - 1];
                if (previous != int.MaxValue) dp[end, k] = Math.Min(dp[end, k], previous + price);
            }

            //int size = queue.Count;
            //while( 0 < size--)
            //{
            //    var flight = queue.Dequeue();
            //    var begin = flight[0];
            //    var end = flight[1];
            //    var price = flight[2];
            //    var previous = dp[begin, k - 1];
            //    if (previous != int.MaxValue) dp[end, k] = Math.Min(dp[end, k], previous + price);
            //    else queue.Enqueue(flight);
            //}
        }

        int ret = int.MaxValue;
        for( int i = 1; i <= upper; i++ )
            if( dp[dst, i] < ret) ret = dp[dst, i];
        return ret == int.MaxValue ? -1 : ret;
    }
}