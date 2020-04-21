using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
实现 pow(x, n) ，即计算 x 的 n 次幂函数。

示例 1:

输入: 2.00000, 10
输出: 1024.00000
示例 2:

输入: 2.10000, 3
输出: 9.26100
示例 3:

输入: 2.00000, -2
输出: 0.25000
解释: 2-2 = 1/22 = 1/4 = 0.25
说明:

-100.0 < x < 100.0
n 是 32 位有符号整数，其数值范围是 [−231, 231 − 1] 。
*/
/// <summary>
/// https://leetcode-cn.com/problems/powx-n/
/// 50. Pow(x,n)
/// 
/// </summary>
class MyPowSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public double MyPow(double x, int n)
    {
        if (x == 0) return 0;
        if (n == 0) return 1;
        if (n == 1) return x;
        if (n == -1) return 1 / x;

        long n1 = n;
        if (n1 < 0) 
        {
            x = 1 / x;
            n1 = -n1;
        }
        return FastPow(x, n1);

        double FastPow(double x, long n) 
        {
            if (n == 0) return 1.0;
            
            double half = FastPow(x, n / 2);
            if (n % 2 == 0) return half * half;
            else return half * half * x;
        }
    }

    //public double MyPow(double x, int n)
    //{
    //    if (x == 0) return 0;
    //    if (n == 0) return 1;
    //    if (n == 1) return x;
    //    if (n == -1) return 1/x;

    //    Dictionary<long, double> pow2v = new Dictionary<long, double>();
    //    long n1 = n;
    //    if( n1 < 0)
    //    {
    //        x = 1.0 / x;
    //        n1 = -n1;
    //    }

    //    double x1 = x;
    //    long pow = 1;

    //    pow2v[pow] = x1;
    //    while ( pow < n1 )
    //    {
    //        x1 = x1 * x1;
    //        pow *= 2;
    //        pow2v[pow] = x1;
    //    }

    //    if (pow == n1) return x1;

    //    double ret = 1;
    //    while( 0 < n1 )
    //    {
    //        if( pow <= n1 )
    //        {
    //            ret *= x1;

    //            n1 -= pow;
    //            continue;
    //        }

    //        pow /= 2;
    //        x1 = pow2v[pow];
    //    }

    //    return ret;
    //}
}
/*
Pow(x, n)
力扣 (LeetCode)
发布于 1 年前
41.8k
方法 1：蛮力
直观想法

直接模拟该过程，将 x 连乘 n 次。

如果 n < 0n<0，我们可以用\dfrac{1}{x}, -n 
x
1
​	
 ,−n 代替 x, nx,n 来保证 n \ge 0n≥0 。这个限制可以简化我们下面的讨论。

但是我们仍需关注边界条件，尤其是正整数和负整数的不同范围限制。

算法

我们使用一个直接的循环来计算结果。

class Solution {
public:
    double myPow(double x, int n) {
        long long N = n;
        if (N < 0) {
            x = 1 / x;
            N = -N;
        }
        double ans = 1;
        for (long long i = 0; i < N; i++)
            ans = ans * x;
        return ans;
    }
};
复杂度分析

时间复杂度：O(n)O(n). 我们需要将 x 连乘 n 次。

空间复杂度：O(1)O(1). 我们只需要一个变量来保存最终 x 的连乘结果。




方法2：快速幂算法（递归）
直观想法

假定我们已经得到了 x ^ nx 
n
  的结果，我们如何得到 x ^ {2 * n}x 
2∗n
  的结果？很明显，我们不需要将 x 再乘 n 次。使用公式 (x ^ n) ^ 2 = x ^ {2 * n}(x 
n
 ) 
2
 =x 
2∗n
  ，我们可以在一次计算内得到 x ^ {2 * n}x 
2∗n
  的值。使用该优化方法，我们可以降低算法的时间复杂度。

算法

假定我们已经得到了 x ^ {n / 2}x 
n/2
  的结果，并且我们现在想得到 x ^ nx 
n
  的结果。我们令 A 是 x ^ {n / 2}x 
n/2
  的结果，我们可以根据 n 的奇偶性来分别讨论 x ^ nx 
n
  的值。如果 n 为偶数，我们可以用公式 (x ^ n) ^ 2 = x ^ {2 * n}(x 
n
 ) 
2
 =x 
2∗n
  来得到 x ^ n = A * Ax 
n
 =A∗A。如果 n 为奇数，那么 A * A = x ^ {n - 1}A∗A=x 
n−1
 。直观上看，我们需要再乘一次 xx ，即 x ^ n = A * A * xx 
n
 =A∗A∗x。该方法可以很方便的使用递归实现。我们称这种方法为 "快速幂"，因为我们只需最多 O(\log n)O(logn) 次运算来得到 x ^ nx 
n
 。

class Solution {
public:
    double fastPow(double x, long long n) {
        if (n == 0) {
            return 1.0;
        }
        double half = fastPow(x, n / 2);
        if (n % 2 == 0) {
            return half * half;
        } else {
            return half * half * x;
        }
    }
    double myPow(double x, int n) {
        long long N = n;
        if (N < 0) {
            x = 1 / x;
            N = -N;
        }
        return fastPow(x, N);
    }
};
复杂度分析

时间复杂度：O(\log n)O(logn). 每一次我们使用公式 (x ^ n) ^ 2 = x ^ {2 * n}(x 
n
 ) 
2
 =x 
2∗n
 , nn 都变为原来的一半。因此我们需要至多 O(\log n)O(logn) 次操作来得到结果。

空间复杂度：O(\log n)O(logn). 每一次计算，我们需要存储 x ^ {n / 2}x 
n/2
  的结果。 我们需要计算 O(\log n)O(logn) 次，所以空间复杂度为 O(\log n)O(logn) 。




方法 3：快速幂算法（循环）
直观想法

使用公式 x ^ {a + b} = x ^ a * x ^ bx 
a+b
 =x 
a
 ∗x 
b
 ，我们可以将 n 看做一系列正整数之和，n = \sum_i b_in=∑ 
i
​	
 b 
i
​	
 。如果我们可以很快得到 x ^ {b_i}x 
b 
i
​	
 
  的结果，计算 x ^ nx 
n
  的总时间将被降低。

算法

我们可以使用 n 的二进制表示来更好的理解该问题。使 n 的二进制从最低位 (LSB) 到最高位 (MSB) 表示为b_1, b_2, ..., b_{length\_limit}b 
1
​	
 ,b 
2
​	
 ,...,b 
length_limit
​	
  。对于第 i 位为，如果 b_i = 1b 
i
​	
 =1 ，意味着我们需要将结果累乘上 x ^ {2 ^ i}x 
2 
i
 
 。

这似乎不能有效率上的提升，因为 \sum_i b_i * 2 ^ i = n∑ 
i
​	
 b 
i
​	
 ∗2 
i
 =n 。但是使用上面提到的公式 (x ^ n) ^ 2 = x ^ {2 * n}(x 
n
 ) 
2
 =x 
2∗n
  ，我们可以进行改进。初始化 x ^ 1 = xx 
1
 =x ，对于每一个 $ i > 1$ ，我们可以在一步操作中使用 x ^ {2 ^ {i - 1}}x 
2 
i−1
 
  来得到 x ^ {2 ^ i}x 
2 
i
 
  。由于 b_ib 
i
​	
  的个数最多为 O(\log n)O(logn) ，我们可以在 O(\log n)O(logn) 的时间内得到所有的 x ^ {2 ^ i}x 
2 
i
 
  。在那之后，对于所有满足 b_i = 1b 
i
​	
 =1 的 i，我们可以用结果累乘 x ^ {2 ^ i}x 
2 
i
 
  。这也只需要 O(\log n)O(logn) 的时间。

使用快速幂的递归或循环方法是采用不同的方式但是实现了同样的目标。对于更多快速幂方法的介绍，可以查阅相关资料。

class Solution {
public:
    double myPow(double x, int n) {
        long long N = n;
        if (N < 0) {
            x = 1 / x;
            N = -N;
        }
        double ans = 1;
        double current_product = x;
        for (long long i = N; i ; i /= 2) {
            if ((i % 2) == 1) {
                ans = ans * current_product;
            }
            current_product = current_product * current_product;
        }
        return ans;
    }
};
复杂度分析

时间复杂度：O(\log n)O(logn). 对每一个 n 的二进制位表示，我们都至多需要累乘 1 次，所以总的时间复杂度为 O(\log n)O(logn) 。
空间复杂的：O(1)O(1). 我们只需要用到 2 个变量来保存当前的乘积和最终的结果 x 。
下一篇：C++ ✍快速幂(递归与迭代
 
public class Solution {
    public double MyPow(double x, int n) {
        long N = n;

        if(n < 0){
            N = -1*N;
            x = 1/x;
        }

        double ans = 1;
        double powBase = x;

        while(N!= 0){
              
              if(N%2 != 0){
                  ans  = ans * powBase;
              }

              powBase*=powBase;
              N/=2;


        }

        return ans;
    }
}

*/