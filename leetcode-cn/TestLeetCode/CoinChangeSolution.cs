using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
给定不同面额的硬币 coins 和一个总金额 amount。编写一个函数来计算可以凑成总金额所需的最少的硬币个数。如果没有任何一种硬币组合能组成总金额，返回 -1。

示例 1:

输入: coins = [1, 2, 5], amount = 11
输出: 3 
解释: 11 = 5 + 5 + 1
示例 2:

输入: coins = [2], amount = 3
输出: -1
说明:
你可以认为每种硬币的数量是无限的。 
*/
/// <summary>
/// https://leetcode-cn.com/problems/coin-change/
/// 322. 零钱兑换
/// https://blog.csdn.net/fisherming/article/details/79839545
/// </summary>
class CoinChangeSolution
{
    public void Test()
    {
        //var ret = CoinChange(new int[] { 1, 2, 5 }, 11);
        var ret = CoinChange(new int[] { 1 }, 0);
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    
    public int CoinChange(int[] coins, int amount)
    {
        if (amount < 1) return 0;
        _map = new Dictionary<int, int>();
        return search( amount, coins );
    }

    private Dictionary<int, int> _map = null;
    private int search( int amount, int[] coins )
    {
        var key = amount;
        if (_map.ContainsKey(key)) return _map[key];

        int subSteps = int.MaxValue;
        for (int i = 0; i < coins.Length; i++)
        {
            var v = amount - coins[i];
            if (v < 0) continue;
            if (v == 0) return _map[key] = 1;

            var s = search( v, coins );
            if ( 0 < s ) if (s < subSteps) subSteps = s;
        }
        return _map[key] = subSteps == int.MaxValue ? -1 : subSteps + 1;
    }
}
/*
public class Solution {
	public int CoinChange(int[] coins, int amount) {
		if (coins == null || coins.Length == 0 || amount < 0)
			return -1;
		int[] coinsComb = new int[amount + 1];
		coinsComb[0] = 0;
		for (int i = 1; i <= amount; i++)
		{
			coinsComb[i] = amount + 1;
		}
		for (int i = 1; i <= amount; i++)
		{
			foreach (int coin in coins)
			{
				if (i - coin >= 0) 
				{
					coinsComb [i] = Math.Min(coinsComb [i] ,coinsComb [i - coin] + 1);
				}
			}
		}
		return coinsComb[amount] > amount? -1:coinsComb[amount];
	}
}
public class Solution {
    public int CoinChange(int[] coins, int amount) {
        if (amount==0)
        {
            return 0;
        }
        Array.Sort(coins);  //面值右小到大排序
        int[] d = new int[amount + 1]; //d[i]表示组成价格i至少需要多少个硬币 d[0]=0;
        for (int i = 1; i <= amount; i++)
        {
            int minNum = Int16.MaxValue;  //最少硬币个数
            for (int j = 0; j < coins.Length; j++)
            {
                if (i == coins[j])
                {
                    d[i] = 1;
                    minNum = 1;
                    break;
                }
                else
                {
                    if (i - coins[j] > 0)
                    {
                        if (d[i - coins[j]] > 0)
                        {
                            if (d[i - coins[j]] + 1 < minNum)
                            {
                                minNum = d[i - coins[j]] + 1;
                            }
                        }
                    }
                }
            }
            if (minNum != Int16.MaxValue)
            {
                d[i] = minNum;
            }
        }
        if (d[amount] == 0)
        {
            return -1;
        }

        return d[amount];
    }
}
*/
