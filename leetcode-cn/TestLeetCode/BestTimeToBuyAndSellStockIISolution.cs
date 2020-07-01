/*
给定一个数组，它的第 i 个元素是一支给定股票第 i 天的价格。

设计一个算法来计算你所能获取的最大利润。你可以尽可能地完成更多的交易（多次买卖一支股票）。

注意：你不能同时参与多笔交易（你必须在再次购买前出售掉之前的股票）。

 

示例 1:

输入: [7,1,5,3,6,4]
输出: 7
解释: 在第 2 天（股票价格 = 1）的时候买入，在第 3 天（股票价格 = 5）的时候卖出, 这笔交易所能获得利润 = 5-1 = 4 。
     随后，在第 4 天（股票价格 = 3）的时候买入，在第 5 天（股票价格 = 6）的时候卖出, 这笔交易所能获得利润 = 6-3 = 3 。
示例 2:

输入: [1,2,3,4,5]
输出: 4
解释: 在第 1 天（股票价格 = 1）的时候买入，在第 5 天 （股票价格 = 5）的时候卖出, 这笔交易所能获得利润 = 5-1 = 4 。
     注意你不能在第 1 天和第 2 天接连购买股票，之后再将它们卖出。
     因为这样属于同时参与了多笔交易，你必须在再次购买前出售掉之前的股票。
示例 3:

输入: [7,6,4,3,1]
输出: 0
解释: 在这种情况下, 没有交易完成, 所以最大利润为 0。
 

提示：

1 <= prices.length <= 3 * 10 ^ 4
0 <= prices[i] <= 10 ^ 4

*/

/// <summary>
/// https://leetcode-cn.com/problems/best-time-to-buy-and-sell-stock-ii/
/// 122. 买卖股票的最佳时机 II
///
///
/// </summary>
internal class BestTimeToBuyAndSellStockIISolution
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
        int len = prices.Length;
        int i = 0;
        int ret = 0;
        while (i < len - 1) 
        {
            while (i < len - 1 && prices[i + 1] <= prices[i] ) i++;
            int valley = prices[i];

            while (i < len - 1 && prices[i] <= prices[i + 1]) i++;
            int peak = prices[i];

            ret += peak - valley;
        }
        return ret;
    }
}

/*

买卖股票的最佳时机 II
力扣 (LeetCode)
发布于 2018-10-25
78.3k
摘要
我们必须确定通过交易能够获得的最大利润（对于交易次数没有限制）。为此，我们需要找出那些共同使得利润最大化的买入及卖出价格。

解决方案
方法一：暴力法
这种情况下，我们只需要计算与所有可能的交易组合相对应的利润，并找出它们中的最大利润。


class Solution {
    public int maxProfit(int[] prices) {
        return calculate(prices, 0);
    }

    public int calculate(int prices[], int s) {
        if (s >= prices.length)
            return 0;
        int max = 0;
        for (int start = s; start < prices.length; start++) {
            int maxprofit = 0;
            for (int i = start + 1; i < prices.length; i++) {
                if (prices[start] < prices[i]) {
                    int profit = calculate(prices, i + 1) + prices[i] - prices[start];
                    if (profit > maxprofit)
                        maxprofit = profit;
                }
            }
            if (maxprofit > max)
                max = maxprofit;
        }
        return max;
    }
}
复杂度分析

时间复杂度：O(n^n)O(n 
n
 )，调用递归函数 n^nn 
n
  次。

空间复杂度：O(n)O(n)，递归的深度为 nn。

方法二：峰谷法
算法

假设给定的数组为：

[7, 1, 5, 3, 6, 4]

如果我们在图表上绘制给定数组中的数字，我们将会得到：

Profit Graph

如果我们分析图表，那么我们的兴趣点是连续的峰和谷。

用数学语言描述为：

Total Profit= \sum_{i}(height(peak_i)-height(valley_i))
TotalProfit= 
i
∑
​	
 (height(peak 
i
​	
 )−height(valley 
i
​	
 ))

关键是我们需要考虑到紧跟谷的每一个峰值以最大化利润。如果我们试图跳过其中一个峰值来获取更多利润，那么我们最终将失去其中一笔交易中获得的利润，从而导致总利润的降低。

例如，在上述情况下，如果我们跳过 peak_ipeak 
i
​	
  和 valley_jvalley 
j
​	
  试图通过考虑差异较大的点以获取更多的利润，获得的净利润总是会小与包含它们而获得的净利润，因为 CC 总是小于 A+BA+B。


class Solution {
    public int maxProfit(int[] prices) {
        int i = 0;
        int valley = prices[0];
        int peak = prices[0];
        int maxprofit = 0;
        while (i < prices.length - 1) {
            while (i < prices.length - 1 && prices[i] >= prices[i + 1])
                i++;
            valley = prices[i];
            while (i < prices.length - 1 && prices[i] <= prices[i + 1])
                i++;
            peak = prices[i];
            maxprofit += peak - valley;
        }
        return maxprofit;
    }
}
复杂度分析

时间复杂度：O(n)O(n)。遍历一次。

空间复杂度：O(1)O(1)。需要常量的空间。

方法三：简单的一次遍历
算法

该解决方案遵循 方法二 的本身使用的逻辑，但有一些轻微的变化。在这种情况下，我们可以简单地继续在斜坡上爬升并持续增加从连续交易中获得的利润，而不是在谷之后寻找每个峰值。最后，我们将有效地使用峰值和谷值，但我们不需要跟踪峰值和谷值对应的成本以及最大利润，但我们可以直接继续增加加数组的连续数字之间的差值，如果第二个数字大于第一个数字，我们获得的总和将是最大利润。这种方法将简化解决方案。
这个例子可以更清楚地展现上述情况：

[1, 7, 2, 3, 6, 7, 6, 7]

与此数组对应的图形是：

Profit Graph

从上图中，我们可以观察到 A+B+CA+B+C 的和等于差值 DD 所对应的连续峰和谷的高度之差。


class Solution {
    public int maxProfit(int[] prices) {
        int maxprofit = 0;
        for (int i = 1; i < prices.length; i++) {
            if (prices[i] > prices[i - 1])
                maxprofit += prices[i] - prices[i - 1];
        }
        return maxprofit;
    }
}
复杂度分析

时间复杂度：O(n)O(n)，遍历一次。
空间复杂度：O(1)O(1)，需要常量的空间。

public class Solution
{
	public int MaxProfit(int[] prices)
	{
		if (prices == null || prices.Length == 1) return 0;
		int profit=0;
		int j = 0;
		int k = 1;
		for (; k < prices.Length; k++)
		{
			if (prices[k] > prices[j])
			{
				profit = profit + prices[k] - prices[j];
			}
			j = k;
		}
		return profit;
	}
}

public class Solution {
    public int MaxProfit(int[] prices) {
        if (prices.Length <= 0) return 0;

            int result = 0;
            int length = prices.Length;
            for (int i = 1; i < length; i++)
            {
                if (prices[i] > prices[i - 1])
                {
                    result += (prices[i] - prices[i - 1]);
                }
            }


            return result;
    }
}


*/
