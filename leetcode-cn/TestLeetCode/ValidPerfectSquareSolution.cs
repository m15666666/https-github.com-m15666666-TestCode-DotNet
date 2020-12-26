/*
给定一个正整数 num，编写一个函数，如果 num 是一个完全平方数，则返回 True，否则返回 False。

说明：不要使用任何内置的库函数，如  sqrt。

示例 1：

输入：16
输出：True
示例 2：

输入：14
输出：False

*/

/// <summary>
/// https://leetcode-cn.com/problems/valid-perfect-square/
/// 367. 有效的完全平方数
///
/// </summary>
internal class ValidPerfectSquareSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public bool IsPerfectSquare(int num)
    {
        if (num < 2) return true;
        long x = num / 2;
        while (num < x * x) x = (x + num / x) / 2;
        return x * x == num;
    }
}

/*
有效的完全平方数
力扣 (LeetCode)
发布于 2019-12-23
18.8k
概述：
平方根相关问题通常可以在对数时间内求解。这里列出了从最坏到最好的三种标准对数时间的方法：

递归
二分查找
牛顿迭代法
后面两个算法是最有趣的，让我们详细的讨论它。

这些解决方法都有相同的起点。num 是一个有效的完全平方数若 x * x == \textrm{num}x∗x==num。

方法一：二分查找
若 num < 2，返回 true。
设置左边界为 2，右边界为 num/2。
当 left <= right：
令 x = (left + right) / 2 作为一个猜测，计算 guess_squared = x * x 与 num 做比较：
如果 guess_squared == num，则 num 是一个完全平方数，返回 true。
如果 guess_squared > num ，设置右边界 right = x-1。
否则设置左边界为 left = x+1。
如果在循环体内没有找到，则说明 num 不是完全平方数，返回 false。
在这里插入图片描述

算法：


class Solution {
  public boolean isPerfectSquare(int num) {
    if (num < 2) {
      return true;
    }

    long left = 2, right = num / 2, x, guessSquared;
    while (left <= right) {
      x = left + (right - left) / 2;
      guessSquared = x * x;
      if (guessSquared == num) {
        return true;
      }
      if (guessSquared > num) {
        right = x - 1;
      } else {
        left = x + 1;
      }
    }
    return false;
  }
}
复杂度分析

时间复杂度：\mathcal{O}(\log N)O(logN)。
空间复杂度：\mathcal{O}(1)O(1)。
方法二：牛顿迭代法
牛顿迭代法：公式是如何推导的呢？让我们做一个非常粗略的推导。

问题是找出：f(x) = x^2 - \textrm{num} = 0f(x)=x 
2
 −num=0 的根。

牛顿迭代法的思想是从一个初始近似值开始，然后作一系列改进的逼近根的过程。
在这里插入图片描述
举个例子：我们取 x_kx 
k
​	
  作为根的初始近似值，然后在 (x_k，f(x_k))(x 
k
​	
 ，f(x 
k
​	
 )) 处做切线与 xx 轴相交经过 x_{k+1}x 
k+1
​	
 。

通过斜率可写等价公式 \frac{f(x_k)}{x_k-x_{k+1}} = f'(x_k) 
x 
k
​	
 −x 
k+1
​	
 
f(x 
k
​	
 )
​	
 =f 
′
 (x 
k
​	
 )

转换后得 x_{k + 1} = x_k - \frac{f(x_k)}{f'(x_k)}x 
k+1
​	
 =x 
k
​	
 − 
f 
′
 (x 
k
​	
 )
f(x 
k
​	
 )
​	
 

将以下公式代入

f(x_k) = x_k^2 - \textrm{num}f(x 
k
​	
 )=x 
k
2
​	
 −num
f'(x_k) = 2x_kf 
′
 (x 
k
​	
 )=2x 
k
​	
 
得到 x_{k + 1} = \frac{1}{2}\left(x_k + \frac{\textrm{num}}{x_k}\right)x 
k+1
​	
 = 
2
1
​	
 (x 
k
​	
 + 
x 
k
​	
 
num
​	
 )

算法：

我们取 num/2 作为初始近似值。
当 x * x > num，用牛顿迭代法取计算下一个近似值：x = \frac{1}{2}\left(x + \frac{\textrm{num}}{x}\right)x= 
2
1
​	
 (x+ 
x
num
​	
 )。
返回 x*x == num。
在这里插入图片描述

class Solution {
  public boolean isPerfectSquare(int num) {
    if (num < 2) return true;

    long x = num / 2;
    while (x * x > num) {
      x = (x + num / x) / 2;
    }
    return (x * x == num);
  }
}
复杂度分析

时间复杂度：\mathcal{O}(\log N)O(logN)。
空间复杂度：\mathcal{O}(1)O(1)。

public class Solution {
    public bool IsPerfectSquare(int num) {
        int a = num % 10;
        if (a != 0 && a != 1 && a != 4 && a != 5 && a != 6 && a != 9) 
        {
            return false;
        }
        int left = 0;
        int right = num;
        while (left <= right) 
        {
            int mid = left + (right - left) / 2;
            if ((long)mid * mid == num)
            {
                return true;
            }
            else if (num > (long)mid * mid) 
            {
                left = mid + 1;
            }
            else
            {
                right = mid - 1;
            }
        }
        return false;
    }
}
*/