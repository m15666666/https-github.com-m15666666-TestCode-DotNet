using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个整数，编写一个函数来判断它是否是 2 的幂次方。

示例 1:

输入: 1
输出: true
解释: 20 = 1
示例 2:

输入: 16
输出: true
解释: 24 = 16
示例 3:

输入: 218
输出: false

*/
/// <summary>
/// https://leetcode-cn.com/problems/power-of-two/
/// 231. 2的幂
/// 
/// 
/// 
/// 
/// </summary>
class PowerOfTwoSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public bool IsPowerOfTwo(int n) {
        if (n == 0) return false;
        long x = n;
        return (x & (-x)) == x;
    }

}
/*
2的幂
力扣 (LeetCode)
发布于 2020-01-14
17.5k
概述
我们不打算在这里讨论时间复杂度为 \mathcal{O}(\log N)O(logN) 的解决方案。


class Solution {
  public boolean isPowerOfTwo(int n) {
    if (n == 0) return false;
    while (n % 2 == 0) n /= 2;
    return n == 1;
  }
}
该问题将通过位运算在 \mathcal{O}(1)O(1) 的时间复杂度解决，通过使用如下的按位技巧：

如何获取二进制中最右边的 1：x & (-x)。
如何将二进制中最右边的 1 设置为 0：x & (x - 1)。
以下的两种解决方案背后的思想都是一样的：2 的幂在二进制中是有一个 1 后跟一些 0：

1 = (0000 0001)_21=(00000001) 
2
​	
 

2 = (0000 0010)_22=(00000010) 
2
​	
 

4 = (0000 0100)_24=(00000100) 
2
​	
 

8 = (0000 1000)_28=(00001000) 
2
​	
 

不是 2 的幂的二进制中有一个以上的 1。

3 = (0000 0011)_23=(00000011) 
2
​	
 

5 = (0000 0101)_25=(00000101) 
2
​	
 

6 = (0000 0110)_26=(00000110) 
2
​	
 

7 = (0000 0111)_27=(00000111) 
2
​	
 

除了 0，我们应该单独处理。

方法一：位运算：获取二进制中最右边的 1
算法：

获取最右边的 1：
首先讨论为什么 x & (-x) 可以获取到二进制中最右边的 1，且其它位设置为 0。

在补码表示法中，-x = \lnot x + 1−x=¬x+1。换句话说，要计算 -x−x，则要将 xx 所有位取反再加 1。

在二进制表示中，\lnot x + 1¬x+1 表示将该 1 移动到 \lnot x¬x 中最右边的 0 的位置上，并将所有较低位的位设置为 0。而 \lnot x¬x 最右边的 0 的位置对应于 xx 最右边的 1 的位置。

总而言之，-x = \lnot x + 1−x=¬x+1，此操作将 xx 所有位取反，但是最右边的 1 除外。

在这里插入图片描述
因此，xx 和 -x−x 只有一个共同点：最右边的 1。这说明 x & (-x) 将保留最右边的 1。并将其他的位设置为 0。

在这里插入图片描述

检测是否为 2 的幂：

我们通过 x & (-x) 保留了最右边的 1，并将其他位设置为 0 若 x 为 2 的幂，则它的二进制表示中只包含一个 1，则有 x & (-x) = x。

若 x 不是 2 的幂，则在二进制表示中存在其他 1，因此 x & (-x) != x。

因此判断是否为 2 的幂的关键是：判断 x & (-x) == x。

在这里插入图片描述


class Solution {
  public boolean isPowerOfTwo(int n) {
    if (n == 0) return false;
    long x = (long) n;
    return (x & (-x)) == x;
  }
}
复杂度分析

时间复杂度：O(1)O(1)。
空间复杂度：O(1)O(1)。
方法二：位运算：去除二进制中最右边的 1
算法：

去除二进制中最右边的 1：

首先讨论为什么 x & (x - 1) 可以将最右边的 1 设置为 0。

(x - 1) 代表了将 x 最右边的 1 设置为 0，并且将较低位设置为 1。

再使用与运算：则 x 最右边的 1 和就会被设置为 0，因为 1 & 0 = 0。

在这里插入图片描述

检测是否为 2 的幂：

2 的幂二进制表示只含有一个 1。
x & (x - 1) 操作会将 2 的幂设置为 0，因此判断是否为 2 的幂是：判断 x & (x - 1) == 0。
在这里插入图片描述


class Solution {
  public boolean isPowerOfTwo(int n) {
    if (n == 0) return false;
    long x = (long) n;
    return (x & (x - 1)) == 0;
  }
}
复杂度分析

时间复杂度：O(1)O(1)。
空间复杂度：O(1)O(1)。

public class Solution {
    public bool IsPowerOfTwo(int n) {
            uint a = 1;
            for (int i = 0; i < 32; i++)
            {
                if (a == n) return true;
                a = a << 1;
            }
            return false;
    }
}

public class Solution {
    public bool IsPowerOfTwo(int n) {
        return (n > 0 ) && ((n & ( -n )) == n);
    }
}

public class Solution {
    public bool IsPowerOfTwo(int n) {
        if(n == 0)
            return false;
        while(n % 2 == 0)
            n /= 2;
        return n == 1;
    }
}

public class Solution {
    public bool IsPowerOfTwo(int n) {
        //当n = 4时，二进制为：0100，n - 1 = 3，二进制为：0011，则：n & (n - 1) == 0
        //当n = 8时，为1000，n - 1 = 7，为0111，则n & (n - 1) == 0
        //再举个反例：当n = 5，为0101，n - 1为0100，则n & (n - 1) = 0100 = 4 != 0
        //从上面我们可以看出，凡是2的幂，均是二进制数的某一高位为1，且仅此高位为1，比如4，0100；8，1000。那么它的n - 1就变成了1所处的高位变成0，剩余低位变成1，如4 - 1,0011,8 - 1,0111，那么n & (n - 1)必为0
        if (n <= 0)
        {
            return false;
        }
        return (n & (n - 1)) == 0;
    }
}


 
*/