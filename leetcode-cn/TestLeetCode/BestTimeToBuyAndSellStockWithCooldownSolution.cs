using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
给定一个整数数组，其中第 i 个元素代表了第 i 天的股票价格 。​

设计一个算法计算出最大利润。在满足以下约束条件下，你可以尽可能地完成更多的交易（多次买卖一支股票）:

你不能同时参与多笔交易（你必须在再次购买前出售掉之前的股票）。
卖出股票后，你无法在第二天买入股票 (即冷冻期为 1 天)。
示例:

输入: [1,2,3,0,2]
输出: 3 
解释: 对应的交易状态为: [买入, 卖出, 冷冻期, 买入, 卖出]
 */
/// <summary>
/// https://leetcode-cn.com/problems/best-time-to-buy-and-sell-stock-with-cooldown/
/// 309. 最佳买卖股票时机含冷冻期
/// https://blog.csdn.net/qq_17550379/article/details/82856452
/// </summary>
class BestTimeToBuyAndSellStockWithCooldownSolution
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
        if (prices == null || prices.Length == 0) return 0;
        var length = prices.Length;
        int[] buyEV = new int[length];
        int[] sellEV = new int[length];
        int[] cooldownEV = new int[length];
        sellEV[0] = 0;
        cooldownEV[0] = 0;
        buyEV[0] = -prices[0];
        for( int i = 1; i < length; i++)
        {
            var price = prices[i];
            cooldownEV[i] = sellEV[i - 1];
            sellEV[i] = Math.Max(sellEV[i - 1], buyEV[i - 1] + price);
            buyEV[i] = Math.Max(buyEV[i - 1], cooldownEV[i - 1] - price);
        }

        return Math.Max(sellEV[length - 1], cooldownEV[length - 1]);
    }
}
/*
public class Solution {
    public int MaxProfit(int[] prices) {
         int buy = int.MinValue, pre_buy = 0, sell = 0, pre_sell = 0;
            for (int i = 0; i < prices.Length; i++)
            {
                pre_buy = buy;
                buy = Math.Max(pre_sell - prices[i], pre_buy);
                pre_sell = sell;
                sell = Math.Max(pre_buy + prices[i], pre_sell);
            }
            return sell;
    }
} 
public class Solution {
    public int MaxProfit(int[] prices) {
          if (prices.Length == 0)
                return 0;
         var sums = new int[prices.Length];
            for (var i = 1; i < prices.Length; i++)
            {
                var max = sums[i - 1];
                for (var j = i - 1; j >= 0; j--)
                {
                    var sum = prices[i] - prices[j];
                    if (j - 2 > 0)
                    {
                        sum = sum + sums[j - 2];
                    }
                    max = Math.Max(sum, max);
                }
                sums[i] = max;
            }
            return sums[prices.Length - 1];
    }
}
public class Solution {
    public int MaxProfit(int[] prices) {
        if(prices.Length <= 1) return 0;
        
        int[] profits = new int[prices.Length];
        profits[0] = 0;
        profits[1] = Math.Max(0, prices[1] - prices[0]);
        for(int i = 2; i<prices.Length; i++){
            int maxProfit = profits[i-1];
            
            for(int j = i-1; j>=0; j--){
                int n1 = prices[i] - prices[j];
                int n2 = j - 2 >= 0 ? profits[j-2] : 0;
                maxProfit = Math.Max(maxProfit, n1 + n2);
            }
            
            profits[i] = maxProfit;
        }
        
        return profits[prices.Length - 1];
    }
}
*/
