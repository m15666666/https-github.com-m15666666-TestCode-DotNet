using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// https://leetcode-cn.com/problems/bitwise-and-of-numbers-range/
/// 201. 数字范围按位与
/// 给定范围 [m, n]，其中 0 <= m <= n <= 2147483647，返回此范围内所有数字的按位与（包含 m, n 两端点）。
/// 示例 1: 
/// 输入: [5,7]
/// 输出: 4
/// 示例 2:
/// 输入: [0,1]
/// 输出: 0
/// </summary>
class BitwiseANDOfNumbersRangeSolution
{
    public static void Test()
    {
        var ret = RangeBitwiseAnd(5, 7);
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public static int RangeBitwiseAnd(int m, int n)
    {
        while (m < n) n &= (n - 1);
        return n;
    }
    //public static int RangeBitwiseAnd(int m, int n)
    //{
    //    if (m == 0) return 0;
    //    if (m == n) return m;

    //    int ret = 0;

    //    // 首先右移的是第31位（右移30位），32位始终为零（非负整数）
    //    int rightShiftCount = 30;
    //    while( 0 <= rightShiftCount)
    //    {
    //        var mbit = (m >> rightShiftCount) & 1;
    //        var nbit = (n >> rightShiftCount) & 1;

    //        if (mbit != nbit) break; // 从这个位及后面的低位都是0.

    //        if (mbit == 1) ret |= (1 << rightShiftCount);

    //        --rightShiftCount;
    //    }
        
    //    return ret;
    //}
}
/*

数字范围按位与
力扣官方题解
发布于 4 天前
16.7k
概述
最直观的解决方案就是迭代范围内的每个数字，依次执行按位与运算，得到最终的结果，但此方法在 [m,n][m,n] 范围较大的测试用例中会因超出时间限制而无法通过，因此我们需要另寻他路。

我们观察按位与运算的性质。对于一系列的位，例如 [1, 1, 0, 1, 1][1,1,0,1,1]，只要有一个零值的位，那么这一系列位的按位与运算结果都将为零。

回到本题，首先我们可以对范围内的每个数字用二进制的字符串表示，例如 9=00001001_{(2)}9=00001001 
(2)
​	
 ，然后我们将每个二进制字符串的位置对齐。

fig1

在上图的例子中，我们可以发现，对所有数字执行按位与运算的结果是所有对应二进制字符串的公共前缀再用零补上后面的剩余位。

那么这个规律是否正确呢？我们可以进行简单的证明。假设对于所有这些二进制串，前 ii 位均相同，第 i+1i+1 位开始不同，由于 [m,n][m,n] 连续，所以第 i+1i+1 位在 [m,n][m,n] 的数字范围从小到大列举出来一定是前面全部是 00，后面全部是 11，在上图中对应 [9,11][9,11] 均为 00，[12,12][12,12] 均为 11。并且一定存在连续的两个数 xx 和 x+1x+1，满足 xx 的第 i+1i+1 位为 00，后面全为 11，x+1x+1 的第 i+1i+1 位为 11，后面全为 00，对应上图中的例子即为 1111 和 1212。这种形如 0111 \ldots0111… 和 1000 \ldots1000… 的二进制串的按位与的结果一定为 0000 \ldots0000…，因此第 i+1i+1 位开始的剩余位均为 00，前 ii 位由于均相同，因此按位与结果不变。最后的答案即为二进制字符串的公共前缀再用零补上后面的剩余位。

进一步来说，所有这些二进制字符串的公共前缀也即指定范围的起始和结束数字 mm 和 nn 的公共前缀（即在上面的示例中分别为 99 和 1212）。

因此，最终我们可以将问题重新表述为：给定两个整数，我们要找到它们对应的二进制字符串的公共前缀。

方法一：位移
思路

鉴于上述问题的陈述，我们的目的是求出两个给定数字的二进制字符串的公共前缀，这里给出的第一个方法是采用位移操作。

我们的想法是将两个数字不断向右移动，直到数字相等，即数字被缩减为它们的公共前缀。然后，通过将公共前缀向左移动，将零添加到公共前缀的右边以获得最终结果。

fig2

算法

如上述所说，算法由两个步骤组成：

我们通过右移，将两个数字压缩为它们的公共前缀。在迭代过程中，我们计算执行的右移操作数。
将得到的公共前缀左移相同的操作数得到结果。


代码


class Solution {
    public int rangeBitwiseAnd(int m, int n) {
        int shift = 0;
        // 找到公共前缀
        while (m < n) {
            m >>= 1;
            n >>= 1;
            ++shift;
        }
        return m << shift;
    }
}
复杂度分析

时间复杂度：O(\log n)O(logn)。算法的时间复杂度取决于 mm 和 nn 的二进制位数，由于 m \le nm≤n，因此时间复杂度取决于 nn 的二进制位数。
空间复杂度：O(1)O(1)。我们只需要常数空间存放若干变量。
方法二：Brian Kernighan 算法
思路与算法

还有一个位移相关的算法叫做「Brian Kernighan 算法」，它用于清除二进制串中最右边的 11。

Brian Kernighan 算法的关键在于我们每次对 \textit{number}number 和 \textit{number}-1number−1 之间进行按位与运算后，\textit{number}number 中最右边的 11 会被抹去变成 00。

fig9

基于上述技巧，我们可以用它来计算两个二进制字符串的公共前缀。

其思想是，对于给定的范围 [m,n][m,n]（m<nm<n），我们可以对数字 nn 迭代地应用上述技巧，清除最右边的 11，直到它小于或等于 mm，此时非公共前缀部分的 11 均被消去。因此最后我们返回 nn 即可。

fig10

在上图所示的示例（m=9, n=12m=9,n=12）中，公共前缀是 0000100001。在对数字 nn 应用 Brian Kernighan 算法后，后面三位都将变为零，最后我们返回 nn 即可。


class Solution {
    public int rangeBitwiseAnd(int m, int n) {
        while (m < n) {
            // 抹去最右边的 1
            n = n & (n - 1);
        }
        return n;
    }
}
复杂度分析

时间复杂度：O(\log n)O(logn)。和位移方法类似，算法的时间复杂度取决于 mm 和 nn 二进制展开的位数。尽管和位移方法具有相同的渐近复杂度，但 Brian Kernighan 的算法需要的迭代次数会更少，因为它跳过了两个数字之间的所有零位。
空间复杂度：O(1)O(1)。我们只需要常数空间存放若干变量。
结语
「461. 汉明距离」可以作为另一个练习应用 Brian Kernighan 算法的题目。

下一篇：巨好理解的位运算思路

public class Solution {
    public int RangeBitwiseAnd(int m, int n) {
            int shift_step=0;
            while (m!=n)
            {
                m=m>>1;
                n=n>>1;
                shift_step++;
            }
            return m<<shift_step;
    }
}

public class Solution {
    public int RangeBitwiseAnd(int m, int n) {
        while(m<n)
        n=n&(n-1);
        return n;
    }
}

public class Solution
{
    public int RangeBitwiseAnd(int m, int n)
    {
        // 5 6 7
        long b = 1;
        int index = 31, ret = 0;
        while (index >= 0 && ((b << index) & n) == 0) index--;
        while (index >= 0 && ((b << index) & m) == ((b << index) & n))
        {
            ret += (int)((b << index) & m);
            index--;
        }
        return ret;
    }
}




public int RangeBitwiseAnd(int m, int n) {
    int i  = 0;
    while((m >> i) != (n >> i))
        i++;
    return (m >> i) << i;
}

*/
