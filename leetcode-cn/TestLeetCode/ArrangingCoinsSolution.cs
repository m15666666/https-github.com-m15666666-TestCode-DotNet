using System;

/*
你总共有 n 枚硬币，你需要将它们摆成一个阶梯形状，第 k 行就必须正好有 k 枚硬币。

给定一个数字 n，找出可形成完整阶梯行的总行数。

n 是一个非负整数，并且在32位有符号整型的范围内。

示例 1:

n = 5

硬币可排列成以下几行:
¤
¤ ¤
¤ ¤

因为第三行不完整，所以返回2.
示例 2:

n = 8

硬币可排列成以下几行:
¤
¤ ¤
¤ ¤ ¤
¤ ¤

因为第四行不完整，所以返回3.

*/

/// <summary>
/// https://leetcode-cn.com/problems/arranging-coins/
/// 441. 排列硬币
///
/// </summary>
internal class ArrangingCoinsSolution
{
    public void Test()
    {
        var ret = ArrangeCoins(1804289383);
    }

    public int ArrangeCoins(int n)
    {
        // 下面三个版本都可以工作

        // 等差数列求和公式：设 k*(k+1)/2 = n，
        // 利用求根公式 (-b +/- sqrt(b^2 - 4ac))/2a，k为非负舍弃负根：k = (-1 + sqrt(1 + 8n ))/ 2
        // 针对n = 1804289383，数字太大，必须首先将n转换为double，否则8*n将产生溢出，计算错误。
        ////double delta = 1 + 8 * (double)n;
        ////return (int)((-1 + Math.Sqrt(delta) ) / 2);

        long delta = 1 + 8 * (long)n;
        return (int)((-1 + Sqrt(delta)) / 2);
        static long Sqrt(long v)
        {
            if (v < 2) return v;
            long low = 1, high = v / 2;
            while (low <= high)
            {
                long mid = (low + high) / 2;
                long sumofk = mid * mid;
                // 针对n = 1804289383，数字太多，long类型也无法容纳，所以主动检测是否溢出
                if (sumofk < 0) // 溢出，减小上限
                {
                    high = mid - 1;
                    continue;
                }
                if (sumofk == v) return mid;
                if (v < sumofk) high = mid - 1;
                else low = mid + 1;
            }
            return low - 1; // 等价于high
        }

        if (n < 2) return n;
        long low = 1, high = n;
        while (low <= high)
        {
            long mid = (low + high) / 2;
            long sumofk = mid * (mid + 1) / 2;
            if ((sumofk <= n) && (n < sumofk + mid + 1)) return (int)mid;
            if (n < sumofk) high = mid - 1;
            else low = mid + 1;
        }
        throw new ArgumentException(nameof(n));
    }
}

/*
用求和公式反解
_Breiman
发布于 9 天前
108
解题思路
用求和公式反解得到x的表达式 x = ((1+8*n)**0.5 - 1)/2
x后面的小数表示不完整的行，因此向下取整
代码

class Solution:
    def arrangeCoins(self, n: int) -> int:
        import math
        # 高斯求和，求根公式
        return math.floor(((1+8*n)**0.5 - 1)/2)

class Solution:
    def arrangeCoins(self, n: int) -> int:
        if n==0: return 0
        k1, k2 = 1, n
        while k1<=k2:  # 标准二分查找写法
            k = (k1+k2)//2
            if k+k*(k-1)//2 <= n < k+1+k*(k+1)//2:  # 等差数列前k项和
                return k
            elif n<k+k*(k-1)//2:
                k2 = k-1
            else:
                k1 = k+1

public class Solution {
    public int ArrangeCoins(int n) {
        int low = 0;
        int high = n;
        while(low<=high){
            int mid = (low + high)/2;
            long sum = ((long)mid * (long)mid + mid)/2;
            if(sum == n){
                return mid;
            }
            if(sum < n){
                low = mid + 1;
            }
            if(sum > n){
                high = mid - 1;
            }
        }
        return high;
    }
}

*/