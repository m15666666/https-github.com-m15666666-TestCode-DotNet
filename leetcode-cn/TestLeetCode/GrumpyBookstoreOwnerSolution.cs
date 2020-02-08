using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
今天，书店老板有一家店打算试营业 customers.length 分钟。每分钟都有一些顾客（customers[i]）会进入书店，所有这些顾客都会在那一分钟结束后离开。

在某些时候，书店老板会生气。 如果书店老板在第 i 分钟生气，那么 grumpy[i] = 1，否则 grumpy[i] = 0。 当书店老板生气时，那一分钟的顾客就会不满意，不生气则他们是满意的。

书店老板知道一个秘密技巧，能抑制自己的情绪，可以让自己连续 X 分钟不生气，但却只能使用一次。

请你返回这一天营业下来，最多有多少客户能够感到满意的数量。
 

示例：

输入：customers = [1,0,1,2,1,1,7,5], grumpy = [0,1,0,1,0,1,0,1], X = 3
输出：16
解释：
书店老板在最后 3 分钟保持冷静。
感到满意的最大客户数量 = 1 + 1 + 1 + 1 + 7 + 5 = 16.
 

提示：

1 <= X <= customers.length == grumpy.length <= 20000
0 <= customers[i] <= 1000
0 <= grumpy[i] <= 1
*/
/// <summary>
/// https://leetcode-cn.com/problems/grumpy-bookstore-owner/
/// 1052. 爱生气的书店老板
/// 
/// </summary>
class GrumpyBookstoreOwnerSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int MaxSatisfied(int[] customers, int[] grumpy, int X)
    {
        int sumOfNoAngry = 0;
        for (int i = 0; i < grumpy.Length; i++)
            if (grumpy[i] == 0)
            {
    			sumOfNoAngry += customers[i];
                customers[i] = 0;
            }

        int max = 0;
        int sumOfWindow = 0;
        int leftIndex = 0;
        int windowLength = 0;
        foreach (var c in customers )
        {
            sumOfWindow += c;
            if (X <= windowLength) sumOfWindow -= customers[leftIndex++];
            else windowLength++;

            if (max < sumOfWindow) max = sumOfWindow;
        }

        return sumOfNoAngry + max;
    }
}
/*
滑动窗口计算连续X个数之和,时间复杂度O(N)
Zhang20191031
77 阅读
2020011901.PNG

解题思路
//先遍历一遍数组grumpy,当grumpy[i]的值为0时,out加上customers[i]的值,并将该customers[i]重新置为0;
//再遍历一遍数组customers,用滑动窗口来计算customers数组中的连续X个数之和(记为sum),并且用max记录当前出现的最大的sum;
//最后返回(out+max)

代码
class Solution {
    public int maxSatisfied(int[] customers, int[] grumpy, int X) {
        //解1：耗时3ms
    	int out = 0;
    	int max = 0;
    	int sum = 0;
    	for(int i =0;i<grumpy.length;i++) {
    		if(grumpy[i]==0) {
    			out += customers[i];
    			customers[i] = 0;
    		}
    	}
    	int i=0;
    	int cnt = -1;
    	while(i<customers.length) {
    		sum += customers[i];
    		if(i>=X-1) {
        		if(sum >= max) {
        			max = sum;
        		}
        		cnt++;
        		if(cnt>=0) {
        			sum -= customers[cnt];//窗口长度为X,窗口在向前滑动一位之前,需要将窗口中的最左边的一位数减掉
        		}
    		}
    		i++;
    	}
    	out += max;
        return out;
    }
} 
*/
