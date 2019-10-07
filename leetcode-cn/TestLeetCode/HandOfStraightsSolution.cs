using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
爱丽丝有一手（hand）由整数数组给定的牌。 

现在她想把牌重新排列成组，使得每个组的大小都是 W，且由 W 张连续的牌组成。

如果她可以完成分组就返回 true，否则返回 false。

 

示例 1：

输入：hand = [1,2,3,6,2,3,4,7,8], W = 3
输出：true
解释：爱丽丝的手牌可以被重新排列为 [1,2,3]，[2,3,4]，[6,7,8]。
示例 2：

输入：hand = [1,2,3,4,5], W = 4
输出：false
解释：爱丽丝的手牌无法被重新排列成几个大小为 4 的组。
 

提示：

1 <= hand.Length <= 10000
0 <= hand[i] <= 10^9
1 <= W <= hand.Length
在真实的面试中遇到过这道题？
*/
/// <summary>
/// https://leetcode-cn.com/problems/hand-of-straights/
/// 846. 一手顺子
/// 
/// </summary>
class HandOfStraightsSolution
{
    public void Test()
    {
        int[] nums = new int[] {1, 2, 3, 6, 2, 3, 4, 7, 8};
        //int k = 6;
        var ret = IsNStraightHand(nums,3);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public bool IsNStraightHand(int[] hand, int W)
    {
        if (hand == null || hand.Length == 0 || hand.Length % W != 0) return false;

        var map = new Dictionary<int, int>();
        foreach (int v in hand)
            if (map.ContainsKey(v)) map[v]++; 
            else map.Add(v, 1);

        hand = map.Keys.ToArray();
        Array.Sort(hand);
        int index = 0;
        for(;index < hand.Length;)
        {
            int h = hand[index];
            int count = map[h];
            int nextIndex = -1;

            // 判断 map 中是否有足够的元素构成顺子
            for (int j = 1; j < W; j++)
            {
                var key = h + j;
                if (!map.ContainsKey(key)) return false;

                int c = map[key];

                if (c < 1 || c < count) return false;
                if (c == count) {
                    map[key] = 0;
                    continue;
                }

                map[key] = c - count;
                if (nextIndex == -1) nextIndex = index + j;
            }
            if (nextIndex == -1) nextIndex = index + W;
            index = nextIndex;
        }
        return true;
    }
}