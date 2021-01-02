using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
猜数字游戏的规则如下：

每轮游戏，我都会从 1 到 n 随机选择一个数字。 请你猜选出的是哪个数字。
如果你猜错了，我会告诉你，你猜测的数字比我选出的数字是大了还是小了。
你可以通过调用一个预先定义好的接口 int guess(int num) 来获取猜测结果，返回值一共有 3 种可能的情况（-1，1 或 0）：

-1：我选出的数字比你猜的数字小 pick < num
1：我选出的数字比你猜的数字大 pick > num
0：我选出的数字和你猜的数字一样。恭喜！你猜对了！pick == num
 

示例 1：

输入：n = 10, pick = 6
输出：6
示例 2：

输入：n = 1, pick = 1
输出：1
示例 3：

输入：n = 2, pick = 1
输出：1
示例 4：

输入：n = 2, pick = 2
输出：2
 

提示：

1 <= n <= 231 - 1
1 <= pick <= n

*/
/// <summary>
/// https://leetcode-cn.com/problems/guess-number-higher-or-lower/
/// 374. 猜数字大小
/// 
/// 
/// 
/// </summary>
class GuessNumberHigherOrLowerSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int GuessNumber(int n) {
        int low = 1;
        int high = n;
        while (low <= high) {
            int mid = low + (high - low) / 2;
            int res = guess(mid);
            if (res == 0) return mid;
            else if (res < 0) high = mid - 1;
            else low = mid + 1;
        }
        return -1;
    }

    private int guess(int num) => 0;

}
/*
猜数字大小
力扣 (LeetCode)

发布于 2019-06-27
15.8k
方法 1：暴力
我们从 1 到 n-1 检查每一个数字，并调用 guessguess 函数。如果输入数字返回 0 说明它是答案。


/* The guess API is defined in the parent class GuessGame.
   @param num, your guess
   @return -1 if my number is lower, 1 if my number is higher, otherwise return 0
      int guess(int num); */

public class Solution extends GuessGame {
    public int guessNumber(int n) {
        for (int i = 1; i < n; i++)
            if (guess(i) == 0)
                return i;
        return n;
    }
}
复杂度分析

时间复杂度： O(n)O(n) 。我们从 1 到 n 扫描检查所有的数字。
空间复杂度： O(1)O(1) 。不需要使用额外空间。
方法 2：使用二分查找
算法

我们可以直接使用二分查找来找到需要的数字。我们从中间的数字开始，将数字传递到 guessguess 函数里。如果返回 -1, ，说明中间数字比答案大，就查找更小的那一半。类似的，如果返回 1 ，我们查找更大的一半，直到找到答案。


/* The guess API is defined in the parent class GuessGame.
   @param num, your guess
   @return -1 if my number is lower, 1 if my number is higher, otherwise return 0
      int guess(int num); */

public class Solution extends GuessGame {
    public int guessNumber(int n) {
        int low = 1;
        int high = n;
        while (low <= high) {
            int mid = low + (high - low) / 2;
            int res = guess(mid);
            if (res == 0)
                return mid;
            else if (res < 0)
                high = mid - 1;
            else
                low = mid + 1;
        }
        return -1;
    }
}
复杂度分析：

时间复杂度： O\big(\log_2 n\big)O(log 
2
​	
 n) 。为二分查找的时间复杂度。
空间复杂度： O(1)O(1) 。没有使用额外的空间。
方法 3：三分查找
算法

在二分查找中，我们选择中间元素作为分隔点。而在三分查找中，我们选择两个分隔点（比方记作 m1m1 和 m2m2），那么给定范围会被分成 3 个相等长度的部分。如果答案 numnum 比 m1m1 小，那么我们对 m1m1 左边的区间做三分查找。如果 numnum 在 m1m1 和 m2m2 中间，我们对中间区域进行三分查找。否则我们对 m2m2 右边的区域进行三分查找。


/* The guess API is defined in the parent class GuessGame.
   @param num, your guess
   @return -1 if my number is lower, 1 if my number is higher, otherwise return 0
      int guess(int num); */

