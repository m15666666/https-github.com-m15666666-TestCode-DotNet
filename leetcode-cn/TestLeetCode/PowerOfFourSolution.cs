/*
给定一个整数，写一个函数来判断它是否是 4 的幂次方。如果是，返回 true ；否则，返回 false 。

整数 n 是 4 的幂次方需满足：存在整数 x 使得 n == 4x

示例 1：

输入：n = 16
输出：true
示例 2：

输入：n = 5
输出：false
示例 3：

输入：n = 1
输出：true
 
提示：

-231 <= n <= 231 - 1

*/

/// <summary>
/// https://leetcode-cn.com/problems/power-of-four/
/// 342. 4的幂
///
/// </summary>
internal class PowerOfFourSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public bool IsPowerOfFour(int n)
    {
        return (0 < n) && ((n & (n - 1)) == 0) && ((n & 0xaaaaaaaa) == 0);
    }
}

/*
4的幂
力扣 (LeetCode)
发布于 2020-01-02
11.3k
概述
我们如何检查一个数是否为 2 的幂：x > 0 and x & (x - 1) == 0。我们将这个当作已知去解决该问题。

这里有个较为明显的方法去解决它，我们不打算在这里进行讨论。


class Solution {
  public boolean isPowerOfTwo(int n) {
    if (n == 0) return false;
    while (n % 4 == 0) n /= 4;
    return n == 1;
  }
}
我们只讨论 \mathcal{O}(1)O(1) 时间复杂度和 \mathcal{O}(1)O(1) 空间复杂度的算法。

方法一：暴力法 + 预计算
我们提前计算所有可能答案。

我们知道输入的整数是 32 位整数 x \le 2^{31} - 1x≤2 
31
 −1。因此我们最大 4 的幂次为 [\log_4\left(2^{31} - 1\right)] = 15[log 
4
​	
 (2 
31
 −1)]=15，那么我们总共有 16 种可能：4^04 
0
 , 4^14 
1
 , 4^24 
2
 , ..., 4^{15}4 
15
 。我们预计算全部可能，然后运行时检查输入数字是否在预计算列表中。

算法：


class Powers {
  private int n = 15;
  public List<Integer> nums = new ArrayList();
  Powers() {
    int lastNum = 1;
    nums.add(lastNum);
    for (int i = 1; i < n + 1; ++i) {
      lastNum = lastNum * 4;
      nums.add(lastNum);
    }
  }
}

class Solution {
  public static Powers p = new Powers();
  public boolean isPowerOfFour(int num) {
    return p.nums.contains(num);
  }
}
复杂度分析

时间复杂度：O(1)O(1)。
空间复杂度：O(1)O(1)。
方法二：数学运算
算法：
如果数字为 4 的幂 x = 4^ax=4 
a
 ，则 a = \log_4 x = \frac{1}{2}\log_2 xa=log 
4
​	
 x= 
2
1
​	
 log 
2
​	
 x 应为整数，那么我们检查 \log_2 xlog 
2
​	
 x 是否为偶数就能判断 x 是否为 4 的幂。


class Solution {
  public boolean isPowerOfFour(int num) {
    return (num > 0) && (Math.log(num) / Math.log(2) % 2 == 0);
  }
}
复杂度分析

时间复杂度：O(1)O(1)。
空间复杂度：O(1)O(1)。
方法三：位操作
算法：

我们首先检查 num 是否为 2 的幂：x > 0 and x & (x - 1) == 0。

现在的问题是区分 2 的偶数幂（当 xx 是 4 的幂时）和 2 的奇数幂（当 xx 不是 4 的幂时）。在二进制表示中，这两种情况都只有一位为 1，其余为 0。

有什么区别？在第一种情况下（4 的幂），1 处于偶数位置：第 0 位、第 2 位、第 4 位等；在第二种情况下，1 处于奇数位置。
在这里插入图片描述

因此 4 的幂与数字 (101010...10)_2(101010...10) 
2
​	
  向与会得到 0。即 4^a \land (101010...10)_2 == 04 
a
 ∧(101010...10) 
2
​	
 ==0。

(101010...10)_2(101010...10) 
2
​	
  用十六进制表示为 ：(aaaaaaaa)_{16}(aaaaaaaa) 
16
​	
 。


class Solution {
  public boolean isPowerOfFour(int num) {
    return (num > 0) && ((num & (num - 1)) == 0) && ((num & 0xaaaaaaaa) == 0);
  }
}
复杂度分析

时间复杂度：O(1)O(1)。
空间复杂度：O(1)O(1)。
方法四：位运算 + 数学运算
算法：

我们首先检车 xx 是否为 2 的幂：x > 0 and x & (x - 1) == 0。然后可以确定 x = 2^ax=2 
a
 ，若 xx 为 4 的幂则 aa 为偶数。
下一步是考虑 a=2ka=2k 和 a=2k+1a=2k+1 两种情况，对 xx 对 3 进行取模：
(2^{2k} \mod 3) = (4^k \mod 3) = ((3 + 1)^k \mod 3) = 1(2 
2k
 mod3)=(4 
k
 mod3)=((3+1) 
k
 mod3)=1

((2^{2k + 1}) \mod 3) = ((2 \times 4^k) \mod 3) = ((2 \times(3 + 1)^k) \mod 3) = 2((2 
2k+1
 )mod3)=((2×4 
k
 )mod3)=((2×(3+1) 
k
 )mod3)=2

若 xx 为 2 的幂且 x%3 == 1，则 xx 为 4 的幂。

class Solution {
  public boolean isPowerOfFour(int num) {
    return (num > 0) && ((num & (num - 1)) == 0) && (num % 3 == 1);
  }
}
工作原理：mod 的计算过程

我们通过计算 x = 2^{2k} \mod 3x=2 
2k
 mod3 来理解：

首先 2^{2k} = {2^2}^k = 4^k2 
2k
 =2 
2
  
k
 =4 
k
  且 4 = 3 + 14=3+1。
则 xx 可以写为 x = ((3 + 1)^k \mod 3)x=((3+1) 
k
 mod3)。
进行分解：(3 + 1)^k = (3 + 1) \times (3 + 1)^{k - 1} = 3 \times (3 + 1)^{k - 1} + (3 + 1)^{k - 1}(3+1) 
k
 =(3+1)×(3+1) 
k−1
 =3×(3+1) 
k−1
 +(3+1) 
k−1
 。
且 (3 \times (3 + 1)^{k - 1}) \mod 3 = 0(3×(3+1) 
k−1
 )mod3=0，则我们可以变换得 x = ((3 + 1)^{k - 1} \mod 3)x=((3+1) 
k−1
 mod3)。
我们可以继续进行以上操作 k -> k - 1 -> k - 2 -> ... -> 1k−>k−1−>k−2−>...−>1 最后得到 x = ((3 + 1)^1 \mod 3) = 1x=((3+1) 
1
 mod3)=1.
复杂度分析

时间复杂度：O(1)O(1)。
空间复杂度：O(1)O(1)。

public class Solution {
    public bool IsPowerOfFour(int num) {
        if (num < 1) 
            return false;
        while (num > 1) {
            if (num % 4 == 0) {
                //num /= 4;
                num >>= 2;
            } else {
                return false;
            }
        }
        return true;
    }
}

public class Solution {
    public bool IsPowerOfFour(int n) {
        if (n <= 0)
			return false;
        //先判断是否是 2 的幂
		if ((n & n - 1) != 0)
			return false;
        //如果与运算之后是本身则是 4 的幂
		if ((n & 0x55555555) == n)
			return true;
		return false;
    }
}

public class Solution {
    public bool IsPowerOfFour(int n) {
        if( n==0)
            return false;
        if( n == 1 )
            return true;
        if( n%4 != 0)
            return false;
        return IsPowerOfFour(n/4);
    }
}



*/