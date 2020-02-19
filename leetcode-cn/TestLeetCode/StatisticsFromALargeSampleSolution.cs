using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
我们对 0 到 255 之间的整数进行采样，并将结果存储在数组 count 中：count[k] 就是整数 k 的采样个数。

我们以 浮点数 数组的形式，分别返回样本的最小值、最大值、平均值、中位数和众数。其中，众数是保证唯一的。

我们先来回顾一下中位数的知识：

如果样本中的元素有序，并且元素数量为奇数时，中位数为最中间的那个元素；
如果样本中的元素有序，并且元素数量为偶数时，中位数为中间的两个元素的平均值。
 

示例 1：

输入：count = [0,1,3,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0]
输出：[1.00000,3.00000,2.37500,2.50000,3.00000]
示例 2：

输入：count = [0,4,3,2,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0]
输出：[1.00000,4.00000,2.18182,2.00000,1.00000]
 

提示：

count.length == 256
1 <= sum(count) <= 10^9
计数表示的众数是唯一的
答案与真实值误差在 10^-5 以内就会被视为正确答案
*/
/// <summary>
/// https://leetcode-cn.com/problems/statistics-from-a-large-sample/
/// 1093. 大样本统计
/// 
/// </summary>
class StatisticsFromALargeSampleSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public double[] SampleStats(int[] count)
    {
        double[] ret = new double[5];

        long sum = 0, countOfSum = 0;
        int min = -1, max = -1;
        int maxCount = 0;
        for (int i = 0; i < count.Length; i++)
        {
            int cnt = count[i];
            if (cnt == 0) continue;

            if (min == -1) min = i;

            max = i;
            sum += cnt * i;
            countOfSum += cnt;

            if (maxCount < cnt)
            {
                maxCount = cnt;
                ret[4] = i;
            }
        }

        ret[0] = min;
        ret[1] = max;

        ret[2] = sum * 1.0 / countOfSum;

        int currentCount = 0;
        long halfCount = countOfSum / 2;
        int a = -1, b = -1;
        for (int i = 0; i < count.Length; i++)
        {
            currentCount += count[i];
            if (a == -1 && halfCount <= currentCount) a = i;
            if (b == -1 && halfCount < currentCount)
            {
                b = i;
                break;
            }
        }
        ret[3] = (countOfSum % 2) == 0 ? (a + b) / 2.0 : b;

        return ret;
    }
}
/*
正常解法 然后就双100% 了 挺懵逼的。。。
azyl99
发布于 7 个月前
610 阅读
执行用时 :
1 ms
, 在所有 Java 提交中击败了
100.00%
的用户
内存消耗 :
37.3 MB
, 在所有 Java 提交中击败了
100.00%
的用户

class Solution {
    public double[] sampleStats(int[] count) {
        double[] result = new double[5];

        long sum = 0, csum = 0;
        int min = -1, max = -1;
        int maxCount = 0;
        for (int i = 0; i < count.length; i++) {
            int cnt = count[i];
            if (cnt == 0) {
                continue;
            }
            if (min == -1) {
                min = i;
            }
            max = i;
            sum += cnt * i;
            csum += cnt;

            if (maxCount < cnt) {
                maxCount = cnt;
                result[4] = i;
            }
        }

        result[2] = sum * 1.0 / csum;

        int curCount = 0;
        int a = -1, b = -1;
        for (int i = 0; i < count.length; i++) {
            int cnt = count[i];
            curCount += cnt;
            if (a == -1 && curCount >= csum / 2) {
                a = i;
            }
            if (b == -1 && curCount >= csum / 2 + 1) {
                b = i;
            }
        }
        if ((csum & 1) == 1) {
            result[3] = b;
        } else {
            result[3] = (a + b) / 2.0;
        }

        result[0] = min;
        result[1] = max;

        return result;
    }
}
下一篇：重点在于中位数
 
*/
