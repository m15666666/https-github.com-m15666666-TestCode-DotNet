using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
两个整数的 汉明距离 指的是这两个数字的二进制数对应位不同的数量。

计算一个数组中，任意两个数之间汉明距离的总和。

示例:

输入: 4, 14, 2

输出: 6

解释: 在二进制表示中，4表示为0100，14表示为1110，2表示为0010。（这样表示是为了体现后四位之间关系）
所以答案为：
HammingDistance(4, 14) + HammingDistance(4, 2) + HammingDistance(14, 2) = 2 + 2 + 2 = 6.
注意:

数组中元素的范围为从 0到 10^9。
数组的长度不超过 10^4。 
*/
/// <summary>
/// https://leetcode-cn.com/problems/total-hamming-distance/
/// 477. 汉明距离总和
/// https://blog.csdn.net/litteng/article/details/80019765
/// https://blog.csdn.net/litteng/article/details/80026641
/// </summary>
class TotalHammingDistanceSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int TotalHammingDistance(int[] nums)
    {
        int ret = 0;
        int n = nums.Length;
        for (int i = 0; i < 32; i++)
        {
            int oneCount = 0;
            for (int j = 0; j < n; j++)
            {
                if ((nums[j] & 1) == 1)
                {
                    oneCount++;
                }
                nums[j] >>= 1;
            }
            int zeroCount = n - oneCount;
            ret += oneCount * zeroCount;
        }
        return ret;
    }
}
/*
public class Solution
{
    public int TotalHammingDistance(int[] nums)
    {
        int result = 0;
        for (int i = 0; i < 8 * sizeof(int); i++)
        {
            int distance = 0;
            foreach (var val in nums)
                distance += val >> i & 1;
            
            result += distance * (nums.Length - distance);
        }
        
        return result;
    }
}
public class Solution {
    public int TotalHammingDistance(int[] nums) {
        int sum=0;
        int cnt=0;
        foreach(var n in nums)
        {
            if((n&1)==0)
                cnt++;
        }
        sum+=cnt*(nums.Length-cnt);
        for(int i=0;i<31;i++)
        {
            cnt=0;
            for(int j=0;j<nums.Length;j++)
            {
                nums[j]=(nums[j]>>1);
                if((nums[j]&1)==0)
                    cnt++;
            }
            sum+=cnt*(nums.Length-cnt);
        }
        return sum;
    }
}
public class Solution {
    public int TotalHammingDistance(int[] nums) {
        int k = 0;

        for (int i = 0; i < 32; i++)
        {
            int n = 0;
            foreach (int num in nums)
            {
                if ((num & (1 << i)) != 0) n++;
            }
            k += n * (nums.Length - n);
        }
        return k;
    }
}
*/