public class Solution extends GuessGame {
    public int guessNumber(int n) {
        int low = 1;
        int high = n;
        while (low <= high) {
            int mid1 = low + (high - low) / 3;
            int mid2 = high - (high - low) / 3;
            int res1 = guess(mid1);
            int res2 = guess(mid2);
            if (res1 == 0)
                return mid1;
            if (res2 == 0)
                return mid2;
            else if (res1 < 0)
                high = mid1 - 1;
            else if (res2 > 0)
                low = mid2 + 1;
            else {
                low = mid1 + 1;
                high = mid2 - 1;
            }
        }
        return -1;
    }
}
复杂度分析

时间复杂度： O\big(\log_3 n \big)O(log 
3
​	
 n) 。为三分查找所需的时间复杂度。
空间复杂度： O(1)O(1) 。没有使用额外的空间。
进阶
看起来三分查找会比二分查找更快，但是为什么二分查找使用得更广泛？

二分查找和三分查找的比较
三分查找比二分查找效果要更差，下面的递归式求出了二分查找最坏情况下的渐近复杂度。

\begin{aligned} T(n) &= T\bigg(\frac{n}{2} \ \bigg) + 2, \quad T(1) = 1 \\ T\bigg(\frac{n}{2} \ \bigg) &= T\bigg(\frac{n}{2^2} \ \bigg) + 2 \\ \\ \therefore{} \quad T(n) &= T\bigg(\frac{n}{2^2} \ \bigg) + 2 \times 2 \\ &= T\bigg(\frac{n}{2^3} \ \bigg) + 3 \times 2 \\ &= \ldots \\ &= T\bigg(\frac{n}{2^{\log_2 n}} \ \bigg) + 2 \log_2 n \\ &= T(1) + 2 \log_2 n \\ &= 1 + 2 \log_2 n \end{aligned}
T(n)
T( 
2
n
​	
  )
∴T(n)
​	
  
=T( 
2
n
​	
  )+2,T(1)=1
=T( 
2 
2
 
n
​	
  )+2
=T( 
2 
2
 
n
​	
  )+2×2
=T( 
2 
3
 
n
​	
  )+3×2
=…
=T( 
2 
log 
2
​	
 n
 
n
​	
  )+2log 
2
​	
 n
=T(1)+2log 
2
​	
 n
=1+2log 
2
​	
 n
​	
 

下面的递归式求出了三分查找最坏情况下的渐近时间复杂度。

\begin{aligned} T(n) &= T\bigg(\frac{n}{3} \ \bigg) + 4, \quad T(1) = 1 \\ T\bigg(\frac{n}{3} \ \bigg) &= T\bigg(\frac{n}{3^2} \ \bigg) + 4 \\ \\ \therefore{} \quad T(n) &= T\bigg(\frac{n}{3^2} \ \bigg) + 2 \times 4 \\ &= T\bigg(\frac{n}{3^3} \ \bigg) + 3 \times 4 \\ &= \ldots \\ &= T\bigg(\frac{n}{3^{\log_3 n}} \ \bigg) + 4 \log_3 n \\ &= T(1) + 4 \log_3 n \\ &= 1 + 4 \log_3 n \end{aligned}
T(n)
T( 
3
n
​	
  )
∴T(n)
​	
  
=T( 
3
n
​	
  )+4,T(1)=1
=T( 
3 
2
 
n
​	
  )+4
=T( 
3 
2
 
n
​	
  )+2×4
=T( 
3 
3
 
n
​	
  )+3×4
=…
=T( 
3 
log 
3
​	
 n
 
n
​	
  )+4log 
3
​	
 n
=T(1)+4log 
3
​	
 n
=1+4log 
3
​	
 n
​	
 

如上所示，二分查找和三分查找最坏情况的比较是 1 + 4 \log_3 n1+4log 
3
​	
 n 和 1 + 2 \log_2 n1+2log 
2
​	
 n 之间的比较。为了比较哪个更大，我们只需要比较表达式 2 \log_3 n2log 
3
​	
 n 和 \log_2 nlog 
2
​	
 n 。表达式 2 \log_3 n2log 
3
​	
 n 可以被写作 \frac{2}{\log_2 3} \times \log_2 n 
log 
2
​	
 3
2
​	
 ×log 
2
​	
 n 。因此 \frac{2}{\log_2 3} 
log 
2
​	
 3
2
​	
  比 1 大，所以最坏情况下三分查找比较次数比二分查找最坏情况要多。
   
 
*/