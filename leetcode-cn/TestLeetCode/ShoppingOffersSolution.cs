using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
在LeetCode商店中， 有许多在售的物品。

然而，也有一些大礼包，每个大礼包以优惠的价格捆绑销售一组物品。

现给定每个物品的价格，每个大礼包包含物品的清单，以及待购物品清单。请输出确切完成待购清单的最低花费。

每个大礼包的由一个数组中的一组数据描述，最后一个数字代表大礼包的价格，其他数字分别表示内含的其他种类物品的数量。

任意大礼包可无限次购买。

示例 1:

输入: [2,5], [[3,0,5],[1,2,10]], [3,2]
输出: 14
解释: 
有A和B两种物品，价格分别为¥2和¥5。
大礼包1，你可以以¥5的价格购买3A和0B。
大礼包2， 你可以以¥10的价格购买1A和2B。
你需要购买3个A和2个B， 所以你付了¥10购买了1A和2B（大礼包2），以及¥4购买2A。
示例 2:

输入: [2,3,4], [[1,1,0,4],[2,2,1,9]], [1,2,1]
输出: 11
解释: 
A，B，C的价格分别为¥2，¥3，¥4.
你可以用¥4购买1A和1B，也可以用¥9购买2A，2B和1C。
你需要买1A，2B和1C，所以你付了¥4买了1A和1B（大礼包1），以及¥3购买1B， ¥4购买1C。
你不可以购买超出待购清单的物品，尽管购买大礼包2更加便宜。
说明:

最多6种物品， 100种大礼包。
每种物品，你最多只需要购买6个。
你不可以购买超出待购清单的物品，即使更便宜。 
*/
/// <summary>
/// https://leetcode-cn.com/problems/shopping-offers/
/// 638. 大礼包
/// https://blog.csdn.net/start_lie/article/details/84111984
/// </summary>
class ShoppingOffersSolution
{
    public void Test()
    {
        var ret = ShoppingOffers(new int[] { 2, 5 }, new int[][] { new int[] { 3, 0, 5 }, new int[] { 1, 2, 10 } }, new int[] { 3, 2 });
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int ShoppingOffers(IList<int> price, IList<IList<int>> special, IList<int> needs)
    {
        Dictionary<int, int> dp = new Dictionary<int, int>();
        dp.Add(0, 0);
        return Dig(price, special, needs, dp, 0);
    }

    private static int Dig(IList<int> price, IList<IList<int>> special, IList<int> needs, Dictionary<int, int> dp, int index)
    {
        //int key = 0;
        int maxPrice = 0;
        for (int i = 0; i < needs.Count; i++)
        {
            //key = key * 10 + needs[i];
            maxPrice += price[i] * needs[i];
        }
        if (maxPrice == 0) return 0;

        // 这道题目的数据没有重复计算，不需要缓存中间结果
        //if (dp.ContainsKey(key)) return dp[key];

        int specialSize = special.Count;
        int priceSize = price.Count;

        //遍历所有礼包
        for( int i = index; i < specialSize; i++)
        {
            var specialItem = special[i];
            bool isOverBuy = false;
            for (int j = 0; j < priceSize; j++)
            {
                needs[j] -= specialItem[j];
                //判断是否超买
                if ( needs[j] < 0 ) isOverBuy = true;
            }
            if (!isOverBuy)
            {
                // 可以买
                var lowerPrice = specialItem[priceSize] + Dig(price, special, needs, dp, i );
                if (lowerPrice < maxPrice) maxPrice = lowerPrice;
            }

            // 恢复
            for (int j = 0; j < priceSize; j++) needs[j] += specialItem[j];
        }

        //dp[key] = maxPrice;
        return maxPrice;
    }

}
/*
public class Solution {
    public int ShoppingOffers(IList<int> price, IList<IList<int>> special, IList<int> needs) {
        if (isEmpty(price)) return 0;
        int count = price.Count;
        int restul = int.MaxValue;
        bool can = false;
        foreach (var package in special)
        {
            var flag = true;
            for (int n = 0; n < count; n++)
            {
                if (package[n] > needs[n])
                {
                    flag = false;
                    break;
                }
            }
            if (flag)
            {
                can = true;
                var nd = new List<int>(needs);
                for (int i = 0; i < count; i++)
                    nd[i] -= package[i];
                int c = ShoppingOffers(price, special, nd) + package[count];
                if (c < restul)
                {
                    restul = c;
                }
            }
        }

        if (can)
        {
            return restul;
        }
        else
        {
            var sum = 0;
            for (int i = 0; i < count; i++)
            {
                sum += (price[i] * needs[i]);
            }
            return sum;
        }
    }
    private bool isEmpty(IList<int> arr)
    {
        foreach (var item in arr)
        {
            if (item > 0) return false;
        }
        return true;
    }
} 
*/
