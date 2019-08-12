using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个整数数组 prices，其中第 i 个元素代表了第 i 天的股票价格 ；非负整数 fee 代表了交易股票的手续费用。

你可以无限次地完成交易，但是你每次交易都需要付手续费。如果你已经购买了一个股票，在卖出它之前你就不能再继续购买股票了。

返回获得利润的最大值。

示例 1:

输入: prices = [1, 3, 2, 8, 4, 9], fee = 2
输出: 8
解释: 能够达到的最大利润:  
在此处买入 prices[0] = 1
在此处卖出 prices[3] = 8
在此处买入 prices[4] = 4
在此处卖出 prices[5] = 9
总利润: ((8 - 1) - 2) + ((9 - 4) - 2) = 8.
注意:

0 < prices.length <= 50000.
0 < prices[i] < 50000.
0 <= fee < 50000. 
*/
/// <summary>
/// https://leetcode-cn.com/problems/best-time-to-buy-and-sell-stock-with-transaction-fee/
/// 714. 买卖股票的最佳时机含手续费
/// https://blog.csdn.net/Viscu/article/details/82352743
/// </summary>
class BestTimeToBuyAndSellStockWithTransactionFeeSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int MaxProfit(int[] prices, int fee)
    {
        if (prices == null || prices.Length == 0) return 0;

        // 使用动态规划，在每一步两个变换出的分支中，去除了不需要计算的分支
        // 空仓利润
        int emptyProfit = 0;

        // 满仓利润
        int fullProfit = -prices[0];
        for (int i = 1; i < prices.Length; ++i)
        {
            var price = prices[i];
            emptyProfit = Math.Max(emptyProfit, fullProfit + price - fee);
            fullProfit = Math.Max(fullProfit, emptyProfit - price);
        }
        return Math.Max(emptyProfit, fullProfit);
    }
}
/*
public class Solution {
    public int MaxProfit(int[] prices, int fee) {
        int n = prices.Length;
        int[] buy = new int[n+1];
        int[] sell = new int[n+1];
        
        buy[0] = int.MinValue;
        sell[0] = 0;
        
        for (int i = 1; i <= n; i ++) {
            buy[i] = Math.Max(buy[i-1], sell[i-1] - prices[i-1] -fee);
            sell[i] = Math.Max(sell[i-1], buy[i-1] + prices[i-1]);
        }
        
        return sell[n];
    }
} 
public class Solution {
    public int MaxProfit(int[] prices, int fee) {
        int cash = 0, hold = -prices[0];
        for (int i = 1; i < prices.Length; i++) {
            cash = Math.Max(cash, hold + prices[i] - fee);
            hold = Math.Max(hold, cash - prices[i]);
        }
        return cash;
    }
}
*/
